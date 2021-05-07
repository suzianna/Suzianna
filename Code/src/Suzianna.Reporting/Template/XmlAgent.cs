using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Suzianna.Reporting.XmlNodes;

namespace Suzianna.Reporting.Template
{
    internal static class XmlAgent
    {
        public static string ToXml(Report report)
        {
            var serializer = new XmlSerializer(report.GetType());
            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, report);
                    return sww.ToString();
                }
            }
        }
    }
}
