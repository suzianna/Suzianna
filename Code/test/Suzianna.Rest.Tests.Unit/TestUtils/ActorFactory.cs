using Suzianna.Core.Screenplay;
using Suzianna.Rest.Screenplay.Abilities;
using Suzianna.Rest.Tests.Unit.TestConstants;

namespace Suzianna.Rest.Tests.Unit.TestUtils
{
    internal static class ActorFactory
    {
        public static Actor CreateSomeActorWithApiCallAbility(IHttpRequestSender sender)
        {
            return Actor.Named(Names.Juliet).WhoCan(CallAnApi.At(Urls.Google).With(sender));
        }
    }
}
