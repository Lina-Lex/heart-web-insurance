using System.ComponentModel.DataAnnotations;

namespace HeartInsurance.Web.ViewModels
{
    public class PassCodeValidationViewModel
    {
        [Required(ErrorMessage = "Pass code required.")]
        [Display(Name = "Pass code", Prompt = "Enter generated pass code")]
        public string CodeValue { get; set; }
        public string Email { get; set; }
    }
}
