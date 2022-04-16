using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICustomerBL
    {
        Task<List<CustomerDTO>> GetAsync();
        Task<CustomerDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CustomerDTO dto);
        Task<int> UpdateAsync(CustomerDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
