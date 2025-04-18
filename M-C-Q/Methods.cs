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
    public class Methods
    {
        public string strCon = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        public string strDefaultPass = ConfigurationManager.AppSettings["DefaultPass"];

        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        public String ModifyGrid(string Action, DataTable dt, DataSet ds, SqlDataAdapter adp, string strCon, int User_ID, string EmailID, string Username)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_MODIFY_GRIDVIEW", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GRID_NAME", "gvTeachers");
            cmd.Parameters.AddWithValue("@ACTION", Action);
            cmd.Parameters.AddWithValue("@USER_ID", User_ID);
            cmd.Parameters.AddWithValue("@EMAIL_ID", EmailID);
            cmd.Parameters.AddWithValue("@USERNAME", Username);
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt.Rows[0]["RESULT"].ToString();
        }

        public DataSet FilterGrid(string strCon, string UserID, string Status, string strRoleID, string srcUser_ID, int intInput, int intRows, string strSrcAll)
        {
            //DataSet ds = new DataSet();
            //SqlDataAdapter adp = new SqlDataAdapter();

            if (strRoleID == "Admin") { strRoleID = "1"; }
            else if (strRoleID == "Teacher") { strRoleID = "2"; }
            else if (strRoleID == "Student") { strRoleID = "3"; }

            if (Status == "Draft") { Status = "4"; }
            else if (Status == "Pending") { Status = "3"; }
            else if (Status == "Rejected") { Status = "2"; }
            else if (Status == "Approved") { Status = "1"; }

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_FILTER_GRIDVIEW", Con);
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

            cmd.Parameters.AddWithValue("@VALUE", strSrcAll);

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        public DataSet FilterGridQuestion(string strCon, string UserID, string srcQuestionID, string srcSubjectName, string srcSubjectTeacher, string srcStatus, int intInput, int intRows, string strSrcAll)
        {
            ds.Dispose(); adp.Dispose();

            //if (srcStatus == "Draft") { srcStatus = "4"; }
            //else if (srcStatus == "Pending") { srcStatus = "3"; }
            //else if (srcStatus == "Rejected") { srcStatus = "2"; }
            //else if (srcStatus == "Approved") { srcStatus = "1"; }

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_FILTER_GRIDVIEW_QUES", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (UserID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USER_ID", Convert.ToInt32(UserID)); }

            if (srcQuestionID == "") { cmd.Parameters.AddWithValue("@SRC_QUESTION_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SRC_QUESTION_ID", Convert.ToInt32(srcQuestionID)); }

            if (srcSubjectName == "Select") { cmd.Parameters.AddWithValue("@SRC_SUBJECT_NAME", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SRC_SUBJECT_NAME", srcSubjectName); }

            if (srcSubjectTeacher == "Select") { cmd.Parameters.AddWithValue("@SRC_SUBJECT_TEACHER", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SRC_SUBJECT_TEACHER", srcSubjectTeacher); }

            if (srcStatus == "Select") { cmd.Parameters.AddWithValue("@SRC_STATUS", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SRC_STATUS", srcStatus); }

            cmd.Parameters.AddWithValue("@INPUT", int.Parse(intInput.ToString()));

            cmd.Parameters.AddWithValue("@ROWS", int.Parse(intRows.ToString()));

            cmd.Parameters.AddWithValue("@VALUE", strSrcAll);

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        public string CheckCreatorOfQues(string SessionUserID, string SubjectName)
        {
            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_CHECK_CREATOR_OF_QUES", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (SessionUserID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USER_ID", Convert.ToInt32(SessionUserID)); }

            if (SubjectName == "") { cmd.Parameters.AddWithValue("@SUBJECT_NAME", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SUBJECT_NAME", SubjectName); }

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(ds);
            cmd.Dispose();

            return ds.Tables[0].Rows[0]["RESULT"].ToString();
        }

        public DataTable BindGridView(string strCon, string strGridViewName, int intInput, int intRows, DataTable dt, DataSet ds, SqlDataAdapter adp)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_BIND_GRIDVIEW", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GRID_VIEW_NAME", strGridViewName);
            cmd.Parameters.AddWithValue("@INPUT", intInput);
            cmd.Parameters.AddWithValue("@ROWS", intRows);
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }

        public DataTable Modify_gvTeachers(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp,
            string Action, int UserId, string Username, string EmailID, string Dob, String Gender, string City, string Phoneno)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_MODIFY_GVTEACHERS", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ACTION", Action);

            cmd.Parameters.AddWithValue("@USER_ID", UserId);

            if (Username == null) { cmd.Parameters.AddWithValue("@USERNAME", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USERNAME", Username); }

            if (EmailID == null) { cmd.Parameters.AddWithValue("@EMAIL_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@EMAIL_ID", EmailID); }

            if (Dob == null) { cmd.Parameters.AddWithValue("@DOB", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@DOB", Dob); }

            if (Gender == null) { cmd.Parameters.AddWithValue("@GENDER", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@GENDER", Gender); }

            if (City == null) { cmd.Parameters.AddWithValue("@CITY", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@CITY", City); }

            if (Phoneno == null) { cmd.Parameters.AddWithValue("@PHONE_NO", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@PHONE_NO", Phoneno); }

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }

        public DataSet fetchDropdown(string dropdownName)
        {

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_FETCH_DROPDOWN", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DROPDOWN_NAME", dropdownName);
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        public DataTable fetchTeachersDrpdowns(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_FETCH_TEACHERS_DRPDOWNS", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }

        public DataTable fetchAssignSubDrpdowns(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string strUserID, string strRoleID)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_FETCH_ASSIGNED_SUBJECTS", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@USER_ID", int.Parse(strUserID));
            cmd.Parameters.AddWithValue("@ROLE_ID", int.Parse(strRoleID));
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }

        public DataTable GetTotalMarksAndQues(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string strSubjectName)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_MARKS_AND_QUES", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SUBJECT_NAME", strSubjectName);
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }
        public DataTable AddQuestions(string strCon, string strUserID, string strSubjectName, string strQueston, string strAllOptions, string strCorrectOption, string strStatus, string strQuestionID, string strAction)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();

            if(strQuestionID == "")
            {
                strQuestionID = "0";
            }

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_ADD_QUESTIONS", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@USER_ID", int.Parse(strUserID));
            cmd.Parameters.AddWithValue("@SUBJECT_NAME", strSubjectName);
            cmd.Parameters.AddWithValue("@QUESTION", strQueston);
            cmd.Parameters.AddWithValue("@OPTIONS", strAllOptions);
            cmd.Parameters.AddWithValue("@CORRECT_OPTION", strCorrectOption);
            cmd.Parameters.AddWithValue("@STATUS_ID", strStatus);
            cmd.Parameters.AddWithValue("@QUESTION_ID", int.Parse(strQuestionID));
            cmd.Parameters.AddWithValue("@ACTION", strAction);
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            cmd.Dispose();
            return dt;
        }

        public DataTable GetFilledQuestions(string strCon, string strQuestionID, string strSubjectName)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_FILLED_QUESTIONS", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QUESTION_ID", int.Parse(strQuestionID));
            cmd.Parameters.AddWithValue("@SUBJECT_NAME", strSubjectName);
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            cmd.Dispose();
            return dt;
        }

        public DataSet GetQuestionDetails(string strCon, string strSubjectName)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_QUESTIONS_DETAILS", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SUBJECT_NAME", strSubjectName);
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        public DataTable GetRowsCount(string strCon, string strTableName, DataTable dt, DataSet ds, SqlDataAdapter adp)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_ROWS_COUNT", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TABLE_NAME", strTableName);
            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }

        public DataTable GetUserDetails(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string intUserID)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();


            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_USER_DETAILS", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (intUserID == null || intUserID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USER_ID", int.Parse(intUserID)); }

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }

        public DataTable GetSubjectDetails(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string intSubjectID)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();


            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_SUBJECT_DETAILS", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (intSubjectID == null || intSubjectID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SUBJECT_ID", int.Parse(intSubjectID)); }

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return dt;
        }
        public DataTable AddUsers(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string Action, string UserID, string Username, string EmailID, String Password, String DOB, String Gender, String City, String PhoneNO, String StatusID, String RoleID, String ModifiedBy, string comment, string RejectionReason)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();


            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_ADD_USERS", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (RoleID == "Admin") { RoleID = "1"; }
            else if (RoleID == "Teacher") { RoleID = "2"; }
            else if (RoleID == "Student") { RoleID = "3"; }

            cmd.Parameters.AddWithValue("@ACTION", Action);
            if (UserID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USER_ID", int.Parse(UserID)); }
            if (Username == " ") { cmd.Parameters.AddWithValue("@USERNAME  ", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USERNAME  ", Username); }
            if (EmailID == "") { cmd.Parameters.AddWithValue("@EMAIL_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@EMAIL_ID", EmailID); }
            if (Password == "") { cmd.Parameters.AddWithValue("@PASSWORD", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@PASSWORD", Password); }
            if (DOB == "") { cmd.Parameters.AddWithValue("@DOB", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@DOB", DOB); }
            if (Gender == "Select") { cmd.Parameters.AddWithValue("@GENDER", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@GENDER", Gender); }
            if (City == "Select") { cmd.Parameters.AddWithValue("@CITY", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@CITY", City); }
            if (PhoneNO == "") { cmd.Parameters.AddWithValue("@PHONE_NO", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@PHONE_NO", PhoneNO); }
            if (StatusID == "") { cmd.Parameters.AddWithValue("@STATUS_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@STATUS_ID", int.Parse(StatusID)); }
            if (RoleID == "Select") { cmd.Parameters.AddWithValue("@ROLE_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@ROLE_ID", int.Parse(RoleID)); }
            if (ModifiedBy == "") { cmd.Parameters.AddWithValue("@MODIFIED_BY", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@MODIFIED_BY", int.Parse(ModifiedBy)); }
            if (comment == "") { cmd.Parameters.AddWithValue("@COMMENT", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@COMMENT", comment); }
            if (RejectionReason == "") { cmd.Parameters.AddWithValue("@REJECTION_REASON", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@REJECTION_REASON", RejectionReason); }

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            //adp.Fill(ds);
            //cmd.Dispose();
            return dt;
        }

        public DataTable RemoveWhiteSpaces(DataTable dt)
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dt.Rows[i];

                bool isEmpty = true;

                foreach (var item in row.ItemArray)
                {
                    if (item != null && !string.IsNullOrWhiteSpace(item.ToString()))
                    {
                        isEmpty = false;
                        break;
                    }
                }

                if (isEmpty)
                {
                    dt.Rows.RemoveAt(i);
                }
            }

            return dt;
        }

        public DataTable AddSubjects(string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string Action, string UserID, string SubjectID, string SubjectName, String SubjectTeacher, String Noofquestions, String TotalMarks, String StatusID, String SubjectDesc, String Comment, string RejectionReason)
        {
            dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();


            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_ADD_SUBJECTS", Con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@ACTION", Action);
            if (UserID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USER_ID", int.Parse(UserID)); }
            if (SubjectID == "") { cmd.Parameters.AddWithValue("@SUBJECT_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SUBJECT_ID  ", int.Parse(SubjectID)); }
            if (SubjectName == "") { cmd.Parameters.AddWithValue("@SUBJECT_NAME", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SUBJECT_NAME", SubjectName); }
            if (SubjectTeacher == "") { cmd.Parameters.AddWithValue("@SUBJECT_TEACHER", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SUBJECT_TEACHER", SubjectTeacher); }
            if (Noofquestions == "") { cmd.Parameters.AddWithValue("@NO_OF_QUESTIONS", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@NO_OF_QUESTIONS", int.Parse(Noofquestions)); }
            if (TotalMarks == "") { cmd.Parameters.AddWithValue("@TOTAL_MARKS", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@TOTAL_MARKS", int.Parse(TotalMarks)); }
            if (StatusID == "") { cmd.Parameters.AddWithValue("@STATUS_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@STATUS_ID", int.Parse(StatusID)); }
            if (SubjectDesc == "") { cmd.Parameters.AddWithValue("@SUBJECT_DESCRIPTION", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SUBJECT_DESCRIPTION", SubjectDesc); }
            if (Comment == "") { cmd.Parameters.AddWithValue("@COMMENT", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@COMMENT", Comment); }
            if (RejectionReason == "") { cmd.Parameters.AddWithValue("@REJECTION_REASON", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@REJECTION_REASON", RejectionReason); }

            adp.SelectCommand = cmd;
            Con.Close();
            adp.Fill(dt);
            return dt;
        }

        public DataSet FilterGridSubjects(string strCon, string UserID, string srcSubject_ID, string srcSubject_Name, string srcSubject_Teacher, string srcStatus, int intInput, int intRows, string strSrcAll)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();

            if (srcStatus == "Draft") { srcStatus = "4"; }
            else if (srcStatus == "Pending") { srcStatus = "3"; }
            else if (srcStatus == "Rejected") { srcStatus = "2"; }
            else if (srcStatus == "Approved") { srcStatus = "1"; }

            SqlConnection Con = new SqlConnection(strCon);
            Con.Open();
            SqlCommand cmd = new SqlCommand("SP_FILTER_GRIDVIEW_SUBJECTS", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (UserID == "") { cmd.Parameters.AddWithValue("@USER_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@USER_ID", Convert.ToInt32(UserID)); }

            if (srcStatus == "Select") { cmd.Parameters.AddWithValue("@STATUS", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@STATUS", Convert.ToInt32(srcStatus)); }

            if (srcSubject_ID == "") { cmd.Parameters.AddWithValue("@SRC_SUBJECT_ID", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SRC_SUBJECT_ID", Convert.ToInt32(srcSubject_ID)); }

            if (srcSubject_Name == "Select") { cmd.Parameters.AddWithValue("@SRC_SUBJECT_NAME", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SRC_SUBJECT_NAME", srcSubject_Name); }

            if (srcSubject_Teacher == "Select") { cmd.Parameters.AddWithValue("@SRC_SUBJECT_TEACHER", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@SRC_SUBJECT_TEACHER", srcSubject_Teacher); }

            cmd.Parameters.AddWithValue("@INPUT", intInput);

            cmd.Parameters.AddWithValue("@ROWS", intRows);

            cmd.Parameters.AddWithValue("@VALUE", strSrcAll);

            adp.SelectCommand = cmd;
            Con.Close();
            //adp.Fill(dt);
            adp.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        public void CreatePaginationButtons(Panel pnlPagination, DropDownList ddlShowCount, string strCount, EventHandler PaginationButton_Click, string ViewState_Count, string strCon, DataTable dt, DataSet ds, SqlDataAdapter adp, string sessionUserID)
        {

            try
            {
                pnlPagination.Controls.Clear();

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

                        Button btnNext = new Button();
                        btnNext.Text = "Next";
                        btnNext.ID = "btnNext";
                        btnNext.CssClass = "btnNext";
                        btnNext.Click += new EventHandler(PaginationButton_Click);
                        pnlPagination.Controls.Add(btnNext);
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
                        ViewState_Count = btnID.ToString();
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

        public string GetRolesByID(string strID)
        {
            string strResult = string.Empty;

            switch (strID)
            {
                case "1":
                    strResult = "(Admin)";
                    break;

                case "2":
                    strResult = "(Teacher)";
                    break;

                case "3":
                    strResult = "(Student)";
                    break;
            }

            return strResult;
        }
    }
}