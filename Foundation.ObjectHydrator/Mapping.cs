using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator
{
    public class Mapping<T>:IMapping
    {
        public Mapping(PropertyInfo propertyInfo, IGenerator<T> generator)
        {
            PropertyName = propertyInfo.Name;
            PropertyInfo = propertyInfo;
            Generator = generator;
        }

        public string PropertyName { get; private set; }
        public PropertyInfo PropertyInfo { get; private set; }
        public IGenerator<T> Generator { get; private set; }

        public object Generate()
        {
            return Generator.Generate();
        }
    }

    public class Mapping : IMapping
    {
        public Mapping(PropertyInfo propertyInfo, IGenerator generator)
        {
            PropertyName = propertyInfo.Name;
            PropertyInfo = propertyInfo;
            Generator = generator;
        }

        public string PropertyName { get; private set; }
        public PropertyInfo PropertyInfo { get; private set; }
        public IGenerator Generator { get; private set; }

        public object Generate()
        {
            return Generator.Generate();
        }
    }
}
