using DigitalRSVP.WAPI.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace DigitalRSVP.WAPI.Services
{
    /*
     * Setup & Configuration
     * 
     * - Google
     *      1. Turn on 2FA
     *      2. Set up an App Password
     *      3. Use App Password as the NetworkCredentials Password
     */

    public class EmailService: IEmailService
    {
        private readonly EmailCredentials _credentials;

        public EmailService(ApplicationEnvironmentVariables variables)
        {
            this._credentials = variables.EmailCredentials;
            if (!this._credentials.Ready)
            {
                throw new InvalidDataException($"Email Credentials have not been set up!");
            }
        }

        public void SendEmail(string subject, string body, string recipientAddress, IEnumerable<Attachment> attachments)
        {
            using (SmtpClient smtpClient = new SmtpClient(this._credentials.Host, int.Parse(this._credentials.Port)))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(this._credentials.Username, this._credentials.Password);

                MailMessage message = new MailMessage(this._credentials.Username, recipientAddress, subject, body);

                if (attachments != null)
                {
                    foreach (Attachment attachment in attachments)
                    {
                        message.Attachments.Add(attachment);
                    }
                }

                smtpClient.Send(message);
            }
        }
    }
}
