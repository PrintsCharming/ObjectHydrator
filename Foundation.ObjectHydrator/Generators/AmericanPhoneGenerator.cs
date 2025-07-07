using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Text;

namespace Foundation.ObjectHydrator.Generators
{
    public class AmericanPhoneGenerator : IGenerator<string>
    {
        Random random;

        public AmericanPhoneGenerator()
        {
            random = RandomSingleton.Instance.Random;
        }

        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            int areacodefirstpart = (int)random.Next(2, 9);
            sb.Append(areacodefirstpart.ToString());
            int areacode = (int)random.Next(0, 99);
            if (areacode < 10)
            {
                areacode += 10;
            }
            sb.Append(areacode.ToString());
            sb.Append(")");
            int prefixfirstpart = (int)random.Next(2, 9);
            sb.Append(prefixfirstpart.ToString());
            int prefix = (int)random.Next(0, 99);
            if (prefix < 10)
            {
                prefix += 10;
            }
            sb.Append(prefix.ToString());
            sb.Append("-");
            int suffix = random.Next(0000, 9999);
            if (suffix < 1000)
            {
                suffix = suffix + 1000;
            }
            sb.Append(suffix.ToString());
            return sb.ToString();
        }

    }
}
