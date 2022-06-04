using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.DataTransferObject;
using RACRMS.Shared.Enum;
using RACRMS.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<IActionResult> Index(string carClassName = null, string startDate = null, string startHour = null, string endDate = null, string endHour = null)
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

                DateTime currentDate = DateTime.Now.AddDays(1);

                DateTime startDateParsed = DateTime.Now;
                DateTime endDateParsed = startDateParsed;

                if (currentDate.Minute <= 30)
                    startDateParsed = Convert.ToDateTime(currentDate.AddHours(1).ToString("dd.MM.yyyy HH:30"));
                else
                    startDateParsed = Convert.ToDateTime(currentDate.AddHours(2).ToString("dd.MM.yyyy HH:00"));

                endDateParsed = startDateParsed.AddDays(1);

                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(startHour) && !string.IsNullOrEmpty(endDate) && !string.IsNullOrEmpty(endHour))
                {
                    startDateParsed = DateTime.ParseExact($"{startDate} {startHour}", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                    endDateParsed = DateTime.ParseExact($"{endDate} {endHour}", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                else
                {
                    startDate = startDateParsed.ToString("dd.MM.yyyy");
                    startHour = startDateParsed.ToString("HH:mm");

                    endDate = endDateParsed.ToString("dd.MM.yyyy");
                    endHour = endDateParsed.ToString("HH:mm");
                }

                FiloViewModel filoViewModel = new FiloViewModel();

                if (carClassName == Enum.GetName(typeof(CarClassEnum), CarClassEnum.Ekonomik))
                    filoViewModel = await getEconomicFiloByReservation(startDateParsed, endDateParsed);
                else if (carClassName == Enum.GetName(typeof(CarClassEnum), CarClassEnum.Konfor))
                    filoViewModel = await getConfortFiloByReservation(startDateParsed, endDateParsed);
                else if (carClassName == Enum.GetName(typeof(CarClassEnum), CarClassEnum.Lüx))
                    filoViewModel = await getLuxFiloByReservation(startDateParsed, endDateParsed);
                else
                    filoViewModel = await getFiloByReservation(startDateParsed, endDateParsed);

                filoViewModel.carClassName = string.IsNullOrEmpty(carClassName)
                        ? "Tüm"
                        : carClassName;

                return View(new VehicleViewModel()
                {
                    filoViewModel = filoViewModel,
                    reservationViewModel = new ReservationViewModel
                    {
                        StartDate = startDate,
                        StartHour = startHour,
                        EndDate = endDate,
                        EndHour = endHour
                    }
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new VehicleViewModel());
            }
        }

        //private async Task<FiloViewModel> getFilo()
        //{
        //    try
        //    {
        //        List<CarDTO> cars = await carBL.GetFilo();

        //        return new FiloViewModel()
        //        {
        //            carViewModels = cars
        //                .Select(x => new FiloViewModel.CarViewModel()
        //                {
        //                    Id = x.Id,
        //                    ImageUrl = x.CarModel.ImageUrl,
        //                    CarClassName = x.CarClassName,
        //                    CarChassisTypeName = x.CarChassisTypeName,
        //                    CarRentalPrice = x.RentalPrice,
        //                    CarBrandName = x.CarBrandName,
        //                    CarModelName = x.CarModelName,
        //                    CarFuelTypeName = x.CarFuelTypeName,
        //                    CarGearTypeName = x.CarGearTypeName,
        //                    TotalKm = 500,
        //                    AgeLimit = $"{21} Yaş ve üzeri"
        //                })
        //                .GroupBy(x => new
        //                {
        //                    Id = x.Id,
        //                    x.ImageUrl,
        //                    x.CarClassName,
        //                    x.CarChassisTypeName,
        //                    x.CarRentalPrice,
        //                    x.CarBrandName,
        //                    x.CarModelName,
        //                    x.CarFuelTypeName,
        //                    x.CarGearTypeName,
        //                    x.TotalKm,
        //                    x.AgeLimit
        //                })
        //                .Select(x => new FiloViewModel.CarViewModel()
        //                {
        //                    Id = x.Key.Id,
        //                    ImageUrl = x.Key.ImageUrl,
        //                    CarClassName = x.Key.CarClassName,
        //                    CarChassisTypeName = x.Key.CarChassisTypeName,
        //                    CarRentalPrice = x.Key.CarRentalPrice,
        //                    CarBrandName = x.Key.CarBrandName,
        //                    CarModelName = x.Key.CarModelName,
        //                    CarFuelTypeName = x.Key.CarFuelTypeName,
        //                    CarGearTypeName = x.Key.CarGearTypeName,
        //                    TotalKm = 500,
        //                    AgeLimit = $"{21} Yaş ve üzeri"
        //                })
        //                .ToList()
        //        };
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private async Task<FiloViewModel> getFiloByReservation(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<CarDTO> cars = await carBL.GetFiloByReservation(startDate, endDate);

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

        //private async Task<FiloViewModel> getEconomicFilo()
        //{
        //    try
        //    {
        //        List<CarDTO> cars = await carBL.GetEconomicFilo();

        //        return new FiloViewModel()
        //        {
        //            carViewModels = cars
        //                .Select(x => new FiloViewModel.CarViewModel()
        //                {
        //                    Id = x.Id,
        //                    ImageUrl = x.CarModel.ImageUrl,
        //                    CarClassName = x.CarClassName,
        //                    CarChassisTypeName = x.CarChassisTypeName,
        //                    CarRentalPrice = x.RentalPrice,
        //                    CarBrandName = x.CarBrandName,
        //                    CarModelName = x.CarModelName,
        //                    CarFuelTypeName = x.CarFuelTypeName,
        //                    CarGearTypeName = x.CarGearTypeName,
        //                    TotalKm = 500,
        //                    AgeLimit = $"{21} Yaş ve üzeri"
        //                })
        //                .GroupBy(x => new
        //                {
        //                    Id = x.Id,
        //                    x.ImageUrl,
        //                    x.CarClassName,
        //                    x.CarChassisTypeName,
        //                    x.CarRentalPrice,
        //                    x.CarBrandName,
        //                    x.CarModelName,
        //                    x.CarFuelTypeName,
        //                    x.CarGearTypeName,
        //                    x.TotalKm,
        //                    x.AgeLimit
        //                })
        //                .Select(x => new FiloViewModel.CarViewModel()
        //                {
        //                    Id = x.Key.Id,
        //                    ImageUrl = x.Key.ImageUrl,
        //                    CarClassName = x.Key.CarClassName,
        //                    CarChassisTypeName = x.Key.CarChassisTypeName,
        //                    CarRentalPrice = x.Key.CarRentalPrice,
        //                    CarBrandName = x.Key.CarBrandName,
        //                    CarModelName = x.Key.CarModelName,
        //                    CarFuelTypeName = x.Key.CarFuelTypeName,
        //                    CarGearTypeName = x.Key.CarGearTypeName,
        //                    TotalKm = 500,
        //                    AgeLimit = $"{21} Yaş ve üzeri"
        //                })
        //                .ToList()
        //        };
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private async Task<FiloViewModel> getEconomicFiloByReservation(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<CarDTO> cars = await carBL.GetEconomicFiloByReservation(startDate, endDate);

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

        //private async Task<FiloViewModel> getConfortFilo()
        //{
        //    try
        //    {
        //        List<CarDTO> cars = await carBL.GetConfortFilo();

        //        return new FiloViewModel()
        //        {
        //            carViewModels = cars
        //                .Select(x => new FiloViewModel.CarViewModel()
        //                {
        //                    Id = x.Id,
        //                    ImageUrl = x.CarModel.ImageUrl,
        //                    CarClassName = x.CarClassName,
        //                    CarChassisTypeName = x.CarChassisTypeName,
        //                    CarRentalPrice = x.RentalPrice,
        //                    CarBrandName = x.CarBrandName,
        //                    CarModelName = x.CarModelName,
        //                    CarFuelTypeName = x.CarFuelTypeName,
        //                    CarGearTypeName = x.CarGearTypeName,
        //                    TotalKm = 500,
        //                    AgeLimit = $"{21} Yaş ve üzeri"
        //                })
        //                .GroupBy(x => new
        //                {
        //                    Id = x.Id,
        //                    x.ImageUrl,
        //                    x.CarClassName,
        //                    x.CarChassisTypeName,
        //                    x.CarRentalPrice,
        //                    x.CarBrandName,
        //                    x.CarModelName,
        //                    x.CarFuelTypeName,
        //                    x.CarGearTypeName,
        //                    x.TotalKm,
        //                    x.AgeLimit
        //                })
        //                .Select(x => new FiloViewModel.CarViewModel()
        //                {
        //                    Id = x.Key.Id,
        //                    ImageUrl = x.Key.ImageUrl,
        //                    CarClassName = x.Key.CarClassName,
        //                    CarChassisTypeName = x.Key.CarChassisTypeName,
        //                    CarRentalPrice = x.Key.CarRentalPrice,
        //                    CarBrandName = x.Key.CarBrandName,
        //                    CarModelName = x.Key.CarModelName,
        //                    CarFuelTypeName = x.Key.CarFuelTypeName,
        //                    CarGearTypeName = x.Key.CarGearTypeName,
        //                    TotalKm = 500,
        //                    AgeLimit = $"{21} Yaş ve üzeri"
        //                })
        //                .ToList()
        //        };
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private async Task<FiloViewModel> getConfortFiloByReservation(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<CarDTO> cars = await carBL.GetConfortFiloByReservation(startDate, endDate);

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

        //private async Task<FiloViewModel> getLuxFilo()
        //{
        //    try
        //    {
        //        List<CarDTO> cars = await carBL.GetLuxFilo();

        //        return new FiloViewModel()
        //        {
        //            carViewModels = cars
        //                .Select(x => new FiloViewModel.CarViewModel()
        //                {
        //                    Id = x.Id,
        //                    ImageUrl = x.CarModel.ImageUrl,
        //                    CarClassName = x.CarClassName,
        //                    CarChassisTypeName = x.CarChassisTypeName,
        //                    CarRentalPrice = x.RentalPrice,
        //                    CarBrandName = x.CarBrandName,
        //                    CarModelName = x.CarModelName,
        //                    CarFuelTypeName = x.CarFuelTypeName,
        //                    CarGearTypeName = x.CarGearTypeName,
        //                    TotalKm = 500,
        //                    AgeLimit = $"{21} Yaş ve üzeri"
        //                })
        //                .GroupBy(x => new
        //                {
        //                    Id = x.Id,
        //                    x.ImageUrl,
        //                    x.CarClassName,
        //                    x.CarChassisTypeName,
        //                    x.CarRentalPrice,
        //                    x.CarBrandName,
        //                    x.CarModelName,
        //                    x.CarFuelTypeName,
        //                    x.CarGearTypeName,
        //                    x.TotalKm,
        //                    x.AgeLimit
        //                })
        //                .Select(x => new FiloViewModel.CarViewModel()
        //                {
        //                    Id = x.Key.Id,
        //                    ImageUrl = x.Key.ImageUrl,
        //                    CarClassName = x.Key.CarClassName,
        //                    CarChassisTypeName = x.Key.CarChassisTypeName,
        //                    CarRentalPrice = x.Key.CarRentalPrice,
        //                    CarBrandName = x.Key.CarBrandName,
        //                    CarModelName = x.Key.CarModelName,
        //                    CarFuelTypeName = x.Key.CarFuelTypeName,
        //                    CarGearTypeName = x.Key.CarGearTypeName,
        //                    TotalKm = 500,
        //                    AgeLimit = $"{21} Yaş ve üzeri"
        //                })
        //                .ToList()
        //        };
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private async Task<FiloViewModel> getLuxFiloByReservation(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<CarDTO> cars = await carBL.GetLuxFiloByReservation(startDate, endDate);

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
