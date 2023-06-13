namespace XReflect
{
    public interface IAccessor<T, out TProperty>
        where T : class
    {
        internal IBuilder<T> Builder { get; }
    }

    public interface IAccessorNext<T, out TProperty>
        where T : class
    {
        internal IBuilder<T> Builder { get; }
    }
}
