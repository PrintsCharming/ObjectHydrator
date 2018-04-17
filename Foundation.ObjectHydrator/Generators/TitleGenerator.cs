using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class TitleGenerator : IGenerator<string>
    {
        private readonly FromListGetSingleGenerator<string> _candidates;

        public TitleGenerator(Func<ITitleOptionsBuilder, ITitleOptionsBuilder> optionBuilder = null)
        {
            var options = new TitleOptions();
            optionBuilder?.Invoke(options);

            var maleFactor = options.IncludeMaleTitles ? 1 : 0;
            var femaleFactor = options.IncludeFemaleTitles ? 1 : 0;

            _candidates = new FromListGetSingleGenerator<string>
            {
                { "Mr", 120 * maleFactor },
                { "Mrs", 110 * femaleFactor},
                { "Miss", 10 * femaleFactor},
                { "Dr", 3 * (maleFactor + femaleFactor)},
                { "Sir", maleFactor},
                { "Reverand", maleFactor+ femaleFactor},
                { "Lady", femaleFactor},
                { "Lord", maleFactor}
            };


        }

        public string Generate()
        {
            return _candidates.Generate();
        }
    }
}
