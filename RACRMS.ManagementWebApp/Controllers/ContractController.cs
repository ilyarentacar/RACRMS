using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RACRMS.BusinessLayer.Abstract;
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
    public class ContractController : Controller
    {
        private readonly IContractBL contractBL;
        private readonly IReservationBL reservationBL;
        private readonly ISendEndpointProvider sendEndpointProvider;

        public ContractController(IContractBL contractBL, IReservationBL reservationBL, ISendEndpointProvider sendEndpointProvider)
        {
            this.contractBL = contractBL;
            this.reservationBL = reservationBL;
            this.sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var contracts = await contractBL.GetAsync();

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

                return View(new ContractViewModel()
                {
                    Contracts = contracts
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new ContractViewModel());
            }
        }

        [HttpGet("ContractDetail")]
        public async Task<IActionResult> ContractDetail(int id)
        {
            try
            {
                var contract = await contractBL.GetByIdAsync(id);
                var contracts = await contractBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new ContractViewModel(OpenDetailPopup: true)
                {
                    Contract = contract,
                    Contracts = contracts
                });
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
