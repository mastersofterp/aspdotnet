<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Budget_Allocation_To_Dept.aspx.cs" Inherits="Stores_Masters_Str_Budget_Head_Master"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BUDGET ALLOCATION MASTER</h3>

                        </div>

                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Add/Edit Budget Allocation</div>
                                            <div class="panel-body">
                                                <asp:Panel ID="pnl" runat="server">

                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group col-md-10" id="spanName" runat="server">
                                                                <label><span style="color: #FF0000">*</span>Budget Name: </label>
                                                                <asp:DropDownList ID="ddlBudgetHead" runat="server" CssClass="form-control" ToolTip="Select Budget Name" AppendDataBoundItems="true"
                                                                    TabIndex="1">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvtxtddlBudgetHead" runat="server" ControlToValidate="ddlBudgetHead"
                                                                    Display="None" ErrorMessage="Please select Budget Name" ValidationGroup="store"
                                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-md-10" id="spanSDate" runat="server">
                                                                <label><span style="color: #FF0000">*</span>Start Date:</label>
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtStartDate" runat="server" Style="z-index: 0" ToolTip="Enter Start Date" CssClass="form-control" TabIndex="3" MaxLength="18" />
                                                                    <div class="input-group-addon">
                                                                        <asp:Image ID="imgCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                            Style="cursor: pointer" />
                                                                    </div>

                                                                    <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtStartDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                                        EnableViewState="true">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                                        Display="None" ErrorMessage="Please Select Start Date in (dd/MM/yyyy Format)"
                                                                        ValidationGroup="store" SetFocusOnError="True">
                                                                    </asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:MaskedEditExtender ID="meStartDate" runat="server" TargetControlID="txtStartDate"
                                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                        AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                                    </ajaxToolKit:MaskedEditExtender>
                                                                    <ajaxToolKit:MaskedEditValidator ID="mevtxtExpiryDate" runat="server" ControlExtender="meStartDate"
                                                                        ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                                                        InvalidValueMessage=" Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                        TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                        ValidationGroup="Store" SetFocusOnError="True" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-md-10" runat="server" id="spanNature">
                                                                <label><span style="color: #FF0000">*</span>Nature:</label>

                                                                <asp:TextBox ID="txtNature" runat="server" CssClass="form-control" MaxLength="100"
                                                                    onKeyUp="LovercaseToUppercase(this);" TabIndex="5" ToolTip="Enter Nature"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtNature" runat="server" ControlToValidate="txtNature"
                                                                    Display="None" ErrorMessage="Please Enter Nature Name" ValidationGroup="store"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-md-10" id="spanCordinator" runat="server">
                                                                <label><span style="color: #FF0000">*</span>Co-ordinator: </label>
                                                                <asp:TextBox ID="txtCoordinator" runat="server" CssClass="form-control" onKeyUp="LovercaseToUppercase(this);"
                                                                    MaxLength="50" TabIndex="7" ToolTip="Enter Co-ordinator"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtCoordinator" runat="server" ControlToValidate="txtCoordinator"
                                                                    Display="None" ErrorMessage="Please Enter Co-ordinator Name" ValidationGroup="store"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-6" runat="server" id="spanAmount">
                                                            <div class="form-group col-md-10">
                                                                <label><span style="color: #FF0000">*</span>Amount:</label>
                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" MaxLength="10" ToolTip="Enter Amount"
                                                                    TabIndex="2"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtAmount" runat="server" ControlToValidate="txtAmount"
                                                                    Display="None" ErrorMessage="Please Enter Proper Amount " ValidationGroup="store"
                                                                    SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="amtrange" runat="server" ControlToValidate="txtAmount" Type="Integer"
                                                                    MinimumValue="0" MaximumValue="999999999" ErrorMessage="Please Enter amount in the range 0-999999999"
                                                                    Display="None" ValidationGroup="store" SetFocusOnError="true">
                                                                </asp:RangeValidator>
                                                                <asp:CompareValidator ID="cmptxtAmount" ControlToValidate="txtAmount" runat="server"
                                                                    ErrorMessage="Please Insert Proper Amount Format" Operator="DataTypeCheck" Type="Double"
                                                                    ValidationGroup="store" Display="None"></asp:CompareValidator>
                                                            </div>
                                                            <div class="form-group col-md-10" id="spanEDate" runat="server">
                                                                <label><span style="color: #FF0000">*</span>End Date:</label>
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtEndDate" runat="server" Style="z-index: 0" CssClass="form-control" TabIndex="4" ToolTip="Enter End Date" MaxLength="18" />
                                                                    <div class="input-group-addon">
                                                                        <asp:Image ID="imgCalEndDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                            Style="cursor: pointer" />
                                                                    </div>

                                                                    <ajaxToolKit:CalendarExtender ID="ceEndDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                                                                        PopupButtonID="imgCalEndDate" Enabled="true" EnableViewState="true">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                                                        Display="None" ErrorMessage="Please Select End Date in (dd/MM/yyyy Format)" ValidationGroup="store"
                                                                        SetFocusOnError="True">
                                                                    </asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:MaskedEditExtender ID="meEndDate" runat="server" TargetControlID="txtEndDate"
                                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                        AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                                    </ajaxToolKit:MaskedEditExtender>
                                                                    <asp:CompareValidator ID="cmpValidator" runat="server" ControlToCompare="txtStartDate"
                                                                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="To Date should be greater than equal to From Date"
                                                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="StoreSearch"></asp:CompareValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-md-10" id="spanScheme" runat="server">
                                                                <label><span style="color: #FF0000">*</span>Scheme:</label>
                                                                <asp:TextBox ID="txtScheme" runat="server" CssClass="form-control" MaxLength="50"
                                                                    onKeyUp="LovercaseToUppercase(this);" TabIndex="6" ToolTip="Enter Scheme"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtScheme" runat="server" ControlToValidate="txtScheme"
                                                                    Display="None" ErrorMessage="Please Enter Scheme Name" ValidationGroup="store"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-md-10">
                                                                <label></label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:Panel>
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="butSubmit" runat="server" OnClick="butSubmit_Click" TabIndex="8"
                                                        Text="Submit" ValidationGroup="store" CssClass="btn btn-primary" ToolTip="Click To Submit" />
                                                    <asp:Button ID="butCancel" runat="server" OnClick="butCancel_Click" TabIndex="9"
                                                        Text="Cancel" CssClass="btn btn-warning" ToolTip="Click To Reset" />
                                                    <asp:Button ID="btnshowrpt" runat="server" TabIndex="10" Text="Report" CssClass="btn btn-info" ToolTip="Click To Show Report" />

                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="store" />
                                                </div>
                                            </div>
                                        </div>

                                        <asp:Panel ID="pnlBudHead" runat="server">
                                            <div class="col-md-12 table-responsive">
                                                <asp:ListView ID="lvBudHead" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <center>
                                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                            </center>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <div class="titlebar">
                                                                <h4>Budget Head Details</h4>
                                                            </div>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <tr class="bg-light-blue">
                                                                    <th>Action
                                                                    </th>
                                                                    <th>Head Name
                                                                    </th>
                                                                    <th>Amount
                                                                    </th>
                                                                    <th>SDATE
                                                                    </th>
                                                                    <th>Edate
                                                                    </th>
                                                                    <th>NATURE
                                                                    </th>
                                                                    <th>SCHEME
                                                                    </th>
                                                                    <th>COORDINATOR
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("BHALNO") %>'
                                                                    ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" TabIndex="9" ToolTip="Edit Record" />
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <%# Eval("BHNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BAMT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BUDFSDATE", "{0:dd/MM/yyyy}")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BUDFEDATE", "{0:dd/MM/yyyy}")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BNATURE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SCHEME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BCOORDINATOR")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>
                                                <div class="vista-grid_datapager text-center">
                                                    <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvBudHead"
                                                        PageSize="10">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="&lt;&lt;" PreviousPageText="&lt;"
                                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowLastPageButton="false"
                                                                ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                                            <asp:NumericPagerField ButtonCount="7" ButtonType="Link" CurrentPageLabelCssClass="current" />
                                                            <asp:NextPreviousPagerField ButtonType="Link" LastPageText="&gt;&gt;" NextPageText="&gt;"
                                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowLastPageButton="true"
                                                                ShowNextPageButton="true" ShowPreviousPageButton="false" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>
                                            </div>

                                        </asp:Panel>
                                </div>

                            </div>
                        </form>
                    </div>

                </div>


            </div>




            <table cellpadding="0" cellspacing="0" width="100%">
                <%--<tr>
                    <td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x; border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;"
                        colspan="6">BUDGET ALOCATION MASTER
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                    </td>
                </tr>--%>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="width: 97%; padding-left: 10px;">
                            <%--  <fieldset class="fieldset">
                                <legend class="legend">Add/Edit Budget Allocation</legend>--%>
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <%--<tr>
                                        <td colspan="3">
                                            <div style="color: Red; font-weight: bold">
                                                &nbsp;Note : * marked field is Mandatory
                                            </div>
                                            <br />
                                        </td>
                                    </tr>--%>
                                <tr>
                                    <td class="form_left_label" style="padding-left: 10px; width: 100px">
                                        <%--<span id="spanName" style="color: #FF0000">*</span> Budget Name
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlBudgetHead" runat="server" CssClass="dropdownlist" AppendDataBoundItems="true"
                                            Width="155px" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvtxtddlBudgetHead" runat="server" ControlToValidate="ddlBudgetHead"
                                            Display="None" ErrorMessage="Please select Budget Name" ValidationGroup="store"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td class="form_left_label" style="padding-left: 10px; width: 100px" >
                                    Start Date :
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtStartDate" runat="server" Width="149px"
                                        ReadOnly="True" Enabled="False" />
                                </td>--%>
                                    <td class="form_left_label" style="padding-left: 10px">
                                        <%--<span id="spanSDate" style="color: #FF0000">*</span> Start Date
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="texbox" Width="125px" MaxLength="18" />
                                        &nbsp;<asp:Image ID="imgCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                            Style="cursor: pointer" />
                                        <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtStartDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                            Display="None" ErrorMessage="Please Select Start Date in (dd/MM/yyyy Format)"
                                            ValidationGroup="store" SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:MaskedEditExtender ID="meStartDate" runat="server" TargetControlID="txtStartDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="mevtxtExpiryDate" runat="server" ControlExtender="meStartDate"
                                            ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                            InvalidValueMessage=" Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Store" SetFocusOnError="True" />--%>
                                    </td>
                                    <%-- <td class="form_left_label" style="padding-right : 20px; width: 100px">
                                    End Date :
                                </td>
                                <td class="form_left_text" style="padding-right : 20px;">
                                    <asp:TextBox ID="txtEndDate" runat="server" Width="149px" 
                                        ReadOnly="True" Enabled="False" />
                                </td>--%>
                                    <td class="form_left_label" style="padding-left: 10px">
                                        <%--<span id="spanEDate" style="color: #FF0000">*</span> End Date
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="texbox" Width="125px" MaxLength="18" />
                                        &nbsp;<asp:Image ID="imgCalEndDate" runat="server" ImageUrl="~/images/calendar.png"
                                            Style="cursor: pointer" />
                                        <ajaxToolKit:CalendarExtender ID="ceEndDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                                            PopupButtonID="imgCalEndDate" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                            Display="None" ErrorMessage="Please Select End Date in (dd/MM/yyyy Format)" ValidationGroup="store"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:MaskedEditExtender ID="meEndDate" runat="server" TargetControlID="txtEndDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                        </ajaxToolKit:MaskedEditExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="padding-left: 10px; width: 100px">
                                        <%--<span id="spanAmount" style="color: #FF0000">*</span> Amount
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="texbox" Width="149px" MaxLength="10"
                                            TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAmount" runat="server" ControlToValidate="txtAmount"
                                            Display="None" ErrorMessage="Please Enter Proper Amount " ValidationGroup="store"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="amtrange" runat="server" ControlToValidate="txtAmount" Type="Integer"
                                            MinimumValue="0" MaximumValue="999999999" ErrorMessage="Please Enter amount in the range 0-999999999"
                                            Display="None" ValidationGroup="store" SetFocusOnError="true">
                                        </asp:RangeValidator>
                                        <asp:CompareValidator ID="cmptxtAmount" ControlToValidate="txtAmount" runat="server"
                                            ErrorMessage="Please Insert Proper Amount Format" Operator="DataTypeCheck" Type="Double"
                                            ValidationGroup="store" Display="None"></asp:CompareValidator>--%>
                                    </td>
                                    <td class="form_left_label" style="padding-right: 20px; width: 100px">
                                        <%--<span id="spanScheme" style="color: #FF0000">*</span> Scheme
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtScheme" runat="server" CssClass="texbox" Width="149px" MaxLength="50"
                                            onKeyUp="LovercaseToUppercase(this);" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtScheme" runat="server" ControlToValidate="txtScheme"
                                            Display="None" ErrorMessage="Please Enter Scheme Name" ValidationGroup="store"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="padding-left: 10px; width: 100px">
                                        <%-- <span id="spanNature" style="color: #FF0000">*</span> Nature
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtNature" runat="server" CssClass="texbox" Width="149px" MaxLength="100"
                                            onKeyUp="LovercaseToUppercase(this);" TabIndex="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtNature" runat="server" ControlToValidate="txtNature"
                                            Display="None" ErrorMessage="Please Enter Nature Name" ValidationGroup="store"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td class="form_left_label" style="padding-right: 20px; width: 100px">
                                        <%--<span id="spanCordinator" style="color: #FF0000">*</span> Co-ordinator
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtCoordinator" runat="server" CssClass="texbox" Width="149px" onKeyUp="LovercaseToUppercase(this);"
                                            MaxLength="50" TabIndex="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCoordinator" runat="server" ControlToValidate="txtCoordinator"
                                            Display="None" ErrorMessage="Please Enter Co-ordinator Name" ValidationGroup="store"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <tr>
                                        <td class="form_left_text">&nbsp;
                                        </td>
                                        <td class="form_left_text">&nbsp;
                                        </td>
                                    </tr>
                                </tr>
                                <caption>
                                    <br />
                                    <tr>
                                        <td class="form_left_label "></td>
                                        <td class="form_left_text" colspan="2">
                                            <%-- <asp:Button ID="butSubmit" runat="server" OnClick="butSubmit_Click" TabIndex="6"
                                                Text="Submit" ValidationGroup="store" Width="70px" />
                                            &nbsp;<asp:Button ID="butCancel" runat="server" OnClick="butCancel_Click" TabIndex="7"
                                                Text="Cancel" Width="70px" />
                                            &nbsp;<asp:Button ID="btnshowrpt" runat="server" TabIndex="8" Text="Report" Width="70px" />
                                            <br />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="store" />--%>
                                        </td>
                                    </tr>
                                </caption>
                            </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <%--  </table>
                    </fieldset>
                </div>
            </td>
        </tr>--%>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding-left: 10px; padding-right: 10px;"></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function LovercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }

    </script>

</asp:Content>
