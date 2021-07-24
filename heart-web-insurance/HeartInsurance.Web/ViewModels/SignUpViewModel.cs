using System.ComponentModel.DataAnnotations;

namespace HeartInsurance.Web.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Email required.")]
        [Display(Name = "Enter your email", Prompt = "Enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name required.")]
        [Display(Name = "Enter your name", Prompt = "Enter your name")]
        public string Name { get; set; }
    }
}
