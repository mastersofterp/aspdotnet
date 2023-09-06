<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkPaymentReport.aspx.cs" Inherits="ACCOUNT_BulkPaymentReport" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
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
                            <h3 class="box-title">BULK PAYMENT REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>
                                <asp:Panel ID="pnlGrpReport" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Bulk Payment Report</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>Payment Mode <span style="color: red">*</span>:</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                   <asp:DropDownList ID="ddlPaymentMode" runat="server" AppendDataBoundItems="true">
                                                               <%-- <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="C">CHEQUE</asp:ListItem>
                                                                <asp:ListItem Value="N">NEFT</asp:ListItem>
                                                                <asp:ListItem Value="R">RTGS</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvMode" runat="server"
                                                                ControlToValidate="ddlPaymentMode" Display="None" ErrorMessage="Please Select Payment Mode" 
                                                                SetFocusOnError="true" InitialValue="0"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>From Date <span style="color: red">*</span> :</label><%--Transaction--%>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"/>                                                        
                                                            <%--Style="text-align: right" AutoPostBack="True" --%>
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server"
                                                                ControlToValidate="txtFrmDate" Display="None" ErrorMessage="Please Select From Date" 
                                                                SetFocusOnError="true" 
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>To Date:</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtUptoDate"  runat="server"
                                                            CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                            TargetControlID="txtUptoDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="txtUptoDate_MaskedEditExtender" runat="server"
                                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtUptoDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtUptoDate" Display="None" ErrorMessage="Please Select To Date" 
                                                                SetFocusOnError="true" 
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        <input id="hdnBal" runat="server" type="hidden" />
                                                        <input id="hdnMode" runat="server" type="hidden" />
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="form-group row">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-10">
                                               
                                                <asp:RadioButtonList ID="rblGroup" runat="server" TabIndex="1" RepeatDirection="Horizontal" 
                                                   >
                                                    <asp:ListItem Enabled="true" Selected="True" Text="Is Approved &nbsp;&nbsp;" Value="1"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="Not Approved &nbsp;&nbsp;" Value="2" ></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                                                 </div>
                                            </div>


                                            <div class="form-group row">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-10">
                                                    <%--<asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" />--%>
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-info" OnClick="btnShow_Click"  ValidationGroup="AccMoney"/>
                                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="AccMoney" DisplayMode="List" />
                                                 </div>
                                            </div>

                                           

                                             <%--<asp:Panel ID="pnlStatus" runat="server" Visible="false">--%>
                                        <div class="col-md-12 table-responsive">
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
                                                                    <th>Voucher No</th>
                                                                    <th>Party</th>
                                                                    <th>Narration</th>
                                                                    <th>Amount</th>
                                                                    <th>Report</th>
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
                                                            <%# Eval("STR_VOUCHER_NO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PARTY_NAME")%> 
                                                        </td>
                                                        <td width="20%">
                                                            
                                                           <%# Eval("NARRATION")%> 
                                                        </td>
                                                        <td width="20%">
                                                            <%# Eval("AMOUNT")%>                                                                  
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnReport" runat="server" Text="Report" CommandArgument='<%# Eval("VOUCHER_SQN")%>' OnClick="btnReport_Click" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                            <div class="text-center">
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
                                            </div>

                                        </div>

                                      
                                    <%--</asp:Panel>--%>


                                        </div>
                                    </div>
                                </asp:Panel>

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

