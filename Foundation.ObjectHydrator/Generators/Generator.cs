using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Reflection;

namespace Foundation.ObjectHydrator.Generators
{
    public class Generator : IGenerator<object>
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
                var type = _info.PropertyType.GetElementType();

                if (CanConstruct(type))
                {
                    return Array.CreateInstance(type, 0);
                }
            }

            return CanConstruct(_info.PropertyType) ? Activator.CreateInstance(_info.PropertyType) : null;
        }

        #endregion



        private static bool CanConstruct(Type type)
        {
            return type != null && !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
