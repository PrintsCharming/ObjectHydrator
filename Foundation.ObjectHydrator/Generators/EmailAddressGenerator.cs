using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class EmailAddressGenerator : IGenerator<string>
    {
        private readonly Random _random;

        public EmailAddressGenerator()
        {
            _random = RandomSingleton.Instance.Random;
        }

        public string Generate()
        {
            IGenerator<string> fng = new FirstNameGenerator();
            IGenerator<string> lng = new LastNameGenerator();
            IGenerator<string> cng = new CompanyNameGenerator();

            var prefix = GetPrefix(fng, lng);
            var bizname = GetBizname(cng);

            var suffix = new[] {".com", ".net", ".org", ".info"};
            var num = _random.Next(0, suffix.Length - 1);
            var domaintype = suffix[num];

            return $"{prefix}@{bizname}{domaintype}";
        }

        private string GetPrefix(IGenerator<string> fng, IGenerator<string> lng)
        {
            var prefixtype = _random.Next(0, 1);
            var prefix = prefixtype == 0 ? $"{fng.Generate()}_{lng.Generate()}" : fng.Generate();
            return prefix;
        }

        private static string GetBizname(IGenerator<string> cng)
        {
            var bizname = cng.Generate();
            bizname = bizname.Replace(".", "");
            bizname = bizname.Replace(" ", "");
            bizname = bizname.Replace(",", "");
            return bizname;
        }
    }
}