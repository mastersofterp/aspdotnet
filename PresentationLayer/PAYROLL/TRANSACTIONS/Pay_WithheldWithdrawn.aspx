<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_WithheldWithdrawn.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_WithheldWithdrawn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">WITHHELD PROCESS</h3>
                        </div>
                        <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Withheld Process</h5>
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
                                                    <label>Month/Year</label>
                                                </div>
                                                <asp:DropDownList ID="ddlMonthYear" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="True" TabIndex="1" AutoPostBack="true" ToolTip="Select Month/Year"
                                                    OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                    ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="True" TabIndex="2" AutoPostBack="true" ToolTip="Select College"
                                                    OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCollege" runat="server" SetFocusOnError="true"
                                                    ControlToValidate="ddlCollege" Display="None" ErrorMessage="Please Select College"
                                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <%--<label>Staff</label>--%>
                                                     <label>Scheme/Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control"
                                                    AppendDataBoundItems="True" TabIndex="3" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Select Staff"
                                                    OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                            ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Staff "
                                                            ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Order By</label>
                                                </div>
                                                <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Order By">
                                                    <asp:ListItem Value="1" Text="IDNO"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="SEQ_NO"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>                                            
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                            DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                        <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="5"
                                            CssClass="btn btn-primary" ToolTip="Click here to Show Records"
                                            ValidationGroup="Payroll" OnClick="btnShow_Click" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="6"
                                            CssClass="btn btn-primary" ToolTip="Click here Save"
                                            ValidationGroup="Payroll" OnClick="btnSave_Click" Enabled="false" />                                                
                                        <asp:Button ID="btnReport" runat="server" Text="Show Paid/Unpaid Count" TabIndex="7"
                                            CssClass="btn btn-primary" ToolTip="Click here to show report"
                                            OnClick="btnsummary_Click"  Visible="false"/>
                                        <asp:Button ID="BtnAbst" runat="server" Text="Show Hold/Release detail" TabIndex="8"
                                            CssClass="btn btn-primary" ToolTip="Click here to show report"
                                            OnClick="BtnAbst_Click"  Visible="false"/>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9"
                                            CssClass="btn btn-warning" ToolTip="Click here to Reset"
                                            OnClick="btnCancel_Click" />
                                        <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                    </div>                                      
                                    <div class="col-12 btn-footer" id="divpaid" runat="server">
                                                <%-- PAID / UNPAID EMPLOYEE SUMMARY--%>
                                                <asp:GridView ID="gdrpaid" runat="server" CssClass="table table-responsive" HeaderStyle-BackColor="#bce8f1"></asp:GridView>
                                            </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlWithheldWithdrawn" runat="server">
                                    <div class="col-12">
                                    <asp:ListView ID="lvWithheldWithdrawn" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" CssClass="text-center mt-3"/>          
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
                                                            <th>Withheld Status
                                                            </th>
                                                            <th>Withheld Remark
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
                                                    <asp:CheckBox ID="ChkWithheldStatus" runat="server" AlternateText="Check to Withheld Salary"
                                                        ToolTip='<%# Eval("IDNO")%>' Checked='<%# (Convert.ToInt32(Eval("WithHeldStatus") )== 0 ?  false : true )%>'
                                                        Font-Bold="true" ForeColor="Green" Text='<%# (Convert.ToInt32(Eval("WithHeldStatus") )== 1 ?  "Withheld" : "" )%>' />
                                                    <%-- <%# Eval("WithHeldStatus")%>--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWithHeldRemark" runat="server" MaxLength="200" Text='<%#Eval("WithHeldRemark")%>'
                                                        ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    </div>
                                </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>


            <%-- <div id="divPaySlip" style="padding-left: 15px; width: 90%" runat="server">
            </div>--%>

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

        <Triggers>

            <asp:PostBackTrigger ControlID="BtnAbst" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

