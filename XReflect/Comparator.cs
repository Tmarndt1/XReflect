using System;
using System.Reflection;

namespace XReflect
{
    internal interface IComparator
    {
        MethodInfo? GetMethodInfo();
    }

    internal class Comparator<T> : IComparator
        where T : class
    {
        private readonly Func<T, T, bool> _func;

        private readonly MethodInfo? _methodInfo;

        public Comparator(Func<T, T, bool> func)
        {
            _func = func;

            _methodInfo = typeof(Comparator<T>).GetMethod(nameof(Comparator<T>.Compare));
        }

        public bool Compare(T entity1, T entity2)
        {
            return _func?.Invoke(entity1, entity2) ?? false;
        }

        public MethodInfo? GetMethodInfo()
        {
            return _methodInfo;
        }
    }
}
