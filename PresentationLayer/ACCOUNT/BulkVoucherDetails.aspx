<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkVoucherDetails.aspx.cs" Inherits="ACCOUNT_BulkVoucherDetails" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VOUCHER CHEQUE DETAILS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>

                                <div class="panel panel-info">
                                    <div class="panel-heading">Voucher Details</div>
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlVch" runat="server">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>Voucher Type <span style="color: red">*</span>:</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <asp:DropDownList ID="ddlVoucherType" runat="server" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="NV">VOUCHER</asp:ListItem>
                                                        <asp:ListItem Value="BK">BULK VOUCHER</asp:ListItem>
                                                        <asp:ListItem Value="AD">ADVANCE VOUCHER</asp:ListItem>
                                                        <asp:ListItem Value="ST">STORE VOUCHER</asp:ListItem>
                                                        <asp:ListItem Value="BL">BILL VOUCHER</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvType" runat="server"
                                                        ControlToValidate="ddlVoucherType" Display="None" ErrorMessage="Please Select Voucher Type" SetFocusOnError="true"
                                                        InitialValue="0" ValidationGroup="Voucher">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>Transaction Date <span style="color: red">*</span>:</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control" />
                                                        <%--Style="text-align: right" AutoPostBack="True" --%>
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                         <asp:RequiredFieldValidator ID="rfvDate" runat="server"
                                                        ControlToValidate="txtFrmDate" Display="None" ErrorMessage="Please Select Transaction Date" SetFocusOnError="true"
                                                         ValidationGroup="Voucher">
                                                    </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>Bank Ledger: </label>
                                                </div>
                                                <div class="form-group col-md-6">

                                                    <asp:TextBox ID="txtAgainstAcc" runat="server" AutoPostBack="true" CssClass="form-control"
                                                        ToolTip="Please Enter Ledger Name"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="aceAcc" runat="server" TargetControlID="txtAgainstAcc"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAgainstAcc" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="txtAgainstAcc" Display="None" ErrorMessage="Please Select Ledger for Against Account" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>--%>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>Cheque Date:</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtChqDate" runat="server" CssClass="form-control" /> 
                                                       <%-- Style="text-align: right" AutoPostBack="True" --%>
                                                        <ajaxToolKit:CalendarExtender ID="txtChqDate_CalendarExtender" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                            TargetControlID="txtChqDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="txtChqDate_MaskedEditExtender" runat="server"
                                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtChqDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                            ControlToValidate="txtChqDate" Display="None" ErrorMessage="Please Select Cheque Date" SetFocusOnError="true"
                                                            ValidationGroup="AccMoney">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            


                                            <div class="row">
                                                <div class="col-md-2"></div>
                                                <div class="form-group col-md-10">
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-info" OnClick="btnShow_Click" 
                                                        ValidationGroup="Voucher" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSave_Click" Visible="false"
                                                        ValidationGroup="AccMoney" />
                                                    <asp:ValidationSummary ID="vsAcc" runat="server" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="AccMoney" DisplayMode="List" />

                                                    <asp:ValidationSummary ID="vsVoucher" runat="server" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="Voucher" DisplayMode="List" />
                                                </div>
                                            </div>

                                            <div class="col-md-12 table-responsive" id="divBulk" runat="server">
                                                <asp:ListView ID="lvBVoucher" runat="server">
                                                    <EmptyDataTemplate>
                                                        <p class="text-center text-bold">
                                                            NO RECORD FOUND                                                               
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <h4>Bulk Voucher</h4>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Sr.No.</th>
                                                                        <th>Select</th>
                                                                        <th>Voucher No</th>
                                                                        <th>Party</th>
                                                                        <th>Amount</th>
                                                                        <th>Cheque Number</th>
                                                                        <th>Bulk Voucher</th>
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
                                                            <td width="5%">
                                                                <%#Container.DataItemIndex+1 %>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("VOUCHER_SQN") %>' /></td>
                                                            <td width="20%">
                                                                <%# Eval("STR_VOUCHER_NO")%>
                                                                <asp:HiddenField ID="hdnVchNo" runat="server" Value='<%# Eval("VOUCHER_NO")%>' />
                                                                <asp:HiddenField ID="hdnVchSeq" runat="server" Value='<%# Eval("VOUCHER_SQN")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("PARTY_NAME")%> 
                                                            </td>

                                                            <td width="20%">
                                                                <%# Eval("AMOUNT")%>                                                                  
                                                            </td>
                                                            <td width="20%">

                                                                <asp:TextBox ID="txtChqNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnDetails" runat="server" Text="Show Details" CommandArgument='<%# Eval("VOUCHER_SQN")%>' OnClick="btnDetails_Click" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                                <%--<div class="text-center">
                                                    <div class="vista-grid_datapager">
                                                        <asp:DataPager ID="dpinfo" runat="server" PagedControlID="lvBVoucher" PageSize="50" Visible="false"
                                                            OnPreRender="dpinfo_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>
                                                </div>--%>

                                            </div>

                                            <div class="col-md-12 table-responsive" id="divOther" runat="server">
                                                <asp:ListView ID="lvVoucher" runat="server">
                                                    <EmptyDataTemplate>
                                                        <p class="text-center text-bold">
                                                            NO RECORD FOUND                                                               
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <h4>Voucher Details</h4>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Sr.No.</th>
                                                                        <th>Select</th>
                                                                        <th>Voucher No</th>
                                                                        <th>Party</th>
                                                                        <th>Amount</th>
                                                                        <th>Cheque Number</th>

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
                                                            <td width="5%">
                                                                <%#Container.DataItemIndex+1 %>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("VOUCHER_SQN") %>' /></td>
                                                            <td width="20%">
                                                                <%# Eval("STR_VOUCHER_NO")%>
                                                                <asp:HiddenField ID="hdnVchNo" runat="server" Value='<%# Eval("VOUCHER_NO")%>' />
                                                                <asp:HiddenField ID="hdnVchSeq" runat="server" Value='<%# Eval("VOUCHER_SQN")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("PARTY_NAME")%> 
                                                            </td>

                                                            <td width="20%">
                                                                <%# Eval("AMOUNT")%>                                                                  
                                                            </td>
                                                            <td width="20%">

                                                                <asp:TextBox ID="txtChqNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                                <%--<div class="text-center">
                                                    <div class="vista-grid_datapager">
                                                        <asp:DataPager ID="dpVch" runat="server" PagedControlID="lvBVoucher" PageSize="50" Visible="false"
                                                            OnPreRender="dpVch_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>
                                                </div>--%>

                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlEmployee" runat="server">
                                            
                                            <div class="col-md-12 table-responsive">
                                                <div class="row" id="divemp" runat="server" visible="false">
                                                <div class="col-md-2">
                                                    <label>Cheque Date:</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtChDate" runat="server" CssClass="form-control" /> 
                                                       <%-- Style="text-align: right" AutoPostBack="True" --%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                            TargetControlID="txtChDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtChDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="txtChDate" Display="None" ErrorMessage="Please Select Cheque Date" SetFocusOnError="true"
                                                            ValidationGroup="Employee">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                                <asp:ListView ID="lvEmp" runat="server">
                                                    <EmptyDataTemplate>
                                                        <p class="text-center text-bold">
                                                            NO RECORD FOUND                                                               
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <h4>Bulk Voucher Details</h4>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Sr.No.</th>
                                                                        <th>Employee Name</th>
                                                                        <th>Amount</th>
                                                                        <th>Cheque Number</th>

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
                                                            <td width="5%">
                                                                <%#Container.DataItemIndex+1 %>
                                                            </td>

                                                            <td width="20%">
                                                                <%# Eval("EMPNAME")%>
                                                                <asp:HiddenField ID="hdnEmp" runat="server" Value='<%# Eval("EMPID")%>' />
                                                                <asp:HiddenField ID="hdnVchNo" runat="server" Value='<%# Eval("VOUCHER_NO")%>' />
                                                                <asp:HiddenField ID="hdnVchSeq" runat="server" Value='<%# Eval("VOUCHER_SQN")%>' />
                                                            </td>


                                                            <td width="20%">
                                                                <%# Eval("AMOUNT")%>                                                                  
                                                            </td>
                                                            <td width="20%">

                                                                <asp:TextBox ID="txtChqNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                                <%--<div class="text-center">
                                                    <div class="vista-grid_datapager">
                                                        <asp:DataPager ID="dpDetail" runat="server" PagedControlID="lvEmp" PageSize="50" Visible="false"
                                                            OnPreRender="dpDetail_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>
                                                </div>--%>

                                            </div>
                                            <div class="row">
                                                <%--<div class="col-md-2"></div>--%>
                                                <div class="form-group col-md-12">
                                                    <p style="text-align:center">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSubmit_Click"
                                                        ValidationGroup="Employee" Visible="false" />
                                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-info" OnClick="btnBack_Click" Visible="false" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="Employee" DisplayMode="List" />
                                                    </p>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

