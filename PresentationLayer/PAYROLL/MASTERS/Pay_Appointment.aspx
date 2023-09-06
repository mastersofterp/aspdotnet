<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Appointment.aspx.cs" Inherits="PayRoll_Pay_Appointment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">APPOINTMENT</h3>
                        </div>

                        <div class="box-body">
                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Appointment</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Appointment Type</label>
                                        </div>
                                        <asp:TextBox ID="txtAppointMent" runat="server" MaxLength="30" CssClass="form-control" placeholder="Enter Appointment"
                                            ToolTip="Enter Appointment" TabIndex="1" />
                                        <asp:RequiredFieldValidator ID="rfvAppointMent" runat="server" ControlToValidate="txtAppointMent"
                                            Display="None" ErrorMessage="Please Enter Appointment Type" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="reAppoint" runat="server" ControlToValidate="txtAppointMent"
                                            ValidationExpression="^[A-za-z ]+$" Display="None" ErrorMessage="Please Enter charaters only"
                                            ValidationGroup="payroll"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" TabIndex="2" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary"
                                    OnClick="btnSave_Click" ToolTip="Click here to Save" />
                                <asp:Button ID="btnShowReport" runat="server" TabIndex="3" Text="Show Report" CssClass="btn btn-info" ToolTip="click here to Show Report"  Visible="false"/>
                                <asp:Button ID="btnCancel" runat="server" TabIndex="4" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning"
                                    OnClick="btnCancel_Click" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlPayhead" runat="server">
                                    <asp:Label ID="Label1" runat="server" SkinID="Msglbl"></asp:Label>
                                    <asp:ListView ID="lvPayhead" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found"/>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Pay Head</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="totalAppointment(this)" />Action
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
                                                    <asp:CheckBox ID="ChkAppointment" runat="server" AlternateText="Check to allocate Payhead"
                                                        ToolTip='<%# Eval("PayHead")%>' />
                                                    <%# Eval("PayHead")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PayShort")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 mt-4">
                                <asp:Panel ID="PnlAppoint" runat="server">
                                    <asp:ListView ID="lvAppoint" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Appointment</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Appointment
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
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("APPOINTNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("APPOINT")%>
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
            </div>


            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
       
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
