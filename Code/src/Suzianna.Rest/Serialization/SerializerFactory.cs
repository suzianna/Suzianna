using System;

namespace Suzianna.Rest.Serialization
{
    internal static class SerializerFactory
    {
        public static ISerializer Create(ContentType contentType)
        {
            if (contentType == ContentType.Json) return new JsonSerializer();
            if (contentType == ContentType.PlainText) return new PlainTextSerializer();
            throw new NotSupportedException();
        }
    }
}