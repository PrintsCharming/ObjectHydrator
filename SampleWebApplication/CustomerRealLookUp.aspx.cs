﻿using System;
using System.Web.UI;

namespace SampleWebApplication
{
    public partial class CustomerRealLookUp : Page
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
