using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleWebApplication
{
    public class Customer
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
            }
        }

        private string _streetAddress;
        public string StreetAddress
        {
            get { return _streetAddress; }
            set
            {
                _streetAddress = value;
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
            }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
            }
        }

        private string _zip;
        public string Zip
        {
            get { return _zip; }
            set
            {
                _zip = value;
            }
        }

        private Company _company;
        public Company Company { get; set; }

        private IList<Company> _companies;
        public IList<Company> Companies {get;set;}

        
        
    }
}
