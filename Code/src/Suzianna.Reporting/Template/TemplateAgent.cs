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
        public static string Render(Report report)
        {
            var ctx = CreateTemplateContext();
            ctx.DefineLocalVariable("model", report.ToLiquid());
            var template = ReadTemplate();
            return Render(template, ctx);
        }
        private static TemplateContext CreateTemplateContext()
        {
            var ctx = new TemplateContext();
            ctx.WithFilter<SuziannaDateFilter>("suziannaDate");
            return ctx;
        }
        private static string ReadTemplate()
        {
            var stream = typeof(TemplateAgent).Assembly.GetManifestResourceStream("Suzianna.Reporting.Template.report.lqd");
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            return text;
        }
        private static string Render(string template, TemplateContext ctx)
        {
            var parsingResult = LiquidTemplate.Create(template);
            return parsingResult.LiquidTemplate.Render(ctx).Result;
        }
    }
}
