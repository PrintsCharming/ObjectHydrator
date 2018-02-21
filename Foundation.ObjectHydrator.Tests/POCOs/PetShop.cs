using System.Collections.Generic;

namespace Foundation.ObjectHydrator.Tests.POCOs
{
    /// <summary>
    /// POCO to test covarience feature of Generator
    /// </summary>
    public class PetShop
    {
        public IEnumerable<Animal> AnimalEnumerable { get; set; }

        public IList<Animal> AnimalList { get; set; }

        public List<Animal> DogList { get; set; }

        public IEnumerable<Cat> CatEnumerable { get; set; }

        public IList<Cat> CatList { get; set; }
    }
}
