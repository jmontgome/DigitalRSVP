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

        public void SendEmail(string subject, string body, string recipientAddress)
        {
            using (SmtpClient smtpClient = new SmtpClient(this._credentials.Host, int.Parse(this._credentials.Port)))
            {
                MailAddress from = new MailAddress(this._credentials.Username);
                MailAddress to = new MailAddress(recipientAddress);

                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(this._credentials.Username, this._credentials.Password);

                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;

                smtpClient.Send(message);
            }
        }
    }
}
