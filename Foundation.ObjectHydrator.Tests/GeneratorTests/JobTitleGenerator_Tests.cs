using System.Collections.Generic;
using System.Linq;
using Foundation.ObjectHydrator.Generators;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.GeneratorTests
{
    [TestFixture]
    public class JobTitleGenerator_Tests
    {
        [Test]
        public void ShouldGenerateAValue()
        {
            var target = new JobTitleGenerator();

            var actual = target.Generate();

            Assert.IsNotNull(actual, "A job title should be generated");
            Assert.IsNotEmpty(actual.Trim(), "A job title should be generated");
        }

        [Test]
        public void ConfiguredToHighLevelOnly()
        {
            // generate enough to validate by probability
            // These are not great tests as they work by probability
            // but they are good enough to start with
            const int generationCount = 200;
            var actuals = new List<string>(generationCount);

            var target = new JobTitleGenerator(opts => opts
                .WithLevel(JobLevel.High)
                .WithoutLevel(JobLevel.Middle)
                .WithoutLevel(JobLevel.Regular)
            );

            for (int i = 0; i < generationCount; i++)
            {
                var actual = target.Generate();
                actuals.Add(actual);

            }

            foreach (var actual in actuals)
            {
                Assert.IsNotNull(actual, "A job title should be generated");
                Assert.IsNotEmpty(actual.Trim(), "A job title should be generated");
            }

            Assert.IsTrue(actuals.Any(a => a.StartsWith("President")));
            Assert.IsTrue(actuals.Any(a => a.StartsWith("Chairman")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Lead")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Senior")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Trainee")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Graduate")));
        }

        [Test]
        public void ConfiguredToMidLevelOnly()
        {
            // generate enough to validate by probability
            // These are not great tests as they work by probability
            // but they are good enough to start with
            const int generationCount = 200;
            var actuals = new List<string>(generationCount);

            var target = new JobTitleGenerator(opts => opts
                .WithoutLevel(JobLevel.High)
                .WithLevel(JobLevel.Middle)
                .WithoutLevel(JobLevel.Regular)
            );

            for (int i = 0; i < generationCount; i++)
            {
                var actual = target.Generate();
                actuals.Add(actual);

            }

            foreach (var actual in actuals)
            {
                Assert.IsNotNull(actual, "A job title should be generated");
                Assert.IsNotEmpty(actual.Trim(), "A job title should be generated");
            }

            Assert.IsFalse(actuals.Any(a => a.StartsWith("President")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Chairman")));
            Assert.IsTrue(actuals.Any(a => a.StartsWith("Lead")));
            Assert.IsTrue(actuals.Any(a => a.StartsWith("Senior")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Trainee")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Graduate")));
        }

        [Test]
        public void ConfiguredToRegularLevelOnly()
        {
            // generate enough to validate by probability
            // These are not great tests as they work by probability
            // but they are good enough to start with
            const int generationCount = 200;
            var actuals = new List<string>(generationCount);

            var target = new JobTitleGenerator(opts => opts
                .WithoutLevel(JobLevel.High)
                .WithoutLevel(JobLevel.Middle)
                .WithLevel(JobLevel.Regular)
            );

            for (int i = 0; i < generationCount; i++)
            {
                var actual = target.Generate();
                actuals.Add(actual);

            }

            foreach (var actual in actuals)
            {
                Assert.IsNotNull(actual, "A job title should be generated");
                Assert.IsNotEmpty(actual.Trim(), "A job title should be generated");
            }

            Assert.IsFalse(actuals.Any(a => a.StartsWith("President")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Chairman")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Lead")));
            Assert.IsFalse(actuals.Any(a => a.StartsWith("Senior")));
            Assert.IsTrue(actuals.Any(a => a.StartsWith("Trainee")));
            Assert.IsTrue(actuals.Any(a => a.StartsWith("Graduate")));
        }
    }
}