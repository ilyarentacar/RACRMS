using RACRMS.MailServiceShared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.MailServiceShared.Concrete
{
    public class EmailHelper : IEmailHelper
    {
        private string From { get => "info@ilyarentacar.com.tr"; }
        private string Host { get => "ilyarentacar.com.tr"; }
        private int Port { get => 587; }
        private string Username { get => "info@ilyarentacar.com.tr"; }
        private string Password { get => "vy82@3eG"; }

        public async Task SendPasswordRecoveryEmail(string emailAddress, string name, string surname, string username, string password)
        {
            try
            {
                string body = $"Sayın {name} {surname}, bu e-postayı isteğiniz üzerine alıyorsunuz.\n\rGiriş bilgileriniz aşağıdaki gibidir;\n\rKullanıcı Adı : {username}\n\rŞifre : {password}\n\rEğer bu e-postayı bilginiz dışında aldıysanız, lütfen sistem yöneticisi ile irtibata geçiniz.";

                MailMessage mailMessage = new MailMessage(From, emailAddress)
                {
                    Subject = "İlya Rent A Car giriş bilgileri hatırlatma",
                    Body = body
                };

                SmtpClient smtpClient = new SmtpClient(Host)
                {
                    Port = Port,
                    Credentials = new NetworkCredential(Username, Password),
                    EnableSsl = false
                };

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> SendReservationApprovedEmail(string emailAddress, string name, string surname, string reservationStartDate, string reservationEndDate)
        {
            try
            {
                string body = $"Sayın {name} {surname},\n\r{reservationStartDate} - {reservationEndDate} tarihleri arasındaki rezervasyonunuz ön onay almıştır.\n\rRezervasyon onayınızın tamamlanabilmesi için 0542 154 54 94 numaralı telefondan en kısa sürede sizinle iletişime geçilecektir.\n\rEğer bu e-postayı bilginiz dışında aldıysanız, lütfen bizimle irtibata geçiniz.";

                MailMessage mailMessage = new MailMessage(From, emailAddress)
                {
                    Subject = "İlya Rent A Car rezervasyonunuz onaylandı",
                    Body = body
                };

                SmtpClient smtpClient = new SmtpClient(Host)
                {
                    Port = Port,
                    Credentials = new NetworkCredential(Username, Password),
                    EnableSsl = false
                };

                await smtpClient.SendMailAsync(mailMessage);

                return body;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> SendReservationRejectedEmail(string emailAddress, string name, string surname, string reservationStartDate, string reservationEndDate)
        {
            try
            {
                string body = $"Sayın {name} {surname},\n\r{reservationStartDate} - {reservationEndDate} tarihleri arasındaki rezervasyonunuzun maalesef onaylanmamıştır.\n\rFarklı tarih ve araç seçenekleri için tekrar rezervasyon oluşturabilirsiniz.\n\rEğer bu e-postayı bilginiz dışında aldıysanız, lütfen bizimle irtibata geçiniz.";

                MailMessage mailMessage = new MailMessage(From, emailAddress)
                {
                    Subject = "İlya Rent A Car rezervasyonunuz reddedildi",
                    Body = body
                };

                SmtpClient smtpClient = new SmtpClient(Host)
                {
                    Port = Port,
                    Credentials = new NetworkCredential(Username, Password),
                    EnableSsl = false
                };

                await smtpClient.SendMailAsync(mailMessage);

                return body;
            }
            catch
            {
                throw;
            }
        }
    }
}
