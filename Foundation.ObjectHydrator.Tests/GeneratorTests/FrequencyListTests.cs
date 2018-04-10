using System;
using System.Linq;
using Foundation.ObjectHydrator.Generators;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.GeneratorTests
{
    [TestFixture]
    public class FrequencyListTests
    {

        #region Enumeration Tests
        [Test]
        public void Enumeration_EmptyList()
        {
            var target = new FrequencyList<string>();

            var actualCount = target.Count();
            var actual = target.ToArray();

            Assert.AreEqual(0, actualCount, "the list is empty");
            Assert.AreEqual(0, actual.Length, "the list is empty");
        }

        [Test]
        public void Enumeration_SingleItemWithSingleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
            };

            var actualCount = target.Count();
            var actual = target.ToArray();

            Assert.AreEqual(1, actualCount, "There is one item in the array");
            Assert.AreEqual("Alpha", string.Join(",", actual));
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

            Assert.AreEqual(3, actualCount, "There are 3 items in the array");
            Assert.AreEqual("Alpha,Alpha,Alpha", string.Join(",", actual));
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

            Assert.AreEqual(5, actualCount, "There are 5 items in the array");
            Assert.AreEqual("Alpha,Bravo,Charlie,Delta,Echo", string.Join(",", actual));
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

            Assert.AreEqual(6, actualCount, "There are 6 items in the array, because alpha has two entries");
            Assert.AreEqual("Alpha,Alpha,Bravo,Charlie,Delta,Echo", string.Join(",", actual));
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

            Assert.AreEqual(6, actualCount, "There are 6 items in the array, because echo has two entries");
            Assert.AreEqual("Alpha,Bravo,Charlie,Delta,Echo,Echo", string.Join(",", actual));
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

            Assert.AreEqual(15, actualCount, "There are 15 items in the array, because echo has two entries");
            Assert.AreEqual("Alpha,Bravo,Bravo,Charlie,Charlie,Charlie,Delta,Delta,Delta,Delta,Echo,Echo,Echo,Echo,Echo", string.Join(",", actual));
        }

        #endregion

        #region Count Tests

        [Test]
        public void Count_EmptyList()
        {
            var target = new FrequencyList<string>();

            var actual = target.Count;

            Assert.AreEqual(0, actual, "this list is empty");
        }

        [Test]
        public void Count_SingleItemWithSingleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
            };

            var actual = target.Count;

            Assert.AreEqual(1, actual, "There is one item in the array");
        }

        [Test]
        public void Count_SingleItemWithMultipleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                { "Alpha", 3 }
            };

            var actual = target.Count;

            Assert.AreEqual(3, actual, "There are 3 items in the array");
        }

        [Test]
        public void Count_ManyItemsWithSingleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
                "Bravo",
                "Charlie",
                "Delta",
                "Echo"
            };

            var actual = target.Count;

            Assert.AreEqual(5, actual, "There are 5 items in the array");
        }

        [Test]
        public void Count_FirstItemHasIncreasedFrequency()
        {
            var target = new FrequencyList<string>()
            {
                {"Alpha", 2},
                "Bravo",
                "Charlie",
                "Delta",
                "Echo"
            };

            var actual = target.Count;

            Assert.AreEqual(6, actual, "There are 6 items in the array, because alpha has two entries");
        }

        [Test]
        public void Count_LastItemHasIncreasedFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
                "Bravo",
                "Charlie",
                "Delta",
                {"Echo", 2}
            };

            var actual = target.Count;

            Assert.AreEqual(6, actual, "There are 6 items in the array, because echo has two entries");
        }

        [Test]
        public void Count_ManyItemsWithIncreasedFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
                {"Bravo", 2},
                {"Charlie", 3},
                {"Delta", 4},
                {"Echo", 5}
            };

            var actualCount = target.Count;

            Assert.AreEqual(15, actualCount, "There are 15 items in the array, because echo has two entries");
        }

        #endregion

        #region Indexer Tests

        [Test]
        public void Indexer_EmptyList()
        {
            var target = new FrequencyList<string>();
            string ignored;
            Assert.Throws<IndexOutOfRangeException>(() => ignored = target[0]);
        }

        [Test]
        public void Indexer_SingleItemWithSingleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
            };

            var actual = target[0];

            Assert.AreEqual("Alpha", actual);
            string ignored;
            Assert.Throws<IndexOutOfRangeException>(() => ignored = target[1]);
        }

        [Test]
        public void Indexer_SingleItemWithMultipleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                { "Alpha", 3 }
            };

            Assert.AreEqual("Alpha", target[0]);
            Assert.AreEqual("Alpha", target[1]);
            Assert.AreEqual("Alpha", target[2]);
            string ignored;
            Assert.Throws<IndexOutOfRangeException>(() => ignored = target[3]);
        }

        [Test]
        public void Indexer_ManyItemsWithSingleFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
                "Bravo",
                "Charlie",
                "Delta",
                "Echo"
            };

            Assert.AreEqual("Alpha", target[0]);
            Assert.AreEqual("Bravo", target[1]);
            Assert.AreEqual("Charlie", target[2]);
            Assert.AreEqual("Delta", target[3]);
            Assert.AreEqual("Echo", target[4]);
        }

        [Test]
        public void Indexer_FirstItemHasIncreasedFrequency()
        {
            var target = new FrequencyList<string>()
            {
                {"Alpha", 2},
                "Bravo",
                "Charlie",
                "Delta",
                "Echo"
            };

            Assert.AreEqual("Alpha", target[0]);
            Assert.AreEqual("Alpha", target[1]);
            Assert.AreEqual("Bravo", target[2]);
            Assert.AreEqual("Charlie", target[3]);
            Assert.AreEqual("Delta", target[4]);
            Assert.AreEqual("Echo", target[5]);
        }

        [Test]
        public void Indexer_LastItemHasIncreasedFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
                "Bravo",
                "Charlie",
                "Delta",
                {"Echo", 2}
            };

            Assert.AreEqual("Alpha", target[0]);
            Assert.AreEqual("Bravo", target[1]);
            Assert.AreEqual("Charlie", target[2]);
            Assert.AreEqual("Delta", target[3]);
            Assert.AreEqual("Echo", target[4]);
            Assert.AreEqual("Echo", target[5]);
        }

        [Test]
        public void Indexer_ManyItemsWithIncreasedFrequency()
        {
            var target = new FrequencyList<string>()
            {
                "Alpha",
                {"Bravo", 2},
                {"Charlie", 3},
                {"Delta", 4},
                {"Echo", 5}
            };

            Assert.AreEqual("Alpha", target[0]);
            Assert.AreEqual("Bravo", target[1]);
            Assert.AreEqual("Bravo", target[2]);
            Assert.AreEqual("Charlie", target[3]);
            Assert.AreEqual("Charlie", target[4]);
            Assert.AreEqual("Charlie", target[5]);
            Assert.AreEqual("Delta", target[6]);
            Assert.AreEqual("Delta", target[7]);
            Assert.AreEqual("Delta", target[8]);
            Assert.AreEqual("Delta", target[8]);
            Assert.AreEqual("Echo", target[10]);
            Assert.AreEqual("Echo", target[11]);
            Assert.AreEqual("Echo", target[12]);
            Assert.AreEqual("Echo", target[13]);
            Assert.AreEqual("Echo", target[14]);
        }

        #endregion
    }
}
