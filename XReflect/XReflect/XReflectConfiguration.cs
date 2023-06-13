namespace XReflect
{
    public enum CollectionOption
    {
        /// <summary>
        /// Configures the reflection algorithm to only add the new objects to the existing collection
        /// </summary>
        Add,

        /// <summary>
        /// Configures the reflection algorithm to only remove non matching objects in the existing collection
        /// </summary>
        Remove,

        /// <summary>
        /// Configures the reflection algorithm to add new objects and remove non matching objects in the existing collection
        /// </summary>
        AddRemove
    }

    public class XReflectConfiguration
    {
        /// <summary>
        /// The settings to be applied when reflecting on collections
        /// </summary>
        public CollectionOption CollectionOption { get; set; } = CollectionOption.AddRemove;

        /// <summary>
        /// Configures the algorithm to ignore null values within a collection
        /// </summary>
        public bool IgnoreNull { get; set; } = true;
    }
}
