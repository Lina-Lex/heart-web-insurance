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
        private readonly IEmailManager<ApplicationUser> emailManager;
        private readonly GeneratePassCode passCode;

        public SystemUserActions(UserManager<ApplicationUser> userManager, ILogger<SystemUserActions> logger,
            IEmailSender emailSender, IEmailManager<ApplicationUser> emailManager, GeneratePassCode passCode)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.emailManager = emailManager;
            this.passCode = passCode;
        }

        public async Task<ResponseModel> EmailConfirmation(string email, string token)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel<BaseUserResponse>> SignIn(string email)
        {
            var response = new ResponseModel<BaseUserResponse>();
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    if (user.EmailConfirmed) 
                    {
                        var codeValue = await passCode.OneTimePassCode(email);
                        logger.LogInformation($"Passcode generated: ==> {codeValue}");
                        if(codeValue.Code != null)
                        {
                            //sending pass code to user's email
                            await emailSender.SendEmailAsync(new EmailSetting
                            {
                                To = user.Email,
                                RecipientName = user.EncFullName,
                                Message = $"<div><p>Your Login Pass code is: {codeValue}.{string.Concat(Enumerable.Repeat(Environment.NewLine, 2))} Expires in 60 minutes </p></div>",
                                Subject = "Sign In Authentication",
                                CancellationToken = CancellationToken.None
                            });
                            response.Status = true;
                            response.Data = new BaseUserResponse
                            {
                                ApplicationUserId = user.Id,
                                Name = user.EncFullName,
                                Email = user.Email
                            };
                            response.Message = "Passcode has been generated and sent to your email. It will expires in 60 munites";
                        }
                        else
                        {
                            response.Status = false;
                            response.Message = "Internal server error on passcode generation";
                            logger.LogInformation("Passcode failed to be generated");
                        }
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Email not confirmed yet | Please confirm your email address.";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Unauthorized -> Error occured";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"Error encountered: ==> {ex}");
                throw;
            }

            return response;
        }

        public async Task<ResponseModel<ApplicationUserResponse>> SignUp(ApplicationUserModel model)
        {
            try
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
                        EncFullName = model.Name, // this will be encrypted for enhancing security.
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };
                    var result = await userManager.CreateAsync(user, FirstTimePasswordGeneration.CreatePassword(model.Email));
                    logger.LogInformation($"Sign in requested payload ===> {result}");

                    if (result.Succeeded)
                    {
                        logger.LogInformation($"Sign in was successful for the request payload ===> {result}");
                        //Send email confirmation to user
                        emailManager.SendEmailConfirmationMail(user);
                        return new ResponseModel<ApplicationUserResponse>
                        {
                            Status = true,
                            Message = "User created successfully!",
                            ResponseStatusCodes = ResponseCodes.SUCCESSFUL,
                            Data = new ApplicationUserResponse
                            {
                                ApplicationUserId = user.Id,
                                Email = user.Email,
                                Name = user.EncFullName,
                                Token = (string)await emailManager.EmailToken(user)
                            }
                        };
                    }
                    return new ResponseModel<ApplicationUserResponse>
                    {
                        Status = false,
                        Message = "User creation failed! Please check user details and try again.",
                        ResponseStatusCodes = ResponseCodes.FAILED,
                        Data = null
                    };
                }

                logger.LogCritical("Sign in information already existed");
                return new ResponseModel<ApplicationUserResponse>
                {
                    Status = false,
                    Message = "User already existed!",
                    ResponseStatusCodes = ResponseCodes.USER_ALREADY_EXIST,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                logger.LogError($"Error encountered ==> {ex}");
                throw;
            }
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
                    else
                    {
                        response.Status = false;
                        response.Message = "Authentication failed";
                    }
                   
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
