using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class CarRentalPrice
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Car Car { get; set; }
    }
}
