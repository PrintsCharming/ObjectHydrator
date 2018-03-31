using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class FromListGetSingleGenerator<T> : IGenerator<T>
    {
        private readonly Random _random;
        private readonly IEnumerable<T> _list;

        public FromListGetSingleGenerator(IEnumerable<T> list)
        {
            _random = RandomSingleton.Instance.Random;
            _list = list;
        }

        public T Generate()
        {
            return _list.ElementAt(_random.Next(0, _list.Count()));
        }
    }
}