using System.ComponentModel.DataAnnotations;

namespace HeartInsurance.Web.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email required.")]
        [Display(Name = "Enter your email", Prompt = "Enter your email")]
        public string Email { get; set; }
    }
}
