using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;


namespace Foundation.ObjectHydrator.Generators
{
    public class CreditCardTypeGenerator:IGenerator<string>
    {
 
        public string Generate()
        {
            return
                new FromListGetSingleGenerator<string>(new List<string>
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
