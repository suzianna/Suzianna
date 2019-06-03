using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Liquid.NET;
using Liquid.NET.Utils;
using Suzianna.Reporting.Model;

namespace Suzianna.Reporting.Template
{
    internal static class TemplateAgent
    {
        //TODO:refactoring case
        public static string Render(Report report)
        {
            var ctx = new TemplateContext();
            ctx.WithFilter<SuziannaDateFilter>("suziannaDate");
            ctx.DefineLocalVariable("model", report.ToLiquid());

            var stream = typeof(TemplateAgent).Assembly.GetManifestResourceStream("Suzianna.Reporting.Template.report.lqd");
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();

            var parsingResult = LiquidTemplate.Create(text);
            var result = parsingResult.LiquidTemplate.Render(ctx).Result;
            return result;
        }
    }
}
