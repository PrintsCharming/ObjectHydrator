using Foundation.ObjectHydrator.Interfaces;
using System;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableGuidGenerator : IGenerator<Guid?>
    {
        private readonly Random _random = RandomSingleton.Instance.Random;
        private readonly bool _allowNulls;

        public NullableGuidGenerator(bool allowNulls)
        {
            _allowNulls = allowNulls;
        }

        #region IGenerator Members

        public Guid? Generate()
        {
            if (_random.Next(0, _allowNulls ? 2 : 1) == 1)
            {
                return null;
            }

            return Guid.NewGuid();
        }

        #endregion
    }
}
