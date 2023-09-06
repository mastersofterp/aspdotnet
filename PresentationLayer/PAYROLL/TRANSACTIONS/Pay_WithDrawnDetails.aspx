<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_WithDrawnDetails.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_WithDrawnDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">

                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">WITHDRAWN PROCESS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select Withdrawn Process</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlShow" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1" data-select2-enable="true"
                                                AutoPostBack="true" ToolTip="Select College"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlCollege" Display="None" ErrorMessage="Please Select College"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Staff</label>--%>
                                                 <label>Scheme/Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="2" AutoPostBack="true" ToolTip="Select Staff"
                                                OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Staff"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Order By</label>
                                            </div>
                                            <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="3" ToolTip="Select Order By">
                                                <asp:ListItem Value="1" Text="IDNO"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="SEQ_NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                    <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="4"
                                        CssClass="btn btn-primary" ToolTip="Click here to Show Records"
                                        ValidationGroup="Payroll" OnClick="btnShow_Click" />
                                    <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="5"
                                        CssClass="btn btn-primary" ToolTip="Click here Save"
                                        ValidationGroup="Payroll" OnClick="btnSave_Click" Enabled="false" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6"
                                        CssClass="btn btn-warning" ToolTip="Click here to Reset"
                                        OnClick="btnCancel_Click" />

                                </div>
                                <div class="col-12">
                                    <div class="text-center">
                                        <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlWithheldWithdrawn" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <asp:ListView ID="lvWithheldWithdrawn" runat="server">
                                            <EmptyDataTemplate>
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" CssClass="w-100 text-center"/>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
	                                                    <h5>Employee List</h5>
                                                    </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr. No.
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                                <th>Withheld Remark
                                                                </th>
                                                                <th>Month Year
                                                                </th>
                                                                <th>Withdrawn Status
                                                                </th>
                                                                <th>Withdrawn Date
                                                                </th>
                                                                <th>Withdrawn Remark
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
                                                        <%# Eval("SRNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtWithHeldRemark" runat="server" MaxLength="200" Text='<%#Eval("WithHeldRemark")%>'
                                                            ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("MonthYear")%>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkWithDrawnStatus" runat="server" AlternateText="Check to Withdrawn Salary"
                                                            ToolTip='<%# Eval("WithHeldID")%>' Checked='<%# (Convert.ToInt32(Eval("WithDrawnStatus") )== 0 ?  false : true )%>'
                                                            Font-Bold="true" ForeColor="Green" Text='<%# (Convert.ToInt32(Eval("WithDrawnStatus") )== 1 ?  "Withdrawn" : "" )%>' />
                                                    </td>
                                                    <%-- <td style="width: 10%">
                                                                            <asp:TextBox ID="txtwd" runat="server" Text='<%#Eval("WithDrawnDate","{0:dd/MM/yyyy}")%>'
                                                                                ToolTip='<%#Eval("IDNO")%>' Width="75px"  />
                                                                            </td>--%>
                                                    <td>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtWithdrawnDate" runat="server" Text='<%#Eval("WithDrawnDate","{0:dd/MM/yyyy}")%>'
                                                                ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" Style="z-index: 0;" />
                                                            <ajaxToolKit:MaskedEditExtender ID="metxtIncDate" runat="server" TargetControlID="txtWithdrawnDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>

                                                            <ajaxToolKit:CalendarExtender ID="cetxtIncDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtWithdrawnDate" PopupButtonID="imgCal"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                        </div>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtWithdrawnRemark" runat="server" MaxLength="200" Text='<%#Eval("WithDrawnRemark")%>'
                                                            ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <%--<div id="divPaySlip" style="padding-left: 15px; width: 90%" runat="server">
                           
                        </div>--%>                    
            </table>
            <div id="divMsg" runat="server"></div>
            <script type="text/javascript">
                function DisableDropDownList(disable) {
                    document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
                    document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
                }
            </script>

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

