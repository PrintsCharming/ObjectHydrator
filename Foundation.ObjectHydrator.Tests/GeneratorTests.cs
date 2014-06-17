using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Foundation.ObjectHydrator.Generators;
using System.Text.RegularExpressions;

namespace Foundation.ObjectHydrator.Tests
{
    [TestFixture]
    public class GeneratorTests
    {
        private Random random = new Random();

        public enum Days { Sat = 1, Sun, Mon, Tue, Wed, Thu, Fri };

        [Test]
        public void AmericanAddressGenerator()
        {
            IAmericanAddressGenerator americanaddressgenerator = new AmericanAddressGenerator();
            string address = americanaddressgenerator.Generate(random);
            Assert.IsNotNull(address);
        }

        [Test]
        public void AmericanAddressGeneratorWithAttributeMap()
        {
            IAmericanAddressGenerator americanaddressgenerator = new AmericanAddressGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "123 Main" };
            string address = americanaddressgenerator.Generate(random, attmap);
            Assert.IsNotNull(address);
            Assert.AreEqual("123 Main", address);
        }

        [Test]
        public void AmericanAddressGeneratorWithStringOverride()
        {
            IAmericanAddressGenerator americanaddressgenerator = new AmericanAddressGenerator();
            string address = americanaddressgenerator.Generate(random, "123 Main");
            Assert.IsNotNull(address);
            Assert.AreEqual("123 Main", address);
        }

        [Test]
        public void AmericanCityGenerator()
        {
            IAmericanCityGenerator americancitygenerator = new AmericanCityGenerator();
            string city = americancitygenerator.Generate(random);
            Assert.IsNotNull(city);
        }

        [Test]
        public void AmericanCityGeneratorWithAttributeMap()
        {
            IAmericanCityGenerator americancitygenerator = new AmericanCityGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "Boston" };
            string city = americancitygenerator.Generate(random, attmap);
            Assert.AreEqual("Boston", city);
        }

        [Test]
        public void AmericanCityGeneratorWithStringOverride()
        {
            IAmericanCityGenerator americancitygenerator = new AmericanCityGenerator();
            string city = americancitygenerator.Generate(random, "Boston");
            Assert.AreEqual("Boston", city);
        }

        private bool CheckPhone(string phonetocheck)
        {
            Regex phonepattern = new Regex("^[\\(]{0,1}([0-9]){3}[\\)]{0,1}[ ]?([^0-1]){1}([0-9]){2}[ ]?[-]?[ ]?([0-9]){4}[ ]*((x){0,1}([0-9]){1,5}){0,1}$");
            bool boolval=phonepattern.IsMatch(phonetocheck);
            return boolval;
        }

        [Test]
        public void AmericanPhoneGenerator()
        {
            IAmericanPhoneGenerator americanphonegenerator = new AmericanPhoneGenerator();
            string americanphone = americanphonegenerator.Generate(random);
            Assert.IsNotNull(americanphone);
            Assert.IsTrue(CheckPhone(americanphone));
        }

        [Test]
        public void AmericanPhoneGeneratorTestWithAttributeMap()
        {
            IAmericanPhoneGenerator americanphonegenerator = new AmericanPhoneGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "(714)323-4567" };
            string phone=americanphonegenerator.Generate(random,attmap);
            Assert.IsNotNull(phone);
            Assert.IsTrue(CheckPhone(phone));
        }

        [Test]
        public void AmericanPhoneGeneratorTestWithStringOverride()
        {
            IAmericanPhoneGenerator americanphonegenerator = new AmericanPhoneGenerator();
            string phone = americanphonegenerator.Generate(random, "(714)323-4567");
            Assert.IsNotNull(phone);
            Assert.IsTrue(CheckPhone(phone));
        }

        private bool IsAmericanPostalCodeValid(string postalcode)
        {
            Regex postalcodepattern=new Regex("^\\d{5}$|^\\d{5}-\\d{4}$");
            return postalcodepattern.IsMatch(postalcode);
        }

        [Test]
        public void AmericanPostalCodeGeneratorNormal()
        {
            IAmericanPostalCodeGenerator americanpostalcodegenerator = new AmericanPostalCodeGenerator();
            string zip = americanpostalcodegenerator.Generate(random, false);
            Assert.IsNotNull(zip);
            Assert.DoesNotContain(zip, "-");
            Assert.IsTrue(IsAmericanPostalCodeValid(zip));
        }

        [Test]
        public void AmericanPostalCodeGeneratorUsePlusFour()
        {
            IAmericanPostalCodeGenerator americanpostalcodegenerator = new AmericanPostalCodeGenerator();
            string zip = americanpostalcodegenerator.Generate(random, true);
            Assert.IsNotNull(zip);
            
            Assert.Contains(zip, "-");
            string[] tester = zip.Split("-".ToCharArray());
            Assert.IsTrue(tester[1].Length == 4);
            Assert.IsTrue(IsAmericanPostalCodeValid(zip));
        }

        [Test]
        public void AmericanPostalCodeGeneratorNormalWithAttributeMap()
        {
            IAmericanPostalCodeGenerator americanpostalcodegenerator=new AmericanPostalCodeGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "12345", DefaultBoolValue=false };
            string postalcode = americanpostalcodegenerator.Generate(random, attmap);
            Assert.IsNotNull(postalcode);
            Assert.AreEqual("12345", postalcode);
            Assert.DoesNotContain(postalcode, "-");
            Assert.IsTrue(IsAmericanPostalCodeValid(postalcode));
        }

        [Test]
        public void AmericanPostalCodeGeneratorPlusFourWithAttributeMap()
        {
            IAmericanPostalCodeGenerator americanpostalcodegenerator = new AmericanPostalCodeGenerator();
            AttributeMap attmap = new AttributeMap { DefaultBoolValue = true };
            string postalcode = americanpostalcodegenerator.Generate(random, attmap);
            Assert.IsNotNull(postalcode);
            Assert.Contains(postalcode, "-");
            Assert.IsTrue(IsAmericanPostalCodeValid(postalcode));
        }

        [Test]
        public void AmericanPostalCodeGeneratorNormalWithStringOverride()
        {
            IAmericanPostalCodeGenerator americanpostalcodegenerator = new AmericanPostalCodeGenerator();
            string postalcode = americanpostalcodegenerator.Generate(random, "12345");
            Assert.IsNotNull(postalcode);
            Assert.IsTrue(IsAmericanPostalCodeValid(postalcode));
        }

        [Test]
        public void AmericanStateGenerator()
        {
            IAmericanStateGenerator americanstategenerator = new AmericanStateGenerator();
            string americanstate = americanstategenerator.Generate(random);
            Assert.IsNotNull(americanstate);
        }

        [Test]
        public void AmericanStateGeneratorWithAttributeMap()
        {
            IAmericanStateGenerator americanstategenerator = new AmericanStateGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "DD" };
            string americanstate = americanstategenerator.Generate(random, attmap);
            Assert.IsNotNull(americanstate);
            Assert.AreEqual("DD", americanstate);
        }

        [Test]
        public void AmericanStateGeneratorWithStringOverride()
        {
            IAmericanStateGenerator americanstategenerator = new AmericanStateGenerator();
            string americanstate = americanstategenerator.Generate(random, "DD");
            Assert.IsNotNull(americanstate);
            Assert.AreEqual("DD", americanstate);
        }

        [Test]
        public void BooleanGenerator()
        {
            IBooleanGenerator booleangeneerator = new BooleanGenerator();
            bool booltest = booleangeneerator.Generate(random);
            Assert.IsNotNull(booltest);
        }

        [Test]
        public void BooleanGeneratorWithAttributeMap()
        {
            IBooleanGenerator booleangenerator = new BooleanGenerator();
            AttributeMap attmap = new AttributeMap { DefaultBoolValue = true };
            bool booltest = booleangenerator.Generate(random, attmap);
            Assert.IsNotNull(booltest);
        }

        [Test]
        public void BusinessNameGenerator()
        {
            IBusinessNameGenerator businessnamegenerator = new BusinessNameGenerator();
            string businessname = businessnamegenerator.Generate(random);
            Assert.IsNotNull(businessname);
        }

        [Test]
        public void BusinessNameGeneratorWithAttributeMap()
        {
            IBusinessNameGenerator businessnamegenerator = new BusinessNameGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "Boogy Boo" };
            string businessname = businessnamegenerator.Generate(random, attmap);
            Assert.IsNotNull(businessname);
            Assert.AreEqual("Boogy Boo", businessname);
        }

        [Test]
        public void BusinessNameGeneratorWithStringOverride()
        {
            IBusinessNameGenerator businessnamegenerator = new BusinessNameGenerator();
            string businessname = businessnamegenerator.Generate(random, "Boogy Boo");
            Assert.IsNotNull(businessname);
            Assert.AreEqual("Boogy Boo", businessname);
        }

        [Test]
        public void DateGenerator()
        {
            IDateGenerator dategenerator = new DateGenerator();
            DateTime testdatetime = dategenerator.Generate(random, DateTime.MinValue, DateTime.MaxValue);
            Assert.IsNotNull(testdatetime);
        }

        [Test]
        public void DateGeneratorTestWithAttributeOverrideMap()
        {
            IDateGenerator dategenerator = new DateGenerator();
            AttributeMap attmap = new AttributeMap { DefaultDate = Convert.ToDateTime("1/1/2009") };
            DateTime testdatetime = dategenerator.Generate(random, attmap);
            Assert.IsNotNull(testdatetime);
            Assert.AreEqual(Convert.ToDateTime("1/1/2009"), testdatetime);
        }

        [Test]
        public void DateGeneratorTestWithAttributeRangeMap()
        {
            IDateGenerator dategenerator = new DateGenerator();
            AttributeMap attmap = new AttributeMap { MinDate = "1/1/2009", maxDate = "4/1/2009" };
            DateTime testdatetime = dategenerator.Generate(random, attmap);
            Assert.IsNotNull(testdatetime);
            Assert.Between(testdatetime, Convert.ToDateTime("1/1/2009"), Convert.ToDateTime("4/1/2009"));
        }

        [Test]
        public void DateGeneratorTestWithStringOverride()
        {
            IDateGenerator dategenerator = new DateGenerator();
            DateTime testdatetime = dategenerator.Generate(random, "1/2/2009");
            Assert.IsNotNull(testdatetime);
            Assert.AreEqual(Convert.ToDateTime("1/2/2009"), testdatetime);
        }

        private bool IsEmailAddressValid(string emailaddress)
        {
            Regex emailpattern = new Regex("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
            return emailpattern.IsMatch(emailaddress);
        }

        [Test]
        public void EmailAddressGenerator()
        {
            IEmailAddressGenerator emailaddressgenerator = new EmailAddressGenerator();
            IFirstNameGenerator fname=new FirstNameGenerator();
            ILastNameGenerator lname=new LastNameGenerator();
            string emailaddress=emailaddressgenerator.Generate(random,fname,lname);
            Assert.IsNotNull(emailaddress);
              Assert.IsTrue(IsEmailAddressValid(emailaddress));
        }

        [Test]
        public void EmailAddressGeneratorAttributeMap()
        {
            IEmailAddressGenerator emailaddressgenerator = new EmailAddressGenerator();
            IFirstNameGenerator fname = new FirstNameGenerator();
            ILastNameGenerator lname = new LastNameGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "abc@123.com" };
            string emailaddress = emailaddressgenerator.Generate(random, fname, lname, attmap);
            Assert.IsNotNull(emailaddress);
            Assert.IsTrue(IsEmailAddressValid(emailaddress));
        }

        [Test]
        public void EmailAddressGeneratorStringOverride()
        {
            IEmailAddressGenerator emailaddressgenerator = new EmailAddressGenerator();
            IFirstNameGenerator fname = new FirstNameGenerator();
            ILastNameGenerator lname = new LastNameGenerator();
            string emailaddress = emailaddressgenerator.Generate(random, fname, lname, "abc@123.com");
            Assert.IsNotNull(emailaddress);
            Assert.AreEqual("abc@123.com", emailaddress);
        }

        [Test]
        public void EnumGenerator()
        {
            IEnumGenerator enumgenerator = new EnumGenerator();
            Days daything = (Days)enumgenerator.Generate(random, typeof(Days));
            Assert.IsNotNull(daything);
        }

        [Test]
        public void First_Name_Generator()
        {
            IFirstNameGenerator fnamegenerator = new FirstNameGenerator();
            string generatedname = fnamegenerator.Generate(random);
            Assert.IsNotNull(generatedname);
        }

        [Test]
        public void FirstNameGeneratorWithAttributeMap()
        {
            IFirstNameGenerator fnamegenerator = new FirstNameGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "Ryan" };
            string firstname = fnamegenerator.Generate(random, attmap);
            Assert.IsNotNull(firstname);
            Assert.AreEqual("Ryan", firstname);
        }

        [Test]
        public void FirstNameGeneratorStringOverride()
        {
            IFirstNameGenerator fnamegenerator = new FirstNameGenerator();
            string firstname = fnamegenerator.Generate(random, "Ryan");
            Assert.IsNotNull(firstname);
            Assert.AreEqual("Ryan", firstname);
        }   

        [Test]
        public void IPAddressGenerator()
        {
            IIPAddressGenerator ipaddressgenerator = new IPAddressGenerator();
            string ipaddy = ipaddressgenerator.Generate(random, string.Empty, string.Empty, string.Empty, string.Empty);
            Assert.IsNotNull(ipaddy);
            Regex testpattern=new Regex("^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
            Assert.IsTrue(testpattern.IsMatch(ipaddy));
        }

        [Test]
        public void Last_Name_Generator()
        {
            ILastNameGenerator lnamegenerator = new LastNameGenerator();
            string generatedlastname = lnamegenerator.Generate(random);
            Assert.IsNotNull(generatedlastname);
        }

        [Test]
        public void LastNameGeneratorAttributeMap()
        {
            ILastNameGenerator lnamegenerator = new LastNameGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "Smith" };
            string generatedlastname = lnamegenerator.Generate(random, attmap);
            Assert.IsNotNull(generatedlastname);
            Assert.AreEqual("Smith", generatedlastname);
        }

        [Test]
        public void LastNameGeneratorWithStringOverride()
        {
            ILastNameGenerator lnamegenerator = new LastNameGenerator();
            string generatedlastname = lnamegenerator.Generate(random, "Smith");
            Assert.IsNotNull(generatedlastname);
            Assert.AreEqual("Smith", generatedlastname);
        }

        [Test]
        public void NumberGenerator()
        {
            INumberGenerator numbergenerator = new NumberGenerator();
            int testnumber = numbergenerator.Generate(random, 0, 1000);
            Assert.Between(testnumber, 0, 1000);
        }

        [Test]
        public void NumberGeneratorWithAttributeRangeMap()
        {
            INumberGenerator numbergenerator = new NumberGenerator();
            AttributeMap attmap = new AttributeMap { MinInt = 0, MaxInt = 100 };
            int testnumber = numbergenerator.Generate(random, attmap);
            Assert.IsNotNull(testnumber);
            Assert.Between(testnumber, 0, 100);
        }

        [Test]
        public void NumberGeneratorWithAttributeOverride()
        {
            INumberGenerator numbergenerator = new NumberGenerator();
            AttributeMap attmap = new AttributeMap { DefaultInt = 99 };
            int testnumber = numbergenerator.Generate(random, attmap);
            Assert.IsNotNull(testnumber);
            Assert.AreEqual(99, testnumber);
        }

        [Test]
        public void NumberGeneratorWithIntOverride()
        {
            INumberGenerator numbergenerator = new NumberGenerator();
            int testnumber = numbergenerator.Generate(random, 99);
            Assert.IsNotNull(testnumber);
            Assert.AreEqual(99, testnumber);
        }

        private bool IsWebsiteAddressValid(string webaddy)
        {
            Regex testpattern = new Regex("((mailto\\:|(news|(ht|f)tp(s?))\\://){1}\\S+)");
            return testpattern.IsMatch(webaddy);
        }

        [Test]
        public void WebsiteAddressGenerator()
        {
            IWebsiteAddressGenerator urlgenerator = new WebsiteAddressGenerator();
            IBusinessNameGenerator biznamegenerator = new BusinessNameGenerator();
            string testaddress = urlgenerator.Generate(random, biznamegenerator);
            Assert.IsNotNull(testaddress);
            Assert.IsTrue(IsWebsiteAddressValid(testaddress));

        }

        [Test]
        public void WebsiteAddressGeneratorWithAttribue()
        {
            IWebsiteAddressGenerator urlgenerator = new WebsiteAddressGenerator();
            IBusinessNameGenerator biznamegenerator = new BusinessNameGenerator();
            AttributeMap attmap = new AttributeMap { GeneratorDefaultValue = "http://www.yahoo.com" };
            string websitename = urlgenerator.Generate(random, biznamegenerator, attmap);
            Assert.IsNotNull(websitename);
            Assert.AreEqual("http://www.yahoo.com", websitename);
            Assert.IsTrue(IsWebsiteAddressValid(websitename));
        }

        [Test]
        public void WebsiteAddressGeneratorWithString()
        {
            IWebsiteAddressGenerator urlgenerator = new WebsiteAddressGenerator();
            IBusinessNameGenerator biznamegenerator = new BusinessNameGenerator();
            string websitename = urlgenerator.Generate(random, biznamegenerator, "http://www.yahoo.com");
            Assert.IsNotNull(websitename);
            Assert.AreEqual("http://www.yahoo.com",websitename);
            Assert.IsTrue(IsWebsiteAddressValid(websitename));
        }
    }
}
