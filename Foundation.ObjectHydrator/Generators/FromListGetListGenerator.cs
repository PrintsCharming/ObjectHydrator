using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class FromListGetListGenerator<T> : IGenerator<IList<T>>
    {
        private readonly int _listLength;
        public IEnumerable<T> List;
        private readonly IList<T> _newList = new List<T>();

        public FromListGetListGenerator(IEnumerable<T> list, int count)
        {
            List = list;
            _listLength = count;
        }

        public IList<T> Generate()
        {
            for (var i = 0; i < _listLength; i++)
                if (i < List.Count())
                    _newList.Add(List.ElementAt(i));
            return _newList;
        }
    }
}