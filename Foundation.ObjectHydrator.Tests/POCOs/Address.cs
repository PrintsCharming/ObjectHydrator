using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.ObjectHydrator.Tests.POCOs
{
    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Address()
        {
            City = "Test City";
        }

    }
}
