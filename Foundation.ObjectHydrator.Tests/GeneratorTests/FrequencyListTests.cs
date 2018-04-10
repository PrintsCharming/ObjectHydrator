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
        public void Enumeration_SingleItemWithSingleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
            };

            var actualCount = target.Count();
            var actual = target.ToArray();

            Assert.AreEqual(actualCount, 1, "There is one item in the array");
            Assert.AreEqual(string.Join(",", actual), "Alpha");
        }

        [Test]
        public void Enumeration_SingleItemWithMultipleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                { "Alpha", 3 }
            };

            var actualCount = target.Count();
            var actual = target.ToArray();

            Assert.AreEqual(actualCount, 3, "There are 3 items in the array");
            Assert.AreEqual(string.Join(",", actual), "Alpha,Alpha,Alpha");
        }

        [Test]
        public void Enumeration_ManyItemsWithSingleFrequency()
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
        public void Enumeration_FirstItemHasIncreasedFrequency()
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

        [Test]
        public void Enumeration_LastItemHasIncreasedFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
                "Bravo",
                "Charlie",
                "Delta",
                {"Echo", 2}
            };

            var actualCount = target.Count();
            var actual = target.ToArray();

            Assert.AreEqual(actualCount, 6, "There are 6 items in the array, because echo has two entries");
            Assert.AreEqual(string.Join(",", actual), "Alpha,Bravo,Charlie,Delta,Echo,Echo");
        }

        [Test]
        public void Enumeration_ManyItemsWithIncreasedFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
                {"Bravo", 2},
                {"Charlie", 3},
                {"Delta", 4},
                {"Echo", 5}
            };

            var actualCount = target.Count();
            var actual = target.ToArray();

            Assert.AreEqual(actualCount, 15, "There are 15 items in the array, because echo has two entries");
            Assert.AreEqual(string.Join(",", actual), "Alpha,Bravo,Bravo,Charlie,Charlie,Charlie,Delta,Delta,Delta,Delta,Echo,Echo,Echo,Echo,Echo");
        }
    }
}
