using MassTransit;
using RACRMS.MailServiceShared.Abstract;
using RACRMS.MailServiceShared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RACRMS.MailServiceWebAPI.Consumers
{
    public class PasswordRecoveryEmailEventConsumer : IConsumer<PasswordRecoveryEmailEvent>
    {
        private readonly IEmailHelper emailHelper;
        public PasswordRecoveryEmailEventConsumer(IEmailHelper emailHelper)
        {
            this.emailHelper = emailHelper;
        }

        public async Task Consume(ConsumeContext<PasswordRecoveryEmailEvent> context)
        {
            try
            {
                string name = context.Message.Name;
                string surname = context.Message.Surname;
                string emailAddress = context.Message.EmailAddress;
                string username = context.Message.Username;
                string password = context.Message.Password;

                await emailHelper.SendPasswordRecoveryEmail(emailAddress, name, surname, username, password);
            }
            catch
            {
                throw;
            }
        }
    }
}
