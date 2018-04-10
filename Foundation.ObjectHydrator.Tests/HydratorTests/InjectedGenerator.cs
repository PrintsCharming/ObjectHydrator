using System;
using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    class InjectedGenerator:IGenerator<string>
    {
         private readonly Random _random;
        private IList<string> _states = new List<string>();

        public InjectedGenerator()
        {
            _random = RandomSingleton.Instance.Random;
            LoadStates();
        }

        private void LoadStates()
        {
            _states = new List<string>
                         {
                             "AK",                           
                             "CA"
                             
                         };
        }
        public string Generate()
        {
            return _states[_random.Next(0, _states.Count)];
        }
    }
}
