using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarTypeBL
    {
        Task<List<CarTypeDTO>> GetAsync();
        Task<CarTypeDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarTypeDTO dto);
        Task<int> UpdateAsync(CarTypeDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
