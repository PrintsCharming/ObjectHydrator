using Foundation.ObjectHydrator.Interfaces;
using System;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableEnumGenerator : IGenerator<object>
    {
        readonly Random _random;
        readonly Array _enumValues;
        private readonly bool _allowNulls;

        public NullableEnumGenerator(Array enumValues, bool allowNulls)
        {
            _enumValues = enumValues;
            _allowNulls = allowNulls;
            _random = RandomSingleton.Instance.Random;

        }

        public object Generate()
        {
            var seed = _random.Next(0, _allowNulls ? _enumValues.Length + 1 : _enumValues.Length);

            if (seed == _enumValues.Length)
            {
                return null;
            }

            return _enumValues.GetValue(seed);
        }
    }
}
