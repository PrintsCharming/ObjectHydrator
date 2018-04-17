using System.Collections.Generic;

namespace Foundation.ObjectHydrator.Generators
{
    public class JobTitleGeneratorOptions : IJobTitleGeneratorOptionsBuilder
    {
        private readonly Dictionary<JobLevel, int> _jobLevels = new Dictionary<JobLevel, int>
        {
            {JobLevel.High, 1 },
            {JobLevel.Middle, 1 },
            {JobLevel.Regular, 1 }
        };

        public IJobTitleGeneratorOptionsBuilder WithLevel(JobLevel level, int frequency = 1)
        {
            _jobLevels[level] = frequency;

            return this;
        }

        public IJobTitleGeneratorOptionsBuilder WithoutLevel(JobLevel level)
        {
            return WithLevel(level, 0);
        }

        public int GetFrequencyFor(JobLevel level)
        {
            return _jobLevels[level];
        }
    }
}