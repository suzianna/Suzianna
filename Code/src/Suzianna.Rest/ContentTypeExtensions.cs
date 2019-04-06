using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Suzianna.Rest
{
    internal static class ContentTypeExtensions
    {
        private static Dictionary<ContentType, string> _mediaTypes = new Dictionary<ContentType, string>();
        static ContentTypeExtensions()
        {
            var contentTypes = Enum.GetValues(typeof(ContentType));
            foreach (var contentType in contentTypes)
            {
                var description = contentType.GetType()
                    .GetMember(contentType.ToString())
                    .First()
                    .GetCustomAttribute<DescriptionAttribute>().Description;
                _mediaTypes.Add((ContentType)contentType, description);
            }
        }
        public static string ToMediaType(this ContentType contentType)
        {
            return _mediaTypes[contentType];
        }
    }
}
