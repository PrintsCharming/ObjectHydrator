using System;

namespace Foundation.ObjectHydrator.Interfaces
{
    [Obsolete]
    public interface IGenerator
    {
        object Generate();
    }

    public interface IGenerator<out T>
    {
        T Generate();
    }
}
