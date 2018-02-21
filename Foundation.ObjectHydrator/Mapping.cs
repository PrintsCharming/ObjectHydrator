using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Reflection;

namespace Foundation.ObjectHydrator
{
    public class Mapping<T> : IMapping
    {
        public Mapping(PropertyInfo propertyInfo, IGenerator<T> generator)
        {
            PropertyName = propertyInfo.Name;
            PropertyInfo = propertyInfo;
            object[] a = propertyInfo.GetCustomAttributes(false);
            foreach (var item in a)
            {
                try
                {
                    System.Attribute attr = (System.Attribute)item;
                    //TODO: Refactor this out to be more flexible and support more annotations
                    if (attr.GetType() == typeof(System.ComponentModel.DataAnnotations.StringLengthAttribute))
                    {
                        System.ComponentModel.DataAnnotations.StringLengthAttribute sla = (System.ComponentModel.DataAnnotations.StringLengthAttribute)attr;
                        if (generator.GetType() == typeof(Generators.TextGenerator))
                        {
                            generator = (IGenerator<T>)new Generators.TextGenerator(sla.MaximumLength);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
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
        public Mapping(PropertyInfo propertyInfo, bool allowNulls)
        {
            PropertyName = propertyInfo.Name;
            PropertyInfo = propertyInfo;
            Generator = Generator = GetGenerator(propertyInfo, allowNulls);
        }

        private IGenerator<object> GetGenerator(PropertyInfo propertyInfo, bool allowNulls)
        {
            //Check if it is nullable Enum
            var isNullable = Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null;

            var propType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

            if (propType.IsEnum)
            {
                if (isNullable)
                {
                    return new NullableEnumGenerator(Enum.GetValues(propType), allowNulls);
                }

                return new EnumGenerator(Enum.GetValues(propType));
            }

            return new Generator(propertyInfo);
        }

        public string PropertyName { get; private set; }
        public PropertyInfo PropertyInfo { get; private set; }
        public IGenerator<object> Generator { get; private set; }

        public object Generate()
        {
            return Generator.Generate();
        }
    }
}
