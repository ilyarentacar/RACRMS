using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RACRMS.DataTransferObject
{
    public partial class UserRoleDTO
    {
        public UserRoleDTO()
        {
            User = new List<UserDTO>();
        }

        public int Id { get; set; }

        [DisplayName("Kullanıcı Rolü :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(15)]
        public string Name { get; set; }
        public bool Usable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public List<UserDTO> User { get; set; }
    }
}
