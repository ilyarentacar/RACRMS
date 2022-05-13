using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class CarModel
    {
        public CarModel()
        {
            Car = new HashSet<Car>();
        }

        public int Id { get; set; }
        public int CarClassId { get; set; }
        public int CarTypeId { get; set; }
        public int CarBrandId { get; set; }
        public int CarChassisTypeId { get; set; }
        public int CarFuelTypeId { get; set; }
        public int CarGearTypeId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CarBrand CarBrand { get; set; }
        public CarChassisType CarChassisType { get; set; }
        public CarClass CarClass { get; set; }
        public CarFuelType CarFuelType { get; set; }
        public CarGearType CarGearType { get; set; }
        public CarType CarType { get; set; }
        public ICollection<Car> Car { get; set; }
    }
}
