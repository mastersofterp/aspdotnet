<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_LOAN_ENTRY.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PF LOAN ENTRY</h3>
                            <%-- <div class="box-tools pull-right">
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />
                            </div>--%>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <%-- Panel for Selection--%>
                                    <asp:Panel ID="pnlSelection" runat="server">
                                        <div class="panel panel-info" style="width: auto">
                                            <div class="panel-heading">Select Criteria</div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <label>Order By&nbsp; :</label>
                                                        <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged" ToolTip="Select Order By" TabIndex="1">
                                                            <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                                            <asp:ListItem Value="2">Name</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                    <label>College :</label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2"  OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Employee Name&nbsp; :</label>
                                                        <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" ToolTip="Select Employee Name">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                                            ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <%--Panel for Employee Details--%>
                                    <asp:Panel ID="pnlEmpDetails" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Employee Details</div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <%--<label class="text-bold">IdNo & Name :</label>--%>
                                                        <h5>IdNo & Name :
                                                            <asp:Label ID="lblIdnoName" runat="server" Font-Bold="true" Text="101-Sanjay"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Basic :
                                                            <asp:Label ID="lblBasic" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Date Of Appointment :                                                               
                                                            <asp:Label ID="lblDOA" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Designation :                                                           
                                                            <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" Text="SoftWare Developer"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Date Of Birth :                                                                
                                                             <asp:Label ID="lblDOB" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Department :                                                           
                                                            <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>PF.No. :                                                               
                                                            <asp:Label ID="lblPfNo" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Eligible PF Type :                                                           
                                                            <asp:Label ID="lblEligiblePFType" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <%--Panel for Loan Entry--%>
                                    <asp:Panel ID="pnlPFLoanEntry" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Enter PF Loan Details</div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <label>Loan Type :</label>
                                                        <asp:DropDownList ID="ddlLoanTakenAs" AppendDataBoundItems="true" runat="server" ToolTip="Select Loan Type" TabIndex="4"
                                                            CssClass="form-control" OnSelectedIndexChanged="ddlLoanTakenAs_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlLoanTakenAs" runat="server" ControlToValidate="ddlLoanTakenAs"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Loan Type" ValidationGroup="payroll"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Fin.Year Start Date :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" OnTextChanged="txtFromDate_TextChanged"
                                                                AutoPostBack="true" TabIndex="5" ToolTip="Enter Financial Year Start Date"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                                PopupPosition="BottomLeft">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                Display="None" ErrorMessage="Please Select Fin.Year Start Date in (dd/MM/yyyy Format)"
                                                                ValidationGroup="PF" SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                                ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Fin.Year Start Date"
                                                                InvalidValueMessage="Fin.Year Start Date is Invalid (Enter mm/dd/yyyy Format)"
                                                                Display="None" TooltipMessage="Please Enter Fin.Year Start Date" EmptyValueBlurredText="Empty"
                                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="PF" SetFocusOnError="True" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Fin.Year End Date :</label>
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Enabled="false" ToolTip="Financial Year End Date"
                                                            TabIndex="6"></asp:TextBox>
                                                        <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Total Amount :</label>
                                                        <asp:TextBox ID="txtProgressiveBalance" runat="server" TabIndex="7" ToolTip="Enter Progressive Balance"
                                                            onkeyup="return ValidateNumeric(this);" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Per.Laon Balance Amt. :</label>
                                                        <asp:TextBox ID="txtPerLaonBalanceAmt" runat="server" CssClass="form-control" ToolTip="Enter Personal Laon Balance Amount"
                                                            onkeyup="return ValidateNumeric(this);" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Date :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="ImgAdvDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtAdvDate" runat="server" CssClass="form-control" TabIndex="11"
                                                                ToolTip="Enter Advance Date"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="cetxtAdvDate" runat="server" Enabled="true" EnableViewState="true"
                                                                Format="dd/MM/yyyy" PopupButtonID="ImgAdvDate" TargetControlID="txtAdvDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtAdvDate" runat="server" ControlToValidate="txtAdvDate"
                                                                Display="None" ErrorMessage="Please Select Adv.Date in (dd/MM/yyyy Format)" SetFocusOnError="True"
                                                                ValidationGroup="payroll">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="metxtAdvDate" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" TargetControlID="txtAdvDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="mevtxtAdvDate" runat="server" ControlExtender="metxtAdvDate"
                                                                ControlToValidate="txtAdvDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Adv.Date"
                                                                InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage=" Adv.Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                SetFocusOnError="True" TooltipMessage="Please Enter Adv.Date" ValidationGroup="payroll" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Sanction Percent :</label>
                                                        <asp:TextBox ID="txtsanctioper" runat="server" TabIndex="9" CssClass="form-control" ToolTip="Enter Sanction Percent"
                                                            onkeyup="return percentage(this);">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>%=</label>
                                                        <asp:TextBox ID="txtsanction" runat="server" TabIndex="10" CssClass="form-control"
                                                            Enabled="false" ToolTip="Sanction Percent Equals to"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Amount :</label>
                                                        <asp:TextBox ID="txtAdvAmt" runat="server" CssClass="form-control" ToolTip="Enter Advance Amount"
                                                            onkeyup="return AmountToBeSanctioned(this);" TabIndex="12"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAdvAmt" runat="server" ControlToValidate="txtAdvAmt"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Advance Amount"
                                                            ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <div runat="server" id="tdlblAmtToBeSanction">
                                                            <label>Amt.to be Sanction :</label>
                                                        </div>
                                                        <div runat="server" id="tdtxtAmtToBeSanction">
                                                            <asp:TextBox ID="txtAmtToBeSanction" runat="server" Enabled="false" ToolTip="Amount to be Sanction"
                                                                CssClass="form-control" TabIndex="13"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div id="trpfLRMI" runat="server">
                                                        <div class="form-group col-md-4">
                                                            <label>Loan Recoverable in (Months) :</label>
                                                            <asp:TextBox ID="txtLoanRecoverablein" runat="server" MaxLength="9" TabIndex="14" ToolTip="Enter Loan Recoverable in"
                                                                onkeyup="return LoanRecoverableIn(this);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rvLoanRecoverablein" runat="server" ControlToValidate="txtLoanRecoverablein"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Loan Recoverable in"
                                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Monthly Inst.Amt :</label>
                                                            <asp:TextBox ID="txtInsatllmentAmount" runat="server" MaxLength="9" TabIndex="15" CssClass="form-control"
                                                                onkeyup="return ValidateNumeric(this);" Enabled="false" ToolTip="Monthly Installment Amount"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtInsatllmentAmount" runat="server" ControlToValidate="txtInsatllmentAmount"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Insatllment Amount"
                                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                        </div>





                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Remark :</label>
                                                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" MaxLength="9" TabIndex="15" CssClass="form-control" ToolTip="Enter Remark"></asp:TextBox>
                                                    </div>
                                                    <br />
                                                    <div class="form-group col-md-12">
                                                        <p class="text-center text-bold">
                                                            <asp:Button ID="butSubmit" runat="server" OnClick="btnSave_Click" Text="Submit" ValidationGroup="payroll"
                                                                CssClass="btn btn-primary" TabIndex="16" ToolTip="Click here to Save" />
                                                            <asp:Button ID="butCancel" runat="server"  Text="Cancel" OnClick="butCancel_Click"
                                                                CssClass="btn btn-warning" TabIndex="17" ToolTip="Click here to reset" />
                                                            <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="payroll" />
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <%--Panel for delete entry--%>
                                    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                        <div class="form-group col-md-12">
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
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </form>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" ToolTip="Click here to Add New PF Loan Details" CssClass="btn btn-primary"
                                    ValidationGroup="ADD" Text="Add New PF Loan Details" TabIndex="3" OnClick="btnAdd_Click"></asp:LinkButton>
                                <asp:ValidationSummary ID="Vadd" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ADD" />
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                        <table class="table table-bordered table-hover table-responsive">
                                            <tr>
                                                <td>
                                                    <asp:ListView ID="lvPFLoanEntry" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <p class="text-center text-bold">
                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="Click Add New To Enter PF Loan" />
                                                            </p>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="demo-grid" class="vista-grid">
                                                                <h4 class="box-title">PF LOAN ENTRY</h4>
                                                                <table class="table table-bordered table-hover table-responsive">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Action </th>
                                                                            <th>Adv.Amt </th>
                                                                            <th>Adv.dt </th>
                                                                            <th>Per </th>
                                                                            <th>Taken As </th>
                                                                            <th>Fin.Sdate </th>
                                                                            <th>Fin.Edate </th>
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
                                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("PFLTNO") %>' ImageUrl="~/images/edit.gif"  ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                                    &nbsp;
                                                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete Record" CommandArgument='<%# Eval("PFLTNO") %>' ImageUrl="~/images/delete.gif" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" ToolTip="Delete Record" />
                                                                </td>
                                                                <td><%# Eval("ADVAMT")%></td>
                                                                <td><%# Eval("ADVDT","{0:dd/MM/yyyy}")%></td>
                                                                <td><%# Eval("PER")%></td>
                                                                <td><%# Eval("SHORTNAME")%></td>
                                                                <td><%# Eval("FDATE","{0:dd/MM/yyyy}")%></td>
                                                                <td><%# Eval("TDATE","{0:dd/MM/yyyy}")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    <div class="vista-grid_datapager">
                                                        <div class="text-center">
                                                            <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvPFLoanEntry" PageSize="10">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="&lt;&lt;" PreviousPageText="&lt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                                                    <asp:NumericPagerField ButtonCount="7" ButtonType="Link" CurrentPageLabelCssClass="current" />
                                                                    <asp:NextPreviousPagerField ButtonType="Link" LastPageText="&gt;&gt;" NextPageText="&gt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px"><%--PF LOAN ENTRY&nbsp;                      
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />--%>
                    </td>
                </tr>
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

                        <%--<ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                            <Animations>
                               <OnClick>
                                   <Sequence>
                                       
                                        <EnableAction Enabled="false" />

                                       
                                        <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>

                                        
                                        <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                        <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                        <FadeIn AnimationTarget="info" Duration=".2"/>
                                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>

                                        
                                        <Parallel AnimationTarget="info" Duration=".5">
                                        <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                        <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                        </Parallel>
                                        <Parallel AnimationTarget="info" Duration=".5">
                                        <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                        <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                        <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                                        </Parallel>
                                    </Sequence>
                                </OnClick>
                            </Animations>
                        </ajaxToolKit:AnimationExtender>
                        <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                            <Animations>
                              <OnClick>
                                    <Sequence AnimationTarget="info">
                                    
                                    <StyleAction Attribute="overflow" Value="hidden"/>
                                    <Parallel Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                    </Parallel>

                                   
                                    <StyleAction Attribute="display" Value="none"/>
                                    <StyleAction Attribute="width" Value="250px"/>
                                    <StyleAction Attribute="height" Value=""/>
                                    <StyleAction Attribute="fontSize" Value="12px"/>
                                    <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />

                                    
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
                        </ajaxToolKit:AnimationExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 20px">
                        <%--<asp:Panel ID="pnlSelection" runat="server" Style="text-align: left; width: 90%; padding-left: 15px;">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay">Select Criteria</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="form_left_text" style="padding-left: 15px">Order By:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" Width="100px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                                <asp:ListItem Value="2">Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px" class="form_left_label">Employee Name :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" Width="300px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                                ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                        </asp:Panel>--%>
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
                                        <td class="form_left_label" style="padding-left: 15px">Basic :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblBasic" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="padding-left: 15px">Date Of Appointment :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblDOA" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                        </td>
                                        <td class="form_left_label" style="padding-left: 15px">Designation :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" Text="SoftWare Developer"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="padding-left: 15px">Date Of Birth :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblDOB" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                        </td>
                                        <td class="form_left_label" style="padding-left: 15px">Department :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="padding-left: 15px">PF.No. :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblPfNo" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                        </td>
                                        <td class="form_left_label" style="padding-left: 15px">Eligible PF Type:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblEligiblePFType" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
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
                        <%-- <asp:Panel ID="pnlPFLoanEntry" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay">Enter PF Loan Details</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="form_left_label">Loan Type:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlLoanTakenAs" AppendDataBoundItems="true" runat="server"
                                                Width="150px" OnSelectedIndexChanged="ddlLoanTakenAs_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlLoanTakenAs" runat="server" ControlToValidate="ddlLoanTakenAs"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Loan Type" ValidationGroup="payroll"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Fin.Year Start Date :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" OnTextChanged="txtFromDate_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Select Fin.Year Start Date in (dd/MM/yyyy Format)"
                                                ValidationGroup="PF" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Fin.Year Start Date"
                                                InvalidValueMessage="Fin.Year Start Date is Invalid (Enter mm/dd/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter Fin.Year Start Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="PF" SetFocusOnError="True" />
                                        </td>
                                        <td class="form_left_label">Fin.Year End Date :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" Enabled="false"></asp:TextBox>--%>
                        <%--Already Committed<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                        <%--<ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Progressive Balance :
                                        </td>
                                        <td class="form_left_text" width="30%">
                                            <asp:TextBox ID="txtProgressiveBalance" runat="server" Width="80px" onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                        </td>
                                        <td class="form_left_label">Per.Laon Balance Amt. :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtPerLaonBalanceAmt" runat="server" Width="80px" onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Sanction Percent:
                                        </td>
                                        <td class="form_left_text">

                                            <asp:TextBox ID="txtsanctioper" runat="server" Width="40px" onkeyup="return percentage(this);">
                                            </asp:TextBox>%=
                                            <asp:TextBox ID="txtsanction" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="form_left_label">Adv.Date:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtAdvDate" runat="server" Width="80px"></asp:TextBox>
                                            &nbsp;<asp:Image ID="ImgAdvDate" runat="server" ImageUrl="~/images/calendar.png"
                                                Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="cetxtAdvDate" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="ImgAdvDate" TargetControlID="txtAdvDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtAdvDate" runat="server" ControlToValidate="txtAdvDate"
                                                Display="None" ErrorMessage="Please Select Adv.Date in (dd/MM/yyyy Format)" SetFocusOnError="True"
                                                ValidationGroup="payroll">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtAdvDate" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" TargetControlID="txtAdvDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevtxtAdvDate" runat="server" ControlExtender="metxtAdvDate"
                                                ControlToValidate="txtAdvDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Adv.Date"
                                                InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage=" Adv.Date is Invalid (Enter dd/MM/yyyy Format)"
                                                SetFocusOnError="True" TooltipMessage="Please Enter Adv.Date" ValidationGroup="payroll" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Advance Amt.:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtAdvAmt" runat="server" Width="80px" onkeyup="return AmountToBeSanctioned(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdvAmt" runat="server" ControlToValidate="txtAdvAmt"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Advance Amount"
                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="form_left_label" runat="server" id="tdlblAmtToBeSanction">Amt.to be Sanction :
                                        </td>
                                        <td class="form_left_text" runat="server" id="tdtxtAmtToBeSanction">
                                            <asp:TextBox ID="txtAmtToBeSanction" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trpfLRMI" runat="server">
                                        <td class="form_left_label">Loan Recoverable in :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtLoanRecoverablein" runat="server" MaxLength="9" Width="80px"
                                                onkeyup="return LoanRecoverableIn(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvLoanRecoverablein" runat="server" ControlToValidate="txtLoanRecoverablein"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Loan Recoverable in"
                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="form_left_label">Monthly Inst.Amt :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtInsatllmentAmount" runat="server" MaxLength="9" Width="80px"
                                                onkeyup="return ValidateNumeric(this);" Enabled="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtInsatllmentAmount" runat="server" ControlToValidate="txtInsatllmentAmount"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Insatllment Amount"
                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <br />
                                            <asp:Button ID="butSubmit" runat="server" OnClick="btnSave_Click" Text="Submit" ValidationGroup="payroll"
                                                Width="80px" />
                                            <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                Width="80px" />
                                            <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="payroll" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                            <td></td>
                                        </td>
                                    </tr>
                                </table>
                                </fieldset>
                            </asp:Panel>--%>
                    </td>
                </tr>
            </table>
            </fieldset>
            </asp:Panel> 
            </td> </tr>
            <tr>
                <td colspan="2" style="padding-left: 35px">
                    <%--<asp:Panel ID="pnlList" runat="server" Width="95%">
                        <table cellpadding="0" cellspacing="0" style="width: 97%; text-align: center">
                            <tr>
                                <td style="text-align: left; padding-left: 50px; padding-top: 10px;">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" ToolTip="Add New"
                                        ValidationGroup="ADD" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                                    <asp:ValidationSummary ID="Vadd" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="ADD" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:ListView ID="lvPFLoanEntry" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter PF Loan" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demo-grid" class="vista-grid">
                                                <div class="titlebar">
                                                    PF LOAN ENTRY
                                                </div>
                                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr class="header">
                                                        <th>Action
                                                        </th>
                                                        <th>Adv.Amt
                                                        </th>
                                                        <th>Adv.dt
                                                        </th>
                                                        <th>Per
                                                        </th>
                                                        <th>Taken As
                                                        </th>
                                                        <th>Fin.Sdate
                                                        </th>
                                                        <th>Fin.Edate
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFLTNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PFLTNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("ADVAMT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ADVDT","{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PER")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SHORTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FDATE","{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TDATE","{0:dd/MM/yyyy}")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFLTNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PFLTNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("ADVAMT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ADVDT", "{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PER")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SHORTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FDATE","{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TDATE","{0:dd/MM/yyyy}")%>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                    <div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvPFLoanEntry" PageSize="10"
                                            OnPreRender="dpPager_PreRender">
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
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>--%>
                </td>
            </tr>
            </table>
            <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <%--<asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
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
            </asp:Panel>--%>

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
                        alert("Only Numeric Characters allowed");
                        return false;
                    }
                    else {
                        return true;
                    }

                }

                function percentage(txt) {
                    if (ValidateNumeric(txt) == true) {
                        var txtProgressiveBalance = document.getElementById("ctl00_ContentPlaceHolder1_txtProgressiveBalance");
                        var txtsanctioper = document.getElementById("ctl00_ContentPlaceHolder1_txtsanctioper");
                        var txtsanction = document.getElementById("ctl00_ContentPlaceHolder1_txtsanction");
                        //alert(Number(txtProgressiveBalance.value)*Number(txtsanction.value)/100);
                        txtsanction.value = Math.round(Number(txtProgressiveBalance.value) * Number(txtsanctioper.value) / 100);
                    }
                }


                function LoanRecoverableIn(txt) {
                    if (ValidateNumeric(txt) == true) {

                        var txtAmtToBeSanction = document.getElementById("ctl00_ContentPlaceHolder1_txtAmtToBeSanction");
                        var txtLoanRecoverablein = document.getElementById("ctl00_ContentPlaceHolder1_txtLoanRecoverablein");
                        var txtInsatllmentAmount = document.getElementById("ctl00_ContentPlaceHolder1_txtInsatllmentAmount");
                        txtInsatllmentAmount.value = Math.round(Number(txtAmtToBeSanction.value) / Number(txtLoanRecoverablein.value));
                    }
                }


                function AmountToBeSanctioned(txt) {
                    if (ValidateNumeric(txt) == true) {
                        var txtProgressiveBalance = document.getElementById("ctl00_ContentPlaceHolder1_txtProgressiveBalance");
                        var txtAmtToBeSanction = document.getElementById("ctl00_ContentPlaceHolder1_txtAmtToBeSanction");
                        var txtPerLaonBalanceAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtPerLaonBalanceAmt");
                        txtAmtToBeSanction.value = Math.round(Number(txt.value) + Number(txtPerLaonBalanceAmt.value));

                        if (Number(txtAmtToBeSanction.value) > Number(txtProgressiveBalance.value)) {
                            alert("Amount to be sanctioned should be less than progressive balance");
                            txt.value = "";
                            txtAmtToBeSanction.value = "";
                            txt.focus();
                        }
                    }
                }

            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>





