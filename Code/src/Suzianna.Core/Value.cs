using System;

namespace Suzianna.Core
{
    public class Value<T>
    {
        public T UnderlayValue { get; private set; }
        public Value(T value)
        {
            UnderlayValue = value;
        }
        public string AsString() => UnderlayValue.ToString();
        public bool AsBoolean() => Convert.ToBoolean(UnderlayValue);
        public DateTime AsDateTime() => Convert.ToDateTime(UnderlayValue);
        public long AsLong() => Convert.ToInt64(UnderlayValue);
        public int AsInt() => Convert.ToInt32(UnderlayValue);
        public short AsShort() => Convert.ToInt16(UnderlayValue);
        public char AsCharacter() => Convert.ToChar(UnderlayValue);
        public TimeSpan AsTimespan() => TimeSpan.Parse(AsString());

        public static implicit operator Value<T>(T value)
        {
            if (value?.GetType() == typeof(Value<T>))
                return (Value<T>)(object)value;

            return new Value<T>(value);
        }
        public static implicit operator string(Value<T> value) => value.AsString();
    }
}
