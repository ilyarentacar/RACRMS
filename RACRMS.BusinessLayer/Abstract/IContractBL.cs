using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface IContractBL
    {
        Task<List<ContractDTO>> GetAsync();
        Task<int> GetWaitingContractCountAsync();
        Task<ContractDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(ContractDTO dto);
        Task<int> UpdateAsync(ContractDTO dto);
        Task<int> UpdateAfterSentEmailAsync(ContractDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
