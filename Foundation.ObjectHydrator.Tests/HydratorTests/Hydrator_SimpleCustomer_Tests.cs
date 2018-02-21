using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class Hydrator_SimpleCustomer_Tests
    {
        [Test]
        public void CanGetSingleSimpleCustomer()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();

            Assert.IsTrue(!String.IsNullOrEmpty(customer.Description), "Customer Description should exist.");

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanGetSingleRestrictedDescriptionCustomer()
        {
            var hydrator = new Hydrator<RestrictedDescriptionCustomer>();
            var customer = hydrator.GetSingle();

            Assert.IsTrue(!String.IsNullOrEmpty(customer.Description), "Customer Description should exist.");
            Assert.IsTrue(customer.Description.Length <= 5, "Length not restricted");
            DumpSimpleCustomer(customer);
        }


        [Test]
        public void CanGetList()
        {
            var listCount = 50;
            var hydrator = new Hydrator<SimpleCustomer>();
            var customers = hydrator.GetList(listCount);

            Assert.IsTrue(customers.Count == listCount, "Customer count is wrong.");

            //DumpCustomers(customers);
        }

        [Test]
        public void CanGetDescription()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();

            Assert.IsTrue(!String.IsNullOrEmpty(customer.Description), "Customer Description should exist.");

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanConstrainIntegers()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithInteger(x => x.Locations, 5, 10);

            var customer = hydrator.GetSingle();

            Assert.IsTrue(customer.Locations >= 5 && customer.Locations <= 10,
                String.Format("Customer Locations [{0}] is outside expected range.", customer.Locations));

            DumpSimpleCustomer(customer);
        }


        [Test]
        public void CanDefaultString()
        {
            var defaultValue = "Testing123";

            var hydrator = new Hydrator<SimpleCustomer>()
                .With(x => x.Description, defaultValue);

            var customer = hydrator.GetSingle();

            Assert.IsTrue(defaultValue == customer.Description, String.Format("Default value is not as expected[{0}]", defaultValue));

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanOverrideGenerator()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .With(x => x.FirstName, "Bob");
            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.FirstName == "Bob");
        }

        [Test]
        public void CanGetInteger()
        {
            var hydrator = new Hydrator<SimpleCustomer>();

            var customer = hydrator.GetSingle();

            Assert.IsTrue(customer.Locations >= 0, String.Format("Customer Locations is expected."));

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanGetDouble()
        {
            var hydrator = new Hydrator<SimpleCustomer>();

            var customer = hydrator.GetSingle();

            Assert.IsTrue(customer.Revenue >= 0, String.Format("Customer Revenue is expected."));

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanConstrainDoubleDecimalPlaces()
        {
            var decimalPlaces = 3;

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithDouble(x => x.Revenue, decimalPlaces);
            var customer = hydrator.GetSingle();

            var decimalPart = customer.Revenue - (int)customer.Revenue;
            Assert.IsTrue(decimalPart >= 0, String.Format("Customer Revenue decimal part is expected."));

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanConstrainDoubleRange()
        {
            var minimum = 15.76;
            var maximum = 76.43;

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithDouble(x => x.Revenue, minimum, maximum);

            var customer = hydrator.GetSingle();
            Assert.That(customer.Revenue, Is.InRange(minimum, maximum));
            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanConstrainDoubleRangeAndDecimals()
        {
            var minimum = 15.76;
            var maximum = 76.43;
            var decimalPlaces = 3;

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithDouble(x => x.Revenue, minimum, maximum, decimalPlaces);

            var customer = hydrator.GetSingle();
            var decimalPart = customer.Revenue - (int)customer.Revenue;

            Assert.That(customer.Revenue, Is.InRange(minimum, maximum));
            Assert.IsTrue(decimalPart >= 0, String.Format("Customer Revenue decimal part is expected."));

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanDefaultInteger()
        {
            var defaultValue = 73;

            var hydrator = new Hydrator<SimpleCustomer>()
                .With(x => x.Locations, defaultValue);

            var customer = hydrator.GetSingle();

            Assert.IsTrue(defaultValue == customer.Locations, String.Format("Default value is not as expected[{0}]", defaultValue));

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanDefaultDateTime()
        {
            var defaultValue = new DateTime(2009, 01, 01);

            var hydrator = new Hydrator<SimpleCustomer>()
                .With(x => x.IncorporatedOn, defaultValue);


            var customer = hydrator.GetSingle();

            Assert.IsTrue(defaultValue == customer.IncorporatedOn, String.Format("Default value is not as expected[{0}]", defaultValue));

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanChainWithDefaultDescription()
        {
            var defaultValue = "Testing123";
            var minimumValue = 65;
            var maximumValue = 75;

            var hydrator = new Hydrator<SimpleCustomer>()
                .With(x => x.Description, defaultValue)
                .With(x => x.Locations, new IntegerGenerator(minimumValue, maximumValue));

            var customer = hydrator.GetSingle();

            Assert.IsTrue(defaultValue == customer.Description, String.Format("Default value is not as expected[{0}]", defaultValue));

            Assert.That(customer.Locations, Is.InRange(minimumValue, maximumValue),
                String.Format("Customer Locations [{0}] is outside expected range [{1},{2}].", customer.Locations, minimumValue,
                    maximumValue));
            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanConstrainWithDates()
        {
            var minimumValue = new DateTime(2009, 01, 01);
            var maximumValue = new DateTime(2009, 01, 10);

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithDate(x => x.IncorporatedOn, minimumValue, maximumValue);

            var customer = hydrator.GetSingle();


            Assert.That(customer.IncorporatedOn, Is.InRange(minimumValue, maximumValue),
                String.Format("Customer IncorporatedOn [{0}] is outside expected range [{1}, {2}].", customer.IncorporatedOn,
                    minimumValue, maximumValue));
            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanConstrainDates()
        {
            var minimumValue = new DateTime(2009, 01, 01);
            var maximumValue = new DateTime(2009, 01, 10);

            var hydrator = new Hydrator<SimpleCustomer>()
                .With(x => x.IncorporatedOn, new DateTimeGenerator(minimumValue, maximumValue));

            var customer = hydrator.GetSingle();


            Assert.That(customer.IncorporatedOn, Is.InRange(minimumValue, maximumValue),
                String.Format("Customer IncorporatedOn [{0}] is outside expected range [{1}, {2}].", customer.IncorporatedOn,
                    minimumValue, maximumValue));
            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanGetByteArray()
        {
            var length = 10;
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithByteArray(x => x.Version, length);

            var customer = hydrator.GetSingle();

            Assert.IsTrue(customer.Version.Length == length, String.Format("Customer Version Length is expected to be {0}.", length));

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanGetCreditCardNumber()
        {
            var length = 16;

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithCreditCardNumber(x => x.CreditCardNumber, length);

            var customer = hydrator.GetSingle();

            Assert.IsNotEmpty(customer.CreditCardNumber, String.Format("Credit Card Number is expected."));
            Assert.IsTrue(customer.CreditCardNumber.Length == length,
                String.Format("Credit Card Number [{0}] should be {1} long.", customer.CreditCardNumber,
                    length));

            DumpSimpleCustomer(customer);
        }

        private bool IsWebsiteAddressValid(string webaddy)
        {
            var testpattern = new Regex("((mailto\\:|(news|(ht|f)tp(s?))\\://){1}\\S+)");
            return testpattern.IsMatch(webaddy);
        }

        [Test]
        public void TestWebsiteAddress()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.homepage);
            Assert.IsTrue(IsWebsiteAddressValid(customer.homepage));
        }

        public bool IsValidIPAddress(string ipaddress)
        {
            var testpattern =
                new Regex(
                    "^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
            return testpattern.IsMatch(ipaddress);
        }

        [Test]
        public void IPAddressTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.ipaddress);
            Assert.IsTrue(IsValidIPAddress(customer.ipaddress));
        }

        [Test]
        public void WithIPAddressTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithIPAddress(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
            Assert.IsTrue(IsValidIPAddress(customer.placeholderstring));
        }

        [Test]
        public void GenderTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.gender);
        }

        [Test]
        public void WithGenderTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithGender(x => x.placeholderstring);

            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
            Assert.IsTrue(customer.placeholderstring.ToLower().Contains("male") ||
                          customer.placeholderstring.ToLower().Contains("female"));
        }

        [Test]
        public void CreditCardTypeTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.creditcardtype);
        }

        [Test]
        public void CountryCodeTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Country);
            Assert.IsTrue(customer.Country.Length == 2);
        }

        private bool IsEmailAddressValid(string emailaddress)
        {
            var emailpattern =
                new Regex(
                    "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
            return emailpattern.IsMatch(emailaddress);
        }

        [Test]
        public void EmailAddressTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.EmailAddress);
            Assert.IsTrue(IsEmailAddressValid(customer.EmailAddress));
        }

        [Test]
        public void BooleanGenerator()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.IsActive);
            Assert.IsInstanceOf<bool>(customer.IsActive);

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanGetTrackingNumberByInference()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.TrackingNumber);
        }

        [Test]
        public void CanGetTrackingNumberBySpecification()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithTrackingNumber(x => x.TrackingNumber, "usps");
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.TrackingNumber);
        }

        [Test]
        public void CanGetCCV()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithCCV(x => x.CCV, "visa");
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.CCV);
            Assert.IsTrue(customer.CCV.Length == 3);
        }

        private bool CheckPhone(string phonetocheck)
        {
            var phonepattern =
                new Regex(
                    "^[\\(]{0,1}([0-9]){3}[\\)]{0,1}[ ]?([^0-1]){1}([0-9]){2}[ ]?[-]?[ ]?([0-9]){4}[ ]*((x){0,1}([0-9]){1,5}){0,1}$");
            var boolval = phonepattern.IsMatch(phonetocheck);
            return boolval;
        }

        [Test]
        public void WithPhoneTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAmericanPhone(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
            Assert.IsTrue(CheckPhone(customer.placeholderstring));
        }

        private bool IsAmericanPostalCodeValid(string postalcode)
        {
            var postalcodepattern = new Regex("^\\d{5}$|^\\d{5}-\\d{4}$");
            return postalcodepattern.IsMatch(postalcode);
        }

        [Test]
        public void WithPostalCodeTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAmericanPostalCode(x => x.placeholderstring, 1);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
            Assert.IsTrue(IsAmericanPostalCodeValid(customer.placeholderstring));
        }

        [Test]
        public void FromListTest()
        {
            IList<string> mylist = new List<string>() { "red", "green", "blue", "orange" };
            var hydrator = new Hydrator<SimpleCustomer>()
                .FromList(x => x.placeholderstring, mylist);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
            Assert.IsTrue(mylist.Contains(customer.placeholderstring));
        }

        [Test]
        public void WithAmericanAddressTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAmericanAddress(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithAmericanCityTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAmericanCity(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithAmericanState()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAmericanState(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithCompanyName()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithCompanyName(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithCreditCardType()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithCreditCardType(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithEmailAddress()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithEmailAddress(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithFirstName()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithFirstName(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithLastName()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithLastName(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithWebsite()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithWebsite(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void PasswordWithInferenceDefaultLength()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Password);
            Assert.AreEqual(10, customer.Password.Length);
        }

        [Test]
        public void PasswordUsingWithAndDefaultLength()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithPassword(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
            Assert.AreEqual(10, customer.placeholderstring.Length);
        }

        [Test]
        public void PasswordUsingWithAndCustomLength()
        {
            var pwlen = 15;
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithPassword(x => x.placeholderstring, pwlen);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
            Assert.AreEqual(pwlen, customer.placeholderstring.Length);
        }

        [Test]
        public void AlphaNumericWithLength()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAlphaNumeric(x => x.placeholderstring, 10);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
            Assert.AreEqual(10, customer.placeholderstring.Length);
        }

        [Test]
        public void WithUnitedKingdomCityTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithUnitedKingdomCity(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithUnitedKingdomCountyTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithUnitedKingdomCounty(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void WithUnitedKingdomPostCodeTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithUnitedKingdomPostCode(x => x.placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.placeholderstring);
        }

        [Test]
        public void CanGetNullableFields()
        {
            var hydrator = new Hydrator<SimpleCustomer>(false);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.RewardPoints);
            Assert.IsInstanceOf<int?>(customer.RewardPoints);

            DumpSimpleCustomer(customer);
        }

        private void DumpCustomers(IList<SimpleCustomer> customers)
        {
            foreach (SimpleCustomer customer in customers)
            {
                DumpSimpleCustomer(customer);
            }
        }

        private void DumpSimpleCustomer(Object theObject)
        {
            Trace.WriteLine("");
            foreach (PropertyInfo propertyInfo in theObject.GetType().GetProperties())
            {
                Trace.WriteLine(String.Format("{0} [{1}]", propertyInfo.Name, propertyInfo.GetValue(theObject, null)));

                if (propertyInfo.PropertyType == typeof(byte[]))
                {
                    var theArray = propertyInfo.GetValue(theObject, null) as byte[];
                    if (theArray != null)
                    {
                        Trace.Write("  byte[] ");
                        for (var i = 0; i < theArray.Length; i++)
                        {
                            Trace.Write(String.Format("[{0}]", theArray[i]));
                        }
                        Trace.WriteLine(String.Empty);
                    }
                }
            }
        }
    }
}
