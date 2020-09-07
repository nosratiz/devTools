using System.Threading.Tasks;

namespace DevTools.Application.Common.Interfaces
{
    public interface IEmailServices
    {
        Task SendMessage(string emailTo, string subject, string body);
    }
}