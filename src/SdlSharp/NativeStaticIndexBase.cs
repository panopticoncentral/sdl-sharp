using System;
using System.Collections.Generic;

namespace SdlSharp
{
    /// <summary>
    /// The base of an object that represents an indexed SDL object.
    /// </summary>
    /// <typeparam name="TIndex">The index type.</typeparam>
    /// <typeparam name="TManaged">The managed type.</typeparam>
    public unsafe abstract class NativeStaticIndexBase<TIndex, TManaged>
        where TIndex : struct
        where TManaged : NativeStaticIndexBase<TIndex, TManaged>, new()
    {
        private static readonly Dictionary<TIndex, WeakReference<TManaged>> s_instances = new Dictionary<TIndex, WeakReference<TManaged>>();

        /// <summary>
        /// The index.
        /// </summary>
        public TIndex Index { get; private set; }

        public static TManaged IndexToInstance(TIndex index)
        {
            if (s_instances.TryGetValue(index, out var weakRef)
                && weakRef.TryGetTarget(out var value))
            {
                return value;
            }

            value = new TManaged() { Index = index };
            s_instances[index] = new WeakReference<TManaged>(value);
            return value;
        }
    }
}
