using System;

namespace Suzianna.Reporting.Tests.Unit.TestDoubles
{
    public class StubClock : IClock
    {
        private DateTime _now;

        public StubClock(DateTime now)
        {
            TimeTravelTo(now);
        }

        public StubClock()
        {
            TimeTravelTo(DateTime.Now);
        }

        public DateTime Now()
        {
            return _now;
        }

        public static StubClock CreateWithTimeTravelingTo(DateTime date)
        {
            return new StubClock(date);
        }

        public void TimeTravelTo(DateTime targetDateTime)
        {
            _now = targetDateTime;
        }
    }
}