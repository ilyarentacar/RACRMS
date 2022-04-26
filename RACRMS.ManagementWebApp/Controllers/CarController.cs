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
    public class CarController : Controller
    {
        private readonly ICarBL carBL;
        private readonly ICarBrandBL carBrandBL;
        private readonly ICarModelBL carModelBL;
        private readonly ICarClassBL carClassBL;
        private readonly ICarTypeBL carTypeBL;
        private readonly ICarChassisTypeBL carChassisTypeBL;
        private readonly ICarFuelTypeBL carFuelTypeBL;
        private readonly ICarGearTypeBL carGearTypeBL;
        private readonly IReservationBL reservationBL;
        private readonly IContractBL contractBL;

        public CarController(ICarBL carBL,
            ICarBrandBL carBrandBL,
            ICarModelBL carModelBL,
            ICarClassBL carClassBL,
            ICarTypeBL carTypeBL,
            ICarChassisTypeBL carChassisTypeBL,
            ICarFuelTypeBL carFuelTypeBL,
            ICarGearTypeBL carGearTypeBL,
            IReservationBL reservationBL,
            IContractBL contractBL)
        {
            this.carBL = carBL;
            this.carBrandBL = carBrandBL;
            this.carModelBL = carModelBL;
            this.carClassBL = carClassBL;
            this.carTypeBL = carTypeBL;
            this.carChassisTypeBL = carChassisTypeBL;
            this.carFuelTypeBL = carFuelTypeBL;
            this.carGearTypeBL = carGearTypeBL;
            this.reservationBL = reservationBL;
            this.contractBL = contractBL;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var cars = await carBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

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

                return View(new CarViewModel()
                {
                    Cars = cars
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new CarViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            try
            {
                var cars = await carBL.GetAsync();
                var carBrands = await carBrandBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new CarViewModel(OpenInsertPopup: true)
                {
                    Cars = cars,
                    Car = new CarDTO()
                    {
                        CarBrands = carBrands,
                        Rentable = true
                    }
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> InsertCarBrandSelected(int id)
        {
            try
            {
                var carModels = await carModelBL.GetByCarBrandIdAsync(id);

                return Ok(carModels);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carBL.InsertAsync(model);

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
                var car = await carBL.GetByIdAsync(id);
                var cars = await carBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                var carBrands = await carBrandBL.GetAsync();
                var carModels = await carModelBL.GetByCarBrandIdAsync(car.CarBrandId);

                car.CarBrands = carBrands;
                car.CarModels = carModels;

                return View("Index", new CarViewModel(OpenUpdatePopup: true)
                {
                    Car = car,
                    Cars = cars
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carBL.UpdateAsync(model);

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
                var car = await carBL.GetByIdAsync(id);
                var cars = await carBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new CarViewModel(OpenDeletePopup: true)
                {
                    Car = car,
                    Cars = cars
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
                await carBL.DeleteAsync(model.Id);

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
            await getWaitingContractCountasync();

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

        private async Task getWaitingContractCountasync()
        {
            try
            {
                ViewBag.WaitingContractCount = await contractBL.GetWaitingContractCountAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
