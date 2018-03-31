using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class ListGenerator<T> : IGenerator<IList<T>>
    {
        private readonly IGenerator<T> _elementGenerator;
        private readonly int _listLength;

        public ListGenerator(int length, IGenerator<T> elementGenerator = null)
        {
            _elementGenerator = elementGenerator ?? new TypeGenerator<T>();
            _listLength = length;
        }

        #region IGenerator<IList<T>> Members

        public virtual IList<T> Generate()
        {
            IList<T> list = new List<T>();
            for (var i = 0; i < _listLength; i++)
                list.Add(_elementGenerator.Generate());
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