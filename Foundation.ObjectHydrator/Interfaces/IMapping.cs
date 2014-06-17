using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Foundation.ObjectHydrator
{
    public interface IMapping
    {
        string PropertyName { get; }
        PropertyInfo PropertyInfo { get; }
        object Generate();
    }
}
