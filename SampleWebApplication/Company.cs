namespace SampleWebApplication
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Homepage { get; set; }

        public override string ToString()
        {
            return CompanyName;
        }
        
    }
}
