using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.Extension
{
    public static class RequirementExtensions
    {
        public static RequirementDTO ToDTO(this Requirement entity)
        {
            try
            {
                return new RequirementDTO()
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
