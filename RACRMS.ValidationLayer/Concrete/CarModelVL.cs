using Microsoft.EntityFrameworkCore;
using RACRMS.DataTransferObject;
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
    public class CarModelVL : ICarModelVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarModelVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereAnyCarModel(CarModelDTO dto)
        {
            try
            {
                if (await unitOfWork.CarModel.Select()
                    .Where(x => x.CarClassId == dto.CarClassId)
                    .Where(x => x.CarTypeId == dto.CarTypeId)
                    .Where(x => x.CarBrandId == dto.CarBrandId)
                    .Where(x => x.CarChassisTypeId == dto.CarChassisTypeId)
                    .Where(x => x.CarFuelTypeId == dto.CarFuelTypeId)
                    .Where(x => x.CarGearTypeId == dto.CarGearTypeId)
                    .Where(x => x.Name == dto.Name)
                    .Where(x => x.ImageUrl == dto.ImageUrl)
                    .AnyAsync())
                    throw new Exception("Bu araç modeli zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }

        public async Task IsThereName(int carBrandId, string name)
        {
            try
            {
                if (await unitOfWork.CarModel.Select().Where(x => x.CarBrandId == carBrandId).Where(x => x.Name == name).AnyAsync())
                    throw new Exception("Bu araç modeli zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }
    }
}
