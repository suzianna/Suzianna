using System.Collections.Generic;

namespace Suzianna.Core
{
    internal class Notepad
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public void WriteDown(string key, object value)
        {
            _values.Add(key, value);
        }

        public T Read<T>(string key)
        {
            return (T) _values[key];
        }

        public bool HasWroteDown(string key)
        {
            return _values.ContainsKey(key);
        }
    }
}