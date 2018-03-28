using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation.ObjectHydrator.Generators;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.GeneratorTests
{
    [TestFixture]
    public class FrequencyListTests
    {
        [Test]
        public void Enumeration_ItemsWithSingleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
                "Bravo",
                "Charlie",
                "Delta",
                "Echo"
            };

            var actual = target.ToArray();

            Assert.AreEqual(string.Join(",", actual), "Alpha,Bravo,Charlie,Delta,Echo");
        }
    }
}
