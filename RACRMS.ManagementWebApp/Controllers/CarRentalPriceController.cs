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
    public class CarRentalPriceController : Controller
    {
        private readonly ICarRentalPriceBL carRentalPriceBL;
        private readonly ICarBL carBL;
        private readonly IReservationBL reservationBL;

        public CarRentalPriceController(ICarRentalPriceBL carRentalPriceBL, ICarBL carBL, IReservationBL reservationBL)
        {
            this.carRentalPriceBL = carRentalPriceBL;
            this.carBL = carBL;
            this.reservationBL = reservationBL;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var carRentalPrices = await carRentalPriceBL.GetAsync();

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

                return View(new CarRentalPriceViewModel()
                {
                    CarRentalPrices = carRentalPrices
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new CarRentalPriceViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            try
            {
                var carRentalPrices = await carRentalPriceBL.GetAsync();
                var cars = await carBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarRentalPriceViewModel(OpenInsertPopup: true)
                {
                    CarRentalPrice = new CarRentalPriceDTO()
                    {
                        Cars = cars
                    },
                    CarRentalPrices = carRentalPrices
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarRentalPriceDTO RentalPrice)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carRentalPriceBL.InsertAsync(RentalPrice);

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
                var cars = await carBL.GetAsync();
                var carRentalPrice = await this.carRentalPriceBL.GetByIdAsync(id);
                var carRentalPrices = await this.carRentalPriceBL.GetAsync();

                await getWaitingReservationCountasync();

                carRentalPrice.Cars = cars;

                return View("Index", new CarRentalPriceViewModel(OpenUpdatePopup: true)
                {
                    CarRentalPrice = carRentalPrice,
                    CarRentalPrices = carRentalPrices
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarRentalPriceDTO RentalPrice)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carRentalPriceBL.UpdateAsync(RentalPrice);

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
                var carRentalPrice = await this.carRentalPriceBL.GetByIdAsync(id);
                var carRentalPrices = await this.carRentalPriceBL.GetAsync();

                await getWaitingReservationCountasync();

                return View("Index", new CarRentalPriceViewModel(OpenDeletePopup: true)
                {
                    CarRentalPrice = carRentalPrice,
                    CarRentalPrices = carRentalPrices
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarDTO RentalPrice)
        {
            try
            {
                await carRentalPriceBL.DeleteAsync(RentalPrice.Id);

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
