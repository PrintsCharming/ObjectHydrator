﻿using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace Foundation.ObjectHydrator
{
    public class Hydrator<T> : IGenerator<T>
    {
        readonly Type typeOfT = null;
        readonly IDictionary<string, IMapping> propertyMap;
        private readonly IList<IMap> typeMap;
        private IList<IMap> defaultTypeMap;
        private readonly bool _allowNulls;

        #region Ctors

        public Hydrator(bool allowNulls = true)
            : this(new DefaultTypeMap(allowNulls))
        {
            _allowNulls = allowNulls;
        }

        public Hydrator(IList<IMap> defaultMap)
        {
            typeOfT = typeof(T);
            propertyMap = new Dictionary<string, IMapping>();
            typeMap = new List<IMap>();
            defaultTypeMap = defaultMap;
        }
        #endregion

        public static T Hydrate()
        {
            return new Hydrator<T>().Generate();
        }

        /// <summary>
        /// Getter for the Random being utilized by the Hydrator.
        /// This Random may be used by external IGenerators to ensure good random results.
        /// </summary>
        public Random Random
        {
            get
            {
                return RandomSingleton.Instance.Random;
            }
        }

        /// <summary>
        /// GetSingle returns a single instance of T populated with default, random data.
        /// </summary>
        /// <returns>An instance of Type T.</returns>
        public T GetSingle()
        {
            return Generate();
        }

        #region IGenerator<T> Members

        public T Generate()
        {
            var instance = (T)Activator.CreateInstance(typeOfT);
            Populate(instance);
            return instance;
        }

        #endregion

        /// <summary>
        /// Returns a generic IList of type T of a random length 1-10 with default, random data
        /// </summary>
        /// <returns></returns>
        public IList<T> GetList()
        {
            int length;

            length = Random.Next(1, 10);

            return GetList(length);
        }

        /// <summary>
        /// Returns a generic IList of type T populated with default, random data.
        /// </summary>
        /// <param name="size">The length of the IList to return.</param>
        /// <returns>A generic IList of Type T containing populated entities.</returns>
        public IList<T> GetList(int size)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException("size", "size must be provided");
            }

            IList<T> toReturn = new List<T>();

            for (int i = 0; i < size; i++)
            {
                T instance = Generate();
                Populate(instance);
                toReturn.Add(instance);
            }

            return toReturn;
        }

        private void SetPropertyMap<TProperty>(Expression<Func<T, TProperty>> expression, IGenerator<TProperty> generator)
        {
            var propertyName = ((MemberExpression)expression.Body).Member.Name;
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // Check to see if we have this property...
            if (propertyInfo == null)
            {
                throw new ArgumentException("The Property can not be found.", propertyName);
            }

            if (!propertyInfo.CanWrite)
            {
                throw new ArgumentException("The Property can not be written.", propertyName);
            }

            propertyMap[propertyInfo.Name] = new Mapping<TProperty>(propertyInfo, generator);
        }

        #region WithTypes
        /// <summary>
        /// Applies a provided default value for the provided Property expression.
        /// </summary>
        /// <param name="expression">The Property expression to use</param>
        /// <param name="defaultValue">The value to set the Property to.</param>
        /// <returns>This instance of the Hydrator for type T.</returns>
        public Hydrator<T> With<TProperty>(Expression<Func<T, TProperty>> expression, TProperty defaultValue)
        {
            SetPropertyMap(expression, new DefaultGenerator<TProperty>(defaultValue));
            return this;
        }

        /// <summary>
        /// Applies a generator generated value for the provided Property expression.
        /// </summary>
        /// <param name="expression">The Property expression to us</param>
        /// <param name="generator">The <see cref="IGenerator{T}">Generator</see> to use</param>
        /// <returns>This instance of the Hydrator for type T.</returns>
        public Hydrator<T> With<TProperty>(Expression<Func<T, TProperty>> expression, IGenerator<TProperty> generator)
        {
            SetPropertyMap(expression, generator);
            return this;
        }

        public Hydrator<T> WithInteger<TProperty>(Expression<Func<T, TProperty>> expression, int minimum, int maximum)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new IntegerGenerator(minimum, maximum);
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithGuid<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new GuidGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithCustomGenerator<TProperty>(Expression<Func<T, TProperty>> expression, IGenerator<TProperty> customgenerator)
        {
            SetPropertyMap(expression, customgenerator);
            return this;
        }


        public Hydrator<T> WithDate<TProperty>(Expression<Func<T, TProperty>> expression, DateTime minimum, DateTime maximum)
        {

            IGenerator<TProperty> gen = (IGenerator<TProperty>)new DateTimeGenerator(minimum, maximum);
            SetPropertyMap(expression, gen);

            return this;
        }

        public Hydrator<T> WithDouble<TProperty>(Expression<Func<T, TProperty>> expression, int decimalPlaces)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new DoubleGenerator(decimalPlaces);
            SetPropertyMap(expression, gen);
            return this;
        }
        public Hydrator<T> WithDouble<TProperty>(Expression<Func<T, TProperty>> expression, double minimum, double maximum)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new DoubleGenerator(minimum, maximum);
            SetPropertyMap(expression, gen);
            return this;
        }


        public Hydrator<T> WithDouble<TProperty>(Expression<Func<T, TProperty>> expression, double minimum, double maximum, int decimalPlaces)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new DoubleGenerator(minimum, maximum, decimalPlaces);
            SetPropertyMap(expression, gen);

            return this;
        }

        /// <summary>
        /// Applies a random selection from the provided enumValues for the provided Property Name.
        /// </summary>
        /// <param name="Expression">The Property to apply the randomly selected Enum to.</param>
        /// <param name="enumValues">The Array of System.Enum values from which to chose a random entry to return.</param>
        /// <returns>This instance of the Hydrator for type T.</returns>
        public Hydrator<T> WithEnum<TProperty>(Expression<Func<T, TProperty>> expression, Array enumValues)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new EnumGenerator(enumValues);
            SetPropertyMap(expression, gen);
            return this;

        }
        /// <summary>
        /// Applies a randomly selected byte[] to the provided Property Name of the specified length.
        /// </summary>
        /// <param name="expression">The Property to apply the randomly generated byte array to.</param>
        /// <param name="length">The length of the byte[].</param>
        /// <returns>This instance of the Hydrator for type T.</returns>
        public Hydrator<T> WithByteArray<TProperty>(Expression<Func<T, TProperty>> expression, int length)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new ByteArrayGenerator(length);
            SetPropertyMap(expression, gen);
            return this;

        }

        public Hydrator<T> WithDecimal<TProperty>(Expression<Func<T, TProperty>> expression, int decimalPlaces)
        {
            var gen = (IGenerator<TProperty>)new DecimalGenerator(decimalPlaces);
            SetPropertyMap(expression, gen);
            return this;
        }
        public Hydrator<T> WithDecimal<TProperty>(Expression<Func<T, TProperty>> expression, decimal minimum, decimal maximum)
        {
            var gen = (IGenerator<TProperty>)new DecimalGenerator(minimum, maximum);
            SetPropertyMap(expression, gen);
            return this;
        }


        public Hydrator<T> WithDecimal<TProperty>(Expression<Func<T, TProperty>> expression, decimal minimum, decimal maximum, int decimalPlaces)
        {
            var gen = (IGenerator<TProperty>)new DecimalGenerator(minimum, maximum, decimalPlaces);
            SetPropertyMap(expression, gen);

            return this;
        }


        #region WithCustomGeneratorTypes

        public Hydrator<T> WithAmericanAddress<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new AmericanAddressGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithAmericanCity<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new AmericanCityGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithAmericanPhone<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new AmericanPhoneGenerator();
            SetPropertyMap(expression, gen);

            return this;

        }

        /// <summary>
        /// Generates an American Postal Code with optional percentage with PLUS four.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <param name="percentageWithPlusFour">Percentage generated that should have plus four</param>
        /// <returns></returns>
        public Hydrator<T> WithAmericanPostalCode<TProperty>(Expression<Func<T, TProperty>> expression, int percentageWithPlusFour)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new AmericanPostalCodeGenerator(percentageWithPlusFour);
            SetPropertyMap(expression, gen);

            return this;

        }

        public Hydrator<T> WithAmericanState<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new AmericanStateGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithUnitedKingdomCity<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new UnitedKingdomCityGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithUnitedKingdomCounty<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new UnitedKingdomCountyGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithUnitedKingdomPostCode<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new UnitedKingdomPostCodeGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        /// <summary>
        /// Returns a CCV based on the type. {Presently just returns a 000-999 ignoring the type}
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="ccvtype"></param>
        /// <returns></returns>
        public Hydrator<T> WithCCV<TProperty>(Expression<Func<T, TProperty>> expression, string ccvtype)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new CCVGenerator(ccvtype);
            SetPropertyMap(expression, gen);

            return this;

        }

        public Hydrator<T> WithCompanyName<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new CompanyNameGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        /// <summary>
        /// Returns a number resembling a credit card.  The layout is valid but it will not pass validity checking.
        /// </summary>
        /// <param name="expression">The Property to apply the generated credit card number to.</param>
        /// <param name="length">Number of digits you want to simulate.</param>
        /// <returns></returns>
        public Hydrator<T> WithCreditCardNumber<TProperty>(Expression<Func<T, TProperty>> expression, int length)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new CreditCardNumberGenerator(length);
            SetPropertyMap(expression, gen);

            return this;

        }

        public Hydrator<T> WithCreditCardType<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new CreditCardTypeGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithEmailAddress<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new EmailAddressGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithFirstName<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new FirstNameGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }



        /// <summary>
        /// Applies a randomly selected Gender to the provided Property Name.
        /// </summary>
        /// <param name="expression">The Property to apply the randomly selected Gender to.</param>
        /// <returns>This instance of the Hydrator for type T.</returns>
        public Hydrator<T> WithGender<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new GenderGenerator();
            SetPropertyMap(expression, gen);
            return this;

        }

        /// <summary>
        /// Applies a randomly selected IP Address to the provided Property Name.
        /// </summary>
        /// <param name="expression">The Property to apply the randomly generated IP Address to.</param>
        /// <returns>This instance of the Hydrator for type T.</returns>
        public Hydrator<T> WithIPAddress<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new IPAddressGenerator();
            SetPropertyMap(expression, gen);

            return this;

        }

        public Hydrator<T> WithLastName<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new LastNameGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithPassword<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new PasswordGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithPassword<TProperty>(Expression<Func<T, TProperty>> expression, int length)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new PasswordGenerator(length);
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithText<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new TextGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithText<TProperty>(Expression<Func<T, TProperty>> expression, int length)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new TextGenerator(length);
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithTrackingNumber<TProperty>(Expression<Func<T, TProperty>> expression, string carrier)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new TrackingNumberGenerator(carrier);
            SetPropertyMap(expression, gen);
            return this;
        }


        public Hydrator<T> WithWebsite<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new WebsiteGenerator();
            SetPropertyMap(expression, gen);
            return this;
        }

        /// <summary>
        /// Applies a random alphanumeric string of a specified length
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public Hydrator<T> WithAlphaNumeric<TProperty>(Expression<Func<T, TProperty>> expression, int length)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new AlphaNumericGenerator(length);
            SetPropertyMap(expression, gen);
            return this;

        }




        #endregion

        //Not working.
        public Hydrator<T> WithListRandomLength<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)ListGenerator<TProperty>.RandomLength();
            SetPropertyMap(expression, gen);
            return this;
        }

        #endregion

        #region WithNullableTypes
        public Hydrator<T> WithNullableInteger<TProperty>(Expression<Func<T, TProperty>> expression, int minimum, int maximum, bool allowNulls = true)
        {
            var gen = (IGenerator<TProperty>)new NullableIntegerGenerator(minimum, maximum, allowNulls);
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithNullableGuid<TProperty>(Expression<Func<T, TProperty>> expression, bool allowNulls = true)
        {
            var gen = (IGenerator<TProperty>)new NullableGuidGenerator(allowNulls);
            SetPropertyMap(expression, gen);
            return this;
        }

        public Hydrator<T> WithNullableDate<TProperty>(Expression<Func<T, TProperty>> expression, DateTime minimum, DateTime maximum, bool allowNulls = true)
        {

            var gen = (IGenerator<TProperty>)new NullableDateTimeGenerator(minimum, maximum, allowNulls);
            SetPropertyMap(expression, gen);

            return this;
        }

        public Hydrator<T> WithNullableDouble<TProperty>(Expression<Func<T, TProperty>> expression, int decimalPlaces, bool allowNulls = true)
        {
            var gen = (IGenerator<TProperty>)new NullableDoubleGenerator(decimalPlaces, allowNulls);
            SetPropertyMap(expression, gen);
            return this;
        }
        public Hydrator<T> WithNullableDouble<TProperty>(Expression<Func<T, TProperty>> expression, double minimum, double maximum, bool allowNulls = true)
        {
            var gen = (IGenerator<TProperty>)new NullableDoubleGenerator(minimum, maximum, allowNulls);
            SetPropertyMap(expression, gen);
            return this;
        }


        public Hydrator<T> WithNullableDouble<TProperty>(Expression<Func<T, TProperty>> expression, double minimum, double maximum, int decimalPlaces, bool allowNulls = true)
        {
            var gen = (IGenerator<TProperty>)new NullableDoubleGenerator(minimum, maximum, decimalPlaces, allowNulls);
            SetPropertyMap(expression, gen);

            return this;
        }

        public Hydrator<T> WithNullableDecimal<TProperty>(Expression<Func<T, TProperty>> expression, int decimalPlaces, bool allowNulls = true)
        {
            var gen = (IGenerator<TProperty>)new NullableDecimalGenerator(decimalPlaces, allowNulls);
            SetPropertyMap(expression, gen);
            return this;
        }
        public Hydrator<T> WithNullableDecimal<TProperty>(Expression<Func<T, TProperty>> expression, decimal minimum, decimal maximum, bool allowNulls = true)
        {
            var gen = (IGenerator<TProperty>)new NullableDecimalGenerator(minimum, maximum, allowNulls);
            SetPropertyMap(expression, gen);
            return this;
        }


        public Hydrator<T> WithNullableDecimal<TProperty>(Expression<Func<T, TProperty>> expression, decimal minimum, decimal maximum, int decimalPlaces, bool allowNulls = true)
        {
            var gen = (IGenerator<TProperty>)new NullableDecimalGenerator(minimum, maximum, decimalPlaces, allowNulls);
            SetPropertyMap(expression, gen);

            return this;
        }

        public Hydrator<T> WithNullableEnum<TProperty>(Expression<Func<T, TProperty>> expression, Array enumValues, bool allowNulls = true)
        {
            var gen = (IGenerator<TProperty>)new NullableEnumGenerator(enumValues, allowNulls);
            SetPropertyMap(expression, gen);
            return this;

        }
        #endregion

        /// <summary>
        /// Applies a random selection from the passed list to the provided Property Name.
        /// </summary>
        /// <param name="propertyName">The Property to apply the randomly selected List Item to.</param>
        /// <param name="list">The IEnumerable<![CDATA[<Object>]]> to randomly choose a value from.</param>
        /// <returns>This instance of the Hydrator for type T.</returns>
        public Hydrator<T> FromList<TProperty>(Expression<Func<T, TProperty>> expression, IEnumerable<TProperty> list)
        {
            IGenerator<TProperty> gen = (IGenerator<TProperty>)new FromListGetSingleGenerator<TProperty>(list);
            SetPropertyMap(expression, gen);
            return this;
        }

        /// <summary>
        /// Provides an empty value for the specified Property expression.
        /// </summary>
        /// <returns>This instance of the Hydrator for type T.</returns>
        public Hydrator<T> Ignoring<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var propertyName = ((MemberExpression)expression.Body).Member.Name;
            PropertyInfo propertyInfo1 = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // Check to see if we have this property...
            if (propertyInfo1 == null)
            {
                throw new ArgumentException("The Property can not be found.", propertyName);
            }

            if (!propertyInfo1.CanWrite)
            {
                throw new ArgumentException("The Property can not be written.", propertyName);
            }
            PropertyInfo propertyInfo = propertyInfo1;

            propertyMap[propertyInfo.Name] = new Mapping<object>(propertyInfo, new NullGenerator());

            return this;
        }

        public Hydrator<T> ForAll<TType>(IGenerator<TType> generator)
        {
            typeMap.Add(new Map<TType>().Using(generator));
            return this;
        }

        public Hydrator<T> For<TType>(Map<TType> map)
        {
            typeMap.Add(map);
            return this;
        }

        private void Populate(object instance)
        {
            AddTypeMapToPropertyMap();
            foreach (IMapping mapping in propertyMap.Values)
            {
                PropertyInfo propertyInfo = instance.GetType().GetProperty(mapping.PropertyName, BindingFlags.Public | BindingFlags.Instance);


                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(instance, mapping.Generate(), null);
                }
            }
        }

        private void AddTypeMapToPropertyMap()
        {
            AddDefaultTypeMapToTypeMap();

            foreach (PropertyInfo propertyInfo in typeOfT.GetProperties())
            {
                if (propertyInfo.CanWrite && !propertyMap.ContainsKey(propertyInfo.Name))
                {
                    PropertyInfo info = propertyInfo;
                    var map = typeMap.FirstOrDefault(infer => infer.Type == info.PropertyType && infer.Match(info));

                    if (map != null)
                    {
                        propertyMap[propertyInfo.Name] = map.Mapping(propertyInfo);
                    }
                    else if (!propertyInfo.PropertyType.IsInterface)
                    {
                        propertyMap[propertyInfo.Name] = new Mapping(propertyInfo, _allowNulls);
                    }
                }
            }
        }

        private void AddDefaultTypeMapToTypeMap()
        {
            foreach (var map in defaultTypeMap)
            {
                typeMap.Add(map);
            }
        }

    }

}
