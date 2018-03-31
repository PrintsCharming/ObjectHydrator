using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class IntegerGenerator : IGenerator<int>
    {
        private readonly Random _random;

        public IntegerGenerator()
            : this(0, 100)
        {
        }

        public IntegerGenerator(int minimumValue, int maximumValue)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;

            _random = RandomSingleton.Instance.Random;
        }

        public int MinimumValue { get; set; }
        public int MaximumValue { get; set; }

        public int Generate()
        {
            return _random.Next(MinimumValue, MaximumValue + 1);
        }
    }
}