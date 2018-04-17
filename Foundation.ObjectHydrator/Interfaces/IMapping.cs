using System.Reflection;

namespace Foundation.ObjectHydrator.Interfaces
{
    public interface IMapping
    {
        string PropertyName { get; }
        PropertyInfo PropertyInfo { get; }
        object Generate();
    }
}