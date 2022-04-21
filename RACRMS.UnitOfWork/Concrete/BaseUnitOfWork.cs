using Microsoft.EntityFrameworkCore;
using RACRMS.DataAccessLayer.Abstract;
using RACRMS.DataAccessLayer.Concrete;
using RACRMS.Entity;
using RACRMS.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.UnitOfWork.Concrete
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        private static readonly object lockerObject = new object();

        private DbContext _dbContext;

        private DbContext dbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    lock (lockerObject)
                    {
                        if (_dbContext == null)
                        {
                            _dbContext = new ILYA_RACRMSContext();
                        }
                    }
                }

                return _dbContext;
            }
        }

        public ICarBrandDAL CarBrand => new CarBrandDAL(dbContext);

        public ICarChassisTypeDAL CarChassisType => new CarChassisTypeDAL(dbContext);

        public ICarClassDAL CarClass => new CarClassDAL(dbContext);

        public ICarDAL Car => new CarDAL(dbContext);

        public ICarFuelTypeDAL CarFuelType => new CarFuelTypeDAL(dbContext);

        public ICarGearTypeDAL CarGearType => new CarGearTypeDAL(dbContext);

        public ICarModelDAL CarModel => new CarModelDAL(dbContext);

        public ICarPreferenceDAL CarPreference => new CarPreferenceDAL(dbContext);

        public ICarRentalPriceDAL CarRentalPrice => new CarRentalPriceDAL(dbContext);

        public ICarRentalRequirementDAL CarRentalRequirement => new CarRentalRequirementDAL(dbContext);

        public ICarTypeDAL CarType => new CarTypeDAL(dbContext);

        public ICustomerDAL Customer => new CustomerDAL(dbContext);

        public IPreferenceDAL Preference => new PreferenceDAL(dbContext);

        public IRequirementDAL Requirement => new RequirementDAL(dbContext);

        public IReservationDAL Reservation => new ReservationDAL(dbContext);

        public IUserDAL User => new UserDAL(dbContext);

        public IUserRoleDAL UserRole => new UserRoleDAL(dbContext);

        public IPaymentTypeDAL PaymentType => new PaymentTypeDAL(dbContext);

        public IContractDAL Contract => new ContractDAL(dbContext);

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                dbContext.Dispose();
            }
        }
    }
}
