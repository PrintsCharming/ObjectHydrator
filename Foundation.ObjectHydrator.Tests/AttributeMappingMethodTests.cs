using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Foundation.ObjectHydrator.Tests.SampleClass;

namespace Foundation.ObjectHydrator.Tests
{
    [TestFixture]
    public class AttributeMappingMethodTests
    {
        [Test]
        public void UsingAttributeMapSingle()
        {
            FillMe<SampleClass.CustomerNoAttibutes> generator = new FillMe<CustomerNoAttibutes>();
            IList<AttributeMap> attmaplist = new List<AttributeMap>();
            AttributeMap firstname = new AttributeMap { GeneratorName = "FirstName", PropName = "CustomerFirstName", GeneratorDefaultValue = "Ryan" };
            attmaplist.Add(firstname);
            AttributeMap lastname = new AttributeMap { GeneratorName = "LastName", PropName = "CustomerLastName" };
            attmaplist.Add(lastname);
            AttributeMap ordernumber = new AttributeMap { GeneratorName = "NumberGen", PropName = "CustomerNumberOfOrders" };
            attmaplist.Add(ordernumber);
            AttributeMap phone = new AttributeMap { GeneratorName = "AmericanPhone", PropName = "CustomerPhone" };
            attmaplist.Add(phone);
            AttributeMap isactive = new AttributeMap { GeneratorName = "BooleanGenerator", PropName = "CustomerIsActive", DefaultBoolValue = true };
            attmaplist.Add(isactive);
            AttributeMap ipaddress = new AttributeMap { GeneratorName = "IPAddressGenerator", PropName = "CustomerIPAddress", GeneratorDefaultValue = "25...25" };
            attmaplist.Add(ipaddress);
            AttributeMap address = new AttributeMap { GeneratorName = "AmericanAddress", PropName = "CustomerAddress" };
            attmaplist.Add(address);
            IList<CustomerNoAttibutes> mycustomer = generator.GetList(20, attmaplist);
            Assert.IsNotNull(mycustomer);
        }
    }
}
