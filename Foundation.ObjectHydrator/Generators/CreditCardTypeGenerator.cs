using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;


namespace Foundation.ObjectHydrator.Generators
{
    public class CreditCardTypeGenerator:IGenerator<string>
    {
 
        public string Generate()
        {
            return
                new FromListGenerator<string>(new List<string>
                                                  {
                                                      "MasterCard",
                                                      "Visa",
                                                      "Discover",
                                                      "American Express"
                                                  })
                    .Generate();
        }
    }
}
