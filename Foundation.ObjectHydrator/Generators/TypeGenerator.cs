using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;
using System.Reflection;

namespace Foundation.ObjectHydrator.Generators
{
    public class TypeGenerator<T>:IGenerator<T>
    {
        public T Generate()
        {
            return new Hydrator<T>().GetSingle();
        }
    }
}
