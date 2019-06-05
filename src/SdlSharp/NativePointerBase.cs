using System;
using System.Collections.Generic;

namespace SdlSharp
{
    /// <summary>
    /// The base class of an object that represents a pointer SDL object.
    /// </summary>
    /// <typeparam name="TNative">The native pointer type.</typeparam>
    /// <typeparam name="TManaged">The managed type.</typeparam>
    public unsafe abstract class NativePointerBase<TNative, TManaged> : IDisposable
        where TNative : unmanaged
        where TManaged : NativePointerBase<TNative, TManaged>, new()
    {
        private static readonly Dictionary<IntPtr, WeakReference<TManaged>> s_instances = new Dictionary<IntPtr, WeakReference<TManaged>>();

        /// <summary>
        /// The pointer.
        /// </summary>
        public TNative* Pointer { get; private set; }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            _ = s_instances.Remove((IntPtr)Pointer);
            Pointer = null;
        }

        public static TManaged? PointerToInstance(TNative* pointer)
        {
            if (pointer == null)
            {
                return null;
            }

            if (s_instances.TryGetValue((IntPtr)pointer, out var weakRef)
                && weakRef.TryGetTarget(out var value))
            {
                return value;
            }

            value = new TManaged
            {
                Pointer = Native.CheckPointer(pointer)
            };
            s_instances[(IntPtr)pointer] = new WeakReference<TManaged>(value);
            return value;
        }

        public static TManaged PointerToInstanceNotNull(TNative* pointer) =>
            PointerToInstance(pointer) ?? throw new SdlException();
    }
}
