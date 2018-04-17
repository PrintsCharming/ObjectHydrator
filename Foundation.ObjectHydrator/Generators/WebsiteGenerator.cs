using System;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class WebsiteGenerator : IGenerator<string>
    {
        private readonly Random _random;

        public WebsiteGenerator()
        {
            _random = RandomSingleton.Instance.Random;
        }

        public string Generate()
        {
            var sb = new StringBuilder();
            sb.Append("http://www.");
            IGenerator<string> companyname = new CompanyNameGenerator();
            var bizname = companyname.Generate();
            bizname = bizname.Replace(".", "");
            bizname = bizname.Replace(" ", "");
            bizname = bizname.Replace(",", "");
            sb.Append(bizname);
            var suffix = new string[] {".com", ".net", ".org", ".info"};
            var num = _random.Next(0, suffix.Length - 1);
            sb.Append(suffix[num]);
            return sb.ToString().ToLower();
        }
    }
}