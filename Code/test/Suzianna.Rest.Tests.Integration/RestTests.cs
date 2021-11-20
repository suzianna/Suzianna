using NFluent;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Abilities;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;
using Suzianna.Rest.Tests.Integration.Model;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Suzianna.Rest.Tests.Integration
{
    public class RestTests
    {
        private readonly IHttpRequestSender _requestSender;

        public RestTests(IHttpRequestSender requestSender)
        {
            _requestSender = requestSender;
        }

        [Fact]
        public async Task Define_a_new_user()
        {
            var jack = Actor.Named("Jack").WhoCan(CallAnApi.At("https://reqres.in", _requestSender));
            var user = new User { Name = "Sarah", Job = "Software Architect"};

            await jack.AttemptsTo(CreateAn(user));

            jack.Should(See.That(LastResponse.Content<CreateUserResponse>()))
                .Considering().All.Properties.HasFieldsWithSameValues(user);
            jack.Should(See.That(LastResponse.StatusCode()))
                .IsEqualTo(HttpStatusCode.Created);
        }

        public static Post CreateAn(User user)
        {
            return Post.DataAsJson(user).To("api/Users");
        }

        [Fact]
        public async Task Fetch_user_by_id()
        {
            var jack = Actor.Named("Jack").WhoCan(CallAnApi.At("https://reqres.in", _requestSender));
            var expectedResponse = new GetUserByIdResponse(id: 2, name: "fuchsia rose");

            await jack.AttemptsTo(FetchUserById(2));

            jack.Should(See.That(LastResponse.Content<GetUserByIdResponse>()))
                .Considering().All.Properties.HasFieldsWithSameValues(expectedResponse);
            jack.Should(See.That(LastResponse.StatusCode())).IsEqualTo(HttpStatusCode.OK);
        }

        private static Get FetchUserById(int id)
        {
            return Get.ResourceAt($"api/Users/{id}");
        }
        [Fact]
        public async Task Modify_entire_user()
        {
            var jack = Actor.Named("Jack").WhoCan(CallAnApi.At("https://reqres.in", _requestSender));
            var user = new User { Name = "Sarah", Job = "Software Architect" };

            await jack.AttemptsTo(ModifyEntire(user));

            jack.Should(See.That(LastResponse.Content<ModifyUserResponse>()))
                .Considering().All.Properties.HasFieldsWithSameValues(user);
            jack.Should(See.That(LastResponse.StatusCode())).IsEqualTo(HttpStatusCode.OK);
        }

        private static Put ModifyEntire(User user)
        {
            return Put.DataAsJson(user).To("api/Users/2");
        }

        [Fact]
        public async Task Modify_part_of_an_user()
        {
            var jack = Actor.Named("Jack").WhoCan(CallAnApi.At("https://reqres.in", _requestSender));
            var user = new User { Name = "Sarah", Job = "Software Architect" };

            await jack.AttemptsTo(ModifyPartOf(user));

            jack.Should(See.That(LastResponse.Content<ModifyUserResponse>()))
                .Considering().All.Properties.HasFieldsWithSameValues(user);
            jack.Should(See.That(LastResponse.StatusCode())).IsEqualTo(HttpStatusCode.OK);
        }

        private static Patch ModifyPartOf(User user)
        {
            return Patch.DataAsJson(user).To("api/users/2");
        }
        [Fact]
        public async Task Delete_an_user()
        {
            var jack = Actor.Named("Jack").WhoCan(CallAnApi.At("https://reqres.in", _requestSender));
            var user = new User { Name = "Sarah", Job = "Software Architect" };

            await jack.AttemptsTo(DeleteAn(user));

            jack.Should(See.That(LastResponse.Content<object>())).IsNull();
            jack.Should(See.That(LastResponse.StatusCode())).IsEqualTo(HttpStatusCode.NoContent);
        }

        private static Delete DeleteAn(User user)
        {
            return Delete.From("api/users/2");
        }
    }
}
