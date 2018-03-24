﻿using System;
using System.ComponentModel.DataAnnotations;
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
            var a = propertyInfo.GetCustomAttributes(false);
            foreach (var item in a)
            {
                try
                {
                    System.Attribute attr = (System.Attribute)item;
                    //TODO: Refactor this out to be more flexible and support more annotations
                    if (attr.GetType()==typeof(System.ComponentModel.DataAnnotations.StringLengthAttribute))
                    {
                        StringLengthAttribute sla = (System.ComponentModel.DataAnnotations.StringLengthAttribute)attr;
                        if (generator.GetType()==typeof(Generators.TextGenerator))
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
