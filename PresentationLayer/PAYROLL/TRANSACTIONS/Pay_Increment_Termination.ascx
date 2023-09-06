<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pay_Increment_Termination.ascx.cs"
    Inherits="PayRoll_Pay_Increment_Termination" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
<br />
<div class="row">
    <div class="col-md-12">
        <form role="form">
            <div class="box-body">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="panel panel-info">
                                <div class="panel panel-heading">Transaction Type Details</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-6">
                                        <label>Sq.No :</label>
                                        <asp:TextBox ID="txtSqNo" runat="server" CssClass="form-control" ToolTip="Enter Sequence Number" MaxLength="5"
                                            onkeyup="return validateNumeric(this);" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSqNo" runat="server" ControlToValidate="txtSqNo"
                                            Display="None" ErrorMessage="Please Enter Sq.No" ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Transaction Type :</label>
                                        <asp:DropDownList ID="ddlTransactionType" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" ToolTip="Select Transaction Type" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfTransactionType" runat="server" ControlToValidate="ddlTransactionType"
                                            Display="None" ErrorMessage="Please Select Transaction Type" ValidationGroup="ServiceBook"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Ord.Eff. Date :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgOrderEffectiveDate" runat="server" ImageUrl="~/images/calendar.png"
                                                    Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtOrderEffectiveDate" runat="server" CssClass="form-control" ToolTip="Enter Order Eff.Date"
                                                TabIndex="6" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceOrderEffectiveDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtOrderEffectiveDate" PopupButtonID="imgOrderEffectiveDate"
                                                Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <%--<asp:RequiredFieldValidator ID="rfvOrderEffectiveDate" runat="server" ControlToValidate="txtOrderEffectiveDate"
                                                        Display="None" ErrorMessage="Please Select Order Effective Date in (dd/MM/yyyy Format)"
                                                        ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:MaskedEditExtender ID="meOrderEffectiveDate" runat="server" TargetControlID="txtOrderEffectiveDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevOrderEffectiveDate" runat="server" ControlExtender="meOrderEffectiveDate"
                                                ControlToValidate="txtOrderEffectiveDate" EmptyValueMessage="Please Enter Order Effective Date"
                                                InvalidValueMessage="Order Effective Date is Invalid (Enter mm/dd/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter OrderEffective Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Department :</label>
                                        <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                            ToolTip="Select Department" TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select Department" ValidationGroup="Leave"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label></label>
                                        <label>Designation :</label>
                                        <asp:DropDownList ID="ddlDesignation" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" ToolTip="Select Designation" TabIndex="8">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation"
                                            Display="None" ErrorMessage="Please Select Designation" ValidationGroup="Leave"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Order.No :</label>
                                        <asp:TextBox ID="txtOredrNo" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Enter Order.No"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvOredrNo" runat="server" ControlToValidate="txtOredrNo"
                                                    Display="None" ErrorMessage="Please Enter Oredr. No" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Order Date :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgOrderDate" runat="server" ImageUrl="~/images/calendar.png"
                                                    Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" ToolTip="Enter Order Date"
                                                TabIndex="10" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceOrderDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtOrderDate" PopupButtonID="imgOrderDate" Enabled="true" EnableViewState="true"
                                                PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <%--<asp:RequiredFieldValidator ID="rfvOrderDate" runat="server" ControlToValidate="txtOrderDate"
                                                        Display="None" ErrorMessage="Please Select Order Date in (dd/MM/yyyy Format)"
                                                        ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:MaskedEditExtender ID="meOrderDate" runat="server" TargetControlID="txtOrderDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevOrderDate" runat="server" ControlExtender="meOrderDate"
                                                ControlToValidate="txtOrderDate" EmptyValueMessage="Please Enter Order Date"
                                                InvalidValueMessage="Order Date is Invalid (Enter mm/dd/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Order Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6" id="grGRNO" runat="server" visible="false">
                                        <label>Gr.No. :</label>
                                        <asp:TextBox ID="txtGrNo" runat="server" CssClass="form-control" ToolTip="Enter Gr.No" TabIndex="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvGrNo" runat="server" ControlToValidate="txtGrNo"
                                            Display="None" ErrorMessage="Please Enter Gr.No." ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6" id="Tr1GRDA" runat="server" visible="false">
                                        <label>Gr. Date :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgGrDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtGrDate" runat="server" CssClass="form-control" ToolTip="Enter Gr.Date"
                                                TabIndex="12" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceGrDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtGrDate"
                                                PopupButtonID="imgGrDate" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvGrDate" runat="server" ControlToValidate="txtGrDate"
                                                Display="None" ErrorMessage="Please Select Gr. Date in (dd/MM/yyyy Format)" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="meGrDate" runat="server" TargetControlID="txtGrDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevGrDate" runat="server" ControlExtender="meGrDate"
                                                ControlToValidate="txtGrDate" EmptyValueMessage="Please Enter Gr. Date" InvalidValueMessage="Gr . Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter Gr. Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Pay Allowance :</label>
                                        <asp:TextBox ID="txtPayAllowance" runat="server" CssClass="form-control" TabIndex="13"
                                            onkeyup="return validateNumeric(this);" ToolTip="Enter Pay Allowance"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvPayAllowance" runat="server" ControlToValidate="txtPayAllowance"
                                                Display="None" ErrorMessage="Please Enter PayAllowance" ValidationGroup="ServiceBook"
                                             SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Ter./Ret. Date :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgTerRet" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtTerRet" runat="server" ToolTip="Enter Ter./Ret. Date" CssClass="form-control"
                                                TabIndex="14" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceTerRet" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTerRet"
                                                PopupButtonID="imgTerRet" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <%--<asp:RequiredFieldValidator ID="rfvTerRet" runat="server" ControlToValidate="txtGrDate"
                                                    Display="None" ErrorMessage="Please Select Termination/Retirement Date in (dd/MM/yyyy Format)"
                                                        ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:MaskedEditExtender ID="meTerRet" runat="server" TargetControlID="txtTerRet"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevTerRet" runat="server" ControlExtender="meTerRet"
                                                ControlToValidate="txtTerRet" EmptyValueMessage="Please Enter Temination/Retirement Date"
                                                InvalidValueMessage="Temination/Retirement Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter Temination/Retirement Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Scale :</label>
                                        <asp:DropDownList ID="ddlScale" AppendDataBoundItems="true" runat="server" CssClass="form-control" ToolTip="Select Scale"
                                            TabIndex="15">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvddlScale" runat="server" ControlToValidate="ddlScale"
                                                 Display="None" ErrorMessage="Please Select Scale" ValidationGroup="Leave" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Remarks :</label>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" ToolTip="Enter Remarks If Any"
                                            TextMode="MultiLine" TabIndex="16"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemarks"
                                                    Display="None" ErrorMessage="Please Enter Remarks" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Upload Document :</label>
                                        <br />
                                        <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="17" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="18"
                                                OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit"/>&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="19"
                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset"/>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-6">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvServiceBook" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Transaction Type Details"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">
                                        Increment / Termination
                                    </div>
                                    </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Ord.Eff.Date
                                                    </th>
                                                    <th>Dept
                                                    </th>
                                                    <th>Desig
                                                    </th>
                                                    <th>Ord.No.
                                                    </th>
                                                    <th>Ord.Date
                                                    </th>
                                                    <th>Attachment
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>                                  
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("TRNO")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("TRNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("OrdEffDt", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("subdept")%>
                                        </td>
                                        <td>
                                            <%# Eval("SubDesig")%>
                                        </td>
                                        <td>
                                            <%# Eval("OrderNo")%>
                                        </td>
                                        <td>
                                            <%# Eval("ORDERDT", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("TRNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </form>
    </div>
</div>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>&nbsp
        </td>
    </tr>
    <tr>
        <td valign="top" width="50%">
            <%--<asp:Panel ID="pnlAdd" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Transaction Type Details</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label">Sq.No :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtSqNo" runat="server" Width="50px" MaxLength="5" onkeyup="return validateNumeric(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSqNo" runat="server" ControlToValidate="txtSqNo"
                                    Display="None" ErrorMessage="Please Enter Sq.No" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Transaction Type :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlTransactionType" AppendDataBoundItems="true" runat="server"
                                    Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfTransactionType" runat="server" ControlToValidate="ddlTransactionType"
                                    Display="None" ErrorMessage="Please Select Transaction Type" ValidationGroup="ServiceBook"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Ord.Eff. Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtOrderEffectiveDate" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="imgOrderEffectiveDate" runat="server" ImageUrl="~/images/calendar.png"
                                    Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceOrderEffectiveDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtOrderEffectiveDate" PopupButtonID="imgOrderEffectiveDate"
                                    Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>--%>
            <%--ALready COmmitted<asp:RequiredFieldValidator ID="rfvOrderEffectiveDate" runat="server" ControlToValidate="txtOrderEffectiveDate"
                                    Display="None" ErrorMessage="Please Select Order Effective Date in (dd/MM/yyyy Format)"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--<ajaxToolKit:MaskedEditExtender ID="meOrderEffectiveDate" runat="server" TargetControlID="txtOrderEffectiveDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevOrderEffectiveDate" runat="server" ControlExtender="meOrderEffectiveDate"
                                    ControlToValidate="txtOrderEffectiveDate" EmptyValueMessage="Please Enter Order Effective Date"
                                    InvalidValueMessage="Order Effective Date is Invalid (Enter mm/dd/yyyy Format)"
                                    Display="None" TooltipMessage="Please Enter OrderEffective Date" EmptyValueBlurredText="Empty"
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Department :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" runat="server" Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                    Display="None" ErrorMessage="Please Select Department" ValidationGroup="Leave"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Designation :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlDesignation" AppendDataBoundItems="true" runat="server"
                                    Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation"
                                    Display="None" ErrorMessage="Please Select Designation" ValidationGroup="Leave"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Order.No :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtOredrNo" runat="server" Width="195px"></asp:TextBox>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvOredrNo" runat="server" ControlToValidate="txtOredrNo"
                                    Display="None" ErrorMessage="Please Enter Oredr. No" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--</td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Order Date
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtOrderDate" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="imgOrderDate" runat="server" ImageUrl="~/images/calendar.png"
                                    Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceOrderDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtOrderDate" PopupButtonID="imgOrderDate" Enabled="true" EnableViewState="true"
                                    PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>--%>
            <%--Already COmmitted<asp:RequiredFieldValidator ID="rfvOrderDate" runat="server" ControlToValidate="txtOrderDate"
                                    Display="None" ErrorMessage="Please Select Order Date in (dd/MM/yyyy Format)"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--<ajaxToolKit:MaskedEditExtender ID="meOrderDate" runat="server" TargetControlID="txtOrderDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevOrderDate" runat="server" ControlExtender="meOrderDate"
                                    ControlToValidate="txtOrderDate" EmptyValueMessage="Please Enter Order Date"
                                    InvalidValueMessage="Order Date is Invalid (Enter mm/dd/yyyy Format)" Display="None"
                                    TooltipMessage="Please Enter Order Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr id="grGRNO" runat="server" visible="false">
                            <td class="form_left_label">Gr.No. :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtGrNo" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvGrNo" runat="server" ControlToValidate="txtGrNo"
                                    Display="None" ErrorMessage="Please Enter Gr.No." ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Tr1GRDA" runat="server" visible="false">
                            <td class="form_left_label">Gr. Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtGrDate" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="imgGrDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceGrDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtGrDate"
                                    PopupButtonID="imgGrDate" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvGrDate" runat="server" ControlToValidate="txtGrDate"
                                    Display="None" ErrorMessage="Please Select Gr. Date in (dd/MM/yyyy Format)" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <ajaxToolKit:MaskedEditExtender ID="meGrDate" runat="server" TargetControlID="txtGrDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevGrDate" runat="server" ControlExtender="meGrDate"
                                    ControlToValidate="txtGrDate" EmptyValueMessage="Please Enter Gr. Date" InvalidValueMessage="Gr . Date is Invalid (Enter dd/MM/yyyy Format)"
                                    Display="None" TooltipMessage="Please Enter Gr. Date" EmptyValueBlurredText="Empty"
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Pay Allowance :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtPayAllowance" runat="server" Width="80px" onkeyup="return validateNumeric(this);"></asp:TextBox>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvPayAllowance" runat="server" ControlToValidate="txtPayAllowance"
                                    Display="None" ErrorMessage="Please Enter PayAllowance" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--</td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Ter./Ret. Date
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtTerRet" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="imgTerRet" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceTerRet" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTerRet"
                                    PopupButtonID="imgTerRet" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvTerRet" runat="server" ControlToValidate="txtGrDate"
                                    Display="None" ErrorMessage="Please Select Termination/Retirement Date in (dd/MM/yyyy Format)"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--<ajaxToolKit:MaskedEditExtender ID="meTerRet" runat="server" TargetControlID="txtTerRet"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevTerRet" runat="server" ControlExtender="meTerRet"
                                    ControlToValidate="txtTerRet" EmptyValueMessage="Please Enter Temination/Retirement Date"
                                    InvalidValueMessage="Temination/Retirement Date is Invalid (Enter dd/MM/yyyy Format)"
                                    Display="None" TooltipMessage="Please Enter Temination/Retirement Date" EmptyValueBlurredText="Empty"
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Scale :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlScale" AppendDataBoundItems="true" runat="server" Width="250px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvddlScale" runat="server" ControlToValidate="ddlScale"
                                    Display="None" ErrorMessage="Please Select Scale" ValidationGroup="Leave" InitialValue="0"></asp:RequiredFieldValidator>--%>
            <%--</td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Remarks
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="200px" Height="50px" TextMode="MultiLine"></asp:TextBox>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemarks"
                                    Display="None" ErrorMessage="Please Enter Remarks" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--</td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Upload Document :
                            </td>
                            <td class="form_left_text">
                                <asp:FileUpload ID="flupld" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook"
                                    OnClick="btnSubmit_Click" Width="80px" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" Width="80px" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>--%>
        </td>
        <td colspan="2" align="center" valign="top">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <%--<asp:ListView ID="lvServiceBook" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Transaction Type Details"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Increment / Termination
                                    </div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">Action
                                                </th>
                                                <th width="10%">Ord.Eff.Date
                                                </th>
                                                <th width="10%">Dept
                                                </th>
                                                <th width="10%">Desig
                                                </th>
                                                <th width="10%">Ord.No.
                                                </th>
                                                <th width="10%">Ord.Date
                                                </th>
                                                <th width="15%" align="left">Attachment
                                                </th>
                                            </tr>
                                            <thead>
                                    </table>
                                </div>
                                <div class="listview-container-servicebook">
                                    <div id="Div1" class="vista-gridServiceBook">
                                        <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td width="10%" align="left">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("TRNO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("TRNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("OrdEffDt", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("subdept")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("SubDesig")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("OrderNo")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("ORDERDT", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("TRNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'"
                                    onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("TRNO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("TRNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("OrdEffDt", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("subdept")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("SubDesig")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("OrderNo")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("ORDERDT", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("TRNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
</table>
<%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
<%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
    runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
    OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
    BackgroundCssClass="modalBackground" />
<div class="col-md-12">
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
</div>

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

    function validateAlphabet(txt) {
        var expAlphabet = /^[A-Za-z]+$/;
        if (txt.value.search(expAlphabet) == -1) {
            txt.value = txt.value.substring(0, (txt.value.length) - 1);
            txt.value = '';
            txt.focus = true;
            alert("Only Alphabets allowed!");
            return false;
        }
        else
            return true;
    }
</script>

