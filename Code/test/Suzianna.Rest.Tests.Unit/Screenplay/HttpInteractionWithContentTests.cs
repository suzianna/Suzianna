using System.Linq;
using NFluent;
using Org.XmlUnit.Builder;
using Org.XmlUnit.Xpath;
using Suzianna.Core.Screenplay;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Screenplay
{
    public abstract class HttpInteractionWithContentTests : HttpInteractionTests
    {
        private class Package
        {
            public string Title { get; set; }
            public string Version { get; set; }
        }

        [Fact]
        public void should_send_request_content_headers()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            actor.AttemptsTo(GetHttpInteraction(Urls.UsersApi)
                .WithHeader(HttpHeaders.ContentEncoding, ContentEncodings.GZip));

            Check.That(Sender.GetLastSentMessage().Content.Headers.ContentEncoding.First()).IsEqualTo(ContentEncodings.GZip);
        }

        [Fact]
        public void should_send_request_as_json_with_correct_serialization()
        {
            var content = new Package { Title = "Suzianna", Version = "1.0"};
            var expectedJson = "{\"Title\":\"Suzianna\",\"Version\":\"1.0\"}";
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            actor.AttemptsTo(GetHttpInteractionWithJsonContent(content));

            Check.That(Sender.GetLastSentMessage().Content.ReadAsStringAsync().Result).IsEqualTo(expectedJson);
        }

        [Fact]
        public void should_send_request_as_json_with_content_type_header()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            actor.AttemptsTo(GetHttpInteractionWithJsonContent(ContentFactory.SomeContent()));

            Check.That(Sender.GetLastSentMessage().Content.Headers.ContentType.MediaType).IsEqualTo(MediaTypes.ApplicationJson);
        }

        [Fact]
        public void should_send_request_as_xml_with_correct_serialization()
        {
            var content = new Package { Title = "Suzianna", Version = "1.0" };
            var expectedXml = "<Package><Title>Suzianna</Title><Version>1.0</Version></Package>";
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            actor.AttemptsTo(GetHttpInteractionWithXmlContent(content));

            var xml = Sender.GetLastSentMessage().Content.ReadAsStringAsync().Result;
            var source = Input.FromString(xml).Build();
            Check.That(new XPathEngine().Evaluate("//Package/Title", source)).IsEqualTo("Suzianna");
            Check.That(new XPathEngine().Evaluate("//Package/Version", source)).IsEqualTo("1.0");
        }

        [Fact]
        public void should_send_request_as_xml_with_content_type_header()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            actor.AttemptsTo(GetHttpInteractionWithXmlContent(ContentFactory.SomeContent()));

            Check.That(Sender.GetLastSentMessage().Content.Headers.ContentType.MediaType).IsEqualTo(MediaTypes.ApplicationXml);
        }

        protected abstract HttpInteraction GetHttpInteractionWithJsonContent(object content);
        protected abstract HttpInteraction GetHttpInteractionWithXmlContent(object content);
    }
}