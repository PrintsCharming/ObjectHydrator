using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class ListGenerator<T>:IGenerator<IList<T>>
    {
        private readonly int listLength;

        public ListGenerator(int length)
        {
            listLength = length;
        }

        #region IGenerator<IList<T>> Members

        public IList<T> Generate()
        {
            IList<T> list = new List<T>();
            for (int i = 0; i < listLength; i++)
            {
                list.Add(new TypeGenerator<T>().Generate());
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
