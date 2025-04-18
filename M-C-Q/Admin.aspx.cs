using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M_C_Q
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["USERNAME"] == null)
            {
                Session["EXPIRED_USERNAME"] = "Username Expired";
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblMessage.Text = Session["USERNAME"].ToString();
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            //Session["Logout"] = "Logout";
            Response.Redirect("Login.aspx");
        }
    }
}