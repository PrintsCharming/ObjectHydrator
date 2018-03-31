using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class CcvGenerator:IGenerator<string>
    {
        readonly Random _random;
        public string CcvType { get; set; }
        public CcvGenerator(string ccvtype)
        {
            _random = RandomSingleton.Instance.Random;
            CcvType = ccvtype;
        }

        public string Generate()
        {
            int ccv = _random.Next(0, 999);
            if (ccv < 100)
            {
                ccv += 100;
            }
            return ccv.ToString();
        }
    }
}
