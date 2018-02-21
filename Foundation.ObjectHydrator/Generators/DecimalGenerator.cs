using Foundation.ObjectHydrator.Interfaces;
using System;

namespace Foundation.ObjectHydrator.Generators
{
    public class DecimalGenerator : IGenerator<decimal>
    {
        readonly Random _random;

        public decimal MinimumValue { get; set; }
        public decimal MaximumValue { get; set; }
        public int DecimalPlaces { get; set; }

        public DecimalGenerator()
            : this(0.0m, 100.00m)
        { }

        public DecimalGenerator(int decimalPlaces) : this(0.0m, 100.00m, decimalPlaces)
        {

        }

        public DecimalGenerator(decimal minimumValue, decimal maximumValue)
            : this(minimumValue, maximumValue, 2)
        { }


        public DecimalGenerator(decimal minimumValue, decimal maximumValue, int decimalPlaces)
        {
            if (minimumValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumValue), minimumValue, "minimumValue must be <= maximumValue");
            }

            if (decimalPlaces > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces), decimalPlaces, "decimalPlaces must be <=5;");
            }

            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            DecimalPlaces = decimalPlaces;

            _random = RandomSingleton.Instance.Random;
        }

        public decimal Generate()
        {
            // The offset adjustment to get down to an Int minimum value
            var offset = MinimumValue - Math.Floor(MinimumValue);
            var adjustedMinimum = MinimumValue - offset;
            var adjustedMaximum = MaximumValue - offset;

            double toReturn = _random.Next((int)adjustedMinimum, (int)adjustedMaximum);
            var decimalPart = _random.NextDouble();
            decimalPart *= Math.Pow(10, DecimalPlaces);
            decimalPart = (int)decimalPart;

            toReturn += decimalPart / Math.Pow(10, DecimalPlaces);

            // Now, add back the offset
            toReturn += (double)offset;

            return (decimal)toReturn;
        }


    }
}
