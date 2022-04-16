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
    public class ReservationVL : IReservationVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public ReservationVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task AreReservationDatesAvailable(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (await unitOfWork.Reservation.Select()
                    .Where(x => x.StartDate >= startDate)
                    .Where(x => x.EndDate <= endDate)
                    .Where(x => x.Approved.HasValue)
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
