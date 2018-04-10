using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator
{
    public class Mapping<T> : IMapping
    {
        public Mapping(PropertyInfo propertyInfo, IGenerator<T> generator)
        {
            PropertyName = propertyInfo.Name;
            PropertyInfo = propertyInfo;
            var a = propertyInfo.GetCustomAttributes(false);
            foreach (var item in a)
                try
                {
                    var attr = (Attribute) item;
                    //TODO: Refactor this out to be more flexible and support more annotations
                    if (attr.GetType() == typeof(StringLengthAttribute))
                    {
                        var sla = (StringLengthAttribute) attr;
                        if (generator.GetType() == typeof(TextGenerator))
                            generator = (IGenerator<T>) new TextGenerator(sla.MaximumLength);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            Generator = generator;
        }

        public IGenerator<T> Generator { get; }

        public string PropertyName { get; }
        public PropertyInfo PropertyInfo { get; }

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

        public IGenerator Generator { get; }

        public string PropertyName { get; }
        public PropertyInfo PropertyInfo { get; }

        public object Generate()
        {
            return Generator.Generate();
        }
    }
}