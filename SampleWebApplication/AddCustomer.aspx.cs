using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Foundation.ObjectHydrator;

namespace SampleWebApplication
{
    public partial class AddCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hydrator<Customer> hydrator = new Hydrator<Customer>();
                Customer startwithme = hydrator.GetSingle();
                txtFirstName.Text = startwithme.FirstName;
                txtLastName.Text = startwithme.LastName;
                txtStreetAddress.Text = startwithme.StreetAddress;
                txtCity.Text = startwithme.City;
                txtState.Text = startwithme.State;
                txtZip.Text = startwithme.Zip;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            //Normally I wouldn't put code like this here
            //But I'm just doing this for simplicity
            //Ryan
            Hydrator<Customer> hydrator = new Hydrator<Customer>()
            .With(x=>x.FirstName, txtFirstName.Text)
            .With(x => x.LastName, txtLastName.Text)
            .With(x=>x.StreetAddress,txtStreetAddress.Text)
            .With(x=>x.City,txtCity.Text)
            .With(x=>x.State,txtState.Text)
            .With(x=>x.Zip,txtZip.Text);
            
            Customer savethis = hydrator.GetSingle();

            CustomerRepository cr = new CustomerRepository();
            cr.SaveCustomer(savethis);
            Response.Redirect("/");
        }
    }
}
