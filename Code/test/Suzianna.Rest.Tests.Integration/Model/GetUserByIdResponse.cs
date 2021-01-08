namespace Suzianna.Rest.Tests.Integration.Model
{
    public class GetUserByIdResponse
    {
        public UserInformation Data { get; set; }

        public GetUserByIdResponse() { }

        public GetUserByIdResponse(int id, string name)
        {
            this.Data = new UserInformation()
            {
                Id = id,
                Name = name
            };
        }
    }
    public class UserInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}