using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Foundation.ObjectHydrator.Tests.HydratorTests {
	public static class TestUtilities {
		public static void DumpSimpleCustomer(Object theObject) {
			Trace.WriteLine("");
			foreach (PropertyInfo propertyInfo in theObject.GetType().GetProperties()) {
				Trace.WriteLine(String.Format("{0} [{1}]", propertyInfo.Name, propertyInfo.GetValue(theObject, null)));

				if (propertyInfo.PropertyType == typeof(byte[])) {
					var theArray = propertyInfo.GetValue(theObject, null) as byte[];
					if (theArray != null) {
						Trace.Write("  byte[] ");
						for (var i = 0; i < theArray.Length; i++) {
							Trace.Write(String.Format("[{0}]", theArray[i]));
						}
						Trace.WriteLine(String.Empty);
					}
				}
			}
		}

	}
}
