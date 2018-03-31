using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Generators;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.GeneratorTests
{
    [TestFixture]
    public class EnumGeneratorTests
    {
        private enum OneDimension
        {
            Up,
            Down
        }

        private enum TwoDimension
        {
            Up,
            Down,
            Left,
            Right
        }

        [Test]
        public void DefaultGenerator_EnumHasTwoOptions()
        {
            var target = new EnumGenerator<OneDimension>();
            var generationCount = 100;
            var generatedValues = new List<OneDimension>(generationCount);


            for (int i = 0; i < generationCount; i++)
            {
                generatedValues.Add(target.Generate());
            }

            // assert
            Assert.AreEqual(generatedValues.Distinct().Count(), 2, "Both enum values should be generated");
        }

        [Test]
        public void EnumHasTwoOptions_OneIsExcluded()
        {
            var target = new EnumGenerator<OneDimension>(opts => opts.Excluding(OneDimension.Down));
            var generationCount = 100;
            var generatedValues = new List<OneDimension>(generationCount);


            for (int i = 0; i < generationCount; i++)
            {
                generatedValues.Add(target.Generate());
            }

            // assert
            Assert.AreEqual(generatedValues.Distinct().Count(), 1, "Only Up values should be generated");
            Assert.IsTrue(generatedValues.Contains(OneDimension.Up));
        }

        [Test]
        public void EnumHasManyOptions_TwoAreExcluded()
        {
            var target = new EnumGenerator<TwoDimension>(opts => opts
                .Excluding(TwoDimension.Down)
                .Excluding(TwoDimension.Up)
            );
            var generationCount = 100;
            var generatedValues = new List<TwoDimension>(generationCount);


            for (int i = 0; i < generationCount; i++)
            {
                generatedValues.Add(target.Generate());
            }

            // assert
            Assert.AreEqual(generatedValues.Distinct().Count(), 2, "Two unique values should be generated");
            Assert.IsTrue(generatedValues.Contains(TwoDimension.Left));
            Assert.IsTrue(generatedValues.Contains(TwoDimension.Right));
        }

        [Test]
        public void EnumHasManyOptions_OneHasHighFrequency()
        {
            var target = new EnumGenerator<OneDimension>(opts => opts
                .WithFrequency(OneDimension.Up, 10)
            );
            var generationCount = 100;
            var generatedValues = new List<OneDimension>(generationCount);


            for (int i = 0; i < generationCount; i++)
            {
                generatedValues.Add(target.Generate());
            }

            // assert
            Assert.IsTrue(generatedValues.Contains(OneDimension.Up));
            Assert.IsTrue(generatedValues.Contains(OneDimension.Down));

            var upCount = generatedValues.Count(v => v == OneDimension.Up);
            var downCount = generatedValues.Count(v => v == OneDimension.Down);
            Assert.IsTrue(upCount > downCount, "There should be about 10 times more ups than downs");
        }

        [Test]
        public void EnumHasManyOptions_TwoHaveHaveHighFrequency()
        {
            var target = new EnumGenerator<TwoDimension>(opts => opts
                .WithFrequency(TwoDimension.Up, 10)
                .WithFrequency(TwoDimension.Down, 5)
            );
            var generationCount = 500;
            var generatedValues = new List<TwoDimension>(generationCount);


            for (int i = 0; i < generationCount; i++)
            {
                generatedValues.Add(target.Generate());
            }

            // assert
            Assert.IsTrue(generatedValues.Contains(TwoDimension.Up));
            Assert.IsTrue(generatedValues.Contains(TwoDimension.Down));
            Assert.IsTrue(generatedValues.Contains(TwoDimension.Left));
            Assert.IsTrue(generatedValues.Contains(TwoDimension.Right));

            var upCount = generatedValues.Count(v => v == TwoDimension.Up);
            var downCount = generatedValues.Count(v => v == TwoDimension.Down);
            var leftCount = generatedValues.Count(v => v == TwoDimension.Left);
            var rightCount = generatedValues.Count(v => v == TwoDimension.Right);
            Assert.IsTrue(upCount > downCount, "There should be about 2 times more ups than downs");
            Assert.IsTrue(downCount > leftCount, "There should be about 5 times more downs than lefts");
            Assert.IsTrue(downCount > rightCount, "There should be about 5 times more downs than rights");
        }
    }
}