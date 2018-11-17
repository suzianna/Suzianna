using System;
using FluentAssertions;
using Xunit;

namespace Suzianna.Core.Tests.Unit
{
    public class StringExtensionsTests
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
        public void should_Surround_string_by_double_quotes()
        {
            var input = "Data";
            var expectedResult = "\"Data\"";

            var actualResult = input.SurroundByDoubleQuotes();

            actualResult.Should().Be(expectedResult);
        }
    }
}
