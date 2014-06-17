using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.ObjectHydrator.Tests.SampleClass
{
    class CustomerNoAttibutes
    {
        
        public string CustomerFirstName { get; set; }

        
        public string CustomerLastName { get; set; }

        
        public DateTime CustomerCreatedDate { get; set; }

        
        public string CustomerPhone { get; set; }

        public int CustomerNumberOfOrders { get; set; }

        public bool CustomerIsActive { get; set; }

        public string CustomerIPAddress { get; set; }

        public string CustomerAddress { get; set; }
    }
}
