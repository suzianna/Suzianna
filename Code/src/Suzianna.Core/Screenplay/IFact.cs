using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay
{
    public interface IFact
    {
        void Setup(Actor actor);
        void Teardown(Actor actor);
    }
}
