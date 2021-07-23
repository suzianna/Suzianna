using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using NFluent;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests
{
    public class ValueConversionTests
    {
        [Fact]
        public void can_do_implicit_cast_on_string()
        {
            Value<string> name = "jack";

            string name2 = name;

            Check.That(name2).IsEqualTo("jack");
        }

        [Fact]
        public void can_convert_value_to_string()
        {
            Value<int> number = 1;

            Check.That(number.AsString()).Equals("1");
        }

        [Fact]
        public void can_convert_value_to_datetime()
        {
            Value<string> date = "2000-01-01";
            var expected = new DateTime(2000,01,01);

            Check.That(date.AsDateTime()).IsEqualTo(expected);
        }

        [Fact]
        public void can_convert_value_to_boolean()
        {
            Value<string> value = "true";

            Check.That(value.AsBoolean()).IsTrue();
        }

        [Fact]
        public void can_convert_value_to_long()
        {
            Value<long> value = 1000000000;

            Check.That(value.AsLong()).IsEqualTo(1000000000);
        }

        [Fact]
        public void can_convert_value_to_int()
        {
            Value<int> value = 10000;

            Check.That(value.AsInt()).IsEqualTo(10000);
        }

        [Fact]
        public void can_convert_value_to_short()
        {
            Value<short> value = 1000;

            Check.That(value.AsShort()).IsEqualTo(1000);
        }

        [Fact]
        public void can_convert_value_to_character()
        {
            Value<string> value = "H";

            Check.That(value.AsCharacter()).IsEqualTo('H');
        }

        [Fact]
        public void can_convert_value_to_timespan()
        {
            Value<string> value = "20:50";
            var expected = new TimeSpan(0, 20, 50,0);

            Check.That(value.AsTimespan()).IsEqualTo(expected);
        }
    }
}
