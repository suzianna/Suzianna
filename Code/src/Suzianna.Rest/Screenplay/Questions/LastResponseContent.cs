using Newtonsoft.Json;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Abilities;

namespace Suzianna.Rest.Screenplay.Questions
{
    public class LastResponseTypedContent<T> : IQuestion<T>
    {
        public T AnsweredBy(Actor actor)
        {
            var apiAbility = actor.FindAbility<CallAnApi>();
            var content = apiAbility.LastResponse.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
