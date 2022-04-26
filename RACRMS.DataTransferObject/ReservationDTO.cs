using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RACRMS.DataTransferObject
{
    public partial class ReservationDTO
    {
        public ReservationDTO()
        {
            Contract = new HashSet<ContractDTO>();
        }

        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("Rezervasyon Kodu :")]
        public string ReservationCode { get; set; }
        [DisplayName("Alış Tarihi :")]
        public DateTime StartDate { get; set; }
        [DisplayName("Teslim Tarihi :")]
        public DateTime EndDate { get; set; }
        [DisplayName("Toplam Ücret :")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Onaylandı :")]
        public bool? Approved { get; set; }
        [DisplayName("Onaylayan Kullanıcı :")]
        public int? ApprovingUserId { get; set; }
        [DisplayName("Onay Tarihi :")]
        public DateTime? ApprovalDate { get; set; }
        [DisplayName("Reddeden Kullanıcı :")]
        public int? RejectingUserId { get; set; }
        [DisplayName("Ret Tarihi :")]
        public DateTime? RejectDate { get; set; }
        [DisplayName("E-Posta Gönderildi :")]
        public bool EmailSent { get; set; }
        [DisplayName("E-Posta Gönderim Tarihi :")]
        public DateTime? EmailSentDate { get; set; }
        [DisplayName("E-Posta İçeriği :")]
        public string EmailTemplate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public UserDTO ApprovingUser { get; set; }
        public CarDTO Car { get; set; }
        public CustomerDTO Customer { get; set; }
        public UserDTO RejectingUser { get; set; }
        public ICollection<ContractDTO> Contract { get; set; }


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
    }
}
