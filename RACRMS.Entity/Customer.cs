using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class Customer
    {
        public Customer()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long IdentityNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CellNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<Reservation> Reservation { get; set; }
    }
}
