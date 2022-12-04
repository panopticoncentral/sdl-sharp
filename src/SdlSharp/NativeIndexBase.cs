// In this case, it's really the whole point, and this will not have a lot of instantiations
#pragma warning disable CA1000 // Do not declare members on generic types

namespace SdlSharp
{
    /// <summary>
    /// The base of an object that represents an indexed SDL object.
    /// </summary>
    /// <typeparam name="TNative">The native pointer type.</typeparam>
    /// <typeparam name="TIndex">The index type.</typeparam>
    /// <typeparam name="TManaged">The managed type.</typeparam>
    public abstract unsafe class NativeIndexBase<TNative, TIndex, TManaged> : IDisposable
        where TNative : unmanaged
        where TIndex : struct
        where TManaged : NativeIndexBase<TNative, TIndex, TManaged>, new()
    {
        private static readonly Dictionary<(nint, TIndex), WeakReference<TManaged>> s_instances = new();

        /// <summary>
        /// The pointer.
        /// </summary>
        public TNative* Native { get; private set; }

        /// <summary>
        /// The index.
        /// </summary>
        public TIndex Index { get; private set; }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            _ = s_instances.Remove(((nint)Native, Index));
            Index = default;
            Native = null;
        }

        /// <summary>
        /// Converts an index and a native object to a managed object.
        /// </summary>
        /// <param name="native">The pointer.</param>
        /// <param name="index">The index.</param>
        /// <returns>A managed object.</returns>
        public static TManaged IndexToInstance(TNative* native, TIndex index)
        {
            if (s_instances.TryGetValue(((nint)native, index), out var weakRef)
                && weakRef.TryGetTarget(out var value))
            {
                return value;
            }

            value = new TManaged() { Native = native, Index = index };
            s_instances[((nint)native, index)] = new WeakReference<TManaged>(value);
            return value;
        }
    }
}
