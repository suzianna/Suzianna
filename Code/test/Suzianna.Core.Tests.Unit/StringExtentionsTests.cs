using System;
using FluentAssertions;
using TestStack.BDDfy;
using Xunit;

namespace Suzianna.Core.Tests.Unit
{
    public class StringExtentionsTests
    {
        private string _input;
        private string _reuslt;

        [Fact]
        public void should_surround_string_by_desired_string()
        {
            this.Given(a => a.GivenTheInputIs("Data"))
                .When(a => a.WhenIRequestToSurroundItWith("?"))
                .Then(a => a.ThenResultShouldBe("?Data?"))
                .BDDfy();
        }

        [Fact]
        public void should_Surround_string_by_double_qoutes()
        {
            this.Given(a => a.GivenTheInputIs("Data"))
                .When(a => a.WhenIRequestToSurroundItWithDoubleQoutes())
                .Then(a => a.ThenResultShouldBe("\"Data\""))
                .BDDfy();
        }

        private void GivenTheInputIs(string data)
        {
            this._input = data;
        }
        private void WhenIRequestToSurroundItWith(string surroundCharater)
        {
            this._reuslt = _input.SurroundBy(surroundCharater);
        }
        private void WhenIRequestToSurroundItWithDoubleQoutes()
        {
            this._reuslt = _input.SurroundByDoubleQoutes();
        }
        private void ThenResultShouldBe(string expectedResult)
        {
            this._reuslt.Should().Be(expectedResult);
        }
    }
}
