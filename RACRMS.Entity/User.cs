using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class User
    {
        public User()
        {
            ReservationApprovingUser = new HashSet<Reservation>();
            ReservationRejectingUser = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Editable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public UserRole UserRole { get; set; }
        public ICollection<Reservation> ReservationApprovingUser { get; set; }
        public ICollection<Reservation> ReservationRejectingUser { get; set; }
    }
}
