using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface IPaymentTypeBL
    {
        Task<List<PaymentTypeDTO>> GetAsync();
        Task<PaymentTypeDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(PaymentTypeDTO dto);
        Task<int> UpdateAsync(PaymentTypeDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
