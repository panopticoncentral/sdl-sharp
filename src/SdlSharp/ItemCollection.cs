using System;
using System.Collections;
using System.Collections.Generic;

namespace SdlSharp
{
    internal sealed class ItemCollection<T> : IReadOnlyList<T>
    {
        private readonly Func<int, T> _getter;
        private readonly Func<int> _counter;

        public T this[int index] => _getter(index);

        public int Count => _counter();

        public ItemCollection(Func<int, T> getter, Func<int> counter)
        {
            _getter = getter;
            _counter = counter;
        }

        private IEnumerable<T> Devices()
        {
            var count = Count;
            for (var index = 0; index < count; index++)
            {
                yield return this[index];
            }
        }

        public IEnumerator<T> GetEnumerator() => Devices().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
