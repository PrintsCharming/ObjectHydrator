using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class ByteArrayGenerator : IGenerator<byte[]>
    {
        readonly Random _random;
        public int Length { get; set; }

        public ByteArrayGenerator()
            : this(8)
        {

        }

        public ByteArrayGenerator(int length)
        {
            _random = RandomSingleton.Instance.Random;
            Length = length;
        }

        #region IGenerator Members

        public byte[] Generate()
        {
            byte[] toReturn = new byte[Length];

            _random.NextBytes(toReturn);

            return toReturn;
        }

        #endregion
    }
}
