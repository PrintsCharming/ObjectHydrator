using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using NUnit.Framework;


namespace Foundation.ObjectHydrator.Tests.GeneratorTests
{
    [TestFixture]
    public class Generator_Tests
    {
        [Test]
        public void FirstNameGeneratorTest()
        {
            IGenerator<string> fng = new FirstNameGenerator();
            var firstname = (string) fng.Generate();
            Assert.That(firstname,Is.Not.Empty);
        }

        [Test]
        public void LastNameGeneratorTest()
        {
            IGenerator<string> lng = new LastNameGenerator();
            var lastname = (string) lng.Generate();
            Assert.That(lastname, Is.Not.Empty);
        }

        [Test]
        public void CompanyNameGeneratorTest()
        {
            IGenerator<string> cng = new CompanyNameGenerator();
            var companyname = (string) cng.Generate();
            Assert.That(companyname, Is.Not.Empty);
        }

        [Test]
        public void DateTimeGeneratorWithDefaultTest()
        {
            IGenerator<DateTime> dtg = new DateTimeGenerator();
            var checkme = (DateTime) dtg.Generate();
            var current = DateTime.Now;
            
            Assert.That(checkme, Is.InRange(current.AddYears(-10), current.AddYears(10)));
        }

        [Test]
        public void DateTimeGeneratorWithOverrideValues()
        {
            var mymin = Convert.ToDateTime("1/1/1972");
            var mymax = Convert.ToDateTime("1/1/1980");

            IGenerator<DateTime> dtg = new DateTimeGenerator(mymin, mymax);
            var checkme = (DateTime) dtg.Generate();
            Assert.That(checkme, Is.InRange(mymin, mymax));
        }

        [Test]
        public void DefaultGenerator()
        {
            var setme = "hi";
            IGenerator<string> defgen = new DefaultGenerator<string>(setme);
            var checkme = (string) defgen.Generate();
            Assert.That(checkme, Is.Not.Empty);
            Assert.That(checkme== setme);
        }

        [Test]
        public void DoubleGeneratorWithDefaultValues()
        {
            IGenerator<double> doublegen = new DoubleGenerator();
            var checkme = (double) doublegen.Generate();
            
            Assert.That(checkme, Is.InRange(0.00, 100.00));
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
            var checkme = (int) intgen.Generate();
            
            Assert.That(checkme, Is.InRange(0, 100));
        }

        [Test]
        public void IntegerGeneratorWithOverrideTest()
        {
            var min = 5;
            var max = 20;
            IGenerator<int> intgen = new IntegerGenerator(min, max);
            var checkme = (int) intgen.Generate();
            
            Assert.That(checkme, Is.InRange(min, max));
        }

        [Test]
        public void ListGeneratorTest()
        {
            IList<string> testlist = new List<string>();
            testlist.Add("hey");
            testlist.Add("yay");
            testlist.Add("nay");
            IGenerator<string> listgen = new FromListGetSingleGenerator<string>(testlist);
            var checkme = (string) listgen.Generate();
            Assert.That(checkme, Is.Not.Empty);
            Assert.That(testlist.Contains(checkme));
        }

        [Test]
        public void NullGeneratorTest()
        {
            IGenerator<object> nullgen = new NullGenerator();
            var checkme = (string) nullgen.Generate();
            Assert.That(checkme, Is.Null);
        }

        private bool CheckPhone(string phonetocheck)
        {
            var phonepattern =
                new Regex(
                    "^[\\(]{0,1}([0-9]){3}[\\)]{0,1}[ ]?([^0-1]){1}([0-9]){2}[ ]?[-]?[ ]?([0-9]){4}[ ]*((x){0,1}([0-9]){1,5}){0,1}$");
            var boolval = phonepattern.IsMatch(phonetocheck);
            return boolval;
        }

        [Test]
        public void AmericanPhoneGeneratorTest()
        {
            IGenerator<string> phonegen = new AmericanPhoneGenerator();
            var phone = (string) phonegen.Generate();
            Assert.That(phone, Is.Not.Null);
            Assert.That(CheckPhone(phone));
        }

        private bool IsAmericanPostalCodeValid(string postalcode)
        {
            var postalcodepattern = new Regex("^\\d{5}$|^\\d{5}-\\d{4}$");
            return postalcodepattern.IsMatch(postalcode);
        }

        [Test]
        public void AmericanPostalCodeGenerator()
        {
            IGenerator<string> postalgen = new AmericanPostalCodeGenerator(1);
            var zipcode = (string) postalgen.Generate();
            Assert.That(zipcode, Is.Not.Empty);
            Assert.That(IsAmericanPostalCodeValid(zipcode));
        }

        [Test]
        public void AmericanPostalCodeGeneratorNoPlusFourTest()
        {
            IGenerator<string> postalgen = new AmericanPostalCodeGenerator(0);
            var zipcode = (string)postalgen.Generate();
            Assert.That(zipcode, Is.Not.Empty);
            Assert.That(IsAmericanPostalCodeValid(zipcode));
        }

        [Test]
        public void TextGeneratorTest()
        {
            IGenerator<string> textgen = new TextGenerator();
            var text = (string) textgen.Generate();
            Assert.That(text, Is.Not.Empty);
        }


        [Test]
        public void AmericanAddressGeneratorTest()
        {
            IGenerator<string> americanaddy = new AmericanAddressGenerator();
            var address = (string) americanaddy.Generate();
            Assert.That(address, Is.Not.Empty);
        }


        [Test]
        public void AmericanCityGeneratorTest()
        {
            IGenerator<string> americancity = new AmericanCityGenerator();
            var city = (string) americancity.Generate();
            Assert.That(city, Is.Not.Empty);
        }

        [Test]
        public void AmericanStateGeneratorTest()
        {
            IGenerator<string> americanstate = new AmericanStateGenerator();
            var state = (string) americanstate.Generate();
            Assert.That(state, Is.Not.Empty);
        }

        public bool IsValidIPAddress(string ipaddress)
        {
            var testpattern =
                new Regex(
                    "^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
            return testpattern.IsMatch(ipaddress);
        }

        [Test]
        public void IPAddressGeneratorTest()
        {
            IGenerator<string> ipaddress = new IPAddressGenerator();
            var ipaddy = (string) ipaddress.Generate();
            Assert.That(ipaddy, Is.Not.Empty);
            Assert.That(IsValidIPAddress(ipaddy));
        }

        private bool IsWebsiteAddressValid(string webaddy)
        {
            var testpattern = new Regex("((mailto\\:|(news|(ht|f)tp(s?))\\://){1}\\S+)");
            return testpattern.IsMatch(webaddy);
        }

        [Test]
        public void WwebsiteAddressGeneratorTest()
        {
            IGenerator<string> websitegen = new WebsiteGenerator();
            var site = (string) websitegen.Generate();
            Assert.That(site, Is.Not.Empty);
            Assert.That(IsWebsiteAddressValid(site));
        }

        [Test]
        public void GenderGenerator()
        {
            IGenerator<string> gendergenerator = new GenderGenerator();
            var gender = (string) gendergenerator.Generate();
            Assert.That(gender, Is.Not.Empty);
        }

        [Test]
        public void CreditCardTypeGenerator()
        {
            IGenerator<string> cardtypegenerator = new CreditCardTypeGenerator();
            var cardtype = (string) cardtypegenerator.Generate();
            Assert.That(cardtype, Is.Not.Empty);
        }

        [Test]
        public void CountryCodeGenerator()
        {
            IGenerator<string> countrycodegenerator = new CountryCodeGenerator();
            var countrycode = (string) countrycodegenerator.Generate();
            Assert.That(countrycode, Is.Not.Empty);
        }

        private bool IsEmailAddressValid(string emailaddress)
        {
            var emailpattern =
                new Regex(
                    "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
            return emailpattern.IsMatch(emailaddress);
        }

        [Test]
        public void EmailAddressGenerator()
        {
            IGenerator<string> emailaddressgenerator = new EmailAddressGenerator();
            var emailaddy = (string) emailaddressgenerator.Generate();
            Assert.That(emailaddy, Is.Not.Empty);
            Assert.That(IsEmailAddressValid(emailaddy));
        }

        [Test]
        public void BooleanGenerator()
        {
            IGenerator<bool> booleangenerator = new BooleanGenerator();
            var boolvalue = (bool) booleangenerator.Generate();
            //Assert.That(boolvalue, Is.Not.Empty);
            Assert.That(boolvalue, Is.TypeOf<bool>());
            
        }

        [Test]
        public void FedExTrackingNumberGenerator()
        {
            IGenerator<string> trackingnumbergenerator = new TrackingNumberGenerator("FedEx");
            var tracknumber = (string) trackingnumbergenerator.Generate();
            Assert.That(tracknumber, Is.Not.Empty);
            Assert.That(tracknumber.Length == 15);
            
            Assert.That(tracknumber.StartsWith("4"));
        }

        [Test]
        public void UPSTrackingNumberGenerator()
        {
            IGenerator<string> trackingnumbergenerator = new TrackingNumberGenerator("UPS");
            var tracknumber = (string) trackingnumbergenerator.Generate();
            Assert.That(tracknumber, Is.Not.Empty);
            Assert.That(tracknumber.Length == 20);
            Assert.That(tracknumber.StartsWith("1Z"));
        }

        [Test]
        public void USPSTrackingNumberGenerator()
        {
            IGenerator<string> trackingnumbergenerator = new TrackingNumberGenerator("USPS");
            var tracknumber = (string) trackingnumbergenerator.Generate();
            Assert.That(tracknumber, Is.Not.Empty);
            Assert.That(tracknumber.Length == 22);
            Assert.That(tracknumber.StartsWith("91"));
        }

        [Test]
        public void CanGetDefaultCCVFromGenerator()
        {
            IGenerator<string> ccvGenerator = new CCVGenerator("");
            var ccv = (string) ccvGenerator.Generate();
            Assert.That(ccv, Is.Not.Empty);
        }

        [Test]
        public void PasswordWithDefaultLengthGenerator()
        {
            IGenerator<string> pwGen = new PasswordGenerator();
            var pw = (string) pwGen.Generate();
            Assert.That(pw, Is.Not.Empty);
            Assert.That(pw.Length == 10);
        }

        [Test]
        public void PasswordWithCustomLengthGenerator()
        {
            var length = 99;
            IGenerator<string> pwGen = new PasswordGenerator(length);
            var pw = (string) pwGen.Generate();
            Assert.That(pw, Is.Not.Empty);
            Assert.That(length== pw.Length);
        }

        [Test]
        public void AlphaNumericGenerator()
        {
            IGenerator<string> alphaNumericGen = new AlphaNumericGenerator(10);
            var alphaNumeric = alphaNumericGen.Generate();
            Assert.That(alphaNumeric, Is.Not.Empty);
            Assert.That(10 == alphaNumeric.Length);
        }

        [Test]
        public void UnitedKingdomNationalInsuranceGeneratorTest()
        {
            var generator = new UnitedKingdomNationalInsuranceGenerator();

            var result = generator.Generate();

            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.Null);
            Assert.That(new Regex(@"^(?!BG)(?!GB)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)(?:[A-CEGHJ-PR-TW-Z][A-CEGHJ-NPR-TW-Z])(?:\s*\d\s*){6}([A-D]|\s)$").IsMatch(result));
        }

        [Test]
        public void UnitedKingdomCityGeneratorTest()
        {
            IGenerator<string> unitedKingdomCityGenerator = new UnitedKingdomCityGenerator();
            var city = (string)unitedKingdomCityGenerator.Generate();
            Assert.That(city, Is.Not.Empty);
        }

        [Test]
        public void UnitedKingdomCountyGeneratorTest()
        {
            IGenerator<string> unitedKingdomCountyGenerator = new UnitedKingdomCountyGenerator();
            var county = (string)unitedKingdomCountyGenerator.Generate();
            Assert.That(county, Is.Not.Empty);
        }

        [Test]
        public void UnitedKingdomPostCodeGeneratorTest()
        {
            IGenerator<string> unitedKingdomCountyGenerator = new UnitedKingdomPostCodeGenerator();
            var county = (string)unitedKingdomCountyGenerator.Generate();
            Assert.That(county, Is.Not.Empty);
        }

        [Test]
        public void UnitedKingdomLandlineGeneratorTest()
        {
            var generator = new UnitedKingdomLandlineGenerator();
            var result = generator.Generate();
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.StartsWith("01"), "Landline numbers always start with \"01\" in the UK");
        }

        [Test]
        public void UnitedKingdomMobileGeneratorTest()
        {
            var generator = new UnitedKingdomMobileGenerator();
            var result = generator.Generate();
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.StartsWith("07"), "Mobile numbers always start with \"07\" in the UK");
        }

        [Test]
        public void UnitedKingdomPhoneGeneratorTest()
        {
            const string ndcPrefix = "6";
            var expectedPrefix = string.Format("0{0}", ndcPrefix);
            var generator = new UnitedKingdomPhoneNumberGenerator(ndcPrefix);

            var result = generator.Generate();

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.StartsWith(expectedPrefix), $"The phone number should start with \"{expectedPrefix}\"" );
        }

        [Test]
        public void CultureInfoNamesGeneratorTest()
        {
            IGenerator<string> cultureDisplayNameGenerator = new CulturesGenerator<string>(culture => culture.DisplayName);
            var cultureDisplayName = cultureDisplayNameGenerator.Generate();
            Assert.That(cultureDisplayName, Is.Not.Empty);

            IGenerator<int> cultureLCIDGenerator = new CulturesGenerator<int>(culture => culture.LCID);
            var cultureLCID = cultureLCIDGenerator.Generate();
            Assert.That(cultureLCID> 0);

        }
    }
}
