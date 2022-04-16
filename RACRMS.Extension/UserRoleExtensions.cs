using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.Extension
{
    public static class UserRoleExtensions
    {
        public static UserRoleDTO ToDTO(this UserRole entity)
        {
            try
            {
                return new UserRoleDTO()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Usable = entity.Usable,
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
