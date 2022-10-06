using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.DataTransferObject;
using RACRMS.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarBL carBL;

        public HomeController(ICarBL carBL)
        {
            this.carBL = carBL;
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

                return View(new HomeViewModel()
                {
                    reservationViewModel = new ReservationViewModel(),
                    mostPreferedCarViewModel = await getMostPreferedCarAsync(),
                    filoViewModel = await getFilo()
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new HomeViewModel());
            }
        }

        [HttpPost]
        public IActionResult Index(ReservationViewModel model)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Index", "Vehicle", new
                {
                    startDate = model.StartDate,
                    startHour = model.StartHour,
                    endDate = model.EndDate,
                    endHour = model.EndHour
                });

            return View(model);
        }

        private async Task<MostPreferedCarViewModel> getMostPreferedCarAsync()
        {
            try
            {
                CarDTO car = await carBL.GetMostPrefered();

                return new MostPreferedCarViewModel()
                {
                    CarBrandName = car.CarBrandName,
                    CarModelName = car.CarModelName,
                    CarRentalPrice = car.RentalPrice,
                    ImageUrl = car.CarModel.ImageUrl,
                    Description = car.CarModel.Description
                };
            }
            catch
            {
                throw;
            }
        }

        private async Task<FiloViewModel> getFilo()
        {
            try
            {
                List<CarDTO> cars = await carBL.GetFilo();

                return new FiloViewModel()
                {
                    carViewModels = cars
                        .Select(x => new FiloViewModel.CarViewModel()
                        {
                            ImageUrl = x.CarModel.ImageUrl,
                            CarClassName = x.CarClassName,
                            CarChassisTypeName = x.CarChassisTypeName,
                            CarRentalPrice = x.RentalPrice,
                            CarBrandName = x.CarBrandName,
                            CarModelName = x.CarModelName,
                            CarFuelTypeName = x.CarFuelTypeName,
                            CarGearTypeName = x.CarGearTypeName,
                            TotalKm = x.CarRentalRequirement.Count != 0
                                ? x.CarRentalRequirement
                                    .Select(y => y.Requirement)
                                    .AsEnumerable()
                                    .FirstOrDefault(y => y.Id == 1).Name
                                : string.Empty,
                            AgeLimit = x.CarRentalRequirement.Count != 0
                                ? x.CarRentalRequirement
                                    .Select(y => y.Requirement)
                                    .AsEnumerable()
                                    .FirstOrDefault(y => y.Id == 2).Name
                                : string.Empty
                        })
                        .GroupBy(x => new
                        {
                            x.ImageUrl,
                            x.CarClassName,
                            x.CarChassisTypeName,
                            x.CarRentalPrice,
                            x.CarBrandName,
                            x.CarModelName,
                            x.CarFuelTypeName,
                            x.CarGearTypeName,
                            x.TotalKm,
                            x.AgeLimit
                        })
                        .Select(x => new FiloViewModel.CarViewModel()
                        {
                            ImageUrl = x.Key.ImageUrl,
                            CarClassName = x.Key.CarClassName,
                            CarChassisTypeName = x.Key.CarChassisTypeName,
                            CarRentalPrice = x.Key.CarRentalPrice,
                            CarBrandName = x.Key.CarBrandName,
                            CarModelName = x.Key.CarModelName,
                            CarFuelTypeName = x.Key.CarFuelTypeName,
                            CarGearTypeName = x.Key.CarGearTypeName,
                            TotalKm = x.Key.TotalKm,
                            AgeLimit = x.Key.AgeLimit
                        })
                        .ToList()
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
