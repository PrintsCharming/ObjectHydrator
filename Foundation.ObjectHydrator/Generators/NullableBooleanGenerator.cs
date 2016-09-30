using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableBooleanGenerator : IGenerator<bool?>
    {
        private readonly BooleanGenerator _innerGenerator;

        public NullableBooleanGenerator()
        {
            _innerGenerator = new BooleanGenerator();
        }

        public bool? Generate()
            => _innerGenerator.Generate();
    }
}