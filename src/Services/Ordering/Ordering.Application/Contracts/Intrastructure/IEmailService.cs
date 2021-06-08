using Ordering.Application.Models;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Intrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email emial);
    }
}
