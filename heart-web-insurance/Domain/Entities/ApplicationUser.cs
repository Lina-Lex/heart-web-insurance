using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string EncFullName { get; set; }
        public string OrganizationName { get; set; }
    }
}