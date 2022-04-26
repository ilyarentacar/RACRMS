using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class Contract
    {
        public int Id { get; set; }
        public int? ReservationId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PlanedEndDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? ContractCompleted { get; set; }
        public DateTime? ContractCompletedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool HasPaid { get; set; }
        public int? PaymentTypeId { get; set; }
        public string CardOwnerName { get; set; }
        public string CardNumber { get; set; }
        public string CardValidDate { get; set; }
        public string CardCvv { get; set; }
        public bool EmailSent { get; set; }
        public DateTime? EmailSentDate { get; set; }
        public string EmailTemplate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public PaymentType PaymentType { get; set; }
        public Reservation Reservation { get; set; }
    }
}
