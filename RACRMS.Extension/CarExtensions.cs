using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RACRMS.Extension
{
    public static class CarExtensions
    {
        public static CarDTO ToDTO(this Car entity)
        {
            try
            {
                return new CarDTO()
                {
                    Id = entity.Id,
                    CarClassId = entity.CarClassId,
                    CarClassName = entity.CarClass != null ? entity.CarClass.Name : string.Empty,
                    CarTypeId = entity.CarTypeId,
                    CarTypeName = entity.CarType != null ? entity.CarType.Name : string.Empty,
                    CarBrandId = entity.CarBrandId,
                    CarBrandName = entity.CarBrand != null ? entity.CarBrand.Name : string.Empty,
                    CarModelId = entity.CarModelId,
                    CarModelName = entity.CarModel != null ? entity.CarModel.Name : string.Empty,
                    CarChassisTypeId = entity.CarChassisTypeId,
                    CarChassisTypeName = entity.CarChassisType != null ? entity.CarChassisType.Name : string.Empty,
                    CarFuelTypeId = entity.CarFuelTypeId,
                    CarFuelTypeName = entity.CarFuelType != null ? entity.CarFuelType.Name : string.Empty,
                    CarGearTypeId = entity.CarGearTypeId,
                    CarGearTypeName = entity.CarGearType != null ? entity.CarGearType.Name : string.Empty,
                    MostPrefered = entity.MostPrefered,
                    ShowOnFilo = entity.ShowOnFilo,
                    Rentable = entity.Rentable,
                    PlateNumber = entity.PlateNumber,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate,
                    CarBrand = entity.CarBrand != null ? entity.CarBrand.ToDTO() : new CarBrandDTO(),
                    CarModel = entity.CarModel != null ? entity.CarModel.ToDTO() : new CarModelDTO(),
                    CarChassisType = entity.CarChassisType != null ? entity.CarChassisType.ToDTO() : new CarChassisTypeDTO(),
                    CarClass = entity.CarClass != null ? entity.CarClass.ToDTO() : new CarClassDTO(),
                    CarFuelType = entity.CarFuelType != null ? entity.CarFuelType.ToDTO() : new CarFuelTypeDTO(),
                    CarGearType = entity.CarGearType != null ? entity.CarGearType.ToDTO() : new CarGearTypeDTO(),
                    CarType = entity.CarType != null ? entity.CarType.ToDTO() : new CarTypeDTO(),
                    RentalPrice = entity.CarRentalPrice != null && entity.CarRentalPrice.Count != 0 ? entity.CarRentalPrice.FirstOrDefault().RentPrice : 0,
                    CarRentalRequirement = entity.CarRentalRequirement != null ? entity.CarRentalRequirement.Select(x => new CarRentalRequirementDTO()
                    {
                        Id = x.Id,
                        RequirementId = x.RequirementId,
                        Requirement = x.Requirement != null ? x.Requirement.ToDTO() : new RequirementDTO()
                    }).ToList() : new List<CarRentalRequirementDTO>()
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
