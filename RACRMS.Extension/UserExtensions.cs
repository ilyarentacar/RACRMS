using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.Extension
{
    public static class UserExtensions
    {
        public static UserDTO ToDTO(this User entity)
        {
            try
            {
                return new UserDTO()
                {
                    Id = entity.Id,
                    UserRoleId = entity.UserRoleId,
                    UserRoleName = entity.UserRole != null ? entity.UserRole.Name : string.Empty,
                    Name = entity.Name,
                    Surname = entity.Surname,
                    EmailAddress = entity.EmailAddress,
                    Username = entity.Username,
                    Password = entity.Password,
                    Editable = entity.Editable,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate,
                    UserRole = entity.UserRole != null ? entity.UserRole.ToDTO() : new UserRoleDTO()
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
