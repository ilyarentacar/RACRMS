using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class Reservation
    {
        public Reservation()
        {
            Contract = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public string ReservationCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool? Approved { get; set; }
        public int? ApprovingUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? RejectingUserId { get; set; }
        public DateTime? RejectDate { get; set; }
        public bool EmailSent { get; set; }
        public DateTime? EmailSentDate { get; set; }
        public string EmailTemplate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public User ApprovingUser { get; set; }
        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public User RejectingUser { get; set; }
        public ICollection<Contract> Contract { get; set; }
    }
}
