using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarClassBL
    {
        Task<List<CarClassDTO>> GetAsync();
        Task<CarClassDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarClassDTO dto);
        Task<int> UpdateAsync(CarClassDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
