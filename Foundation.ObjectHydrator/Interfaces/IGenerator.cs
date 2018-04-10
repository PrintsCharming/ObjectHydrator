namespace Foundation.ObjectHydrator.Interfaces
{
    public interface IGenerator
    {
        object Generate();
    }

    public interface IGenerator<out T>
    {
        T Generate();
    }
}