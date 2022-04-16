using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Abstract
{
    public interface ICustomerVL
    {
        void IdentityNumverValid(decimal identityNumber);
        Task IsThereIdentityNumber(decimal identityNumber);
        Task IsThereAnyCustomer(CustomerDTO dto);
    }
}
