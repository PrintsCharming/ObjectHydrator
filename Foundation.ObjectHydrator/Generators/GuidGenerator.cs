using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class GuidGenerator : IGenerator<Guid>
    {
        #region IGenerator Members

        public Guid Generate()
        {
            return Guid.NewGuid();
        }

        #endregion
    }
}
