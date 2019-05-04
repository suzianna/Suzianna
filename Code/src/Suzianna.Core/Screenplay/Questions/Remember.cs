using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Core.Screenplay.Questions
{
    public static class Remember
    {
        public static IQuestion<T> ValueOf<T>(string key)
        {
            return new Remember<T>(key);
        }
    }
}
