using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableGuidGenerator : IGenerator<Guid?>
    {
        private readonly GuidGenerator _innerGenerator;

        public NullableGuidGenerator()
        {
            _innerGenerator = new GuidGenerator();
        }

        public Guid? Generate()
            => _innerGenerator.Generate();
    }
}