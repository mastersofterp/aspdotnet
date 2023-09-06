<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MiscFeeDescription.aspx.cs" Inherits="ACADEMIC_MASTERS_MiscFeeDescription"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" language="javascript" src="../../Javascripts/FeeCollection.js"></script>--%>


    <%--<script type="text/javascript" language="javascript" src="../../includes/prototype.js"></script>

    <script type="text/javascript" language="javascript" src="../../includes/scriptaculous.js"></script>

    <script type="text/javascript" language="javascript" src="../../includes/modalbox.js"></script>--%>
    <%--<table cellpadding="2" cellspacing="2" border="0" width="100%">
       
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                MISCELLANIOUS FEES
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
    <style>
        .dataTables_scrollHeadInner 
        {
            width: max-content!important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">MISCELLANEOUS FEES</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Cash Book</label>
                                        </div>
                                        <asp:DropDownList ID="ddlMisc" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlMisc_SelectedIndexChanged" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMisc"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Miscellanious Fees."
                                            InitialValue="0" />
                                    </div>
                                     <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                      <label>Student Receipt For</label>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                            <asp:RadioButtonList ID="rbdstudent" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbdstudent_SelectedIndexChanged">
                                                <asp:ListItem Value="0"> Internal &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value=1> External</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Transaction Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPaymentType" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged">
                                            <asp:ListItem Value="C">Please Select</asp:ListItem>
                                            <asp:ListItem Value="R">Receipt</asp:ListItem>
                                            <asp:ListItem Value="P">Payment</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPaymentType"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Payment Mode."
                                            InitialValue="0" />
                                    </div>
                                   

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt No</label>
                                        </div>
                                        <asp:Label ID="lblReceiptNo" runat="server" Font-Bold="True" ForeColor="Red" BackColor="White"></asp:Label>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Date</label>
                                        </div>
                                        <div class="input-group">
                                            <span class="input-group-addon" id="imgCalDDDate">
                                                <span class="fa fa-calendar"></span>
                                            </span>
                                            <asp:TextBox ID="txtReceiptDate" TabIndex="3" CssClass="form-control" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReceiptDate"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Date." />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtReceiptDate"
                                                PopupButtonID="imgCalDDDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtReceiptDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                OnInvalidCssClass="errordate" />
                                            <%--<ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                                    ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Demand draft date is required"
                                                    InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                                    InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />--%>
                                        </div>
                                    </div>
                                </div>
                                <%--     <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="usnno" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtStudIdno" TabIndex="4" CssClass="form-control" runat="server" Visible="false" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Onblur="blankfield(this);"
                                            runat="server" ControlToValidate="txtStudIdno" ValidationGroup="Submit" Display="None"
                                            ErrorMessage="Please enter Student Idno." />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:Button ID="btnShow" CssClass="btn btn-primary" Style="margin-top: 15px;" TabIndex="5" runat="server" Visible="false" Text="Show" OnClick="btnShow_Click" />

                                    </div>
                                </div>--%>


                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row" id="usnno" runat="server" visible="false">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Search Criteria</label>
                                                </div>

                                                <%--onchange=" return ddlSearch_change();"--%>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlSearchPanel" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchPanel_SelectedIndexChanged"
                                                    AppendDataBoundItems="true" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfidno" runat="server" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                <asp:Panel ID="pnltextbox" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search String</label>
                                                    </div>
                                                   <%-- <>--%>
                                                    <div id="divtxt" runat="server" style="display: block">
                                                        <asp:TextBox ID="txtSearchPanel" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlDropdown" runat="server">
                                                    <div id="divDropDown" runat="server" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div class="form-group col-lg-3 col-12">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                <asp:Button ID="btnSearchPanel" runat="server" Text="Search" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSearchPanel_Click" />
                                                <asp:Button ID="btnClosePanel" runat="server" Text="Clear" CausesValidation="false" CssClass="btn btn-warning" OnClick="btnClosePanel_Click" />

                                                <asp:ValidationSummary ID="ValidationSummary2" DisplayMode="List" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                  
                                </asp:UpdatePanel>




                                <div class="row mt-4">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Name</label>
                                        </div>
                                        <asp:TextBox ID="txtStudName" TabIndex="6" CssClass="form-control" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStudName"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Student Name." />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Head</label>
                                        </div>
                                        <asp:DropDownList ID="ddlHead" TabIndex="7" ToolTip="Please Select Cash Book to Show Head" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem>Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlHead"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Head." InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtAmount" TabIndex="8" CssClass="form-control" runat="server" onkeyup="IsNumeric(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAmount"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Amount." />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" TabIndex="9" CssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click"
                                            CausesValidation="False" />
                                        <asp:ValidationSummary ID="validationsummary" runat="server" EnableTheming="true"
                                            ShowMessageBox="true" ShowSummary="false" />
                                        <asp:HiddenField ID="hfcount" runat="server" Value="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Total Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtTotalAmount" TabIndex="10" Enabled="false" CssClass="form-control" runat="server" Text="0.00"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTotalAmount"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Amount Name." />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Pay Type (C/D/T)</label>
                                        </div>
                                        <asp:TextBox ID="txtPayType" onblur="ValidatePayType(this); UpdateCash_DD_Amount();" onchange="BindAmt()"
                                            runat="server" MaxLength="1" TabIndex="11" ToolTip="Enter C for cash payment OR D for payment by demand draft."
                                            CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="valPayType" runat="server" ControlToValidate="txtPayType"
                                            Display="None" ErrorMessage="Please enter type of payment whether cash(C) or demand draft(D)."
                                            SetFocusOnError="true" ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtNarration" runat="server" TabIndex="12" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtNarration"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Narration." />--%>
                                    </div>
                                </div>

                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" TabIndex="13" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReport" Visible="false" CssClass="btn btn-info" runat="server" Text="Report"
                                    OnClick="btnReport_Click" />
                                <asp:Button ID="btnCancel" TabIndex="14" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="validationsummary1" runat="server" EnableTheming="true"
                                    ShowMessageBox="true" ShowSummary="false" />
                                <asp:HiddenField ID="hdnAmt" runat="server" />
                            </div>

                            <div class="col-12">
                                <div id="divDDDetails" runat="server" style="display: none;">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-12">
                                            <div class="sub-heading">
                                                <h5>Demand Draft/Cheque/Transfer Reference Details</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>DD/Cheque/Trans-Ref No.</label>
                                            </div>
                                            <asp:TextBox ID="txtDDNo" runat="server" TabIndex="15" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="valDDNo" ControlToValidate="txtDDNo" runat="server"
                                                Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Date</label>
                                            </div>
                                            <div class="input-group">
                                                <span class="input-group-addon" id="Span1">
                                                    <span class="fa fa-calendar"></span>
                                                </span>
                                                <asp:TextBox ID="txtDDDate" TabIndex="3" CssClass="form-control" runat="server" />
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDDDate"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Date." />--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDDDate"
                                                    PopupButtonID="Span1" />
                                                <ajaxToolKit:MaskedEditExtender ID="mevDDDate" runat="server" TargetControlID="txtDDDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                    OnInvalidCssClass="errordate" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtDDAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="17" ONCHANGE="BindAmt(THIS)"
                                                CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="valDdAmount" ControlToValidate="txtDDAmount" runat="server"
                                                Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>City</label>
                                            </div>
                                            <asp:TextBox ID="txtDDCity" runat="server" TabIndex="18" CssClass="data_label" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Bank</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" TabIndex="19" runat="server"
                                                CssClass="form-control" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlBank"
                                                Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                InitialValue="0" SetFocusOnError="true" />
                                            <%--<td>
                                                    <asp:Button ID="btnSaveDD_Info" runat="server" Text="Save Demand Draft" OnClick="btnSaveDD_Info_Click"
                                                        ValidationGroup="dd_info" TabIndex="11" />
                                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="dd_info" />
                                                </td>--%>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div id="divTDetails" runat="server" style="display: none;">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divTrans">
                                            <div class="label-dynamic">
                                                <label>Transaction Id/Reff. No.</label>
                                            </div>
                                            <asp:TextBox ID="txtTransReff" runat="server" CssClass="data_label" MaxLength="20" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtTransReff" runat="server"
                                                Display="None" ErrorMessage="Please enter ." ValidationGroup="dd_info" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divBank">
                                            <div class="label-dynamic">
                                                <label>Bank</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBankT" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" id="trListview" runat="server">
                                    <asp:HiddenField ID="hdnCount" Value="0" runat="server" />
                                    <asp:ListView ID="lvStudentList" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYRNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Student Name.
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <%-- <th>
                                                        Name
                                                    </th>
                                                    <th>
                                                        Status
                                                    </th>
                                                    <th>
                                                        Amount
                                                    </th>--%>
                                                        <%--<th>
                                                        Cancelled
                                                    </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <%-- DO NOT FORMAT FOLLOWING 5-6 LINES. JAVASCRIPT IS DEPENDENT ON ELEMENT HIERARCHY --%>
                                            <tr class="item">
                                                <td>
                                                    <%--<asp:RadioButtonList ID ="rdoSelectRecord"  RepeatDirection="Vertical" runat = "server" ToolTip ='<%# Eval("MISCDCRSRNO") %>'></asp:RadioButtonList>--%>
                                                    <input id="rdoSelectRecord" value='<%# Container.DataItemIndex%>' name="Receipts" type="radio"
                                                        onclick="ShowRemark(this);" />
                                                    <asp:HiddenField ID="hidRemark" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    <asp:HiddenField ID="hidDcrNo" runat="server" Value='<%# Eval("NAME") %>' />
                                                    <%-- <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />--%>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>
                                                    <%--<asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("NAME") %>' />--%>
                                                </td>
                                                <td>
                                                    <%# Eval("SHORTNAME") %>
                                                </td>
                                                <%-- <td>
                                                <%# Eval("NAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("STATUS")%>
                                            </td>
                                            <td>
                                                <%# Eval("CHDDAMT")%>
                                            </td>--%>
                                                <%--<td>
                                                <asp:Label ID="lblStatus" runat="server" Text = '<%# Eval("CANCELLED") %>' ToolTip = '<%# Eval("CAN") %>' />
                                            </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="ListView1" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Miscellaneous Detailt</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="head">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th></th>
                                                        <th>Head
                                                        </th>
                                                        <th>Amount
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="dd" class="item">
                                                <%--<td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                    CommandArgument='<%# Eval("MISCHEADSRNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" TabIndex="10" />
                                            </td>--%>
                                                <td>
                                                    <asp:ImageButton ID="btnDeleteDDInfo" runat="server" OnClick="btnDeleteDDInfo_Click"
                                                        CommandArgument='<%# Eval("DD_NO") %>' ImageUrl="~/images/delete.png" ToolTip="Delete Record" TabIndex="14" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblHead" runat="server" Text='<%# Eval("MISCHEAD").ToString()%>' ToolTip='<%# Eval("MISCHEADCODE") %>'></asp:Label>
                                                </td>
                                                <td id="amt">
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT").ToString()%>'></asp:Label>
                                                    <asp:HiddenField ID="hfamt" runat="server" Value='<%# Eval("AMOUNT").ToString()%>' />
                                                </td>
                                                <%--   <td>
                                                    <%# ((Eval("UA_FULLNAME").ToString() != string.Empty) ? Eval("UA_FULLNAME") : "--") %>
                                                </td>
                                                <td>
                                                    <%# ((Eval("RECEIPT_PERMISSION").ToString() != string.Empty) ? Eval("RECEIPT_PERMISSION") : "--")%>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5 style="display: none">GST PAYMENT</h5>
                                    </div>
                                    <div class="col-12" id="idchk" runat="server" visible="false">
                                        <asp:CheckBox ID="chkgstpay" runat="server" OnCheckedChanged="chkgstpay_CheckedChanged" AutoPostBack="true" />
                                        <span style="color: black">GST</span>
                                    </div>

                                    <div class="row" id="divcgst" runat="server" visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    <asp:Label ID="lblcgst" runat="server" Text="CGST"></asp:Label><span></label>
                                            </div>
                                            <asp:Label ID="lblcgper" Style="color: black" runat="server"></asp:Label><span style="color: black">%</span>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    <asp:Label ID="lblsgst" runat="server" Text="SGST"></asp:Label></label>
                                            </div>
                                            <asp:Label ID="lblsgper" Style="color: black" runat="server"></asp:Label><span style="color: black">%</span>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Total Amount(Rs)</label>
                                            </div>
                                            <asp:TextBox runat="server" ID="lblcgtotal" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMsg" runat="server">
                </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="btnDelete_Click"/>
            <asp:PostBackTrigger ControlID="lvCourse" />
        </Triggers>--%>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function totamt() {
            var a = document.getElementById('ctl00_ContentPlaceHolder1_ListView1_ctrl0_lblAmount').getElementsByTagName('td');
            for (i = 1; i < dataRows.length; i++) {
                var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                var dataCell = dataCellCollection.item(2);

            }
        }
        function lvamount() {
            alert("hello");
            var a = 0;
            var dataRows = null;

            //if (document.getElementById('ctl00_ContentPlaceHolder1_ListView1_ListView1') != null) {
            dataRows = document.getElementById('dd').getElementsByTagName('tr');
            alert(dataRows.length);
            //}

            for (i = 0; i < dataRows.length; i++) {

                var b = document.getElementById('ctl00_ContentPlaceHolder1_ListView1_ctrl' + i + '_lblAmount').text;
                alert(b);
                a = b + a;
                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value = a;
            }
        }
    function IsNumeric(txt) {
        var ValidChars = "0123456789.";
        var num = true;
        var mChar;
        cnt = 0

        for (i = 0; i < txt.value.length && num == true; i++) {
            mChar = txt.value.charAt(i);

            if (ValidChars.indexOf(mChar) == -1) {
                num = false;
                txt.value = '';
                alert("Please enter Numeric values only")
                txt.select();
                txt.focus();
            }
        }
        return num;
    }

        function totAllSubjects(label) {

            var txtTot = document.getElementById('<%= txtTotalAmount.ClientID %>');
            var hdfTot = document.getElementById('<%= hdnAmt.ClientID %>');
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {

                var e = frm.elements[i];
                if (e.type == 'label') {
                    alert("alert");
                    hdfTot.value;
                }
            }

            if (label != null)
                txtTot.value = hdfTot.value;
            else
                txtTot.value = 0;
        }

        function UpdateTotalAndBalance() {

            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_ListView1_ctrl0_lblAmount') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_ListView1_ctrl0_lblAmount').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < dataRows.length; i++) {
                        alert
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(2);
                        var txtAmt = controls.item(0).value;
                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);
                    }
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value = totalFeeAmt;

                    var totalPaidAmt = 0;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                        totalPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();

                    // if (document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance') != null)
                    //   document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance').value = (parseFloat(totalPaidAmt) - parseFloat(totalFeeAmt));
                }
                //UpdateCash_DD_Amount();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
        function ValidateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters are Allowed.');
                txt.focus();
                return;
            }
        }

        function blankfield(txt) {
            txt.value = '';
            alert('Please Enter IDNO of student');
            txt.focus();
            return;
        }

        function ShowRemark(rdoSelect) {
            //  alert("insHOW");
            var count = rdoSelect.value;
            //alert(count);
            var name = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentList_ctrl' + count + '_hidDcrNo').value;
            // alert(name);

            document.getElementById('ctl00_ContentPlaceHolder1_txtStudName').value = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentList_ctrl' + count + '_hidDcrNo').value;
            //document.getElementById('ctl00_ContentPlaceHolder1_txtStudName').value = name;
            document.getElementById('ctl00_ContentPlaceHolder1_hdfidno').value = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentList_ctrl' + count + '_hidRemark').value;

            document.getElementById('ctl00_ContentPlaceHolder1_trListview').style.display = "none";
        }
        //function DDamount()
        //{
        //    alert("insHOW");
        //    document.getElementById('ctl00$ContentPlaceHolder1$txtTotalAmount').value = document.getElementById('ctl00$ContentPlaceHolder1$txtDDAmount').value
        //}

        function BindAmt() {
            var amt = $('#ctl00_ContentPlaceHolder1_txtTotalAmount').val();
            if (amt > 0)
                $('#ctl00_ContentPlaceHolder1_txtDDAmount').val(amt);
        }
        function ValidatePayType(txtPayType) {
            try {
                if (txtPayType != null && txtPayType.value != '') {
                    if (txtPayType.value.toUpperCase() == 'D') {
                        txtPayType.value = txtPayType.value.toUpperCase();
                        if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "block";
                            document.getElementById('ctl00_ContentPlaceHolder1_divTDetails').style.display = "none";
                            // document.getElementById('txtTotalAmount').value = document.getElementById('txtDDAmount').value;
                            document.getElementById('ctl00_ContentPlaceHolder1_txtDDNo').focus();
                        }
                    }
                    else if (txtPayType.value.toUpperCase() == 'T') {
                        txtPayType.value = "T";
                        if (document.getElementById('ctl00_ContentPlaceHolder1_divTDetails') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_divTDetails').style.display = "block";
                            document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
                            //document.getElementById('ctl00_ContentPlaceHolder1_txtDDNo').focus();
                        }
                    }
                    else {
                        if (txtPayType.value.toUpperCase() == 'C') {
                            txtPayType.value = "C";
                            if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails')) {
                                document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
                                document.getElementById('ctl00_ContentPlaceHolder1_divTDetails').style.display = "none";

                                //document.getElementById('ctl00_ContentPlaceHolder1_divFeeItems').style.display = "block";
                            }
                        }
                        else {
                            alert("Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts OR 'T' for Transfer Payment to Online Transfer");
                            if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null)
                                document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
                            document.getElementById('ctl00_ContentPlaceHolder1_divTDetails').style.display = "none";

                            txtPayType.value = "";
                            txtPayType.focus();
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
        //function ValidatePayType(txtPayType)
        //{
        //    try {
        //        //debugger;
        //        if (txtPayType != null && txtPayType.value != '') {
        //            if (txtPayType.value.toUpperCase() == 'D') {
        //                txtPayType.value = "D";
        //                document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
        //                if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null)
        //                {
        //                    document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "block";
        //                    document.getElementById('ctl00_ContentPlaceHolder1_txtDDNo').focus();

        //                }
        //            }
        //            else {
        //                if (txtPayType.value.toUpperCase() == 'C')
        //                {

        //                    txtPayType.value = "C";

        //                    if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null &&
        //                        document.getElementById('ctl00_ContentPlaceHolder1_divTDetails') != null)
        //                    {
        //                        document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
        //                        //document.getElementById('ctl00_ContentPlaceHolder1_divFeeItems').style.display = "block";

        //                    }
        //                }
        //                else {
        //                    if (txtPayType.value.toUpperCase() == 'T') {
        //                        txtPayType.value = "T";
        //                        alert("D");
        //                        if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null &&
        //                        document.getElementById('ctl00_ContentPlaceHolder1_divTDetails') != null) {
        //                            document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
        //                            document.getElementById('ctl00_ContentPlaceHolder1_divTDetails').style.display = "block";
        //                            alert("t");
        //                        }
        //                    }
        //                    else {
        //                        alert("Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts OR 'T' for Transfer Payment To Online Transfer.");
        //                        if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null)
        //                            document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
        //                        txtPayType.value = "";
        //                        txtPayType.focus();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (e) {
        //        alert("Error: " + e.description);
        //    }
        //}
    </script>

</asp:Content>
