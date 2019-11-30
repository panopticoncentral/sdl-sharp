using System;
using System.Collections.Generic;

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
        private static readonly Dictionary<(IntPtr, TIndex), WeakReference<TManaged>> s_instances = new Dictionary<(IntPtr, TIndex), WeakReference<TManaged>>();

        /// <summary>
        /// The pointer.
        /// </summary>
        public TNative* Pointer { get; private set; }

        /// <summary>
        /// The index.
        /// </summary>
        public TIndex Index { get; private set; }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            _ = s_instances.Remove(((IntPtr)Pointer, Index));
            Index = default;
            Pointer = null;
        }

        public static TManaged IndexToInstance(TNative* pointer, TIndex index)
        {
            if (s_instances.TryGetValue(((IntPtr)pointer, index), out var weakRef)
                && weakRef.TryGetTarget(out var value))
            {
                return value;
            }

            value = new TManaged() { Pointer = pointer, Index = index };
            s_instances[((IntPtr)pointer, index)] = new WeakReference<TManaged>(value);
            return value;
        }
    }
}
