using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class AmericanPostalCodeGenerator : IGenerator<string>
    {
        private readonly Random random;

        public AmericanPostalCodeGenerator(int percentageWithPlusFour)
        {
            PercentageWithPlusFour = percentageWithPlusFour;

            random = RandomSingleton.Instance.Random;
        }

        public int PercentageWithPlusFour { get; }

        public string Generate()
        {
            var plusFour = string.Empty;

            if (PercentageWithPlusFour > 0 && random.Next(0, 100) % (100 / PercentageWithPlusFour) == 0)
                plusFour = $"-{random.Next(1, 9999):0000}";

            return $"{random.Next(501, 99950):00000}{plusFour}";
        }
    }
}