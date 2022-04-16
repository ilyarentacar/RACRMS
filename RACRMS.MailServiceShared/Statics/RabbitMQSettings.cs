using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.MailServiceShared.Statics
{
    public static class RabbitMQSettings
    {
        public static string ReservationAcceptedEventQueue { get => "reservation_accepted_event_queue"; }
        public static string ReservationRejectedEventQueue { get => "reservation_rejected_event_queue"; }
        public static string ReservationAcceptedMailSentEventQueue { get => "reservation_accepted_mail_sent_event_queue"; }
        public static string ReservationRejectedMailSentEventQueue { get => "reservation_rejected_mail_sent_event_queue"; }
        public static string PasswordRecoveryEmailEventQueue { get => "password_recovery_email_event_queue"; }
    }
}
