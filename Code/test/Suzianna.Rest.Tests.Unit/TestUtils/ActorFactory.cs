using Suzianna.Core.Screenplay;
using Suzianna.Http.Tests.Unit.TestConstants;
using Suzianna.Rest;
using Suzianna.Rest.Screenplay.Abilities;

namespace Suzianna.Http.Tests.Unit.TestUtils
{
    internal static class ActorFactory
    {
        public static Actor CreateSomeActorWithApiCallAbility(IHttpRequestSender sender)
        {
            return Actor.Named(Names.Juliet).WhoCan(CallAnApi.At(Urls.Google).With(sender));
        }
    }
}
