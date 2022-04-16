using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarGearTypeBL
    {
        Task<List<CarGearTypeDTO>> GetAsync();
        Task<CarGearTypeDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarGearTypeDTO dto);
        Task<int> UpdateAsync(CarGearTypeDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
