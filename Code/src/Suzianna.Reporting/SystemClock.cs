using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Reporting
{
    public class SystemClock : IClock
    {
        public DateTime Now() => DateTime.Now;
    }
}
