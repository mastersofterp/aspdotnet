<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="transferRecordsToTally_New.aspx.cs" Inherits="Tally_Transactions_transferRecordsToTally_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upDetails"
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

    <asp:UpdatePanel ID="upDetails" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TRANFER RECORDS TO TALLY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCashbook" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCashbook" runat="server" ControlToValidate="ddlCashbook" Display="None" ErrorMessage="Please Select Reciept Type" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Date From</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalDateOfBirth" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtDateFrom" onkeydown="return (event.keyCode!=13);" runat="server" CssClass="form-control" placeholder="Enter From Date" TabIndex="2" Width="120px">
                                            </asp:TextBox>

                                            <%--<asp:Image ID="imgCalDateOfBirth" runat="server" src="../../IMAGES/calendar.png" Style="cursor: pointer; z-index: 0" Height="16px" />--%>

                                            <%--<ajaxToolKit:CalendarExtender ID="cetxtDateFrom" runat="server" Enabled="true" EnableViewState="true" CssClass=" cal_Theme1"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCalDateOfBirth" PopupPosition="BottomLeft" TargetControlID="txtDateFrom">
                                                        </ajaxToolKit:CalendarExtender>--%>
                                            <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateFrom" PopupButtonID="imgCalDateOfBirth" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtDateFrom" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDateFrom">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <asp:RegularExpressionValidator ID="regexpName" runat="server" ValidationGroup="Submit" ForeColor="Red"
                                                ErrorMessage="Enter Date format in (dd/MM/yyyy)" Display="None"
                                                ControlToValidate="txtDateFrom"
                                                ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" />
                                            <asp:RequiredFieldValidator ID="rfvtxtDateFrom" runat="server" ControlToValidate="txtDateFrom" Display="None" ErrorMessage="Please Enter Date From" ValidationGroup="Submit"
                                                SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Date To</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtDateTo" onkeydown="return (event.keyCode!=13);" runat="server" CssClass="form-control" placeholder="Enter To Date" TabIndex="3" Width="120px">
                                            </asp:TextBox>
                                            <%--<asp:Image ID="Image1" runat="server" src="../../IMAGES/calendar.png" Style="cursor: pointer; z-index: 0" Height="16px" />--%>

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateTo" PopupButtonID="Image1" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meetxtDateTo" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDateTo">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <asp:RegularExpressionValidator ID="revtxtDateTo" runat="server" ValidationGroup="Submit" ForeColor="Red"
                                                ErrorMessage="Enter Date format in (dd/MM/yyyy)" Display="None"
                                                ControlToValidate="txtDateTo"
                                                ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" />
                                            <asp:RequiredFieldValidator ID="rfvtxtDateTo" runat="server" ControlToValidate="txtDateTo" Display="None" ErrorMessage="Please Enter Date To" ValidationGroup="Submit"
                                                SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmpVal1" ControlToCompare="txtDateFrom"
                                                ControlToValidate="txtDateTo" Type="Date" Operator="GreaterThanEqual"
                                                ErrorMessage="Session Start Date Should be Less Than Session End Date." ValidationGroup="Submit" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>
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
                                                        <asp:Label ID="lblChequeAmount" Visible="false" runat="server" Text="--" Font-Bold="true">
                                                        </asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>TOTAL AMOUNT</td>
                                                    <td>
                                                        <asp:Label ID="lblTotalAmount" runat="server" Text="--" Font-Bold="true">
                                                        </asp:Label></td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" TabIndex="4" Text="Show" ToolTip="Click to Save" ValidationGroup="Submit" OnClick="btnShow_Click" UseSubmitBehavior="false" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" TabIndex="5" Text="Cancel" ToolTip="Click to Cancel" OnClientClick="Cancel()" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="DivReceipt" runat="server" Visible="false">
                                    <asp:ListView ID="Rep_Receipt" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <div class="sub-heading">
                                                    <h5>Receipts Details</h5>
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
                                        <asp:Button ID="btnGetRecTally" runat="server" CssClass="btn btn-primary" Text="Get Record" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnGetRecTally_Click" />
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Transfer To Tally" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnSubmit_Click" Enabled="false" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
