using System;
using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;
using Foundation.ObjectHydrator.Generators;

namespace Foundation.ObjectHydrator
{
    public class DefaultTypeMap:List<IMap>
    {
        public DefaultTypeMap()
        {
            Add(new Map<DateTime>().Using(new DateTimeGenerator()));
            Add(new Map<double>().Using(new DoubleGenerator()));
            Add(new Map<Double>().Using(new DoubleGenerator()));
            Add(new Map<int>().Using(new IntegerGenerator()));
            Add(new Map<Int32>().Using(new IntegerGenerator()));
            Add(new Map<bool>().Using(new BooleanGenerator()));
            Add(new Map<Guid>().Using(new GuidGenerator()));
            Add(new Map<byte[]>().Using(new ByteArrayGenerator(8)));
            Add(new EnumMap());
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower() == "firstname" || info.Name.ToLower() == "fname")
                    .Using(new FirstNameGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower() == "lastname" || info.Name.ToLower() == "lname")
                    .Using(new LastNameGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower().Contains("email"))
                    .Using(new EmailAddressGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower().Contains("password"))
                    .Using(new PasswordGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower().Contains("trackingnumber"))
                    .Using(new TrackingNumberGenerator("ups")));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower() == "ipaddress")
                    .Using(new IPAddressGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower().Contains("country"))
                    .Using(new CountryCodeGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower() == "gender")
                    .Using(new GenderGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower() == "creditcardtype")
                    .Using(new CreditCardTypeGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower().Contains("addressline") || info.Name.ToLower().Contains("address"))
                    .Using(new AmericanAddressGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower().Contains("creditcard") ||
                              info.Name.ToLower().Contains("cardnum") ||
                              info.Name.ToLower().Contains("ccnumber"))
                    .Using(new CreditCardNumberGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower().Contains("url") ||
                              info.Name.ToLower().Contains("website") ||
                              info.Name.ToLower().Contains("homepage"))
                    .Using(new WebsiteGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower() == "city")
                    .Using(new AmericanCityGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower() == "state")
                    .Using(new AmericanStateGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower() == "company" ||
                              info.Name.ToLower() == "business" ||
                              info.Name.ToLower() == "companyname")
                    .Using(new CompanyNameGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower()
                    .Contains("descri")).Using(new TextGenerator(25)));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower().Contains("phone")).Using(
                    new AmericanPhoneGenerator()));
            Add(new Map<string>()
                    .Matching(info => info.Name.ToLower().Contains("zip") || info.Name.ToLower().Contains("postal"))
                    .Using(new AmericanPostalCodeGenerator(25)));
            Add(new Map<string>().Using(new TextGenerator(50)));
        }
    }
}
