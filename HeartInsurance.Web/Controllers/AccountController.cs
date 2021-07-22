using HeartInsurance.Web.DTOs.Requests;
using HeartInsurance.Web.Services.HeartInsuranceMicroservice.Interfaces;
using HeartInsurance.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeartInsurance.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHeartInsuranceService insuranceService;

        public AccountController(IHeartInsuranceService insuranceService)
        {
            this.insuranceService = insuranceService;
        }
        public IActionResult SignUp() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("Request", "Error found on request.");

            var req = new CreateAccountDTORequest
            {
                Email = model.Email,
                Name = model.Name
            };

            var result = await insuranceService.SignUp(req);
            if (result.Status.Equals(true))
            {
                TempData["User-Email"] = result.Data.Email;
                TempData["User-Token"] = result.Data.Token;
                ModelState.Clear();
                return RedirectToAction(nameof(ConfirmEmail), this);
            }                
            else
                ViewBag.Message = result.Message;

            return View(model);
        }
        
        public IActionResult SignIn() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("Request", "Error found on request.");

            var result = await insuranceService.SignIn(new LoginDTORequest { Email = model.Email });
            if (result.Status.Equals(true))
            {
                TempData["email"] = model.Email;
                ModelState.Clear();
                return RedirectToAction(nameof(ValidatePassCode), this);
            }
            else
                ViewBag.Message = result.Message;

            return View(model);
        }
        public IActionResult ConfirmEmail() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("Request", "Error found on request.");

            var result = await insuranceService.ConfirmEmail(new EmailConfirmationDTORequest 
            {
                Email = model.Email,
                Token = model.Token 
            });
            if (result.Status.Equals(true))
            {
                ModelState.Clear();
                return RedirectToAction(nameof(SignIn), this);
            }
            else
                ViewBag.Message = result.Message;

            return View(model);
        }
        
        public IActionResult ValidatePassCode() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidatePassCode(PassCodeValidationViewModel model)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("Request", "Error found on request.");

            var result = await insuranceService.ValidatePassCode(new ValidatePasscodeDTORequest
            {
                Email = model.Email,
                CodeValue = model.CodeValue
            });
            if (result.Status.Equals(true))
                ViewBag.Message = result.Message;
            else
                ViewBag.Message = result.Message;

            return View(model);
        }
    }
}
