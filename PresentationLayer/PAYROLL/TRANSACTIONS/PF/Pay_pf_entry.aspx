<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_pf_entry.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <style type="text/css">
        .ajax__myTab .ajax__tab_header {
            font-family: verdana,tahoma,helvetica;
            font-size: 15px;
            border-bottom: solid 30px #999999;
        }

        .ajax__myTab .ajax__tab_outer {
            padding-right: 4px;
            height: 21px;
            background-color: #C0C0C0;
            margin-right: 2px;
            border-right: solid 1px #666666;
            border-top: solid 1px #aaaaaa;
        }

        .ajax__myTab .ajax__tab_inner {
            padding-left: 3px;
            background-color: #C0C0C0;
        }

        .ajax__myTab .ajax__tab_tab {
            height: 13px;
            /*padding: 4px;*/
            margin: 0;
            padding-bottom: 30px;
        }

        .ajax__myTab .ajax__tab_hover .ajax__tab_outer {
            background-color: #cccccc;
        }

        .ajax__myTab .ajax__tab_hover .ajax__tab_inner {
            background-color: #cccccc;
        }

        .ajax__myTab .ajax__tab_hover .ajax__tab_tab {
            padding-bottom: 30px;
        }

        .ajax__myTab .ajax__tab_active .ajax__tab_outer {
            background-color: #fff;
            border-left: solid 1px #999999;
            height: 25px;
        }

        .ajax__myTab .ajax__tab_active .ajax__tab_inner {
            background-color: #fff;
        }

        .ajax__myTab .ajax__tab_active .ajax__tab_tab {
            padding-bottom: 30px;
        }

        .ajax__myTab .ajax__tab_body {
            font-family: verdana,tahoma,helvetica;
            font-size: 10pt;
            border: 1px solid #999999;
            border-top: 0;
            padding: 1px;
            background-color: #ffffff;
        }

        .style2 {
            padding-left: 5px;
            padding-top: 2px;
            padding-bottom: 2px;
            width: 15%;
        }

        .style3 {
            padding-left: 5px;
            padding-top: 2px;
            padding-bottom: 2px;
            width: 25%;
        }
    </style>
    <style>
        /* unvisited link */ linkbutton:link {
            color: green;
        }
        /* visited link */ linkbutton:visited {
            color: green;
        }
        /* mouse over link */ linkbutton:hover {
            color: red;
        }
        /* selected link */ linkbutton:active {
            color: yellow;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
     <ContentTemplate>
          <div class="row">
               <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PF ENTRY</h3>
                            <%--<div class="box-tools pull-right">
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />
                            </div>--%>
                        </div>
                         <form role="form">
                           <div class="box-body">
                               <div class="col-md-12">
                                     <asp:Panel ID="pnl" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Selection Criteria</div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">
                                                    <label>College :</label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Employee :</label>
                                                        <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1"
                                                            CssClass="form-control" ToolTip="Select Employee" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged" >
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Eligible For :</label>
                                                        <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server" CssClass="form-control" ToolTip="Eligible For"
                                                            TabIndex="2" Enabled="false"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Contribution Entry :</label>
                                                        <br />
                                                        <asp:CheckBox AutoPostBack="true" runat="server" ID="chkpfcontribution" Text="PF Contribution Entry" OnCheckedChanged="chkpfcontribution_CheckedChanged"
                                                             TabIndex="3" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div id="divPfContribution" runat="server" visible="false">
                                        <asp:Panel ID="pnlPfContribution" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">PF Contribution</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-4">
                                                            <label>Month Year :</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtMonthYearContributionAmount" runat="server" CssClass="form-control"
                                                                    TabIndex="4" ToolTip="Enter Month Year for PF Contribution"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceContributionAmount" runat="server" Format="MM/yyyy"
                                                                    TargetControlID="txtMonthYearContributionAmount" PopupButtonID="Image3" Enabled="true"
                                                                    EnableViewState="true" PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvtxtMonthYearContributionAmount" runat="server"
                                                                    ControlToValidate="txtMonthYearContributionAmount" Display="None" ErrorMessage="Month Year in (MM/yyyy Format)"
                                                                    ValidationGroup="PF" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Deduction Amount1 :</label>
                                                            <asp:TextBox runat="server" ID="txtDeductionAmount1" Text="0.00" CssClass="form-control"
                                                                TabIndex="5" ToolTip="Enter Deduction Amount 1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount1" runat="server" ControlToValidate="txtDeductionAmount1"
                                                                Display="None" ErrorMessage="Please Enter Deduction Amount1" ValidationGroup="PF"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cvtxtDeductionAmount1" runat="server" Display="None" ErrorMessage="Deduction Amount1 Should be Numeric"
                                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount1"
                                                                Type="Double"></asp:CompareValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Deduction Amount2 :</label>
                                                            <asp:TextBox runat="server" ID="txtDeductionAmount2" Text="0.00" CssClass="form-control" TabIndex="6"
                                                                ToolTip="Enter Deduction Amount 2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount2" runat="server" ControlToValidate="txtDeductionAmount2"
                                                                Display="None" ErrorMessage="Please Enter Deduction Amount2" ValidationGroup="PF"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cvtxtDeductionAmount2" runat="server" Display="None" ErrorMessage="Deduction Amount2 Should be Numeric"
                                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount2"
                                                                Type="Double"></asp:CompareValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Deduction Amount3 :</label>
                                                            <asp:TextBox runat="server" ID="txtDeductionAmount3" Text="0.00" CssClass="form-control" TabIndex="7"
                                                                ToolTip="Enter Deduction Amount 3"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount3" runat="server" ControlToValidate="txtDeductionAmount3"
                                                                Display="None" ErrorMessage="Please Enter Deduction Amount3" ValidationGroup="PF"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cvtxtDeductionAmount3" runat="server" Display="None" ErrorMessage="Deduction Amount3 Should be Numeric"
                                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount3"
                                                                Type="Double"></asp:CompareValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Deduction Amount4 :</label>
                                                            <asp:TextBox runat="server" ID="txtDeductionAmount4" Text="0.00" CssClass="form-control" TabIndex="8"
                                                                ToolTip="Enter Deduction Amount 4"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount4" runat="server" ControlToValidate="txtDeductionAmount4"
                                                                Display="None" ErrorMessage="Please Enter Deduction Amount4" ValidationGroup="PF"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cvtxtDeductionAmount4" runat="server" Display="None" ErrorMessage="Deduction Amount4 Should be Numeric"
                                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount4"
                                                                Type="Double"></asp:CompareValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <ajaxToolKit:TabContainer runat="server" ID="Tabs" OnActiveTabChanged="ActiveTabChanged"
                                        AutoPostBack="true" ActiveTabIndex="0" CssClass="linkedin linkedin-tab btn">
                                        <%--CssClass="linkedin linkedin-tab"--%>

                                        <ajaxToolKit:TabPanel runat="server" ID="OpeningBalance">
                                            <ContentTemplate>
                                                <div class="panel panel-info">
                                                    <div class="panel panel-heading" style="text-align:center;">Opening Balance</div>
                                                    <div class="panel panel-body">
                                                        <asp:UpdatePanel ID="updOpeningBalance" runat="server">
                                                            <ContentTemplate>
                                                                <div class="form-group col-md-12">
                                                                    <div class="form-group col-md-4">
                                                                        <label>Fin.Year Start Date :</label>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon">
                                                                                <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                            </div>
                                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"
                                                                                OnTextChanged="txtFromDate_TextChanged" ToolTip="Enter Financial Year Start Date"
                                                                                AutoPostBack="true"></asp:TextBox>
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
                                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Enabled="false"
                                                                            ToolTip="Financial Year End Date"></asp:TextBox>
                                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                        </ajaxToolKit:MaskedEditExtender>
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                        <label>Opening Balance :</label>
                                                                        <asp:TextBox runat="server" ID="txtOB" CssClass="form-control" ToolTip="Enter Opening Balance">
                                                                        </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtOB" runat="server" ControlToValidate="txtOB"
                                                                            Display="None" ErrorMessage="Please Enter Opening Balance" ValidationGroup="PF"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="cvtxtOB" runat="server" Display="None" ErrorMessage="Opening Balance Should be Numeric"
                                                                            ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtOB" Type="Double">
                                                                        </asp:CompareValidator>
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                        <label>Loan Opening Balance:</label>
                                                                        <asp:TextBox runat="server" ID="txtLoanOB" CssClass="form-control" ToolTip="Enter Loan Opening Balance">
                                                                        </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvLoanOB" runat="server" ControlToValidate="txtOB"
                                                                            Display="None" ErrorMessage="Please Enter Loan OB" ValidationGroup="PF" SetFocusOnError="True">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="CvLoanOB" runat="server" Display="None" ErrorMessage="Loan OB Should be Numeric"
                                                                            ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtLoanOB" Type="Double">
                                                                        </asp:CompareValidator>

                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </ajaxToolKit:TabPanel>
                                        <ajaxToolKit:TabPanel runat="server" ID="ProcessPF">
                                            <ContentTemplate>
                                                <div class="panel panel-info">
                                                    <div class="panel panel-heading">Process PF</div>
                                                    <div class="panel panel-body">
                                                        <asp:UpdatePanel ID="updProcessPF" runat="server">
                                                            <ContentTemplate>
                                                                <div class="form-group col-md-12">
                                                                    <div class="form-group col-md-4">
                                                                        <label>Month Year :</label>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon">
                                                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                            </div>
                                                                            <asp:TextBox ID="txtMonYearProcess" runat="server" CssClass="form-control"
                                                                                ToolTip="Enter Month Year"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="cetxtMonYearProcess" runat="server" Format="MM/yyyy"
                                                                                TargetControlID="txtMonYearProcess" PopupButtonID="Image4" Enabled="true" EnableViewState="true"
                                                                                PopupPosition="BottomLeft">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="rfvtxtMonYearProcess" runat="server" ControlToValidate="txtMonYearProcess"
                                                                                Display="None" ErrorMessage="Month Year in (MM/yyyy Format)" ValidationGroup="PF"
                                                                                SetFocusOnError="True">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </ajaxToolKit:TabPanel>
                                        <%--  <ajaxToolKit:TabPanel runat="server" ID="Panel2" OnClientClick="PanelClick" HeaderText="Item Group Master">--%>
                                        <%-- <ajaxToolKit:TabPanel runat="server" ID="LoanRepayment" CssClass="ajax__myTab">--%>
                                        <ajaxToolKit:TabPanel runat="server" ID="LoanRepayment">
                                            <ContentTemplate>
                                                <%--  HeaderText="Loan Repayment"--%>
                                                <div class="panel panel-info">
                                                    <div class="panel panel-heading">Loan Repayment</div>
                                                    <div class="panel panel-body">
                                                        <asp:UpdatePanel ID="updLoanRepayment" runat="server">
                                                            <ContentTemplate>
                                                                <div class="form-group col-md-12">
                                                                    <div class="form-group col-md-4">
                                                                        <label>Month Year :</label>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon">
                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                            </div>
                                                                            <asp:TextBox ID="txtMonthYear" runat="server" CssClass="form-control"
                                                                                ToolTip="Enter Month Year"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/yyyy"
                                                                                TargetControlID="txtMonthYear" PopupButtonID="Image2" Enabled="true" EnableViewState="true"
                                                                                PopupPosition="BottomLeft">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" ControlToValidate="txtMonthYear"
                                                                                Display="None" ErrorMessage="Month Year in (MM/yyyy Format)" ValidationGroup="PF"
                                                                                SetFocusOnError="True">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                        <label>Amount :</label>
                                                                        <asp:TextBox runat="server" ID="txtamount" Text="0.00" CssClass="form-control"
                                                                            ToolTip="Enter Amount"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rvftxtamount" runat="server" ControlToValidate="txtamount"
                                                                            Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="PF" SetFocusOnError="True">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="cvtxtamount" runat="server" Display="None" ErrorMessage="Amount Should be Numeric"
                                                                            ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtamount" Type="Double">
                                                                        </asp:CompareValidator>

                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </ajaxToolKit:TabPanel>
                                    </ajaxToolKit:TabContainer>
                               </div>
                            </div>
                         </form>
                        <div class="box-footer">
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="butSubmit" Text="Submit" runat="server" CssClass="btn btn-primary" ValidationGroup="PF"
                                         OnClick="butSubmit_Click" />
                                    <asp:Button ID="Button2" Text="Cancel" runat="server" CssClass="btn btn-warning"  OnClick="Button2_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="PnlGpfCpfContibutionEntry" runat="server" ScrollBars="Auto">
                                    <table class="table table-bordered table-hover table-responsive">
                                        <tr>
                                            <td>
                                                <asp:ListView ID="lvItemGpfCpfContibutionEntry" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <h4 class="box-title">GPF/CPF Contribution</h4>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Month Year
                                                                        </th>
                                                                        <th>G.P.F
                                                                        </th>
                                                                        <th>G.P.F ADD
                                                                        </th>
                                                                        <th>G.P.F Loan
                                                                        </th>
                                                                        <%-- <th>
                                                                                    Deduction Amount4
                                                                                </th>--%>
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
                                                                <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                            </td>
                                                            <td>
                                                                <%# Eval("MONYEAR")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H1")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H2")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H3")%>
                                                            </td>
                                                            <%--<td>
                                                                            <%# Eval("H4")%>
                                                                        </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <%--<div class="vista-grid_datapager">
                                                    <div class="text-center">
                                                        <asp:DataPager ID="dpPagerGroupMaster" runat="server" PagedControlID="GpfContibutionEntry"
                                                            PageSize="5" OnPreRender="dpPagerGroupMaster_PreRender">
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
                                                </div>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlOpeningBalance" runat="server" ScrollBars="Auto">
                                    <table class="table table-bordered table-hover table-responsive">
                                        <tr>
                                            <td>
                                                <asp:ListView ID="lvOpeningBalance" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <h4 class="box-title">Opening Balance</h4>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Month Year
                                                                        </th>
                                                                        <th>Fin.Year Start Date
                                                                        </th>
                                                                        <th>Fin.Year End Date
                                                                        </th>
                                                                        <th>Opening Balance
                                                                        </th>
                                                                        <th>Loan Bal
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
                                                                <asp:ImageButton ID="btnEditOB" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                    AlternateText="Edit Record" OnClick="btnEdit_Click" ToolTip="Edit Record" />&nbsp;
                                                            </td>
                                                            <td>
                                                                <%# Eval("MONYEAR")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FSDATE","{0:dd/MM/yyyy}")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FEDATE","{0:dd/MM/yyyy}")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("OB")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("LOANBAL")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlProcessPF" runat="server" ScrollBars="Auto">
                                    <table class="table table-bordered table-hover table-responsive">
                                        <tr>
                                            <td>
                                                <asp:ListView ID="lvProcessPF" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <h4 class="box-title">Process Result</h4>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <%--<th>
                                                                          Action
                                                                      </th>--%>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblMonYear" Text="Month Year"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblPf" Text="PF"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblPfAdd" Text="PF Add"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblPfLoan" Text="PF Loan"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblstatus" Text="Status"></asp:Label>
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
                                                            <%--<td>
                                                                 <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("MISGNO") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditSubGroup_Click" />&nbsp;
                                                             </td>--%>
                                                            <td>
                                                                <%# Eval("Monyear")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H1")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H2")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H3")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("status")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlLoanRepayment" runat="server" ScrollBars="Auto">
                                    <table class="table table-bordered table-hover table-responsive">
                                        <tr>
                                            <td>
                                                <asp:ListView ID="lvLoanRepayment" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <h4 class="box-title">Loan Repayment</h4>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Action
                                                                        </th>
                                                                        <th>MonYear
                                                                        </th>
                                                                        <th>Amount
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
                                                                <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                            </td>
                                                            <td>
                                                                <%# Eval("MONYEAR")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H3")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
               </div>
          </div>
          <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px"><%--PF ENTRY&nbsp;--%>
                        <!-- Button used to launch the help (animation) -->
                        <%--<asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
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
                    <td style="padding-left: 10px">
                        <div style="text-align: left; width: 95%;">
                            <%--<fieldset class="fieldsetPay">
                                <legend class="legendPay">Selection Criteria</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="form_left_label" width="20%">Employee :
                                        </td>
                                        <td class="form_left_text" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true"
                                                Width="300px" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Eligible For :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Contribution Entry :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:CheckBox AutoPostBack="true" runat="server" ID="chkpfcontribution" Text="PF Contribution Entry"
                                                OnCheckedChanged="pfcontribution_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>--%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px;" colspan="5">
                        <br />
                        <%-- <div id="divPfContribution" runat="server" style="text-align: left; width: 95%;" visible="false">--%>
                        <%--<fieldset class="fieldsetPay">
                                <legend class="legendPay">PF Contribution</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="form_left_label" width="20%">Month Year :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtMonthYearContributionAmount" runat="server" Width="80px"></asp:TextBox>
                                            &nbsp;<asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="ceContributionAmount" runat="server" Format="MM/yyyy"
                                                TargetControlID="txtMonthYearContributionAmount" PopupButtonID="Image3" Enabled="true"
                                                EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtMonthYearContributionAmount" runat="server"
                                                ControlToValidate="txtMonthYearContributionAmount" Display="None" ErrorMessage="Month Year in (MM/yyyy Format)"
                                                ValidationGroup="PF" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Deduction Amount1 :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount1" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount1" runat="server" ControlToValidate="txtDeductionAmount1"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount1" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount1" runat="server" Display="None" ErrorMessage="Deduction Amount1 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount1"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Deduction Amount2 :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount2" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount2" runat="server" ControlToValidate="txtDeductionAmount2"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount2" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount2" runat="server" Display="None" ErrorMessage="Deduction Amount2 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount2"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Deduction Amount3 :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount3" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount3" runat="server" ControlToValidate="txtDeductionAmount3"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount3" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount3" runat="server" Display="None" ErrorMessage="Deduction Amount3 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount3"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Deduction Amount4 :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount4" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount4" runat="server" ControlToValidate="txtDeductionAmount4"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount4" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount4" runat="server" Display="None" ErrorMessage="Deduction Amount4 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount4"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="padding-left: 5px">
                                            <br />
                                            <asp:Panel ID="PnlGpfCpfContibutionEntry" runat="server">
                                                <table cellpadding="0" cellspacing="0" style="width: 99%;">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:ListView ID="lvItemGpfCpfContibutionEntry" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <br />
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid" class="vista-grid">
                                                                        <div class="titlebar">
                                                                            GPF/CPF Contribution
                                                                        </div>
                                                                        <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr class="header">
                                                                                <th>Action
                                                                                </th>
                                                                                <th>Month Year
                                                                                </th>
                                                                                <th>G.P.F
                                                                                </th>
                                                                                <th>G.P.F ADD
                                                                                </th>
                                                                                <th>G.P.F Loan
                                                                                </th>--%>
                        <%--Already Committed <th>
                                                                                    Deduction Amount4
                                                                                </th>--%>
                        <%-- </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MONYEAR")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H1")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H2")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H3")%>
                                                                        </td>--%>
                        <%--Already Committed<td>
                                                                            <%# Eval("H4")%>
                                                                        </td>--%>
                        <%-- </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MONYEAR")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H1")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H2")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H3")%>
                                                                        </td>--%>
                        <%-- <td>
                                                                            <%# Eval("H4")%>
                                                                        </td>--%>
                        <%--</tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>--%>
                        <%--Already Committed<div class="vista-grid_datapager">
                                                                            <asp:DataPager ID="dpPagerGroupMaster" runat="server" PagedControlID="GpfContibutionEntry"
                                                                                PageSize="5" OnPreRender="dpPagerGroupMaster_PreRender">
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
                                                                        </div>--%>
                        <%-- </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>--%>
                        <%-- </div>--%>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px;">
                        <br />

                    </td>
                </tr>
                <tr>
                    <%--<td colspan="4" align="center">
                        <br />
                        <asp:Button ID="butSubmit" Text="Submit" runat="server" Width="70px" ValidationGroup="PF"
                            OnClick="butSubmit_Click" />
                        <asp:Button ID="Button2" Text="Cancel" runat="server" Width="70px" OnClick="butCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </td>--%>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

