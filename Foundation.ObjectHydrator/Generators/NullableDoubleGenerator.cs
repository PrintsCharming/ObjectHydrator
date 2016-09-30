using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableDoubleGenerator : IGenerator<double?>
    {
        private readonly DoubleGenerator _innerGenerator;
        public NullableDoubleGenerator()
            : this(0.0, 100)
        { }

        public NullableDoubleGenerator(int decimalPlaces)
            :this(0.0,100.00,decimalPlaces)
        {

        }

        public NullableDoubleGenerator(double minimumValue, double maximumValue)
            : this(minimumValue, maximumValue, 2)
        { }


        public NullableDoubleGenerator(double minimumValue, double maximumValue, int decimalPlaces)
        {
            _innerGenerator = new DoubleGenerator(minimumValue, maximumValue, decimalPlaces);
        }

        public double? Generate()
            => _innerGenerator.Generate();
    }
}