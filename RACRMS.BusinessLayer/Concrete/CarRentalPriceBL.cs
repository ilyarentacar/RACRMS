using Microsoft.EntityFrameworkCore;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.DataTransferObject;
using RACRMS.Entity;
using RACRMS.Extension;
using RACRMS.UnitOfWork.Abstract;
using RACRMS.UnitOfWork.Concrete;
using RACRMS.ValidationLayer.Abstract;
using RACRMS.ValidationLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Concrete
{
    public class CarRentalPriceBL : ICarRentalPriceBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarRentalPriceBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                CarRentalPrice carRentalPrice = await getById(id);

                if (carRentalPrice != null)
                {
                    unitOfWork.CarRentalPrice.Delete(carRentalPrice);

                    return await unitOfWork.SaveChangesAsync();
                }
                else
                    throw new Exception("Kayıt bulunamadı.");
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CarRentalPriceDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarRentalPrice.Select()
                    .Include(x => x.Car)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarRentalPriceDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarRentalPrice.Select(x => x.Id == id)
                    .Include(x => x.Car)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarRentalPriceDTO dto)
        {
            try
            {
                await dateRangeValidation(dto.CarId, dto.StartDate, dto.EndDate);

                CarRentalPrice carRentalPrice = new CarRentalPrice()
                {
                    CarId = dto.CarId,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    RentPrice = dto.RentPrice,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.CarRentalPrice.InsertAsync(carRentalPrice);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CarRentalPriceDTO dto)
        {
            try
            {
                await isThereAnyCarRentalPriceValidation(dto);

                CarRentalPrice carRentalPrice = await getById(dto.Id);

                if (carRentalPrice == null)
                    throw new Exception("Kayıt bulunamadı.");

                carRentalPrice.CarId = dto.CarId;
                carRentalPrice.StartDate = (DateTime.ParseExact(dto.StartDateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                carRentalPrice.EndDate = (DateTime.ParseExact(dto.EndDateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                carRentalPrice.RentPrice = dto.RentPrice;
                carRentalPrice.UpdateDate = DateTime.Now;

                unitOfWork.CarRentalPrice.Update(carRentalPrice);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarRentalPrice> getById(int id)
        {
            try
            {
                return await unitOfWork.CarRentalPrice.Select(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task dateRangeValidation(int carId, DateTime startDate, DateTime endDate)
        {
            try
            {
                ICarRentalPriceVL carRentalPriceVL = new CarRentalPriceVL();

                carRentalPriceVL.DateRangeCheck(startDate, endDate);

                await carRentalPriceVL.IsThereDateRange(carId, startDate, endDate);
            }
            catch
            {
                throw;
            }
        }

        private async Task isThereAnyCarRentalPriceValidation(CarRentalPriceDTO dto)
        {
            try
            {
                ICarRentalPriceVL carRentalPriceVL = new CarRentalPriceVL();

                await carRentalPriceVL.IsThereAnyCarRentalPrice(dto);
            }
            catch
            {
                throw;
            }
        }
    }
}
