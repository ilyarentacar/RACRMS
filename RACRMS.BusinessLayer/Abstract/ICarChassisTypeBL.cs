using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarChassisTypeBL
    {
        Task<List<CarChassisTypeDTO>> GetAsync();
        Task<CarChassisTypeDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarChassisTypeDTO dto);
        Task<int> UpdateAsync(CarChassisTypeDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
