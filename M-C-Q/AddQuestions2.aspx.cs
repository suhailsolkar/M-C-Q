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
    public partial class AddQuestions : System.Web.UI.Page
    {
        Methods methods = new Methods();

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            tabSubmit.Style.Add("color", "#69022f");
            tabSubmit.Style.Add("background", "#fff");
            pnlSubmit.Visible = true;


            tabViewSearch.Style.Add("color", "grey");
            tabViewSearch.Style.Add("background", "none");
            //pnlViewSearch.Visible = false;

            if (!IsPostBack)
            {
                BindDropdowns();
            }

            if (ViewState["ROWS"] != null)
            {
                CreateNewRow(int.Parse(ViewState["ROWS"].ToString()));
            }
            else
            {
                CreateNewRow(1);
            }
            if (txtQuestion.Text == "")
            {
                txtQuestion.Focus();
            }
            if(ViewState["SELECTED_ID"] != null)
            {
                string tstID = ViewState["SELECTED_ID"].ToString();
                string tstText = ViewState["SELECTED_ITEM"].ToString();

                SetSelectedRow(tstID, tstText);
            }
            //btnSaveDraft.Text = "Save Draft ✔";
            //btnSubmit.Text = "Submit ✔";
        }

        #region functions
        private void SetSelectedRow(string strID, string strText)
        {
            int intRows = int.Parse(ViewState["ROWS"].ToString());

            if (strText == "Correct")
            {
                for (int j = 1; j <= intRows; j++)
                {
                    string ddlID = "ddlCorrect" + j;

                    DropDownList dropDownList = (DropDownList)pnlAddAns.FindControl(ddlID);

                    if (dropDownList != null)
                    {
                        if (dropDownList.SelectedItem.ToString() == "Correct")
                        {
                            dropDownList.SelectedValue = "Incorrect";
                        }
                    }
                }

                for (int j = 1; j <= intRows; j++)
                {
                    string txtID = "txtOption" + j;

                    TextBox txtOption = (TextBox)pnlAddAns.FindControl(txtID);

                    if (txtOption != null)
                    {
                        txtOption.CssClass = "txtOption form-control";
                    }
                }

                DropDownList ddl = (DropDownList)pnlAddAns.FindControl(strID);
                if (ddl != null)
                {
                    ddl.SelectedValue = "Correct";
                }

                char charLast = strID[strID.Length - 1];
                string strTextID = "txtOption" + charLast;
                TextBox txtBox = (TextBox)pnlAddAns.FindControl(strTextID);
                if (txtBox != null)
                {
                    txtBox.CssClass = "selectedRow form-control";
                    //txtBox.Focus();
                }

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
            }
            else
            {
                for (int j = 1; j <= intRows; j++)
                {
                    string txtID = "txtOption" + j;

                    TextBox txtOption = (TextBox)pnlAddAns.FindControl(txtID);

                    if (txtOption != null)
                    {
                        txtOption.CssClass = "txtOption form-control";
                    }
                }
            }
        }

        private void BindDropdowns()
        {
            try
            {
                methods.fetchAssignSubDrpdowns(methods.strCon, dt, ds, adp, Session["USER_ID"].ToString(), Session["ROLE_ID"].ToString());

                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                    ddlsrcSubjectName.Items.Add(dt.Rows[j]["DROPDOWN_VALUE"].ToString());
                }
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "Bind_AssingedTeachers");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }

        private void CreateNewRow(int intRows)
        {
            int intMaxRows = 8;
            string[] arValues = new string[intMaxRows];

            #region Storing added values
            if (txtQuestion.Text != "")
            {
                for (int j = 1; j <= intRows; j++)
                {
                    string txtID = "txtOption" + j;

                    TextBox tmptxtOption = (TextBox)pnlAddAns.FindControl(txtID);

                    if (tmptxtOption != null)
                    {
                        arValues[j - 1] = tmptxtOption.Text;
                    }
                }
            }
            #endregion

            #region Creating dynamic options
            pnlAddAns.Controls.Clear();
            int intID;

            for (int i = 1; i <= intRows; i++)
            {
                intID = i;

                Panel pnl = new Panel();
                pnl.ID = "pnlAddOptions" + intID;
                pnl.CssClass = "txt pnlAddOptions";
                //pnl.Style.Add("Border-bottom", "1.5px solid green");

                Label lblAddOptions = new Label();
                lblAddOptions.Text = intID.ToString();
                lblAddOptions.ID = "lblAddOptions" + intID;
                lblAddOptions.CssClass = "lblOption";
                pnl.Controls.Add(lblAddOptions);

                TextBox txt = new TextBox();
                txt.ID = "txtOption" + intID.ToString();
                txt.CssClass = "txtOption form-control";
                //txt.Style.Add("border", "1.5px solid green");
                txt.Text = "";
                pnl.Controls.Add(txt);


                DropDownList ddl = new DropDownList();
                ddl.Text = intID.ToString();
                ddl.ID = "ddlCorrect" + intID;
                ddl.CssClass = "ddlCorrect form-control-m";
                ddl.Items.Add("Incorrect");
                ddl.Items.Add("Correct");
                ddl.SelectedIndexChanged += new EventHandler(ddlCorrect_SelectedIndexChanged);
                ddl.AutoPostBack = true;
                pnl.Controls.Add(ddl);

                //if (i == 1)
                //{
                //    ddl.SelectedValue = "Correct";
                //}

                if (i == intRows)
                {
                    Button btn = new Button();
                    btn.Text = "+";
                    btn.ID = "btnAddOptions" + intID;
                    btn.CssClass = "btnAddOptions mx-2";
                    btn.Click += new EventHandler(btnAddOptions_Click);

                    if (intRows == intMaxRows)
                    {
                        btn.Enabled = false;
                        btn.CssClass = "disableAddOption mx-2";
                    }

                    pnl.Controls.Add(btn);
                }
                else
                {
                    Button btn = new Button();
                    btn.Text = "x";
                    btn.ID = "btnRemoveOption" + intID;
                    btn.CssClass = "btnRemoveOption mx-2";
                    btn.Click += new EventHandler(btnAddOptions_Click);
                    pnl.Controls.Add(btn);
                }

                pnlAddAns.Controls.Add(pnl);
                txt.Focus();

            }
            #endregion

            #region Assinging added values
            if (ViewState["OPTIONS"] != null)
            {
                arValues = ViewState["OPTIONS"].ToString().Split('/');
                ViewState["OPTIONS"] = null;
            }
            for (int j = 1; j <= intRows; j++)
            {
                string txtID = "txtOption" + j;

                TextBox tmptxtOption = (TextBox)pnlAddAns.FindControl(txtID);

                if (tmptxtOption != null)
                {
                    tmptxtOption.Text = arValues[j - 1];
                    if (ViewState["CORRECT_OPTION"] != null)
                    {
                        if (tmptxtOption.Text == ViewState["CORRECT_OPTION"].ToString())
                        {
                            string ddlID = "ddlCorrect" + j;

                            DropDownList tmpddlCorrect = (DropDownList)pnlAddAns.FindControl(ddlID);
                            tmpddlCorrect.SelectedValue = "Correct";

                            ViewState["SELECTED_ID"] = tmpddlCorrect.ID;
                            ViewState["SELECTED_ITEM"] = tmpddlCorrect.SelectedItem.ToString();

                            SetSelectedRow(ViewState["SELECTED_ID"].ToString(), ViewState["SELECTED_ITEM"].ToString());
                        }
                    }
                }
            }
            #endregion

            ViewState["ROWS"] = intRows;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
        }

        private void RemoveRow(int lastChar, int intRows)
        {
            int intMaxRows = 8;

            #region Storing added values
            string[] arValues = new string[intRows - 1];
            for (int j = 1; j <= intRows - 1; j++)
            {
                string txtID;
                if (j >= lastChar)
                {
                    txtID = "txtOption" + (j + 1);
                }
                else
                {
                    txtID = "txtOption" + j;
                }

                TextBox tmptxtOption = (TextBox)pnlAddAns.FindControl(txtID);

                if (tmptxtOption != null)
                {
                    if (tmptxtOption.Text != "")
                    {
                        arValues[j - 1] = tmptxtOption.Text;
                    }
                }
            }
            #endregion

            #region Creating dynamic options
            pnlAddAns.Controls.Clear();
            int intID;

            for (int i = 1; i <= intRows; i++)
            {
                intID = i;

                if (intID == lastChar)
                {
                    intID = intID - 1;
                }
                else
                {
                    if (intID > lastChar)
                    {
                        intID = intID - 1;
                    }

                    Panel pnl = new Panel();
                    pnl.ID = "pnlAddOptions" + intID;
                    pnl.CssClass = "txt pnlAddOptions";

                    Label lblAddOptions = new Label();
                    lblAddOptions.Text = intID.ToString();
                    lblAddOptions.ID = "lblAddOptions" + intID;
                    lblAddOptions.CssClass = "lblOption";
                    pnl.Controls.Add(lblAddOptions);

                    TextBox txt = new TextBox();
                    txt.ID = "txtOption" + intID.ToString();
                    txt.CssClass = "txtOption form-control";
                    pnl.Controls.Add(txt);

                    if (i == 1)
                    {
                        DropDownList ddl = new DropDownList();
                        ddl.Text = intID.ToString();
                        ddl.ID = "ddlCorrect" + intID;
                        ddl.CssClass = "ddlCorrect form-control-m";
                        ddl.Items.Add("Incorrect");
                        ddl.Items.Add("Correct");
                        ddl.SelectedIndexChanged += new EventHandler(ddlCorrect_SelectedIndexChanged);
                        ddl.AutoPostBack = true;
                        //ddl.Enabled = false;
                        pnl.Controls.Add(ddl);
                    }
                    else
                    {
                        DropDownList ddl = new DropDownList();
                        ddl.Text = intID.ToString();
                        ddl.ID = "ddlCorrect" + intID;
                        ddl.CssClass = "ddlCorrect form-control-m";
                        ddl.Items.Add("Incorrect");
                        ddl.Items.Add("Correct");
                        ddl.SelectedIndexChanged += new EventHandler(ddlCorrect_SelectedIndexChanged);
                        ddl.AutoPostBack = true;
                        //ddl.Enabled = false;
                        pnl.Controls.Add(ddl);
                    }

                    if (i == intRows)
                    {
                        Button btn = new Button();
                        btn.Text = "+";
                        btn.ID = "btnAddOptions" + intID;
                        btn.CssClass = "btnAddOptions mx-2";
                        btn.Click += new EventHandler(btnAddOptions_Click);

                        if (intRows - 1 == intMaxRows)
                        {
                            btn.Enabled = false;
                            btn.CssClass = "disableAddOption mx-2";
                        }

                        pnl.Controls.Add(btn);
                    }
                    else
                    {
                        Button btn = new Button();
                        btn.Text = "x";
                        btn.ID = "btnRemoveOption" + intID;
                        btn.CssClass = "btnRemoveOption mx-2";
                        btn.Click += new EventHandler(btnAddOptions_Click);
                        pnl.Controls.Add(btn);
                    }

                    pnlAddAns.Controls.Add(pnl);
                }
            }
            #endregion

            #region Assinging added values
            for (int j = 1; j <= intRows; j++)
            {
                string txtID = "txtOption" + j;

                TextBox tmptxtOption = (TextBox)pnlAddAns.FindControl(txtID);

                if (tmptxtOption != null)
                {
                    tmptxtOption.Text = arValues[j - 1];
                }
            }
            #endregion

            ViewState["ROWS"] = intRows - 1;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
        }

        public void GetSubjectDetails()
        {
            if (ddlsrcSubjectName.SelectedItem.ToString() != "Select")
            {
                ds = methods.GetQuestionDetails(methods.strCon, ddlsrcSubjectName.SelectedItem.ToString());

                if(ds.Tables[1].Rows[0]["RESULT"].ToString() == "COMPLETED")
                {
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                }

                dt = ds.Tables[0];

                int intNoOfQuestions = int.Parse(dt.Rows[0]["NO_OF_QUESTIONS"].ToString());
                int intCount = int.Parse(dt.Rows[0]["COUNT"].ToString());

                //ViewState["NO_OF_QUESTIONS"] = intNoOfQuestions;
                ViewState["FILLED_QUESTION"] = intCount;

                //for (int i = 1; i <= intCount + 1; i++)
                //{
                //    ddlPageNo.Items.Add(i.ToString());
                //}

                if (ViewState["CURRENT_QUESTION"] == null)
                {
                    ViewState["CURRENT_QUESTION"] = intCount + 1;
                }

                if (intNoOfQuestions > intCount)
                {
                    //lblQuesNo.Text = ("" + (intCount + 1) + "") + "/" + ("" + intNoOfQuestions + "");
                    lblQuesNo.Text = "" + ("" + intNoOfQuestions + "");

                    string txtID;
                    txtID = "txtOption" + 1;
                    TextBox tmptxtOption = (TextBox)pnlAddAns.FindControl(txtID);

                    if (tmptxtOption != null)
                    {
                        if (tmptxtOption.Text != "")
                        {
                            tmptxtOption.Text = "";
                        }
                    }

                    txtQuestion.Text = "";
                    CreateNewRow(1);
                }
                else
                {
                    //btnNext.Enabled = false;
                    //txtQuestion.Enabled = false;

                    int intRows = int.Parse(ViewState["ROWS"].ToString());
                    string[] arValues = new string[intRows - 1];
                    for (int j = 1; j <= intRows; j++)
                    {
                        string txtID; string btnID; string ddlID;

                        txtID = "txtOption" + j;
                        TextBox tmptxtOption = (TextBox)pnlAddAns.FindControl(txtID);
                        if (tmptxtOption != null)
                        {
                            tmptxtOption.Enabled = false;
                        }

                        btnID = "btnRemoveOption" + j;
                        Button btnRemove = (Button)pnlAddAns.FindControl(btnID);
                        if (btnRemove != null)
                        {
                            btnRemove.Enabled = false;
                            btnRemove.CssClass = "disableAddOption mx-2";
                        }

                        btnID = "btnAddOptions" + j;
                        Button btnAdd = (Button)pnlAddAns.FindControl(btnID);
                        if (btnAdd != null)
                        {
                            btnAdd.Enabled = false;
                            btnAdd.CssClass = "disableAddOption mx-2";
                        }

                        ddlID = "ddlCorrect" + j;
                        DropDownList ddlCorrect = (DropDownList)pnlAddAns.FindControl(ddlID);
                        if (ddlCorrect != null)
                        {
                            ddlCorrect.Enabled = false;
                            ddlCorrect.CssClass = "disableddlCorrect";
                        }
                    }
                }
            }
        }

        private void SaveQuestion(string Type)
        {
            if (Type == "Next" || Type == "Prev")
            {
                string[] arrAllOptions = new string[8];
                string strCorrectOption = string.Empty;
                string strAllOptions = string.Empty;
                for (int i = 1; i <= int.Parse(ViewState["ROWS"].ToString()); i++)
                {
                    string ddlID = "ddlCorrect" + i;
                    string txtID = "txtOption" + i;
                    DropDownList ddlCorrect = (DropDownList)pnlAddAns.FindControl(ddlID);
                    TextBox txtOptions = (TextBox)pnlAddAns.FindControl(txtID);
                    if (ddlCorrect.SelectedItem.ToString() == "Correct")
                    {
                        if (strAllOptions == string.Empty)
                        {
                            strAllOptions = txtOptions.Text;
                        }
                        else
                        {
                            strAllOptions = strAllOptions + '/' + txtOptions.Text;
                        }
                        strCorrectOption = txtOptions.Text;
                    }
                    else
                    {
                        if (strAllOptions == string.Empty)
                        {
                            strAllOptions = txtOptions.Text;
                        }
                        else
                        {
                            strAllOptions = strAllOptions + '/' + txtOptions.Text;
                        }
                    }
                }

                int intQuesID = int.Parse(ViewState["CURRENT_QUESTION"].ToString());

                dt = methods.AddQuestions(methods.strCon, Session["USER_ID"].ToString(), ddlsrcSubjectName.SelectedItem.ToString(), txtQuestion.Text, strAllOptions, strCorrectOption, "4", intQuesID.ToString(), Type);
            }
        }

        private void FetchQuestion(string strCurrentQues)
        {
            dt = methods.GetFilledQuestions(methods.strCon, strCurrentQues, ddlsrcSubjectName.SelectedItem.ToString());

            if (dt.Rows.Count > 0)
            {
                ViewState["OPTIONS"] = dt.Rows[0]["OPTIONS"].ToString();
                ViewState["CORRECT_OPTION"] = dt.Rows[0]["CORRECT_OPTION"].ToString();

                txtQuestion.Text = dt.Rows[0]["QUESTION"].ToString();
                string[] splitOptions = dt.Rows[0]["OPTIONS"].ToString().Split('/');
                CreateNewRow(splitOptions.Length);
            }
            else
            {
                txtQuestion.Text = "";
                CreateNewRow(1);
            }

            ds = methods.GetQuestionDetails(methods.strCon, ddlsrcSubjectName.SelectedItem.ToString());

            dt = ds.Tables[0];

            int intNoOfQuestions = int.Parse(dt.Rows[0]["NO_OF_QUESTIONS"].ToString());
            int intFilledQues = int.Parse(dt.Rows[0]["NO_OF_QUESTIONS"].ToString());
            int intCount = int.Parse(strCurrentQues);

            //if (intNoOfQuestions == intFilledQues)
            //{
            //    btnSubmit.Visible = true;
            //}

            //lblQuesNo.Text = ("" + (intCount) + "") + "/" + ("" + intNoOfQuestions + "");
            lblQuesNo.Text = "/" + ("" + intNoOfQuestions + "");

            btnPrev.Enabled = true;


        }

        private void ManageButtons()
        {
            ds = methods.GetQuestionDetails(methods.strCon, ddlsrcSubjectName.SelectedItem.ToString());

            dt = ds.Tables[0];

            int intNoOfQuestions = int.Parse(dt.Rows[0]["NO_OF_QUESTIONS"].ToString());
            int intFilledQues = int.Parse(dt.Rows[0]["COUNT"].ToString());
            //if (intNoOfQuestions == int.Parse(ViewState["CURRENT_QUESTION"].ToString()))
            //{
            //    btnSubmit.Visible = true;
            //}
            //else
            //{
            //    btnSubmit.Visible = false;
            //}

            if (int.Parse(ViewState["CURRENT_QUESTION"].ToString()) == 1)
            {
                //btnPrev.Enabled = false;
                btnPrev.Text = "Save";
            }
            else if (int.Parse(ViewState["CURRENT_QUESTION"].ToString()) == intNoOfQuestions)
            {
                //btnNext.Enabled = false;
                btnNext.Text = "Save";
            }
            else
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
            }
        }

        private void SwitchPages()
        {
            //Get Details for Questions
            ds = methods.GetQuestionDetails(methods.strCon, ddlsrcSubjectName.SelectedItem.ToString());

            if (ds.Tables[1].Rows[0]["RESULT"].ToString() == "COMPLETED")
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }

            dt = ds.Tables[0];

            ViewState["FILLED_QUESTION"] = int.Parse(dt.Rows[0]["COUNT"].ToString());

            int intNoOfQuestions = int.Parse(dt.Rows[0]["NO_OF_QUESTIONS"].ToString());

            if (int.Parse(ViewState["CURRENT_QUESTION"].ToString()) == 1)
            {
                btnPrev.Text = "Save";
                btnNext.Text = "Save & Next >";
            }
            else if (int.Parse(ViewState["CURRENT_QUESTION"].ToString()) == intNoOfQuestions)
            {
                btnNext.Text = "Save";
                btnPrev.Text = "< Save & Prev";
            }
            else
            {
                btnPrev.Text = "< Save & Prev";
                btnNext.Text = "Save & Next >";
            }

            //Switch Pages
            ddlPageNo.Items.Clear();
            int intFilledQues = int.Parse(ViewState["FILLED_QUESTION"].ToString());
            int intTotalNoOfQues = int.Parse(txtTotalNoOfQuestions.Text);
            int intRange = 5;
            int intPageNo = int.Parse(ViewState["CURRENT_QUESTION"].ToString());

            for (int i = intPageNo - intRange; i <= intPageNo + intRange; i++)
            {
                if (i <= intFilledQues + 1 && i <= intTotalNoOfQues && i >= 1){
                    ddlPageNo.Items.Add(i.ToString());
                }
            }

            if(intPageNo > 0)
            {
                ddlPageNo.SelectedValue = ViewState["CURRENT_QUESTION"].ToString();
            }
        }
        #endregion

        #region Events
        protected void btnAddOptions_Click(object sender, EventArgs e)
        {
            int intRows;

            Button clickedButton = (Button)sender;

            string tstID = clickedButton.ID;
            string tstText = clickedButton.Text;

            if (clickedButton.Text == "+")
            {
                int lastChar = int.Parse(tstID[tstID.Length - 1].ToString());

                string txtID = "txtOption" + lastChar;

                TextBox tmptxtOption = (TextBox)pnlAddAns.FindControl(txtID);

                if (txtQuestion.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please fill question.')", true);
                    txtQuestion.Focus();
                }
                else if (tmptxtOption.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please fill Option " + lastChar + ".')", true);
                }
                else
                {
                    if (ViewState["ROWS"] != null)
                    {
                        ViewState["ROWS"] = int.Parse(ViewState["ROWS"].ToString()) + 1;
                    }
                    else
                    {
                        ViewState["ROWS"] = 1;
                    }
                    intRows = int.Parse(ViewState["ROWS"].ToString());
                    CreateNewRow(intRows);
                }
            }
            else
            {
                intRows = int.Parse(ViewState["ROWS"].ToString());
                int lastChar = int.Parse(tstID[tstID.Length - 1].ToString());
                RemoveRow(lastChar, intRows);
            }

            if (ViewState["SELECTED_ID"] != null)
            {
                string strID = ViewState["SELECTED_ID"].ToString();
                string strText = ViewState["SELECTED_ITEM"].ToString();

                SetSelectedRow(strID, strText);
            }
        }

        protected void ddlCorrect_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            ViewState["SELECTED_ID"] = ddl.ID;
            ViewState["SELECTED_ITEM"] = ddl.SelectedItem.ToString();

            string tstID = ViewState["SELECTED_ID"].ToString();
            string tstText = ViewState["SELECTED_ITEM"].ToString();

            SetSelectedRow(tstID, tstText);
        }

        protected void ddlsrcSubjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsrcSubjectName.SelectedItem.ToString() == "Select")
                {
                    txtTotalNoOfQuestions.Text = "";
                    txtTotalMarks.Text = "";
                    txtMarksPerQuestion.Text = "";
                }
                else
                {
                    methods.GetTotalMarksAndQues(methods.strCon, dt, ds, adp, ddlsrcSubjectName.SelectedItem.ToString());

                    txtTotalMarks.Text = dt.Rows[0]["TOTAL_MARKS"].ToString();
                    txtTotalNoOfQuestions.Text = dt.Rows[0]["NO_OF_QUESTIONS"].ToString();

                    dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();

                    string strTotalMarks = txtTotalMarks.Text;
                    string strTotalNoOfQuestions = txtTotalNoOfQuestions.Text;

                    txtMarksPerQuestion.Text = ((int.Parse(strTotalMarks) / int.Parse(strTotalNoOfQuestions)).ToString());
                }

            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "ddlsrcSubjectName_SelectedIndexChanged");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnPrev.Text = "< Save & Prev";
                btnNext.Text = "Save & Next >";
                ViewState["CURRENT_QUESTION"] = ddlPageNo.SelectedItem.ToString();
                string strCurrentQues = ViewState["CURRENT_QUESTION"].ToString();
                FetchQuestion(strCurrentQues);//fetch Added questions
                SwitchPages();
                ddlPageNo.Focus();
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(methods.strCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERT_ERROR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERROR_NAME", "ddlPageNo_SelectedIndexChanged");
                cmd.Parameters.AddWithValue("@ERROR_MSG", ex.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            finally
            {
                dt.Clear(); dt.Dispose(); ds.Dispose(); adp.Dispose();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pnlQuestions.Visible = true;
            pnlAddAns.Visible = true;
            ddlsrcSubjectName.Enabled = false;
            pnlSaveSubmit.Visible = true;
            pnlNotify.Visible = false;

            GetSubjectDetails();

            ViewState["CURRENT_QUESTION"] = 1;

            //ddlPageNo.SelectedValue = ViewState["CURRENT_QUESTION"].ToString();

            string strCurrentQues = ViewState["CURRENT_QUESTION"].ToString();
            FetchQuestion(strCurrentQues);//fetch Added questions

            SwitchPages();

            //if (ViewState["ROWS"] != null)
            //{
            //    CreateNewRow(int.Parse(ViewState["ROWS"].ToString()));
            //}
            //else
            //{
            //    CreateNewRow(1);
            //}
            //if (txtQuestion.Text == "")
            //{
            //    txtQuestion.Focus();
            //}
            //GetSubjectDetails();
            //ManageButtons();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            bool isVlid = true;
            bool isCorrectOptSelected = false;

            for (int i = 1; i <= int.Parse(ViewState["ROWS"].ToString()); i++)
            {
                string ddlID = "ddlCorrect" + i;

                DropDownList ddlCorrect = (DropDownList)pnlAddAns.FindControl(ddlID);
                if (ddlCorrect != null)
                {
                    if (ddlCorrect.SelectedItem.ToString() == "Correct")
                    {
                        isCorrectOptSelected = true;
                    }
                }

                string txtID = "txtOption" + i;
                TextBox txtOptions = (TextBox)pnlAddAns.FindControl(txtID);
                if (txtOptions != null)
                {
                    if (txtOptions.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Option no " + i + " cannot be empty.')", true);
                        isVlid = false;
                        break;
                    }
                }
                else
                {
                    isVlid = false;
                    break;
                }
            }

            if (int.Parse(ViewState["ROWS"].ToString()) < 2)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Add atleast 2 options.')", true);
            }
            else if (!isCorrectOptSelected)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select atleast one correct option.')", true);
            }
            else if (isVlid)
            {
                if (btnNext.Text != "Save")
                {
                    SaveQuestion("Next");//Add or upadte questions

                    ViewState["OPTION"] = null; ViewState["CORRECT_OPTION"] = null;
                    ViewState["SELECTED_ID"] = null; ViewState["SELECTED_ITEM"] = null;

                    ViewState["CURRENT_QUESTION"] = int.Parse(ViewState["CURRENT_QUESTION"].ToString()) + 1;
                    string strCurrentQues = ViewState["CURRENT_QUESTION"].ToString();

                    FetchQuestion(strCurrentQues);//fetch Added questions

                    //ManageButtons();

                    btnPrev.Text = "< Save & Prev";

                    SwitchPages();
                }
                else
                {
                    SaveQuestion("Next");//Add or upadte questions
                    string strCurrentQues = ViewState["CURRENT_QUESTION"].ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Question no : " + strCurrentQues + " saved successfully.')", true);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);
                
            }

            //int intCount = int.Parse(txtTotalNoOfQuestions.Text);
            //int intddlCount = int.Parse(ddlPageNo.SelectedItem.ToString());
            //int intRange = int.Parse(ViewState["FILLED_QUESTION"].ToString());

            //int intTest = 0;

            //ddlPageNo.Items.Clear();

            //if (intddlCount % 5 == 0)
            //{
            //    for (int i = intddlCount; i <= intRange + 1; i++)
            //    {
            //        //ddlPageNo.Items.Add(i.ToString());
            //        if (i <= intCount)
            //        {
            //            ddlPageNo.Items.Add(i.ToString());
            //        }
            //    }
            //}
            //else
            //{
            //    int j = 1;
            //    for (int i = intddlCount; i <= intddlCount + 1; i++)
            //    {
            //        if (i <= 5 * j)
            //        {
            //            intTest = 5 * j;
            //            break;
            //        }
            //        else
            //        {
            //            j++;
            //        }
            //    }

            //    for (int i = (intTest - 5); i <= intTest + 1; i++)
            //    {
            //        if(i != 0 && i <= intRange + 1)
            //        {
            //            if(i <= intCount)
            //            {
            //                ddlPageNo.Items.Add(i.ToString());
            //            }
            //        }
            //    }
            //}

            //lblAddQuestions.Text = intTest.ToString();

            //ddlPageNo.SelectedValue = ViewState["CURRENT_QUESTION"].ToString();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            bool isVlid = true;
            bool isCorrectOptSelected = false;

            for (int i = 1; i <= int.Parse(ViewState["ROWS"].ToString()); i++)
            {
                string ddlID = "ddlCorrect" + i;

                DropDownList ddlCorrect = (DropDownList)pnlAddAns.FindControl(ddlID);
                if (ddlCorrect != null)
                {
                    if (ddlCorrect.SelectedItem.ToString() == "Correct")
                    {
                        isCorrectOptSelected = true;
                    }
                }

                string txtID = "txtOption" + i;
                TextBox txtOptions = (TextBox)pnlAddAns.FindControl(txtID);
                if (txtOptions != null)
                {
                    if (txtOptions.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Option no " + i + " cannot be empty.')", true);
                        isVlid = false;
                        break;
                    }
                }
                else
                {
                    isVlid = false;
                    break;
                }
            }

            if (int.Parse(ViewState["ROWS"].ToString()) < 2)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Add atleast 2 options.')", true);
            }
            else if (!isCorrectOptSelected)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select atleast one correct option.')", true);
            }
            else if (isVlid)
            {
                if (btnPrev.Text != "Save")
                {
                    SaveQuestion("Prev");//Add or upadte questions

                    ViewState["OPTION"] = null; ViewState["CORRECT_OPTION"] = null;
                    ViewState["SELECTED_ID"] = null; ViewState["SELECTED_ITEM"] = null;

                    ViewState["CURRENT_QUESTION"] = int.Parse(ViewState["CURRENT_QUESTION"].ToString()) - 1;
                    string strCurrentQues = ViewState["CURRENT_QUESTION"].ToString();

                    FetchQuestion(strCurrentQues);//fetch Added questions

                    //ManageButtons();

                    btnNext.Text = "Save & Next >";

                    SwitchPages();
                }
                else
                {
                    SaveQuestion("Prev");//Add or upadte questions
                    string strCurrentQues = ViewState["CURRENT_QUESTION"].ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Question no : " + strCurrentQues + " saved successfully.')", true);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollScript", "scrollToEnd();", true);

                SwitchPages();

            }
        }

        protected void tabSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void tabViewSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}