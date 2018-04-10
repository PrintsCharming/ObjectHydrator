using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class BooleanGenerator : IGenerator<bool>
    {
        private readonly Random _random;

        public BooleanGenerator()
        {
            _random = RandomSingleton.Instance.Random;
        }

        #region IGenerator Members

        public bool Generate()
        {
            return Convert.ToBoolean(_random.Next(0, 2));
        }

        #endregion
    }
}