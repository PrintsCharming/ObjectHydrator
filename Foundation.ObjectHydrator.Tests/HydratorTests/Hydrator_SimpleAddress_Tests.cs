using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.ObjectHydrator.Tests.POCOs;
using Foundation.ObjectHydrator.Interfaces;
using Foundation.ObjectHydrator.Generators;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class Hydrator_SimpleAddress_Tests
    {
        [Test]
        public void SimpleTest()
        {
            
            Hydrator<Address> hydrator = new Hydrator<Address>();
            
            Address checkme = hydrator.GetSingle();
            Assert.IsNotNull(checkme);
        }

        [Test]
        public void CityTest()
        {
            Hydrator<Address> hydrator = new Hydrator<Address>();

            Address checkme = hydrator.GetSingle();
            Assert.IsNotNull(checkme);

        }

        [Test]
        public void StateTest()
        {
            Hydrator<Address> hydrator = new Hydrator<Address>();

            Address checkme = hydrator.GetSingle();
            Assert.IsNotNull(checkme);

        }
        
            
    }
}
