using System.IO;
using System.Text;
using System.Xml.Serialization;
using Suzianna.Reporting.XmlNodes;

namespace Suzianna.Reporting
{
    public static class ReportParser
    {
        public static Report Parse(string xml)
        {
            var serializer = new XmlSerializer(typeof(Report));
            using (TextReader reader = new StringReader(xml))
            {
                return (Report) serializer.Deserialize(reader);
            }
        }
    }
}