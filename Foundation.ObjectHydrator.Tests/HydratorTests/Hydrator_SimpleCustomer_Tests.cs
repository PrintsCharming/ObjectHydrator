using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class HydratorSimpleCustomerTests
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
            Assert.IsTrue(customer.Description.Length <= 5,"Length not restricted");
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
                $"Customer Locations [{customer.Locations}] is outside expected range.");

            DumpSimpleCustomer(customer);
        }


        [Test]
        public void CanDefaultString()
        {
            var defaultValue = "Testing123";

            var hydrator = new Hydrator<SimpleCustomer>()
                .With(x => x.Description, defaultValue);

            var customer = hydrator.GetSingle();

            Assert.IsTrue(defaultValue == customer.Description, $"Default value is not as expected[{defaultValue}]");

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

            Assert.IsTrue(customer.Locations >= 0, "Customer Locations is expected.");

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanGetDouble()
        {
            var hydrator = new Hydrator<SimpleCustomer>();

            var customer = hydrator.GetSingle();

            Assert.IsTrue(customer.Revenue >= 0, "Customer Revenue is expected.");

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanConstrainDoubleDecimalPlaces()
        {
            var decimalPlaces = 3;

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithDouble(x => x.Revenue, decimalPlaces);
            var customer = hydrator.GetSingle();

            var decimalPart = customer.Revenue - (int) customer.Revenue;
            Assert.IsTrue(decimalPart >= 0, "Customer Revenue decimal part is expected.");

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
            var decimalPart = customer.Revenue - (int) customer.Revenue;

            Assert.That(customer.Revenue, Is.InRange(minimum, maximum));
            Assert.IsTrue(decimalPart >= 0, "Customer Revenue decimal part is expected.");

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanDefaultInteger()
        {
            var defaultValue = 73;

            var hydrator = new Hydrator<SimpleCustomer>()
                .With(x => x.Locations, defaultValue);

            var customer = hydrator.GetSingle();

            Assert.IsTrue(defaultValue == customer.Locations, $"Default value is not as expected[{defaultValue}]");

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanDefaultDateTime()
        {
            var defaultValue = new DateTime(2009, 01, 01);

            var hydrator = new Hydrator<SimpleCustomer>()
                .With(x => x.IncorporatedOn, defaultValue);


            var customer = hydrator.GetSingle();

            Assert.IsTrue(defaultValue == customer.IncorporatedOn, $"Default value is not as expected[{defaultValue}]");

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

            Assert.IsTrue(defaultValue == customer.Description, $"Default value is not as expected[{defaultValue}]");

            Assert.That(customer.Locations, Is.InRange(minimumValue, maximumValue),
                $"Customer Locations [{customer.Locations}] is outside expected range [{minimumValue},{maximumValue}].");
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
                $"Customer IncorporatedOn [{customer.IncorporatedOn}] is outside expected range [{minimumValue}, {maximumValue}].");
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
                $"Customer IncorporatedOn [{customer.IncorporatedOn}] is outside expected range [{minimumValue}, {maximumValue}].");
            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanGetByteArray()
        {
            var length = 10;
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithByteArray(x => x.Version, length);

            var customer = hydrator.GetSingle();

            Assert.IsTrue(customer.Version.Length == length, $"Customer Version Length is expected to be {length}.");

            DumpSimpleCustomer(customer);
        }

        [Test]
        public void CanGetCreditCardNumber()
        {
            var length = 16;

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithCreditCardNumber(x => x.CreditCardNumber, length);

            var customer = hydrator.GetSingle();

            Assert.IsNotEmpty(customer.CreditCardNumber, "Credit Card Number is expected.");
            Assert.IsTrue(customer.CreditCardNumber.Length == length,
                $"Credit Card Number [{customer.CreditCardNumber}] should be {length} long.");

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
            Assert.IsNotNull(customer.Homepage);
            Assert.IsTrue(IsWebsiteAddressValid(customer.Homepage));
        }

        public bool IsValidIpAddress(string ipaddress)
        {
            var testpattern =
                new Regex(
                    "^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
            return testpattern.IsMatch(ipaddress);
        }

        [Test]
        public void IpAddressTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Ipaddress);
            Assert.IsTrue(IsValidIpAddress(customer.Ipaddress));
        }

        [Test]
        public void WithIpAddressTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithIpAddress(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
            Assert.IsTrue(IsValidIpAddress(customer.Placeholderstring));
        }

        [Test]
        public void GenderTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Gender);
        }

        [Test]
        public void WithGenderTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithGender(x => x.Placeholderstring);

            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
            Assert.IsTrue(customer.Placeholderstring.ToLower().Contains("male") ||
                          customer.Placeholderstring.ToLower().Contains("female"));
        }

        [Test]
        public void CreditCardTypeTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>();
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Creditcardtype);
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
        public void CanGetCcv()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithCcv(x => x.Ccv, "visa");
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Ccv);
            Assert.IsTrue(customer.Ccv.Length == 3);
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
                .WithAmericanPhone(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
            Assert.IsTrue(CheckPhone(customer.Placeholderstring));
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
                .WithAmericanPostalCode(x => x.Placeholderstring, 1);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
            Assert.IsTrue(IsAmericanPostalCodeValid(customer.Placeholderstring));
        }

        [Test]
        public void FromListTest()
        {
            IList<string> mylist = new List<string> {"red", "green", "blue", "orange"};
            var hydrator = new Hydrator<SimpleCustomer>()
                .FromList(x => x.Placeholderstring, mylist);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
            Assert.IsTrue(mylist.Contains(customer.Placeholderstring));
        }

        [Test]
        public void WithAmericanAddressTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAmericanAddress(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithAmericanCityTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAmericanCity(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithAmericanState()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAmericanState(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithCompanyName()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithCompanyName(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithCreditCardType()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithCreditCardType(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithEmailAddress()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithEmailAddress(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithFirstName()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithFirstName(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithLastName()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithLastName(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithWebsite()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithWebsite(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
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
                .WithPassword(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
            Assert.AreEqual(10, customer.Placeholderstring.Length);
        }

        [Test]
        public void PasswordUsingWithAndCustomLength()
        {
            var pwlen = 15;
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithPassword(x => x.Placeholderstring, pwlen);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
            Assert.AreEqual(pwlen, customer.Placeholderstring.Length);
        }

        [Test]
        public void AlphaNumericWithLength()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithAlphaNumeric(x => x.Placeholderstring, 10);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
            Assert.AreEqual(10, customer.Placeholderstring.Length);
        }

        [Test]
        public void WithUnitedKingdomNationalInsuranceNumberTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithUnitedKingdomNationalInsuranceNumber(x => x.Placeholderstring);

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer.Placeholderstring);
            Assert.IsNotEmpty(customer.Placeholderstring);
        }

        [Test]
        public void WithUnitedKingdomCityTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithUnitedKingdomCity(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithUnitedKingdomCountyTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithUnitedKingdomCounty(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithUnitedKingdomPostCodeTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithUnitedKingdomPostCode(x => x.Placeholderstring);
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer.Placeholderstring);
        }

        [Test]
        public void WithUnitedKingdomLandlineTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithUnitedKingdomLandline(x => x.Placeholderstring);

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer.Placeholderstring);
            Assert.IsNotEmpty(customer.Placeholderstring);
            Assert.IsTrue(customer.Placeholderstring.StartsWith("01"));
        }

        [Test]
        public void WithUnitedKingdomMobileTest()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithUnitedKingdomMobile(x => x.Placeholderstring);

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer.Placeholderstring);
            Assert.IsNotEmpty(customer.Placeholderstring);
            Assert.IsTrue(customer.Placeholderstring.StartsWith("07"));
        }

        private void DumpSimpleCustomer(Object theObject)
        {
            Trace.WriteLine("");
            foreach (PropertyInfo propertyInfo in theObject.GetType().GetProperties())
            {
                Trace.WriteLine($"{propertyInfo.Name} [{propertyInfo.GetValue(theObject, null)}]");

                if (propertyInfo.PropertyType == typeof (byte[]))
                {
                    if (propertyInfo.GetValue(theObject, null) is byte[] theArray)
                    {
                        Trace.Write("  byte[] ");
                        foreach (byte t in theArray)
                        {
                            Trace.Write($"[{t}]");
                        }
                        Trace.WriteLine(String.Empty);
                    }
                }
            }
        }
    }
}
