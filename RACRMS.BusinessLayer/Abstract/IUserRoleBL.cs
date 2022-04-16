using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface IUserRoleBL
    {
        Task<List<UserRoleDTO>> GetAsync();
        Task<UserRoleDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(UserRoleDTO dto);
        Task<int> UpdateAsync(UserRoleDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
