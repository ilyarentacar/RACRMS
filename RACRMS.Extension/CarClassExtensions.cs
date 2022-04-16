using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Extension
{
    public static class CarClassExtensions
    {
        public static CarClassDTO ToDTO(this CarClass entity)
        {
            try
            {
                return new CarClassDTO()
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
