using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.MailServiceShared.Events
{
    public class ReservationAcceptedMailSentEvent
    {
        public int ReservationId { get; set; }        
        public DateTime EmailSentDate { get; set; }
        public string EmailTemplate { get; set; }
    }
}
