using System;
using FluentAssertions;
using Xunit;

namespace Suzianna.Core.Tests.Unit
{
    public class StringExtentionsTests
    {
        [Fact]
        public void should_surround_string_by_desired_string()
        {
            var input = "Data";
            var expectedResult = "?Data?";

            var actualResult = input.SurroundBy("?");

            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public void should_Surround_string_by_double_qoutes()
        {
            var input = "Data";
            var expectedResult = "\"Data\"";

            var actualResult = input.SurroundByDoubleQoutes();

            actualResult.Should().Be(expectedResult);
        }
    }
}
