using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    class InjectedGenerator:IGenerator<string>
    {
         private readonly Random random;
        private IList<string> states = new List<string>();

        public InjectedGenerator()
        {
            random = RandomSingleton.Instance.Random;
            LoadStates();
        }

        private void LoadStates()
        {
            states = new List<string>
                         {
                             "AK",                           
                             "CA"
                             
                         };
        }
        public string Generate()
        {
            return states[random.Next(0, states.Count)];
        }
    }
}
