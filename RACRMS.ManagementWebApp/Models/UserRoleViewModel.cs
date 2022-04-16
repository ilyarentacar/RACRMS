using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Models
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel(bool OpenInsertPopup = false, bool OpenUpdatePopup = false, bool OpenDeletePopup = false)
        {
            this.OpenInsertPopup = OpenInsertPopup;
            this.OpenUpdatePopup = OpenUpdatePopup;
            this.OpenDeletePopup = OpenDeletePopup;

            UserRoles = new List<UserRoleDTO>();
        }

        public bool OpenInsertPopup { get; }
        public bool OpenUpdatePopup { get; }
        public bool OpenDeletePopup { get; set; }

        public UserRoleDTO UserRole { get; set; }

        public List<UserRoleDTO> UserRoles { get; set; }
    }
}
