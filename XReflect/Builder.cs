using System;

namespace XReflect
{
    internal class Builder<T> : IBuilder<T>
        where T : class
    {
        private readonly Aggregator<T> _aggregator;

        public Builder()
        {
            _aggregator = new Aggregator<T>();
        }

        IAggregator<T> IBuilder<T>.Aggregator => _aggregator;

        public IBuilderConfiguration<T> Configure(XReflectConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _aggregator.Configuration = configuration;

            return this;
        }

        public T? Run(T source, T target)
        {
            return _aggregator.Reflect(source, target);
        }

        T? IBuilderFinal<T>.Run(T source, T target)
        {
            return Run(source, target);
        }

        IBuilderConfiguration<T> IBuilder<T>.Configure(XReflectConfiguration configuration)
        {
            return Configure(configuration);
        }
    }
}
