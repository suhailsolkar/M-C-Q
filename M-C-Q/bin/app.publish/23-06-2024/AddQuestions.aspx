<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddQuestions.aspx.cs" Inherits="M_C_Q.AddQuestions1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <script src="Script/Index.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        window.onload = function () {
            var inputFields = document.getElementsByTagName('input');
            for (var i = 0; i < inputFields.length; i++) {
                inputFields[i].setAttribute('autocomplete', 'off');
            }
        }

        function scrollToEnd() {
            $('html, body').animate({ scrollTop: $(document).height() }, 'fast');
        }
    </script>

    <style>
        select {
            background: transparent;
        }

        .add-users-ques {
            display: flex;
            flex-direction: row;
            justify-content: center;
            gap: 15px;
            font-family: 'Lato', sans-serif;
            /*margin: 20px 0;*/
            /*padding-bottom: 10px;
            border-bottom:1.5px solid #ddd;*/
        }
    </style>

    <div class="admin-menu-main">
        <div class="admin-menu-content">

            <header>
                <asp:Button runat="server" ID="tabSubmit" Text="Submit" CssClass="tab tabSubmit" OnClick="tabSubmit_Click"></asp:Button>
                <asp:Button runat="server" ID="tabViewSearch" Text="View/Search" CssClass="tab tabViewSearch" OnClick="tabViewSearch_Click"></asp:Button>
            </header>

            <asp:Panel runat="server" ID="pnlSubmit" CssClass="pnlSubmit">

                <div class="top-of-grid">
                    <div class="add-users col-md-12">

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblSrcSubjectName" Text="Assigned Subjects"></asp:Label>
                                <span class="mendatory-sign">*</span>
                            </div>
                            <div class="txt">
                                <asp:DropDownList runat="server" CssClass="ddl-input form-control" ID="ddlsrcSubjectName" OnSelectedIndexChanged="ddlsrcSubjectName_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblTotalMarks" Text="Total Marks"></asp:Label>
                            </div>
                            <div class="txt">
                                <asp:TextBox runat="server" ID="txtTotalMarks" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblNoOfQuestions" Text="No of Questions"></asp:Label>
                            </div>
                            <div class="txt">
                                <asp:TextBox runat="server" ID="txtTotalNoOfQuestions" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="Label2" Text="Marks per Question"></asp:Label>
                            </div>
                            <div class="txt">
                                <asp:TextBox runat="server" ID="txtMarksPerQuestion" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnBegin" Text="Begin" CssClass="btn btnSubmit" OnClick="btnBegin_Click" Style="margin-top: 29px;" />

                            </div>

                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnEnd" Text="End" CssClass="btn btnSubmit" OnClick="btnEnd_Click" Style="margin-top: 29px;" />

                            </div>

                        </div>

                    </div>

                </div>

                <asp:Panel runat="server" ID="pnlNotify" CssClass="pnlNotify">
                    <div class="center">
                        <asp:Label runat="server" ID="lblNotify2" CssClass="lblNotify2" Text="Select Assigned Subject and Click Begin."></asp:Label>
                    </div>
                    <div class="center">
                        <img src="Img/select.png" alt="..." />
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" class="" ID="pnlQuestions" Visible="false">

                    <asp:Panel class="add-users-ques heading col-md-12" runat="server" ID="dvReminder" Visible="false">
                        <div class="add-user-content" style="display: flex; justify-content: start;">
                            <div class="lbl">
                                <asp:Label runat="server" ID="remSubjectID" ForeColor="#808080" autocomplete="off" CssClass="Lato"></asp:Label>
                            </div>
                        </div>

                        <div class="add-user-content" style="display: flex; justify-content: center;">
                            <div class="lbl">
                                <asp:Label runat="server" ID="remStatus" ForeColor="#808080" autocomplete="off" CssClass="Lato"></asp:Label>
                            </div>
                        </div>


                        <div class="add-user-content" style="display: flex; justify-content: end;">
                            <div class="lbl">
                                <asp:Label runat="server" ID="remCreatedBy" ForeColor="#808080" autocomplete="off" CssClass="Lato"></asp:Label>
                            </div>
                        </div>

                    </asp:Panel>

                    <div style="display: flex; justify-content: space-between;">
                        <div class="lbl" style="margin-top: 15px;">
                            <asp:Label runat="server" ID="lblAddQuestions" Text="Add Question"></asp:Label>
                            <span class="mendatory-sign">*</span>
                        </div>

                        <div class="lbl" style="margin-top: 15px;">
                            <%--<asp:Label runat="server" Text="Showing" style="color:#9a9a9a;></asp:Label>--%>
                            <asp:Label runat="server" Text="Question no:" Style="color: #000;"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlPageNo" CssClass="ddlPageNo" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged" AutoPostBack="true">
                                <%--<asp:ListItem Selected="True">1</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:Label runat="server" ID="lblQuesNo" Text="1/50" Style="color: #000;"></asp:Label>
                        </div>
                    </div>

                    <div class="txt">
                        <asp:TextBox runat="server" ID="txtQuestion" TextMode="MultiLine" CssClass="form-control txtQuestion" placeholder="Type your Question..."></asp:TextBox>
                    </div>

                    <div class="lbl" style="margin-top: 15px;">
                        <asp:Label runat="server" ID="Label1" Text="Add Options"></asp:Label>
                        <span class="mendatory-sign">*</span>
                    </div>
                </asp:Panel>

                <div class="add-user-content" style="margin-bottom: 20px;">
                    <asp:Panel runat="server" ID="pnlAddAns" CssClass="pnlAddAns" Visible="false">
                    </asp:Panel>
                </div>

                <asp:Panel runat="server" ID="pnlSaveSubmit" Visible="false">

                    <div style="width: 100%; display: flex; justify-content: center;" class="add-users">

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnSaveAndPrev" Text="< Save & Prev" CssClass="btn btnSubmit" OnClick="btnSaveAndPrev_Click" />
                            </div>

                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-danger btnSubmit" Visible="false" />
                            </div>

                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnSaveAndNext" Text="Save & Next >" CssClass="btn btn-danger btnSubmit" OnClick="btnSaveAndNext_Click" />
                            </div>

                        </div>

                    </div>
                </asp:Panel>

            </asp:Panel>

            <asp:Panel runat="server" ID="pnlViewSearch" CssClass="pnlViewSearch">
                <div class="top-of-grid">
                    <div class="add-users col-md-12">

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblSrcQuestionID" Text="Question ID"></asp:Label>
                            </div>
                            <div class="txt">
                                <asp:TextBox runat="server" ID="txtSrcQuestionID" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="Label3" Text="Subject Name"></asp:Label>
                            </div>
                            <div class="txt">
                                <asp:DropDownList runat="server" CssClass="ddl-input form-control" ID="ddlSrcViewSubjectName">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblSrcSubjectTeacher" Text="Subject Teacher"></asp:Label>
                            </div>
                            <div class="txt">
                                <asp:DropDownList runat="server" CssClass="ddl-input form-control" ID="ddlsrcSubjectTeacher">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblSearchStatus" Text="Status"></asp:Label>
                            </div>
                            <div class="txt">
                                <asp:DropDownList runat="server" CssClass="ddl-input form-control" ID="ddlsrcStatus">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-danger btnSubmit" OnClick="btnSearch_Click" Style="margin-top: 29px;" />

                            </div>

                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="btn btnSaveDraft" OnClick="btnClear_Click" Style="margin-top: 29px;" />
                            </div>

                        </div>

                    </div>

                </div>

                <asp:Panel runat="server" ID="pnlGridviewview" class="add-user-grid">
                    <div style="display: flex; justify-content: space-between; align-items: center; color: #69022f;">
                        <div>
                            <asp:Label runat="server" ID="lblSohowCount1" Text="Showing " CssClass="lblSohowCount"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlShowCount" CssClass="ddl-count" OnSelectedIndexChanged="ddlShowCount_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="10" Value="1"></asp:ListItem>
                                <asp:ListItem Text="15" Value="2"></asp:ListItem>
                                <asp:ListItem Text="20" Value="3"></asp:ListItem>
                                <asp:ListItem Text="25" Value="4"></asp:ListItem>
                                <asp:ListItem Text="30" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label runat="server" ID="lblCount" Text=""></asp:Label>
                        </div>

                        <div>
                            <asp:TextBox runat="server" ID="txtSrcAll" CssClass="txtSrcAll form-control" placeholder="Search..." OnTextChanged="txtSrcAll_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lbl">

                        <asp:GridView runat="server" ID="gvQuestions" CssClass="data-grid"
                            AutoGenerateColumns="False"
                            EmptyDataText="No records found">
                            <Columns>

                                <%-- <asp:TemplateField ControlStyle-CssClass="data-grid-header" HeaderText="Question ID" ControlStyle-ForeColor="#69022f">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvQuestionID" runat="server" OnClick="gvQuestionID_Click" Text='<%# Eval("Question_ID") %>' ToolTip="Click to view"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="data-grid-header" ForeColor="#69022F" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Subject ID">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvSubjectID" runat="server" OnClick="gvSubjectID_Click" Text='<%# Eval("Subject_ID") %>' ToolTip="Click to view"></asp:LinkButton>
                                        <controlstyle cssclass="data-grid-header" forecolor="#69022F" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblStatus" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Subject Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblSubject_Name" Text='<%# Eval("Subject_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Marks">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblTotalMarks" Text='<%# Eval("Total_Marks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="No. of Question">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblNoOfQuestions" Text='<%# Eval("No_Of_Questions") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Subject Teacher">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblSubject_Teache" Text='<%# Eval("Subject_Teacher") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Created By">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblCreatedBy" Text='<%# Eval("Created_By") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Created Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvglblCreated_DT" Text='<%# Eval("Created_DT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                        <div style="display: flex; justify-content: space-between;">
                            <asp:Panel runat="server" ID="pnlPagination">
                            </asp:Panel>
                        </div>

                    </div>
                </asp:Panel>
            </asp:Panel>

        </div>
    </div>
</asp:Content>
