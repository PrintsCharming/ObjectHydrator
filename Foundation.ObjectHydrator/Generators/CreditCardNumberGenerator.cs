using System;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class CreditCardNumberGenerator : IGenerator<string>
    {
        private readonly Random _random;

        public CreditCardNumberGenerator()
            : this(13)
        {
        }

        public CreditCardNumberGenerator(int length)
        {
            _random = RandomSingleton.Instance.Random;
            Length = length;
        }

        public int Length { get; set; }

        #region IGenerator Members

        public string Generate()
        {
            var toReturn = new StringBuilder();

            // Accumulator for the check digit calculation
            var accumulator = 0;

            // Counter to use with mod 2 to determine if the digit should be * by 2 when accumulating.
            var counter = 0;

            for (var i = 0; i < Length - 1; i++)
            {
                counter++;
                var digit = _random.Next(0, 9);

                if (counter % 2 == 1)
                    accumulator += digit * 2;
                else
                    accumulator += digit;

                toReturn.Append(digit);
            }

            // Do the check digit part...
            toReturn.Append(9 - accumulator % 10);
            return toReturn.ToString();
        }

        #endregion
    }
}