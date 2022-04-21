using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class Car
    {
        public Car()
        {
            CarPreference = new HashSet<CarPreference>();
            CarRentalPrice = new HashSet<CarRentalPrice>();
            CarRentalRequirement = new HashSet<CarRentalRequirement>();
            Contract = new HashSet<Contract>();
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public int CarClassId { get; set; }
        public int CarTypeId { get; set; }
        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public int CarChassisTypeId { get; set; }
        public int CarFuelTypeId { get; set; }
        public int CarGearTypeId { get; set; }
        public bool Rentable { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CarBrand CarBrand { get; set; }
        public CarChassisType CarChassisType { get; set; }
        public CarClass CarClass { get; set; }
        public CarFuelType CarFuelType { get; set; }
        public CarGearType CarGearType { get; set; }
        public CarModel CarModel { get; set; }
        public CarType CarType { get; set; }
        public ICollection<CarPreference> CarPreference { get; set; }
        public ICollection<CarRentalPrice> CarRentalPrice { get; set; }
        public ICollection<CarRentalRequirement> CarRentalRequirement { get; set; }
        public ICollection<Contract> Contract { get; set; }
        public ICollection<Reservation> Reservation { get; set; }
    }
}
