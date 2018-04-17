using System;
using System.Collections.Generic;
using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class HydratorDoTests
    {
        [Test]
        public void CanSetConstantValue()
        {
            const int expected = 90;

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithFirstName(x => x.FirstName)
                .WithLastName(x => x.LastName)
                .WithCompanyName(x => x.Company)
                .Do(x => x.Locations = expected);

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Locations, expected, "The value should have been set in the Do method");
        }

        [Test]
        public void CanSetConstantValue_MultipleActions()
        {
            const string expected = "pop";

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithFirstName(x => x.FirstName)
                .WithLastName(x => x.LastName)
                .WithCompanyName(x => x.Company)
                .Do(x => x.Description = expected)
                .Do(x => x.Password = expected);

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Description, expected, "The value should have been set in the Do method");
            Assert.AreEqual(customer.Password, expected, "The value should have been set in the Do method");
        }

        [Test]
        public void CanOverwriteOtherValue()
        {
            const string expected = "NotAFirstName";

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithFirstName(x => x.FirstName)
                .WithLastName(x => x.LastName)
                .WithCompanyName(x => x.Company)
                .Do(x => x.FirstName = expected);

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.FirstName, expected, "The value should have been set in the Do method");
        }

        [Test]
        public void CanSetIgnoredProperty()
        {
            const string expected = "NotAFirstName";

            var hydrator = new Hydrator<SimpleCustomer>()
                .Ignoring(x => x.FirstName)
                .Do(x => x.FirstName = expected);

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.FirstName, expected, "The value should have been set in the Do method");
        }

        [Test]
        public void CanUseGenerator()
        {
            var countryGenerator = new CountryCodeGenerator();

            var hydrator = new Hydrator<SimpleCustomer>()
                .Ignoring(x => x.Country)
                .Do(x => x.Country = countryGenerator.Generate());

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.IsNotNull(customer.Country, "The value should have been set in the Do method");
            Assert.IsNotEmpty(customer.Country, "The value should have been set in the Do method");
        }

        [Test]
        public void CanUseGenerator2()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .Ignoring(x => x.Country)
                .Do(x =>
                {
                    var countryGenerator = new CountryCodeGenerator();
                    x.Country = countryGenerator.Generate();
                });

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.IsNotNull(customer.Country, "The value should have been set in the Do method");
            Assert.IsNotEmpty(customer.Country, "The value should have been set in the Do method");
        }

        [Test]
        public void CanUseGenerator3()
        {
            const int sampleSize = 500;
            var maleTitles = new TitleGenerator(o => o.ExcludingFemaleTitles());
            var femaleTitles = new TitleGenerator(o => o.ExcludingMaleTitles());

            var hydrator = new Hydrator<SimpleCustomer>()
                .WithGender(x => x.Gender)
                .Do(x =>
                {
                    x.Title = x.Gender.StartsWith("M", StringComparison.CurrentCultureIgnoreCase)
                        ? maleTitles.Generate()
                        : femaleTitles.Generate();
                });

            var expectedMaleTitles = new[] {"Mr", "Sir", "Lord", "Dr", "Reverand"};
            var expectedFemaleTitles = new[] {"Mrs", "Miss", "Lady", "Dr", "Reverand"};
            for (int i = 0; i < sampleSize; i++)
            {
                var customer = hydrator.GetSingle();

                Assert.IsNotNull(customer);
                Assert.IsNotNull(customer.Title, "The value should have been set in the Do method");

                IEnumerable<string> allowedTitles;
                if (customer.Gender.StartsWith("M", StringComparison.CurrentCultureIgnoreCase))
                {
                    allowedTitles = expectedMaleTitles;
                }
                else
                {
                    allowedTitles = expectedFemaleTitles;
                }
                CollectionAssert.Contains(allowedTitles, customer.Title);
            }
        }

        [Test]
        public void CanSetOnePropertyFromAnother()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithFirstName(x => x.FirstName)
                .WithLastName(x => x.LastName)
                .WithCompanyName(x => x.Company)
                .Do(x => x.EmailAddress = $"{x.FirstName}.{x.LastName}@{x.Company}.com");

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.EmailAddress, $"{customer.FirstName}.{customer.LastName}@{customer.Company}.com", "The value should have been set in the Do method");
        }
    }
}