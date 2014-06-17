using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleWebApplication
{
    public partial class CustomerRealLookUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            CustomerRepository custrepos = new CustomerRepository();
            GridView1.DataSource = custrepos.RealSearchByLastName(txtLastName.Text);
            GridView1.DataBind();
        }
    }
}
