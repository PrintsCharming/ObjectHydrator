namespace Foundation.ObjectHydrator.Generators
{
    public interface ITitleOptionsBuilder
    {
        ITitleOptionsBuilder ExcludingMaleTitles();
        ITitleOptionsBuilder ExcludingFemaleTitles();
    }
}