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
    public class VehicleController : Controller
    {
        private readonly ICarBL carBL;

        public VehicleController(ICarBL carBL)
        {
            this.carBL = carBL;
        }

        public async Task<IActionResult> Index(string className = null)
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

                return View(new VehicleViewModel()
                {
                    filoViewModel = await getFilo()
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new HomeViewModel());
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
                            Id = x.Id,
                            ImageUrl = x.CarModel.ImageUrl,
                            CarClassName = x.CarClassName,
                            CarChassisTypeName = x.CarChassisTypeName,
                            CarRentalPrice = x.RentalPrice,
                            CarBrandName = x.CarBrandName,
                            CarModelName = x.CarModelName,
                            CarFuelTypeName = x.CarFuelTypeName,
                            CarGearTypeName = x.CarGearTypeName,
                            TotalKm = 500,
                            AgeLimit = $"{21} Yaş ve üzeri"
                        })
                        .GroupBy(x => new
                        {
                            Id = x.Id,
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
                            Id = x.Key.Id,
                            ImageUrl = x.Key.ImageUrl,
                            CarClassName = x.Key.CarClassName,
                            CarChassisTypeName = x.Key.CarChassisTypeName,
                            CarRentalPrice = x.Key.CarRentalPrice,
                            CarBrandName = x.Key.CarBrandName,
                            CarModelName = x.Key.CarModelName,
                            CarFuelTypeName = x.Key.CarFuelTypeName,
                            CarGearTypeName = x.Key.CarGearTypeName,
                            TotalKm = 500,
                            AgeLimit = $"{21} Yaş ve üzeri"
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
