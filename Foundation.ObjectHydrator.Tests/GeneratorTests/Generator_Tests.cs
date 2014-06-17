using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Foundation.ObjectHydrator.Interfaces;
using Foundation.ObjectHydrator.Generators;
using System.Text.RegularExpressions;
using System.Xml;


namespace Foundation.ObjectHydrator.Tests.GeneratorTests
{
    [TestFixture]
    public class Generator_Tests
    {
        [Test]
        public void FirstNameGeneratorTest()
        {
            IGenerator<string> fng = new FirstNameGenerator();
            string firstname = (string)fng.Generate();
            Assert.IsNotNull(firstname);
        }

        [Test]
        public void LastNameGeneratorTest()
        {
            IGenerator<string> lng = new LastNameGenerator();
            string lastname = (string)lng.Generate();
            Assert.IsNotNull(lastname);
        }

        [Test]
        public void CompanyNameGeneratorTest()
        {
            IGenerator<string> cng = new CompanyNameGenerator();
            string companyname = (string)cng.Generate();
            Assert.IsNotNull(companyname);
        }

        [Test]
        public void DateTimeGeneratorWithDefaultTest()
        {
            IGenerator<DateTime> dtg = new DateTimeGenerator();
            DateTime checkme = (DateTime)dtg.Generate();
            DateTime current=DateTime.Now;
            Assert.IsNotNull(checkme);
            Assert.Between<DateTime>(checkme, current.AddYears(-10), current.AddYears(10));
        }

        [Test]
        public void DateTimeGeneratorWithOverrideValues()
        {
            
            DateTime mymin = Convert.ToDateTime("1/1/1972");
            DateTime mymax = Convert.ToDateTime("1/1/1980");

            IGenerator<DateTime> dtg = new DateTimeGenerator(mymin,mymax);
            DateTime checkme = (DateTime)dtg.Generate();
            Assert.IsNotNull(checkme);
            Assert.Between<DateTime>(checkme, mymin, mymax);
        }

        [Test]
        public void DefaultGenerator()
        {
            string setme = "hi";
            IGenerator<string> defgen = new DefaultGenerator<string>(setme);
            string checkme = (string)defgen.Generate();
            Assert.IsNotNull(checkme);
            Assert.AreEqual(checkme, setme);

        }

        [Test]
        public void DoubleGeneratorWithDefaultValues()
        {
            IGenerator<double> doublegen = new DoubleGenerator();
            double checkme = (double)doublegen.Generate();
            Assert.IsNotNull(checkme);
            Assert.Between<double>(checkme, 0.00, 100.00);
        }

        [Test]
        public void EnumGeneratorTest()
        {
            //IGenerator enumgen = new EnumGenerator();

        }

        [Test]
        public void IntegerGeneratorWithDefaultTest()
        {
            IGenerator<int> intgen = new IntegerGenerator();
            int checkme = (int)intgen.Generate();
            Assert.IsNotNull(checkme);
            Assert.Between<int>(checkme, 0, 100);
        }

        [Test]
        public void IntegerGeneratorWithOverrideTest()
        {
            int min = 5;
            int max = 20;
            IGenerator<int> intgen = new IntegerGenerator(min,max);
            int checkme = (int)intgen.Generate();
            Assert.IsNotNull(checkme);
            Assert.Between<int>(checkme, min, max);

        }

        [Test]
        public void ListGeneratorTest()
        {
            IList<string> testlist = new List<string>();
            testlist.Add("hey");
            testlist.Add("yay");
            testlist.Add("nay");
            IGenerator<string> listgen = new FromListGetSingleGenerator<string>(testlist);
            string checkme = (string)listgen.Generate();
            Assert.IsNotNull(checkme);
            Assert.IsTrue(testlist.Contains(checkme));
        }

        [Test]
        public void NullGeneratorTest()
        {
            IGenerator<object> nullgen = new NullGenerator();
            string checkme = (string)nullgen.Generate();
            Assert.IsNull(checkme);
        }

        private bool CheckPhone(string phonetocheck)
        {
            Regex phonepattern = new Regex("^[\\(]{0,1}([0-9]){3}[\\)]{0,1}[ ]?([^0-1]){1}([0-9]){2}[ ]?[-]?[ ]?([0-9]){4}[ ]*((x){0,1}([0-9]){1,5}){0,1}$");
            bool boolval = phonepattern.IsMatch(phonetocheck);
            return boolval;
        }

        [Test]
        public void AmericanPhoneGeneratorTest()
        {
            IGenerator<string> phonegen = new AmericanPhoneGenerator();
            string phone = (string)phonegen.Generate();
            Assert.IsNotNull(phone);
            Assert.IsTrue(CheckPhone(phone));
        }

        private bool IsAmericanPostalCodeValid(string postalcode)
        {
            Regex postalcodepattern = new Regex("^\\d{5}$|^\\d{5}-\\d{4}$");
            return postalcodepattern.IsMatch(postalcode);
        }

        [Test]
        public void AmericanPostalCodeGenerator()
        {
            IGenerator<string> postalgen = new AmericanPostalCodeGenerator(1);
            string zipcode = (string)postalgen.Generate();
            Assert.IsNotNull(zipcode);
            Assert.IsTrue(IsAmericanPostalCodeValid(zipcode));


        }

        [Test]
        public void TextGeneratorTest()
        {
            IGenerator<string> textgen = new TextGenerator();
            string text = (string)textgen.Generate();
            Assert.IsNotNull(text);

        }

        
        [Test]
        public void AmericanAddressGeneratorTest()
        {
            IGenerator<string> americanaddy = new AmericanAddressGenerator();
            string address = (string)americanaddy.Generate();
            Assert.IsNotNull(address);
        }

        
        [Test]
        public void AmericanCityGeneratorTest()
        {
            IGenerator<string> americancity = new AmericanCityGenerator();
            string city=(string)americancity.Generate();
            Assert.IsNotNull(city);
        }

        [Test]
        public void AmericanStateGeneratorTest()
        {
            IGenerator<string> americanstate = new AmericanStateGenerator();
            string state = (string)americanstate.Generate();
            Assert.IsNotNull(state);
        }

        public bool IsValidIPAddress(string ipaddress)
        {
            Regex testpattern = new Regex("^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
            return testpattern.IsMatch(ipaddress);
        }

        [Test]
        public void IPAddressGeneratorTest()
        {
            IGenerator<string> ipaddress = new IPAddressGenerator();
            string ipaddy = (string)ipaddress.Generate();
            Assert.IsNotNull(ipaddy);
            Assert.IsTrue(IsValidIPAddress(ipaddy));
        }

        private bool IsWebsiteAddressValid(string webaddy)
        {
            Regex testpattern = new Regex("((mailto\\:|(news|(ht|f)tp(s?))\\://){1}\\S+)");
            return testpattern.IsMatch(webaddy);
        }

        [Test]
        public void WwebsiteAddressGeneratorTest()
        {
            IGenerator<string> websitegen = new WebsiteGenerator();
            string site = (string)websitegen.Generate();
            Assert.IsNotNull(site);
            Assert.IsTrue(IsWebsiteAddressValid(site));
        }

        [Test]
        public void GenderGenerator()
        {
            
            IGenerator<string> gendergenerator = new GenderGenerator();
            string gender = (string)gendergenerator.Generate();
            Assert.IsNotNull(gender);
        }

        [Test]
        public void CreditCardTypeGenerator()
        {
            IGenerator<string> cardtypegenerator = new CreditCardTypeGenerator();
            string cardtype = (string)cardtypegenerator.Generate();
            Assert.IsNotNull(cardtype);
        }

        [Test]
        public void CountryCodeGenerator()
        {
            IGenerator<string> countrycodegenerator = new CountryCodeGenerator();
            string countrycode = (string)countrycodegenerator.Generate();
            Assert.IsNotNull(countrycode);
        }

        private bool IsEmailAddressValid(string emailaddress)
        {
            Regex emailpattern = new Regex("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
            return emailpattern.IsMatch(emailaddress);
        }

        [Test]
        public void EmailAddressGenerator()
        {
            IGenerator<string> emailaddressgenerator = new EmailAddressGenerator();
            string emailaddy = (string)emailaddressgenerator.Generate();
            Assert.IsNotNull(emailaddy);
            Assert.IsTrue(IsEmailAddressValid(emailaddy));
        }

        [Test]
        public void BooleanGenerator()
        {
            IGenerator<bool> booleangenerator = new BooleanGenerator();
            bool boolvalue = (bool)booleangenerator.Generate();
            Assert.IsNotNull(boolvalue);
            Assert.IsInstanceOfType(typeof(bool), boolvalue);
        }

        [Test]
        public void FedExTrackingNumberGenerator()
        {
            IGenerator<string> trackingnumbergenerator = new TrackingNumberGenerator("FedEx");
            string tracknumber = (string)trackingnumbergenerator.Generate();
            Assert.IsNotNull(tracknumber);
            Assert.IsTrue(tracknumber.Length == 15);
            Assert.StartsWith(tracknumber, "4");

        }
        [Test]
        public void UPSTrackingNumberGenerator()
        {
            IGenerator<string> trackingnumbergenerator = new TrackingNumberGenerator("UPS");
            string tracknumber = (string)trackingnumbergenerator.Generate();
            Assert.IsNotNull(tracknumber);
            Assert.IsTrue(tracknumber.Length == 20);
            Assert.StartsWith(tracknumber, "1Z");

        }

        [Test]
        public void USPSTrackingNumberGenerator()
        {
            IGenerator<string> trackingnumbergenerator = new TrackingNumberGenerator("USPS");
            string tracknumber = (string)trackingnumbergenerator.Generate();
            Assert.IsNotNull(tracknumber);
            Assert.IsTrue(tracknumber.Length == 22);
            Assert.StartsWith(tracknumber, "91");

        }

        [Test]
        public void CanGetDefaultCCVFromGenerator()
        {
            IGenerator<string> ccvGenerator = new CCVGenerator("");
            string ccv = (string)ccvGenerator.Generate();
            Assert.IsNotNull(ccv);
        }

        [Test]
        public void PasswordWithDefaultLengthGenerator()
        {
            IGenerator<string> pwGen = new PasswordGenerator();
            string pw = (string)pwGen.Generate();
            Assert.IsNotNull(pw);
            Assert.IsTrue(pw.Length == 10);
        }

        [Test]
        public void PasswordWithCustomLengthGenerator()
        {
            int length = 99;
            IGenerator<string> pwGen = new PasswordGenerator(length);
            string pw = (string)pwGen.Generate();
            Assert.IsNotNull(pw);
            Assert.AreEqual(length, pw.Length);
        }
    }
}
