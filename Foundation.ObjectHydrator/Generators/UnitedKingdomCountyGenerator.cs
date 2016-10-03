using System;
using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class UnitedKingdomCountyGenerator : IGenerator<string>
    {
        private readonly Random _random;
        private IList<string> _countyNames = new List<string>();

        public UnitedKingdomCountyGenerator()
        {
            _random = RandomSingleton.Instance.Random;
            LoadCountyNames();
        }

        private void LoadCountyNames()
        {
            _countyNames = new List<string>()
            {
                "London",
                "Bedfordshire",
                "Buckinghamshire",
                "Cambridgeshire",
                "Cheshire",
                "Cornwall and Isles of Scilly",
                "Cumbria",
                "Derbyshire",
                "Devon",
                "Dorset",
                "Durham",
                "East Sussex",
                "Essex",
                "Gloucestershire",
                "Greater London",
                "Greater Manchester",
                "Hampshire",
                "Hertfordshire",
                "Kent",
                "Lancashire",
                "Leicestershire",
                "Lincolnshire",
                "Merseyside",
                "Norfolk",
                "North Yorkshire",
                "Northamptonshire",
                "Northumberland",
                "Nottinghamshire",
                "Oxfordshire",
                "Shropshire",
                "Somerset",
                "South Yorkshire",
                "Staffordshire",
                "Suffolk",
                "Surrey",
                "Tyne and Wear",
                "Warwickshire",
                "West Midlands",
                "West Sussex",
                "West Yorkshire",
                "Wiltshire",
                "Worcestershire",
                "Flintshire",
                "Glamorgan",
                "Merionethshire",
                "Monmouthshire",
                "Montgomeryshire",
                "Pembrokeshire",
                "Radnorshire",
                "Anglesey",
                "Breconshire",
                "Caernarvonshire",
                "Cardiganshire",
                "Carmarthenshire",
                "Denbighshire",
                "Kirkcudbrightshire",
                "Lanarkshire",
                "Midlothian",
                "Moray",
                "Nairnshire",
                "Orkney",
                "Peebleshire",
                "Perthshire",
                "Renfrewshire",
                "Ross & Cromarty",
                "Roxburghshire",
                "Selkirkshire",
                "Shetland",
                "Stirlingshire",
                "Sutherland",
                "West Lothian",
                "Wigtownshire",
                "Aberdeenshire",
                "Angus",
                "Argyll",
                "Ayrshire",
                "Banffshire",
                "Berwickshire",
                "Bute",
                "Caithness",
                "Clackmannanshire",
                "Dumfriesshire",
                "Dumbartonshire",
                "East Lothian",
                "Fife",
                "Inverness",
                "Kincardineshire",
                "Kinross-shire"
            };
        }

        public string Generate()
        {
            return _countyNames[_random.Next(0, _countyNames.Count)];
        }
    }
}