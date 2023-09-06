<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Annual_Salary_Detail.aspx.cs" Inherits="PayRoll_Annual_Salary_Detail"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ANNUAL SALARY DETAIL REPORT</h3>
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
                                <div class="panel-heading">Annual Salary Report</div>
                                <div class="panel-body">
                                    <div id="divAnnualSummaryRport" runat="server">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="form-group col-md-10">
                                                    <label>From Date :<span style="color: Red">*</span></label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtFromDate" ToolTip="Enter From Date" runat="server" TabIndex="1" CssClass="form-control" />
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
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
                                                    <label>To Date :<span style="color: Red">*</span></label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtToDate" ToolTip="Enter To Date" runat="server" TabIndex="2" CssClass="form-control" AutoPostBack="True"
                                                            OnTextChanged="txtToDate_TextChanged" />
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCalToDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
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
                                                    <label>Select Employee :</label>
                                                    <br />
                                                    <asp:RadioButton ID="rdoParticularEmployee" runat="server" Checked="true" Text="Particular Employee"
                                                        GroupName="Employee" onclick="DisableDropDownListParticularEmployee(true);" TabIndex="3" />
                                                    <asp:RadioButton ID="rdoAllEmployee" runat="server" Text="All Employee" GroupName="Employee"
                                                        TabIndex="4" onclick="DisableDropDownListAllEmployee(true);" />
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Staff No :</label>

                                                    <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Staff No" AppendDataBoundItems="true"
                                                        TabIndex="5">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Employee :</label>

                                                    <asp:DropDownList ID="ddlEmployeeNo" ToolTip="Select Employee" TabIndex="6" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Month :</label>

                                                    <asp:ListBox ID="lstMonth" runat="server" TabIndex="7" ToolTip="Select Month" SelectionMode="Multiple" Height="100px"
                                                        CssClass="form-control" AppendDataBoundItems="true"></asp:ListBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6"></div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" OnClick="btnShowReport_Click"
                                    ValidationGroup="Payroll" TabIndex="8" ToolTip="Click to Show Report" CssClass="btn btn-info" />
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="9" CssClass="btn btn-danger" ToolTip="Click To Reset" />
                            </div>
                        </div>

                    </div>
                </form>
            </div>

        </div>


    </div>






    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Flash the text/border red and fade in the "close" button --%>
        <%-- <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">ANNUAL SALARY DETAIL REPORT&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
        <%-- <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll" 
                    DisplayMode="List" ShowMessageBox="false" ShowSummary="false" />--%>
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
    <%-- <div id="divAnnualSummaryRport" style="padding-left: 20px; width: 80%">--%>
    <%-- <fieldset class="fieldsetPay">
            <legend class="legendPay">Annual Salary Report</legend>
            <br />--%>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="form_left_label"><%--From Date :
                    </td>
                    <td class="form_left_text">
                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" Width="80px" />
                        &nbsp;<asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
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
            <td class="form_left_label"><%--To Date :
            </td>
            <td class="form_left_text">
                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Width="80px" AutoPostBack="True"
                    OnTextChanged="txtToDate_TextChanged" />
                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                </ajaxToolKit:CalendarExtender>
                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                <asp:Image ID="imgCalToDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
                <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                    Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                    ValidationGroup="Payroll" />--%>
            </td>
        </tr>
        <tr>
            <td class="form_left_label"><%--Select Employee :
            </td>
            <td class="form_left_text">
                <asp:RadioButton ID="rdoParticularEmployee" runat="server" Checked="true" Text="Particular Employee"
                    GroupName="Employee" onclick="DisableDropDownListParticularEmployee(true);" TabIndex="4" />
                <asp:RadioButton ID="rdoAllEmployee" runat="server" Text="All Employee" GroupName="Employee"
                    TabIndex="3" onclick="DisableDropDownListAllEmployee(true);" />--%>
            </td>
        </tr>
        <tr>
            <td class="form_left_label"><%--Staff No :
            </td>
            <td class="form_left_text">
                <asp:DropDownList ID="ddlStaffNo" runat="server" Width="300px" AppendDataBoundItems="true"
                    TabIndex="5">
                </asp:DropDownList>--%>
            </td>
        </tr>
        <tr>
            <td class="form_left_label"><%--Employee :
            </td>
            <td class="form_left_text">
                <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True" Width="300px">
                </asp:DropDownList>--%>
            </td>
        </tr>
        <tr>
            <td class="form_left_label" valign="top"><%--Month :
            </td>
            <td class="form_left_text">
                <asp:ListBox ID="lstMonth" runat="server" SelectionMode="Multiple" Height="100px"
                    Width="200px" AppendDataBoundItems="true"></asp:ListBox>--%>
            </td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <%-- <asp:Button ID="btnShowReport" runat="server" Text="Show Report" OnClick="btnShowReport_Click"
                    ValidationGroup="Payroll" TabIndex="6" />
                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                    DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Width="123px" />
                &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            TabIndex="7" />--%>
            </td>
        </tr>
    </table>
    <br />
    </fieldset>
    </div>

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

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
