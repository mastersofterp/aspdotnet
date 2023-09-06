<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AssignCashbook.aspx.cs" Inherits="ACCOUNT_AssignCashbook" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .checkbox {
            padding-left: 20px;
        }

            .checkbox label {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }

                .checkbox label::before {
                    content: "";
                    display: inline-block;
                    position: absolute;
                    width: 17px;
                    height: 17px;
                    left: 0;
                    margin-left: -20px;
                    border: 1px solid #cccccc;
                    border-radius: 3px;
                    background-color: #fff;
                    -webkit-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    -o-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                }

                .checkbox label::after {
                    display: inline-block;
                    position: absolute;
                    width: 16px;
                    height: 16px;
                    left: 0;
                    top: 0;
                    margin-left: -20px;
                    padding-left: 3px;
                    padding-top: 1px;
                    font-size: 11px;
                    color: #555555;
                }

            .checkbox input[type="checkbox"] {
                opacity: 0;
                z-index: 1;
            }

                .checkbox input[type="checkbox"]:checked + label::after {
                    font-family: 'Font Awesome 5 Free';
                    content: "\f00c";
                }

        .checkbox-primary input[type="checkbox"]:checked + label::before {
            background-color: #337ab7;
            border-color: #337ab7;
        }

        .checkbox-primary input[type="checkbox"]:checked + label::after {
            color: #fff;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAssignCashBook"
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
    </div>
    <asp:UpdatePanel ID="updAssignCashBook" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ASSIGN CASHBOOK</h3>
                        </div>
                        <div class="box-body">
                            <div id="divCompName" runat="server"></div>
                            <asp:UpdatePanel ID="updCashBookAssign" runat="server">
                            </asp:UpdatePanel>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <asp:Panel ID="pnlUserType" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>User Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                                        ValidationGroup="show">

                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ValidationGroup="show"
                                                        ErrorMessage="Please Select User Type" ControlToValidate="ddlUserType" Display="None"
                                                        InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="show"
                                                        OnClick="btnShow_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false"
                                                        OnClick="btnCancel_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12 mt-4 mb-4 ">
                                        <div class="row">
                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select CashBook</label>
                                                </div>
                                                <div class="form-group col-md-12 checkbox-list-box">
                                                    <asp:Panel ID="pnlTree" runat="server">
                                                        <asp:CheckBoxList ID="chkCashbook" CssClass="checkbox checkbox-list-style" runat="server" Font-Size="Smaller">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlListMain" runat="server" Visible="false">
                                <div class="col-12 btn-footer">
                                    <%--          <asp:Button ID="btnAssign" runat="server" Text="Assign Link" Width="100px" OnClientClick="return validateAssign();" 
                                               onclick="btnAssign_Click" />--%>
                                    <asp:Button ID="btnAssign" runat="server" Text="Assign Cash Book" CssClass="btn btn-primary" OnClick="btnAssign_Click" />
                                </div>

                                <div class="col-12 mt-4">
                                    <asp:Panel ID="pnlListMain1" runat="server">
                                        <asp:ListView ID="lvUsers" runat="server" OnItemDataBound="lvUsers_ItemDataBound">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>User List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="chkHead" runat="server" Checked="true" onclick="totAllSubjects(this)" />
                                                                </th>
                                                                <th>UserName
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                                <th>Assign Cash Book
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
                                                        <asp:CheckBox ID="chkAccept" runat="server" Checked="true" ToolTip='<%# Eval("UA_NO")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_NAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_FULLNAME") %>
                                                    </td>
                                                    <td>
                                                        <div id="divCashBook" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </asp:Panel>




                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function totAllSubjects(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkAccept')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else
                            e.checked = false;

                    }
                }
            }

            if (headchk.checked == false) hdfTot.value = "0";
        }

        function validateAssign() {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>').value;

            if (hdfTot == 0) {
                alert('Please Select Atleast One User from User List');
                return false;
            }
            else
                return true;
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
