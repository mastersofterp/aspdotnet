<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_ITDeclaration.aspx.cs" Inherits="Pay_ITDeclaration" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="~/includes/prototype.js" type="text/javascript"></script>
    <script src="~/includes/scriptaculous.js" type="text/javascript"></script>
    <script src="../../includes/prototype.js" type="text/javascript"></script>
    <script src="../../includes/scriptaculous.js" type="text/javascript"></script>--%>

    <style>
        .DocumentList1 {
            /*overflow-x: scroll;*/
            overflow-y: scroll;
            height: 350px;
            /*width: 100%;*/
            padding: 3%;
        }
    </style>

    <script type="text/javascript">
        function showLockConfirm() {
            var ret = confirm('Do you really want to Lock! It will not allowed to modify further.');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>

    <style>
        .ledgermodalPopup {
            background-color: #e5ecf9;
            border-width: 3px;
            border-style: double;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 80%;
            height: 590px;
        }

        .ledgermodalBackground {
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .ledgermodalPopup1 {
            background-color: #e5ecf9;
            border-width: 3px;
            border-style: double;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 50%;
            height: 450px;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpanel"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DECLARATION FORM FOR INCOME TAX CALCULATION</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <%--   <div class="sub-heading">
                                        <h5>DECLARATION FORM FOR INCOME TAX CALCULATION</h5>
                                    </div>--%>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-5 col-md-5 col-12" id="pnlPeriod" runat="server">

                                            <div class="sub-heading">
                                                <h5>Employee Selection</h5>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>
                                                        <span style="color: #FF0000">*</span>
                                                        From:</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <div class="input-group date">
                                                        <%--<div class="input-group-addon">
                                                                    <asp:Image ID="imgFdate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control textbox" TabIndex="28" Enabled="false"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgFdate" TargetControlID="txtFromDate" />
                                                        <cc1:MaskedEditExtender ID="meeFromDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                            TargetControlID="txtFromDate">
                                                        </cc1:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                            Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>
                                                        <span style="color: #FF0000">*</span>
                                                        To:</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <div class="input-group date">
                                                        <%--<div class="input-group-addon">
                                                                    <asp:Image ID="imgTdate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="28" CssClass="form-control textbox" Enabled="false"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                            PopupButtonID="imgTdate" Enabled="true" EnableViewState="true" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                            MaskType="Date" Mask="99/99/9999">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                            Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="divOrderBy" runat="server" visible="true">
                                                <div class="col-md-4">
                                                    <label>Order By:</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>--%>
                                                    <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged" Visible="false">
                                                        <asp:ListItem Text="IDNO" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="NAME" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="SEQUENCE NUMBER" Value="3"></asp:ListItem>
                                                        <asp:ListItem Value="4">EMPLOYEE NUMBER</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="form-group row" id="divCollege" runat="server" visible="true">
                                                <div class="col-md-4">
                                                    <label><span style="color: #FF0000">*</span>College:</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                             Display="None" ErrorMessage="Please Select College" SetFocusOnError="true" InitialValue="0"
                                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="ddlCollege" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label><span style="color: #FF0000">*</span> Employee:</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlemp" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlemp_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfEmpName" runat="server" />
                                                  <%--  <asp:RequiredFieldValidator ID="rfvEmpName" runat="server" ControlToValidate="txtEmpName"
                                                        Display="None" ErrorMessage="Please Enter Employee Name" SetFocusOnError="true"
                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlemp"
                                                     Display="None" ErrorMessage="Please Select Employee" SetFocusOnError="true" InitialValue="0"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtEmpName" runat="server" AutoPostBack="true" BackColor="ActiveBorder"
                                                        OnTextChanged="txtEmpName_TextChanged" TabIndex="1" Visible="false" CssClass="form-control"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="txtEmpName_AutoCompleteExtender" runat="server" CompletionInterval="0"
                                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="6" Enabled="True"
                                                        MinimumPrefixLength="1" OnClientItemSelected="GetEmpName" ServiceMethod="GetEmpName"
                                                        ServicePath="~/Autocomplete.asmx" TargetControlID="txtEmpName">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                            </div>

                                            <div class="form-group row" id="divITRuleName" runat="server" visible="false">
                                                <div class="col-md-4">
                                                    <label><span style="color: #FF0000">*</span>IT Rule Name:</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlITRule" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select IT Rules" OnSelectedIndexChanged="ddlITRule_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">------Select------</asp:ListItem>
                                                    </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlITRule"
                                                  Display="None" ErrorMessage="Please Select IT Rule Scheme" SetFocusOnError="true" InitialValue="0"
                                                  ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>HRA:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtHRA1" runat="server" onKeyUp="validateNumeric(this)" TabIndex="13"
                                                        CssClass="form-control textbox"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtSalHRA" runat="server" CssClass="form-control textbox" Enabled="false"
                                                        TabIndex="13"></asp:TextBox></td>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-7 col-md-7 col-12" id="pnlEmployee" runat="server">
                                            <div class="sub-heading">
                                                <h5>Employee Details</h5>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Name</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:TextBox ID="lblName" runat="server" ForeColor="Black" Enabled="false"
                                                        CssClass="form-control textbox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Designation</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="lblDesignation" runat="server" Text="" ForeColor="Black" Enabled="false"
                                                        CssClass="form-control textbox"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Department</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="lblDepartment" runat="server" Text="" ForeColor="Black" Enabled="false"
                                                        CssClass="form-control textbox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Staff</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="lblStaff" runat="server" Text="" ForeColor="Black" Enabled="false"
                                                        CssClass="form-control textbox"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>PAN No.</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtPANNO" runat="server" TabIndex="10" CssClass="form-control textbox" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Phone No.</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtPhoneNo" runat="server" TabIndex="11" CssClass="form-control textbox" onKeyUp="validateNumeric(this)" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label><span style="color: #FF0000">*</span> Email ID</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtEmailID" runat="server" TabIndex="12" CssClass="form-control textbox" Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvemailid" runat="server" ControlToValidate="txtEmailID"
                                                        Display="None" ErrorMessage="Please Enter Employee Email" SetFocusOnError="true"
                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Perquisite</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtSumPerquisite" runat="server" CssClass="form-control textbox"
                                                        TabIndex="13"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label><%--Medical:--%></label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtGross" runat="server" CssClass="form-control textbox" Visible="false"
                                                        TabIndex="13"></asp:TextBox>
                                                    <asp:TextBox ID="txtMedical" runat="server" CssClass="form-control textbox" onKeyUp="validateNumeric(this)"
                                                        TabIndex="13" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSendMail" runat="server" CssClass="btn btn-primary" Text="Perquisites Entry" TabIndex="8" Enabled="false" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5 col-md-5 col-12">
                                            <div class="sub-heading" id="pnlHRAEntry" runat="server">
                                                <h5>HRA</h5>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Pay</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtpay" runat="server" CssClass="form-control textbox" Enabled="false" onKeyUp="validateNumeric(this)"
                                                        TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>DA</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtDA" runat="server" CssClass="form-control textbox" Enabled="false" onKeyUp="validateNumeric(this)"
                                                        TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Actual HRA</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtActualHra" runat="server" AutoPostBack="true" CssClass="form-control textbox"
                                                        Enabled="false" onKeyUp="validateNumeric(this)" TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="divROP" runat="server" visible="false">
                                                <div class="col-md-4">
                                                    <label>ROP HRA Only</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtROPHra" runat="server" AutoPostBack="true" CssClass="form-control textbox"
                                                        onKeyUp="validateNumeric(this)" OnTextChanged="txtROPHra_TextChanged" TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>
                                                        Total HRA Received
                                                    </label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtHRAReceived" runat="server" AutoPostBack="true" CssClass="form-control textbox"
                                                        Enabled="false" onKeyUp="validateNumeric(this)" TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Paid Rent</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtPaidRent" runat="server" AutoPostBack="true" CssClass="form-control textbox"
                                                        onKeyUp="validateNumeric(this)" TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>10%&nbsp;Sal</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txt10persal" runat="server" AutoPostBack="true" CssClass="form-control textbox"
                                                        Enabled="false" onKeyUp="validateNumeric(this)" TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Rent paid in access of 10% of sal</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtRentPaidinAccess" runat="server" AutoPostBack="true" CssClass="form-control textbox"
                                                        Enabled="false" onKeyUp="validateNumeric(this)" TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <%--<label>40% Salary:</label>--%>
                                                    <asp:Label ID="lblMetroCity" runat="server" Text="50% Salary" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txt40persal" runat="server" AutoPostBack="true" CssClass="form-control textbox"
                                                        Enabled="false" onKeyUp="validateNumeric(this)" TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Less HRA</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtHRAEntry" runat="server" AutoPostBack="true" CssClass="form-control textbox"
                                                        Enabled="false" onKeyUp="validateNumeric(this)" TabIndex="28"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Select </label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:RadioButtonList ID="rdbMetro" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbMetro_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Selected="True">Metro City&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="2">Non-Metro City</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnHraEntryActual" runat="server" CssClass="btn btn-primary" OnClick="btnHraEntryActual_Click"
                                                        Text="HRA Calculate" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>TOTAL GROSS </label>
                                                </div>

                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txttotalgross" runat="server" Enabled="false" TabIndex="28"
                                                        CssClass="form-control textbox"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-7 col-md-7 col-12" id="pnlIncomeReported" runat="server">
                                            <div class="sub-heading">
                                                <h5>Any Other Income/Deduction Reported </h5>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-7">
                                                    <label><span style="color: #FF0000">*</span>Reported as on date -</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="imgdate">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDate" runat="server" TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgdate" TargetControlID="txtDate" />
                                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtDate"
                                                            Display="None" ErrorMessage="Please Select Date" SetFocusOnError="true" ValidationGroup="submit">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-7">
                                                    <label>Interest on Housing Loan </label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtIntrHousingLoan" runat="server" onKeyUp="validateNumeric(this)"
                                                        TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                    <%--OnTextChanged="UpdateOTHERINCOMETotals"--%>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-7">
                                                    <label>Additional Deduction under Sec 80CCD NPS -:</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txt80CCDNPS" runat="server" onKeyUp="validateNumeric(this)"
                                                        TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                    <%--OnTextChanged="UpdateOTHERINCOMETotals"--%>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-7">
                                                    <label>Deduction under RGESS Sec 80CCG </label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtRGRSS80CCG" runat="server" onKeyUp="validateNumeric(this)"
                                                        TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                    <%--OnTextChanged="UpdateOTHERINCOMETotals"--%>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-7">
                                                    <label><%-- Interest on NSC:--%></label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtIntrNSC" runat="server" onKeyUp="validateNumeric(this)" Visible="false"
                                                        TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                    <%--OnTextChanged="UpdateOTHERINCOMETotals"--%>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-7">
                                                    <label><%-- Other Income TDS:--%></label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtOtherIncTDS" runat="server" AutoPostBack="true" onKeyUp="validateNumeric(this)"
                                                        Visible="false" TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-9">
                                                    <label>Enter Reported/ Mention Any Other Income details -:</label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-7">
                                                    <div class="text-center">
                                                        <label>Particulars</label>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRP1" runat="server" TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRP2" runat="server" TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRP3" runat="server" TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRP4" runat="server" TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRP5" runat="server" TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="text-center">
                                                        <label>Amount</label>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRAm1" runat="server" onKeyUp="validateNumeric(this)"
                                                            TabIndex="28" CssClass="form-control textbox" onblur="return check(this);"></asp:TextBox>
                                                        <%--onkeypress--%>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRAm2" runat="server" onKeyUp="validateNumeric(this)"
                                                            TabIndex="28" CssClass="form-control textbox" onblur="return check(this);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRAm3" runat="server" onKeyUp="validateNumeric(this)"
                                                            TabIndex="28" CssClass="form-control textbox" onblur="return check(this);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRAm4" runat="server" onKeyUp="validateNumeric(this)"
                                                            TabIndex="28" CssClass="form-control textbox" onblur="return check(this);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:TextBox ID="txtINTRAm5" runat="server" onKeyUp="validateNumeric(this)"
                                                            TabIndex="28" CssClass="form-control textbox" onblur="return check(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-7 text-center">
                                                    <label>GRAND TOTAL </label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtINTRTotal" runat="server" Enabled="false" TabIndex="28"
                                                        CssClass="form-control textbox"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-12">
                                            <asp:Panel ID="pnlVIAList" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvVIAHeads" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <div class="sub-heading">
                                                                <h5>Details of DEDUCTION Under CHAPTER VI A</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Particulars
                                                                        </th>
                                                                        <th>Deduc. Under Section
                                                                        </th>
                                                                        <th>Actual Amount 
                                                                        </th>
                                                                        <th>Limit
                                                                        </th>
                                                                        <th>Amount
                                                                        </th>
                                                                        <th>Upload Document
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
                                                                <asp:Label ID="lblHeadFullName" runat="server" Text='<%# Eval("HEADNAMEFULL") %>' />
                                                                <asp:HiddenField ID="hdnCNO" runat="server" Value='<%# Eval("CNO") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSec" runat="server" Text='<%# Eval("SEC") %>' CssClass="form-control" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtActualAmount" runat="server" CssClass="form-control textbox" Text='<%# Eval("ACTUAL_AMOUNT") %>' Enabled="false"></asp:TextBox>
                                                                <%-- <asp:Label ID="lblActalAmt" runat="server" Text='<%# Eval("ACTUAL_AMOUNT") %>' CssClass="form-control" />--%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblLimit" runat="server" Text='<%# Eval("LIMIT") %>' CssClass="form-control" Enabled="false" />
                                                                <asp:Label ID="lblShowPercentage" runat="server" Text='<%# Eval("SHOW_PERCENTAGE") %>' />
                                                                <asp:HiddenField ID="hdnIsPercentage" runat="server" Value='<%# Eval("ISPERCENTAGE") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAmount" runat="server" AutoPostBack="false" onKeyUp="ChapterVIAListView(this),validateNumeric(this)"
                                                                    OnTextChanged="UpdateCHAPVIATotals" CssClass="form-control textbox" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                                                <%--<asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'  CssClass="form-control"/>--%>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnUploadDocument" runat="server" CssClass="btn btn-primary" Text="Attachment"
                                                                    OnClick="btnUploadDocument_Click" CommandArgument='<%# Eval("CNO")%>' CommandName='<%# Eval("HEADNAMEFULL") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="pnlVIARebateList" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvVIARebate" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <div class="sub-heading"><h5>Rebate Under Section 80C LISTVIEW</h5></div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Particulars
                                                                        </th>
                                                                        <th>Total
                                                                        </th>
                                                                        <th>Upload Document
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
                                                                <asp:Label ID="lblPayHead" runat="server" Text='<%# Eval("PAYHEAD") %>' />
                                                                <asp:HiddenField ID="hdnFNO" runat="server" Value='<%# Eval("FNO") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtValue" runat="server" CssClass="form-control textbox" Text='<%# Eval("VALUE") %>' onKeyUp="validateNumeric(this)"></asp:TextBox>

                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnUploadRebate" runat="server" CssClass="btn btn-primary" Text="Attachment" OnClick="btnUploadRebate_Click"
                                                                    CommandArgument='<%# Eval("FNO")%>' CommandName='<%# Eval("PAYHEAD") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label>
                                                Remark:
                                            </label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtRemark" runat="server" TabIndex="28" CssClass="form-control textbox"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row">

                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-10">
                                            <asp:CheckBox ID="ChkIsLock" runat="server" Text=" IT Declaration Completed & Lock" onclick="return confirm('Are You Sure? Further you can not Modify IT Declaration.');" />
                                        </div>

                                    </div>

                                    <div class="form-group row">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-10">
                                            <asp:CheckBox ID="ChkSendEmail" runat="server" Text=" Is Send Email" AutoPostBack="true" OnCheckedChanged="ChkSendEmail_CheckedChanged" />
                                        </div>
                                    </div>

                                    <div class="form-group row" id="divMessage" runat="server" visible="false">
                                        <div class="col-md-2">
                                            <label>
                                                Mail Message:<span style="color: #FF0000">*</span>
                                            </label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtMailMessage" runat="server" TabIndex="29" CssClass="form-control textbox" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMailMessage" runat="server" ControlToValidate="txtMailMessage" Display="None"
                                                ErrorMessage="Please Enter Mail Message" ValidationGroup="email" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-12 btn-footer">
                                            <%--  By prashant--%>
                                            <%--  <asp:CheckBox ID="ChkIsMailSend" runat="server" Text="Mail Send" Visible="false" />--%>
                                            <%-- <asp:CheckBox ID="ChkIsFinalSubmit" runat="server" Text="Final Submition" Visible="false" />--%>
                                            <asp:Button ID="btnITSendMail" runat="server" OnClick="btnITSendMail_Click" TabIndex="30" Text="Send Mail" ToolTip="Click To Send Mail"
                                                Visible="false" CssClass="btn btn-primary" ValidationGroup="email" />
                                            <asp:Button ID="btnSave0" runat="server" OnClick="btnSave_Click" TabIndex="31" Text="Save" ToolTip="Click To Save Details"
                                                ValidationGroup="submit" Visible="True" CssClass="btn btn-primary" />
                                          <asp:Button ID="btnLockUnlock" runat="server" OnClick="btnLockUnlock_Click" TabIndex="33" Text="Lock / Unlock" ToolTip="Click To Lock/Unlock"
                                                Visible="false" CssClass="btn btn-primary" />
                                              <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" TabIndex="32" Text="Cancel" ToolTip="Click To Clear Details"
                                                Visible="True" CssClass="btn btn-warning" />
                                         
                                            <asp:ValidationSummary ID="vsEmp" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                            <asp:ValidationSummary ID="vsEmail" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="email" />
                                        </div>
                                    </div>


                                    <div class="form-group row">

                                        <div>
                                            <asp:Label ID="lblEmail" runat="server" Text=" " /><br />
                                            <asp:Label ID="lblPwd" runat="server" Text=" " /><br />
                                            <%--<asp:Button ID="btn1" runat="server" Text="Edit"/>--%>
                                        </div>


                                        <ajaxToolKit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlEntryPopup" TargetControlID="btnSendMail"
                                            BackgroundCssClass="ledgermodalBackground" CancelControlID="btnCanceModal">
                                        </ajaxToolKit:ModalPopupExtender>

                                        <asp:Panel ID="pnlEntryPopup" runat="server" CssClass="ledgermodalPopup" BackColor="White">
                                            <asp:Label ID="lblpopup" CssClass="control-label" runat="server"></asp:Label>
                                            <div class="form-group row text-center" style="margin-left: 450px;">
                                                <asp:Button ID="btnCanceModal" CssClass="btn btn-warning" runat="server" Text="Cancel" OnClick="CancelPerquisite" />
                                                <asp:Button ID="btnSavePerquisite" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="SavePerquisite" />
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <div class="DocumentList" style="overflow-x: scroll; height: 500px">
                                                            <asp:ListView ID="lvEmp" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="lgv1">
                                                                        <h4 class="sub-heading"><h5>Perquisites Details</h5>
                                                                        </h4>
                                                                        <table class="table table-bordered table-hover">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>Nature of perquisite
                                                                                    </th>
                                                                                    <th>Amount1
                                                    <%--of perquisite as per rules(Rs.)--%>
                                                                                    </th>
                                                                                    <th>Amount2
                                                    <%--, if any, recovered from the employee(Rs.)--%>
                                                                                    </th>
                                                                                    <th>Amount3
                                                    <%--of perquisite chargeable to tax Col.(3)-Col.(4)(Rs.)--%>
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
                                                                            <%--<asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("PERQUISITES")%>' CommandArgument='<%# Eval("IDNO")%>'
                                               OnClick="lnkId_Click" ></asp:LinkButton> --%>
                                                                            <asp:Label ID="lnkId" runat="server" Text='<%# Eval("PERQUISITES")%>' CommandArgument='<%# Eval("PERQUISITESID")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtVALUE3" runat="server" MaxLength="7" Text='<%#Eval("VALUE3")%>'
                                                                                ToolTip='<%#Eval("PERQUISITESID")%>' Width="100px" onkeyup="return Increment(this),validateNumeric(this);" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtVALUE4" runat="server" MaxLength="7" Text='<%#Eval("VALUE4")%>'
                                                                                ToolTip='<%#Eval("PERQUISITESID")%>' Width="100px" onkeyup="return Increment2(this),validateNumeric(this);" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtVALUE5" runat="server" MaxLength="7" Text='<%#Eval("VALUE5")%>'
                                                                                ToolTip='<%#Eval("PERQUISITESID")%>' Width="100px" />
                                                                            <%--onkeyup="return check(this);" --%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lblpopup2" BackgroundCssClass="ledgermodalBackground"
                                        Y="100" PopupControlID="ModelPanel_Upload" />

                                    <div class="form-group row">
                                        <asp:Panel ID="ModelPanel_Upload" runat="server" CssClass="ledgermodalPopup1" BackColor="White">
                                            <asp:Label ID="lblpopup2" CssClass="control-label" runat="server"></asp:Label>
                                            <div class="DocumentList1">
                                                <div class="form-group row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-12">
                                                            <div class="panel panel-info" style="height: 600px">
                                                                <div class="panel-heading">
                                                                    Add Deduction Amount
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="form-group row">
                                                                        <div class="col-md-3">
                                                                            <label>Particular</label>
                                                                        </div>
                                                                        <div class="col-md-9">
                                                                            <asp:Label ID="lblPerticular" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row" id="tr_txtHouseOwnerName" runat="server" visible="false">
                                                                        <div class="col-md-3">
                                                                            <label>Title</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox ID="txtHouseOwnerName" runat="server" CssClass="form-control" TabIndex="28"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row" id="tr_txtPanNumber" runat="server" visible="false">
                                                                        <div class="col-md-3">
                                                                            <label>Pan Number</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="form-control" TabIndex="28"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="col-md-3">
                                                                            <label>Amount <span style="color: red">*</span></label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TabIndex="28"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAmount"
                                                                                Display="None" ErrorMessage="Please Enter Amount" SetFocusOnError="true"
                                                                                ValidationGroup="VI"></asp:RequiredFieldValidator>

                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="filterExt" runat="server"
                                                                                TargetControlID="txtAmount"
                                                                                FilterType="Custom"
                                                                                FilterMode="ValidChars"
                                                                                ValidChars="1234567890.">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </div>


                                                                    <div class="form-group row">
                                                                        <div class="col-md-3">
                                                                            <label>Document Name<span style="color: red">*</span></label>
                                                                        </div>
                                                                        <div class="col-md-9">
                                                                            <asp:TextBox ID="txtDocumentName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="col-md-3">
                                                                            <label>Upload Document <span style="color: red">*</span></label>
                                                                        </div>
                                                                        <div class="col-md-9">

                                                                            <asp:FileUpload ID="fupDocument" runat="server" />

                                                                            <h3 style="color: red"><b>
                                                                                <asp:Label ID="lblFileNaame" runat="server"></asp:Label></b></h3>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row" id="tr_DocSubDate" runat="server" visible="false">
                                                                        <div class="col-md-3">
                                                                            <label>Document submission Date</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtDeclarationDate" runat="server" CssClass="form-control" TabIndex="28" onKeyUp="validateNumeric(this);"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtDeclarationDate" />
                                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                    TargetControlID="txtDeclarationDate">
                                                                                </cc1:MaskedEditExtender>
                                                                                <div class="input-group-addon">
                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row" id="div_Remark" runat="server" visible="false">
                                                                        <div class="col-md-3">
                                                                            <label>Remark</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox ID="txtRemarkPopUp" runat="server" CssClass="form-control" TabIndex="28"></asp:TextBox>

                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="col-md-3">
                                                                        </div>
                                                                        <div class="col-md-9">
                                                                            <asp:Button ID="btnAddDocument" runat="server" CssClass="btn btn-primary" Text="Add" ValidationGroup="VI"
                                                                                OnClick="btnAddDocument_Click" />

                                                                            <asp:Button ID="btnAddRebate" runat="server" CssClass="btn btn-primary" Text="Add" Visible="false" OnClick="btnAddRebate_Click" />

                                                                            <asp:Button ID="btnShowDetail" runat="server" CssClass="btn btn-info" Text="Show" Visible="false" />

                                                                            <asp:Button ID="btnClosePopUp" runat="server" CssClass="btn btn-warning" Text="Close" />

                                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                                ShowSummary="false" ValidationGroup="VI" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="DocumentList">
                                                                            <div class="col-md-12" id="div_lv_VI" runat="server" style="width: 650px;">
                                                                                <%-- visible="false"  --%>
                                                                                <div class="DocumentList">
                                                                                    <asp:ListView ID="lvEmpAmount" runat="server">
                                                                                        <LayoutTemplate>
                                                                                            <div class="lgv1">
                                                                                                <h4 class="box-title">Details of Deduction Under CHAPTER VI A
                                                                                                </h4>
                                                                                                <table class="table table-bordered table-hover">
                                                                                                    <thead>
                                                                                                        <tr class="bg-light-blue">
                                                                                                            <th width="5%">Action
                                                                                                            </th>
                                                                                                            <th width="20%">Amount
                                                                                                            </th>
                                                                                                            <th width="10%">Document Name
                                                                                                            </th>
                                                                                                            <th style="display: none;"></th>
                                                                                                            <th style="display: none;"></th>
                                                                                                            <th width="5%">Download
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
                                                                                                <td width="15%">
                                                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CHAPVI_ID")%>'
                                                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;                                                                           
                                                                                                 <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("CHAPVI_ID") %>' Visible="false"
                                                                                                     AlternateText="Delete Record" ToolTip="Delete Record" OnClientClick="showConfirmDel(this); return false;" />
                                                                                                    <ajaxToolKit:ConfirmButtonExtender ID="CnDelete" runat="server" ConfirmText="Are you sure you want to delete this record?"
                                                                                                        TargetControlID="btnDelete">
                                                                                                    </ajaxToolKit:ConfirmButtonExtender>

                                                                                                </td>


                                                                                                <td width="20%">
                                                                                                    <%# Eval("Amount")%>
                                                                                                </td>

                                                                                                <td width="20%">

                                                                                                    <%# Eval("EmpDocumentName")%>
                                                                                                </td>
                                                                                                <td style="display: none;">

                                                                                                    <asp:Label ID="docURL" runat="server" Text='<%# Eval("EmpDocumentUrl")%>'></asp:Label>

                                                                                                </td>
                                                                                                <td style="display: none;">
                                                                                                    <asp:Label ID="docNAME" runat="server" Text='<%# Eval("DocumentType")%>'></asp:Label>

                                                                                                </td>
                                                                                                <td style="width: 2%; text-align: center;">
                                                                                                    <asp:UpdatePanel ID="updpnld" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/IMAGES/downarrow.jpg" Width="25px" Height="25px" TabIndex="14"
                                                                                                                ToolTip="Download" CommandArgument='<%# Eval("CHAPVI_ID") %>' CommandName='<%# Eval("EmpDocumentUrl") %>'
                                                                                                                OnClick="btnDownload_Click" />
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:PostBackTrigger ControlID="btnDownload" />
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                </div>
                                                                            </div>


                                                                            <div class="col-md-12" id="div_lv_Rebate" runat="server" visible="false" style="width: 650px;">
                                                                                <div class="DocumentList" style="overflow-x: scroll; height: 500px">
                                                                                    <asp:ListView ID="lv_Rebate" runat="server">
                                                                                        <LayoutTemplate>
                                                                                            <div class="lgv1">
                                                                                                <h4 class="box-title">Rebate Under Section 80C
                                                                                                </h4>
                                                                                                <table class="table table-bordered table-hover">
                                                                                                    <thead>
                                                                                                        <tr class="bg-light-blue">
                                                                                                            <th width="20%">Action
                                                                                                            </th>
                                                                                                            <th width="20%">Amount
                                                                                                            </th>
                                                                                                            <th width="20%">Document
                                                                                                            </th>
                                                                                                            <th style="display: none;"></th>
                                                                                                            <th style="display: none;"></th>
                                                                                                            <th width="5%">Download
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
                                                                                                <td width="20%" align="left">
                                                                                                    <asp:ImageButton ID="btnEditRebate" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("RebateHeadId")%>'
                                                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditRebate_Click" />&nbsp;
                                                                           
                                                                                                          <asp:ImageButton ID="btnDeleteRebate" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("RebateHeadId") %>' Visible="false"
                                                                                                              AlternateText="Delete Record" ToolTip="Delete Record" OnClientClick="showConfirmDel(this); return false;" />
                                                                                                </td>

                                                                                                <td width="20%">
                                                                                                    <%# Eval("Amount")%>
                                                                                                </td>

                                                                                                <td width="50%">
                                                                                                    <%# Eval("EmpDocumentName")%>
                                                                                                </td>
                                                                                                <td style="display: none;">

                                                                                                    <asp:Label ID="docURLR80C" runat="server" Text='<%# Eval("EmpDocumentUrl")%>'></asp:Label>

                                                                                                </td>
                                                                                                <td style="display: none;">
                                                                                                    <asp:Label ID="docNAMER80C" runat="server" Text='<%# Eval("DocumentType")%>'></asp:Label>

                                                                                                </td>
                                                                                                <td style="width: 2%; text-align: center;">
                                                                                                    <asp:UpdatePanel ID="updpnldRebate" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:ImageButton ID="btnDownloadRebate" runat="server" ImageUrl="~/IMAGES/downarrow.jpg" Width="25px" Height="25px" TabIndex="14"
                                                                                                                ToolTip="Download" CommandArgument='<%# Eval("RebateHeadId") %>' CommandName='<%# Eval("EmpDocumentUrl") %>'
                                                                                                                OnClick="btnDownloadRebate_Click" />
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:PostBackTrigger ControlID="btnDownloadRebate" />
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>

                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>

            <script type="text/javascript">

                function validateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = '';
                        txt.focus = true;
                        alert("Only Numeric Characters allowed !");
                        return false;
                    }
                    else
                        return true;
                }
            </script>



            <script type="text/javascript" language="javascript">
                function update(obj) {

                    try {
                        var mvar = obj.split('¤');
                        document.getElementById(mvar[1]).value = mvar[0];
                        document.getElementById('ctl00_ctp_hdnId').value = mvar[0] + "  ";
                        setTimeout('__doPostBack(\'' + mvar[1] + '\',\'\')', 0);
                        //document.forms.submit;
                    }
                    catch (e) {
                        alert(e);
                    }
                }


                function GetEmpName(source, eventArgs) {
                    var idno = eventArgs.get_value().split("*");
                    var Name = idno[0].split("-");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName').value = Name[0];
                    document.getElementById('ctl00_ContentPlaceHolder1_hfEmpName').value = idno[1];
                }


                function toggleExpansion(imageCtl, divId) {
                    if (document.getElementById(divId).style.display == "block") {
                        document.getElementById(divId).style.display = "none";
                        imageCtl.src = "../../images/expand_blue.jpg";
                    }
                    else if (document.getElementById(divId).style.display == "none") {
                        document.getElementById(divId).style.display = "block";
                        imageCtl.src = "../../images/collapse_blue.jpg";
                    }
                }

                //function VIAMT1(vall) {

                //    // alert('1');
                //    var fixamt = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm1').value;
                //    // alert(fixamt);
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI1').value = fixamt;
                //    //alert('2');
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);

                //}
                //function VIAMT2(vall) {

                //    //alert('1');
                //    var fixamt1 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm2').value;

                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI2').value = fixamt1;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT3(vall) {

                //    //alert('1');
                //    var fixamt2 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm3').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI3').value = fixamt2;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT4(vall) {

                //    //alert('1');
                //    var fixamt3 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm4').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI4').value = fixamt3;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT5(vall) {

                //    //alert('1');
                //    var fixamt4 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm5').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI5').value = fixamt4;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT6(vall) {

                //    //alert('1');
                //    var fixamt5 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm6').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI6').value = fixamt5;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT7(vall) {

                //    //alert('1');
                //    var fixamt6 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm7').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI7').value = fixamt6;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT8(vall) {

                //    //alert('1');
                //    var fixamt7 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm8').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI8').value = fixamt7;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT9(vall) {

                //    //alert('1');
                //    var fixamt8 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm9').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI9').value = fixamt8;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT10(vall) {

                //    //alert('1');
                //    var fixamt9 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm10').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI10').value = fixamt9;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT11(vall) {

                //    //alert('1');
                //    var fixamt10 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm11').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI11').value = fixamt10;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT12(vall) {

                //    //alert('1');
                //    var fixamt11 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm12').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI12').value = fixamt11;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}
                //function VIAMT13(vall) {

                //    //alert('1');
                //    var fixamt12 = document.getElementById('ctl00_ContentPlaceHolder1_txtVIAAm13').value;
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtActualAmtVI13').value = fixamt12;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
                //}

                function submitPopup(btnsearch) {
                    var rbText;
                    var searchtxt;
                    __doPostBack(btnsearch);
                    return true;
                }

                function submitPerquisite(btnSavePerquisite) {
                    __doPostBack(btnSavePerquisite);
                    return true;
                }


                function check(me) {
                    var val1 = 0.00;
                    var val2 = 0.00;
                    var val3 = 0.00;
                    var val4 = 0.00;
                    var val5 = 0.00;
                    var totalval = 0.00;
                    val1 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtINTRAm1").value);
                    val2 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtINTRAm2").value);
                    val3 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtINTRAm3").value);
                    val4 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtINTRAm4").value);
                    val5 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtINTRAm5").value);
                    totalval = val1 + val2 + val3 + val4 + val5;
                    document.getElementById("ctl00_ContentPlaceHolder1_txtINTRTotal").value = totalval;
                }

                function Increment(vall) {
                    // alert(1);
                    var st = vall.id.split("lvEmp_ctrl");
                    var i = st[1].split("_txtVALUE3");
                    var index = i[0];
                    var val3 = document.getElementById('ctl00_ContentPlaceHolder1_lvEmp_ctrl' + index + '_txtVALUE3').value;
                    var val4 = document.getElementById('ctl00_ContentPlaceHolder1_lvEmp_ctrl' + index + '_txtVALUE4').value;
                    // alert(val3);
                    // alert(val4);
                    var val5 = (Number(val3).toFixed(2) - Number(val4).toFixed(2));
                    document.getElementById('ctl00_ContentPlaceHolder1_lvEmp_ctrl' + index + '_txtVALUE5').value = val5.toFixed(0);
                }

                function Increment2(vall) {
                    // alert(1);
                    var st = vall.id.split("lvEmp_ctrl");
                    var i = st[1].split("_txtVALUE4");
                    var index = i[0];
                    var val3 = document.getElementById('ctl00_ContentPlaceHolder1_lvEmp_ctrl' + index + '_txtVALUE3').value;
                    var val4 = document.getElementById('ctl00_ContentPlaceHolder1_lvEmp_ctrl' + index + '_txtVALUE4').value;
                    // alert(val3);
                    // alert(val4);
                    var val5 = (Number(val3).toFixed(2) - Number(val4).toFixed(2));
                    document.getElementById('ctl00_ContentPlaceHolder1_lvEmp_ctrl' + index + '_txtVALUE5').value = val5.toFixed(0);
                }

            </script>

            <script type="text/javascript" language="javascript">
                function validateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = '';
                        txt.focus = true;
                        alert("Only Numeric Characters allowed !");
                        return false;
                    }
                    else
                        return true;
                }
            </script>

            <script type="text/javascript" language="javascript">
                function ChapterVIAListView(vall) {

                    // alert('1');
                    var st = vall.id.split("lvVIAHeads_ctrl");
                    var i = st[1].split("_txtAmount");
                    var index = i[0];

                    var amount = document.getElementById('ctl00_ContentPlaceHolder1_lvVIAHeads_ctrl' + index + '_txtAmount').value;
                    document.getElementById('ctl00_ContentPlaceHolder1_lvVIAHeads_ctrl' + index + '_txtActualAmount').value = amount;
                }

            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddDocument" />
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddRebate" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

