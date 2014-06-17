using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Foundation.ObjectHydrator.Interfaces
{
    public interface IMap
    {
        Type Type { get; }
        bool Match(PropertyInfo info);
        IMapping Mapping(PropertyInfo info);
    }
}
