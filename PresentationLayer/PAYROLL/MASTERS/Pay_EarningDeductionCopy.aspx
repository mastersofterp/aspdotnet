<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_EarningDeductionCopy.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_EarningDeductionCopy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COPY EARNING AND DEDUCTION RULES</h3>
                        </div>

                        <div class="box-body">

                            <div class="box box-primary">


                                <div class="box-body">
                                    <div class="col-12">


                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" TabIndex="10" ToolTip="Select College" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCollege" runat="server"
                                                    ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please select College" ValidationGroup="payroll" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Rule</label>
                                                </div>
                                                <asp:DropDownList ID="ddlPayRuleFrom" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlPayRuleFrom_SelectedIndexChanged" ToolTip="Select Rule" TabIndex="11" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please select Rule" ValidationGroup="payroll" InitialValue="0">
                                                </asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>.</label>
                                                </div>
                                                <asp:Button ID="btnshow" runat="server" Text="Show" TabIndex="13" ToolTip="Click to Show" CssClass="btn btn-primary" OnClick="btnshow_Click" ValidationGroup="payroll" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divshow">

                                <div class="box box-primary">


                                    <div class="box-body">
                                        <div id="List" runat="server">
                                            <div class="col-12">
                                                <asp:ListView ID="lvCalPay" runat="server">
                                                    <EmptyDataTemplate>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Earning and Deduction Rules for Copy</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="chkAll" runat="server" onclick="totalAppointment(this)" />Select
                                                                    </th>
                                                                    <th>Code
                                                                    </th>

                                                                    <th>From
                                                                    </th>
                                                                    <th>To
                                                                    </th>
                                                                    <th>Per
                                                                    </th>
                                                                    <th>Min_limit
                                                                    </th>
                                                                    <th>Max_limit
                                                                    </th>
                                                                    <th>Fix_Amt
                                                                    </th>
                                                                    <th>Pay Rule
                                                                    </th>
                                                                    <th>FromDate
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
                                                                <asp:CheckBox ID="ChkAppointment" runat="server" AlternateText="Check to allocate Payhead"
                                                                    ToolTip='<%# Eval("CALNO")%>' />

                                                            </td>
                                                            <td>
                                                                <%# Eval("PAYHEAD")%>
                                                            </td>

                                                            <td>
                                                                <%# Eval("B_MIN")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("B_MAX")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("PER")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MIN")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MAX")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FIX")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("PAYRULE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FDT","{0:dd/MM/yyyy}")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box box-primary">


                                    <div class="box-body">
                                        <div class="col-12">


                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>College</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollegeCopy" AppendDataBoundItems="true" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCollegeCopy_SelectedIndexChanged" TabIndex="10" ToolTip="Select College" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="ddlCollegeCopy"
                                                        Display="None" ErrorMessage="Please select Copy College" ValidationGroup="payroll" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Copy Rule</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRuleCopy" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlRuleCopy_SelectedIndexChanged" ToolTip="Select Rule" TabIndex="11" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="ddlRuleCopy"
                                                        Display="None" ErrorMessage="Please select Rule" ValidationGroup="payroll" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>.</label>
                                                    </div>
                                                    <asp:Button ID="btnShow1" Visible="false" runat="server" Text="Show Rules" TabIndex="13" ToolTip="Click to Show" CssClass="btn btn-primary" OnClick="btnShow1_Click" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Copy selected Rule" TabIndex="13" ToolTip="Click to Copy Rule" CssClass="btn btn-primary" OnClick="btnSave_Click" />


                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="16" ToolTip="Click to Reset" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>

                                        </div>


                                    <%--</div>
                                </div>

                                <div class="box box-primary">


                                    <div class="box-body">--%>

                                        <div class="col-12">
                                            <asp:ListView ID="lvCopyRule" runat="server">
                                                <EmptyDataTemplate>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Existing Earning and Deduction rules </h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>

                                                                <th>Code
                                                                </th>

                                                                <th>From
                                                                </th>
                                                                <th>To
                                                                </th>
                                                                <th>Per
                                                                </th>
                                                                <th>Min_limit
                                                                </th>
                                                                <th>Max_limit
                                                                </th>
                                                                <th>Fix_Amt
                                                                </th>
                                                                <th>Pay Rule
                                                                </th>
                                                                <th>FromDate
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
                                                            <%# Eval("PAYHEAD")%>
                                                        </td>

                                                        <td>
                                                            <%# Eval("B_MIN")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("B_MAX")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PER")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("MIN")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("MAX")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FIX")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PAYRULE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FDT","{0:dd/MM/yyyy}")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>


                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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

