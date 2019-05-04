using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Core
{
    internal class Notepad
    {
        private Dictionary<string, object> _values = new Dictionary<string, object>();

        public void WriteDown(string key, object value)
        {
            _values.Add(key,value);
        }

        public T Read<T>(string key)
        {
            return (T)_values[key];
        }
    }
}
