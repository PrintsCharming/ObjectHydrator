using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    class Hydrator_InjectedGenerator_Test
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
