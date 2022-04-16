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
    public class CarGearTypeVL : ICarGearTypeVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarGearTypeVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereName(string name)
        {
            try
            {
                if (await unitOfWork.CarGearType.Select(x => x.Name == name).AnyAsync())
                    throw new Exception("Bu vites tipi zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }
    }
}
