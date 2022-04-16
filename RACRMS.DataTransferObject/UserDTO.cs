using RACRMS.DataTransferObject.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RACRMS.DataTransferObject
{
    public partial class UserDTO
    {
        public UserDTO()
        {
            Reservation = new List<ReservationDTO>();
            UserRoles = new List<UserRoleDTO>();
        }

        public int Id { get; set; }
        public int UserRoleId { get; set; }

        [DisplayName("Adı :")]
        public string Name { get; set; }

        [DisplayName("Soyadı :")]
        public string Surname { get; set; }

        [DisplayName("E-Posta Adresi :")]
        public string EmailAddress { get; set; }

        [DisplayName("Kullanıcı Adı :")]
        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MaxLength(25)]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [DisplayName("Şifre :")]
        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Editable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public UserRoleDTO UserRole { get; set; }
        public List<ReservationDTO> Reservation { get; set; }

        [DisplayName("Kullanıcı Rolü :")]
        public string UserRoleName { get; set; }

        public List<UserRoleDTO> UserRoles { get; set; }

        //**CUSTOME PROPERTIES**//
        public bool RememberMe { get; set; }
    }
}
