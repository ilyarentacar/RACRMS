using Microsoft.EntityFrameworkCore;
using RACRMS.UnitOfWork.Abstract;
using RACRMS.UnitOfWork.Concrete;
using RACRMS.ValidationLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Concrete
{
    public class PaymentTypeVL : IPaymentTypeVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public PaymentTypeVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereName(string name)
        {
            try
            {
                if (await unitOfWork.PaymentType.Select(x => x.Name == name).AnyAsync())
                    throw new Exception("Bu ödeme türü zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }
    }
}
