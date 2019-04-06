using System.Net;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Abilities;

namespace Suzianna.Rest.Screenplay.Questions
{
    public class LastResponseStatusCode : IQuestion<HttpStatusCode>
    {
        public HttpStatusCode AnsweredBy(Actor actor)
        {
            var callApi = actor.FindAbility<CallAnApi>();
            return callApi.LastResponse.StatusCode;
        }
    }
}
