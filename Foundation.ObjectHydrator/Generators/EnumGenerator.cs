using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class EnumGenerator<T> : IGenerator<T>
    {
        Random random;
        Array EnumValues;

        public EnumGenerator(Array enumValues)
        {
            EnumValues = enumValues;
            random = RandomSingleton.Instance.Random;

        }

        public T Generate()
        {
            return (T)EnumValues.GetValue(random.Next(0, EnumValues.Length - 1));
        }
    }
}
