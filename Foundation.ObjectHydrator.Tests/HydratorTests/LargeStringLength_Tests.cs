using NUnit.Framework;
using Foundation.ObjectHydrator.Tests.POCOs;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class LargeStringLength_Tests
    {
        [Test]
        public void CanHandleLargeStringLengthAttribute()
        {
            var hydrator = new Hydrator<LargeStringLengthCustomer>();
            
            // This should not throw an exception
            var customer = hydrator.GetSingle();
            
            Assert.That(customer.LargeDescription, Is.Not.Null);
            Assert.That(customer.LargeDescription.Length, Is.LessThanOrEqualTo(4000));
        }
        
        [Test]
        public void CanHandleStringLengthOfOne()
        {
            var hydrator = new Hydrator<LargeStringLengthCustomer>();
            
            // This might throw an exception with the current implementation
            var customer = hydrator.GetSingle();
            
            Assert.That(customer.TinyDescription, Is.Not.Null);
            Assert.That(customer.TinyDescription.Length, Is.LessThanOrEqualTo(1));
        }
        
        [Test]
        public void CanHandleStringLengthOfTwo()
        {
            var hydrator = new Hydrator<LargeStringLengthCustomer>();
            
            // This might throw an exception with the current implementation
            var customer = hydrator.GetSingle();
            
            Assert.That(customer.SmallDescription, Is.Not.Null);
            Assert.That(customer.SmallDescription.Length, Is.LessThanOrEqualTo(2));
        }
        
        [Test]
        public void CanGenerateMultipleLargeStringLengthObjects()
        {
            var hydrator = new Hydrator<LargeStringLengthCustomer>();
            
            // Run multiple times to increase chances of hitting the random edge case
            for (int i = 0; i < 100; i++)
            {
                var customer = hydrator.GetSingle();
                Assert.That(customer.LargeDescription, Is.Not.Null);
                Assert.That(customer.TinyDescription, Is.Not.Null);
                Assert.That(customer.SmallDescription, Is.Not.Null);
            }
        }
    }
}