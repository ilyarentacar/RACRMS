using MassTransit;
using RACRMS.MailServiceShared.Abstract;
using RACRMS.MailServiceShared.Events;
using RACRMS.MailServiceShared.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.MailServiceWebAPI.Consumers
{
    public class ReservationRejectedEventConsumer : IConsumer<ReservationRejectedEvent>
    {
        private readonly ISendEndpointProvider sendEndpointProvider;
        private readonly IEmailHelper emailHelper;

        public ReservationRejectedEventConsumer(ISendEndpointProvider sendEndpointProvider, IEmailHelper emailHelper)
        {
            this.sendEndpointProvider = sendEndpointProvider;
            this.emailHelper = emailHelper;
        }

        public async Task Consume(ConsumeContext<ReservationRejectedEvent> context)
        {
            ISendEndpoint sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.ReservationRejectedMailSentEventQueue}"));
            
            string name = context.Message.Name;
            string surname = context.Message.Surname;
            string emailAddress = context.Message.EmailAddress;
            string reservationStartDate = context.Message.ReservationStartDate;
            string reservationEndDate = context.Message.ReservationEndDate;

            string emailTemplate = await emailHelper.SendReservationRejectedEmail(emailAddress, name, surname, reservationStartDate, reservationEndDate);

            await sendEndpoint.Send(new ReservationRejectedMailSentEvent()
            {
                ReservationId = context.Message.ReservationId,
                EmailSentDate = DateTime.Now,
                EmailTemplate = emailTemplate
            });
        }
    }
}
