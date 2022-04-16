using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class Requirement
    {
        public Requirement()
        {
            CarRentalRequirement = new HashSet<CarRentalRequirement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<CarRentalRequirement> CarRentalRequirement { get; set; }
    }
}
