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
    public class CarPreferenceVL : ICarPreferenceVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarPreferenceVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsTherePreference(int carId, int preferenceId)
        {
            try
            {
                if (await unitOfWork.CarPreference.Select().Where(x => x.CarId == carId).Where(x => x.PreferenceId == preferenceId).AnyAsync())
                    throw new Exception("Bu araç özelliği zaten eklenmiştir.");
            }
            catch
            {
                throw;
            }
        }
    }
}
