using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Tests.POCOs;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.ObjectHydrator.Tests.HydratorTests
{
    [TestFixture]
    public class Hydrator_PetShop_Tests
    {
        [Test]
        public void Test_Covariance()
        {
            var hy = new Hydrator<PetShop>();
            var shop = hy.GetSingle();

            Assert.IsNotNull(shop);
            //By default, no instantiation for interfaces and abstract types
            Assert.IsNull(shop.AnimalEnumerable);
            Assert.IsNull(shop.AnimalList);
            Assert.IsNull(shop.CatEnumerable);

            //Dog list should be initialized as it is type of generic List
            Assert.IsNotNull(shop.DogList);
            CollectionAssert.IsEmpty(shop.DogList);

            //setup generators
            hy = hy.With(x => x.AnimalEnumerable, new ListGenerator<Dog>(2)) //Can setup as IEnumerable supports covariance
                .With(x => x.CatEnumerable, new ListGenerator<Cat>(2)) //Can setup as no covariance required
                .With(x => x.CatList, new ListGenerator<Cat>(3)); //Can setup List type as no covarinace required
                                                                  //.With(x=>x.AnimalList, new ListGenerator<Cat>(2)) --Cannot setup as IList type doesn't support covariance
                                                                  //.With(x=>x.DogList, new ListGenerator<Dog>(2)) --Cannot setup as List type doesn't support covariance

            shop = hy.GetSingle();

            Assert.IsNotNull(shop);

            //Ienumerable types must be instantiated
            Assert.IsNotNull(shop.AnimalEnumerable);
            Assert.AreEqual(2, shop.AnimalEnumerable.Count());
            CollectionAssert.AllItemsAreInstancesOfType(shop.AnimalEnumerable, typeof(Dog));
            Assert.IsNotNull(shop.CatEnumerable);
            Assert.AreEqual(2, shop.CatEnumerable.Count());
            CollectionAssert.AllItemsAreInstancesOfType(shop.CatEnumerable, typeof(Cat));

            //List type with no covariance should be instantiated
            Assert.IsNotNull(shop.CatList);
            Assert.AreEqual(3, shop.CatList.Count);
            CollectionAssert.AllItemsAreInstancesOfType(shop.CatList, typeof(Cat));

            //As we have seen that we cannot setup IList and List types in case of covariance
            //We can use the following apporach for such types
            shop.AnimalList = new List<Animal>(new Hydrator<Cat>().GetList(2));
            shop.DogList = new List<Animal>(new Hydrator<Dog>().GetList(2));

            Assert.IsNotNull(shop.DogList);
            Assert.AreEqual(2, shop.DogList.Count);
            CollectionAssert.AllItemsAreInstancesOfType(shop.DogList, typeof(Dog));
            Assert.IsNotNull(shop.AnimalList);
            Assert.AreEqual(2, shop.AnimalList.Count);
            CollectionAssert.AllItemsAreInstancesOfType(shop.AnimalList, typeof(Cat));
        }
    }
}
