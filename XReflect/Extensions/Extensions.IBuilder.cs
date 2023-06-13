using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XReflect
{
    public static partial class Extensions
    {
        public static IAccessor<T, TProperty> Access<T, TProperty>(this IBuilder<T> builder, Expression<Func<T, TProperty>> expression)
            where T : class
            where TProperty : class
        {
            builder.Aggregator.Enqueue(expression);

            return new Accessor<T, TProperty>(builder);
        }

        public static IAccessor<T, TProperty> Access<T, TProperty>(this IBuilder<T> builder, Expression<Func<T, IList<TProperty>>> expression)
            where T : class
            where TProperty : class
        {
            builder.Aggregator.Enqueue(expression);

            return new Accessor<T, TProperty>(builder);
        }

        public static IAccessorNext<T, TProperty> Map<T, TProperty>(this IBuilder<T> builder, Expression<Func<T, TProperty>> expression)
            where T : class
            where TProperty : class
        {
            builder.Aggregator.Enqueue(expression);

            return new Accessor<T, TProperty>(builder);
        }
    }
}
