using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.DataTransferObject
{
    public partial class ContractDTO
    {
        public int Id { get; set; }
        public int? ReservationId { get; set; }
        [DisplayName("Rezervasyon Kodu :")]
        public string ReservationCode { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("Alış Tarihi :")]
        public DateTime StartDate { get; set; }
        [DisplayName("Planlanan Teslim Tarihi :")]
        public DateTime PlanedEndDate { get; set; }
        [DisplayName("Teslim Tarihi :")]
        public DateTime? EndDate { get; set; }
        [DisplayName("Sözleşme Durumu :")]
        public bool? ContractCompleted { get; set; }
        [DisplayName("Sözleşme Tamamlanma Tarihi :")]
        public DateTime? ContractCompletedDate { get; set; }
        [DisplayName("Toplam Ücret :")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Ödeme Yapıldı :")]
        public bool HasPaid { get; set; }
        public int? PaymentTypeId { get; set; }
        [DisplayName("Kart Sahibi :")]
        public string CardOwnerName { get; set; }
        [DisplayName("Kart Numarası :")]
        public string CardNumber { get; set; }
        [DisplayName("Kart Geçerlilik Tarihi :")]
        public string CardValidDate { get; set; }
        [DisplayName("Kart CVV Kodu :")]
        public string CardCvv { get; set; }
        [DisplayName("E-Posta Gönderildi :")]
        public bool EmailSent { get; set; }
        [DisplayName("E-Posta Gönderim Tarihi :")]
        public DateTime? EmailSentDate { get; set; }
        [DisplayName("E-Posta İçeriği :")]
        public string EmailTemplate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CarDTO Car { get; set; }
        public CustomerDTO Customer { get; set; }
        public PaymentTypeDTO PaymentType { get; set; }
        public ReservationDTO Reservation { get; set; }

        [DisplayName("Plakası :")]
        public string PlateNumber { get; set; }
        [DisplayName("Araç Markası :")]
        public string CarBrandName { get; set; }
        [DisplayName("Araç Modeli :")]
        public string CarModelName { get; set; }
        [DisplayName("Müşteri Adı :")]
        public string CustomerName { get; set; }
        [DisplayName("Müşteri Soyadı :")]
        public string CustomerSurname { get; set; }
        [DisplayName("Müşteri Eposta Adresi :")]
        public string CustomerEmailAddress { get; set; }
        [DisplayName("Müşteri Cep Telefonu :")]
        public string CustomerCellNumber { get; set; }
        [DisplayName("Ödeme Türü :")]
        public string PaymentTypeName { get; set; }
    }
}
