using Microsoft.EntityFrameworkCore;
using RACRMS.UnitOfWork.Abstract;
using RACRMS.UnitOfWork.Concrete;
using RACRMS.ValidationLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Concrete
{
public    class CarTypeVL: ICarTypeVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarTypeVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereName(string name)
        {
            try
            {
                if (await unitOfWork.CarType.Select(x => x.Name == name).AnyAsync())
                    throw new Exception("Bu araç tipi zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }
    }
}
