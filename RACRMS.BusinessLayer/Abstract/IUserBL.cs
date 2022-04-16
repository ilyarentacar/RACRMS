using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface IUserBL
    {
        Task<List<UserDTO>> GetAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(UserDTO dto);
        Task<int> UpdateAsync(UserDTO dto);
        Task<int> DeleteAsync(int id);
        Task<UserDTO> CheckCridentialAsync(UserDTO dto);
        Task<UserDTO> CheckCridentialForPasswordRover(UserDTO dto);
    }
}
