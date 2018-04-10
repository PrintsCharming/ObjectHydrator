using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    class HydratorInjectedGeneratorTest
    {
        [Test]
        public void SimpleTest()
        {
            var hydrator = new Hydrator<Address>().WithCustomGenerator(x=>x.State, new InjectedGenerator());

            var checkme = hydrator.GetSingle();
            Assert.IsNotNull(checkme);
        }
    }
}
