namespace Foundation.ObjectHydrator.Generators
{
    public interface IJobTitleGeneratorOptionsBuilder
    {
        IJobTitleGeneratorOptionsBuilder WithLevel(JobLevel level, int frequency = 1);
        IJobTitleGeneratorOptionsBuilder WithoutLevel(JobLevel level);
    }
}