<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageSubjects.aspx.cs" Inherits="M_C_Q.ManageSubjects" %>

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

        function restrictStringInput(input) {
            // Remove any non-numeric characters from the input value
            input.value = input.value.replace(/[^0-9]/g, '');

            if (input.value.length > 10) {
                input.value = input.value.slice(0, 10);
            }
        }
        function scrollToEnd() {
            window.scrollTo(0, document.body.scrollHeight);
        }

        function ToggleComments(input) {
            debugger;
            if (input == 'right') {
                $('.spnArrow-right').hide();
                $('.spnArrow-down').show();
                $('.txtComment').hide();
            }
            else {
                $('.spnArrow-right').show();
                $('.spnArrow-down').hide();
                $('.txtComment').show();
            }
        }


    </script>

    <div class="admin-menu-main">
        <div class="admin-menu-content">
            <header>
                <%--<h1>Add users</h1>--%>
                <%--<ul>
                    <li id="tabSubmit">Submit</li>
                    <li>View/Search</li>

                </ul>--%>

                <asp:Button runat="server" ID="tabSubmit" Text="Submit" CssClass="tab tabSubmit" OnClick="tabSubmit_Click"></asp:Button>
                <asp:Button runat="server" ID="tabViewSearch" Text="View/Search" CssClass="tab tabViewSearch" OnClick="tabViewSearch_Click"></asp:Button>
            </header>

            <%--submit--%>

            <asp:Panel runat="server" ID="pnlSubmit" CssClass="pnlSubmit">

                <div class="add-users heading col-md-12">
                    <div class="add-user-content" style="display: flex; justify-content: start;">
                        <div class="lbl">

                            <asp:Label runat="server" ID="remUserID" ForeColor="#808080" autocomplete="off" CssClass="Lato"></asp:Label>
                            <%--<asp:Label runat="server" ID="remUserID" ForeColor="#808080" Text="1" CssClass="Lato"></asp:Label>--%>
                        </div>
                    </div>

                    <div class="add-user-content" style="display: flex; justify-content: center;">
                        <div class="lbl">

                            <asp:Label runat="server" ID="remStatus" ForeColor="#808080" autocomplete="off" CssClass="Lato"></asp:Label>
                            <%--<asp:Label runat="server" ID="Label6" Text="Rejected" ForeColor="#808080" autocomplete="off" CssClass="Lato"></asp:Label>--%>
                        </div>
                    </div>


                    <div class="add-user-content" style="display: flex; justify-content: end;">
                        <div class="lbl">

                            <asp:Label runat="server" ID="remCreatedBy" ForeColor="#808080" autocomplete="off" CssClass="Lato"></asp:Label>
                            <%--<asp:Label runat="server" ID="Label8" Text="Admin1" ForeColor="#808080" autocomplete="off" CssClass="Lato"></asp:Label>--%>
                        </div>
                    </div>



                </div>

                <div class="add-users">
                    <div class="add-user-content">
                        <div class="lbl">
                            <asp:Label runat="server" ID="lblSubjectname" Text="Subject Name" autocomplete="off"></asp:Label>
                            <span class="mendatory-sign">*</span>
                        </div>
                        <div class="txt">
                            <asp:TextBox runat="server" ID="txtSubejctName" CssClass="txtSubejctName form-control"></asp:TextBox>
                        </div>
                        <div class="lbl">
                            <asp:Label runat="server" ID="warSubejctName" Text="Please Enter Subject Name." CssClass="lblWarning warSubejctName" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <div class="add-user-content">
                        <div class="lbl">
                            <asp:Label runat="server" ID="lblSubjectTeacher" Text="Subject Teacher"></asp:Label>
                            <span class="mendatory-sign">*</span>
                        </div>
                        <div class="txt">
                            <asp:DropDownList runat="server" ID="ddlSubjectTeacher" CssClass="ddlSubjectTeacher ddl-input form-control">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="lbl">
                            <asp:Label runat="server" ID="warSubjectTeacher" Text="Please Select Subject Teacher." CssClass="lblWarning warSubjectTeacher" Visible="false"></asp:Label>
                        </div>
                    </div>



                </div>

                <div class="add-users">

                    <div class="add-user-content">
                        <div class="lbl">
                            <asp:Label runat="server" ID="lblNoofquestions" Text="No. of Questions"></asp:Label>
                            <span class="mendatory-sign">*</span>
                        </div>
                        <div class="txt">
                            <asp:TextBox runat="server" ID="txtNoofquestions" CssClass="txtNoofquestions form-control"></asp:TextBox>
                        </div>
                        <div class="lbl">
                            <asp:Label runat="server" ID="warNoofQuestions" Text="Please Enter No. of Ques." CssClass="lblWarning warNoofQuestions" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <div class="add-user-content">
                        <div class="lbl">
                            <asp:Label runat="server" ID="lblTotalmarks" Text="Total Marks"></asp:Label>
                            <span class="mendatory-sign">*</span>
                        </div>
                        <div class="txt">
                            <asp:TextBox runat="server" ID="txtTotakmarks" CssClass="txtTotakmarks form-control"></asp:TextBox>
                        </div>
                        <div class="lbl">
                            <asp:Label runat="server" ID="warTotalMarks" Text="Please Enter Total Marks." CssClass="warTotalMarks lblWarning warTotalMarks" Visible="false"></asp:Label>
                        </div>
                    </div>


                </div>

                <div class="dvComment col-md-12" style="margin-bottom:20px;">

                    <div class="lbl">
                        <asp:Label runat="server" ID="lblSubjectDesc" Text="Subject Description"></asp:Label>
                        <span class="mendatory-sign">*</span>
                    </div>

                    <div class="txt">
                        <asp:TextBox runat="server" ID="txtSubjectDesc" TextMode="MultiLine" CssClass="txtSubjectDesc form-control txtComment" placeholder="Add Subject Description"></asp:TextBox>
                    </div>
                    <div class="lbl">
                        <asp:Label runat="server" ID="warSubjectDesc" Text="Please Enter Subject Description." CssClass="lblWarning warSubjectDesc" Visible="false"></asp:Label>
                    </div>

                </div>

                <div class="dvComment col-md-12">

                    <div class="lbl">
                        <asp:Label runat="server" ID="lblComment" Text="Comment"></asp:Label>
                    </div>

                    <div class="txt">
                        <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="form-control txtComment" placeholder="Add a Comment" Style="margin-bottom: 20px;"></asp:TextBox>
                    </div>

                </div>

                <asp:Panel runat="server" ID="pnlApproveReject" Visible="false">
                    <div class="add-user-refresh col-md-12">

                        <div class="lbl">
                            <asp:Label runat="server" ID="lblRejectionReason" Text="Rejection Reason"></asp:Label>
                            <%--<span class="mendatory-sign">*</span>--%>
                        </div>

                        <div class="txt">
                            <asp:TextBox runat="server" ID="txtRejectionReason" TextMode="MultiLine" CssClass="form-control txtRejectionReason" placeholder="Enter Rejection Reason"></asp:TextBox>
                        </div>

                    </div>
                    <div style="width: 100%; display: flex; justify-content: center;" class="add-users">



                        <div class="add-user-refresh">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnReject" Text="Reject" OnClick="btnReject_Click" CssClass="btn btnSaveDraft" />
                            </div>

                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnApprove" Text="Approve" OnClick="btnApprove_Click" CssClass="btn btnSubmit" />
                            </div>

                        </div>

                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlSaveSubmit">

                    <div style="width: 100%; display: flex; justify-content: center;" class="add-users">

                        <div class="add-user-refresh">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnSaveDraft" Text="Save Draft" OnClick="btnSaveDraft_Click" CssClass="btn btn-secondary btnSaveDraft" />
                            </div>

                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnAddusers" Text="Submit" OnClick="btnAddusers_Click" CssClass="btn btn-danger btnSubmit" />
                            </div>

                        </div>

                    </div>
                </asp:Panel>

                <div class="add-user-content">
                    <div class="lbl">
                        <asp:Label runat="server" ID="lblActionHistory" Text="Action History" CssClass="lblActionHistory" Visible="false"></asp:Label>
                    </div>
                </div>


            </asp:Panel>

            <%--view search--%>

            <asp:Panel runat="server" ID="pnlViewSearch" CssClass="pnlViewSearch">
                <div class="top-of-grid">
                    <div class="add-users col-md-12">

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblSrcSubjectID" Text="Subject ID"></asp:Label>
                            </div>
                            <div class="txt">
                                <asp:TextBox runat="server" ID="txtSrcSubjectID" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblSrcSubjectName" Text="Subject Name"></asp:Label>
                            </div>
                            <div class="txt">
                                <%--<asp:TextBox runat="server" ID="txtSrcSubjectName" CssClass="form-control"></asp:TextBox>--%>
                                <asp:DropDownList runat="server" CssClass="ddl-input form-control" ID="ddlsrcSubjectName">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblSrcSubjectTeacher" Text="Subject Teacher"></asp:Label>
                            </div>
                            <div class="txt">
                                <%--<asp:TextBox runat="server" ID="txtSrcSubjectName" CssClass="form-control"></asp:TextBox>--%>
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
                                <asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="btn btn-secondary btnSaveDraft" OnClick="btnClear_Click" Style="margin-top: 29px;" />
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
                            <%--<asp:label text="text" runat="server" /> runat="server" ID="lblSrcAll" Text="Search"></asp:Label>--%>
                            <%--&#x1F50D;--%>
                            <asp:TextBox runat="server" ID="txtSrcAll" CssClass="txtSrcAll form-control" placeholder="Search..." OnTextChanged="txtSrcAll_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lbl">

                        <asp:GridView runat="server" ID="gvManageUsers" CssClass="data-grid"
                            AutoGenerateColumns="False"
                            EmptyDataText="No records found">
                            <Columns>


                                <asp:TemplateField ControlStyle-CssClass="data-grid-header" HeaderText="Subject ID" ControlStyle-ForeColor="#69022f">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvlblUserID" runat="server" OnClick="gvlblUserID_Click" Text='<%# Eval("Subject_ID") %>' ToolTip="Click to view"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="data-grid-header" ForeColor="#69022F" />
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

                                <asp:TemplateField HeaderText="Subject Teacher">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblSubject_Teache" Text='<%# Eval("Subject_Teacher") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="No. of Questions">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblNo_of_Questions" Text='<%# Eval("No_of_Questions") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <%-- <asp:TemplateField HeaderText="Total Marks">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblTotal_Marks" Text='<%# Eval("Total_Marks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Created By">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="gvlblCreated_By" Text='<%# Eval("Created_By") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

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
                            <div>
                                <%-- <asp:LinkButton runat="server" ID="lnkDownloadXL" Text="Export to excel"></asp:LinkButton>--%>
                                <%--<asp:Label runat="server" ID="lblExport" Text="Export to excel"></asp:Label>--%>
                            </div>
                        </div>

                    </div>
                </asp:Panel>
            </asp:Panel>

        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdnUserID" />
</asp:Content>
