using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Extension
{
    public static class CarRentalPriceExtensions
    {
        public static CarRentalPriceDTO ToDTO(this CarRentalPrice entity)
        {
            try
            {
                return new CarRentalPriceDTO()
                {
                    Id = entity.Id,
                    CarId = entity.CarId,
                    PlateNumber = entity.Car.PlateNumber,
                    StartDate = entity.StartDate,
                    StartDateStr = entity.StartDate.ToString("dd.MM.yyyy"),
                    EndDate = entity.EndDate,
                    EndDateStr = entity.EndDate.ToString("dd.MM.yyyy"),
                    RentPrice = entity.RentPrice,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
