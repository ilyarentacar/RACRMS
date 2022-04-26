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
    public class UserRoleController : Controller
    {
        private readonly IUserRoleBL userRoleBL;
        private readonly IReservationBL reservationBL;
        private readonly IContractBL contractBL;

        public UserRoleController(IUserRoleBL userRoleBL, IReservationBL reservationBL, IContractBL contractBL)
        {
            this.userRoleBL = userRoleBL;
            this.reservationBL = reservationBL;
            this.contractBL = contractBL;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var userRoles = await userRoleBL.GetAsync();

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

                return View(new UserRoleViewModel()
                {
                    UserRoles = userRoles
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new UserRoleViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            try
            {
                var userRoles = await userRoleBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new UserRoleViewModel(OpenInsertPopup: true)
                {
                    UserRoles = userRoles
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(UserRoleDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await userRoleBL.InsertAsync(model);

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
                var userRole = await userRoleBL.GetByIdAsync(id);
                var userRoles = await userRoleBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new UserRoleViewModel(OpenUpdatePopup: true)
                {
                    UserRole = userRole,
                    UserRoles = userRoles
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserRoleDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Session.SetString("ErrorMessage", "Form alanı geçerli bilgilerle doldurulmalıdır.");

                    return RedirectToAction("Index");
                }

                await userRoleBL.UpdateAsync(model);

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
                var userRole = await userRoleBL.GetByIdAsync(id);
                var userRoles = await userRoleBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new UserRoleViewModel(OpenDeletePopup: true)
                {
                    UserRole = userRole,
                    UserRoles = userRoles
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
                await userRoleBL.DeleteAsync(model.Id);

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
