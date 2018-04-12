using System;
using System.Collections.Generic;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    /// <summary>
    ///     Produces a random UK style Street Address
    ///     ex: 123 High Street
    /// </summary>
    public class UnitedKingdonAddressGenerator : IGenerator<string>
    {
        private static readonly Random _randomiser = RandomSingleton.Instance.Random;

        /// <summary>
        ///     Local var for holding streetnames
        /// </summary>
        private readonly FromListGetSingleGenerator<string> _streetnames = new FromListGetSingleGenerator<string>()
        {
            { "High", 200 },
            { "Church", 180 },
            { "Station", 160 },
            { "Mill", 130 },
            { "London", 100 },
            { "Green", 95 },
            { "Main", 90 },
            { "School", 85 },
            { "Bridge", 85 },
            { "New", 82 },
            { "Park", 65 },
            { "Manor", 53 },
            { "Manchester", 45 },
            { "Edinburgh", 45 },
            { "Swansea", 45 },
            { "Coventry", 45 },
            { "Harrogate", 45 },
            { "Bath", 45 },
            { "Birmingham", 45 },
            { "Liverpool", 45 },
            { "Blackpool", 45 },
            { "Burnley", 45 },
            { "Bradford", 45 },
            { "Leeds", 45 },
            { "Scarborough", 45 },
            { "York", 45 },
            { "Newcastle", 45 },
            { "Lincoln", 45 },
            { "Newark", 45 },
            { "Nottingham", 45 },
            { "Crewe", 45 },
            { "Chester", 45 },
            { "Derby", 45 },
            { "Preston", 45 },
            { "Manse", 45 },
            { "Wakefield", 45 },
            { "Bury", 45 },
            { "Bolton", 45 },
            { "Stockport", 45 },
            { "Sheffield", 45 },
            { "Hull", 45 },
            { "Tadcaster", 45 },
            { "North", 45 },
            { "South", 45 },
            { "East", 45 },
            { "West", 45 },
            { "George", 35 },
            { "Victoria", 35 },
            { "Commercial", 35 },
            { "Hill", 35 },
            { "Market", 35 },
            { "Water", 25 },
            { "Main", 25 },
        };

        // taken from https://en.wikipedia.org/wiki/Street_suffix
        private readonly FromListGetSingleGenerator<string> _suffixes = new FromListGetSingleGenerator<string>()
        {
            { "Road", 100 },
            { "Street", 100 },
            { "Way", 50 },
            { "Avenue", 50},
            { "Drive", 50 },
            { "Grove", 25 },
            { "Lane", 50 },
            { "Gardens", 15 },
            { "Place", 15 },
            { "Circus", 5 },
            { "Crescent", 15 },
            { "Close", 15 },
            { "Square", 10 },
            { "Hill", 10 },
            { "Mews", 5 },
            { "Vale", 2 },
            { "Row", 5 },
            { "Wharf", 2 },
        };

        /// <summary>
        ///     Initializes a new instance of the AmericanAddressGenerator class.
        /// </summary>
        public UnitedKingdonAddressGenerator()
        {
        }

        /// <summary>
        ///     Generates the Address string
        /// </summary>
        /// <returns>String containing the generated address</returns>
        public string Generate()
        {
            var sb = new StringBuilder();
            var numericPortion = _randomiser.Next(1, 125);
            sb.Append(numericPortion);
            sb.Append(" ");

            sb.Append(_streetnames.Generate());
            sb.Append(" ");

            sb.Append(_suffixes.Generate());

            return sb.ToString();
        }
    }
}