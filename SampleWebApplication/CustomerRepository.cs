using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleWebApplication
{
    public class CustomerRepository
    {
        public IEnumerable<Customer> GetAllCustomers()
        {
            IList<Customer> customers = (IList<Customer>)HttpContext.Current.Application["customers"];
            var custlist = from p in customers orderby p.LastName select p;
            return custlist;
        }

        public IEnumerable<Customer> RealSearchByLastName(string searchbylastname)
        {
            IList<Customer> customers = (IList<Customer>)HttpContext.Current.Application["customers"];
            var custlist = from p in customers where p.LastName.ToLower().Contains(searchbylastname.ToLower()) orderby p.FirstName select p;
            return custlist;
        }

        public void SaveCustomer(Customer saveme)
        {
            IList<Customer> customers = (IList<Customer>)HttpContext.Current.Application["customers"];
            customers.Add(saveme);
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["customers"] = customers;
            HttpContext.Current.Application.UnLock();
        }
    }
}
