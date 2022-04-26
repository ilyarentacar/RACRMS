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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Concrete
{
    public class ReservationBL : IReservationBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public ReservationBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                Reservation reservation = await getById(id);

                if (reservation != null)
                {
                    unitOfWork.Reservation.Delete(reservation);

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

        public async Task<List<ReservationDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.Reservation.Select()
                    .Include(x => x.ApprovingUser)
                    .Include(x => x.Car)
                        .ThenInclude(x => x.CarBrand)
                    .Include(x => x.Car)
                        .ThenInclude(x => x.CarModel)
                    .Include(x => x.Customer)
                    .Include(x => x.RejectingUser)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ReservationDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.Reservation.Select(x => x.Id == id)
                    .Include(x => x.ApprovingUser)
                    .Include(x => x.Car)
                        .ThenInclude(x => x.CarBrand)
                    .Include(x => x.Car)
                        .ThenInclude(x => x.CarModel)
                    .Include(x => x.Customer)
                    .Include(x => x.RejectingUser)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(ReservationDTO dto)
        {
            try
            {
                await dateValidation(dto.StartDate, dto.EndDate);

                Reservation reservation = new Reservation()
                {
                    CarId = dto.CarId,
                    ReservationCode = $"RZV{DateTime.Now.Ticks}",
                    CustomerId = dto.CustomerId,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    TotalPrice = dto.TotalPrice,
                    Approved = dto.Approved,
                    ApprovingUserId = dto.ApprovingUserId,
                    ApprovalDate = dto.ApprovalDate,
                    EmailSent = dto.EmailSent,
                    EmailSentDate = dto.EmailSentDate,
                    EmailTemplate = dto.EmailTemplate,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.Reservation.InsertAsync(reservation);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(ReservationDTO dto)
        {
            try
            {
                await dateValidation(dto.StartDate, dto.EndDate);

                Reservation reservation = await getById(dto.Id);

                if (reservation == null)
                    throw new Exception("Kayıt bulunamadı.");

                reservation.Approved = dto.Approved;
                reservation.ApprovingUserId = dto.ApprovingUserId;
                reservation.ApprovalDate = dto.ApprovalDate;
                reservation.RejectingUserId = dto.RejectingUserId;
                reservation.RejectDate = dto.RejectDate;
                reservation.EmailSent = dto.EmailSent;
                reservation.EmailSentDate = dto.EmailSentDate;
                reservation.EmailTemplate = dto.EmailTemplate;
                reservation.UpdateDate = DateTime.Now;

                unitOfWork.Reservation.Update(reservation);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<Reservation> getById(int id)
        {
            try
            {
                return await unitOfWork.Reservation.Select(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task dateValidation(DateTime startDate, DateTime endDate)
        {
            try
            {
                IReservationVL reservationVL = new ReservationVL();

                await reservationVL.AreReservationDatesAvailable(startDate, endDate);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GetWaitingReservationCountAsync()
        {
            try
            {
                return await unitOfWork.Reservation.Select(x => !x.Approved.HasValue).CountAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAfterSentEmailAsync(ReservationDTO dto)
        {
            try
            {
                Reservation reservation = await getById(dto.Id);

                if (reservation == null)
                    throw new Exception("Kayıt bulunamadı.");

                reservation.Approved = dto.Approved;
                reservation.ApprovingUserId = dto.ApprovingUserId;
                reservation.ApprovalDate = dto.ApprovalDate;
                reservation.RejectingUserId = dto.RejectingUserId;
                reservation.RejectDate = dto.RejectDate;
                reservation.EmailSent = dto.EmailSent;
                reservation.EmailSentDate = dto.EmailSentDate;
                reservation.EmailTemplate = dto.EmailTemplate;
                reservation.UpdateDate = DateTime.Now;

                unitOfWork.Reservation.Update(reservation);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
