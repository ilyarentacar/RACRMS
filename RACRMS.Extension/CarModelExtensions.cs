using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Extension
{
    public static class CarModelExtensions
    {
        public static CarModelDTO ToDTO(this CarModel entity)
        {
            try
            {
                return new CarModelDTO()
                {
                    Id = entity.Id,
                    CarClassId = entity.CarClassId,
                    CarClassName = entity.CarClass != null ? entity.CarClass.Name : string.Empty,
                    CarTypeId = entity.CarTypeId,
                    CarTypeName = entity.CarType != null ? entity.CarType.Name : string.Empty,
                    CarBrandId = entity.CarBrandId,
                    CarBrandName = entity.CarBrand != null ? entity.CarBrand.Name : string.Empty,
                    CarChassisTypeId = entity.CarChassisTypeId,
                    CarChassisTypeName = entity.CarChassisType != null ? entity.CarChassisType.Name : string.Empty,
                    CarFuelTypeId = entity.CarFuelTypeId,
                    CarFuelTypeName = entity.CarFuelType != null ? entity.CarFuelType.Name : string.Empty,
                    CarGearTypeId = entity.CarGearTypeId,
                    CarGearTypeName = entity.CarGearType != null ? entity.CarGearType.Name : string.Empty,
                    CarImageId = entity.CarImageId,
                    Name = entity.Name,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate,
                    CarBrand = entity.CarChassisType != null ? entity.CarBrand.ToDTO() : new CarBrandDTO(),
                    CarChassisType = entity.CarChassisType != null ? entity.CarChassisType.ToDTO() : new CarChassisTypeDTO(),
                    CarClass = entity.CarClass != null ? entity.CarClass.ToDTO() : new CarClassDTO(),
                    CarFuelType = entity.CarFuelType != null ? entity.CarFuelType.ToDTO() : new CarFuelTypeDTO(),
                    CarGearType = entity.CarGearType != null ? entity.CarGearType.ToDTO() : new CarGearTypeDTO(),
                    CarType = entity.CarType != null ? entity.CarType.ToDTO() : new CarTypeDTO()
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
