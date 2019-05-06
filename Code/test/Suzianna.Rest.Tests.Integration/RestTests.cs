using System;
using System.Collections.Generic;
using System.Net;
using NFluent;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Abilities;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;
using Suzianna.Rest.Tests.Integration.Collections;
using Suzianna.Rest.Tests.Integration.Constants;
using Suzianna.Rest.Tests.Integration.Model;
using Xunit;

namespace Suzianna.Rest.Tests.Integration
{
    [Collection(TestCollections.NeedsHost)]
    public class RestTests
    {
        private readonly HostFixture _fixture;
        public RestTests(HostFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void should_post_a_person_to_api()
        {
            var jack = Actor.Named("Jack").WhoCan(CallAnApi.At(_fixture.Host.BaseUrl));
            var person = new CreatePerson { Firstname = "Sarah", Lastname = "Ohara"};

            jack.AttemptsTo(Post.DataAsJson(person).To("api/People"));

            jack.AttemptsTo(Get.ResourceAt("api/People"));

            jack.Should(See.That(LastResponse.Content<List<Person>>()))
                .HasElementAt(0).Which.HasFieldsWithSameValues(person);
        }
    }
}
