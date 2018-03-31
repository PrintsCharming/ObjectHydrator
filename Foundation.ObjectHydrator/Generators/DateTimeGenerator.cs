using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class DateTimeGenerator:IGenerator<DateTime>
    {
        readonly Random _random;
        public DateTime MinimumValue { get; private set; }
        public DateTime MaximumValue { get; private set; }

        public DateTimeGenerator()
            : this(DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10))
        { }


        public DateTimeGenerator(DateTime minimumValue, DateTime maximumValue)
        {
            MinimumValue=minimumValue;
            MaximumValue=maximumValue;

            _random=RandomSingleton.Instance.Random;
        }

        public DateTime Generate()
        {
            TimeSpan timeSpan = MaximumValue - MinimumValue;
            int dayOffset = _random.Next(0, timeSpan.Days);
            return MinimumValue.Date.AddDays(dayOffset) + new TimeSpan(_random.Next(0, 24), _random.Next(0, 59), _random.Next(0, 59));

        }
    }
}
