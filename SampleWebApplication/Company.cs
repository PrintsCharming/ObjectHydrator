namespace SampleWebApplication
{
    public class Company
    {
        private int _companyID;
        public int CompanyID
        {
            get { return _companyID; }
            set
            {
                _companyID = value;
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
            }
        }

        private string _addressLine;
        public string AddressLine
        {
            get { return _addressLine; }
            set
            {
                _addressLine = value;
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

        private string _homepage;
        public string Homepage
        {
            get { return _homepage; }
            set
            {
                _homepage = value;
            }
        }

        public override string ToString()
        {
            return CompanyName;
        }
        
    }
}
