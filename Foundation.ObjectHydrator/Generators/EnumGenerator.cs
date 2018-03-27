using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    /// <summary>
    /// Produces a random value from the given list of enum values
    /// </summary>
    public class EnumGenerator:IGenerator<object>
    {
        private readonly Random _random = RandomSingleton.Instance.Random;
        private readonly Array _enumValues;


        public EnumGenerator(Array enumValues)
        {
            _enumValues = enumValues;
        }

        public object Generate()
        {
            return _enumValues.GetValue(_random.Next(0, _enumValues.Length));
        }
    }

    /// <summary>
    /// Produces a random value from the given enum
    /// </summary>
    public class EnumGenerator<TEnum> : IGenerator<TEnum>
        where TEnum : struct, IConvertible // attempt to restrict to enums only
    {
        private readonly Random _random;
        private readonly TEnum[] _values;

        public EnumGenerator(Func<IEnumGeneratorOptionsBuilder<TEnum>, IEnumGeneratorOptionsBuilder<TEnum>> optionBuilder = null)
        {
            this._random = RandomSingleton.Instance.Random;
            var options = new EnumGeneratorOptionsBuilder<TEnum>();
            if (optionBuilder != null)
            {
                optionBuilder(options);
            }


            var values = new List<TEnum>();
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                var enumValue = (TEnum)value;
                values.Add(enumValue);
            }

            this._values = values.Where(e => options.ShouldInclude(e)).ToArray();
        }

        public TEnum Generate()
        {
            var idx = _random.Next(0, _values.Length);
            return _values[idx];
        }
    }
}
