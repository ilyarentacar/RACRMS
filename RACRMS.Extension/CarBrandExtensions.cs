using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Extension
{
    public static class CarBrandExtensions
    {
        public static CarBrandDTO ToDTO(this CarBrand entity)
        {
            try
            {
                return new CarBrandDTO()
                {
                    Id = entity.Id,
                    Name = entity.Name,
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
