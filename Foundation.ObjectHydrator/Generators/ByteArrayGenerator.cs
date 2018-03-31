using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class ByteArrayGenerator : IGenerator<byte[]>
    {
        private readonly Random _random;

        public ByteArrayGenerator()
            : this(8)
        {
        }

        public ByteArrayGenerator(int length)
        {
            _random = RandomSingleton.Instance.Random;
            Length = length;
        }

        public int Length { get; set; }

        #region IGenerator Members

        public byte[] Generate()
        {
            var toReturn = new byte[Length];

            _random.NextBytes(toReturn);

            return toReturn;
        }

        #endregion
    }
}