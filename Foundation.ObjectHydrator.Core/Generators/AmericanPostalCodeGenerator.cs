using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class AmericanPostalCodeGenerator:IGenerator<string>
    {
        Random random;
        public int PercentageWithPlusFour { get; private set; }

        public AmericanPostalCodeGenerator(int percentageWithPlusFour)
        {
            PercentageWithPlusFour = percentageWithPlusFour;

            random = RandomSingleton.Instance.Random;
        }

         public string Generate()
        {
            string plusFour = String.Empty;

            if (PercentageWithPlusFour > 0 && random.Next(0, 100) % (100 / PercentageWithPlusFour) == 0)
            {
                plusFour = String.Format("-{0:0000}", random.Next(1, 9999));
            }

            return String.Format("{0:00000}{1}",
                random.Next(501, 99950),
                plusFour).ToString();
        }
    }
}
