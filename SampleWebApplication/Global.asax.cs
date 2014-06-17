using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Foundation.ObjectHydrator;

using Foundation.ObjectHydrator.Generators;

namespace SampleWebApplication
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //Create a list of companies and tuck them away for later use
            Hydrator<Company> hydrator2=new Hydrator<Company>();
            IList<Company> comps = hydrator2.GetList(10);
            Application["companies"] = comps;

            //Create our customer database using one of the companies created above as the Company property of the customer.
            //Whoa.
            int listSize = 4;
            var args = new object[] { listSize };
            //Hydrator<Customer> hydrator = new Hydrator<Customer>()

            //    .FromList("Company", System.Linq.Enumerable.Cast<object>((IList<Company>)Application["companies"]))
            //    .WithChildEntityList("Companies",typeof(Company),args);
            Hydrator<Customer> hydrator = new Hydrator<Customer>()
            .With(x => x.Company, new FromListGetSingleGenerator<Company>((IList<Company>)Application["companies"]))
            .With(x => x.Companies, new ListGenerator<Company>(listSize));
            Application["customers"] = hydrator.GetList(50);
            

            

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}