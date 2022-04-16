using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.Extension
{
    public static class ReservationExtensions
    {
        public static ReservationDTO ToDTO(this Reservation entity)
        {
            try
            {
                return new ReservationDTO()
                {
                    Id = entity.Id,
                    CarId = entity.CarId,
                    PlateNumber = entity.Car != null ? entity.Car.PlateNumber : string.Empty,
                    CarBrandName = entity.Car != null && entity.Car.CarBrand != null ? entity.Car.CarBrand.Name : string.Empty,
                    CarModelName = entity.Car != null && entity.Car.CarModel != null ? entity.Car.CarModel.Name : string.Empty,
                    CustomerId = entity.CustomerId,
                    CustomerName = entity.Customer != null ? entity.Customer.Name : string.Empty,
                    CustomerSurname = entity.Customer != null ? entity.Customer.Surname : string.Empty,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    TotalPrice = entity.TotalPrice,
                    Approved = entity.Approved,
                    ApprovingUserId = entity.ApprovingUserId,
                    ApprovalDate = entity.ApprovalDate,
                    RejectingUserId = entity.RejectingUserId,
                    RejectDate = entity.RejectDate,
                    EmailSent = entity.EmailSent,
                    EmailSentDate = entity.EmailSentDate,
                    EmailTemplate = entity.EmailTemplate,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate,
                    ApprovingUser = entity.ApprovingUser != null ? entity.ApprovingUser.ToDTO() : new UserDTO(),
                    Car = entity.Car != null ? entity.Car.ToDTO() : new CarDTO(),
                    Customer = entity.Customer != null ? entity.Customer.ToDTO() : new CustomerDTO(),
                    RejectingUser = entity.RejectingUser != null ? entity.RejectingUser.ToDTO() : new UserDTO(),
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
