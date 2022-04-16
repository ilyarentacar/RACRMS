using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Extension
{
    public static class CarChassisTypeExtensions
    {
        public static CarChassisTypeDTO ToDTO(this CarChassisType entity)
        {
            try
            {
                return new CarChassisTypeDTO()
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
