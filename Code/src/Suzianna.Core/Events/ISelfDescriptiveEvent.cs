using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Core.Events
{
    public interface ISelfDescriptiveEvent : IEvent, ICanDescribeMyself
    {
    }
}
