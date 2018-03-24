using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class EmailAddressGenerator:IGenerator<string>
    {
        Random random;
        
        public EmailAddressGenerator()
        {
            random = RandomSingleton.Instance.Random;
        }

        private string GetPrefix(IGenerator<string> fng, IGenerator<string> lng)
        {
            int prefixtype = random.Next(0, 1);
            string prefix;
            if (prefixtype == 0)
            {
                prefix = String.Format("{0}_{1}", (string)fng.Generate(), (string)lng.Generate());
            }
            else
            {
                prefix = (string)fng.Generate();
            }
            return prefix;
        }
        private static string GetBizname(IGenerator<string> cng)
        {
            string bizname = (string)cng.Generate();
            bizname = bizname.Replace(".", "");
            bizname = bizname.Replace(" ", "");
            bizname = bizname.Replace(",", "");
            return bizname;
        }
        public string Generate()
        {
           
            IGenerator<string> fng = new FirstNameGenerator();
            IGenerator<string> lng = new LastNameGenerator();
            IGenerator<string> cng = new CompanyNameGenerator();

            string prefix = GetPrefix(fng, lng);
            string bizname = GetBizname(cng);

            string[] suffix = new string[4] { ".com", ".net", ".org", ".info" };
            int num = random.Next(0, suffix.Length - 1);
            string domaintype = suffix[num];

            return String.Format("{0}@{1}{2}", prefix, bizname, domaintype);


        }



    }
}
