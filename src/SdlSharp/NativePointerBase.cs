using System;
using System.Collections.Generic;

// In this case, it's really the whole point, and this will not have a lot of instantiations
#pragma warning disable CA1000 // Do not declare members on generic types

namespace SdlSharp
{
    /// <summary>
    /// The base class of an object that represents a pointer SDL object.
    /// </summary>
    /// <typeparam name="TNative">The native pointer type.</typeparam>
    /// <typeparam name="TManaged">The managed type.</typeparam>
    public abstract unsafe class NativePointerBase<TNative, TManaged> : IDisposable
        where TNative : unmanaged
        where TManaged : NativePointerBase<TNative, TManaged>, new()
    {
        private static readonly Dictionary<nint, WeakReference<TManaged>> s_instances = new();

        /// <summary>
        /// The pointer.
        /// </summary>
        public TNative* Native { get; private set; }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            _ = s_instances.Remove((nint)Native);
            Native = null;
        }

        /// <summary>
        /// Converts a native object to a managed object.
        /// </summary>
        /// <param name="native">The pointer.</param>
        /// <returns>A managed object.</returns>
        public static TManaged? PointerToInstance(TNative* native)
        {
            if (native == null)
            {
                return null;
            }

            if (s_instances.TryGetValue((nint)native, out var weakRef)
                && weakRef.TryGetTarget(out var value))
            {
                return value;
            }

            value = new TManaged
            {
                Native = SdlSharp.Native.CheckPointer(native)
            };
            s_instances[(nint)native] = new WeakReference<TManaged>(value);
            return value;
        }

        /// <summary>
        /// Converts a native object to a managed object.
        /// </summary>
        /// <param name="native">The pointer.</param>
        /// <returns>A managed object.</returns>
        public static TManaged PointerToInstanceNotNull(TNative* native) =>
            PointerToInstance(native) ?? throw new SdlException();
    }
}
