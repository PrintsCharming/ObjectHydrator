using System;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    /// <summary>
    /// Generates a telephone number in the format used in the UK
    /// </summary>
    public class UnitedKingdomPhoneNumberGenerator : IGenerator<string>
    {
        private readonly string _ndcPrefix;
        private readonly Random _random = RandomSingleton.Instance.Random;

        /// <summary>
        /// Creates an instance of <see cref="UnitedKingdomPhoneNumberGenerator"/>
        /// See <a href="https://www.area-codes.org.uk/formatting.php">this formatting guide</a>
        /// </summary>
        /// <param name="ndcPrefix">
        /// The first non-zero part of the area code.
        /// <ul>
        ///     <li>1 0r 2 = Landline (geographic)</li>
        ///     <li>3 = Landline (non-geographic)</li>
        ///     <li>5 = Corporate and VOIP</li>
        ///     <li>7 = Mobiles</li>
        ///     <li>8 = special rate numbers</li>
        ///     <li>9 = premium rate numbers</li> 
        /// </ul>
        /// 
        /// </param>
        public UnitedKingdomPhoneNumberGenerator(string ndcPrefix)
        {
            _ndcPrefix = ndcPrefix ?? "1";
        }

        public string Generate()
        {
            // create an 11 character phone number
            // 01234 567 890
            var stringBuilder = new StringBuilder();

            // area code (National Destination Code)
            stringBuilder.Append("0");
            stringBuilder.Append(_ndcPrefix);
            for (int i = 0; i < 3; i++)
            {
                int val = this._random.Next(1, 9);
                stringBuilder.Append(val);
            }

            stringBuilder.Append(" ");
            //Subscriber number
            for (int i = 0; i < 6; i++)
            {
                int val = this._random.Next(0, 9);
                stringBuilder.Append(val);

                if ((i == 2) && ShouldAddSeparator())
                {
                    // add a space in the middle for nice formatting
                    stringBuilder.Append(" ");
                }
            }

            return stringBuilder.ToString();
        }

        private bool ShouldAddSeparator()
        {
            // randomly omit the formatting separator
            return _random.Next(0, 10) % 3 != 0;
        }
    }

    public class UnitedKingdomMobileGenerator : UnitedKingdomPhoneNumberGenerator
    {
        public UnitedKingdomMobileGenerator():base("7") { }
    }

    public class UnitedKingdomLandlineGenerator : UnitedKingdomPhoneNumberGenerator
    {
        public UnitedKingdomLandlineGenerator() : base("1") { }
    }
}