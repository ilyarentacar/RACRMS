using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.DataTransferObject;
using RACRMS.ManagementWebApp.Filters;
using RACRMS.ManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Controllers
{
    [UserCheck]
    [UserRoleCheck("Sistem Yöneticisi", "Administrator")]
    public class CarPreferenceController : Controller
    {
        private readonly ICarPreferenceBL carPreferenceBL;
        private readonly ICarBL carBL;
        private readonly IPreferenceBL preferenceBL;
        private readonly IReservationBL reservationBL;

        public CarPreferenceController(ICarPreferenceBL carPreferenceBL, ICarBL carBL, IPreferenceBL preferenceBL, IReservationBL reservationBL)
        {
            this.carPreferenceBL = carPreferenceBL;
            this.carBL = carBL;
            this.preferenceBL = preferenceBL;
            this.reservationBL = reservationBL;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var carPreferences = await carPreferenceBL.GetAsync();

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

                return View(new CarPreferenceViewModel()
                {
                    CarPreferences = carPreferences
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new CarFuelTypeViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            try
            {
                var carPreferences = await carPreferenceBL.GetAsync();
                var cars = await carBL.GetAsync();
                var preferences = await preferenceBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarPreferenceViewModel(OpenInsertPopup: true)
                {
                    CarPreference = new CarPreferenceDTO()
                    {
                        Cars = cars,
                        Preferences = preferences.ToArray()
                    },
                    CarPreferences = carPreferences
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarPreferenceDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carPreferenceBL.BulkInsertAsync(model);

                HttpContext.Session.SetString("SuccessMessage", "Kayıt işlemi başarıyla tamamlanmıştır.");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var carPreference = await carPreferenceBL.GetByIdAsync(id);
                var carPreferences = await carPreferenceBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarPreferenceViewModel(OpenDeletePopup: true)
                {
                    CarPreference = carPreference,
                    CarPreferences = carPreferences
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarPreferenceDTO model)
        {
            try
            {
                await carPreferenceBL.DeleteAsync(model.Id);

                HttpContext.Session.SetString("SuccessMessage", "Silme işlemi başarıyla tamamlanmıştır.");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]

        public async Task<IActionResult> Cancel()
        {
            await getWaitingReservationCountasync();

            return RedirectToAction("Index");
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
