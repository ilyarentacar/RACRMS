using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.MailServiceShared.Events
{
    public class ReservationRejectedEvent
    {
        public int ReservationId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string ReservationStartDate { get; set; }
        public string ReservationEndDate { get; set; }
    }
}
