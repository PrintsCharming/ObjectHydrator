using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    /// <summary>
    ///     Produces a random value from the given list of enum values
    /// </summary>
    public class EnumGenerator : IGenerator<object>
    {
        private readonly Array _enumValues;
        private readonly Random _random = RandomSingleton.Instance.Random;


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
    ///     Produces a random value from the given enum
    /// </summary>
    public class EnumGenerator<TEnum> : IGenerator<TEnum>
        where TEnum : struct, IConvertible // attempt to restrict to enums only
    {
        private readonly FromListGetSingleGenerator<TEnum> _generator;

        public EnumGenerator(Func<IEnumGeneratorOptionsBuilder<TEnum>, IEnumGeneratorOptionsBuilder<TEnum>> optionBuilder = null)
        {
            var options = new EnumGeneratorOptionsBuilder<TEnum>();
            optionBuilder?.Invoke(options);

            var values = GetAllValuesForEnum();
            var generator = new FromListGetSingleGenerator<TEnum>();

            foreach (var value in values.Where(e => options.ShouldInclude(e)))
            {
                generator.Add(value, options.ValueFrequency(value));
            }

            _generator = generator;
        }

        public TEnum Generate()
        {
            return _generator.Generate();
        }

        private static List<TEnum> GetAllValuesForEnum()
        {
            var values = new List<TEnum>();
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                var enumValue = (TEnum) value;
                values.Add(enumValue);
        }

            return values;
    }
    }
}