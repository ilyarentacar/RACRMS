using MassTransit;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.MailServiceShared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Consumers
{
    public class ReservationAcceptedMailSentEventConsumer : IConsumer<ReservationAcceptedMailSentEvent>
    {
        private readonly IReservationBL reservationBL;

        public ReservationAcceptedMailSentEventConsumer(IReservationBL reservationBL)
        {
            this.reservationBL = reservationBL;
        }

        public async Task Consume(ConsumeContext<ReservationAcceptedMailSentEvent> context)
        {
            try
            {
                var reservation = await reservationBL.GetByIdAsync(context.Message.ReservationId);

                reservation.EmailSent = true;
                reservation.EmailSentDate = context.Message.EmailSentDate;
                reservation.EmailTemplate = context.Message.EmailTemplate;

                await reservationBL.UpdateAfterSentEmailAsync(reservation);
            }
            catch
            {
                throw;
            }
        }
    }
}
