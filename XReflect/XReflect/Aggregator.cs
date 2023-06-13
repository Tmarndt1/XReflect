using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace XReflect
{
    internal class Aggregator<T> : IAggregator<T>
        where T : class
    {
        public Chain Active { get; private set; } = new Chain();

        private List<Chain>? _chains;

        public List<Chain> Chains
        {
            get
            {
                if (_chains == null) _chains = new List<Chain>();

                return _chains;
            }
        }

        private Dictionary<Type, IComparator>? _comparators;

        public Dictionary<Type, IComparator> Comparators
        {
            get
            {
                if (_comparators == null) _comparators = new Dictionary<Type, IComparator>();

                return _comparators;
            }
        }

        public XReflectConfiguration Configuration { get; internal set; } = new XReflectConfiguration();

        public void Enqueue(LambdaExpression expression)
        {
            Active.Queue.Enqueue(expression);
        }

        public void CloseWith<TProperty>(Func<TProperty, TProperty, bool> func)
            where TProperty : class
        {
            if (func == null) throw new InvalidOperationException("Expression cannot be null");

            Comparators.Add(typeof(TProperty), new Comparator<TProperty>(func));

            Chains.Add(Active);

            Active = new Chain();
        }

        public bool IsMatch(object entity1, object entity2)
        {
            if (entity1 == null || entity2 == null) return false;

            if (entity1 == entity2) return true;

            Comparators.TryGetValue(entity1.GetType(), out IComparator? comparator);

            if (comparator == null) return false;

            MethodInfo? methodInfo = comparator.GetMethodInfo();

            if (methodInfo == null) return false;

            object? result = methodInfo.Invoke(comparator, new object[] { entity1, entity2 });

            if (result == null) return false;

            return (bool)result;
        }

        public T? Reflect(T source, T target)
        {
            foreach (var chain in Chains.OrderBy(x => x.Queue.Count))
            {
                source.Map(target, chain, this);
            }

            return source.MapProps(target);
        }
    }
}
