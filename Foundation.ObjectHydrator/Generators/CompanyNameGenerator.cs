using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;

namespace Foundation.ObjectHydrator.Generators
{
    public class CompanyNameGenerator : IGenerator<string>
    {
        Random random;
        IList<string> companynames = new List<string>();

        public CompanyNameGenerator()
        {
            random = RandomSingleton.Instance.Random;
            LoadNames();
        }

        public string Generate()
        {
            return companynames[random.Next(0, companynames.Count)];
        }

        private void LoadNames()
        {
            companynames = new List<String> {
                "Acme, inc.",
                "Widget Corp",
                "123 Warehousing",
                "Demo Company",
                "Smith and Co.",
                "Foo Bars",
                "ABC Telecom",
                "Fake Brothers",
                "QWERTY Logistics",
                "Demo, inc.",
                "Sample Company",
                "Sample, inc",
                "Acme Corp",
                "Allied Biscuit",
                "Ankh-Sto Associates",
                "Extensive Enterprise",
                "Galaxy Corp",
                "Globo-Chem",
                "Mr. Sparkle",
                "Globex Corporation",
                "LexCorp",
                "LuthorCorp",
                "North Central Positronics",
                "Omni Consimer Products",
                "Praxis Corporation",
                "Sombra Corporation",
                "Sto Plains Holdings",
                "Tessier-Ashpool",
                "Wayne Enterprises",
                "Wentworth Industries",
                "ZiffCorp",
                "Bluth Company",
                "Strickland Propane",
                "Thatherton Fuels",
                "Three Waters",
                "Water and Power",
                "Western Gas & Electric",
                "Mammoth Pictures",
                "Mooby Corp",
                "Gringotts",
                "Thrift Bank",
                "Flowers By Irene",
                "The Businessmens Club",
                "Osato Chemicals",
                "Transworld Consortium",
                "Universal Export",
                "United Fried Chicken",
                "Virtucon",
                "Kumatsu Motors",
                "Keedsler Motors",
                "Powell Motors",
                "Industrial Automation",
                "Sirius Cybernetics Corporation",
                "U.S. Robotics and Mechanical Men",
                "Colonial Movers",
                "Corellian Engineering Corporation",
                "Incom Corporation",
                "General Products",
                "Leeding Engines Ltd.",
                "Blammo",
                "Input, Inc.",
                "Mainway Toys",
                "Videlectrix",
                "Zevo Toys",
                "Ajax",
                "Axis Chemical Co.",
                "Barrytron",
                "Carrys Candles",
                "Cogswell Cogs",
                "Spacely Sprockets",
                "General Forge and Foundry",
                "Duff Brewing Company",
                "Dunder Mifflin",
                "General Services Corporation",
                "Monarch Playing Card Co.",
                "Krustyco",
                "Initech",
                "Roboto Industries",
                "Primatech",
                "Sonky Rubber Goods",
                "St. Anky Beer",
                "Stay Puft Corporation",
                "Vandelay Industries",
                "Wernham Hogg",
                "Gadgetron",
                "Burleigh and Stronginthearm",
                "BLAND Corporation",
                "Nordyne Defense Dynamics",
                "Petrox Oil Company",
                "Roxxon",
                "McMahon and Tate",
                "Sixty Second Avenue",
                "Charles Townsend Agency",
                "Spade and Archer",
                "Megadodo Publications",
                "Rouster and Sideways",
                "C.H. Lavatory and Sons",
                "Globo Gym American Corp",
                "The New Firm",
                "SpringShield",
                "Compuglobalhypermeganet",
                "Data Systems",
                "Gizmonic Institute",
                "Initrode",
                "Taggart Transcontinental",
                "Atlantic Northern",
                "Niagular",
                "Plow King",
                "Big Kahuna Burger",
                "Big T Burgers and Fries",
                "Chez Quis",
                "Chotchkies",
                "The Frying Dutchman",
                "Klimpys",
                "The Krusty Krab",
                "Monks Diner",
                "Milliways",
                "Minuteman Cafe",
                "Taco Grande",
                "Tip Top Cafe",
                "Moes Tavern",
                "Central Perk",
                "Chasers"
            };
        }

    }
}
