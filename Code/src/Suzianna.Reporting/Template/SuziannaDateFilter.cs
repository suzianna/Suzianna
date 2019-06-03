using Liquid.NET;
using Liquid.NET.Constants;
using Liquid.NET.Filters;
using Liquid.NET.Utils;

namespace Suzianna.Reporting.Template
{
    public class SuziannaDateFilter : FilterExpression<LiquidDate, LiquidString>
    {
        public override LiquidExpressionResult ApplyTo(ITemplateContext ctx, LiquidDate val)
        {
            return LiquidExpressionResult.Success(val.DateTimeValue.Value.ToString(ReportConstants.DateFormat));
        }

        public override LiquidExpressionResult ApplyToNil(ITemplateContext ctx)
        {
            return LiquidExpressionResult.Success(ReportConstants.Unknown);
        }
    }

}
