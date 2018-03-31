using System;
using System.Linq;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class AlphaNumericGenerator : IGenerator<string>
    {
        readonly Random _random;
        private readonly int _stringLength;

        public AlphaNumericGenerator(int length)
        {
            _random = RandomSingleton.Instance.Random;
            _stringLength = length;
        }

        public string Generate()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var result = new string(
                Enumerable.Repeat(chars, _stringLength)
                          .Select(s => s[_random.Next(s.Length)])
                          .ToArray());

            return result;
        }
    }
}