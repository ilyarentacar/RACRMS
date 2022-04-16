using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.MailServiceShared.Abstract
{
    public interface IEmailHelper
    {
        Task SendPasswordRecoveryEmail(string emailAddress, string name, string surname, string username, string password);
        Task<string> SendReservationApprovedEmail(string emailAddress, string name, string surname, string reservationStartDate, string reservationEndDate);
        Task<string> SendReservationRejectedEmail(string emailAddress, string name, string surname, string reservationStartDate, string reservationEndDate);
    }
}
