using Foundation.ObjectHydrator.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.ObjectHydrator.Generators
{
    public class ArrayGenerator<T> : ListGenerator<T>
    {
        public ArrayGenerator(int length, IGenerator<T> elementGenerator = null) : base(length, elementGenerator)
        {
        }

        public override IList<T> Generate()
        {
            return base.Generate().ToArray();
        }
    }
}