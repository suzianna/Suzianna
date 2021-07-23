using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Core;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.WebDriver.Screenplay.Questions
{
    public abstract class UserInterfaceState<T>
    {
        public abstract Value<T> ViewedBy(Actor actor);
    }
}
