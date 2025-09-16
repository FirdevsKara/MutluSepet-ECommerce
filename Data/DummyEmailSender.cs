using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace MutluSepet.Data
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Bu metod hiçbir şey yapmaz, sadece Identity UI'nın çalışmasını sağlar
            return Task.CompletedTask;
        }
    }
}
