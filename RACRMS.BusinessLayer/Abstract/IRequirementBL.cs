using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface IRequirementBL
    {
        Task<List<RequirementDTO>> GetAsync();
        Task<RequirementDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(RequirementDTO dto);
        Task<int> UpdateAsync(RequirementDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
