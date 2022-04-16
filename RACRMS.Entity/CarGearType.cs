using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class CarGearType
    {
        public CarGearType()
        {
            Car = new HashSet<Car>();
            CarModel = new HashSet<CarModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<Car> Car { get; set; }
        public ICollection<CarModel> CarModel { get; set; }
    }
}
