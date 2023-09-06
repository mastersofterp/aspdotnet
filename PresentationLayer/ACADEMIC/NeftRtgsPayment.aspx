<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NeftRtgsPayment.aspx.cs" Inherits="ACADEMIC_NeftRtgsPayment" MasterPageFile="~/SiteMasterPage.master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPayment"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updPayment" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">NEFT/RTGS PAYMENT DETAILS</h3>
                        </div>
                        <div class="pull-right" style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                        <div class="box-body">
                            <div class="form-group col-md-12">
                                <div class="form-group col-md-4">
                                    <span style="color: red">* </span>
                                    <label>Receipt Type</label>
                                    <asp:DropDownList ID="ddlReceipt" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlReceipt_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReceipt" ErrorMessage="Please Select Receipt Type" Display="None" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <span style="color: red">* </span>
                                    <label>Semester</label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester" ErrorMessage="Please Select Semester" Display="None" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <span style="color: red">* </span>
                                    <label>Transaction ID</label>
                                    <asp:TextBox ID="txtTransaction" runat="server" TabIndex="3" MaxLength="24" AutoComplete="off" Style="text-transform: uppercase" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTransaction" ErrorMessage="Please Enter Transaction ID" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" InvalidChars="!@#$%^&*()-_+={[}]:;'<,>.?/"
                                        FilterMode="InvalidChars" TargetControlID="txtTransaction">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-md-4">
                                    <span style="color: red">* </span>
                                    <label>Date of Transaction</label>
                                    <div class="input-group">
                                        <div class="input-group-addon" id="icon">
                                            <div class="fa fa-calendar"></div>
                                        </div>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="ceAdmDt" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtDate" PopupButtonID="icon" Enabled="True">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeAdmDt" runat="server" TargetControlID="txtDate"
                                            Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                            CultureTimePlaceholder="" Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDate" ErrorMessage="Please Enter Date of Transaction" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-md-4">
                                    <span style="color: red">* </span>
                                    <label>Bank Name</label>
                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" TabIndex="5" Style="text-transform: uppercase" AutoComplete="off" MaxLength="64"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBankName" ErrorMessage="Please Enter Bank Name" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" InvalidChars="0123456789!@#$%^&*()_-+={}[]:;'<.?/"
                                        FilterMode="InvalidChars" TargetControlID="txtBankName">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-md-4">
                                    <span style="color: red">* </span>
                                    <label>Amount</label>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TabIndex="6" AutoComplete="off" MaxLength="7"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="0123456789"
                                        FilterMode="ValidChars" TargetControlID="txtAmount">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please Enter Amount" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-5">
                                    <span style="color: red">* </span>
                                    <label>Upload Receipt</label>
                                    <span style="color: red">(Select only .jpeg,.jpg or .pdf file within 300 kb.)</span>
                                    <asp:FileUpload ID="fuUpload" runat="server" TabIndex="7" BackColor="Bisque" Width="80%" />
                                    <asp:Label ID="lblUpload" runat="server" Visible="false"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="fuUpload" ErrorMessage="Please Select File To Upload" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group col-md-12" style="text-align: center">
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="8" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click" ValidationGroup="Submit" />
                                <asp:Button ID="btnCancel" runat="server" TabIndex="9" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="vsSubmit" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="form-group col-md-12 table table-responsive">
                                <asp:Panel ID="pnlPaymentDetails" runat="server" Visible="true">
                                    <asp:ListView ID="lvDetails" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div>
                                                  <h3>
                                                <div class="label label-default">NEFT/RTGS Details</div>
                                            </h3>
                                                <table class="table table-hover table-bordered" id="tbllist">
                                                    <thead>
                                                        <tr class="bg-light-blue" style="background-color: #337ab7; color: white">
                                                            <th style="text-align:center;width:5%">
                                                                Sr.No.
                                                            </th>
                                                            <th style="width:8%">
                                                                Receipt Type
                                                            </th>
                                                            <th style="width:5%">
                                                                Semester
                                                            </th>
                                                            <th style="width:20%">
                                                                Transaction ID
                                                            </th>
                                                            <th style="width:8%">
                                                                Date Of Transaction
                                                            </th>
                                                            <th style="width:20%">
                                                                Bank Name
                                                            </th>
                                                            <th style="width:10%">
                                                                Amount
                                                            </th>
                                                            <th style="width:8%">
                                                                Download Receipt
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
                                                <td style="text-align:center;width:5%">
                                                    <%# Container.DataItemIndex +1 %>
                                                </td>
                                                <td style="width:8%">
                                                    <%# Eval("RECIEPT_TITLE") %>
                                                </td>
                                                <td style="width:5%">
                                                    <%# Eval("SEMESTER") %>

                                                </td>
                                                <td style="width:20%">
                                                    <%# Eval("TRANSACTIONID") %>

                                                </td>
                                                <td style="width:8%">
                                                    <%# Eval("TRANSACTION_DATE") %>

                                                </td>
                                                <td style="width:20%">
                                                    <%# Eval("BANK_NAME") %>

                                                </td>
                                                <td style="width:10%">
                                                    <%# Eval("AMOUNT") %>
                                                </td>
                                                <td style="text-align:center;width:8%">
                                                <asp:ImageButton ID="btnDownload" runat="server" OnClick="btnDownload_Click" Visible='<%# Convert.ToString(Eval("FILE_UPLOAD"))== string.Empty ? false:true %>' ImageUrl="~/IMAGES/down-arrow.png" CommandArgument='<%# Eval("FILE_UPLOAD") %>' TabIndex="13" />
                                                </td>
                                            </tr>

                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="lvDetails" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
