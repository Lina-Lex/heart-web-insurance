using Application.Common.Helpers.PasswordHelper;
using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces.Application;
using Domain.Entities;
using Infrastructure.Services.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class SystemUserActions : ISystemUserActions
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<SystemUserActions> logger;
        private readonly IEmailSender emailSender;
        private readonly GeneratePassCode passCode;

        public SystemUserActions(UserManager<ApplicationUser> userManager, ILogger<SystemUserActions> logger,
            IEmailSender emailSender, GeneratePassCode passCode)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.passCode = passCode;
        }

        public async Task<ResponseModel> EmailConfirmation(string email, string token)
        {
            var response = new ResponseModel();
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                response.Status = false;
                response.Message = "User not found.";
                response.ResponseStatusCodes = ResponseCodes.USER_NOTFOUND;
            }
            
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                response.Status = false;
                response.Message = "Sorry! An error occurred while processing your request. Please try again later.";
                response.ResponseStatusCodes = ResponseCodes.FAILED;
            }

            await emailSender.SendEmailAsync(new EmailSetting 
            {
                To = user.Email,
                Message = "Your email was confirmed successfully." 
            });
            response.Status = true;
            response.Message = "Email confirmed successfully";
            response.ResponseStatusCodes = ResponseCodes.SUCCESSFUL;

            return response;
        }

        public async Task<ResponseModel> SignIn(string email)
        {
            var response = new ResponseModel();
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var codeValue = passCode.OneTimePassCode(email).ConfigureAwait(false);
                    //sending pass code to user's email
                    await emailSender.SendEmailAsync(new EmailSetting
                    {
                        To = user.Email,
                        RecipientName = user.FullName,
                        Message = $"<div><p>Your Login Passcode is: {codeValue}.{string.Concat(Enumerable.Repeat(Environment.NewLine, 2))} Expires in 60 minutes </p></div>",
                        Subject = "Sign In Authentication",
                        CancellationToken = CancellationToken.None
                    });
                    response.Status = true;
                    response.Message = "Passcode has been generated and sent to your email. It will expires in 60 munites";
                }

                response.Status = false;
                response.Message = "Unauthorized -> Error occured";

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> SignUp(ApplicationUserModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists == null)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NormalizedUserName = model.Email.Normalize().ToUpper(),
                    NormalizedEmail = model.Email.Normalize().ToUpper(),
                    FullName = model.Name, // this can be encrypted for enhancing security.
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                var result = await userManager.CreateAsync(user, FirstTimePasswordGeneration.CreatePassword(model.Email));
                logger.LogInformation($"Sign in requested payload ===> {result}");

                if (result.Succeeded)
                {
                    logger.LogInformation($"Sign in was successful for the request payload ===> {result}");
                    //Send email confirmation to user
                    emailSender.SendConfirmationMail(user);
                    return new ResponseModel
                    {
                        Status = true,
                        Message = "User created successfully!",
                        ResponseStatusCodes = ResponseCodes.SUCCESSFUL,
                    };
                }
                return new ResponseModel
                {
                    Status = false,
                    Message = "User creation failed! Please check user details and try again.",
                    ResponseStatusCodes = ResponseCodes.FAILED,
                };
            }

            logger.LogCritical("Sign in information already existed");
            return new ResponseModel
            {
                Status = false,
                Message = "User already existed!",
                ResponseStatusCodes = ResponseCodes.USER_ALREADY_EXIST,
            };
        }

        public async Task<ResponseModel> VerifyPassCodeThenLogin(string email, string generatedValue)
        {
            var response = new ResponseModel();
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user != null && !generatedValue.Equals(null))
                {
                    bool isValidCode = passCode.VerifyPassCode(email, generatedValue);
                    if (isValidCode.Equals(true))
                    {
                        response.Status = true;
                        response.Message = "Authentication successful";
                    }

                    response.Status = false;
                    response.Message = "Authentication failed";
                    return response;
                }
                return ResponseModel.Failure("User not found or Pass code cannot be empty.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
