using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace M_C_Q
{
    public partial class ManageTeachers : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        Methods oMethods = new Methods();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "Alertme('success')", true);
            if (!IsPostBack)
            {
                BindGrid();
                
            }
        }

        protected void gvTeachers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTeachers.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void gvTeachers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTeachers.EditIndex = -1;
            BindGrid();
        }

        protected void gvTeachers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int User_ID = int.Parse(((Label)gvTeachers.Rows[e.RowIndex].FindControl("lblUserID2")).Text);
            string EmailID = ((TextBox)gvTeachers.Rows[e.RowIndex].FindControl("txtEmailID")).Text;
            string Username = ((TextBox)gvTeachers.Rows[e.RowIndex].FindControl("txtUsername")).Text;
            int RoleID = int.Parse(((TextBox)gvTeachers.Rows[e.RowIndex].FindControl("txtRoleID")).Text);

            try
            {
                string strResult = oMethods.ModifyGrid("UPDATE", dt, ds, adp, strCon, User_ID, EmailID, Username);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + strResult.ToString() + "')", true);
                gvTeachers.EditIndex = -1;
                BindGrid();
            }
            catch 
            {
                SqlConnection con = new SqlConnection(strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "gvTeachers_RowUpdating");
                cmd.Parameters.AddWithValue("@ERROR_MSG", e.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }
        protected void gvTeachers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int User_ID = int.Parse(((Label)gvTeachers.Rows[e.RowIndex].FindControl("lblUserID")).Text);
            string EmailID = ((Label)gvTeachers.Rows[e.RowIndex].FindControl("lblEmailID")).Text;
            string Username = ((Label)gvTeachers.Rows[e.RowIndex].FindControl("lblUsername")).Text;
            int RoleID = int.Parse(((Label)gvTeachers.Rows[e.RowIndex].FindControl("lblRoleID")).Text);

            try
            {
                string strResult = oMethods.ModifyGrid("DELETE", dt, ds, adp, strCon, User_ID, EmailID, Username);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + strResult.ToString() + "')", true);
                gvTeachers.EditIndex = -1;
                BindGrid();
            }
            catch
            {
                SqlConnection con = new SqlConnection(strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "gvTeachers_RowDeleting");
                cmd.Parameters.AddWithValue("@ERROR_MSG", e.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }
        public void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                dt = oMethods.BindGridView(strCon, "gvTeachers", 1,5, dt, ds, adp);

                con.Open();
                if (dt.Rows.Count >= 1)
                {
                    gvTeachers.Visible = true;
                    gvTeachers.DataSource = dt;
                    gvTeachers.DataBind();
                    con.Close();
                }
                else
                {
                    gvTeachers.Visible = false;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strResult = oMethods.ModifyGrid("INSERT", dt, ds, adp, strCon, 0, txtEmail.Text, txtUsername.Text);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + strResult.ToString() + "')", true);
                BindGrid();
            }
            catch
            {
                SqlConnection con = new SqlConnection(strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "gvTeachers_RowDeleting");
                cmd.Parameters.AddWithValue("@ERROR_MSG", e.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }
    }
}