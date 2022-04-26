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
                    ReservationCode = entity.Reservation != null ? entity.Reservation.ReservationCode : string.Empty,
                    CarId = entity.CarId,
                    PlateNumber = entity.Car != null ? entity.Car.PlateNumber : string.Empty,
                    CarBrandName = entity.Car != null && entity.Car.CarBrand != null ? entity.Car.CarBrand.Name : string.Empty,
                    CarModelName = entity.Car != null && entity.Car.CarModel != null ? entity.Car.CarModel.Name : string.Empty,
                    CustomerId = entity.CustomerId,
                    CustomerName = entity.Customer != null ? entity.Customer.Name : string.Empty,
                    CustomerSurname = entity.Customer != null ? entity.Customer.Surname : string.Empty,
                    CustomerEmailAddress = entity.Customer != null ? entity.Customer.EmailAddress : string.Empty,
                    CustomerCellNumber = entity.Customer != null ? entity.Customer.CellNumber : string.Empty,
                    StartDate = entity.StartDate,
                    PlanedEndDate = entity.PlanedEndDate,
                    EndDate = entity.EndDate,
                    ContractCompleted = entity.ContractCompleted,
                    ContractCompletedDate = entity.ContractCompletedDate,
                    TotalPrice = entity.TotalPrice,
                    HasPaid = entity.HasPaid,
                    PaymentTypeId = entity.PaymentTypeId,
                    PaymentTypeName = entity.PaymentType != null ? entity.PaymentType.Name : string.Empty,
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
