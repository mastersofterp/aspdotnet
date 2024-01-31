<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Holidays.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_Holidays" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

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

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
        onkeypress = "return CheckAlphabet(event,this);"
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">HOLIDAYS ENTRY</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add/Edit Holiday Details</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select College Name">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup> </sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlstaff" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select Staff Type">
                                    </asp:DropDownList>
                                   <%-- <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlstaff"
                                        Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Holiday"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Holiday Name</label>
                                    </div>
                                    <asp:TextBox ID="txtHoliday" runat="server" MaxLength="50" CssClass="form-control"
                                        ToolTip="Enter Holiday Name" TabIndex="3" onkeypress="return CheckAlphabet(event,this);"/>
                                    <asp:RequiredFieldValidator ID="rfvHoliday" runat="server" ControlToValidate="txtHoliday"
                                        Display="None" ErrorMessage="Please Enter Holiday Name" ValidationGroup="Holiday"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Holiday Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlHoliDayType" runat="server" CssClass="form-control" TabIndex="4" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select Holiday Type">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvHolidaytype" runat="server" ControlToValidate="ddlHoliDayType"
                                        Display="None" ErrorMessage="Please Enter Holiday Type" ValidationGroup="Holiday"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalholidayDt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <%--<div class="input-group-addon">
                                                <asp:Image ID="imgCalholidayDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>--%>
                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="5"
                                            ToolTip="Enter Holiday Starting Date" />
                                        <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                            Display="None" ErrorMessage="Please Enter Holiday From Date" ValidationGroup="Holiday"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter Holiday From Date"
                                            InvalidValueMessage="Holiday From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter Holiday From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Holiday" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
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
                                        <%--<div class="input-group-addon">
                                                <asp:Image ID="imgCalToDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>--%>
                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="6" AutoPostBack="true"
                                            ToolTip="Enter Holiday Ending Date" OnTextChanged="txtToDt_TextChanged" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                            Display="None" ErrorMessage="Please Enter Holiday To Date" ValidationGroup="Holiday"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter Holiday To Date"
                                            InvalidValueMessage="Holiday To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter Holiday To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Holiday" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="tryr" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Year</label>
                                    </div>
                                    <asp:TextBox ID="txtYear" runat="server" TabIndex="7" MaxLength="4" CssClass="form-control" ToolTip="Enter Year" />
                                    <asp:RangeValidator ID="rngYear" runat="server" ControlToValidate="txtYear" Display="None"
                                        ErrorMessage="Please Enter Year Between 0 to 9999" ValidationGroup="Holiday"
                                        SetFocusOnError="true" MinimumValue="0000" MaximumValue="9999" Type="Double"></asp:RangeValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Period</label>
                                    </div>
                                    <asp:DropDownList ID="ddlPeriod" TabIndex="8" runat="server" CssClass="form-control" ToolTip="Select Period" data-select2-enable="true"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPeriod" runat="server" ControlToValidate="ddlPeriod"
                                        Display="None" ErrorMessage="Please Select Period" ValidationGroup="Holiday"
                                        SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Restricted Holiday</label>
                                    </div>
                                    <asp:CheckBox ID="chkRestrict" runat="server" TabIndex="9"
                                        ToolTip="Check to Restrict the Holiday" />
                                </div>
                            </div>
                        </div>
                <div class="col-12 btn-footer">
                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                </div>
                </asp:Panel>

                    <asp:Panel ID="pnlFilter" runat="server">
                    <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div id="divnote" runat="server">
                                        Note <b>:</b> <span style="color: #FF0000">Please Select Year for Filter Holiday List.</span>
                                    </div>
                                </div>
                            </div>
                    </div>
                    <div class="form-group col-12 mt-3">
                    <div class="row">
                    <div class="form-group col-lg-3 col-md-6 col-12">
                        <div class="label-dynamic">
                            <sup>* </sup>
                            <label>Year</label>
                        </div>
                        <asp:DropDownList ID="ddlYear" TabIndex="10" runat="server" CssClass="form-control" ToolTip="Select Year" data-select2-enable="true"
                             AppendDataBoundItems="true" AutoPostBack="true">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                        Display="None" ErrorMessage="Please Select Year" ValidationGroup="HolidayShow"
                        SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-lg-3 col-md-6 col-12">
                        <div class="label-dynamic">
                            <%--<sup>* </sup>--%>
                            <label>Month</label>
                        </div>
                        <asp:DropDownList ID="ddlMonth" TabIndex="11" runat="server" CssClass="form-control" ToolTip="Select Month" data-select2-enable="true"
                             AppendDataBoundItems="true" AutoPostBack="true">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlYear"
                        Display="None" ErrorMessage="Please Select Year" 
                        SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>--%>
                    </div>
                    </div>
                    </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnShow" runat="server" SkinID="LinkAddNew" OnClick="btnShow_Click" Text="Show" TabIndex="12"
                            CssClass="btn btn-primary" ToolTip="Click here to show Holidays List" ValidationGroup="HolidayShow"></asp:LinkButton>
                        <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New" TabIndex="13"
                            CssClass="btn btn-primary" ToolTip="Click here to Add New Holiday"></asp:LinkButton>
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Holiday" OnClick="btnSave_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="14" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Return to Previous Menu" TabIndex="15" />
                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                            OnClick="btnShowReport_Click" TabIndex="16" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                            OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="17" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Holiday"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="HolidayShow"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                <asp:Panel ID="pnlList" runat="server">
                    <div class="col-12">
                        <%-- <asp:ListView ID="lvHoliday" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Holidays" />
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Holidays List
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Holiday Name
                                                    </th>
                                                    <th>From Date
                                                    </th>
                                                    <th>To Date
                                                    </th>
                                                    <th>Period
                                                    </th>
                                                    <th>Type
                                                    </th>
                                                    <th>Staff Type
                                                    </th>
                                                    <th>
                                                       Restricted
                                                     </th>
                                                    <th>College Name
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("HNO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                            
                                        </td>
                                        <td>
                                            <%# Eval("HOLIDAYNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("DT")%>
                                        </td>
                                        <td>
                                            <%# Eval("TODATE") %>
                                        </td>
                                        <td>
                                            <%# Eval("PERIOD") %>
                                        </td>
                                        <td>
                                            <%# Eval("HOLIDAYTYPE") %>
                                        </td>
                                        <td>
                                            <%# Eval("STAFFTYPE") %>
                                        </td>
                                        <td>
                                          <%# Eval("RESTRICT_STATUS")%>
                                         </td>
                                        <td>
                                            <%# Eval("COLLEGE_NAME") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div class="vista-grid_datapager">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvHoliday" PageSize="10"
                                        OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>--%>

                        <asp:Repeater ID="lvHoliday" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>Holidays List</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Action
                                            </th>
                                            <th>Holiday Name
                                            </th>
                                            <th>From Date
                                            </th>
                                            <th>To Date
                                            </th>
                                            <th>Period
                                            </th>
                                            <th>Holiday Type
                                            </th>
                                            <th>Staff Type
                                            </th>
                                            <th>Restricted
                                            </th>
                                            <th>College Name
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("HNO") %>'
                                            AlternateText="Edit Record" ToolTip='<%# Eval("HNO") %>' OnClick="btnEdit_Click" TabIndex="18" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HNO") %>'
                                                    AlternateText="Delete Record" ToolTip='<%# Eval("HNO") %>' OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td>
                                        <%# Eval("HOLIDAYNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("DT")%>
                                    </td>
                                    <td>
                                        <%# Eval("TODATE") %>
                                    </td>
                                    <td>
                                        <%# Eval("PERIOD") %>
                                    </td>
                                    <td>
                                        <%# Eval("HOLIDAYTYPE") %>
                                    </td>
                                    <td>
                                        <%# Eval("STAFFTYPE") %>
                                    </td>
                                    <td>
                                        <%# Eval("RESTRICT_STATUS")%>
                                    </td>
                                    <td>
                                        <%# Eval("COLLEGE_NAME") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel>

                <%--<asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
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
                    </asp:Panel>--%>
            </div>
        </div>
    </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
