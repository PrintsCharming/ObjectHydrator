using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Foundation.ObjectHydrator.Generators;


namespace SampleWebApplication
{
    public partial class CustomerFakeLookup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Foundation.ObjectHydrator.Hydrator<Customer> hydrator = new Foundation.ObjectHydrator.Hydrator<Customer>()
            .With(x => x.Company, new FromListGetSingleGenerator<Company>((IList<Company>)Application["companies"]))
            .With(x => x.LastName, txtLastName.Text);
            GridView1.DataSource = hydrator.GetList(30);
            GridView1.DataBind();
        }
    }
}
