using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarFuelTypeBL
    {
        Task<List<CarFuelTypeDTO>> GetAsync();
        Task<CarFuelTypeDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarFuelTypeDTO dto);
        Task<int> UpdateAsync(CarFuelTypeDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
