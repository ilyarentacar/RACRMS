using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarPreferenceBL
    {
        Task<List<CarPreferenceDTO>> GetAsync();
        Task<CarPreferenceDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarPreferenceDTO dto);
        Task BulkInsertAsync(CarPreferenceDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
