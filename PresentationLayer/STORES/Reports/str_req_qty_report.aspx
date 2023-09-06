<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="str_req_qty_report.aspx.cs" Inherits="STORES_Reports_str_req_qty_report"
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
                            <h3 class="box-title">REQUIRED TOTAL ITEM</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlStrConfig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <%--       <div class="sub-heading">REQUIRED TOTAL QUANTITY</div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date:</label>
                                            </div>
                                             <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter From Date" />
                                               
                                                <ajaxToolKit:CalendarExtender ID="ceQuotDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrmDt"
                                                    PopupButtonID="imgCal" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtQuotDate" runat="server" ControlToValidate="txtFrmDt"
                                                    Display="None" ErrorMessage="Please Select From Date" ValidationGroup="submit"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meQuotDate" runat="server" TargetControlID="txtFrmDt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meQuotDate"
                                                    ControlToValidate="txtFrmDt" InvalidValueMessage="From Date Is Invalid (Enter In dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Quotation opening date " ValidationGroup="submit"
                                                    SetFocusOnError="True" ErrorMessage="MaskedEditValidator2" IsValidEmpty="false" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date</label>
                                            </div>
                                           <div class="input-group date">
                                                <div class="input-group-addon" id="Image1">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTodt" CssClass="form-control" TabIndex="2" ToolTip="Enter To Date" runat="server" />
                                              

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTodt" PopupButtonID="Image1" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTodt"
                                                    Display="None" ErrorMessage="Please Select To Date" ValidationGroup="submit"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTodt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtTodt" InvalidValueMessage="To Date Is Invalid (Enter In dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Quotation opening date " ValidationGroup="submit"
                                                    SetFocusOnError="True" ErrorMessage="MaskedEditValidator2" IsValidEmpty="false" />
                                         
                                        </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Main Group:</label>
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
                                                <label>Select Particular Main Group </label>
                                            </div>
                                            <asp:DropDownList ID="ddlVendor" data-select2-enable="true" runat="server" CssClass="form-control" ToolTip="Please Select Vendor name"
                                                AppendDataBoundItems="true " TabIndex="4">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCddlVendor" runat="server" ControlToValidate="ddlVendor"
                                                Display="None" ErrorMessage="Please Select Vendor name" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>



                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnGRR" runat="server" Text="Required Qty Report" TabIndex="5"
                                                ToolTip="Click To Show Required qty report" CssClass="btn btn-info"
                                                ValidationGroup="submit" OnClick="btnGRR_Click" />
                                            <asp:Button ID="btnStrReg" runat="server" Text="Stores Register " TabIndex="6" ToolTip="Click To Stores Register"
                                                ValidationGroup="submit" Visible="false" CssClass="btn btn-info" />
                                            <asp:Button ID="btnInwardreport" runat="server" Text="Inward Report" ValidationGroup="submit"
                                                Visible="false" CssClass="btn btn-info" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7" ToolTip="Click To Cancel"
                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>

                    </div>
                 
            </div>


            </div>

       
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
