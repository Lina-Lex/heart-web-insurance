using System.ComponentModel.DataAnnotations;

namespace HeartInsurance.Web.ViewModels
{
    public class EmailConfirmationViewModel
    {
        [Required(ErrorMessage = "Token required.")]
        [Display(Name = "Token", Prompt = "Enter generated token")]
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
