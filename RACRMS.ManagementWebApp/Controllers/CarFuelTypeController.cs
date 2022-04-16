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
    public class CarFuelTypeController : Controller
    {
        private readonly ICarFuelTypeBL carFuelTypeBL;
        private readonly IReservationBL reservationBL;

        public CarFuelTypeController(ICarFuelTypeBL carFuelTypeBL, IReservationBL reservationBL)
        {
            this.carFuelTypeBL = carFuelTypeBL;
            this.reservationBL = reservationBL;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var carFuelTypes = await carFuelTypeBL.GetAsync();

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

                return View(new CarFuelTypeViewModel()
                {
                    CarFuelTypes = carFuelTypes
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
                var carFuelTypes = await carFuelTypeBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarFuelTypeViewModel(OpenInsertPopup: true)
                {
                    CarFuelTypes = carFuelTypes
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarFuelTypeDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carFuelTypeBL.InsertAsync(model);

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
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var carFuelType = await this.carFuelTypeBL.GetByIdAsync(id);
                var carFuelTypes = await this.carFuelTypeBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarFuelTypeViewModel(OpenUpdatePopup: true)
                {
                    CarFuelType = carFuelType,
                    CarFuelTypes = carFuelTypes
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarFuelTypeDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carFuelTypeBL.UpdateAsync(model);

                HttpContext.Session.SetString("SuccessMessage", "Güncelleme işlemi başarıyla tamamlanmıştır.");

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
                var carFuelType = await this.carFuelTypeBL.GetByIdAsync(id);
                var carFuelTypes = await this.carFuelTypeBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarFuelTypeViewModel(OpenDeletePopup: true)
                {
                    CarFuelType = carFuelType,
                    CarFuelTypes = carFuelTypes
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarDTO model)
        {
            try
            {
                await carFuelTypeBL.DeleteAsync(model.Id);

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
