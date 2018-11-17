using System.Threading.Tasks;

namespace Suzianna.Web.Hosting
{
    public interface IStartAbleHost : IHost
    {
        Task Start();
        Task Stop();
    }
}