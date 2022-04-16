using RACRMS.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.UnitOfWork.Abstract
{
    public interface IBaseUnitOfWork
    {
        ICarBrandDAL CarBrand { get; }
        ICarChassisTypeDAL CarChassisType { get; }
        ICarClassDAL CarClass { get; }
        ICarDAL Car { get; }
        ICarFuelTypeDAL CarFuelType { get; }
        ICarGearTypeDAL CarGearType { get; }
        ICarModelDAL CarModel { get; }
        ICarPreferenceDAL CarPreference { get; }
        ICarRentalPriceDAL CarRentalPrice { get; }
        ICarRentalRequirementDAL CarRentalRequirement { get; }
        ICarTypeDAL CarType { get; }
        ICustomerDAL Customer { get; }
        IPreferenceDAL Preference { get; }
        IRequirementDAL Requirement { get; }
        IReservationDAL Reservation { get; }
        IUserDAL User { get; }
        IUserRoleDAL UserRole { get; }

        Task<int> SaveChangesAsync();
    }
}
