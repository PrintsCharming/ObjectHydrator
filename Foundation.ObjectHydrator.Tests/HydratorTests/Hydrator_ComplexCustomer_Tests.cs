using System;
using System.Collections.Generic;
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
        public void CanLoadSingleComplexCustomerWithPhoneList()
        {
            var listSize = 6;
            var args = new object[] {listSize};

            var customer = new Hydrator<ComplexCustomer>()
                .With(x => x.PhoneNumbers, new ArrayGenerator<string>(listSize, new AmericanPhoneGenerator()))
                .GetSingle();


            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.PhoneNumbers.Length == listSize,
                  string.Format("customer.PhoneNumbers.Length [{0}] is not expected value of [{1}].",
                  customer.PhoneNumbers.Length, listSize));

            Trace.WriteLine("Addresses Generated...");
            foreach (string ph in customer.PhoneNumbers)
            {
                Trace.WriteLine(ph);
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

        [Test]
        public void ThrowExceptionForAbstractTypeGeneration()
        {
            var abstractGenerator = new TypeGenerator<Animal>();
            
            Assert.Throws<MissingMethodException>(() => abstractGenerator.Generate());
        }

        [Test]
        public void CanLoadSingleComplexCustomerWithAbstractTypeMapper()
        {
            var hy = new Hydrator<ComplexCustomer>();

            var customer = hy.GetSingle();

            Assert.IsNotNull(customer);
            
            //As Animal is an abstract class, it will not be instantiate by default
            Assert.IsNull(customer.Pet, "Abstract class cannot be instantiate");

            //Set up the generator for abstract type
            hy = hy.With(x => x.Pet, new TypeGenerator<Dog>());
            customer = hy.GetSingle();

            Assert.IsNotNull(customer.Pet);
            Assert.IsInstanceOf<Dog>(customer.Pet);
        }
    }
}