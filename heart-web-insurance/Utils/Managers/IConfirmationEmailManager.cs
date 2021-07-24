using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Managers
{
    public interface IConfirmationEmailManager
    {
        Task<object> GenerateEmailUrl(ApplicationUser user);
    }
}
