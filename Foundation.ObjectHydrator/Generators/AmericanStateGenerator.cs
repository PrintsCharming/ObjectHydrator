using System;
using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class AmericanStateGenerator : IGenerator<string>
    {
        private readonly Random random;
        private IList<string> states = new List<string>();

        public AmericanStateGenerator()
        {
            random = RandomSingleton.Instance.Random;
            LoadStates();
        }

        #region IGenerator<string> Members

        public string Generate()
        {
            return states[random.Next(0, states.Count)];
        }

        #endregion

        private void LoadStates()
        {
            states = new List<string>
                         {
                             "AK",
                             "AL",
                             "AP",
                             "AR",
                             "AS",
                             "AZ",
                             "CA",
                             "CO",
                             "CT",
                             "DC",
                             "DE",
                             "FL",
                             "FM",
                             "GA",
                             "GU",
                             "HI",
                             "IA",
                             "ID",
                             "IL",
                             "IN",
                             "KS",
                             "KY",
                             "LA",
                             "MA",
                             "MD",
                             "ME",
                             "MH",
                             "MI",
                             "MN",
                             "MO",
                             "MP",
                             "MS",
                             "MT",
                             "NC",
                             "ND",
                             "NE",
                             "NH",
                             "NJ",
                             "NM",
                             "NV",
                             "NY",
                             "OH",
                             "OK",
                             "OR",
                             "PA",
                             "PR",
                             "PW",
                             "RI",
                             "SC",
                             "SD",
                             "TN",
                             "TX",
                             "UT",
                             "VA",
                             "VI",
                             "VT",
                             "WA",
                             "WV",
                             "WI",
                             "WY"
                         };
        }
    }
}