using System;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class WebsiteGenerator:IGenerator<string>
    {
        Random random;

        public WebsiteGenerator()
        {
            random = RandomSingleton.Instance.Random;
        }

        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("http://www.");
            IGenerator<string> companyname = new CompanyNameGenerator();
            string bizname = (string)companyname.Generate();
            bizname = bizname.Replace(".", "");
            bizname = bizname.Replace(" ", "");
            bizname = bizname.Replace(",", "");
            sb.Append(bizname);
            string[] suffix = new string[4] { ".com", ".net", ".org", ".info" };
            int num = random.Next(0, suffix.Length - 1);
            sb.Append(suffix[num]);
            return sb.ToString().ToLower();


        }
    }
}
