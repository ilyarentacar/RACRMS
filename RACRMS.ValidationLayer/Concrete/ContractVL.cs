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
    public class ContractVL : IContractVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public ContractVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task AreContractDatesAvailable(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (await unitOfWork.Contract.Select()
                    .Where(x => x.StartDate >= startDate)
                    .Where(x => x.EndDate <= endDate)
                    .Where(x => x.HasPaid)
                    .AnyAsync())
                    throw new Exception("Bu tarih aralıkları kiralama için uygun değildir.");
            }
            catch
            {
                throw;
            }
        }
    }
}
