using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    /// <summary>
    /// Attempts to generate a unique value that conforms to the UK National Insurance number specification
    /// </summary>
    public class UnitedKingdomNationalInsuranceGenerator : IGenerator<string>
    {
        private readonly Random _random;

        private static readonly List<string> UsedValues = new List<string>();
        private static readonly char[] ValidPrefixChars = new char[] { 'A', 'B', 'C', 'E', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'W', 'X', 'Y', 'Z' };
        private static readonly string[] InvalidPrefixes = new string[] { "BG", "GB", "NK", "KN", "TN", "NT", "ZZ", "OO", "CR", "FY", "MW", "NC", "PP", "PY", "PZ", "MA", "JY", "GY" };
        private static readonly char[] InvalidPrefixSecondChars = new char[] { 'O' };
        private static readonly char[] ValidSuffixChars = new char[] { 'A', 'B', 'C', 'D' };


        public UnitedKingdomNationalInsuranceGenerator()
        {
            _random = RandomSingleton.Instance.Random;
        }

        public string Generate()
        {
            const int maxAttempts = 10000;
            var attempt = 0;

            string candidate;
            do
            {
                candidate = GetValue();
            } while (UsedValues.Contains(candidate) && ++attempt < maxAttempts);

            UsedValues.Add(candidate);
            return candidate;
        }

        private string GetValue()
        {
            string validPrefix = GetValidPrefix();
            string str = SequenceOfDigits(6);
            char chr = ValidSuffixCharacter();
            return string.Format("{0}{1}{2}", new object[] { validPrefix, str, chr });
        }

        private string SequenceOfDigits(int length)
        {
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                var val = _random.Next(0, 10);
                result[i] = val.ToString()[0];
            }

            return new string(result);
        }

        private string GetValidPrefix()
        {
            string str;
            do
            {
                str = new string(new char[]
                {
                    ValidPrefixFirstCharacter(),
                    ValidPrefixSecondCharacter()
                });
            } while (InvalidPrefixes.Contains(str));

            return str;

        }
        private char ValidPrefixFirstCharacter()
        {
            return AnyElement(ValidPrefixChars);
        }

        private char ValidPrefixSecondCharacter()
        {
            char chr;
            do
            {
                chr = AnyElement(ValidPrefixChars);
            }
            while (InvalidPrefixSecondChars.Contains(chr));
            return chr;
        }

        private T AnyElement<T>(IReadOnlyCollection<T> list)
        {
            var max = list.Count;
            if (max < 0)
            {
                return default(T);
            }

            var idx = _random.Next(0, max);
            return list.ElementAt(idx);
        }

        private char ValidSuffixCharacter()
        {
            return AnyElement(ValidSuffixChars);
        }

    }
}