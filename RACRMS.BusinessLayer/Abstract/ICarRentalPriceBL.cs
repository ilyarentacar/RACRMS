using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarRentalPriceBL
    {
        Task<List<CarRentalPriceDTO>> GetAsync();
        Task<CarRentalPriceDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarRentalPriceDTO dto);
        Task<int> UpdateAsync(CarRentalPriceDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
