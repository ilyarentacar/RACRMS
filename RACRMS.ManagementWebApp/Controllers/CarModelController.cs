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
    public class CarModelController : Controller
    {
        private readonly ICarModelBL carModelBL;
        private readonly ICarBrandBL carBrandBL;
        private readonly ICarClassBL carClassBL;
        private readonly ICarTypeBL carTypeBL;
        private readonly ICarChassisTypeBL carChassisTypeBL;
        private readonly ICarFuelTypeBL carFuelTypeBL;
        private readonly ICarGearTypeBL carGearTypeBL;
        private readonly IReservationBL reservationBL;
        private readonly IContractBL contractBL;

        public CarModelController(ICarModelBL carModelBL,
            ICarBrandBL carBrandBL,
            ICarClassBL carClassBL,
            ICarTypeBL carTypeBL,
            ICarChassisTypeBL carChassisTypeBL,
            ICarFuelTypeBL carFuelTypeBL,
            ICarGearTypeBL carGearTypeBL,
            IReservationBL reservationBL,
            IContractBL contractBL)
        {
            this.carModelBL = carModelBL;
            this.carBrandBL = carBrandBL;
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
                var carModels = await carModelBL.GetAsync();

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

                return View(new CarModelViewModel()
                {
                    CarModels = carModels
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new CarModelViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            try
            {
                var carModels = await carModelBL.GetAsync();
                var carBrands = await carBrandBL.GetAsync();
                var carClasses = await carClassBL.GetAsync();
                var carTypes = await carTypeBL.GetAsync();
                var carChassisTypes = await carChassisTypeBL.GetAsync();
                var carFuelTypes = await carFuelTypeBL.GetAsync();
                var carGearTypes = await carGearTypeBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new CarModelViewModel(OpenInsertPopup: true)
                {
                    CarModels = carModels,
                    CarModel = new CarModelDTO()
                    {
                        CarBrands = carBrands,
                        CarClasses = carClasses,
                        CarTypes = carTypes,
                        CarChassisTypes = carChassisTypes,
                        CarFuelTypes = carFuelTypes,
                        CarGearTypes = carGearTypes
                    }
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarModelDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carModelBL.InsertAsync(model);

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
                var carModel = await this.carModelBL.GetByIdAsync(id);
                var carModels = await carModelBL.GetAsync();

                var carBrands = await carBrandBL.GetAsync();
                var carClasses = await carClassBL.GetAsync();
                var carTypes = await carTypeBL.GetAsync();
                var carChassisTypes = await carChassisTypeBL.GetAsync();
                var carFuelTypes = await carFuelTypeBL.GetAsync();
                var carGearTypes = await carGearTypeBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                carModel.CarBrands = carBrands;
                carModel.CarClasses = carClasses;
                carModel.CarTypes = carTypes;
                carModel.CarChassisTypes = carChassisTypes;
                carModel.CarFuelTypes = carFuelTypes;
                carModel.CarGearTypes = carGearTypes;

                return View("Index", new CarModelViewModel(OpenUpdatePopup: true)
                {
                    CarModel = carModel,
                    CarModels = carModels
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarModelDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carModelBL.UpdateAsync(model);

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
                var carModel = await this.carModelBL.GetByIdAsync(id);
                var carModels = await this.carModelBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new CarModelViewModel(OpenDeletePopup: true)
                {
                    CarModel = carModel,
                    CarModels = carModels
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
                await carModelBL.DeleteAsync(model.Id);

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
