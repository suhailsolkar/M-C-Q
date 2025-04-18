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
    public class Pagination
    {

        public string CreatePaginationButtons(Panel pnlPagination, DropDownList ddlShowCount, string strCount, EventHandler PaginationButton_Click, string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string sessionUserID)
        {
            string vsbtnPagination_Count = "0";
            try
            {
                pnlPagination.Controls.Clear();

                //string strSearchStatus = ddlsrcStatus.SelectedItem.ToString();
                //string strSearchRole = ddlsrcRole.SelectedItem.ToString();

                //dt = GetTblUsersCount(strCon, dt, ds, adp, sessionUserID, strSearchStatus, strSearchRole, txtSrcUserID.Text, 1, int.Parse(ddlShowCount.SelectedItem.ToString()));

                //int intRowCount = int.Parse(dt.Rows[0]["COUNT"].ToString());
                //lblCount.Text = "of " + intRowCount + " records";

                int i = 0;
                int j = int.Parse(ddlShowCount.SelectedItem.ToString());
                int btnID = 1;
                while (i <= int.Parse(strCount))
                {
                    if (i == 0)
                    {
                        Button btnPrev = new Button();
                        btnPrev.Text = "Prev";
                        btnPrev.ID = "btnPrev";
                        btnPrev.CssClass = "btnPrev";
                        btnPrev.Click += new EventHandler(PaginationButton_Click);
                        pnlPagination.Controls.Add(btnPrev);
                    }
                    if (i == j || i == int.Parse(strCount))
                    {
                        j = j + int.Parse(ddlShowCount.SelectedItem.ToString());
                        Button newButton = new Button();
                        newButton.Text = btnID.ToString();
                        newButton.ID = "btn" + btnID.ToString();
                        newButton.CssClass = "btnPagination";
                        newButton.Click += new EventHandler(PaginationButton_Click);
                        pnlPagination.Controls.Add(newButton);
                        vsbtnPagination_Count = btnID.ToString();
                        btnID++;

                        newButton.Style.Remove("Background");
                        newButton.Style.Remove("Color");

                        if (newButton.ID == "btn1")
                        {
                            newButton.Style.Add("Background", "#69022f");
                            newButton.Style.Add("Color", "#fff");
                        }
                    }
                    i++;
                }
                Button btnNext = new Button();
                btnNext.Text = "Next";
                btnNext.ID = "btnNext";
                btnNext.CssClass = "btnNext";
                btnNext.Click += new EventHandler(PaginationButton_Click);
                pnlPagination.Controls.Add(btnNext);
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "Pagination/CreatePaginationButtons");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
            return vsbtnPagination_Count;
        }

        public string CreatePaginationButtons2(Panel pnlPagination, int intPageno, string ddlShowCount, string strCount, EventHandler PaginationButton_Click, string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string sessionUserID)
        {
            string vsbtnPagination_Count = "0";
            try
            {
                pnlPagination.Controls.Clear();

                int intRange = int.Parse(strCount);
                int intRow = int.Parse(ddlShowCount);

                if (intRange % intRow == 0)
                {
                    intRange = intRange / intRow;
                }
                else
                {
                    intRange = intRange / intRow + 1;
                }

                //int i = 0;
                int j = int.Parse(ddlShowCount);

                Button btnPrev = new Button();
                btnPrev.Text = "<";
                btnPrev.ID = "btnPrev";
                btnPrev.CssClass = "btnPrev";
                btnPrev.Click += new EventHandler(PaginationButton_Click);
                pnlPagination.Controls.Add(btnPrev);

                if (intPageno == 1)
                {
                    //btnPrev.Style.Add("cursor", "default");
                    //btnPrev.CssClass = "disableNextPrev";
                    btnPrev.Enabled = false;
                }

                if (intPageno >= 3)
                {
                    Button btn1st = new Button();
                    btn1st.Text = "1";
                    btn1st.ID = "btn1";
                    btn1st.CssClass = "btnPagination";
                    btn1st.Click += new EventHandler(PaginationButton_Click);
                    pnlPagination.Controls.Add(btn1st);

                    Label lblExt1 = new Label();
                    lblExt1.Text = "...";
                    lblExt1.ID = "lblExt1";
                    lblExt1.CssClass = "lblExt";
                    pnlPagination.Controls.Add(lblExt1);
                }

                for (int i = 1; i <= intRange; i++)
                {
                    if(intRange <= intRow)
                    {
                        if((i - 1) + intPageno <= intRange)
                        {
                            Button newButton = new Button();
                            newButton.Text = (i + (intPageno - 1)).ToString();
                            newButton.ID = "btn" + (i + (intPageno - 1)).ToString();
                            newButton.CssClass = "btnPagination";
                            newButton.Click += new EventHandler(PaginationButton_Click);
                            pnlPagination.Controls.Add(newButton);
                            vsbtnPagination_Count = (i + (intPageno - 1)).ToString();

                            if (i == 1)
                            {
                                newButton.CssClass = "clicked-button";
                            }
                        }
                    }
                    else
                    {
                        if (i <= 5)
                        {
                            if (i + intPageno <= intRange)
                            {
                                Button newButton = new Button();
                                newButton.Text = (i + (intPageno - 1)).ToString();
                                newButton.ID = "btn" + (i + (intPageno - 1)).ToString();
                                newButton.CssClass = "btnPagination";
                                newButton.Click += new EventHandler(PaginationButton_Click);
                                pnlPagination.Controls.Add(newButton);
                                vsbtnPagination_Count = (i + (intPageno - 1)).ToString();

                                if (i == 1)
                                {
                                    newButton.CssClass = "clicked-button";
                                }
                            }

                        }
                        else
                        {
                            if (intPageno < intRange - 5)
                            {
                                Label lblExt2 = new Label();
                                lblExt2.Text = "...";
                                lblExt2.ID = "lblExt2";
                                lblExt2.CssClass = "lblExt";
                                pnlPagination.Controls.Add(lblExt2);
                            }

                            Button newButton = new Button();
                            newButton.Text = (intRange).ToString();
                            newButton.ID = "btn" + (intRange).ToString();
                            newButton.CssClass = "btnPagination";
                            newButton.Click += new EventHandler(PaginationButton_Click);
                            pnlPagination.Controls.Add(newButton);
                            vsbtnPagination_Count = (intRange).ToString();

                            if (intPageno == intRange)
                            {
                                newButton.CssClass = "clicked-button";
                            }

                            break;
                        }
                    }
                }

                Button btnNext = new Button();
                btnNext.Text = ">";
                btnNext.ID = "btnNext";
                btnNext.CssClass = "btnNext";
                btnNext.Click += new EventHandler(PaginationButton_Click);
                pnlPagination.Controls.Add(btnNext);

                if (intPageno == intRange)
                {
                    //btnNext.Style.Add("cursor", "default");
                    //btnNext.CssClass = "disableNextPrev";
                    btnNext.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "Pagination/CreatePaginationButtons");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
            return vsbtnPagination_Count;
        }

        public DataTable GetTblUsersCount(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string UserID, string Status, string strRoleID, string srcUser_ID, int intInput, int intRows)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            if (strRoleID == "Admin") { strRoleID = "1"; }
            else if (strRoleID == "Teacher") { strRoleID = "2"; }
            else if (strRoleID == "Student") { strRoleID = "3"; }

            if (Status == "Draft") { Status = "4"; }
            else if (Status == "Pending") { Status = "3"; }
            else if (Status == "Rejected") { Status = "2"; }
            else if (Status == "Approved") { Status = "1"; }

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_TBL_USERS_COUNT", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (UserID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USER_ID", Convert.ToInt32(UserID)); }

            if (Status == "Select") { cmd.Parameters.AddWithValue("@STATUS", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@STATUS", Convert.ToInt32(Status)); }

            if (strRoleID == "Select") { cmd.Parameters.AddWithValue("@ROLE_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@ROLE_ID", Convert.ToInt32(strRoleID)); }

            if (srcUser_ID == "") { cmd.Parameters.AddWithValue("@SRC_USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SRC_USER_ID", Convert.ToInt32(srcUser_ID)); }

            cmd.Parameters.AddWithValue("@INPUT", intInput);

            cmd.Parameters.AddWithValue("@ROWS", intRows);

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }

        public DataTable GetTblSubjectsCount(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string UserID, string SubjectID, string Status, string SubjectName, string SubjectTeacher, int intInput, int intRows)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            if (Status == "Draft") { Status = "4"; }
            else if (Status == "Pending") { Status = "3"; }
            else if (Status == "Rejected") { Status = "2"; }
            else if (Status == "Approved") { Status = "1"; }

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_TBL_SUBJECTS_COUNT", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (UserID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USER_ID", Convert.ToInt32(UserID)); }

            if (SubjectID == "") { cmd.Parameters.AddWithValue("@SUBJECT_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SUBJECT_ID", Convert.ToInt32(SubjectID)); }

            if (Status == "Select") { cmd.Parameters.AddWithValue("@STATUS", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@STATUS", Convert.ToInt32(Status)); }

            if (SubjectName == "Select") { cmd.Parameters.AddWithValue("@SUBJECT_NAME", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SUBJECT_NAME", SubjectName); }

            if (SubjectTeacher == "Select") { cmd.Parameters.AddWithValue("@SUBJECT_TEACHER", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SUBJECT_TEACHER", SubjectTeacher); }

            cmd.Parameters.AddWithValue("@INPUT", intInput);

            cmd.Parameters.AddWithValue("@ROWS", intRows);

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }

        public DataTable GetTblUsersSubjects(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string UserID, string Status, string strRoleID, string srcUser_ID, int intInput, int intRows)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            if (strRoleID == "Admin") { strRoleID = "1"; }
            else if (strRoleID == "Teacher") { strRoleID = "2"; }
            else if (strRoleID == "Student") { strRoleID = "3"; }

            if (Status == "Draft") { Status = "4"; }
            else if (Status == "Pending") { Status = "3"; }
            else if (Status == "Rejected") { Status = "2"; }
            else if (Status == "Approved") { Status = "1"; }

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_TBL_USERS_COUNT", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (UserID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USER_ID", Convert.ToInt32(UserID)); }

            if (Status == "Select") { cmd.Parameters.AddWithValue("@STATUS", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@STATUS", Convert.ToInt32(Status)); }

            if (strRoleID == "Select") { cmd.Parameters.AddWithValue("@ROLE_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@ROLE_ID", Convert.ToInt32(strRoleID)); }

            if (srcUser_ID == "") { cmd.Parameters.AddWithValue("@SRC_USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SRC_USER_ID", Convert.ToInt32(srcUser_ID)); }

            cmd.Parameters.AddWithValue("@INPUT", intInput);

            cmd.Parameters.AddWithValue("@ROWS", intRows);

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }
    }
}