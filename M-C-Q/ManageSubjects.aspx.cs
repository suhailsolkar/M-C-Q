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
    public partial class ManageSubjects : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        Methods methods = new Methods();
        Pagination pagination = new Pagination();
        int intRowCount;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "Test()", true);
            if (!IsPostBack)
            {
                BindDropdowns();
                tabSubmit.Style.Add("color", "#69022f");
                tabSubmit.Style.Add("background", "#fff");
                pnlSubmit.Visible = true;


                tabViewSearch.Style.Add("color", "grey");
                tabViewSearch.Style.Add("background", "none");
                pnlViewSearch.Visible = false;

                BindGrid();
                View("Clear Submit");
            }
            else
            {

            }
            if (Session["USERNAME"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            CreatePaginationButtons();
        }

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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            if (txtRejectionReason.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please fill rejection reason.')", true);
            }
            else
            {
                try
                {
                    dt = methods.AddSubjects(strCon, dt, ds, adp, "Reject", Session["USER_ID"].ToString(), hdnUserID.Value, txtSubejctName.Text, ddlSubjectTeacher.SelectedValue, txtNoofquestions.Text, txtTotakmarks.Text, "2", txtSubjectDesc.Text, txtComment.Text, txtRejectionReason.Text);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('Subject rejected successfully.', '', 'success', '')", true);
                            View("ViewSearch");
                            BindGrid();
                            CreatePaginationButtons();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong.')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong.')", true);
                    }
                }
                catch (Exception ex)
                {
                    SqlConnection con = new SqlConnection(strCon);
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
                    dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please remove rejection reason..')", true);
            }
            else
            {
                try
                {
                    dt = methods.AddSubjects(strCon, dt, ds, adp, "Approve", Session["USER_ID"].ToString(), hdnUserID.Value, txtSubejctName.Text, ddlSubjectTeacher.SelectedValue, txtNoofquestions.Text, txtTotakmarks.Text, "1", txtSubjectDesc.Text, txtComment.Text, "");

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('Subject approved successfully.', '', 'success', '')", true);
                            View("ViewSearch");
                            BindGrid();
                            CreatePaginationButtons();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong.')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowething went wrong.')", true);
                    }
                }
                catch (Exception ex)
                {
                    SqlConnection con = new SqlConnection(strCon);
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
                dt = methods.AddSubjects(strCon, dt, ds, adp, "Save Draft", Session["USER_ID"].ToString(), hdnUserID.Value, txtSubejctName.Text, ddlSubjectTeacher.SelectedValue, txtNoofquestions.Text, txtTotakmarks.Text, "4", txtSubjectDesc.Text, txtComment.Text, "");

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["RESULT"].ToString() == "SUCCESS")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('Subject saved draft successfully.', '', 'success', '')", true);
                        View("ViewSearch");
                        BindGrid();
                        CreatePaginationButtons();

                    }
                    else if (dt.Rows[0]["RESULT"].ToString() == "APPROVED")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This subject is approved by Admin.')", true);
                    }
                    else if (dt.Rows[0]["RESULT"].ToString() == "REJECTED")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This subject is rejected by Admin.')", true);
                    }
                    else if (dt.Rows[0]["RESULT"].ToString() == "PENDING")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This subject is pending for approval.')", true);
                    }
                    else if (dt.Rows[0]["RESULT"].ToString() == "DRAFT")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This subject is saved as draft.')", true);
                    }
                    else if (dt.Rows[0]["RESULT"].ToString() == "FAIL")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Subject already exists for selected role.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowmthing went wrong..')", true);
                }
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(strCon);
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
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }

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

                    if (Session["ROLE_ID"].ToString() == "1")
                    {
                        strStatus = "1";
                    }
                    else
                    {
                        strStatus = "3";
                    }

                    dt = methods.AddSubjects(strCon, dt, ds, adp, "Submit", Session["USER_ID"].ToString(), hdnUserID.Value, txtSubejctName.Text, ddlSubjectTeacher.SelectedValue, txtNoofquestions.Text, txtTotakmarks.Text, strStatus, txtSubjectDesc.Text, txtComment.Text, "");

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "swal('Subject submitted successfully.', '', 'success', '')", true);
                            View("ViewSearch");
                            BindGrid();
                            CreatePaginationButtons();
                        }
                        else if (dt.Rows[0]["RESULT"].ToString() == "APPROVED")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This subject is approved by admin.')", true);
                        }
                        else if (dt.Rows[0]["RESULT"].ToString() == "REJECTED")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This subject is rejected by admin.')", true);

                        }
                        else if (dt.Rows[0]["RESULT"].ToString() == "PENDING")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This subject is pending for approval.')", true);
                        }
                        else if (dt.Rows[0]["RESULT"].ToString() == "DRAFT")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This subject is saved as draft.')", true);
                        }
                        else if (dt.Rows[0]["RESULT"].ToString() == "FAIL")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Subject already exists for selected role.')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sowmthing went wrong..')", true);
                    }
                }
                catch (Exception ex)
                {
                    SqlConnection con = new SqlConnection(strCon);
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
                SqlConnection con = new SqlConnection(strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "ManageSubjects/ddlShowCount_SelectedIndexChanged");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose(); BindGrid();
            }
        }

        protected void gvlblUserID_Click(object sender, EventArgs e)
        {
            LinkButton lnkbutton = (LinkButton)sender;
            //Label lblStatus = (Label)sender;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            ClearAll();
            try
            {
                hdnUserID.Value = lnkbutton.Text;

                dt = methods.GetSubjectDetails(strCon, dt, ds, adp, hdnUserID.Value);

                if (dt.Rows.Count > 0)
                {
                    String Username = dt.Rows[0]["USERNAME"].ToString();
                    char[] delimiter = { ' ' };
                    string[] result = Username.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

                    if (dt.Rows[0]["SUBJECT_NAME"].ToString() != "")
                    {
                        txtSubejctName.Text = dt.Rows[0]["SUBJECT_NAME"].ToString();
                    }
                    if(dt.Rows[0]["USERNAME"].ToString() != "")
                    {
                        ddlSubjectTeacher.SelectedValue = dt.Rows[0]["USERNAME"].ToString();
                    }
                    if (dt.Rows[0]["NO_OF_QUESTIONS"].ToString() != "")
                    {
                        txtNoofquestions.Text = dt.Rows[0]["NO_OF_QUESTIONS"].ToString();
                    }
                    if (dt.Rows[0]["TOTAL_MARKS"].ToString() != "")
                    {
                        txtTotakmarks.Text = dt.Rows[0]["TOTAL_MARKS"].ToString();
                    }
                    if (dt.Rows[0]["SUBJECT_DESCRIPTION"].ToString() != "")
                    {
                        txtSubjectDesc.Text = dt.Rows[0]["SUBJECT_DESCRIPTION"].ToString();
                    }
                    if (dt.Rows[0]["COMMENT"].ToString() != "")
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
                        if (dt.Rows[0]["STATUS"].ToString() == "Draft")
                        {
                            View("SaveSubmit");
                        }
                        else if (dt.Rows[0]["STATUS"].ToString() == "Rejected")
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
                            EnableDisable("Disable");
                        }
                        else if (dt.Rows[0]["STATUS"].ToString() == "Approved")
                        {
                            View("Approved");
                        }
                        else if(dt.Rows[0]["STATUS"].ToString() == "Pending")
                        {
                            if(Session["ROLE_ID"].ToString() == "1")//Admin can approve reject
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
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('error','Sowmthing went wrong.', '')", true);
                }
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(strCon);
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSrcSubjectID.Text = "";
            ddlsrcSubjectName.SelectedValue = "Select";
            ddlsrcSubjectTeacher.SelectedValue = "Select";
            ddlsrcStatus.SelectedValue = "Select";
            ddlShowCount.SelectedValue = "1";
            BindGrid();
            CreatePaginationButtons();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
            CreatePaginationButtons();
        }

        protected void ClearAll()
        {
            remUserID.Text = "";
            remStatus.Text = "";
            remCreatedBy.Text = "";

            txtSubejctName.Text = "";
            ddlSubjectTeacher.SelectedValue = "Select";
            txtNoofquestions.Text = "";
            txtTotakmarks.Text = "";
            txtSubjectDesc.Text = ""; txtComment.Text = "";
            txtRejectionReason.Text = "";

            //txtComment
        }

        protected void EnableDisable(string input)
        {
            if (input == "Enable")
            {
                txtSubejctName.Enabled = true;
                ddlSubjectTeacher.Enabled = true;
                txtNoofquestions.Enabled = true;
                txtTotakmarks.Enabled = true;
                txtSubjectDesc.Enabled = true;
                txtRejectionReason.Enabled = true;
                txtComment.Enabled = true;
            }
            else
            {
                txtSubejctName.Enabled = false;
                ddlSubjectTeacher.Enabled = false;
                txtNoofquestions.Enabled = false;
                txtTotakmarks.Enabled = false;
                txtSubjectDesc.Enabled = false;
                txtRejectionReason.Enabled = false;
                txtComment.Enabled = false;
            }
        }
        private void BindDropdowns()
        {
            try
            {
                methods.fetchTeachersDrpdowns(strCon, dt, ds, adp);  
                      
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {      
                    if(dt.Rows[j]["DROPDOWN_NAME"].ToString() == "TEACHERS")
                    {
                        ddlSubjectTeacher.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                        ddlsrcSubjectTeacher.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                    }
                    else if (dt.Rows[j]["DROPDOWN_NAME"].ToString() == "STATUS")
                    {
                        ddlsrcStatus.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                    }
                    else if (dt.Rows[j]["DROPDOWN_NAME"].ToString() == "SUBJECT_NAME")
                    {
                        ddlsrcSubjectName.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                    }
                }
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(strCon);
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

        protected bool CheckEmptyFields()
        {
            bool isValid = true;
            if (txtSubejctName.Text == "")
            {
                warSubejctName.Visible = true;
                isValid = false;
            }
            else
            {
                warSubejctName.Visible = false;
            }
            if (ddlSubjectTeacher.SelectedValue == "Select")
            {
                warSubjectTeacher.Visible = true;
                isValid = false;
            }
            else
            {
                warSubjectTeacher.Visible = false;
            }

            if (txtNoofquestions.Text == "")
            {
                warNoofQuestions.Visible = true;
                isValid = false;
            }
            else
            {
                warNoofQuestions.Visible = false;
            }

            //if (txtTotakmarks.Text == "")
            //{
            //    warTotalMarks.Visible = true;
            //    isValid = false;
            //}
            //else
            //{
            //    warTotalMarks.Visible = false;
            //}
            if (txtSubjectDesc.Text == "")
            {
                warSubjectDesc.Visible = true;
                isValid = false;
            }
            else
            {
                warSubjectDesc.Visible = false;
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
            txtTotakmarks.Enabled = false;

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
            }
            else if (view == "SaveSubmit")
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

                txtTotakmarks.Enabled = false;

                if (txtRejectionReason.Text == "")
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

                warSubejctName.Visible = false;
                warSubjectTeacher.Visible = false;
                warNoofQuestions.Visible = false;
                warTotalMarks.Visible = false;
                warSubjectDesc.Visible = false;

                //txtNoofquestions.Text = "0";
                //txtTotakmarks.Text = Convert.ToString(int.Parse(txtNoofquestions.Text) * 2);
                txtTotakmarks.Enabled = false;

                pnlApproveReject.Visible = false;
            }
            else if (view == "ViewSearch")
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
                EnableDisable("Enable");
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
                pnlApproveReject.Visible = false;

                btnApprove.Visible = false;
                btnReject.Visible = false;
                txtRejectionReason.Visible = false;
                txtRejectionReason.Enabled = false;
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
            else if (view == "Clear Submit")
            {
                warSubejctName.Visible = false;
                warSubjectTeacher.Visible = false;
                warNoofQuestions.Visible = false;
                warTotalMarks.Visible = false;
                warSubjectDesc.Visible = false;

                //txtNoofquestions.Text = "0";
                //txtTotakmarks.Text = Convert.ToString(int.Parse(txtNoofquestions.Text) * 2);
                txtTotakmarks.Enabled = false;

                pnlApproveReject.Visible = false;
            }
        }

        

        private void BindGrid()
        {
            try
            {
                string strSearchStatus = ddlsrcStatus.SelectedItem.ToString();
                string strSubjectName = ddlsrcSubjectName.SelectedItem.ToString();
                string strSubjectTeacer = ddlsrcSubjectTeacher.SelectedItem.ToString();

                ds = methods.FilterGridSubjects(strCon, Session["USER_ID"].ToString(), txtSrcSubjectID.Text, strSubjectName, strSubjectTeacer, strSearchStatus, 1, int.Parse(ddlShowCount.SelectedItem.ToString()), txtSrcAll.Text);

                dt = methods.RemoveWhiteSpaces(ds.Tables[0]);

                ViewState["COUNT"] = ds.Tables[1].Rows[0]["COUNT"].ToString();

                gvManageUsers.DataSource = dt;
                gvManageUsers.DataBind();


                SetGridComponents();
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(strCon);
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

        private void SetGridComponents()
        {
            foreach (GridViewRow row in gvManageUsers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Find the TextBox control in the row
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

        private void CreatePaginationButtons()
        {

            try
            {
                string strSearchStatus = ddlsrcStatus.SelectedItem.ToString();
                string strSubjectName = ddlsrcSubjectName.SelectedItem.ToString();
                string strSubjectTeacer = ddlsrcSubjectTeacher.SelectedItem.ToString();

                intRowCount = int.Parse(ViewState["COUNT"].ToString());
                lblCount.Text = "of " + intRowCount + " records";

                //New
                string strCurrentPage = "1";

                if (ViewState["Current_Page"] != null)
                {
                    strCurrentPage = ViewState["Current_Page"].ToString();
                }
                else
                {
                    strCurrentPage = "1";
                }
                //New

                ViewState["btnPagination_Count"] = pagination.CreatePaginationButtons2(pnlPagination, int.Parse(strCurrentPage), ddlShowCount.SelectedItem.ToString(), intRowCount.ToString(), PaginationButton_Click, strCon, dt, ds, adp, Session["USER_ID"].ToString());
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(strCon);
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

                ds = methods.FilterGridSubjects(strCon, Session["USER_ID"].ToString(), txtSrcSubjectID.Text, ddlsrcSubjectName.SelectedValue, ddlsrcSubjectTeacher.SelectedValue, strSearchStatus, intClickedID, int.Parse(ddlShowCount.SelectedItem.ToString()), txtSrcAll.Text);

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
                SqlConnection con = new SqlConnection(strCon);
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

        protected void txtSrcAll_TextChanged(object sender, EventArgs e)
        {
            ViewState["Current_Page"] = 1;
            BindGrid();
            CreatePaginationButtons();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
        }
    }
}