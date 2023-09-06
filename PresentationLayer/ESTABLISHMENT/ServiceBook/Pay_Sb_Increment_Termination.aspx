<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_Increment_Termination.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_Increment_Termination" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Transaction Type Details (Increment/Termination)</h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <%-- <div class="panel panel-heading">Transaction Type Details</div>--%>

                                            <div class="panel panel-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Sq.No :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSqNo" runat="server" CssClass="form-control" ToolTip="Enter Sequence Number" MaxLength="5"
                                                                onkeyup="return validateNumeric(this);" TabIndex="1"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvSqNo" runat="server" ControlToValidate="txtSqNo"
                                                        Display="None" ErrorMessage="Please Enter Sq.No" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Transaction Type :</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlTransactionType" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" ToolTip="Select Transaction Type" TabIndex="2" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfTransactionType" runat="server" ControlToValidate="ddlTransactionType"
                                                                Display="None" ErrorMessage="Please Select Transaction Type" ValidationGroup="ServiceBook"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Ord.Eff. Date :</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgOrderEffectiveDate" runat="server" class="fa fa-calendar text-blue" />
                                                                </div>

                                                                <asp:TextBox ID="txtOrderEffectiveDate" runat="server" CssClass="form-control" ToolTip="Enter Order Eff.Date"
                                                                    TabIndex="3" Style="z-index: 0;"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceOrderEffectiveDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtOrderEffectiveDate" PopupButtonID="imgOrderEffectiveDate"
                                                                    Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvOrderEffectiveDate" runat="server" ControlToValidate="txtOrderEffectiveDate"
                                                                    Display="None" ErrorMessage="Please Select Order Effective Date in (dd/MM/yyyy Format)"
                                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="meOrderEffectiveDate" runat="server" TargetControlID="txtOrderEffectiveDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevOrderEffectiveDate" runat="server" ControlExtender="meOrderEffectiveDate"
                                                                    ControlToValidate="txtOrderEffectiveDate" EmptyValueMessage="Please Enter Order Effective Date"
                                                                    InvalidValueMessage="Order Effective Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter OrderEffective Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Department :</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                ToolTip="Select Department" TabIndex="4">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                                        Display="None" ErrorMessage="Please Select Department" ValidationGroup="Leave"
                                                        InitialValue="0"></asp:RequiredFieldValidator>--%>

                                                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="ServiceBook"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label></label>
                                                                <sup>*</sup>
                                                                <label>Designation :</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDesignation" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Designation" TabIndex="5">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation"
                                                                Display="None" ErrorMessage="Please Select Designation" ValidationGroup="ServiceBook"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation"
                                                        Display="None" ErrorMessage="Please Select Designation" ValidationGroup="Leave"
                                                        InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Order.No :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOredrNo" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Enter Order.No"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvOredrNo" runat="server" ControlToValidate="txtOredrNo"
                                                    Display="None" ErrorMessage="Please Enter Oredr. No" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Order Date :</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgOrderDate" runat="server" class="fa fa-calendar text-blue"
                                                                        Style="cursor: pointer" />
                                                                </div>




                                                                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" ToolTip="Enter Order Date"
                                                                    TabIndex="7" Style="z-index: 0;"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceOrderDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtOrderDate" PopupButtonID="imgOrderDate" Enabled="true" EnableViewState="true"
                                                                    PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvOrderDate" runat="server" ControlToValidate="txtOrderDate"
                                                                    Display="None" ErrorMessage="Please Select Order Date in (dd/MM/yyyy Format)"
                                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="meOrderDate" runat="server" TargetControlID="txtOrderDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevOrderDate" runat="server" ControlExtender="meOrderDate"
                                                                    ControlToValidate="txtOrderDate" EmptyValueMessage="Please Enter Order Date"
                                                                    InvalidValueMessage="Order Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                    TooltipMessage="Please Enter Order Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                    ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-6" id="grGRNO" runat="server" visible="false">
                                                            <label>Gr.No. :</label>
                                                            <asp:TextBox ID="txtGrNo" runat="server" CssClass="form-control" ToolTip="Enter Gr.No" TabIndex="8"></asp:TextBox>
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
                                                                    TabIndex="9" Style="z-index: 0;"></asp:TextBox>
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
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                            </div>
                                                            <label>Pay Allowance :</label>
                                                            <asp:TextBox ID="txtPayAllowance" runat="server" CssClass="form-control" TabIndex="10"
                                                                onkeyup="return validateNumeric(this);" ToolTip="Enter Pay Allowance"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvPayAllowance" runat="server" ControlToValidate="txtPayAllowance"
                                                Display="None" ErrorMessage="Please Enter PayAllowance" ValidationGroup="ServiceBook"
                                             SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Incre./Ter./Ret. Date :</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgTerRet" runat="server" class="fa fa-calendar text-blue" />
                                                                </div>




                                                                <asp:TextBox ID="txtTerRet" runat="server" ToolTip="Enter Ter./Ret. Date" CssClass="form-control"
                                                                    TabIndex="11" Style="z-index: 0;"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceTerRet" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTerRet"
                                                                    PopupButtonID="imgTerRet" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvTerRet" runat="server" ControlToValidate="txtTerRet"
                                                                    Display="None" ErrorMessage="Please Select Termination/Retirement Date in (dd/MM/yyyy Format)"
                                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
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
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Scale Range:</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlScale" AppendDataBoundItems="true" runat="server" CssClass="form-control" ToolTip="Select Scale"
                                                                TabIndex="12" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvddlScale" runat="server" ControlToValidate="ddlScale"
                                                 Display="None" ErrorMessage="Please Select Scale" ValidationGroup="Leave" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Remarks :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" ToolTip="Enter Remarks If Any"
                                                                TextMode="MultiLine" TabIndex="13"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemarks"
                                                    Display="None" ErrorMessage="Please Enter Remarks" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Upload Document :</label>

                                                                <br />
                                                                <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="14" />
                                                                <span style="color: red">Only .Jpeg/.Jpg/.Png/.Pdf Format is Allowed of Maximum Size 10 Mb</span>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                                        <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                                        <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <p class="text-center">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="15"
                                                                OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="16"
                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12">
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
                                      <%--  <table class="table table-bordered table-hover">--%>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
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
                                                             <th id="divFolder" runat="server">Attachment
                                                            </th>
                                                             <th id="divBlob" runat="server">Attachment
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("TRNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("TRNO") %>'
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
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("TRNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                     <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

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
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
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

        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

