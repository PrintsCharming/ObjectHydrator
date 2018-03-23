using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class Hydrator_Do_Tests
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
        public void CanSetOnePropertyFromAnother()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithFirstName(x => x.FirstName)
                .WithLastName(x => x.LastName)
                .WithCompanyName(x => x.Company)
                .Do(x => x.EmailAddress = string.Format("{0}.{1}@{2}.com", x.FirstName, x.LastName, x.Company));

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.EmailAddress, string.Format("{0}.{1}@{2}.com", customer.FirstName, customer.LastName, customer.Company), "The value should have been set in the Do method");
        }
    }
}