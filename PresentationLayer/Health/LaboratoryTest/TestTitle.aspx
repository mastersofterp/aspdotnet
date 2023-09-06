<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TestTitle.aspx.cs" Inherits="Health_LaboratoryTest_TestTitle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <%--<div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TEST TITLE MASTER</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlTestTitle" runat="server">
                                <div class="col-12">
                              <%--      <div class="sub-heading">
                                        <h5>Create Test Title</h5>
                                    </div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Test Title</label>
                                            </div>
                                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" CssClass="form-control"
                                                ToolTip="Enter Test Title" TabIndex="1"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeTitle" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtTitle"
                                                InvalidChars="0123456789" ValidChars="-  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please Enter Test Title." ControlToValidate="txtTitle"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                           
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="3"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" CausesValidation="true" />

                                    <asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="5" CssClass="btn btn-outline-info"
                                        OnClick="btnRport_Click" ToolTip="CLick here to Show Report" />
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="4"
                                        CssClass="btn btn-outline-danger" ToolTip="Click here to Reset" CausesValidation="false" />
                                </div>
                                <div class="col-12 mt-3">
                                    <asp:Panel ID="pnlTestTitleList" runat="server">
                                        <asp:ListView ID="lvTitle" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Test Title Entry List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>EDIT
                                                                </th>
                                                                <th>TEST TITLE
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                            CommandArgument='<%# Eval("TITLENO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("TITLE")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>

