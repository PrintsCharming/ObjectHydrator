using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class ListGenerator<T>:IGenerator<IList<T>>
    {
        private readonly int listLength;
        private readonly IGenerator<T> elementGenerator;

        public ListGenerator(int length, IGenerator<T> elementGenerator = null)
        {
            this.elementGenerator =  elementGenerator ?? new TypeGenerator<T>();
            listLength = length;
        }

        #region IGenerator<IList<T>> Members

        public virtual IList<T> Generate()
        {
            IList<T> list = new List<T>();
            for (int i = 0; i < listLength; i++)
            {
                list.Add(elementGenerator.Generate());
            }
            return list;
        }

        #endregion

        public static ListGenerator<T> RandomLength()
        {
            return RandomLength(1, 10);
        }

        public static ListGenerator<T> RandomLength(int minimumValue, int maximumValue)
        {
            return new ListGenerator<T>(RandomSingleton.Instance.Random.Next(minimumValue, maximumValue));
        }
    }
}
