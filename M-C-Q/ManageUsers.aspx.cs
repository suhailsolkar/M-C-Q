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
using System.Globalization;

namespace M_C_Q
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        Methods methods = new Methods();
        Pagination pagination = new Pagination();
        int intRowCount;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (ddlCity.Items.Count == 1 && ddlGender.Items.Count == 1 && ddlRole.Items.Count == 1)
                {
                    BindDropdowns();
                    tabSubmit.Style.Add("color", "#69022f");
                    tabSubmit.Style.Add("background", "#fff");
                    pnlSubmit.Visible = true;


                    tabViewSearch.Style.Add("color", "grey");
                    tabViewSearch.Style.Add("background", "none");
                    pnlViewSearch.Visible = false;
                }
                BindGrid();
                View("Clear Submit");
            }
            else
            {
                //View("Clear Submit");
            }
            if (Session["USERNAME"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            CreatePaginationButtons();
        }

        #region Functions
        private void BindGrid()
        {
            try
            {
                string strSearchStatus = ddlsrcStatus.SelectedItem.ToString();
                string strSearchRole = ddlsrcRole.SelectedItem.ToString();

                dt.Clear(); dt.Dispose(); ds.Clear(); ds.Dispose(); adp.Dispose();

                ds = methods.FilterGrid(methods.strCon, Session["USER_ID"].ToString(), strSearchStatus, strSearchRole, txtSrcUserID.Text, 1, int.Parse(ddlShowCount.SelectedItem.ToString()), txtSrcAll.Text);

                dt = methods.RemoveWhiteSpaces(ds.Tables[0]);

                ViewState["COUNT"] = ds.Tables[1].Rows[0]["COUNT"].ToString();

                gvManageUsers.DataSource = dt;
                gvManageUsers.DataBind();

                SetGridComponents();
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "BindGrid");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose(); 
            }
        }

        private void BindDropdowns()
        {
            try
            {
                for (int i = 0; i <= 4; i++)
                {
                    string dropdownName = "";
                    if (i == 1) { dropdownName = "City"; }
                    else if (i == 2) { dropdownName = "Gender"; }
                    else if (i == 3) { dropdownName = "Role"; }
                    else if (i == 4) { dropdownName = "Status"; }

                    ds = methods.fetchDropdown(dropdownName);

                    dt = ds.Tables[0];

                    for (int j = 0; j <= dt.Rows.Count - 1; j++)
                    {
                        if (dropdownName == "City")
                        {
                            ddlCity.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                        }
                        else if (dropdownName == "Gender")
                        {
                            ddlGender.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                        }
                        else if (dropdownName == "Role")
                        {
                            ddlRole.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                            ddlsrcRole.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                        }
                        else if (dropdownName == "Status")
                        {
                            ddlsrcStatus.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                        }

                    }
                    dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
                }

            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "BindDropdowns");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }
        private void CreatePaginationButtons()
        {
            
            try
            {
                string strSearchStatus = ddlsrcStatus.SelectedItem.ToString();
                string strSearchRole = ddlsrcRole.SelectedItem.ToString();

                intRowCount = int.Parse(ViewState["COUNT"].ToString());
                lblCount.Text = "of " + intRowCount + " records";

                //New
                string strCurrentPage =  "1";

                if (ViewState["Current_Page"]!= null)
                {
                    strCurrentPage = ViewState["Current_Page"].ToString();
                }
                else
                {
                    strCurrentPage = "1";
                }
                //New

                ViewState["btnPagination_Count"] = pagination.CreatePaginationButtons2(pnlPagination, int.Parse(strCurrentPage), ddlShowCount.SelectedItem.ToString(), intRowCount.ToString(), PaginationButton_Click, methods.strCon, dt, ds, adp, Session["USER_ID"].ToString());
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "CreatePaginationButtons");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }
        #endregion

        #region Events
        protected void btnAddusers_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();

            bool isValid = CheckEmptyFields();

            if (isValid)
            {
                try
                {
                    string strStatus = string.Empty;

                    if(Session["ROLE_ID"].ToString() == "1")
                    {
                        strStatus = "1";
                    }
                    else
                    {
                        strStatus = "3";
                    }

                    dt = methods.AddUsers(methods.strCon, dt, ds, adp, "Submit", hdnUserID.Value, (txtFirstname.Text + ' ' + txtLastname.Text), txtEmailID.Text, txtPassword.Text, txtDOB.Text, ddlGender.SelectedValue, ddlCity.SelectedValue, txtPhoneno.Text, strStatus, ddlRole.SelectedValue, Session["USER_ID"].ToString(), txtComment.Text, "");

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('User submitted successfully', '', 'success', '')", true);
                            View("ViewSearch");
                            BindGrid();
                            CreatePaginationButtons();
                        }
                        else if (dt.Rows[0]["RESULT"].ToString() == "APPROVED")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This user is approved for selected role.')", true);
                        }
                        else if (dt.Rows[0]["RESULT"].ToString() == "PENDING")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This user is pending for approval.')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong')", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    SqlConnection con = new SqlConnection(methods.strCon);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ERROR_NAME", "btnAddusers_Click");
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

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            try
            {
                dt = methods.AddUsers(methods.strCon, dt, ds, adp, "Save Draft", hdnUserID.Value, (txtFirstname.Text + ' ' + txtLastname.Text), txtEmailID.Text, txtPassword.Text, txtDOB.Text, ddlGender.SelectedValue, ddlCity.SelectedValue, txtPhoneno.Text, "4", ddlRole.SelectedValue, Session["USER_ID"].ToString(), txtComment.Text , "");

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["RESULT"].ToString() == "SUCCESS")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('User saved draft successfully.', '', 'success', '')", true);
                        View("ViewSearch");
                        BindGrid();
                        CreatePaginationButtons();
                    }
                    else if (dt.Rows[0]["RESULT"].ToString() == "APPROVED")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This User is approved for selected role.')", true);
                    }
                    else if (dt.Rows[0]["RESULT"].ToString() == "PENDING")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This user is pending for approval.')", true);
                    }
                    else if (dt.Rows[0]["RESULT"].ToString() == "FAIL")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User already exists for selected role.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong..')", true);
                }
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "btnSaveDraft_Click");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose(); BindGrid();
            }
        }

        private void SetGridComponents()
        {
            foreach (GridViewRow row in gvManageUsers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblStatus = row.FindControl("gvlblStatus") as Label;


                    if (lblStatus.Text == "Pending")
                    {
                        lblStatus.CssClass = "lblPending";
                    }
                    else if (lblStatus.Text == "Approved")
                    {
                        lblStatus.CssClass = "lblApproved";
                    }
                    else if (lblStatus.Text == "Draft")
                    {
                        lblStatus.CssClass = "lblDraft";
                    }
                    else
                    {
                        lblStatus.CssClass = "lblRejected";
                    }
                }
            }
        }
        #endregion

        #region Grid Controls
        private void PaginationButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                string dynamicButtonID = clickedButton.ID;
                int intClickedID = 1;

                foreach (Control control in pnlPagination.Controls)
                {
                    if (control is Button button)
                    {
                        button.Style.Remove("Background");
                        button.Style.Remove("Color");
                    }
                }

                clickedButton.Style.Add("Background", "#69022f");
                clickedButton.Style.Add("Color", "#fff");

                int Test = int.Parse(ViewState["btnPagination_Count"].ToString());

                if (ViewState["Current_Page"] == null)
                {
                    ViewState["Current_Page"] = 1;
                }

                if (dynamicButtonID == "btnPrev")
                {
                    intClickedID = int.Parse(ViewState["Current_Page"].ToString()) - 1;
                    ViewState["Current_Page"] = intClickedID;
                }
                else if (dynamicButtonID == "btnNext")
                {
                    intClickedID = int.Parse(ViewState["Current_Page"].ToString()) + 1;
                    ViewState["Current_Page"] = intClickedID;
                }
                else
                {
                    intClickedID = int.Parse(dynamicButtonID.Replace("btn", "").ToString());
                    ViewState["Current_Page"] = intClickedID;
                }

                if (intClickedID < 1) { intClickedID = 1; ViewState["Current_Page"] = intClickedID; }
                else if (intClickedID > int.Parse(ViewState["btnPagination_Count"].ToString())) { intClickedID = int.Parse(ViewState["btnPagination_Count"].ToString()); ViewState["Current_Page"] = intClickedID; }

                string strSearchStatus = ddlsrcStatus.SelectedItem.ToString();
                string strSearchRole = ddlsrcRole.SelectedItem.ToString();

                ds = methods.FilterGrid(methods.strCon, Session["USER_ID"].ToString(), strSearchStatus, strSearchRole, txtSrcUserID.Text, intClickedID, int.Parse(ddlShowCount.SelectedItem.ToString()), txtSrcAll.Text);

                ViewState["Current_Page"] = intClickedID;

                dt = methods.RemoveWhiteSpaces(ds.Tables[0]);

                gvManageUsers.DataSource = dt;
                gvManageUsers.DataBind();

                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

                SetGridComponents();
                CreatePaginationButtons();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "PaginationButton_Click");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }
        
        protected void ddlShowCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["Current_Page"] = 1;
                CreatePaginationButtons();

                SetGridComponents();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "ddlShowCount_SelectedIndexChanged");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose(); BindGrid();
            }
        }

        protected void ddlsrcStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CreatePaginationButtons();

                string strSearchStatus = ddlsrcStatus.SelectedItem.ToString();
                string strSearchRole = ddlsrcRole.SelectedItem.ToString();

                ds = methods.FilterGrid(methods.strCon, Session["USER_ID"].ToString(), strSearchStatus, strSearchRole, txtSrcUserID.Text, 1, int.Parse(ddlShowCount.SelectedItem.ToString()), txtSrcAll.Text);

                dt = ds.Tables[0];

                gvManageUsers.DataSource = dt;
                gvManageUsers.DataBind();
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

                SetGridComponents();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "ddlsrcStatus_SelectedIndexChanged");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose(); BindGrid();
            }
        }

        protected void ddlsrcRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CreatePaginationButtons();

                string strSearchStatus = ddlsrcStatus.SelectedItem.ToString();
                string strSearchRole = ddlsrcRole.SelectedItem.ToString();

                methods.FilterGrid(methods.strCon, Session["USER_ID"].ToString(), strSearchStatus, strSearchRole, txtSrcUserID.Text, 1, int.Parse(ddlShowCount.SelectedItem.ToString()), txtSrcAll.Text);

                gvManageUsers.DataSource = dt;
                gvManageUsers.DataBind();
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

                SetGridComponents();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "ddlsrcRole_SelectedIndexChanged");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose(); BindGrid();
            }
        }
        #endregion

        protected void tabSubmit_Click(object sender, EventArgs e)
        {
            View("SaveSubmit");
            hdnUserID.Value = "";
            ClearAll();
        }

        protected void tabViewSearch_Click(object sender, EventArgs e)
        {
            View("ViewSearch");
            hdnUserID.Value = "";
        }

        protected bool CheckEmptyFields()
        {
            warFirstName.Visible = false;
            warEmailID.Visible = false;
            warDOB.Visible = false;
            warPhoneno.Visible = false;
            warCity.Visible = false;
            warGender.Visible = false;
            warRole.Visible = false;

            bool isValid = true;
            if(txtFirstname.Text == "")
            {
                warFirstName.Visible = true;
                isValid = false;
            }
            else
            {
                warFirstName.Visible = false;
            }
            if(txtEmailID.Text == "")
            {
                warEmailID.Visible = true;
                isValid = false;
            }
            else
            {
                warEmailID.Visible = false;
            }
            if (txtDOB.Text == "")
            {
                warDOB.Visible = true;
                isValid = false;
            }
            else
            {
                warDOB.Visible = false;
            }
            if (txtPhoneno.Text == "")
            {
                warPhoneno.Text = "Please Enter Phone no.";
                warPhoneno.Visible = true;
                isValid = false;
            }
            else if (txtPhoneno.Text.Length < 10)
            {
                warPhoneno.Text = "Phone no. should be of 10 digits.";
                warPhoneno.Visible = true;
                isValid = false;
            }
            else
            {
                warPhoneno.Visible = false;
            }
            if (ddlCity.SelectedValue == "Select")
            {
                warCity.Visible = true;
                isValid = false;
            }
            else
            {
                warCity.Visible = false;
            }
            if (ddlGender.SelectedValue == "Select")
            {
                warGender.Visible = true;
                isValid = false;
            }
            else
            {
                warGender.Visible = false;
            }
            if (ddlRole.SelectedValue == "Select")
            {
                warRole.Visible = true;
                isValid = false;
            }
            else
            {
                warRole.Visible = false;
            }

            return isValid;
        }

        protected void View(string view)
        {
            EnableDisable("Enable");
            btnSaveDraft.Visible = true;
            btnAddusers.Visible = true;
            btnApprove.Visible = true;
            btnReject.Visible = true;
            txtRejectionReason.Visible = true;
            txtPassword.Enabled = false;

            if (view == "ApproveReject")
            {
                EnableDisable("Enable");
                tabSubmit.Style.Add("color", "#69022f");
                tabSubmit.Style.Add("background", "#fff");
                pnlSubmit.Visible = true;


                tabViewSearch.Style.Add("color", "grey");
                tabViewSearch.Style.Add("background", "none");
                pnlViewSearch.Visible = false;

                pnlSaveSubmit.Visible = false;
                pnlApproveReject.Visible = true;
                lblRejectionReason.Visible = true;

                txtComment.Enabled = false;
            }
            else if(view == "SaveSubmit")
            {
                EnableDisable("Enable");
                tabSubmit.Style.Add("color", "#69022f");
                tabSubmit.Style.Add("background", "#fff");
                pnlSubmit.Visible = true;


                tabViewSearch.Style.Add("color", "grey");
                tabViewSearch.Style.Add("background", "none");
                pnlViewSearch.Visible = false;

                pnlSaveSubmit.Visible = true;
                pnlApproveReject.Visible = true;

                btnApprove.Visible = false;
                btnReject.Visible = false;
                if(txtRejectionReason.Text == "")
                {
                    lblRejectionReason.Visible = false;
                    txtRejectionReason.Visible = false;
                }
                else
                {
                    lblRejectionReason.Visible = true;
                    txtRejectionReason.Visible = true;
                    txtRejectionReason.Enabled = false;
                }
                lblComment.Visible = true;
                txtComment.Visible = true;

                warFirstName.Visible = false;
                warEmailID.Visible = false;
                warDOB.Visible = false;
                warPhoneno.Visible = false;
                warCity.Visible = false;
                warGender.Visible = false;
                warRole.Visible = false;
                txtPassword.Text = methods.strDefaultPass;
                //txtPassword.Enabled = false;

                //pnlApproveReject.Visible = false;
            }
            else if(view == "ViewSearch")
            {
                tabViewSearch.Style.Add("color", "#69022f");
                tabViewSearch.Style.Add("background", "#fff");
                pnlViewSearch.Visible = true;

                tabSubmit.Style.Add("color", "grey");
                tabSubmit.Style.Add("background", "none");
                pnlSubmit.Visible = false;
            }
            else if (view == "Rejected")
            {
                EnableDisable("Disable");
                tabSubmit.Style.Add("color", "#69022f");
                tabSubmit.Style.Add("background", "#fff");
                pnlSubmit.Visible = true;


                tabViewSearch.Style.Add("color", "grey");
                tabViewSearch.Style.Add("background", "none");
                pnlViewSearch.Visible = false;

                pnlSaveSubmit.Visible = false;
                pnlApproveReject.Visible = true;

                btnApprove.Visible = false;
                btnReject.Visible = false;
                txtRejectionReason.Visible = true;
                txtRejectionReason.Enabled = false;
                if (txtComment.Text == "")
                {
                    lblComment.Visible = false;
                    txtComment.Visible = false;
                }
                else
                {
                    lblComment.Visible = true;
                    txtComment.Visible = true;
                    txtComment.Enabled = false;
                }
            }
            else if (view == "Approved")
            {
                EnableDisable("Disable");
                tabSubmit.Style.Add("color", "#69022f");
                tabSubmit.Style.Add("background", "#fff");
                pnlSubmit.Visible = true;


                tabViewSearch.Style.Add("color", "grey");
                tabViewSearch.Style.Add("background", "none");
                pnlViewSearch.Visible = false;

                pnlSaveSubmit.Visible = false;
                pnlApproveReject.Visible = true;

                btnApprove.Visible = false;
                btnReject.Visible = false;

                lblRejectionReason.Visible = false;
                txtRejectionReason.Visible = false;

                if (txtComment.Text == "")
                {
                    lblComment.Visible = false;
                    txtComment.Visible = false;
                }
                else
                {
                    lblComment.Visible = true;
                    txtComment.Visible = true;
                    txtComment.Enabled = false;
                }
            }
            else if (view == "ReadOnly")
            {
                EnableDisable("Disable");
                tabSubmit.Style.Add("color", "#69022f");
                tabSubmit.Style.Add("background", "#fff");
                pnlSubmit.Visible = true;


                tabViewSearch.Style.Add("color", "grey");
                tabViewSearch.Style.Add("background", "none");
                pnlViewSearch.Visible = false;

                pnlSaveSubmit.Visible = false;
                pnlApproveReject.Visible = true;

                btnApprove.Visible = false;
                btnReject.Visible = false;

                if (txtRejectionReason.Text == "")
                {
                    lblRejectionReason.Visible = false;
                    txtRejectionReason.Visible = false;
                }
                else
                {
                    lblRejectionReason.Visible = true;
                    txtRejectionReason.Visible = true;
                }

                lblComment.Visible = true;
                txtComment.Visible = true;
            }
            else if(view == "Clear Submit")
            {
                warFirstName.Visible = false;
                warEmailID.Visible = false;
                warDOB.Visible = false;
                warPhoneno.Visible = false;
                warCity.Visible = false;
                warGender.Visible = false;
                warRole.Visible = false;
                txtPassword.Text = methods.strDefaultPass;
                //txtPassword.Enabled = false;

                pnlApproveReject.Visible = false;
            }
        }
        protected void gvlblUserID_Click(object sender, EventArgs e)
        {
            LinkButton lnkbutton = (LinkButton)sender;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            ClearAll();
            try
            {
                hdnUserID.Value = lnkbutton.Text;

                dt = methods.GetUserDetails(methods.strCon, dt, ds, adp, hdnUserID.Value);

                if (dt.Rows.Count > 0)
                {
                    String Username = dt.Rows[0]["USERNAME"].ToString();
                    char[] delimiter = { ' ' };
                    string[] result = Username.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

                    
                    if(result.Length > 1) 
                    {
                        txtFirstname.Text = result[0].ToString();
                        txtLastname.Text = result[1].ToString();
                    }
                    else
                    {
                        txtFirstname.Text = result[0].ToString();
                    }
                    
                    txtEmailID.Text = dt.Rows[0]["EMAIL_ID"].ToString();
                    txtPassword.Text = dt.Rows[0]["PASSWORD"].ToString();

                    if(dt.Rows[0]["DOB"].ToString() != "")
                    {
                        txtDOB.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    } 
                    

                    txtPhoneno.Text = dt.Rows[0]["PHONE_NO"].ToString();
                    if(dt.Rows[0]["CITY"].ToString() == "")
                    {
                        ddlCity.Text = "Select";
                    }
                    else
                    {
                        ddlCity.Text = dt.Rows[0]["CITY"].ToString();
                    }
                    
                    if(dt.Rows[0]["GENDER"].ToString() == "")
                    {
                        ddlGender.Text = "Select";
                    }
                    else
                    {
                        ddlGender.Text = dt.Rows[0]["GENDER"].ToString();
                    }
                    
                    if(dt.Rows[0]["ROLE"].ToString() == "")
                    {
                        ddlRole.Text = "Select";
                    }
                    else
                    {
                        ddlRole.Text = dt.Rows[0]["ROLE"].ToString();
                    }

                    if (dt.Rows[0]["COMMENT"].ToString() == "")
                    {
                        txtComment.Text = "";
                    }
                    else
                    {
                        txtComment.Text = dt.Rows[0]["COMMENT"].ToString();
                    }

                    txtRejectionReason.Text = dt.Rows[0]["REJECTION_REASON"].ToString();

                    remUserID.Text = "User ID : " + hdnUserID.Value;
                    remStatus.Text = "Status : " + dt.Rows[0]["STATUS"].ToString();
                    remCreatedBy.Text = "Created By : " + dt.Rows[0]["CREATED_BY_NAME"].ToString();

                    //If same user is the creator
                    if (Session["USER_ID"].ToString() == dt.Rows[0]["CREATED_BY"].ToString())
                    {
                        if(dt.Rows[0]["STATUS"].ToString() == "Draft")
                        {
                            View("SaveSubmit");
                        }
                        else if(dt.Rows[0]["STATUS"].ToString() == "Rejected")
                        {
                            View("SaveSubmit");
                            txtRejectionReason.Enabled = false;
                        }
                        else if (dt.Rows[0]["STATUS"].ToString() == "Approved")
                        {
                            View("Approved");
                        }
                        else
                        {
                            View("ReadOnly");
                        }
                        
                    }
                    else
                    {
                        if (dt.Rows[0]["STATUS"].ToString() == "Rejected")
                        {
                            View("Rejected");
                        }
                        else if (dt.Rows[0]["STATUS"].ToString() == "Approved")
                        {
                            View("Approved");
                        }
                        else if (dt.Rows[0]["STATUS"].ToString() == "Pending")
                        {
                            if (Session["ROLE_ID"].ToString() == "1")//Admin can approve reject
                            {
                                View("ApproveReject");
                            }
                            else//Other cannot approve or reject
                            {
                                View("ReadOnly");
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong..')", true);
                }
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "gvlblUserID_Click");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            if (txtRejectionReason.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please fill rejection reason')", true);
            }
            else
            {
                try
                {
                   dt =  methods.AddUsers(methods.strCon, dt, ds, adp, "Reject", hdnUserID.Value, (txtFirstname.Text + ' ' + txtLastname.Text), txtEmailID.Text, txtPassword.Text, txtDOB.Text, ddlGender.SelectedValue, ddlCity.SelectedValue, txtPhoneno.Text, "2", ddlRole.SelectedValue, Session["USER_ID"].ToString(), txtComment.Text, txtRejectionReason.Text);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('User rejected successfully.', '', 'success', '')", true);
                            View("ViewSearch");
                            BindGrid();
                            CreatePaginationButtons();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong..')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong..')", true);
                    }
                }
                catch (Exception ex)
                {
                    SqlConnection con = new SqlConnection(methods.strCon);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ERROR_NAME", "btnReject_Click");
                    cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                finally
                {
                    dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose(); BindGrid();
                }
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            if (txtRejectionReason.Text != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please remove rejection reason.')", true);
                txtRejectionReason.Focus();
            }
            else
            {
                try
                {
                    dt = methods.AddUsers(methods.strCon, dt, ds, adp, "Approve", hdnUserID.Value, (txtFirstname.Text + ' ' + txtLastname.Text), txtEmailID.Text, txtPassword.Text, txtDOB.Text, ddlGender.SelectedValue, ddlCity.SelectedValue, txtPhoneno.Text, "1", ddlRole.SelectedValue, Session["USER_ID"].ToString(), txtComment.Text, txtRejectionReason.Text);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('User approved successfully.', '', 'success', '')", true);
                            View("ViewSearch");
                            BindGrid();
                            CreatePaginationButtons();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong..')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong..')", true);
                    }
                }
                catch (Exception ex)
                {
                    SqlConnection con = new SqlConnection(methods.strCon);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ERROR_NAME", "btnApprove_Click");
                    cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                finally
                {
                    dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose(); BindGrid();
                }
            }
        }

        protected void ClearAll()
        {
            remUserID.Text = "";
            remStatus.Text = "";
            remCreatedBy.Text = "";

            txtFirstname.Text = "";
            txtLastname.Text = "";
            txtEmailID.Text = "";
            txtDOB.Text = ""; txtPhoneno.Text = "";
            ddlCity.SelectedValue = "Select";
            ddlGender.SelectedValue = "Select";
            ddlRole.SelectedValue = "Select";
            txtComment.Text = "";
            txtRejectionReason.Text = "";
            txtPassword.Text = methods.strDefaultPass;
            txtPassword.Enabled = false;
            //txtComment
        }
        
        protected void EnableDisable(string input)
        {
            if(input == "Enable")
            {
                txtFirstname.Enabled = true;
                txtLastname.Enabled = true;
                txtEmailID.Enabled = true;
                txtPassword.Enabled = true;
                txtDOB.Enabled = true; txtPhoneno.Enabled = true;
                ddlCity.Enabled = true;
                ddlGender.Enabled = true;
                ddlRole.Enabled = true;
                txtRejectionReason.Enabled = true;
                txtComment.Enabled = true;
            }
            else
            {
                txtFirstname.Enabled = false;
                txtLastname.Enabled = false;
                txtEmailID.Enabled = false;
                txtPassword.Enabled = false;
                txtDOB.Enabled = false; txtPhoneno.Enabled = false;
                ddlCity.Enabled = false;
                ddlGender.Enabled = false;
                ddlRole.Enabled = false;
                txtRejectionReason.Enabled = false;
                txtComment.Enabled = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["Current_Page"] = 1;
            BindGrid();
            CreatePaginationButtons();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSrcUserID.Text = "";
            ddlsrcStatus.SelectedValue = "Select";
            ddlsrcRole.SelectedValue = "Select";
            ddlShowCount.SelectedValue = "1";
            txtSrcAll.Text = "";
            BindGrid();
            CreatePaginationButtons();
        }

        protected void txtSrcAll_TextChanged(object sender, EventArgs e)
        {
            ViewState["Current_Page"] = 1;
            BindGrid();
            CreatePaginationButtons();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
        }

        protected void ddlRole_TextChanged(object sender, EventArgs e)
        {
            if(Session["ROLE_ID"].ToString() == "2" && ddlRole.SelectedValue == "Admin")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only admin can add other admins.')", true);

                ddlRole.SelectedValue = "Select";
                bool isVlid = CheckEmptyFields();
            }
            else
            {
                bool isVlid = CheckEmptyFields();
            }
        }
    }
}