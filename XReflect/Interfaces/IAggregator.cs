using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XReflect
{
    internal interface IAggregator<T>
        where T : class
    {
        Chain Active { get; }

        List<Chain> Chains { get; }

        XReflectConfiguration Configuration { get; }

        void Enqueue(LambdaExpression expression);

        void CloseWith<TProperty>(Func<TProperty, TProperty, bool> func)
            where TProperty : class;

        T? Reflect(T source, T target);

        bool IsMatch(object entity1, object entity2);
    }
}
