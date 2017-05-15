using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.ObjectHydrator.Core.Generators
{
    public class RandomOptionsGenerator<T> : IGenerator<T>
    {
        Random random;

        public T[] RandomStringOptions { get; set; }

        public RandomOptionsGenerator(params T[] randomStringOptions)
        {
            random = RandomSingleton.Instance.Random;
            RandomStringOptions = randomStringOptions ?? new T[] { };
        }

        public T Generate()
        {
            var options = RandomStringOptions;
            int num = random.Next(0, options.Length - 1);
            return options[num];
        }
    }
}
