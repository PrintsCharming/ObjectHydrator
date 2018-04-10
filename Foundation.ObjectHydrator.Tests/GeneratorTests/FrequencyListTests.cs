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

            var actualCount = target.Count();
            var actual = target.ToArray();

            Assert.AreEqual(actualCount, 5, "There are 5 items in the array");
            Assert.AreEqual(string.Join(",", actual), "Alpha,Bravo,Charlie,Delta,Echo");
        }

        [Test]
        public void Enumeration_OneItemHasIncreasedFrequency()
        {
            var target = new FrequencyList<string>()
            {
                {"Alpha", 2},
                "Bravo",
                "Charlie",
                "Delta",
                "Echo"
            };

            var actualCount = target.Count();
            var actual = target.ToArray();

            Assert.AreEqual(actualCount, 6, "There are 6 items in the array, because alpha has two entries");
            Assert.AreEqual(string.Join(",", actual), "Alpha,Alpha,Bravo,Charlie,Delta,Echo");
        }
    }
}
