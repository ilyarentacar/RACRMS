using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.ManagementWebApp.Filters;
using RACRMS.ManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Controllers
{
    [UserCheck]
    [UserRoleCheck("Sistem Yöneticisi", "Administrator")]
    public class ProfileController : Controller
    {
        private readonly IUserBL userBL;
        private readonly IReservationBL reservationBL;

        public ProfileController(IUserBL userBL, IReservationBL reservationBL)
        {
            this.userBL = userBL;
            this.reservationBL = reservationBL;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber).Value);

                var user = await userBL.GetByIdAsync(userId);

                await getWaitingReservationCountasync();

                if (HttpContext.Session.Keys.Any(x => x == "ErrorMessage"))
                {
                    ViewBag.ErrorMessage = HttpContext.Session.GetString("ErrorMessage");
                    HttpContext.Session.Remove("ErrorMessage");
                }

                if (HttpContext.Session.Keys.Any(x => x == "SuccessMessage"))
                {
                    ViewBag.SuccessMessage = HttpContext.Session.GetString("SuccessMessage");
                    HttpContext.Session.Remove("SuccessMessage");
                }

                return View(new ProfileViewModel()
                {
                    User = user
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new ProfileViewModel());
            }
        }

        private async Task getWaitingReservationCountasync()
        {
            try
            {
                ViewBag.WaitingReservationCount = await reservationBL.GetWaitingReservationCountAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
