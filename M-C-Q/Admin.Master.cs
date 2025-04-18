using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M_C_Q
{
    public partial class Admin1 : System.Web.UI.MasterPage
    {
        Methods methods = new Methods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblUsername.Text = Session["USERNAME"].ToString();
                string strRole = Session["ROLE_ID"].ToString();
                lblRole.Text = methods.GetRolesByID(strRole);
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "setSideBar();", true);
        }
    }
}