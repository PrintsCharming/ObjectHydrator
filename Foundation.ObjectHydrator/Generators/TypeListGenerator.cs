using System;
using System.Reflection;
using Foundation.ObjectHydrator.Interfaces;
using System.Collections.Generic;

namespace Foundation.ObjectHydrator.Generators
{
    public class TypeListGenerator : IGenerator
    {
        private readonly Type typeOfEntity;

        public TypeListGenerator(Type childObjectType, object[] parameters)
        {
            typeOfEntity = childObjectType;
            Parameters = parameters;
        }

        public Object ChildObject { get; private set; }
        public object[] Parameters { get; private set; }

        #region IGenerator Members

        public object Generate()
        {
            Object instance = Activator.CreateInstance(typeOfEntity);

            //this string scares me.
            Type hydratorType = Type.GetType("Foundation.ObjectHydrator.Hydrator`1").MakeGenericType(typeOfEntity);

            Object theHydrator = Activator.CreateInstance(hydratorType);

            MethodInfo methodInfo;
            if (Parameters.Length > 0)
            {
                methodInfo = hydratorType.GetMethod("GetFixedLengthList");
            }
            else
            {
               methodInfo = hydratorType.GetMethod("GetList");
            }

            try
            {
                instance = methodInfo.Invoke(theHydrator, Parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instance;
        }

        #endregion
    }
}