using System;

namespace Suzianna.Core.Screenplay.Actors
{
    public class ActorIsUnableException<T> : Exception  where T: IAbility
    {
        public ActorIsUnableException(Actor actor) 
            :base(PrepareMessage(actor))
        {
            
        }
        private static string PrepareMessage(Actor actor)
        {
            return string.Format(ExceptionFormats.ActorUnableError, actor.Name, typeof(T).Name);
        }
    }
}
