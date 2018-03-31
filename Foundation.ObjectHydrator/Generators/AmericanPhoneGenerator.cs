using System;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class AmericanPhoneGenerator:IGenerator<string>
    {
        readonly Random _random;

        public AmericanPhoneGenerator()
        {
            _random = RandomSingleton.Instance.Random;
        }

        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            int areacodefirstpart = _random.Next(2, 9);
            sb.Append(areacodefirstpart.ToString());
            int areacode = _random.Next(0, 99);
            if (areacode < 10)
            {
                areacode += 10;
            }
            sb.Append(areacode.ToString());
            sb.Append(")");
            int prefixfirstpart = _random.Next(2, 9);
            sb.Append(prefixfirstpart.ToString());
            int prefix = _random.Next(0, 99);
            if (prefix < 10)
            {
                prefix += 10;
            }
            sb.Append(prefix.ToString());
            sb.Append("-");
            int suffix = _random.Next(0000, 9999);
            if (suffix < 1000)
            {
                suffix = suffix + 1000;
            }
            sb.Append(suffix.ToString());
            return sb.ToString();
        }
        
    }
}
