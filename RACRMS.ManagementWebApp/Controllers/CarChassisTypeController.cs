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
    public class CarChassisTypeController : Controller
    {
        private readonly ICarChassisTypeBL carChassisTypeBL;
        private readonly IReservationBL reservationBL;
        private readonly IContractBL contractBL;

        public CarChassisTypeController(ICarChassisTypeBL carChassisTypeBL, IReservationBL reservationBL, IContractBL contractBL)
        {
            this.carChassisTypeBL = carChassisTypeBL;
            this.reservationBL = reservationBL;
            this.contractBL = contractBL;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var carChassisTypes = await carChassisTypeBL.GetAsync();

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

                return View(new CarChassisTypeViewModel()
                {
                    CarChassisTypes = carChassisTypes
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new CarChassisTypeViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            try
            {
                var carChassisTypes = await carChassisTypeBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new CarChassisTypeViewModel(OpenInsertPopup: true)
                {
                    CarChassisTypes = carChassisTypes
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarChassisTypeDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carChassisTypeBL.InsertAsync(model);

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
                var carChassisType = await carChassisTypeBL.GetByIdAsync(id);
                var carChassisTypes = await carChassisTypeBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new CarChassisTypeViewModel(OpenUpdatePopup: true)
                {
                    CarChassisType = carChassisType,
                    CarChassisTypes = carChassisTypes
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarChassisTypeDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await carChassisTypeBL.UpdateAsync(model);

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
                var carChassisType = await carChassisTypeBL.GetByIdAsync(id);
                var carChassisTypes = await carChassisTypeBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new CarChassisTypeViewModel(OpenDeletePopup: true)
                {
                    CarChassisType = carChassisType,
                    CarChassisTypes = carChassisTypes
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarChassisTypeDTO model)
        {
            try
            {
                await carChassisTypeBL.DeleteAsync(model.Id);

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
