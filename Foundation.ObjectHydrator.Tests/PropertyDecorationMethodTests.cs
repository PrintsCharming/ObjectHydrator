using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Foundation.ObjectHydrator.Tests.SampleClass;

namespace Foundation.ObjectHydrator.Tests
{
   
    [TestFixture]
    public class PropertyDecorationMethodTests
    {       
        private FillMe<Customer> generator = new FillMe<Customer>();
        

        [Test]
        public void AmericanAddressTest()
        {
            
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerAddress);
            Assert.IsFalse(mycustomer.CustomerAddress == string.Empty);
        }

        [Test]
        public void AmericanCityTest()
        {
            
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerCity);
            Assert.IsFalse(mycustomer.CustomerCity == string.Empty);
        }

        [Test]
        public void AmericanPhoneTest()
        {
            
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerPhone);
            Assert.IsFalse(mycustomer.CustomerPhone == string.Empty);

        }

        [Test]
        public void AmericanPostalCodeTestNoPlusFour()
        {
            
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerPostalCode);
            Assert.DoesNotContain(mycustomer.CustomerPostalCode, "-");
        }

        [Test]
        public void AmericanStateGeneratorTest()
        {
            
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerAmericanState);
            Assert.IsFalse(mycustomer.CustomerAmericanState == string.Empty);
        }

        [Test]
        public void BooleanGeneratorTest()
        {
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerIsActive);
            
        }



        [Test]
        public void BusinessNameGeneratorTest()
        {
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerBusinessName);
            Assert.IsFalse(mycustomer.CustomerBusinessName == string.Empty);
        }

        [Test]
        public void DateGenerator()
        {

            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerCreatedDate);

        }

        [Test]
        public void EmailAddressGenerator()
        {
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerEmailAddress);
            Assert.IsFalse(mycustomer.CustomerEmailAddress == string.Empty);
        }

        [Test]
        public void EnumTest()
        {


            Customer mycust = generator.GetSingle();
            Assert.IsNotNull(mycust);

        }

        [Test]
        public void SimpleFirstNameTest()
        {
            
            Customer mynewcustomer = generator.GetSingle();
            Assert.IsNotNull(mynewcustomer.CustomerFirstName);
            Assert.IsFalse(mynewcustomer.CustomerFirstName == string.Empty);
        }


        [Test]
        public void SimpleLastNameTest()
        {
            
            Customer mynewcustomer = generator.GetSingle();
            Assert.IsNotNull(mynewcustomer.CustomerLastName);
            Assert.IsFalse(mynewcustomer.CustomerLastName == string.Empty);
        }


        [Test]
        public void NumberGenerator()
        {
            
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerNumberOfOrders);
        }

        [Test]
        public void WebsiteAddressGenerator()
        {
            Customer mycustomer = generator.GetSingle();
            Assert.IsNotNull(mycustomer.CustomerWebsite);
            Assert.IsFalse(mycustomer.CustomerWebsite == string.Empty);
        }

        [Test]
        public void MultiTest()
        {
            
            IList<Customer> mycustomerlist = generator.GetList(5);
            Assert.IsTrue(mycustomerlist.Count == 5);

        }

        
        [Test]
        public void SimpTest()
        {
            
            Customer cust = new Customer();
            Customer gen = generator.SingleTest(cust);
            Assert.IsNotNull(gen.CustomerFirstName);

        }

        [Test]
        public void SimpTestBooleanPreFilled()
        {
            Customer cust = new Customer { CustomerIsActive = true };
            Customer gen = generator.SingleTest(cust);
            Assert.IsTrue(gen.CustomerIsActive == true);

        }

        [Test]
        public void SimpleDoNotOverrideDefaultValueTest()
        {
            Customer cust = new Customer { CustomerFirstName = "Guido" };
            Customer gen = generator.SingleTest(cust);
            Assert.IsTrue(gen.CustomerFirstName == "Guido");
        }



    }
}
