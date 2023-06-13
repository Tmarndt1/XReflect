namespace XReflect
{
    public interface IBuilder<T> : IBuilderConfiguration<T>
        where T : class
    {
        internal IAggregator<T> Aggregator { get; }

        IBuilderConfiguration<T> Configure(XReflectConfiguration configuration);
    }

    public interface IBuilderConfiguration<T> : IBuilderFinal<T>
    where T : class
    {

    }

    public interface IBuilderFinal<T>
        where T : class
    {
        T? Run(T source, T target);
    }
}
