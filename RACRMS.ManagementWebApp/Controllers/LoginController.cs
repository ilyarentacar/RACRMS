using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.DataTransferObject;
using RACRMS.MailServiceShared.Events;
using RACRMS.MailServiceShared.Statics;
using RACRMS.ManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserBL userBL;
        private readonly ISendEndpointProvider sendEndpointProvider;

        public LoginController(IUserBL userBL, ISendEndpointProvider sendEndpointProvider)
        {
            this.userBL = userBL;
            this.sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
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

                if (HttpContext.User.Identity.IsAuthenticated) return await signOutAsync();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                model = await userBL.CheckCridentialAsync(model);

                await signInAsync(model);

                string controller = HttpContext.Session.GetString("Controller");
                string action = HttpContext.Session.GetString("Action");

                HttpContext.Session.Remove("Controller");
                HttpContext.Session.Remove("Action");

                if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
                    return RedirectToAction("Index", "Reservation");
                else
                    return RedirectToAction(action, controller);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult PasswordRecover()
        {
            try
            {
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

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> PasswordRecover(UserDTO model)
        {
            try
            {
                model = await userBL.CheckCridentialForPasswordRover(model);

                await sendEmailAsync(model);

                HttpContext.Session.SetString("SuccessMessage", "Giriş bilgileriniz E-Posta adresinize gönderilmiştir.");

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }

        private async Task signInAsync(UserDTO model)
        {
            try
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.SerialNumber,model.Id.ToString()),
                    new Claim(ClaimTypes.Name,model.Name),
                    new Claim(ClaimTypes.Surname,model.Surname),
                    new Claim(ClaimTypes.Actor,"İlya Rent A Car"),
                    new Claim(ClaimTypes.StateOrProvince,"Adapazarı"),
                    new Claim(ClaimTypes.Role,model.UserRole.Name)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Default");

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties()
                {
                    IsPersistent = model.RememberMe
                });
            }
            catch
            {
                throw;
            }
        }

        private async Task<IActionResult> signOutAsync()
        {
            try
            {
                await HttpContext.SignOutAsync();
            }
            catch
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        private async Task sendEmailAsync(UserDTO model)
        {
            try
            {
                ISendEndpoint sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.PasswordRecoveryEmailEventQueue}"));

                await sendEndpoint.Send(new PasswordRecoveryEmailEvent()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    EmailAddress = model.EmailAddress,
                    Username = model.Username,
                    Password = model.Password
                });
            }
            catch
            {
                throw;
            }
        }
    }
}
