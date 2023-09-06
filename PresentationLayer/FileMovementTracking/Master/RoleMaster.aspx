<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RoleMaster.aspx.cs" Inherits="FileMovementTracking_Master_RoleMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <%--<asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ROLE MASTER</h3>
                </div>
                <div>
                    <div class="box-body">
                        <div class="col-12">
                            <asp:Panel ID="pnlDesig" runat="server">
                                <div class="sub-heading">
                                    <h5>Add/Edit Role Name</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Role Name </label>
                                        </div>
                                        <asp:TextBox ID="txtRoleName" runat="server" onkeypress="return CheckAlphabet(event,this);"
                                          ValidationGroup="Submit" MaxLength="30" TabIndex="1" CssClass="form-control" ToolTip="Enter Role Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRoleName" runat="server" ControlToValidate="txtRoleName"
                                            Display="None" ErrorMessage="Please Enter Role Name" ValidationGroup="Submit"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>

                                      
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                </div>
                <div class="col-12 btn-footer">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" CausesValidation="true" TabIndex="1" OnClientClick="return ValidateFields()"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="1" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                </div>
                <div class="col-12">
                    <asp:Panel ID="pnlList" runat="server">
                        <asp:ListView ID="lvRole" runat="server" Visible="false">
                            <LayoutTemplate>
                                <div id="lgv1">
                                    <div class="sub-heading">
                                        <h5>ROLE NAME ENTRY LIST</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead>
                                            <tr class="bg-light-blue">
                                                <th>EDIT
                                                </th>
                                                <th>ROLE NAME 
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
                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                            CommandArgument='<%# Eval("ROLE_ID") %>' ImageUrl="~/Images/edit1.png"
                                            ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("ROLENAME")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>


            </div>
        </div>
    </div>
    <script type="text/javascript" >


        function ValidateFields() {
            document.getElementById("ctl00_ContentPlaceHolder1_btnSubmit").onclick = function () {
                //disable
                this.disabled = true;
            }
        }


        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

