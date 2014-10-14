
﻿using System.Collections.Generic;
using System.Diagnostics;
using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class Hydrator_ComplexCustomer_Tests
    {
        [Test]
        public void CanLoadSingleComplexCustomer()
        {
            int[] values = {1, 2, 3};
            var args = new object[] {values};
            var hydrator = new Hydrator<ComplexCustomer>()
                .With(x => x.HomeAddress, new TypeGenerator<Address>());
            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer);
            Assert.IsNotNull(customer.HomeAddress, "CustomerAddress is null");
        }

        [Test]
        public void CanGetListOfComplexCustomer()
        {
            int[] values = {1, 2, 3};
            var args = new object[] {values};
            var hydrator = new Hydrator<ComplexCustomer>()
                .With(x => x.HomeAddress, new TypeGenerator<Address>());
            var customerlist = hydrator.GetList(10);
            Assert.IsNotNull(customerlist);
            Assert.IsTrue(customerlist.Count == 10);
            Assert.IsNotNull(customerlist[1].HomeAddress, "CustomerAddress is null");
        }


        [Test]
        public void CanLoadSingleComplexCustomerWithAddressList()
        {
            var listSize = 6;
            var args = new object[] {listSize};

            var customer = new Hydrator<ComplexCustomer>()
                .With(x => x.Addresses, new ListGenerator<Address>(listSize))
                .With(x => x.FirstName, "Test")
                .GetSingle();


            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.Addresses.Count == listSize,
                string.Format("Customer.Address.Count [{0}] is not expected value of [{1}].",
                    customer.Addresses.Count, listSize));

            Trace.WriteLine("Addresses Generated...");
            foreach (Address address in customer.Addresses)
            {
                Trace.WriteLine(address.AddressLine1);
            }
        }

        [Test]
        public void CanLoadSingleComplexCustomerWithRandomCountOfAddressList()
        {
            var args = new object[] {};
            var hydrator = new Hydrator<ComplexCustomer>()
                .With(x => x.Addresses, ListGenerator<Address>.RandomLength());

            var customer = hydrator.GetSingle();
            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.Addresses.Count > 0);

            Trace.WriteLine("Addresses Generated...");
            foreach (Address address in customer.Addresses)
            {
                Trace.WriteLine(address.AddressLine1);
            }
        }

        [Test]
        public void CanLoadSingleComplexCustomerWithCustumTypeMappers()
        {
            var lastNameDefault = "Lennon";
            var hy = new Hydrator<ComplexCustomer>()
                .ForAll<Address>(new Hydrator<Address>())
                .For<IList<Address>>(new Map<IList<Address>>().Using(new ListGenerator<Address>(10)))
                .For<string>(new Map<string>().Matching(info => info.Name.ToLower() == "lastname").Using(lastNameDefault))
                .GetSingle();

            Assert.AreEqual(lastNameDefault, hy.LastName);
        }
    }
}