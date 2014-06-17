using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.ObjectHydrator.GeneratorTypes;

namespace Foundation.ObjectHydrator.Tests.SampleClass
{
    public class Customer
    {
        public enum Days { Sat = 1, Sun, Mon, Tue, Wed, Thu, Fri };

        [FirstName("")]
        public string CustomerFirstName { get; set; }

        [LastName("Jones")]
        public string CustomerLastName { get; set; }

        [DateGen("1/1/2009","1/1/2010")]
        public DateTime CustomerCreatedDate { get; set; }

        [AmericanPhone("")]
        public string CustomerPhone { get; set; }

        [NumberGen(1,23)]
        public int CustomerNumberOfOrders { get; set; }

        [AmericanAddress("")]
        public string CustomerAddress { get; set; }

        [AmericanCity("")]
        public string CustomerCity { get; set; }

        [AmericanPostalCode(false)]
        public string CustomerPostalCode { get; set; }

        [EnumGen(typeof(Days))]
        public Days CustomerDay { get; set; }

        [BoolGen(false)]
        public bool? CustomerIsActive { get; set; }

        [AmericanState("")]
        public string CustomerAmericanState { get; set; }

        [EmailAddress("")]
        public string CustomerEmailAddress { get; set; }

        [BusinessName("")]
        public string CustomerBusinessName { get; set; }

        [WebsiteAddress("")]
        public string CustomerWebsite { get; set; }

        [IPAddress("25", "25", "25", "25")]
        public string CustomerIPAddress { get; set; }

    }
}
