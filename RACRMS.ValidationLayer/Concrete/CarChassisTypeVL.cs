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
    public class CarChassisTypeVL : ICarChassisTypeVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarChassisTypeVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereName(string name)
        {
            try
            {
                if (await unitOfWork.CarChassisType.Select(x => x.Name == name).AnyAsync())
                    throw new Exception("Bu kasa tipi zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }
    }
}
