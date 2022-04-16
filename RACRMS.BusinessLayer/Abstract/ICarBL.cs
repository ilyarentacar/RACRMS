using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarBL
    {
        Task<List<CarDTO>> GetAsync();
        Task<CarDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarDTO dto);
        Task<int> UpdateAsync(CarDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
