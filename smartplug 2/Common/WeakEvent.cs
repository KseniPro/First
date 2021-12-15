using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmartPlug.Common
{
    public class WeakEvent<T> : IWeakEvent<T>
        where T : EventArgs
    {
        private readonly HashSet<InvocationTarget> _invocationList = new();

        public void Subscribe(InvocationDelegate<T> d)
        {
            _invocationList.Add(new InvocationTarget(d.Target, d.Method));
        }

        public void Unsubscribe(InvocationDelegate<T> d)
        {
            _invocationList.RemoveWhere(x => Equals(x.Object.Target, d.Target) && Equals(x.Method, d.Method));
        }

        public void Raise(object sender, T args)
        {
            foreach (var item in _invocationList.ToList())
            {
                var ob = item.Object.Target;
                if (ob != null)
                {
                    item.Method.Invoke(ob, new object[] { sender, args });
                }
                else
                {
                    _invocationList.Remove(item);
                }
            }
        }

        class InvocationTarget
        {
            public MethodInfo Method { get; }
            public WeakReference Object { get; }

            public InvocationTarget(object ob, MethodInfo method)
            {
                Method = method;
                Object = new WeakReference(ob, false);
            }

            public override int GetHashCode()
            {
                return HashCodeUtils.CombineHashCode(Method.GetHashCode(), Object.GetHashCode());
            }
        }
    }

    public delegate void InvocationDelegate<in T>(object sender, T args);
}