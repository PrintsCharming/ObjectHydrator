using Foundation.ObjectHydrator.Interfaces;
using System;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableBooleanGenerator : IGenerator<bool?>
    {
        private readonly Random _random;
        private readonly bool _allowNulls;
        public NullableBooleanGenerator(bool allowNulls)
        {
            _allowNulls = allowNulls;
            _random = RandomSingleton.Instance.Random;
        }

        #region IGenerator Members

        public bool? Generate()
        {
            var seed = _random.Next(0, _allowNulls ? 3 : 2);

            if (seed > 1)
            {
                return null;
            }
            return Convert.ToBoolean(seed);
        }

        #endregion
    }
}