<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="GetRecordsForTallyTransfer.aspx.cs" Inherits="Tally_Transactions_GetRecordsForTallyTransfer" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    GET RECORDS FOR TALLY TRANSFER

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TRANFER RECORDS TO TALLY</h3>
                        </div>

                        <div class="box-body">
                            <div id="tblDetails" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    <asp:Label ID="lblCashBook" runat="server" Text="Reciept Type"></asp:Label>
                                                </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCashbook" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCashbook" runat="server" ControlToValidate="ddlCashbook" Display="None" ErrorMessage="Please Select Cashbook" ValidationGroup="Submit"
                                                SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    <asp:Label ID="lblServerName" runat="server" Text="Date From"></asp:Label></label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalDateOfBirth" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" TabIndex="2" />
                                                <%--<asp:Image ID="imgCalDateOfBirth" runat="server" src="../../images/calendar.png" Style="cursor: pointer" Height="16px" />--%>

                                                <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDateFrom"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateFrom" PopupButtonID="imgCalDateOfBirth" Enabled="True">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateFrom"
                                                    Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    <asp:Label ID="lblCompanyName" runat="server" Text="Date To"></asp:Label></label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgtodate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" TabIndex="3" />
                                                <%--<asp:Image ID="imgtodate" runat="server" src="../../images/calendar.png" Style="cursor: pointer" Height="16px" />--%>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateTo"
                                                    Display="None" ErrorMessage="Please Enter Date To" SetFocusOnError="True"
                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateTo" PopupButtonID="imgtodate" Enabled="True">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDateTo"
                                                    Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="sub-heading">
                                                <h5>Amount Description</h5>
                                            </div>

                                            <table class="table table-bordered">
                                                <tbody>
                                                    <tr>
                                                        <td>CASH</td>
                                                        <td>
                                                            <asp:Label ID="lblCashAmount" runat="server" Text="--" Font-Bold="true">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>DEMAND DRAFT</td>
                                                        <td>
                                                            <asp:Label ID="lblDDAmount" runat="server" Text="--" Font-Bold="true">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="d-none">
                                                        <td>CHEQUE</td>
                                                        <td>
                                                            <asp:Label ID="lblChequeAmount" runat="server" Text="--" Font-Bold="true">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>TOTAL AMOUNT</td>
                                                        <td>
                                                            <asp:Label ID="lblTotalAmount" runat="server" Text="--" Font-Bold="true">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" TabIndex="4" Text="Show" ToolTip="Click to Save" ValidationGroup="Submit" CssClass="btn btn-primary" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnShow_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-warning" TabIndex="5" Text="Cancel" ToolTip="Click to Cancel" OnClientClick="Cancel()" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                </div>
                            </div>

                            <div class="col-12" id="Table1" runat="server">
                                <asp:Panel ID="DivReceipt" runat="server" Visible="false">
                                    <asp:ListView ID="Rep_Receipt" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <div class="sub-heading">
                                                    <h5>Receipt Details</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SR.NO.
                                                            </th>
                                                            <th>RECEIPT NUMBER
                                                            </th>


                                                            <th>RECEIPT DATE
                                                            </th>

                                                            <th>CASH AMOUNT  
                                                            </th>

                                                            <%-- <th style="width: 40%; text-align: left;">CHEQUE AMOUNT
                                                            </th>--%>
                                                            <th>DD AMOUNT
                                                            </th>

                                                            <th>STUDENT NAME
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
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>
                                                <td>
                                                    <%# Eval("ReceiptNumber")%>
                                                </td>

                                                <td>
                                                    <%# Eval("ReceiptDate")%>
                                                      
                                                </td>

                                                <td>
                                                    <%# ((decimal)Eval("Cash") <=0) ? "--" : Eval("Cash") %>
                                                </td>
                                                <%-- <td style="width: 40%; text-align: left;">
                                                       <%# Eval("Cheque")%>
                                                    </td>--%>
                                                <td>
                                                    <%# ((decimal)Eval("DemandDraft") <=0) ? "--" : Eval("DemandDraft") %>
                                                </td>
                                                <td>
                                                    <p><strong><%# Eval("StudentName")%></strong></p>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                    <div class="col-12 btn-footer mt-4">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Save" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnSubmit_Click" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

