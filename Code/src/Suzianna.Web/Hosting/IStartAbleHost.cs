using System.Threading.Tasks;

namespace Suzianna.Web.Hosting
{
    public interface IStartableHost : IHost
    {
        Task Start();
        Task Stop();
    }
}