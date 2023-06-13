using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XReflect
{
    public static partial class Extensions
    {
        public static IAccessorNext<T, TProperty> Map<T, TPreviousProperty, TProperty>(this IAccessor<T, TPreviousProperty> accessor, Expression<Func<TPreviousProperty, TProperty>> expression)
            where T : class, IXReflectEntity
            where TPreviousProperty : class
            where TProperty : class
        {
            accessor.Builder.Aggregator.Enqueue(expression);

            return new Accessor<T, TProperty>(accessor.Builder);
        }

        public static IAccessorNext<T, TProperty> Map<T, TPreviousProperty, TProperty>(this IAccessor<T, IList<TPreviousProperty>> accessor, Expression<Func<TPreviousProperty, TProperty>> expression)
            where T : class, IXReflectEntity
            where TPreviousProperty : class
            where TProperty : class
        {
            accessor.Builder.Aggregator.Enqueue(expression);

            return new Accessor<T, TProperty>(accessor.Builder);
        }

        public static IBuilder<T> When<T, TProperty>(this IAccessorNext<T, TProperty> accessor, Func<TProperty, TProperty, bool> expression)
            where T : class, IXReflectEntity
            where TProperty : class
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            accessor.Builder.Aggregator.CloseWith<TProperty>(expression);

            return accessor.Builder;
        }

        public static IBuilder<T> When<T, TProperty>(this IAccessorNext<T, IList<TProperty>> accessor, Func<TProperty, TProperty, bool> expression)
            where T : class, IXReflectEntity
            where TProperty : class
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            accessor.Builder.Aggregator.CloseWith<TProperty>(expression);

            return accessor.Builder;
        }
    }
}
