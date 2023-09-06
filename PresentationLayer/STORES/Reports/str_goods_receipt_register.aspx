<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="str_goods_receipt_register.aspx.cs" Inherits="STORES_Reports_str_goods_receipt_register"
    Title="Untitled Page" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
    <%-- Flash the text/border red and fade in the "close" button --%>

    <%-- <script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GOODS RECEIPT REGISTER</h3>

                        </div>


                        <div class="box-body">
                            <asp:Panel ID="pnlStrConfig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDt" runat="server" ToolTip="Enter From Date" TabIndex="1" CssClass="form-control" />


                                                <ajaxToolKit:CalendarExtender ID="ceQuotDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrmDt"
                                                    PopupButtonID="imgCal" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="meQuotDate" runat="server" TargetControlID="txtFrmDt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mefromdate" runat="server" ControlExtender="meQuotDate" ControlToValidate="txtFrmDt"
                                                    EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                    ValidationGroup="submit" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date</label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtTodt" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter To Date" />

                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtTodt" PopupButtonID="Image1" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>

                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTodt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtTodt"
                                                        EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                        ValidationGroup="submit" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Vendor</label>
                                            </div>
                                            <asp:RadioButtonList ID="rblSelectAllVendor" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="rblSelectAllVendor_SelectedIndexChanged">
                                                <asp:ListItem Enabled="true" Text="Particular" Value="1"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="All" Value="0" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="drpoDowntr" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Particular Vendor Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlVendor" runat="server" data-select2-enable="true" CssClass="form-control" ToolTip="Please Select Vendor name"
                                                AppendDataBoundItems="true " TabIndex="4">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCddlVendor" runat="server" ControlToValidate="ddlVendor"
                                                Display="None" ErrorMessage="Please Select Particular Vendor Name" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnGRR" runat="server" Text="Goods Receipt Register" TabIndex="5"
                                        ToolTip="Click To Show Goods Receipt Register" CssClass="btn btn-primary"
                                        ValidationGroup="submit" OnClick="btnGRR_Click" />
                                    <asp:Button ID="btnStrReg" runat="server" Text="Stores Register " CssClass="btn btn-info" TabIndex="6" ToolTip="Click To Cancel"
                                        ValidationGroup="submit" OnClick="btnStrReg_Click" Visible="false" />
                                    <asp:Button ID="btnInwardreport" runat="server" Text="Inward Report" CssClass="btn btn-info"
                                        ValidationGroup="submit" Visible="false"
                                        OnClick="btnInwardreport_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7" CssClass="btn btn-warning" ToolTip="Click To Cancel"
                                        OnClick="btnCancel_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>


                </div>

            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
