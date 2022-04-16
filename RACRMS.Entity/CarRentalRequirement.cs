using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class CarRentalRequirement
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int RequirementId { get; set; }

        public Car Car { get; set; }
        public Requirement Requirement { get; set; }
    }
}
