using System.ComponentModel;

namespace Suzianna.Rest
{
    internal enum ContentType
    {
        [Description(MediaTypes.ApplicationJson)] Json = 0,
        [Description(MediaTypes.PlainText)] PlainText =2
    }
}
