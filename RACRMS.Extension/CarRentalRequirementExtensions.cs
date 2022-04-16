using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Extension
{
    public static class CarRentalRequirementExtensions
    {
        public static CarRentalRequirementDTO ToDTO(this CarRentalRequirement entity)
        {
            try
            {
                return new CarRentalRequirementDTO()
                {
                    Id = entity.Id,
                    CarId = entity.CarId,
                    PlateNumber = entity.Car != null ? entity.Car.PlateNumber : string.Empty,
                    RequirementId = entity.RequirementId,
                    RequirementName = entity.Requirement != null ? entity.Requirement.Name : string.Empty,
                    Car = entity.Car != null ? entity.Car.ToDTO() : new CarDTO(),
                    Requirement = entity.Requirement != null ? entity.Requirement.ToDTO() : new RequirementDTO()
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
