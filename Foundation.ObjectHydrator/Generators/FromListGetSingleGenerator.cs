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
}
