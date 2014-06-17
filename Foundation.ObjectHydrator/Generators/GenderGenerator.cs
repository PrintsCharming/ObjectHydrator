using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;
using Foundation.ObjectHydrator.Generators;

namespace Foundation.ObjectHydrator.Generators
{
    public class GenderGenerator : IGenerator<string>
    {



        public string Generate()
        {
            return ((IGenerator<string>)new FromListGetSingleGenerator<string>(new List<string> { "Male", "Female" })).Generate();
        }
    }
}
