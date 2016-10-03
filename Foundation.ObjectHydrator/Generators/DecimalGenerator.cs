using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class DecimalGenerator : IGenerator<decimal>
    {
        Random random;

        public decimal MinimumValue { get; set; }
        public decimal MaximumValue { get; set; }
        public int DecimalPlaces { get; set; }

        public DecimalGenerator()
            : this(0, 100)
        { }

        public DecimalGenerator(int decimalPlaces):this(0,100,decimalPlaces)
        {

        }

        public DecimalGenerator(decimal minimumValue, decimal maximumValue)
            : this(minimumValue, maximumValue, 2)
        { }

        public DecimalGenerator(decimal minimumValue, decimal maximumValue, int decimalPlaces)
        {
            if (minimumValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException("minimumValue", minimumValue, "minimumValue must be <= maximumValue");
            }

            if (decimalPlaces > 5)
            {
                throw new ArgumentOutOfRangeException("decimalPlaces", decimalPlaces, "decimalPlaces must be <=5;");
            }

            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            DecimalPlaces = decimalPlaces;

            random = RandomSingleton.Instance.Random;
        }

        public decimal Generate()
        {
            decimal toReturn;
            double decimalPart;

            // The offset adjustment to get down to an Int minimum value
            decimal offset = MinimumValue - Math.Floor(MinimumValue);
            decimal adjustedMinimum = MinimumValue - offset;
            decimal adjustedMaximum = MaximumValue - offset;

            toReturn = random.Next((int)adjustedMinimum, (int)adjustedMaximum);
            decimalPart = random.NextDouble();
            decimalPart *= Math.Pow(10, DecimalPlaces);
            decimalPart = (int)decimalPart;

            toReturn += Convert.ToDecimal(decimalPart / Math.Pow(10, DecimalPlaces));

            // Now, add back the offset
            toReturn += offset;

            return toReturn;
        }
    }
}