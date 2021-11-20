using Suzianna.Core.Screenplay.Actors;
using System.Threading.Tasks;

namespace Suzianna.Core.Screenplay
{
    /// <summary>
    /// A task or interaction that can be performed by an actor.
    /// </summary>
    public interface IPerformable
    {
        Task PerformAs<T>(T actor) where T : Actor;
    }
}
