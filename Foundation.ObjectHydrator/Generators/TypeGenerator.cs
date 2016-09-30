using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class TypeGenerator<T>:IGenerator<T>
    {
        public T Generate()
        {
            return new Hydrator<T>().GetSingle();
        }
    }
}
