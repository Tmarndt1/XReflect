using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace XReflect
{
    public static partial class Extensions
    {
        internal static TEntity Map<TEntity>(this TEntity source, TEntity target, Chain chain, IAggregator<TEntity> aggregator)
            where TEntity : class
        {
            string? bodyName = chain.ElementAt(0).GetBody();

            if (bodyName == null) throw new InvalidOperationException("Invalid expression");

            RecursiveReflect(source, target, bodyName, aggregator, chain, 1);

            return target;
        }

        internal static void RecursiveReflect<TEntity>(object source, object target, string propertyName, IAggregator<TEntity> aggregator, Chain chain, int count)
            where TEntity : class
        {
            PropertyInfo propertyInfo = source.GetType().GetProperty(propertyName);

            object sourceValue = propertyInfo.GetValue(source);

            object targetValue = propertyInfo.GetValue(target);

            if (count == chain.Count)
            {
                if (typeof(ICollection).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    if (sourceValue == null) return;

                    IList sourceList = (IList)sourceValue;
                    IList targetList = (IList)targetValue;

                    if (aggregator.Configuration.CollectionOption != CollectionOption.Add)
                    {
                        RemoveEntities(sourceList, targetList, aggregator);
                    }

                    if (aggregator.Configuration.CollectionOption != CollectionOption.Remove)
                    {
                        AddEntities(sourceList, targetList, aggregator);
                    }
                }
                else if (propertyInfo.PropertyType.IsClass)
                {
                    if (targetValue == null || sourceValue == null)
                    {
                        propertyInfo.SetValue(target, sourceValue);
                    }
                    else
                    {
                        sourceValue.MapProps(targetValue);
                    }
                }
            }
            else
            {
                if (typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    if (sourceValue == null) return;

                    IList sourceList = (IList)sourceValue;
                    IList targetList = (IList)targetValue;

                    for (int i = 0; i < sourceList.Count; i++)
                    {
                        object? match = default;

                        for (int j = 0; j < targetList.Count; j++)
                        {
                            if (aggregator.IsMatch(sourceList[i], targetList[j]))
                            {
                                match = targetList[j];
                                break;
                            }
                        }

                        if (match == null) continue;

                        string? propName = chain.ElementAt(count).GetBody();

                        if (propName == null) throw new InvalidOperationException("Invalid expression");

                        RecursiveReflect(sourceList[i], match, propName, aggregator, chain, ++count);
                    }
                }
                else if (propertyInfo.PropertyType.IsClass)
                {
                    string? propName = chain.ElementAt(count).GetBody();

                    if (propName == null) throw new InvalidOperationException("Invalid expression");

                    RecursiveReflect(sourceValue, targetValue, propName, aggregator, chain, count + 1);
                }
            }
        }

        internal static TEntity? MapProps<TEntity>(this TEntity source, TEntity target) 
            where TEntity : class
        {
            if (source == null || target == null) return default;

            PropertyInfo[] propertyInfos = source.GetType().GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo == null ) continue;

                if (!propertyInfo.CanWrite) continue;

                if (!propertyInfo.GetSetMethod().IsPublic) continue;

                if (propertyInfo.PropertyType.IsClass)
                {
                    if (propertyInfo.PropertyType != typeof(string)) continue;
                }

                if (propertyInfo.GetCustomAttributes<NotMappedAttribute>(true).Any()) continue;

                propertyInfo.SetValue(target, propertyInfo.GetValue(source));
            }

            return target;
        }

        private static void RemoveEntities<TEntity>(IList sourceList, IList targetList, IAggregator<TEntity> aggregator)
            where TEntity : class
        {
            for (int i = targetList.Count - 1; i > -1; i--)
            {
                object? match = default;

                for (int j = 0; j < sourceList.Count; j++)
                {
                    if (aggregator.IsMatch(sourceList[j], targetList[i]))
                    {
                        match = sourceList[j];
                        break;
                    }
                }

                if (match != null) continue;

                if (i == default && aggregator.Configuration.IgnoreNull) continue;

                targetList.Remove(i);
            }
        }

        private static void AddEntities<TEntity>(IList sourceList, IList targetList, IAggregator<TEntity> aggregator)
            where TEntity : class
        {
            for (int i = 0; i < sourceList.Count; i++)
            {
                object? match = default;

                for (int j = 0; j < targetList.Count; j++)
                {
                    if (aggregator.IsMatch(sourceList[i], targetList[j]))
                    {
                        match = targetList[j];
                        break;
                    }
                }

                if (match != null)
                {
                    sourceList[i].MapProps(match);
                }
                else 
                {
                    if (sourceList[i] == default && aggregator.Configuration.IgnoreNull) continue;

                    targetList.Add(sourceList[i]);
                }
            }
        }
    }
}
