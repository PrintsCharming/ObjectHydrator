using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullGenerator:IGenerator<object>
    {
        public object Generate()
        {
            return null;
        }
    }
}
