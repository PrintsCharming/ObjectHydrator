namespace Foundation.ObjectHydrator.Interfaces
{
    public interface IGenerator
    {
        object Generate();
    }

    public interface IGenerator<T>
    {
        T Generate();
    }
}