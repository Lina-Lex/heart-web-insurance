using HeartInsurance.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HeartInsurance.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        List<PatientModel> patients = new List<PatientModel>();

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            FetchData();
            return View(patients);
        }

        private void FetchData()
        {
            try
            {
                var arr = "[{\"index\": 1,\"firstName\": \"Kayque\",\"lastName\": \"Pereira\",\"contact\": \"5511999999990\"},{\"index\": 2,\"firstName\": \"Mila\",\"LastName\": \"Son\",\"Contact\": \"5511999999991\"},{\"index\": 3,\"firstName\": \"Michael\",\"LastName\": \"Gray\",\"Contact\": \"5511999999992\"},{\"index\": 4,\"firstName\": \"Erik\",\"LastName\": \"Dolberg\",\"Contact\": \"5511999999993\"},{\"index\": 5,\"firstName\": \"Joe\",\"LastName\": \"Kane\",\"Contact\": \"5511999999994\"},{\"index\": 6,\"firstName\": \"Dennis\",\"LastName\": \"Adams\",\"Contact\": \"5511999999995\"},{\"index\": 7,\"firstName\": \"Peter\",\"LastName\": \"Parker\",\"Contact\": \"5511999999996\"},{\"index\": 8,\"firstName\": \"Amanda\",\"LastName\": \"Valencia\",\"Contact\": \"5511999999997\"},{\"index\": 9,\"firstName\": \"John\",\"LastName\": \"Cena\",\"Contact\": \"5511999999998\"},{\"index\": 10,\"firstName\": \"Gabi\",\"LastName\": \"Batista\",\"Contact\": \"5511999999999\"},{\"index\": 11,\"firstName\": \"Mario\",\"LastName\": \"Cancelo\",\"Contact\": \"5511999999900\"}, {\"index\": 12,\"firstName\": \"Mary\",\"LastName\": \"Solo\",\"Contact\": \"5511999999901\"}]";

                patients = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PatientModel>>(arr);
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
