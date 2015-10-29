using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundation.ObjectHydrator.Tests.HydratorTests {
	[TestFixture]
	public class GenericHydrator_SimpleCustomer_Tests {

		Hydrator hydrator;

		[TestFixtureSetUp]
		public void Initialize() {
			hydrator = new Hydrator(typeof(SimpleCustomer));
		}

		[Test]
		public void Generic_CanGetSingleSimpleCustomer() {
			var customer = hydrator.GetSingle() as SimpleCustomer;
			Assert.IsTrue(!String.IsNullOrEmpty(customer.Description), "Customer Description should exist.");
			TestUtilities.DumpSimpleCustomer(customer);
		}

		[Test]
		public void Generic_CanGetSingleRestrictedDescriptionCustomer() {
			var customer = hydrator.GetSingle() as SimpleCustomer;

			Assert.IsTrue(!String.IsNullOrEmpty(customer.Description), "Customer Description should exist.");
			Assert.IsTrue(customer.Description.Length <= 5, "Length not restricted");
			TestUtilities.DumpSimpleCustomer(customer);
		}

		[Test]
		public void Generic_CanGetList() {
			var listCount = 50;
			var customers = hydrator.GetList(listCount) as IList;

			Assert.IsTrue(customers.Count == listCount, "Customer count is wrong.");

			//DumpCustomers(customers);
		}

		[Test]
		public void Generic_CanGetDescription() {
			var customer = hydrator.GetSingle() as SimpleCustomer;

			Assert.IsTrue(!String.IsNullOrEmpty(customer.Description), "Customer Description should exist.");

			TestUtilities.DumpSimpleCustomer(customer);
		}



	}
}
