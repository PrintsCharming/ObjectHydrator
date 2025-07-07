using Foundation.ObjectHydrator.Interfaces;
using System;

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
