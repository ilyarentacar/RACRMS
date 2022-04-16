using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Models
{
    public class ReservationViewModel
    {
        public ReservationViewModel(bool OpenReservationAcceptPopup = false, bool OpenReservationRejectPopup = false, bool OpenDeletePopup = false, bool OpenDetailPopup = false)
        {
            this.OpenReservationAcceptPopup = OpenReservationAcceptPopup;
            this.OpenReservationRejectPopup = OpenReservationRejectPopup;
            this.OpenDeletePopup = OpenDeletePopup;
            this.OpenDetailPopup = OpenDetailPopup;

            Reservations = new List<ReservationDTO>();
        }

        public bool OpenReservationAcceptPopup { get; }
        public bool OpenReservationRejectPopup { get; }
        public bool OpenDeletePopup { get; }
        public bool OpenDetailPopup { get; }

        public ReservationDTO Reservation { get; set; }

        public List<ReservationDTO> Reservations { get; set; }
    }
}
