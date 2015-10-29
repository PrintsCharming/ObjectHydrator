using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Foundation.ObjectHydrator {
	public class Hydrator : IGenerator {

		protected readonly Type typeOfT = null;
		protected readonly IDictionary<string, IMapping> propertyMap;
		protected readonly IList<IMap> typeMap;
		protected IList<IMap> defaultTypeMap;

		public Hydrator(Type t) : this(t, new DefaultTypeMap()) {
		}

		public Hydrator(Type t, IList<IMap> defaultMap) {
			typeOfT = t;
			propertyMap = new Dictionary<string, IMapping>();
			typeMap = new List<IMap>();
			defaultTypeMap = defaultMap;
		}

		public Random Random {
			get {
				return RandomSingleton.Instance.Random;
			}
		}

		public object Generate() {
			var instance = Activator.CreateInstance(typeOfT);
			Populate(instance);
			return instance;
		}

		public object GetSingle() {
			return Generate();
		}

		public IList GetList() {
			int length;
			length = Random.Next(1, 10);
			return GetList(length);
		}

		public IList GetList(int size) {
			if (size < 1) {
				throw new ArgumentOutOfRangeException("size", "size must be provided");
			}
			IList toReturn = new ArrayList();
			for (int i = 0; i < size; i++) {
				var instance = Generate();
				Populate(instance);
				toReturn.Add(instance);
			}
			return toReturn;
		}

		protected void Populate(object instance) {
			AddTypeMapToPropertyMap();
			foreach (IMapping mapping in propertyMap.Values) {
				PropertyInfo propertyInfo = instance.GetType().GetProperty(mapping.PropertyName, BindingFlags.Public | BindingFlags.Instance);


				if (propertyInfo != null) {
					propertyInfo.SetValue(instance, mapping.Generate(), null);
				}
			}
		}

		protected void AddTypeMapToPropertyMap() {
			AddDefaultTypeMapToTypeMap();

			foreach (PropertyInfo propertyInfo in typeOfT.GetProperties()) {
				if (propertyInfo.CanWrite && !propertyMap.ContainsKey(propertyInfo.Name)) {
					PropertyInfo info = propertyInfo;
					var map = typeMap.FirstOrDefault(infer => infer.Type == info.PropertyType && infer.Match(info));

					if (map != null) {
						propertyMap[propertyInfo.Name] = map.Mapping(propertyInfo);
					} else if (!propertyInfo.PropertyType.IsInterface) {
						propertyMap[propertyInfo.Name] = new Mapping(propertyInfo, new Generator(propertyInfo));
					}
				}
			}
		}

		protected void AddDefaultTypeMapToTypeMap() {
			foreach (var map in defaultTypeMap) {
				typeMap.Add(map);
			}
		}

		protected void SetPropertyMap<TProperty>(string name, IGenerator<TProperty> generator) {
			PropertyInfo pi = typeOfT.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
			if (pi == null) {
				throw new ArgumentException("The property cannot be found.", name);
			}
			if (!pi.CanWrite) {
				throw new ArgumentException("The property cannot be written.", name);
			}
			propertyMap[pi.Name] = new Mapping<TProperty>(pi, generator);
		}

	}
}
