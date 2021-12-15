using System;

namespace SmartPlug.Common
{
    public class EventArgs<T> : EventArgs
    {
        public T Value { get; }

        public EventArgs(T val)
        {
            Value = val;
        }
    }
}