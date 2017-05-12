using System;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class IPAddressGenerator:IGenerator<string>
    {
        Random random;

        public IPAddressGenerator()
        {
            random = RandomSingleton.Instance.Random;
        }

        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(random.Next(1, 255));
            sb.Append(".");

            sb.Append(random.Next(0, 255));
            sb.Append(".");

            sb.Append(random.Next(0, 255));
            sb.Append(".");

            sb.Append(random.Next(0, 255));
            return sb.ToString();
        }
    }
}
