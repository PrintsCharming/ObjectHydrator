namespace Foundation.ObjectHydrator.Tests.POCOs
{
    public abstract class Animal
    {
        public abstract string Name { get; set; }

    }

    public class Dog : Animal
    {
        public override string Name { get; set; }
    }

    public class Cat : Animal
    {
        public override string Name { get; set; }
    }
}
