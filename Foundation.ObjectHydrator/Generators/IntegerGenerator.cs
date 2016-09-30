using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class IntegerGenerator:IGenerator<int>
    {
        Random random;

        public int MinimumValue { get; set; }
        public int MaximumValue { get; set; }

        public IntegerGenerator()
            : this(0, 100)
        { }

        public IntegerGenerator(int minimumValue, int maximumValue)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;

            random = RandomSingleton.Instance.Random;
        }

        public int Generate()
        {
            return random.Next(MinimumValue, MaximumValue + 1);
        }
    }
}
