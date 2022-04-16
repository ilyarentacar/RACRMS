using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
   public interface ICarBrandBL
    {
        Task<List<CarBrandDTO>> GetAsync();
        Task<CarBrandDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarBrandDTO dto);
        Task<int> UpdateAsync(CarBrandDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
