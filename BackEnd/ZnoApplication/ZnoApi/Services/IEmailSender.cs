using System.Threading.Tasks;

namespace ZnoApi.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}