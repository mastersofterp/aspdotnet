<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Itle_FrequentlyAskedQuestions_Master.aspx.cs" Inherits="Itle_Itle_FrequentlyAskedQuestions_Master"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

    <%--<script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" type="text/javascript"></script>--%>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <%--<script type="text/javascript" charset="utf-8">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }
        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });
            });
        }
    </script>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FREQUENTLY ASKED QUESTIONS</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlQuestrions" runat="server">
                        <div class="col-12" id="trCreate" runat="server">
                            <asp:Panel ID="pnlQuestionsByStudent" runat="server">
                                <div class="row">

                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Current Date :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCurrdate" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-12 col-md-6 col-12 mt-2">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Course Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                        <div class="label-dynamic">
                                            <label>Subject</label>
                                        </div>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" ToolTip="Enter Subject"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-9 col-md-12 col-12 mt-md-3">
                                        <div class="label-dynamic">
                                            <label>Question</label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtQuestion" runat="server" Height="150" BasePath="~/plugins/ckeditor">		                        
                                        </CKEditor:CKEditorControl>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuestion"
                                            ErrorMessage="Please Enter Question" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                        <%--<ajaxToolkit:CascadingDropDown ID="cddDept" runat="server" TargetControlID="ddlBranch"
                                                                 Category="City" PromptText="Please Select" LoadingText="[Loading...]" ServicePath="~/WebService.asmx"
                                                                ServiceMethod="GetBranch" ParentControlID="ddlDept" />--%>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:UpdatePanel ID="updFAQ" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit Question" Font-Bold="false" ValidationGroup="submit"
                                                    OnClick="btnSubmit_Click" ToolTip="Click here to Submit Question" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnViewFAQ" runat="server" Font-Bold="false" Text="Download FAQ Answers"
                                                    CssClass="btn btn-primary" ToolTip="Click here to Download FAQ Answers" OnClick="btnViewFAQ_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="false" CssClass="btn btn-warning"
                                                    OnClick="btnCancel_Click" ToolTip="Click here to Reset" />

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                                <asp:PostBackTrigger ControlID="btnCancel" />
                                                <asp:PostBackTrigger ControlID="btnViewFAQ" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-12">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                    </div>

                                </div>
                            </asp:Panel>
                        </div>

                        <div class="col-12" id="trAnswer" runat="server" visible="false">
                            <asp:Panel ID="pnlFacultyAnswer" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Faculty Answer Reply</h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Subject :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSubject" runat="server"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Question :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblQuestion" runat="server"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>


                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <label>Answer</label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtAnswer" runat="server" Height="200" BasePath="~/ckeditor">		                        
                                        </CKEditor:CKEditorControl>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trfile">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:HyperLink ID="linkAssingReplyFile" runat="server" Target="_blank">Download Attachment</asp:HyperLink>
                                        <%--Text='<%# Eval("ATTACHMENT") %>' NavigateUrl='<%# GetFileName(Eval("ATTACHMENT"), Eval("FQUES_NO"))%>'--%><%--<asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">LinkButton</asp:LinkButton>--%><%--<a href='<%# GetFileName(Eval("ATTACHMENT"), Eval("FQUES_NO"))%>'>Download </a>--%><%--<asp:LinkButton ID="LinkButton1" runat="server"  Text='<%# Eval("ATTACHMENT") %>'></asp:LinkButton>--%><asp:HiddenField
                                            ID="hdnFile" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trReplyDate">
                                        <div class="label-dynamic">
                                            <label>Reply Date</label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="tdDate">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer" runat="server" id="trButtons">

                                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary"
                                        ToolTip="Click here to Go Back" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false"
                                        DisplayMode="List" ValidationGroup="submit" />

                                </div>

                                <div class="col-12">
                                    <asp:Label ID="Label4" runat="server" SkinID="Msglbl"></asp:Label>

                                </div>

                            </asp:Panel>
                        </div>

                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server" Visible="false">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                    <asp:Repeater ID="lvlinks" runat="server">
                                        <HeaderTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>SENTBOX</h5>
                                                </div>
                                            </div>

                                            <thead class="bg-light-blue">
                                                <tr>

                                                <tr class="header">
                                                    <th>Action
                                                    </th>
                                                    <th>Subject
                                                    </th>
                                                    <th>Question
                                                    </th>
                                                    <th>Created Date
                                                    </th>
                                                    <th>Faculty Reply(Inbox)
                                                    </th>
                                                    <th>Student Reply</th>
                                                </tr>
                                                <%--<tr id="itemPlaceholder" runat="server" />--%>
                                            <thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%--<tr class="item">--%>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("QUES_NO") %>'
                                                        ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click"
                                                        Enabled='<%# checkEdit(Eval("FQUES_NO"))%>' />&nbsp;<%--<asp:ImageButton ID="btnDelete"
                                                                     runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("AS_NO") %>'
                                                                ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />--%>
                                                </td>
                                                <td>
                                                    <%--<%# Eval("SUBJECT")%>--%><asp:Label ID="lblUser" runat="server" Text='<%# Eval("SUBJECT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("QUESTION")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CREATED_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnReplyAnswer" runat="server" Enabled='<%# checkeEnable(Eval("FQUES_NO"))%>'
                                                        OnClick="btnReplyAnswer_Click" Text='<%# Eval("FQUES_NO") %>'
                                                        CommandArgument='<%# Eval("QUES_NO") %>'></asp:LinkButton>
                                                    <%--<asp:HyperLink ID="HyperLink2" Enabled='<%# checkeEnable(Eval("FQUES_NO"))%>' Text="Replied"
                                                                 NavigateUrl='<%# "Itle_ViewFAQ_Answer.aspx?QUES_NO=" + Eval("QUES_NO")%>'
                                                             runat="server" OnClick="btnReplyAnswer_Click"> </asp:HyperLink>--%>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
