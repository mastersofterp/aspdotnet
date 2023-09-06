<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AssignShift.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_AssignShift" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>--%>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

    <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ASSIGN SHIFT</h3>
                        </div>
                        <div class="box-body">
                           
                                <asp:Panel ID="pnlAdd" runat="server">

                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select Criteria to Assign Change Shift</h5>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select College" ValidationGroup="Shift"
                                                    SetFocusOnError="True" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Staff Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                                    AppendDataBoundItems="true" ToolTip="Select Staff Type" AutoPostBack="true" OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                                    Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Shift"
                                                    SetFocusOnError="true" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStafftype"
                                                    Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave1"
                                                    SetFocusOnError="true" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trdept" runat="server">
                                                <div class="label-dynamic">
                                                    <%--<sup>* </sup>--%>
                                                    <label>Department</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                                    AppendDataBoundItems="true" ToolTip="Select Department" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%--  <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                        Display="None" ErrorMessage="Please Select Department" ValidationGroup="Shift"
                                                        SetFocusOnError="True" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Shift</label>
                                                </div>
                                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    TabIndex="4" ToolTip="Select Shift">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvShift" runat="server" ControlToValidate="ddlShift"
                                                    Display="None" ErrorMessage="Please Select Shift" ValidationGroup="Shift"
                                                    SetFocusOnError="True" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Effect From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalholidayDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control"
                                                        ToolTip="Enter Effect From Date" TabIndex="5" Style="z-index: 0;" />
                                                    <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Shift"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                                        ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter Holiday Date"
                                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtToDt_TextChanged"
                                                        ToolTip="Enter Effect To Date" TabIndex="6" Style="z-index: 0;" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                                        Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Shift"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                                        ControlToValidate="txtToDt" EmptyValueMessage="Please Enter Holiday Date"
                                                        InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </div>
                                </asp:Panel>
                        
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Assign" ValidationGroup="Shift" OnClick="btnSave_Click" TabIndex="7"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" OnClick="btnShowReport_Click"
                                    Visible="false" ToolTip="Click here to Show Report" TabIndex="8" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="9"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Shift"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:Repeater ID="lvEmpList" runat="server">
                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Employees</h5>
                                            </div>
                                            <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <%--<th>Sr.No
                                                </th--%>
                                                        <th>
                                                            <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true" runat="server"
                                                                onclick="checkAllEmployees(this)" TabIndex="10" ToolTip="Check to Select All Employee" />
                                                        </th>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Shift Name
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%--<td>
                                            <%#Container.DataItemIndex+1 %>
                                        </td>--%>
                                                <td>
                                                    <asp:CheckBox ID="chkID" runat="server" TabIndex="11" ToolTip='<%#Eval("Idno")%>' />
                                                </td>
                                                <td>
                                                    <%#Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("SHIFTNAME")%>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>

                                    <%--  <asp:ListView ID="lvEmpList" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees" />
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Select Employees
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>
                                                        Sr.No
                                                    </th>
                                                    <th>
                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true" runat="server" 
                                                            onclick="checkAllEmployees(this)" TabIndex="10" ToolTip="Check to Select All Employee" />
                                                    </th>
                                                    <th>Employee Name
                                                    </th>
                                                    <th>Shift Name
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
                                             <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                         
                                             <asp:CheckBox ID="chkID" runat="server" TabIndex="11" ToolTip='<%#Eval("Idno")%>' />
                                            

                                        </td>
                                        <td>
                                            <%#Eval("NAME")%>
                                            
                                        </td>
                                        <td>
                                            <%#Eval("SHIFTNAME")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>--%>
                                    <asp:ListView ID="lvContinuerecord" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees" CssClass="d-block text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Employees</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true" runat="server"
                                                                onclick="checkAllEmployees(this)" TabIndex="12" ToolTip="Check to Select All Employee" />
                                                        </th>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Shift Name
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
                                                    <asp:CheckBox ID="chkSelect" runat="server" TabIndex="13" ToolTip='<%#Eval("Idno")%>' />
                                                </td>
                                                <td>
                                                    <%#Eval("NAME")%>
                                                    <%-- <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("NAME")%>'/>--%>
                                                </td>
                                                <td>
                                                    <%#Eval("SHIFTNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                        <%--<div class="col-md-12">
                                <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                    <div class="text-center">
                                        <div class="modal-content">
                                            <div class="modal-body">
                                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                                                <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                                <div class="text-center">
                                                    <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                                    <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>--%>
                    </div>
                </div>
            </div>
           


            <script type="text/javascript">
                //  keeps track of the delete button for the row
                //  that is going to be removed
                var _source;
                // keep track of the popup div
                var _popup;


                function checkAllEmployees(chkcomplaint) {
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

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="ddlStafftype" />
            <asp:PostBackTrigger ControlID="ddlDept" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

