using System;
using System.Globalization;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public sealed class CulturesGenerator<TProperty> : IGenerator<TProperty>
    {
        private readonly Func<CultureInfo, TProperty> _propertyGetter;

        private readonly Random _random = RandomSingleton.Instance.Random;

        public CulturesGenerator(Func<CultureInfo, TProperty> propertyGetter)
        {
            _propertyGetter = propertyGetter;
        }

        public TProperty Generate()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            var next = _random.Next(0, cultures.Length - 1);

            var culture = cultures[next];

            return _propertyGetter(culture);
        }
    }
}