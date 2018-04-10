using System.Collections;
using System.Collections.Generic;

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
        private readonly List<GeneratedItemFrequencyDefinition<T>> _data = new List<GeneratedItemFrequencyDefinition<T>>();

        public FrequencyList()
        {
            
        }

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
        public IEnumerator<T> GetEnumerator()
        {
            return new FrequencyEnumerator(_data);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class FrequencyEnumerator:IEnumerator<T>
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