using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundation.ObjectHydrator.Generators
{
    public class FrequencyList<T> : IEnumerable<T>
    {
        private readonly List<GeneratedItemFrequencyDefinition<T>> _data = new List<GeneratedItemFrequencyDefinition<T>>();

        public FrequencyList()
        {
            
        }

        public void Add(T item)
        {
            this.Add(item, 1);
        }
        public void Add(T item, int frequency)
        {
            this._data.Add(new GeneratedItemFrequencyDefinition<T>(item, frequency));
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new FrequencyEnumerator<T>(_data);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class FrequencyEnumerator<T>:IEnumerator<T>
        {
            private readonly List<GeneratedItemFrequencyDefinition<T>> _data;
            private int _idx = 0;
            private T _current = default(T);

            public FrequencyEnumerator(List<GeneratedItemFrequencyDefinition<T>> data)
            {
                _data = data;
            }

            public void Dispose()
            {
                

            }

            public bool MoveNext()
            {
                if (_idx >= _data.Count)
                {
                    return false;
                }

                _current = _data[_idx++].Value;
                return true;
            }

            public void Reset()
            {
                this._idx = 0;
                this._current = default(T);
            }

            public T Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }   
        }
    }
}