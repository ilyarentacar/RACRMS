using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Contract = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<Contract> Contract { get; set; }
    }
}
