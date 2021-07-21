using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Core.Screenplay.Actors
{
    public interface IReferToActor
    {
        T AsActor<T>(Actor actor) where T : class, IAbility;
    }
}
