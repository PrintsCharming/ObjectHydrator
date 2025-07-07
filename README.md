ObjectHydrator
==============

This project allows you to pass custom POCO's to it, and have it return an instance of the class populated with randomly generated but real-world looking data. This random data can be overridden by convention.

**Basic syntax looks like this:**

```csharp
Hydrator<Customer> _customerHydrator = new Hydrator<Customer>();
Customer customer = _customerHydrator.GetSingle();
List<Customer> customerlist=_customerHydrator.GetList(20);
```

**Advanced syntax looks like:**

```csharp
Hydrator<Customer> _customerHydrator = new Hydrator<Customer>()
    .WithInteger(x => x.CustomerAge, 1, 100)
    .WithAmericanPhone(x=>x.CustomerPhone);
```

**A custom generator looks like:**

```csharp
public class FullNameGenerator : IGenerator<string>
{
    static readonly IGenerator<string> FirstNameGen = new FirstNameGenerator();
    static readonly IGenerator<string> LastNameGen = new LastNameGenerator();

    public string Generate()
    {
        return FirstNameGen.Generate() + " " + LastNameGen.Generate();
    }
}
```
    
Use a custom generator like this:

```csharp
Hydrator<Customer> _customerHydrator = new Hydrator<Customer>()
    .With(x => x.FullName, new FullNameGenerator());
```

## Changes

Version 1.3.0
* Added 3 new generators UnitedKingdomPhoneNumberGenerator, UnitedKingdomInsuranceNumberGenerator, and CulturesGenerator thanks to rikrak and ushenkodmitry
*  "Do" method to allow a property to be set from the generated values in other properties, thanks to rikrak
* Fixed randomization method which was excluding the last possible, thanks to rikrak and ushenkodmitry

Version 1.1.0
* Added the ability to respect the StringLength property attribute from System.ComponentModel.DataAnnotations (more annotations are welcome and likely)

Version 1.0.0

* Added the ability to inject Generators at instantiation time. Which looks like this:
  `var hydrator = new Hydrator<Address>().WithCustomGenerator(x=>x.State, new InjectedGenerator());`

Version 0.7.0

* Added new generators: UnitedKingdomCityGenerator, UnitedKingdomCountyGenerator, UnitedKingdomPostcodeGenerator, AlphaNumericGenerator.

## NuGet [#objecthydrator](http://www.nuget.org/packages/objecthydrator/)

    Install-Package objecthydrator

This version is for Visual Studio 2022 .Net 10. 

## Summary

So basically, you create a class and invoke the Hydrator object with that class type. Then call the `GetSingle` or `GetList` functions and you are returned an instance of the object populated with realistic data. The idea behind it is to use it to replace a database call to use in your UI. 

## Generators

Presently the generators are pretty simple and can generate limited values, they include:

Generator | Description | Notes
----------|-------------|--------
FirstName | Returns a random english First Name
LastName  | Return a random english Last Name
DateTimeGenerator | Returns a random Date within a given range.
AmericanPhone | Returns a randon American Phone Number
AmericanAddress | Returns a random American Address (street part)
AmericanCity | Returns a random American City
AmericanPostalCode | Returns a random Postal Code (including optional +4 component)
Integer Generator | Returns an int within a range
Enum Generator | Define the enum and it will return the string value of a random one 
Boolean Generator | Returns a random boolean
AmericanState | Returns a random US abbreviation
EmailAddress | Returns a random email address | Thanks ScottMonnig! 
Business Name Generator | Returns a random 3 part business name
URL Generator | Returns random URL based on BusinessName Generator 
IPAddress Generator | Returns a random ip address
TextGenerator | Random Greek Text
CountryCode | Random Country Code
ByteArray Generator | Returns an array of random bytes
GUID Generator | Returns a new GUID
TypeGenerator | Return a hydrated object of Type
TypeListGenerator | Return a list of objects
PasswordGenerator | Returns a string of random pw characters with length parameter
UnitedKingdomCityGenerator | Returns a UK City | Thanks to fuzzy-afterlife
UnitedKingdomCountyGenerator | Returns a UK County | Thanks to fuzzy-afterlife
UnitedKingdomPostcodeGenerator | Returns a UK Post Code | Thanks to fuzzy-afterlife
UnitedKingdomPhoneNumberGenerator | Returns a UK Phone | Thanks to rikrak
UnitedKingdomInsuranceNumberGenerator | Returns a UK Ins Number | Thanks to rikrak
AlphaNumericGenerator | Returns an string with alpha chars of n length | Thanks to fuzzy-afterlife
CulturesGenerator | Returns a Culture Info property | Thanks to ushenkodmitry

All values can be overridden so you can do things like fake search results etc...
