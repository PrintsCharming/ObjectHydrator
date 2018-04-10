using System;
using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class AmericanStateGenerator : IGenerator<string>
    {
        private readonly Random _random;
        private IList<string> _states = new List<string>();

        public AmericanStateGenerator()
        {
            _random = RandomSingleton.Instance.Random;
            LoadStates();
        }

        #region IGenerator<string> Members

        public string Generate()
        {
            return _states[_random.Next(0, _states.Count)];
        }

        #endregion

        private void LoadStates()
        {
            _states = new List<string>
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