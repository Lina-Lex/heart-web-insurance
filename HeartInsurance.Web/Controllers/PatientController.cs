using HeartInsurance.Web.Models;
using HeartInsurance.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace HeartInsurance.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        public IActionResult BloodPressure()
        {
            FetchDataGetPatient();
            var model = new BloodPressureViewModel();
            return View(model);
        }

        private void FetchDataGetPatient()
        {
            try
            {

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}