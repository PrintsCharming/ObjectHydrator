using System;

namespace Foundation.ObjectHydrator.Tests.POCOs
{
    public class SimpleCustomer
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public int Locations { get; set; }
        public DateTime IncorporatedOn { get; set; }
        public Double Revenue { get; set; }
        public string Homepage { get; set; }
        public string Ipaddress { get; set; }
        public string Gender { get; set; }
        public string Creditcardtype { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }

        public Guid UniqueId { get; set; }
        public byte[] Version { get; set; }
        public string CreditCardNumber { get; set; }
        public string TrackingNumber { get; set; }

        public string Ccv { get; set; }
        public string Password { get; set; }

        public string Placeholderstring { get; set; }
    }
}
