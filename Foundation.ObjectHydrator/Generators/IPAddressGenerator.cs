using System;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class IpAddressGenerator : IGenerator<string>
    {
        private readonly Random _random;

        public IpAddressGenerator()
        {
            _random = RandomSingleton.Instance.Random;
        }

        public string Generate()
        {
            var sb = new StringBuilder();
            sb.Append(_random.Next(1, 255));
            sb.Append(".");

            sb.Append(_random.Next(0, 255));
            sb.Append(".");

            sb.Append(_random.Next(0, 255));
            sb.Append(".");

            sb.Append(_random.Next(0, 255));
            return sb.ToString();
        }
    }
}