﻿using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.DataTransferObject;
using RACRMS.MailServiceShared.Events;
using RACRMS.MailServiceShared.Statics;
using RACRMS.ManagementWebApp.Filters;
using RACRMS.ManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Controllers
{
    [UserCheck]
    [UserRoleCheck("Sistem Yöneticisi", "Administrator")]
    public class ReservationController : Controller
    {
        private readonly IReservationBL reservationBL;
        private readonly IContractBL contractBL;
        private readonly ISendEndpointProvider sendEndpointProvider;

        public ReservationController(IReservationBL reservationBL, IContractBL contractBL, ISendEndpointProvider sendEndpointProvider)
        {
            this.reservationBL = reservationBL;
            this.contractBL = contractBL;
            this.sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null, string errorMessage = null)
        {
            try
            {
                var reservations = await reservationBL.GetAsync();

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

                return View(new ReservationViewModel()
                {
                    Reservations = reservations
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new ReservationViewModel());
            }
        }

        [HttpGet("ReservationAccept")]
        public async Task<IActionResult> ReservationAccept(int id)
        {
            try
            {
                var reservation = await reservationBL.GetByIdAsync(id);
                var reservations = await reservationBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new ReservationViewModel(OpenReservationAcceptPopup: true)
                {
                    Reservation = reservation,
                    Reservations = reservations
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost("ReservationAccept")]
        public async Task<IActionResult> ReservationAccept(ReservationDTO model)
        {
            try
            {
                model = await reservationBL.GetByIdAsync(model.Id);

                int approvingUserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber).Value);

                model.Approved = true;
                model.ApprovingUserId = approvingUserId;
                model.ApprovalDate = DateTime.Now;

                await reservationBL.UpdateAsync(model);

                ContractDTO contractModel = new ContractDTO()
                {
                    ReservationId = model.Id,
                    CarId = model.CarId,
                    CustomerId = model.CustomerId,
                    StartDate = model.StartDate,
                    PlanedEndDate = model.EndDate,
                    TotalPrice = model.TotalPrice,
                    CreateDate = DateTime.Now
                };

                await contractBL.InsertAsync(contractModel);

                await wakeUpMailService();

                ISendEndpoint sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.ReservationAcceptedEventQueue}"));

                await sendEndpoint.Send(new ReservationAcceptedEvent()
                {
                    ReservationId = model.Id,
                    Name = model.CustomerName,
                    Surname = model.CustomerSurname,
                    EmailAddress = model.Customer.EmailAddress,
                    ReservationStartDate = model.StartDate.ToString("dd.MM.yyyy HH:mm:ss"),
                    ReservationEndDate = model.EndDate.ToString("dd.MM.yyyy HH:mm:ss")
                });

                HttpContext.Session.SetString("SuccessMessage", "Rezervasyon onaylanmıştır.");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpGet("ReservationReject")]
        public async Task<IActionResult> ReservationReject(int id)
        {
            try
            {
                var reservation = await reservationBL.GetByIdAsync(id);
                var reservations = await reservationBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new ReservationViewModel(OpenReservationRejectPopup: true)
                {
                    Reservation = reservation,
                    Reservations = reservations
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpGet("ReservationDetail")]
        public async Task<IActionResult> ReservationDetail(int id)
        {
            try
            {
                var reservation = await reservationBL.GetByIdAsync(id);
                var reservations = await reservationBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new ReservationViewModel(OpenDetailPopup: true)
                {
                    Reservation = reservation,
                    Reservations = reservations
                });
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ErrorMessage", ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost("ReservationReject")]
        public async Task<IActionResult> ReservationReject(ReservationDTO model)
        {
            try
            {
                model = await reservationBL.GetByIdAsync(model.Id);

                int rejectingUserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber).Value);

                model.Approved = false;
                model.RejectingUserId = rejectingUserId;
                model.RejectDate = DateTime.Now;

                await reservationBL.UpdateAsync(model);

                await wakeUpMailService();

                ISendEndpoint sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.ReservationRejectedEventQueue}"));

                await sendEndpoint.Send(new ReservationRejectedEvent()
                {
                    ReservationId = model.Id,
                    Name = model.CustomerName,
                    Surname = model.CustomerSurname,
                    EmailAddress = model.Customer.EmailAddress,
                    ReservationStartDate = model.StartDate.ToString("dd.MM.yyyy HH:mm:ss"),
                    ReservationEndDate = model.EndDate.ToString("dd.MM.yyyy HH:mm:ss")
                });

                HttpContext.Session.SetString("SuccessMessage", "Rezervasyon reddedilmiştir.");

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
                var reservation = await reservationBL.GetByIdAsync(id);
                var reservations = await reservationBL.GetAsync();

                await getWaitingReservationCountasync();
                await getWaitingContractCountasync();

                return View("Index", new ReservationViewModel(OpenDeletePopup: true)
                {
                    Reservation = reservation,
                    Reservations = reservations
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
                await reservationBL.DeleteAsync(model.Id);

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

        private async Task wakeUpMailService()
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(MailServiceSettings.MailServiceRequestUrl));

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            }
            catch
            {
                throw;
            }
        }
    }
}
