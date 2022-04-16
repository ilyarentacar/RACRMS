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
    public class CarTypeController : Controller
    {
        private readonly ICarTypeBL carTypeBL;
        private readonly IReservationBL reservationBL;

        public CarTypeController(ICarTypeBL carTypeBL, IReservationBL reservationBL)
        {
            this.carTypeBL = carTypeBL;
            this.reservationBL = reservationBL;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var carTypes = await carTypeBL.GetAsync();

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

                return View(new CarTypeViewModel()
                {
                    CarTypes = carTypes
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new CarTypeViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            try
            {
                var carTypes = await carTypeBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarTypeViewModel(OpenInsertPopup: true)
                {
                    CarTypes = carTypes
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarTypeDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carTypeBL.InsertAsync(model);

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
                var carType = await this.carTypeBL.GetByIdAsync(id);
                var carTypes = await this.carTypeBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarTypeViewModel(OpenUpdatePopup: true)
                {
                    CarType = carType,
                    CarTypes = carTypes
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarTypeDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carTypeBL.UpdateAsync(model);

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
                var carType = await this.carTypeBL.GetByIdAsync(id);
                var carTypes = await this.carTypeBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarTypeViewModel(OpenDeletePopup: true)
                {
                    CarType = carType,
                    CarTypes = carTypes
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
                await carTypeBL.DeleteAsync(model.Id);

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
