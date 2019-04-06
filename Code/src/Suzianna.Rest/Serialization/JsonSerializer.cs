using Newtonsoft.Json;

namespace Suzianna.Rest.Serialization
{
    internal class JsonSerializer : ISerializer
    {
        public string Serialize(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }
    }
}