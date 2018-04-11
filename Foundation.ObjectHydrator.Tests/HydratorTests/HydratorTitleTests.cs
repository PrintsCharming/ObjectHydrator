using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class HydratorTitleTests
    {
        private const int SampleSize = 500;

        [Test]
        public void CanSetTitle()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithTitle(x => x.Title);

            var customer = hydrator.GetSingle();

            Assert.IsNotNull(customer);
            Assert.IsNotNull(customer.Title, "The value should have been set with a title");
            Assert.IsNotEmpty(customer.Title, "The value should have been set with a title");
        }

        [Test]
        public void CanSetMaleOnlyTitle()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithTitle(x => x.Title, opts => opts.ExcludingFemaleTitles());

            for (int i = 0; i < SampleSize; i++)
            {
                var customer = hydrator.GetSingle();

                Assert.IsNotNull(customer);
                Assert.IsNotNull(customer.Title, "The value should have been set with a title");
                Assert.IsNotEmpty(customer.Title, "The value should have been set with a title");
                Assert.AreNotEqual("Mrs", customer.Title);
                Assert.AreNotEqual("Miss", customer.Title);
                Assert.AreNotEqual("Lady", customer.Title);
            }
        }

        [Test]
        public void CanSetFemaleOnlyTitle()
        {
            var hydrator = new Hydrator<SimpleCustomer>()
                .WithTitle(x => x.Title, opts => opts.ExcludingMaleTitles());

            for (int i = 0; i < SampleSize; i++)
            {
                var customer = hydrator.GetSingle();

                Assert.IsNotNull(customer);
                Assert.IsNotNull(customer.Title, "The value should have been set with a title");
                Assert.IsNotEmpty(customer.Title, "The value should have been set with a title");
                Assert.AreNotEqual("Mr", customer.Title);
                Assert.AreNotEqual("Sir", customer.Title);
                Assert.AreNotEqual("Lord", customer.Title);
            }
        }

    }
}