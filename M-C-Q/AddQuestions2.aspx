<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddQuestions2.aspx.cs" Inherits="M_C_Q.AddQuestions" EnableEventValidation="true" EnableViewState="true" %>

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
        select{
            background:transparent;
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
                                <asp:TextBox runat="server" ID="txtTotalMarks" CssClass="form-control" Enabled="false" ></asp:TextBox>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblNoOfQuestions" Text="No of Questions"></asp:Label>
                            </div>
                            <div class="txt">
                                    <asp:TextBox runat="server" ID="txtTotalNoOfQuestions" CssClass="form-control" Enabled="false" ></asp:TextBox>
                            </div>
                        </div>

                        <div class="add-user-content">
                            <div class="lbl">
                                <asp:Label runat="server" ID="Label2" Text="Marks per Question"></asp:Label>
                            </div>
                            <div class="txt">
                                    <asp:TextBox runat="server" ID="txtMarksPerQuestion" CssClass="form-control" Enabled="false" ></asp:TextBox>
                            </div>
                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnSearch" Text="Begin" CssClass="btn btnSubmit" OnClick="btnSearch_Click" Style="margin-top: 29px;" />

                            </div>

                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnEnd" Text="End" CssClass="btn btnSubmit" OnClick="btnSearch_Click" Style="margin-top: 29px;" />

                            </div>

                        </div>

                    </div>

                </div>

                <asp:Panel runat="server" ID="pnlNotify" CssClass="pnlNotify">
                    <%--<div class="center">
                        <asp:Label runat="server" ID="lblNotify" Text="Select Assigned Subject and Click Begin"></asp:Label>
                    </div>--%>
                    <div class="center">
                        <asp:Label runat="server" ID="lblNotify2" CssClass="lblNotify2" Text="Select Assigned Subject and Click Begin."></asp:Label>
                    </div>
                    <div class="center">
                        <img src="Img/select.png" alt="..." />
                    </div>
                </asp:Panel>
                
                <asp:panel runat="server" class="" ID="pnlQuestions" Visible="false">

                    <div style="display:flex; justify-content:space-between;">
                        <div class="lbl" style="margin-top: 15px;">
                        <asp:Label runat="server" ID="lblAddQuestions" Text="Add Question"></asp:Label>
                        <span class="mendatory-sign">*</span>
                    </div>

                    <div class="lbl" style="margin-top: 15px;">
                        <%--<asp:Label runat="server" Text="Showing" style="color:#9a9a9a;></asp:Label>--%>
                        <asp:Label runat="server" Text="Question no:" style="color: #000;"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlPageNo" CssClass="ddlPageNo" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged" AutoPostBack="true">
                            <%--<asp:ListItem>Select</asp:ListItem>--%>
                         </asp:DropDownList>
                        <asp:Label runat="server" ID="lblQuesNo" Text="1/50" style="color:#000;"></asp:Label>
                    </div>
                    </div>

                    <div class="txt">
                        <asp:TextBox runat="server" ID="txtQuestion" TextMode="MultiLine" CssClass="form-control txtQuestion" placeholder="Type your Question..."></asp:TextBox>
                    </div>

                    <div class="lbl" style="margin-top: 15px;">
                        <asp:Label runat="server" ID="Label1" Text="Add Options"></asp:Label>
                        <span class="mendatory-sign">*</span>
                    </div>
                </asp:panel>

                <div class="add-user-content">
                    <asp:Panel runat="server" ID="pnlAddAns" CssClass="pnlAddAns" Visible="false" >

                    </asp:Panel>
                </div>

                <asp:Panel runat="server" ID="pnlSaveSubmit" Visible="false">

                    <div style="width: 100%; display: flex; justify-content:center;" class="add-users">

                         <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnPrev" Text="< Save & Prev" CssClass="btn btnSubmit" OnClick="btnPrev_Click" />
                            </div>

                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-danger btnSubmit" visible="false" />
                            </div>

                        </div>

                        <div class="add-user-submit">

                            <div class="txt">
                                <asp:Button runat="server" ID="btnNext" Text="Save & Next >" CssClass="btn btn-danger btnSubmit" OnClick="btnNext_Click" />
                            </div>

                        </div>

                    </div>
                </asp:Panel>

            </asp:Panel>

        </div>
    </div>

</asp:Content>
