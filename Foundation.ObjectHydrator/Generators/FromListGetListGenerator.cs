using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class FromListGetListGenerator<T>:IGenerator<IList<T>>
    {
        private readonly int listLength;
        public IEnumerable<T> list = new List<T>();
        private IList<T> newList = new List<T>();
        public FromListGetListGenerator(IEnumerable<T> list, int count)
        {
            this.list = list;
            listLength = count;
        }

        public IList<T> Generate()
        {
            for (int i = 0; i < listLength; i++)
            {
                if (i < list.Count())
                {
                    newList.Add(list.ElementAt(i));
                }
            }
            return newList;
        }
        
    }
}
