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
    public class CarRentalRequirementController : Controller
    {
        private readonly ICarRentalRequirementBL carRentalRequirementBL;
        private readonly ICarBL carBL;
        private readonly IRequirementBL requirementBL;
        private readonly IReservationBL reservationBL;
        private readonly IContractBL contractBL;

        public CarRentalRequirementController(ICarRentalRequirementBL carRentalRequirementBL, ICarBL carBL, IRequirementBL requirementBL, IReservationBL reservationBL, IContractBL contractBL)
        {
            this.carRentalRequirementBL = carRentalRequirementBL;
            this.carBL = carBL;
            this.requirementBL = requirementBL;
            this.reservationBL = reservationBL;
            this.contractBL = contractBL;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var carRentalRequirements = await carRentalRequirementBL.GetAsync();

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

                return View(new CarRentalRequirementViewModel()
                {
                    CarRentalRequirements = carRentalRequirements
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new CarRentalRequirementViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            try
            {
                var carRentalRequirements = await carRentalRequirementBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                var cars = await carBL.GetAsync();
                var requirements = await requirementBL.GetAsync();

                return View("Index", new CarRentalRequirementViewModel(OpenInsertPopup: true)
                {
                    CarRentalRequirement = new CarRentalRequirementDTO()
                    {
                        Cars = cars,
                        Requirements = requirements.ToArray()
                    },
                    CarRentalRequirements = carRentalRequirements
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarRentalRequirementDTO model)
        {
            try
            {
                await carRentalRequirementBL.BulkInsertAsync(model);

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
                var carRentalRequirement = await carRentalRequirementBL.GetByIdAsync(id);
                var carRentalRequirements = await carRentalRequirementBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new CarRentalRequirementViewModel(OpenDeletePopup: true)
                {
                    CarRentalRequirement = carRentalRequirement,
                    CarRentalRequirements = carRentalRequirements
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarRentalRequirementDTO model)
        {
            try
            {
                await carRentalRequirementBL.DeleteAsync(model.Id);

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
