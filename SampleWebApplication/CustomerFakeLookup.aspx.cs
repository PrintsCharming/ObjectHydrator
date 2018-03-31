using System;
using System.Collections.Generic;
using System.Web.UI;
using Foundation.ObjectHydrator;
using Foundation.ObjectHydrator.Generators;

namespace SampleWebApplication
{
    public partial class CustomerFakeLookup : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Hydrator<Customer> hydrator = new Hydrator<Customer>()
            .With(x => x.Company, new FromListGetSingleGenerator<Company>((IList<Company>)Application["companies"]))
            .With(x => x.LastName, txtLastName.Text);
            GridView1.DataSource = hydrator.GetList(30);
            GridView1.DataBind();
        }
    }
}
