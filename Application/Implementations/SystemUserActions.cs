using Application.Common.Helpers.PasswordHelper;
using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces.Application;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class SystemUserActions : ISystemUserActions
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<SystemUserActions> logger;

        public SystemUserActions(UserManager<ApplicationUser> userManager, ILogger<SystemUserActions> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }
        public async Task<ResponseModel> SignUp(ApplicationUserModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists == null)
            {
                var result = await userManager.CreateAsync(new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NormalizedUserName = model.Email.Normalize().ToUpper(),
                    NormalizedEmail = model.Email.Normalize().ToUpper(),
                    FullName = model.Name, // this can be encrypted for enhancing security.
                    SecurityStamp = Guid.NewGuid().ToString(),
                }, FirstTimePasswordGeneration.CreatePassword(model.Email));
                logger.LogInformation($"Sign in requested payload ===> {result}");

                if (result.Succeeded)
                {
                    logger.LogInformation($"Sign in was successful for the request payload ===> {result}");
                    //Send email confirmation to user
                    //SendConfirmationMail(user, model.Email, string.Concat(model.FirstName, " ", model.LastName));
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
    }
}
