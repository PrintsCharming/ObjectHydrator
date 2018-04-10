using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class HydratorSimpleAddressTests
    {
        [Test]
        public void SimpleTest()
        {
            var hydrator = new Hydrator<Address>();

            var checkme = hydrator.GetSingle();
            Assert.IsNotNull(checkme);
        }

        [Test]
        public void CityTest()
        {
            var hydrator = new Hydrator<Address>();

            var checkme = hydrator.GetSingle();
            Assert.IsNotNull(checkme);
        }

        [Test]
        public void StateTest()
        {
            var hydrator = new Hydrator<Address>();

            var checkme = hydrator.GetSingle();
            Assert.IsNotNull(checkme);
        }
    }
}
