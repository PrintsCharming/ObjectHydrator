using Foundation.ObjectHydrator.Interfaces;
using System;

namespace Foundation.ObjectHydrator.Generators
{
    public class NullableDateTimeGenerator : IGenerator<DateTime?>
    {
        readonly Random _random;
        private readonly bool _allowNulls;
        public DateTime MinimumValue { get; }
        public DateTime MaximumValue { get; }

        public NullableDateTimeGenerator(bool allowNulls)
            : this(DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10), allowNulls)
        { }


        public NullableDateTimeGenerator(DateTime minimumValue, DateTime maximumValue, bool allowNulls)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            _allowNulls = allowNulls;

            _random = RandomSingleton.Instance.Random;
        }

        public DateTime? Generate()
        {
            var timeSpan = MaximumValue - MinimumValue;
            var max = _allowNulls ? timeSpan.Days + 1 : timeSpan.Days;
            var dayOffset = _random.Next(0, max);

            //null will be represneted by MaximumValue 
            if (dayOffset == timeSpan.Days)
            {
                return null;
            }

            return MinimumValue.Date.AddDays(dayOffset) + new TimeSpan(_random.Next(0, 24), _random.Next(0, 59), _random.Next(0, 59));

        }
    }
}
