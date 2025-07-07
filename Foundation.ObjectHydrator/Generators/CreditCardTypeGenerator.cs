using Foundation.ObjectHydrator.Interfaces;
using System.Collections.Generic;


namespace Foundation.ObjectHydrator.Generators
{
    public class CreditCardTypeGenerator : IGenerator<string>
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
