using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class DefaultGenerator<T> : IGenerator<T>
    {
        public DefaultGenerator(T defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public T DefaultValue { get; }

        public T Generate()
        {
            return DefaultValue;
        }
    }
}