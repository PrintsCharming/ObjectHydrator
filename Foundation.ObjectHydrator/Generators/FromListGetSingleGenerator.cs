using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class FromListGetSingleGenerator<T> : IGenerator<T>
    {
        private readonly Random _random = RandomSingleton.Instance.Random;
        private readonly List<T> _list;

        public FromListGetSingleGenerator(IEnumerable<T> list)
        {
            this._list = list.ToList();
        }

        public void Add(T value, int frequency)
        {

        }

        public T Generate()
        {
            return _list[_random.Next(0, _list.Count)];
        }
    }

    internal class GeneratedItemFrequencyDefinition<T>
    {
        public GeneratedItemFrequencyDefinition(T value, int frequency= 1)
        {
            Value = value;
            Frequency = frequency;
        }

        public GeneratedItemFrequencyDefinition(KeyValuePair<T, int> value)
        {
            Value = value.Key;
            Frequency = value.Value;
        }

        public T Value { get; }
        public int Frequency { get; }

        public IReadOnlyCollection<T> ToValues
        {
            get
            {
                var valuesAtFrequency = new List<T>(this.Frequency);
                for (int i = 0; i < this.Frequency; i++)
                {
                    valuesAtFrequency.Add(this.Value);
                }

                return valuesAtFrequency;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} x {1}", Value == null ? "null": Value.ToString(), Frequency);
        }
    }
}
