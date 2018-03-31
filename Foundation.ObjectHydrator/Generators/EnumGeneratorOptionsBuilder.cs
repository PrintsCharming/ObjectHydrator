using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.ObjectHydrator.Generators
{
    /// <summary>
    ///     Allows the generation of enum values to be configured
    /// </summary>
    /// <typeparam name="TEnum">The enum type</typeparam>
    internal class EnumGeneratorOptionsBuilder<TEnum> : IEnumGeneratorOptionsBuilder<TEnum>
        where TEnum : struct, IConvertible // attempt to restrict to just enum values
    {
        private readonly Dictionary<TEnum, int> _valueFrequency = new Dictionary<TEnum, int>();
        private readonly List<TEnum> _valuesToExclude = new List<TEnum>();

        public IReadOnlyCollection<TEnum> ValuesToExclude => _valuesToExclude.Distinct().ToArray();

        public IEnumGeneratorOptionsBuilder<TEnum> Excluding(TEnum value)
        {
            _valuesToExclude.Add(value);
            return this;
        }

        public IEnumGeneratorOptionsBuilder<TEnum> WithFrequency(TEnum value, int frequency)
        {
            _valueFrequency[value] = Math.Abs(frequency);
            return this;
        }

        public bool ShouldInclude(TEnum value)
        {
            return !_valuesToExclude.Contains(value);
        }

        public int ValueFrequency(TEnum value)
        {
            if (_valueFrequency.ContainsKey(value))
                return _valueFrequency[value];

            return 1;
        }
    }
}