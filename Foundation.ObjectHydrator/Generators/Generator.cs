using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;
using System.Reflection;

namespace Foundation.ObjectHydrator.Generators
{
    public class Generator:IGenerator
    {
        private readonly PropertyInfo _info;

        public Generator(PropertyInfo info)
        {
            _info = info;
        }
        #region Implementation of IGenerator

        public object Generate()
        {
            return Activator.CreateInstance(_info.PropertyType);
        }

        #endregion
    }
}
