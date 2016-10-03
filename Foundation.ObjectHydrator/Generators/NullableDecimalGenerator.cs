using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableDecimalGenerator : IGenerator<decimal?>
    {
        private readonly DecimalGenerator _innerGenerator;

        public NullableDecimalGenerator()
            : this(0, 100)
        { }

        public NullableDecimalGenerator(int decimalPlaces)
            : this(0,100,decimalPlaces)
        { }

        public NullableDecimalGenerator(decimal minimumValue, decimal maximumValue)
            : this(minimumValue, maximumValue, 2)
        { }

        public NullableDecimalGenerator(decimal minimumValue, decimal maximumValue, int decimalPlaces)
        {
            _innerGenerator = new DecimalGenerator(minimumValue, maximumValue, decimalPlaces);
        }

        public decimal? Generate()
            => _innerGenerator.Generate();
    }
}