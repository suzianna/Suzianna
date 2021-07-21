using System.Net.Http;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Abilities;

namespace Suzianna.Rest.Screenplay.Questions
{
    internal class LastResponseRaw : IQuestion<HttpResponseMessage>
    {
        public HttpResponseMessage AnsweredBy(Actor actor)
        {
            var callApi = actor.AbilityTo<CallAnApi>();
            return callApi.LastResponse;
        }
    }
}