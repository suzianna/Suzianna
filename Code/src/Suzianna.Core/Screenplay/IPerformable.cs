namespace Suzianna.Core.Screenplay
{
    public interface IPerformable
    {
        void PerformAs<T>(T actor) where T : Actor;
    }
}
