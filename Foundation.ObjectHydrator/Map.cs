using System;
using Foundation.ObjectHydrator.Interfaces;
using Foundation.ObjectHydrator.Generators;
using System.Reflection;

namespace Foundation.ObjectHydrator
{
    public class Map<T>:IMap
    {
        private Func<PropertyInfo, bool> _func;
        private IGenerator<T> _generator;

        public Map()
        {
            _func = info => info.CanWrite;
        }

        Type IMap.Type
        {
            get
            {
                return typeof(T);
            }
        }

        bool IMap.Match(PropertyInfo info)
        {
            return _func(info);
        }

        IMapping IMap.Mapping(PropertyInfo info)
        {
            return new Mapping<T>(info, _generator);
        }

        public Map<T> Matching(Func<PropertyInfo, bool> func)
        {
            _func = func;
            return this;
        }

        public Map<T> Using(IGenerator<T> generator)
        {
            _generator = generator;
            return this;
        }

        public Map<T> Using(T defaultValue)
        {
            _generator = new DefaultGenerator<T>(defaultValue);
            return this;
        }

    }
}
