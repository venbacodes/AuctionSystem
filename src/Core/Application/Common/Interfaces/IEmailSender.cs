namespace Application.Common.Interfaces
{
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string receiver, string subject, string htmlMessage);
    }
}