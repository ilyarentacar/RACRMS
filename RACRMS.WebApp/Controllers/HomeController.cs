using Microsoft.AspNetCore.Mvc;
using RACRMS.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new HomeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return RedirectToAction("Index");
        }
    }
}
