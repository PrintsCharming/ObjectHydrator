using Foundation.ObjectHydrator.Interfaces;
using System;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableIntegerGenerator : IGenerator<int?>
    {
        readonly Random _random;
        private readonly bool _allowNulls;

        public int MinimumValue { get; set; }
        public int MaximumValue { get; set; }

        public NullableIntegerGenerator(bool allowNulls)
            : this(0, 100, allowNulls)
        { }

        public NullableIntegerGenerator(int minimumValue, int maximumValue, bool allowNulls)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            _allowNulls = allowNulls;

            _random = RandomSingleton.Instance.Random;
        }

        public int? Generate()
        {
            var result = _random.Next(MinimumValue, _allowNulls ? MaximumValue + 1 : MaximumValue);

            //null will be represented by MaximumValue
            if (result == MaximumValue)
            {
                return null;
            }

            return result;
        }
    }
}
