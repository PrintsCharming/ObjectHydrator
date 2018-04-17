namespace Foundation.ObjectHydrator.Generators
{
    class TitleOptions : ITitleOptionsBuilder
    {
        public TitleOptions()
        {
            IncludeMaleTitles = true;
            IncludeFemaleTitles = true;
        }

        public bool IncludeMaleTitles { get; private set; }
        public bool IncludeFemaleTitles { get; private set; }

        ITitleOptionsBuilder ITitleOptionsBuilder.ExcludingMaleTitles()
        {
            IncludeMaleTitles = false;
            return this;
        }

        ITitleOptionsBuilder ITitleOptionsBuilder.ExcludingFemaleTitles()
        {
            IncludeFemaleTitles = false;
            return this;
        }
    }
}