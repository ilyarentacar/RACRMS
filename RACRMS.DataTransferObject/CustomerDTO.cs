using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RACRMS.DataTransferObject
{
    public partial class CustomerDTO
    {
        public CustomerDTO()
        {
            Contract = new HashSet<ContractDTO>();
            Reservation = new List<ReservationDTO>();
        }

        public int Id { get; set; }

        [DisplayName("Adı :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(25)]
        public string Name { get; set; }

        [DisplayName("Soyadı :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(40)]
        public string Surname { get; set; }

        [DisplayName("TC Kimlik Numarası :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public long IdentityNumber { get; set; }

        [DisplayName("E-Posta Adresi :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(35)]
        public string EmailAddress { get; set; }

        [DisplayName("Cep Telefonu :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(20)]
        public string CellNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<ContractDTO> Contract { get; set; }
        public List<ReservationDTO> Reservation { get; set; }
    }
}
