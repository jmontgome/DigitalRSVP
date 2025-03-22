namespace DigitalRSVP.WAPI.Services.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(string subject, string body, string recipientAddress);
    }
}
