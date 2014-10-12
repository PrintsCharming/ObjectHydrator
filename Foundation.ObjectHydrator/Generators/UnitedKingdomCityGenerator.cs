using System;
using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class UnitedKingdomCityGenerator : IGenerator<string>
    {
        private readonly Random _random;
        private IList<string> _citynames = new List<string>();

        public UnitedKingdomCityGenerator()
        {
            _random = RandomSingleton.Instance.Random;
            LoadCityNames();
        }

        private void LoadCityNames()
        {
            _citynames = new List<string>()
            {
                "Bath",
                "Birmingham",
                "Bradford",
                "Brighton & Hove",
                "Bristol",
                "Cambridge",
                "Canterbury",
                "Carlisle",
                "Chelmsford ",
                "Chester ",
                "Chichester ",
                "Coventry ",
                "Derby ",
                "Durham ",
                "Ely ",
                "Exeter ",
                "Gloucester ",
                "Hereford ",
                "Kingston upon Hull",
                "Lancaster ",
                "Leeds ",
                "Leicester ",
                "Lichfield ",
                "Lincoln ",
                "Liverpool ",
                "City of London",
                "Manchester ",
                "Newcastle upon Tyne",
                "Norwich ",
                "Nottingham ",
                "Oxford ",
                "Peterborough ",
                "Plymouth ",
                "Portsmouth ",
                "Preston ",
                "Ripon ",
                "Salford",
                "Salisbury",
                "Sheffield ",
                "Southampton ",
                "St ",
                "Stoke-on-Trent",
                "Sunderland ",
                "Truro ",
                "Wakefield ",
                "Wells ",
                "Westminster ",
                "Winchester ",
                "Wolverhampton ",
                "Worcester ",
                "York ",
                "Aberdeen",
                "Dundee",
                "Edinburgh",
                "Glasgow",
                "Inverness",
                "Perth",
                "Stirling",
                "Bangor ",
                "Cardiff",
                "Newport ",
                "St David's",
                "Swansea",
                "Armagh",
                "Belfast",
                "Londonderry",
                "Lisburn",
                "Newry"
            };
        }

        public string Generate()
        {
            return _citynames[_random.Next(0, _citynames.Count)];
        }
    }
}