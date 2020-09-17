using System;
using System.Collections.Generic;

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
        private static readonly Dictionary<nint, WeakReference<TManaged>> s_instances = new Dictionary<nint, WeakReference<TManaged>>();

        /// <summary>
        /// The pointer.
        /// </summary>
        public TNative* Pointer { get; private set; }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            _ = s_instances.Remove((nint)Pointer);
            Pointer = null;
        }

        /// <summary>
        /// Converts a native object to a managed object.
        /// </summary>
        /// <param name="pointer">The pointer.</param>
        /// <returns>A managed object.</returns>
        public static TManaged? PointerToInstance(TNative* pointer)
        {
            if (pointer == null)
            {
                return null;
            }

            if (s_instances.TryGetValue((nint)pointer, out var weakRef)
                && weakRef.TryGetTarget(out var value))
            {
                return value;
            }

            value = new TManaged
            {
                Pointer = Native.CheckPointer(pointer)
            };
            s_instances[(nint)pointer] = new WeakReference<TManaged>(value);
            return value;
        }

        /// <summary>
        /// Converts a native object to a managed object.
        /// </summary>
        /// <param name="pointer">The pointer.</param>
        /// <returns>A managed object.</returns>
        public static TManaged PointerToInstanceNotNull(TNative* pointer) =>
            PointerToInstance(pointer) ?? throw new SdlException();
    }
}
