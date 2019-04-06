namespace Suzianna.Rest.Serialization
{
    internal class PlainTextSerializer : ISerializer
    {
        public string Serialize(object objectToSerialize)
        {
            return objectToSerialize.ToString();
        }
    }
}