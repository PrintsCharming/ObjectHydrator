using System;
using System.Reflection;
using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator
{
    public class EnumMap : IMap
    {
        #region IMap Members

        Type IMap.Type => typeof(object);

        bool IMap.Match(PropertyInfo info)
        {
            return info.PropertyType.IsEnum;
        }

        IMapping IMap.Mapping(PropertyInfo info)
        {
            return new Mapping<object>(info, new EnumGenerator(Enum.GetValues(info.PropertyType)));
        }

        #endregion
    }
}