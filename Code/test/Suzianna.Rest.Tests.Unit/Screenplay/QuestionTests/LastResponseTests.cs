using System.Net.Http;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Rest.Screenplay.Abilities;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestDoubles;
using Suzianna.Rest.Tests.Unit.TestUtils;

namespace Suzianna.Rest.Tests.Unit.Screenplay.QuestionTests
{
    public abstract class LastResponseTests
    {
        public Actor Actor { get; private set; }
        protected void SetupResponse(HttpResponseMessage response)
        {
            var sender = new FakeHttpRequestSender();
            sender.SetupResponse(response);
            this.Actor = ActorFactory.CreateSomeActorWithApiCallAbility(sender);
        }

    }
}