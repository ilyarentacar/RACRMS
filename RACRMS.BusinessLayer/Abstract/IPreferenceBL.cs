using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface IPreferenceBL
    {
        Task<List<PreferenceDTO>> GetAsync();
        Task<PreferenceDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(PreferenceDTO dto);
        Task<int> UpdateAsync(PreferenceDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
