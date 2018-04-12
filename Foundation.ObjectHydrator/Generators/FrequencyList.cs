using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Foundation.ObjectHydrator.Generators
{
    /// <summary>
    /// A list of items where each item's membership can be weighted
    /// </summary>
    /// <remarks>
    /// This is intended to allow an efficient list of items where some items occur more frequently than others.  
    /// This will help with data generation where the distribution of values is not even.
    /// For example: If we wanted to populate a value for IsLotteryWinner on a list of people where the chances 
    /// of a lottery win are 1,000,000 to 1 we could create an array of 1,000,000 items where 1 value is true 
    /// and the rest false.  The FrequencyList allows us to declare this with two entries:
    /// 
    /// var x = new FrequencyList&lt;bool&gt; = new FrequencyList&lt;bool&gt;()
    /// {
    ///     true,              // Frequency of 1
    ///     { false, 999999 }  // Frequency of 999,999
    /// };
    /// 
    /// var isLotteryWinner = x[Random(0, x.Count())];  // ~ 1 in 1,000,000 will be true
    /// 
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class FrequencyList<T> : IEnumerable<T>
    {
        private readonly List<GeneratedItemFrequencyDefinition<T>> _data;

        #region Constructors
        public FrequencyList()
        {
            _data = new List<GeneratedItemFrequencyDefinition<T>>();
        }

        /// <summary>
        /// Makes a copy of the frequency list
        /// </summary>
        /// <param name="data"></param>
        public FrequencyList(FrequencyList<T> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            _data = new List<GeneratedItemFrequencyDefinition<T>>(data._data);
        }

        #endregion

        /// <summary>
        /// Adds an item with a frequency of 1
        /// </summary>
        /// <param name="item">the item to add</param>
        public void Add(T item)
        {
            this.Add(item, 1);
        }

        /// <summary>
        /// Adds the <paramref name="item"/> with the given <paramref name="frequency"/>
        /// </summary>
        /// <param name="item">The value to add</param>
        /// <param name="frequency">The number of occurences</param>
        public void Add(T item, int frequency)
        {
            this._data.Add(new GeneratedItemFrequencyDefinition<T>(item, frequency));
        }

        /// <summary>
        /// Gets an enumerator for the collection
        /// </summary>
        /// <returns>an enumerator</returns>
        [Pure]
        public IEnumerator<T> GetEnumerator()
        {
            return new FrequencyEnumerator(_data);
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [Pure]
        public T this[int index]
        {
            get
            {
                if (index >= this.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                var value = default(T);
                for (int i = 0; i < _data.Count; i++)
                {
                    var item = _data[i];
                    if (index >= item.Frequency)
                    {
                        index -= item.Frequency;
                    }
                    else
                    {
                        value = item.Value;
                        break;
                    }
                }
                return value;
            }
        }

        [Pure]
        public int Count
        {
            get { return _data.Sum(d => d.Frequency); }
        }

        [Pure]
        public FrequencyList<T> Concat(FrequencyList<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            var result = new FrequencyList<T>(this);
            result._data.AddRange(other._data);

            return result;
        }

        private class FrequencyEnumerator : IEnumerator<T>
        {
            private readonly List<GeneratedItemFrequencyDefinition<T>> _data;
            private int _dataIdx = 0;
            private int _itemIdx = 0;
            private T _current = default(T);

            public FrequencyEnumerator(List<GeneratedItemFrequencyDefinition<T>> data)
            {
                if (data == null)
                {
                    throw new ArgumentNullException(nameof(data));
                }

                _data = data;
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                if (_dataIdx >= _data.Count)
                {
                    return false;
                }

                var currentData = _data[_dataIdx];
                if (_itemIdx >= currentData.Frequency)
                {
                    ++_dataIdx;
                    _itemIdx = 0;
                    if (_dataIdx >= _data.Count)
                    {
                        return false;
                    }
                    currentData = _data[_dataIdx];
                }

                _current = currentData.Value;
                ++_itemIdx;
                return true;
            }

            public void Reset()
            {
                this._dataIdx = 0;
                this._itemIdx = 0;
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