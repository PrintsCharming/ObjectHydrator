using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableIntegerGenerator : IGenerator<int?>
    {
        private readonly IntegerGenerator _innIntegerGenerator;
        public NullableIntegerGenerator()
            : this(0, 100)
        { }

        public NullableIntegerGenerator(int minimumValue, int maximumValue)
        {
            _innIntegerGenerator = new IntegerGenerator(minimumValue, maximumValue);
        }

        public int? Generate()
            => _innIntegerGenerator.Generate();
    }
}