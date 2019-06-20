using System.IO;
using YAXLib;

namespace Suzianna.Rest.Serialization
{
    internal class XmlSerializer :ISerializer
    {
        public string Serialize(object objectToSerialize)
        {
            YAXSerializer serializer = new YAXSerializer(objectToSerialize.GetType());
            return serializer.Serialize(objectToSerialize);
        }
    }
}