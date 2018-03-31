using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class FromListGetSingleGenerator<T> : IGenerator<T>
    {
        private readonly Random random;
        private readonly IEnumerable<T> list = new List<T>();

        public FromListGetSingleGenerator(IEnumerable<T> list)
        {
            random = RandomSingleton.Instance.Random;
            this.list = list;
        }

        public T Generate()
        {
            return list.ElementAt(random.Next(0, list.Count()));
        }
    }
}