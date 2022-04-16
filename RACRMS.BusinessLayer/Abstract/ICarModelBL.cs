using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarModelBL
    {
        Task<List<CarModelDTO>> GetAsync();
        Task<List<CarModelDTO>> GetByCarBrandIdAsync(int carBrandId);
        Task<CarModelDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarModelDTO dto);
        Task<int> UpdateAsync(CarModelDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
