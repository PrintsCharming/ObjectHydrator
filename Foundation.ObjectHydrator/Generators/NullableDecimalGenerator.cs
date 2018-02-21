using Foundation.ObjectHydrator.Interfaces;
using System;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableDecimalGenerator : IGenerator<decimal?>
    {
        readonly Random _random;
        private readonly bool _allowNulls;
        public decimal MinimumValue { get; set; }
        public decimal MaximumValue { get; set; }
        public int DecimalPlaces { get; set; }

        public NullableDecimalGenerator(bool allowNulls)
            : this(0.0m, 100.00m, allowNulls)
        { }

        public NullableDecimalGenerator(int decimalPlaces, bool allowNulls) : this(0.0m, 100.00m, decimalPlaces, allowNulls)
        {

        }

        public NullableDecimalGenerator(decimal minimumValue, decimal maximumValue, bool allowNulls)
            : this(minimumValue, maximumValue, 2, allowNulls)
        { }


        public NullableDecimalGenerator(decimal minimumValue, decimal maximumValue, int decimalPlaces, bool allowNulls)
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
            _allowNulls = allowNulls;

            _random = RandomSingleton.Instance.Random;
        }

        public decimal? Generate()
        {
            // The offset adjustment to get down to an Int minimum value
            var offset = MinimumValue - Math.Floor(MinimumValue);
            var adjustedMinimum = MinimumValue - offset;
            var adjustedMaximum = MaximumValue - offset;

            var max = _allowNulls ? (int)adjustedMaximum + 1 : (int)adjustedMaximum;
            var nextRandomVal = _random.Next((int)adjustedMinimum, max);

            if (nextRandomVal == (int)adjustedMaximum)
            {
                return null;
            }

            double toReturn = nextRandomVal;

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
