using Application.Common.Responses;
using Application.Common.Utilities;
using Application.Helper;
using DAL.DataContext;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Helpers.PasswordHelper
{
    public abstract class PassCodeGeneration
    {
        public abstract string CreatePassCode();
        public abstract Task<PassCodeResponse> OneTimePassCode(string email);
        public abstract bool VerifyPassCode(string email, string generatedValue);
        protected abstract int PassCodeInterval(DateTime currentTime, DateTime initiatedTime);
    }

    public class GeneratePassCode : PassCodeGeneration
    {
        private readonly IApplicationDbContext dbContext;
        readonly Random Random;

        public GeneratePassCode(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Random = new Random();
        }
        public override string CreatePassCode()
        {
            var value = Random.Next().ToString("D20");
            if (value.Distinct().Count() == 1)
                value = CreatePassCode();

            var spanLength = value.AsSpan(0, 15);
            if (value.Length > 15)
                return spanLength.ToString();

            return value; // to be encrypted
        }

        public async override Task<PassCodeResponse> OneTimePassCode(string email)
        {
            var resposeResult = new PassCodeResponse();

            var passCode = dbContext.PassCode.FirstOrDefault(x => x.Email.Equals(email));
            if (passCode.Equals(null))
            {
                await dbContext.PassCode.AddAsync(new PassCode
                {
                    Email = email,
                    CodeValue = CreatePassCode(),
                    IsApplied = false,
                });

                resposeResult.Code = passCode.CodeValue;
            }
            else
            {
                passCode.CodeValue = CreatePassCode();
                resposeResult.Code = passCode.CodeValue;
                dbContext.PassCode.Update(passCode);
            }
                
            await dbContext.SaveChangesAsync();

            return resposeResult;
        }

        public override bool VerifyPassCode(string email, string generatedValue)
        {
            string uniqueId = "";
            bool isApplied = false;
            string storedCode = "";
            DateTime ValueInitiatedTime;

            var user = dbContext.PassCode.FirstOrDefault(x => x.Email.Equals(email));
            if (!user.Equals(null))
            {
                uniqueId = user.UniqueCodeId;
                isApplied = user.IsApplied;
                storedCode = user.CodeValue;
                ValueInitiatedTime = user.InitiatedTime;

                int CodeExpiredTime = PassCodeInterval(DateTime.UtcNow, ValueInitiatedTime);

                if (CodeExpiredTime <= Constants.PASSCODE_EXPIRED_TIME_IN_MINUTES)
                {
                    if (!isApplied && uniqueId != null)
                        if (storedCode.Equals(storedCode))
                        {
                            user.IsApplied = true;
                            user.LastCreatedOrUpdated = DateTime.UtcNow;

                            var updated = dbContext.PassCode.Update(user);
                            if (updated.State.Equals(EntityState.Modified))
                            {
                                dbContext.SaveChanges();
                                return true;
                            }
                        }
                        else
                            AppUtility.BrokerFailureMessage("Invalid Passcode entered.");
                }
                else
                    AppUtility.BrokerFailureMessage("Passoode has expired. Try and Login again.");
            }
            else
                AppUtility.BrokerFailureMessage("User not found. Request failed.");
            return false;
        }

        protected override int PassCodeInterval(DateTime currentTime, DateTime initiatedTime)
        {
            TimeSpan tSpan = currentTime - initiatedTime;
            var timeInMinutes = tSpan.TotalMinutes;
            return (int)timeInMinutes;
        }
    }
}
