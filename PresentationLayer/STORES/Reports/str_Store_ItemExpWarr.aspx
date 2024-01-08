<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="str_Store_ItemExpWarr.aspx.cs" Inherits="STORES_Reports_str_Store_ItemExpWarr" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpanel">
        <ProgressTemplate>
            <div>
                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpanel"
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
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Item Expiry / Warranty Report</h3>

                        </div>

                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-md-10">
                                    <label>Select Group :<span style="color: #FF0000">*</span></label>
                                    <asp:RadioButtonList ID="rblGroup" runat="server" RepeatDirection="Horizontal" TabIndex="1" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblGroup_SelectedIndexChanged">
                                        <asp:ListItem Enabled="true" Selected="True" Text="Item wise Expiry Report" Value="1"></asp:ListItem>
                                        <asp:ListItem Enabled="true" Text="Item Warranty Expired Report" Value="2"></asp:ListItem>

                                    </asp:RadioButtonList>
                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgCal">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" ToolTip="Enter From Date" CssClass="form-control" TabIndex="2" Text=""></asp:TextBox>
                                        <%--  <div class="input-group-addon">
                                                               <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>--%>

                                        <ajaxToolKit:CalendarExtender ID="ceQuotDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                            PopupButtonID="imgCal" Enabled="true" EnableViewState="true" BehaviorID="_Fromdate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meQuotDate" runat="server" TargetControlID="txtFromDate"
                                            Enabled="true" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                        </ajaxToolKit:MaskedEditExtender>

                                        <ajaxToolKit:MaskedEditValidator ID="mevtodate" runat="server" ControlExtender="meQuotDate" ControlToValidate="txtFromDate"
                                            EmptyValueMessage="Please Select From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                            ValidationGroup="Store" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>

                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgToDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" runat="server" ToolTip="Enter To Date" CssClass="form-control" TabIndex="3" Text=""></asp:TextBox>
                                        <%-- <div class="input-group-addon">
                                                                    <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />
                                                                </div>--%>
                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                            Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                            PopupPosition="BottomRight" TargetControlID="txtToDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meToDate" ControlToValidate="txtToDate"
                                            EmptyValueMessage="Please Select To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                            ValidationGroup="Store" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>

                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                         <sup></sup>
                                        <label>Item</label>
                                    </div>
                                    <asp:DropDownList ID="ddlItem" data-select2-enable="true" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="4" ToolTip="Select Department">
                                        <asp:ListItem Selected="True" Value="0" Text="--Please Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItem"
                                        Display="None" ErrorMessage="Please Select Item" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnRpt" runat="server" Text="Show Report" OnClick="btnRpt_Click"
                                        CssClass="btn btn-primary" TabIndex="4" ToolTip="Click To Show Report" ValidationGroup="Store" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn btn-warning" TabIndex="5" ToolTip="Click To Reset" />
                                    <asp:ValidationSummary ID="vsstore" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="Store" />
                                </div>
                            </div>
                        </div>
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
</asp:Content>

