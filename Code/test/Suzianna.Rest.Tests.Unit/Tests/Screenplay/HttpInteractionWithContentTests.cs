using System.Linq;
using System.Threading.Tasks;
using NFluent;
using Org.XmlUnit.Builder;
using Org.XmlUnit.Xpath;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Tests.Screenplay
{
    public abstract class HttpInteractionWithContentTests : HttpInteractionTests
    {
        private class Package
        {
            public string Title { get; set; }
            public string Version { get; set; }
        }

        [Fact]
        public async Task should_send_request_content_headers()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            await actor.AttemptsTo(GetHttpInteraction(Urls.UsersApi)
                .WithHeader(HttpHeaders.ContentEncoding, ContentEncodings.GZip));

            Check.That(Sender.GetLastSentMessage().Content.Headers.ContentEncoding.First()).IsEqualTo(ContentEncodings.GZip);
        }


        [Fact]
        public async Task should_send_request_as_json_with_correct_serialization()
        {
            var content = new Package { Title = "Suzianna", Version = "1.0"};
            var expectedJson = "{\"Title\":\"Suzianna\",\"Version\":\"1.0\"}";
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            await actor.AttemptsTo(GetHttpInteractionWithJsonContent(content));

            Check.That(await Sender.GetLastSentMessage().Content.ReadAsStringAsync()).IsEqualTo(expectedJson);
        }

        [Fact]
        public async Task should_send_request_as_json_with_content_type_header()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            await actor.AttemptsTo(GetHttpInteractionWithJsonContent(ContentFactory.SomeContent()));

            Check.That(Sender.GetLastSentMessage().Content.Headers.ContentType.MediaType).IsEqualTo(MediaTypes.ApplicationJson);
        }

        [Fact]
        public async Task should_send_request_as_xml_with_correct_serialization()
        {
            var content = new Package { Title = "Suzianna", Version = "1.0" };
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            await actor.AttemptsTo(GetHttpInteractionWithXmlContent(content));

            var xml = await Sender.GetLastSentMessage().Content.ReadAsStringAsync();
            var source = Input.FromString(xml).Build();
            Check.That(new XPathEngine().Evaluate("//Package/Title", source)).IsEqualTo("Suzianna");
            Check.That(new XPathEngine().Evaluate("//Package/Version", source)).IsEqualTo("1.0");
        }

        [Fact]
        public async Task should_send_request_as_xml_with_content_type_header()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            await actor.AttemptsTo(GetHttpInteractionWithXmlContent(ContentFactory.SomeContent()));

            Check.That(Sender.GetLastSentMessage().Content.Headers.ContentType.MediaType).IsEqualTo(MediaTypes.ApplicationXml);
        }

        [Fact]
        public async Task should_send_request_with_custom_content()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);
            var content = ContentFactory.AccessTokenRequest();

            await actor.AttemptsTo(GetHttpInteractionWithCustomContent(content));

            Check.That(await Sender.GetLastSentMessage().Content.ReadAsStringAsync()).IsEqualTo(content);

        }

        protected abstract HttpInteraction GetHttpInteractionWithJsonContent(object content);
        protected abstract HttpInteraction GetHttpInteractionWithXmlContent(object content);
        protected abstract HttpInteraction GetHttpInteractionWithCustomContent(string content);
    }
}