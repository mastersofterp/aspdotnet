﻿<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_SalaryDeposit.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_SalaryDeposit" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SALARY DEPOSIT REPORT</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                            </h5>
                            <div class="panel panel-info">
                                <div class="panel-heading"></div>
                                <div class="panel-body">
                                    <asp:Panel ID="pnl" runat="server">
                                        <fieldset class="fieldsetPay">
                                            <legend class="legendPay"></legend>
                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-6">
                                                    <div class="form-group col-md-10">
                                                        <label>From Date:<span style="color: Red">*</span></label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtFromDate" runat="server" ToolTip="Enter From Date" TabIndex="1" CssClass="form-control" />
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Style="cursor: hand" />
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                                                ValidationGroup="Payroll" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-10">
                                                        <label>To Date:<span style="color: Red">*</span></label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" ToolTip="Enter To Date" CssClass="form-control"
                                                                AutoPostBack="True" OnTextChanged="txtToDate_TextChanged" />
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCalToDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Style="cursor: hand" />
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />

                                                            <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                                Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                                                ValidationGroup="Payroll" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-10">
                                                        <label>College Name:</label>

                                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="true" TabIndex="3" ToolTip="Select College">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-10">
                                                        <asp:RadioButton ID="rdoParticularEmployee" runat="server" Checked="true"
                                                            Text="Particular Employee" ToolTip="Particular Employee" GroupName="Employee" onclick="DisableDropDownListParticularEmployee(true);"
                                                            TabIndex="4" />

                                                        <asp:RadioButton ID="rdoAllEmployee" ToolTip="All Employee" runat="server" Text="All Employee" GroupName="Employee" onclick="DisableDropDownListAllEmployee(true);"
                                                            TabIndex="5" />
                                                    </div>
                                                    <div class="form-group col-md-10">
                                                        <label>Staff No.:</label>

                                                        <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="true" TabIndex="6" ToolTip="Select Staff No">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-10">
                                                        <label>Employee:</label>

                                                        <asp:DropDownList ID="ddlEmployeeNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                            ToolTip="Select Employee" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6">
                                                </div>
                                            </div>
                                        </fieldset>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report"
                                    OnClick="btnShowReport_Click" ValidationGroup="Payroll" TabIndex="7" ToolTip="Click To Show Report" CssClass="btn btn-info" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" TabIndex="8" ToolTip="Click To Reset" CssClass="btn btn-danger" />
                            </div>
                        </div>

                    </div>
                </form>
            </div>

        </div>


    </div>









    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Flash the text/border red and fade in the "close" button --%>
        <%--<tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">SALARY DEPOSIT REPORT&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
        <%--<asp:CheckBox id="chkPartiularColumn" runat="server" Text="Particular Column" 
                     TabIndex="9" onclick="DisableListboxOnParticularColumn(true);" />--%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>


            </td>
        </tr>
    </table>
    <br />
    <%-- <fieldset class="fieldsetPay">
        <legend class="legendPay">Salary Deposit Report</legend>--%>
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td class="data_label" width="25%"><%--From Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" Width="80px" />
                    &nbsp;
                    <asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png"
                        Style="cursor: hand" />
                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                    </ajaxToolKit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                        Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                        ValidationGroup="Payroll" />
                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="true" />--%>
            </td>
        </tr>
        <tr>
            <td class="data_label" width="25%"><%--To Date:
            </td>
            <td>
                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Width="80px"
                    AutoPostBack="True" OnTextChanged="txtToDate_TextChanged" />
                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                </ajaxToolKit:CalendarExtender>
                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                &nbsp;<asp:Image ID="imgCalToDate" runat="server" ImageUrl="~/images/calendar.png"
                    Style="cursor: hand" />
                <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                    Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                    ValidationGroup="Payroll" />--%>
            </td>
        </tr>
        <tr>
            <td width="25%"><%--College Name:
            </td>
            <td>
                <asp:DropDownList ID="ddlCollege" runat="server" Width="150px"
                    AppendDataBoundItems="true" TabIndex="5">
                </asp:DropDownList>--%>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <%-- <asp:RadioButton ID="rdoParticularEmployee" runat="server" Checked="true"
                    Text="Particular Employee" GroupName="Employee" onclick="DisableDropDownListParticularEmployee(true);"
                    TabIndex="3" />
            </td>
            <td>
                <asp:RadioButton ID="rdoAllEmployee" runat="server" Text="All Employee" GroupName="Employee" onclick="DisableDropDownListAllEmployee(true);"
                    TabIndex="4" />--%>
            </td>
        </tr>
        <tr>
            <td width="25%"><%--Staff No.:
            </td>
            <td>
                <asp:DropDownList ID="ddlStaffNo" runat="server" Width="150px"
                    AppendDataBoundItems="true" TabIndex="5">
                </asp:DropDownList>--%>
            </td>
        </tr>
        <tr>
            <td width="25%" style="height: 28px"><%--Employee:
            </td>
            <td style="height: 28px">
                <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True"
                    Width="150px" TabIndex="6">
                </asp:DropDownList>--%>
            </td>
        </tr>

        <tr>
            <td width="25%">

                <%-- <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                    DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                    Width="123px" />

            </td>
            <td>&nbsp;<asp:Button ID="btnShowReport" runat="server" Text="Show Report"
                OnClick="btnShowReport_Click" ValidationGroup="Payroll" TabIndex="11" />
                &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        OnClick="btnCancel_Click" TabIndex="12" />--%>
            </td>
        </tr>

    </table>
    </fieldset>
    <script type="text/javascript">
        function DisableDropDownListAllEmployee(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = false;

        }
        function DisableDropDownListParticularEmployee(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = false;
        }





    </script>
    <div id="divMsg" runat="server"></div>
</asp:Content>

