using System;

namespace SmartPlug.Common
{
    public interface IWeakEvent<T>
        where T : EventArgs
    {
        void Subscribe(InvocationDelegate<T> d);
        void Unsubscribe(InvocationDelegate<T> d);
    }
}