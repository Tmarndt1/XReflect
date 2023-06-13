namespace XReflect
{
    internal class Accessor<T, TProperty> : IAccessor<T, TProperty>, IAccessorNext<T, TProperty>
        where T : class
        where TProperty : class
    {
        private readonly IBuilder<T> _builder;

        internal Accessor(IBuilder<T> builder)
        {
            _builder = builder;
        }

        IBuilder<T> IAccessor<T, TProperty>.Builder => _builder;

        IBuilder<T> IAccessorNext<T, TProperty>.Builder => _builder;
    }
}
