namespace Suzianna.Core.Screenplay
{
    public class Actor
    {
        public string Name { get;private set; }
        public Actor(string name)
        {
            this.Name = name;
        }

        public static Actor Named(string name)
        {
            return new Actor(name);
        }
    }
}