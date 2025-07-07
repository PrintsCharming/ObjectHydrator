using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Reflection;

namespace Foundation.ObjectHydrator.Generators
{
    public class Generator : IGenerator
    {
        private readonly PropertyInfo _info;

        public Generator(PropertyInfo info)
        {
            _info = info;
        }
        #region Implementation of IGenerator

        public object Generate()
        {
            if (_info.PropertyType.IsArray)
            {
                return Array.CreateInstance(_info.PropertyType.GetElementType(), 0);
            }

            return Activator.CreateInstance(_info.PropertyType);
        }

        #endregion
    }
}
