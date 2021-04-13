using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Core.Screenplay;

namespace Suzianna.WebDriver.Screenplay.Actors
{
    public class OnlineCast : Cast
    {
        public OnlineCast(List<IAbility> abilities) : base(abilities)
        {
        }

        public new static OnlineCast WhereEveryoneCan(List<IAbility> abilities)
        {
            return new OnlineCast(abilities);
        }
    }
}
