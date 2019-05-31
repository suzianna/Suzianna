using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Abilities;

namespace Suzianna.Rest.Screenplay.Questions
{
    internal class LastResponseHeader : IQuestion<string>
    {
        private readonly string _key;
        public LastResponseHeader(string key)
        {
            _key = key;
        }
        public string AnsweredBy(Actor actor)
        {
            var callApi = actor.FindAbility<CallAnApi>();
            return callApi.LastResponse.Headers.FirstOrDefault(a => a.Key == this._key).Value.FirstOrDefault();
        }
    }
}
