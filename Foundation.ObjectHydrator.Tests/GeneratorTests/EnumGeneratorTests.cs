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
        public void DefaultGenerator_EnumHasTwoOptions_OneIsExcluded()
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
        public void DefaultGenerator_EnumHasManyOptions_TwoAreExcluded()
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
    }
}