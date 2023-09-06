<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Head_PrivilegeToUser.aspx.cs" Inherits="PayRoll_Pay_Head_PrivilegeToUser"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">PAY HEAD PRIVILEGES TO USER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select Employee</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Employee Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlUserName" AppendDataBoundItems="true" ToolTip="Select Employee Name" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" TabIndex="1">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="ddlUserName"
                                                Display="None" ErrorMessage="Please Select Employee" ValidationGroup="payroll"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlPayhead" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvPayhead" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Pay Head</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="totalAppointment(this)" TabIndex="2" />Head
                                                        </th>
                                                        <th>Pay Short
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="ChkPayHead" runat="server" AlternateText="Check to allocate Payhead"
                                                        ToolTip='<%# Eval("PayHead")%>' />
                                                    <%# Eval("PayHead")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PayShort")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlButton" runat="server">
                                <div class="col-12 btn-footer mt-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary"
                                        OnClick="btnSave_Click" TabIndex="3" ToolTip="Click To Save" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" ValidationGroup="payroll"
                                        CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To go to Previous" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" TabIndex="5" ToolTip="Click To Reset" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>


                        </div>
                    </div>
                </div>
            </div>

                
                        <asp:Panel ID="pnluser" runat="server" Width="90%" CssClass="d-none">
                            <%--<fieldset class="fieldsetPay">
                                <legend class="legendPay"></legend>--%>
                            <br />
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td class="form_left_text"><%--Employee Name:
                                            <asp:DropDownList ID="ddlUserName" AppendDataBoundItems="true" runat="server" Width="300px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="ddlUserName"
                                                Display="None" ErrorMessage="Please Select Employee" ValidationGroup="payroll"
                                                InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <%--    </table>
                             <br />
                            </fieldset>
                        </asp:Panel>
                    </td>
                </tr>--%>
                                <tr>
                                    <td>&nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="70%">
                                            <tr>
                                                <td width="60%" style="padding-left: 10px"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%--<asp:Panel ID="pnlButton" runat="server">
                                                        <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                    <br />
                                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" Width="80px"
                                                                        OnClick="btnSave_Click" />&nbsp;
                                                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" ValidationGroup="payroll"
                                                                            Width="80px" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />&nbsp;
                                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                                            OnClick="btnCancel_Click" Width="80px" />
                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            </fieldset>
                        </asp:Panel>
                    
            

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function totalAppointment(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

</asp:Content>
