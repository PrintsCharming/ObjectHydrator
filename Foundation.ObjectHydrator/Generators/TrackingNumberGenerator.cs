using System;
using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class TrackingNumberGenerator : IGenerator<string>
    {
        private readonly Random random;

        public TrackingNumberGenerator(string carrier)
        {
            random = RandomSingleton.Instance.Random;
            Carrier = carrier;
        }

        public string Carrier { get; set; }

        public string Generate()
        {
            var sb = new StringBuilder();
            var chararray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            var sizeofcharray = chararray.Length - 1;
            switch (Carrier.ToLower())
            {
                default:
                case "ups":
                    sb.Append("1Z");
                    for (var i = 0; i < 18; i++)
                        sb.Append(chararray[random.Next(0, sizeofcharray)]);
                    break;
                case "fedex":
                    sb.Append("4");
                    for (var i = 0; i < 14; i++)
                        sb.Append(random.Next(0, 9));
                    break;
                case "usps":
                    sb.Append("91");
                    for (var i = 0; i < 20; i++)
                        sb.Append(random.Next(0, 9));
                    break;
            }
            return sb.ToString();
        }
    }
}