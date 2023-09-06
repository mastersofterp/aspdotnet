<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_MonInstallmentEntry.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_MonInstallmentEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">MONTHLY INSTALLMENT AMOUNT ENTRY</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">Select Criteria</div>
                                <div class="panel-body">
                                    <asp:Panel ID="pnlSelection" runat="server">
                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-6">
                                                <div class="form-group col-md-10">
                                                    <label>Order By:</label>
                                                    <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" TabIndex="1"
                                                        ToolTip="Select Order By"
                                                        runat="server" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                                        <asp:ListItem Value="2">Name</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>College Name :<span style="color: Red">*</span></label>


                                                    <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2"
                                                        ToolTip="Select College Name"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                        >
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select College Name"
                                                        ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Employee Name :<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3"
                                                        ToolTip="Select Employee Name"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                                        ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:CheckBox ID="chkStopInstall" runat="server" AutoPostBack="true" OnCheckedChanged="chkStopInstall_CheckedChanged"
                                                        ValidationGroup="ADD" Text="Show stop InstallMent Entry" />
                                                </div>

                                            </div>
                                            <div class="form-group col-md-6">
                                            </div>
                                        </div>

                                    </asp:Panel>


                                    <asp:Panel ID="pnlEmpDetails" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Employee Details</div>
                                            <div class="panel-body">

                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-12">
                                                        <h5>ID No & Name :
                                                        <asp:Label ID="lblIdnoName" runat="server" Font-Bold="true" Text="101-Sanjay"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <h5>Designation :
                                                        <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" Text="SoftWare Developer"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <h5>Department :
                                                         <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" Text=".NET"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnldeducationentry" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Select Pay Head For Deducation</div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <div class="form-group col-md-10">
                                                            <label>Pay Head :<span style="color: Red">*</span></label>

                                                            <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select Pay Head"
                                                                 AutoPostBack="true" OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select Payhead</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlPayhead" runat="server" ControlToValidate="ddlPayhead"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Payhead" ValidationGroup="payroll"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                            <asp:CheckBox ID="chkStop" CssClass="legendPay" runat="server" Text="Stop" />
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Installment For Sub Payhead :</label>

                                                            <asp:DropDownList ID="ddlSubpayhead" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Select Installment For Sub Payhead">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Monthly Deducted Amt :<span style="color: Red">*</span></label>
                                                            <asp:TextBox ID="txtMonthlyDedAmt" runat="server" MaxLength="9" CssClass="form-control" TabIndex="3" ToolTip="Enter Monthly Deducted Amt" onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rvfMonthlyDedAmt" runat="server" ControlToValidate="txtMonthlyDedAmt"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Monthly Deducted Amt"
                                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Total No.of Inst :<span style="color: Red">*</span></label>
                                                            <asp:TextBox ID="txtNoofInstallMent" runat="server" MaxLength="4" CssClass="form-control" TabIndex="4" ToolTip="Enter Total No.of Inst" onkeyup="return totalamount(this)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvNoofInstallMent" runat="server" ControlToValidate="txtNoofInstallMent"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter  No.of Installment "
                                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Total Amount :</label>
                                                            <asp:TextBox ID="txtTotalAmount" runat="server" MaxLength="9" CssClass="form-control" TabIndex="5" ToolTip="Enter Total Amount"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Total No.of Paid.Inst:</label>
                                                            <asp:TextBox ID="txtNoofInstPaid" runat="server" MaxLength="6" CssClass="form-control" TabIndex="6" ToolTip="Enter  Total No.of Paid.Inst:"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Outstanding Amt :</label>
                                                            <asp:TextBox ID="txtOutStandingAmt" runat="server" MaxLength="6"
                                                                CssClass="form-control" TabIndex="7" ToolTip="Enter Outstanding Amt"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Bank :<span style="color: Red">*</span></label>
                                                            <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Select Bank">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlBank" runat="server" ControlToValidate="ddlBank"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Bank" ValidationGroup="payroll"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Bank Place :<span style="color: Red">*</span></label>

                                                            <asp:DropDownList ID="ddlBankPlace" AppendDataBoundItems="true" runat="server" ToolTip="Select Bank Place" CssClass="form-control" TabIndex="8">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvBankPlace" runat="server" ControlToValidate="ddlBankPlace"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Bank Place" ValidationGroup="payroll"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10" id="tdPolicyno" visible="false" runat="server">
                                                            <label>Policy.No :</label>
                                                        </div>
                                                        <div class="form-group col-md-10" id="tdRdNo" visible="false" runat="server">
                                                            <label>Rd.No :</label>
                                                        </div>

                                                        <div class="form-group col-md-10" id="tdAccountNO" runat="server">
                                                            <label>Account No:<span style="color: Red">*</span></label>
                                                            <asp:TextBox ID="txtAccNo" runat="server" MaxLength="20" CssClass="form-control" TabIndex="9" ToolTip="Enter Account No"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rvfAccNo" runat="server" ControlToValidate="txtAccNo"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Account No "
                                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbAccNo" runat="server"
                                                                TargetControlID="txtAccNo"
                                                                FilterType="Custom,Numbers"
                                                                FilterMode="ValidChars"
                                                                ValidChars="">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Installment Drawn Date :<span style="color: Red">*</span></label>

                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtInstallmentDrawnDate" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Enter Installment Drawn Date"></asp:TextBox>
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="ImgInstallmentDrawnDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <ajaxToolKit:CalendarExtender ID="cetxtInstallmentDrawnDate" runat="server" Enabled="true"
                                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImgInstallmentDrawnDate" TargetControlID="txtInstallmentDrawnDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvtxtInstallmentDrawnDate" runat="server" ControlToValidate="txtStartDate"
                                                                    Display="None" ErrorMessage="Please select Installment Drawn Date in (dd/MM/yyyy Format)"
                                                                    SetFocusOnError="True" ValidationGroup="payroll">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="metxtInstallmentDrawnDate" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" TargetControlID="txtInstallmentDrawnDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevtxtInstallmentDrawnDate" runat="server" ControlExtender="metxtInstallmentDrawnDate"
                                                                    ControlToValidate="txtInstallmentDrawnDate" Display="None" EmptyValueBlurredText="Empty"
                                                                    EmptyValueMessage="Please select Installment Drawn Date" InvalidValueBlurredMessage="Invalid Date"
                                                                    InvalidValueMessage="Installment Drawn Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                    SetFocusOnError="True" TooltipMessage="Please select Installment Drawn Date"
                                                                    ValidationGroup="payroll" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Start Date :<span style="color: Red">*</span></label>

                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TabIndex="11" ToolTip="Enter Start Date" OnTextChanged="txtStartDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                        Style="cursor: pointer" />
                                                                </div>
                                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtStartDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtStartDate"
                                                                    Display="None" ErrorMessage="Please Select Start Date in (dd/MM/yyyy Format)"
                                                                    SetFocusOnError="True" ValidationGroup="payroll">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="metxtStartDate" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" TargetControlID="txtStartDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevtxtStartDate" runat="server" ControlExtender="metxtStartDate"
                                                                    ControlToValidate="txtStartDate" Display="None" EmptyValueBlurredText="Empty"
                                                                    EmptyValueMessage="Please Enter Start Date" InvalidValueBlurredMessage="Invalid Date"
                                                                    InvalidValueMessage=" Start Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                                                                    TooltipMessage="Please Enter Inc.Date" ValidationGroup="payroll" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Expiry Date :<span style="color: Red">*</span></label>

                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" TabIndex="12" ToolTip="Enter Expiry Date" />
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgCalExpiryDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                        Style="cursor: pointer" />
                                                                </div>
                                                                <ajaxToolKit:CalendarExtender ID="cetxtExpiryDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtExpiryDate" PopupButtonID="imgCalExpiryDate" Enabled="true"
                                                                    EnableViewState="true">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvtxtExpiryDate" runat="server" ControlToValidate="txtExpiryDate"
                                                                    Display="None" ErrorMessage="Please Select Expiry Date in (dd/MM/yyyy Format)"
                                                                    ValidationGroup="payroll" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="metxtExpiryDate" runat="server" TargetControlID="txtExpiryDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevtxtExpiryDate" runat="server" ControlExtender="metxtExpiryDate"
                                                                    ControlToValidate="txtExpiryDate" EmptyValueMessage="Please Enter Expiry Date"
                                                                    InvalidValueMessage=" Expiry Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                    TooltipMessage="Please Enter Inc.Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                    ValidationGroup="payroll" SetFocusOnError="True" />
                                                                <asp:CompareValidator ID="cmpVal1" ControlToCompare="txtStartDate"
                                                                    ControlToValidate="txtExpiryDate" Type="Date" Operator="GreaterThanEqual"
                                                                    ErrorMessage="Start Date Should be Less than  Expiry Date." ValidationGroup="payroll" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>
                                                                Remarks :            
                                                            </label>
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Enter Remarks" TextMode="MultiLine" />
                                                        </div>

                                                    </div>
                                                    <div class="form-group col-md-6">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="butSubmit" runat="server" OnClick="btnSave_Click" Text="Submit" ValidationGroup="payroll" TabIndex="13" ToolTip="Click To Save"
                                                CssClass="btn btn-success" />
                                            <asp:Button ID="butCancel" runat="server" OnClick="butCancel_Click" Text="Cancel"
                                                CssClass="btn btn-danger" TabIndex="14" ToolTip="Click To Reset" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="payroll" />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <asp:Panel ID="pnlList" runat="server">
                                <div class="text-center">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" ToolTip="Click Add New To Enter Installment Details" CssClass="btn btn-primary" Text="Add New" TabIndex="4"
                                        ValidationGroup="ADD" OnClick="btnAdd_Click"></asp:LinkButton>
                                    <asp:ValidationSummary ID="Vadd" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="ADD" />


                                </div>
                                <div class="table-responsive">
                                    <asp:ListView ID="lvMonthlyIstallment" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demo-grid" class="vista-grid">
                                                <div class="titlebar">
                                                    <h4>Monthly InstallMent Amount Entry</h4>
                                                </div>
                                                <table class="table table-bordered table-hover table-responsive">
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>PayHead
                                                        </th>
                                                        <th>Monthly Amt
                                                        </th>
                                                        <th>No.of Inst
                                                        </th>
                                                        <th>Tot.Amt
                                                        </th>
                                                        <th>Outstand Amt
                                                        </th>
                                                        <th>Expiry Date
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("INO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click"/>&nbsp;
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("INO") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("PAYSHORT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MONAMT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("INSTALNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAMT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BAL_AMT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EXPDT", "{0:dd/MM/yyyy}")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                    <div class="vista-grid_datapager text-center">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvMonthlyIstallment" PageSize="10" OnPreRender="dpPager_PreRender"
                                            >
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
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
            <td class="vista_page_title_bar" valign="top" style="height: 30px">MONTHLY INSTALLMENT AMOUNT ENTRY&nbsp;
                        <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
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


                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                    <Animations>
                              <OnClick>
                                    <Sequence AnimationTarget="info">
                                    <%--  Shrink the info panel out of view --%>
                                    <StyleAction Attribute="overflow" Value="hidden"/>
                                    <Parallel Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                    </Parallel>

                                    <%--  Reset the sample so it can be played again --%>
                                    <StyleAction Attribute="display" Value="none"/>
                                    <StyleAction Attribute="width" Value="250px"/>
                                    <StyleAction Attribute="height" Value=""/>
                                    <StyleAction Attribute="fontSize" Value="12px"/>
                                    <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />

                                    <%--  Enable the button so it can be played again --%>
                                    <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                                    </Sequence>
                                </OnClick>
                                <OnMouseOver>
                                <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                                </OnMouseOver>
                                <OnMouseOut>
                                <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                                </OnMouseOut>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
            </td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                <%--<asp:Panel ID="pnlSelection" runat="server" Style="text-align: left; width: 90%; padding-left: 15px;">--%>
                <fieldset class="fieldsetPay">
                    <legend class="legendPay"></legend>
                    <br>
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label" style="padding-left: 15px"><%--Order By:
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" AutoPostBack="true"
                                        runat="server" Width="100px" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                        <asp:ListItem Value="2">Name</asp:ListItem>
                                    </asp:DropDownList>--%>
                            </td>
                        </tr>

                        <tr>
                            <td class="form_left_label" style="padding-left: 15px"><%--College Name :<span style="color: Red">*</span>
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" Width="300px"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select College Name"
                                        ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>

                        <tr>
                            <td class="form_left_label" style="padding-left: 15px"><%--Employee Name :<span style="color: Red">*</span>
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" Width="300px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                        ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:CheckBox ID="chkStopInstall" runat="server" AutoPostBack="true" OnCheckedChanged="chkStopInstall_CheckedChanged"
                                        ValidationGroup="ADD" Text="Show stop InstallMent Entry" />--%>
                            </td>
                        </tr>
                    </table>
                    <br />
                </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:Panel ID="pnlEmpDetails" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Employee Details</legend>
                        <br>
                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td class="form_left_label" style="padding-left: 15px">IdNo& Name:
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblIdnoName" runat="server" Font-Bold="true" Text="101-Sanjay"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" style="padding-left: 15px">Designation :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" Text="SoftWare Developer"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" style="padding-left: 15px">Department :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" Text=".NET"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                &nbsp
                                            <td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>--%>
            </td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:Panel ID="pnldeducationentry" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">--%>
                <%--<fieldset class="fieldsetPay">
                        <legend class="legendPay">Select Pay Head For Deducation</legend>--%>
                <br>
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td class="form_left_label" style="padding-left: 15px"><%--Pay Head :<span style="color: Red">*</span>
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" Width="200px"
                                        OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select Payhead</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlPayhead" runat="server" ControlToValidate="ddlPayhead"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Payhead" ValidationGroup="payroll"
                                        InitialValue="0"></asp:RequiredFieldValidator>--%>
                            <%-- <ajaxToolkit:CascadingDropDown ID="cddsupayhead" runat="server" TargetControlID="ddlSubpayhead"
                                             Category="Employee" PromptText="Please Select"   LoadingText="[Loading...]" ServicePath="Pay_Installment.asmx"
                                             ServiceMethod="fillsubpayheads" ParentControlID="ddlPayhead" />  --%>
                                
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 15px;" width="30%"><%--Installment For Sub Payhead :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSubpayhead" AppendDataBoundItems="true" runat="server" Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
                            <%-- <asp:RequiredFieldValidator ID="rfvddlSubpayhead" runat="server" ControlToValidate="ddlSubpayhead"
                                                Display="None" ErrorMessage="Please Select Installment For Sub Payhead" ValidationGroup="Payroll"
                                                InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="padding-left: 15px"><%--Monthly Deducted Amt :<span style="color: Red">*</span>
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtMonthlyDedAmt" runat="server" MaxLength="9" Width="195px" onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfMonthlyDedAmt" runat="server" ControlToValidate="txtMonthlyDedAmt"
                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Monthly Deducted Amt"
                                    ValidationGroup="payroll"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="padding-left: 15px"><%--Total No.of Inst :<span style="color: Red">*</span>
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtNoofInstallMent" runat="server" MaxLength="4" Width="195px" onkeyup="return totalamount(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNoofInstallMent" runat="server" ControlToValidate="txtNoofInstallMent"
                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Enter  No.of Installment "
                                    ValidationGroup="payroll"></asp:RequiredFieldValidator>--%>

                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="padding-left: 15px"><%--Total Amount :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtTotalAmount" runat="server" MaxLength="9" Width="195px"></asp:TextBox>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="padding-left: 15px"><%--Total No.of Paid.Inst:
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtNoofInstPaid" runat="server" MaxLength="6" Width="195px"></asp:TextBox>--%>
                        </td>
                        <tr>
                            <td class="form_left_label" style="padding-left: 15px"><%--Outstanding Amt :
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtOutStandingAmt" runat="server" MaxLength="6"
                                        Width="195px"></asp:TextBox>--%>
                            </td>
                        </tr>
                    <tr>
                        <td class="form_left_label" style="padding-left: 15px"><%--Bank :<span style="color: Red">*</span>
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" runat="server" Width="200px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlBank" runat="server" ControlToValidate="ddlBank"
                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Bank" ValidationGroup="payroll"
                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="padding-left: 15px"><%--Bank Place :<span style="color: Red">*</span>
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlBankPlace" AppendDataBoundItems="true" runat="server" Width="200px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBankPlace" runat="server" ControlToValidate="ddlBankPlace"
                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Bank Place" ValidationGroup="payroll"
                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <%-- 
                                           Depends on the payhead selection value of  
                        --%>
                        <%-- <td class="form_left_label" id="tdPolicyno" visible="false" runat="server" style="padding-left: 15px;">Policy.No :
                            </td>
                            <td class="form_left_label" id="tdRdNo" visible="false" runat="server" style="padding-left: 15px;">Rd.No :--%>
            </td>
            <td class="form_left_label" style="padding-left: 15px"><%--Account No:<span style="color: Red">*</span>
            </td>
            <td class="form_left_text">
                <asp:TextBox ID="txtAccNo" runat="server" MaxLength="20" Width="195px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvfAccNo" runat="server" ControlToValidate="txtAccNo"
                    Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Account No "
                    ValidationGroup="payroll"></asp:RequiredFieldValidator>
                <ajaxToolKit:FilteredTextBoxExtender ID="ftbAccNo" runat="server"
                    TargetControlID="txtAccNo"
                    FilterType="Custom,Numbers"
                    FilterMode="ValidChars"
                    ValidChars="">
                </ajaxToolKit:FilteredTextBoxExtender>--%>
            </td>
        </tr>
        <tr>
            <td class="form_left_label" style="padding-left: 15px"><%--Installment Drawn Date :<span style="color: Red">*</span>
            </td>
            <td class="form_left_text">
                <asp:TextBox ID="txtInstallmentDrawnDate" runat="server" Width="100px"></asp:TextBox>
                &nbsp;<asp:Image ID="ImgInstallmentDrawnDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                <ajaxToolKit:CalendarExtender ID="cetxtInstallmentDrawnDate" runat="server" Enabled="true"
                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImgInstallmentDrawnDate" TargetControlID="txtInstallmentDrawnDate">
                </ajaxToolKit:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvtxtInstallmentDrawnDate" runat="server" ControlToValidate="txtStartDate"
                    Display="None" ErrorMessage="Please select Installment Drawn Date in (dd/MM/yyyy Format)"
                    SetFocusOnError="True" ValidationGroup="payroll">
                </asp:RequiredFieldValidator>
                <ajaxToolKit:MaskedEditExtender ID="metxtInstallmentDrawnDate" runat="server" AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                    MessageValidatorTip="true" TargetControlID="txtInstallmentDrawnDate">
                </ajaxToolKit:MaskedEditExtender>
                <ajaxToolKit:MaskedEditValidator ID="mevtxtInstallmentDrawnDate" runat="server" ControlExtender="metxtInstallmentDrawnDate"
                    ControlToValidate="txtInstallmentDrawnDate" Display="None" EmptyValueBlurredText="Empty"
                    EmptyValueMessage="Please select Installment Drawn Date" InvalidValueBlurredMessage="Invalid Date"
                    InvalidValueMessage="Installment Drawn Date is Invalid (Enter dd/MM/yyyy Format)"
                    SetFocusOnError="True" TooltipMessage="Please select Installment Drawn Date"
                    ValidationGroup="payroll" />
            </td>--%>
        </tr>
        <tr>
            <td class="form_left_label" style="padding-left: 15px"><%--Start Date :<span style="color: Red">*</span>
            </td>
            <td class="form_left_text">
                <asp:TextBox ID="txtStartDate" runat="server" Width="100px" OnTextChanged="txtStartDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                &nbsp;<asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                    Style="cursor: pointer" />
                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                    Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtStartDate">
                </ajaxToolKit:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtStartDate"
                    Display="None" ErrorMessage="Please Select Start Date in (dd/MM/yyyy Format)"
                    SetFocusOnError="True" ValidationGroup="payroll">
                </asp:RequiredFieldValidator>
                <ajaxToolKit:MaskedEditExtender ID="metxtStartDate" runat="server" AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                    MessageValidatorTip="true" TargetControlID="txtStartDate">
                </ajaxToolKit:MaskedEditExtender>
                <ajaxToolKit:MaskedEditValidator ID="mevtxtStartDate" runat="server" ControlExtender="metxtStartDate"
                    ControlToValidate="txtStartDate" Display="None" EmptyValueBlurredText="Empty"
                    EmptyValueMessage="Please Enter Start Date" InvalidValueBlurredMessage="Invalid Date"
                    InvalidValueMessage=" Start Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                    TooltipMessage="Please Enter Inc.Date" ValidationGroup="payroll" />--%>
            </td>
        </tr>
        <tr>
            <td class="form_left_label" style="padding-left: 15px"><%--Expiry Date :<span style="color: Red">*</span>
            </td>
            <td class="form_left_text">
                <asp:TextBox ID="txtExpiryDate" runat="server" Width="100px" />
                &nbsp;<asp:Image ID="imgCalExpiryDate" runat="server" ImageUrl="~/images/calendar.png"
                    Style="cursor: pointer" />
                <ajaxToolKit:CalendarExtender ID="cetxtExpiryDate" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtExpiryDate" PopupButtonID="imgCalExpiryDate" Enabled="true"
                    EnableViewState="true">
                </ajaxToolKit:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvtxtExpiryDate" runat="server" ControlToValidate="txtExpiryDate"
                    Display="None" ErrorMessage="Please Select Expiry Date in (dd/MM/yyyy Format)"
                    ValidationGroup="payroll" SetFocusOnError="True">
                </asp:RequiredFieldValidator>
                <ajaxToolKit:MaskedEditExtender ID="metxtExpiryDate" runat="server" TargetControlID="txtExpiryDate"
                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                </ajaxToolKit:MaskedEditExtender>
                <ajaxToolKit:MaskedEditValidator ID="mevtxtExpiryDate" runat="server" ControlExtender="metxtExpiryDate"
                    ControlToValidate="txtExpiryDate" EmptyValueMessage="Please Enter Expiry Date"
                    InvalidValueMessage=" Expiry Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                    TooltipMessage="Please Enter Inc.Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                    ValidationGroup="payroll" SetFocusOnError="True" />
                <asp:CompareValidator ID="cmpVal1" ControlToCompare="txtStartDate"
                    ControlToValidate="txtExpiryDate" Type="Date" Operator="GreaterThanEqual"
                    ErrorMessage="Start Date Should be Less than  Expiry Date." ValidationGroup="payroll" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>
            --%><%-- <asp:CheckBox ID="chkRegularDed" runat="server" Text="Regular Deducation" />--%>
            </td>
        </tr>
        <tr>
            <td class="form_left_label" style="padding-left: 15px"><%--Remarks :
            </td>
            <td class="form_left_text" colspan="2">
                <asp:TextBox ID="txtRemarks" runat="server" Width="500px" TextMode="MultiLine" />--%>
            </td>
            <tr>
                <td align="center" colspan="3">
                    <%--<asp:Button ID="butSubmit" runat="server" OnClick="btnSave_Click" Text="Submit" ValidationGroup="payroll"
                        Width="80px" />
                    &nbsp;
                                                    <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                        Width="80px" />
                    &nbsp;
                                                    <%--  <asp:Button ID="butPrint" runat="server" Text="Print" ValidationGroup="payroll" Width="80px"  />&nbsp;
                    <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="payroll" />--%>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                                                    <td></td>
                </td>
            </tr>
        </tr>
    </table>
    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left: 35px">
                <%--<asp:Panel ID="pnlList" runat="server" Width="95%">--%>
                    <table cellpadding="0" cellspacing="0" style="width: 97%; text-align: center">
                        <tr>
                            <td style="text-align: left; padding-left: 50px; padding-top: 10px;">
                                <%--<asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" ToolTip="Add New"
                                    ValidationGroup="ADD" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                                <asp:ValidationSummary ID="Vadd" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ADD" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="center"></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>

    <script type="text/javascript">

        //Calculating the total amount
        function totalamount(val) {
            if (ValidateNumeric(val)) {
                var txtMonthlyDedAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtMonthlyDedAmt");
                var txtTotalAmount = document.getElementById("ctl00_ContentPlaceHolder1_txtTotalAmount");
                var txtOutStandingAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtOutStandingAmt");
                var txtNoofInstPaid = document.getElementById("ctl00_ContentPlaceHolder1_txtNoofInstPaid");
                txtTotalAmount.value = Number(val.value) * Number(txtMonthlyDedAmt.value);
                txtNoofInstPaid.value = 0;
                txtOutStandingAmt.value = txtTotalAmount.value;
            }
        }


        function ValidateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters alloewd");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


