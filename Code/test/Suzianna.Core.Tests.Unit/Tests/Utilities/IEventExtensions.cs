using System;
using System.Text;
using Suzianna.Core.Events;

namespace Suzianna.Core.Tests.Unit.Tests.Utilities
{
    internal static class EventExtensions
    {
        public static T As<T>(this IEvent @event) where T : class
        {
            return @event as T;
        }
    }
}
