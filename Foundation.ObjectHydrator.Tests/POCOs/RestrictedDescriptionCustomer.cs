using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Foundation.ObjectHydrator.Tests.POCOs
{
    public class RestrictedDescriptionCustomer
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        [StringLength(5)]
        public string Description { get; set; }
        public int Locations { get; set; }
        public DateTime IncorporatedOn { get; set; }
        public Double Revenue { get; set; }
        public string homepage { get; set; }
        public string ipaddress { get; set; }
        public string gender { get; set; }
        public string creditcardtype { get; set; }
        public string Country { get; set; }
        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
            }
        }
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
            }
        }

        public Guid UniqueId { get; set; }
        public byte[] Version { get; set; }
        public string CreditCardNumber { get; set; }
        public string TrackingNumber { get; set; }

        public string CCV { get; set; }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }
        
        public string placeholderstring { get; set; }
    }
}
