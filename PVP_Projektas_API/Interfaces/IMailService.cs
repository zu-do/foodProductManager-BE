namespace PVP_Projektas_API.Interfaces
{
    public interface IMailService
    {
        public Task SendMail(string to, string subject, string body);
    }
}