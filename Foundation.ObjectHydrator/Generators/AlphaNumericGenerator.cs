using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Linq;

namespace Foundation.ObjectHydrator.Generators
{
    public class AlphaNumericGenerator : IGenerator<string>
    {
        Random random;
        private int stringLength;

        public AlphaNumericGenerator(int length)
        {
            random = RandomSingleton.Instance.Random;
            stringLength = length;
        }

        public string Generate()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var result = new string(
                Enumerable.Repeat(chars, stringLength)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }
    }
}