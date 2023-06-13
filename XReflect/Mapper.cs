using System;

namespace XReflect
{
    public class Mapper<T> 
        where T : class
    {
        private readonly Builder<T> _builder = new Builder<T>();

        public T? Run(T source, T target)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (target == null) throw new ArgumentNullException(nameof(target));

            return _builder.Run(source, target);
        }

        public Mapper(Action<IBuilder<T>> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            action.Invoke(_builder);
        }
    }
}
