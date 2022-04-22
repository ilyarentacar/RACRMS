using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.Extension
{
    public static class ContractExtensions
    {
        public static ContractDTO ToDTO(this Contract entity)
        {
            try
            {
                return new ContractDTO()
                {
                    Id = entity.Id,
                    ReservationId = entity.ReservationId,
                    CarId = entity.CarId,
                    CustomerId = entity.CustomerId,
                    StartDate = entity.StartDate,
                    PlanedEndDate = entity.PlanedEndDate,
                    EndDate = entity.EndDate,
                    ContractCompleted = entity.ContractCompleted,
                    ContractCompletedDate = entity.ContractCompletedDate,
                    TotalPrice = entity.TotalPrice,
                    HasPaid = entity.HasPaid,
                    PaymentTypeId = entity.PaymentTypeId,
                    CardOwnerName = entity.CardOwnerName,
                    CardNumber = entity.CardNumber,
                    CardValidDate = entity.CardValidDate,
                    CardCvv = entity.CardCvv,
                    EmailSent = entity.EmailSent,
                    EmailSentDate = entity.EmailSentDate,
                    EmailTemplate = entity.EmailTemplate,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate,
                    Reservation = entity.Reservation != null ? entity.Reservation.ToDTO() : new ReservationDTO(),
                    Car = entity.Car != null ? entity.Car.ToDTO() : new CarDTO(),
                    Customer = entity.Customer != null ? entity.Customer.ToDTO() : new CustomerDTO(),
                    PaymentType = entity.PaymentType != null ? entity.PaymentType.ToDTO() : new PaymentTypeDTO(),
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
