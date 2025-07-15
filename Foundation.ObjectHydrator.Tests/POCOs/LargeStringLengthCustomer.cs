using System.ComponentModel.DataAnnotations;

namespace Foundation.ObjectHydrator.Tests.POCOs
{
    public class LargeStringLengthCustomer
    {
        public string FirstName { get; set; }
        
        [StringLength(4000)]
        public string LargeDescription { get; set; }
        
        [StringLength(1)]
        public string TinyDescription { get; set; }
        
        [StringLength(2)]
        public string SmallDescription { get; set; }
    }
}