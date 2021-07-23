using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NFluent;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests
{
    public class ValueImplicitCastTests
    {
        [Fact]
        public void can_do_implicit_cast_on_string()
        {
            Value<string> name = "jack";

            string name2 = name;

            Check.That(name2).IsEqualTo("jack");
        }
    }
}
