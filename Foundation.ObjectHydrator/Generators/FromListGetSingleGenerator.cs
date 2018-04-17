using System;
using System.Collections;
using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class FromListGetSingleGenerator<T> : IGenerator<T>, IEnumerable<T>
    {
        private readonly Random _random = RandomSingleton.Instance.Random;
        private readonly FrequencyList<T> _list;

        public FromListGetSingleGenerator() : this(null) { }
        public FromListGetSingleGenerator(IEnumerable<T> list)
        {
            _list = new FrequencyList<T>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    _list.Add(item);
                }
            }
        }

        public void Add(T value, int frequency)
        {
            _list.Add(value, frequency);
        }

        public void Add(T value)
        {
            _list.Add(value);
        }

        public T Generate()
        {
            return _list[_random.Next(0, _list.Count)];
        }

        #region IEnumerable implementation

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _list).GetEnumerator();
        }

        #endregion
    }
}
