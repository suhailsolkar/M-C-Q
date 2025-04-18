using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace M_C_Q
{
    public partial class Login : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            if(Session["EXPIRED_USERNAME"] != null)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Your Session has expired";
            }
            if (Session["COUNT"] != null)
            {
                pnlLoginAs.Controls.Clear();
                string strBtnText = "";
                string strBtnID = "";
                for (int i = 1; i <= int.Parse(Session["COUNT"].ToString()); i++)
                {
                    if (i == 1) { strBtnText = "Admin"; strBtnID = "btn" + strBtnText; }
                    else if (i == 2) { strBtnText = "Teacher"; strBtnID = "btn" + strBtnText; }
                    else if (i == 3) { strBtnText = "Student"; strBtnID = "btn" + strBtnText; }
                    CreateLoginAsButtons(strBtnText, strBtnID);
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)//Login
        {
            lblMessage.Text = "";
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            if(txtEmail.Text.ToString() == "" || txtPassword.Text.ToString() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "warning", "swal('Values cannot be empty!', '', 'warning', '')", true);
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(strCon);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_LOGIN", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMAIL_ID", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@PASSWORD", txtPassword.Text);
                    adp.SelectCommand = cmd;
                    adp.Fill(dt);
                    adp.Fill(ds);
                    cmd.Dispose();
                    string strRole = "";
                    if (dt.Rows.Count == 1)
                    {
                        if (dt.Rows[0]["RESULT"].ToString() == "Invalid Email ID or password.")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('Invalid Email ID or password!', 'Check your credentials and try again.', 'error', '')", true);
                        }
                        else if (dt.Rows[0]["RESULT"].ToString() == "User does not exist.")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('User does not exist!', 'Check your credentials and try again.', 'error', '')", true);
                        }
                        else if (dt.Rows[0]["RESULT"].ToString() == "Your approval is pending.")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "swal('Your approval is pending!', 'Please contact admin for approval.', 'info', '')", true);
                        }
                        else//Single Role
                        {
                            Session["USER_ID"] = dt.Rows[0]["USER_ID"].ToString();
                            Session["ROLE_ID"] = dt.Rows[0]["ROLE_ID"].ToString();
                            Session["USERNAME"] = dt.Rows[0]["USERNAME"].ToString();
                            if (int.Parse(Session["ROLE_ID"].ToString()) == 1) { strRole = "Admin"; }
                            else if (int.Parse(Session["ROLE_ID"].ToString()) == 2) { strRole = "Teacher"; }
                            else if (int.Parse(Session["ROLE_ID"].ToString()) == 3) { strRole = "Student"; }
                            RedirectByRoles("btn" + strRole);
                        }
                    }
                    else if (dt.Rows.Count > 1)//Multiple Roles
                    {
                        pnlLoginForm.Visible = false;
                        pnlLoginAsForm.Visible = true;

                        Session["COUNT"] = dt.Rows.Count;
                        string strBtnText = "";
                        string strBtnID = "";
                        pnlLoginAs.Controls.Clear();
                        Session["USER_ID"] = dt.Rows[0]["USER_ID"].ToString();
                        Session["ROLE_ID"] = dt.Rows[0]["ROLE_ID"].ToString();
                        Session["USERNAME"] = dt.Rows[0]["USERNAME"].ToString();
                        for (int i = 0; i <= dt.Rows.Count; i++)
                        {
                            if (int.Parse(dt.Rows[i]["ROLE_ID"].ToString()) == 1)
                            {
                                strBtnText = "Admin"; strBtnID = "btn" + strBtnText;
                            }
                            else if (int.Parse(dt.Rows[i]["ROLE_ID"].ToString()) == 2) 
                            { 
                                strBtnText = "Teacher"; strBtnID = "btn" + strBtnText; 
                            }
                            else if (int.Parse(dt.Rows[i]["ROLE_ID"].ToString()) == 3) 
                            { 
                                strBtnText = "Student"; strBtnID = "btn" + strBtnText; 
                            }
                            CreateLoginAsButtons(strBtnText, strBtnID);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User does not exist.')", true);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('User does not exists!', 'Check your credentials and try again.', 'error', '')", true);
                    }
                }
                catch (Exception ex)
                {
                    SqlConnection con = new SqlConnection(strCon);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ERROR_NAME", "Login_Click");
                    cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                finally
                {
                    dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
                }
            }
            
        }
        private void CreateLoginAsButtons(string strBtnText, string strBtnID)
        {
            Button newButton = new Button();
            newButton.Text = strBtnText; newButton.ID = strBtnID; newButton.CssClass = "btnSubmit";
            newButton.Click += new EventHandler(DynamicButton_Click);
            pnlLoginAs.Controls.Add(newButton);
        }
        private void DynamicButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string dynamicButtonID = clickedButton.ID;
            RedirectByRoles(dynamicButtonID);
        }
        private void RedirectByRoles(string RoleName)
        {
            Session["COUNT"] = null;
            switch (RoleName)
            {
                case "btnAdmin":
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('Login Successfull!', '', 'success', 'AdminMenu.aspx')", true);
                    break;

                case "btnTeacher":
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('Login Successfull!', '', 'success', 'AdminMenu.aspx')", true);
                    break;

                case "btnStudent":
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('Login Successfull!', '', 'success', 'Login.aspx')", true);
                    break;

                default :
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('Login Successfull!', '', 'success', 'Login.aspx')", true);
                    break;
            }
        }
    }
}