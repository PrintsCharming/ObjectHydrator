using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class DoubleGenerator : IGenerator<double>
    {
        private readonly Random random;

        public DoubleGenerator()
            : this(0.0, 100)
        {
        }

        public DoubleGenerator(int decimalPlaces) : this(0.0, 100.00, decimalPlaces)
        {
        }

        public DoubleGenerator(double minimumValue, double maximumValue)
            : this(minimumValue, maximumValue, 2)
        {
        }


        public DoubleGenerator(double minimumValue, double maximumValue, int decimalPlaces)
        {
            if (minimumValue > maximumValue)
                throw new ArgumentOutOfRangeException("minimumValue", minimumValue,
                    "minimumValue must be <= maximumValue");

            if (decimalPlaces > 5)
                throw new ArgumentOutOfRangeException("decimalPlaces", decimalPlaces, "decimalPlaces must be <=5;");

            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            DecimalPlaces = decimalPlaces;

            random = RandomSingleton.Instance.Random;
        }

        public double MinimumValue { get; set; }
        public double MaximumValue { get; set; }
        public int DecimalPlaces { get; set; }

        public double Generate()
        {
            double toReturn;
            double decimalPart;

            // The offset adjustment to get down to an Int minimum value
            var offset = MinimumValue - Math.Floor(MinimumValue);
            var adjustedMinimum = MinimumValue - offset;
            var adjustedMaximum = MaximumValue - offset;

            toReturn = random.Next((int) adjustedMinimum, (int) adjustedMaximum);
            decimalPart = random.NextDouble();
            decimalPart *= Math.Pow(10, DecimalPlaces);
            decimalPart = (int) decimalPart;

            toReturn += decimalPart / Math.Pow(10, DecimalPlaces);

            // Now, add back the offset
            toReturn += offset;

            return toReturn;
        }
    }
}