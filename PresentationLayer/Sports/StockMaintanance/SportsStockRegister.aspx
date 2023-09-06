<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SportsStockRegister.aspx.cs" Inherits="Sports_StockMaintanance_SportsStockRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STOCK REGISTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlCommitteeReport" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Item Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlItem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Item" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvItems" runat="server" ValidationGroup="Submit" ControlToValidate="ddlItem" Display="None" InitialValue="0" ErrorMessage="Please Select Item Name."></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date </label>
                                            </div>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImgBntCalc" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control" ToolTip="Enter From Date" TabIndex="2"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <%--Modified by Saahil Trivedi 24-02-2022--%>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server"
                                                    ControlExtender="medt"
                                                    ControlToValidate="txtFDate"
                                                    EmptyValueMessage="Please Select From Date"
                                                    IsValidEmpty="false"
                                                    ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None"
                                                    Text="*"
                                                    ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTDate" runat="server" CssClass="form-control" ToolTip="Select To Date" TabIndex="3"> </asp:TextBox>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTDate" PopupButtonID="dvcal1"></ajaxToolKit:CalendarExtender>
                                                <%--Modified by Saahil Trivedi 24-02-2022--%>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"  IsValidEmpty="false"
                                                    ControlExtender="MaskedEditExtender1" ControlToValidate="txtTDate"
                                                    EmptyValueMessage="Please Select To Date"  ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit"> </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                           <div class=" col-12 btn-footer">
                               <%--Modified by Saahil Trivedi 24-02-2022--%>
                            <asp:Button ID="btnRport" runat="server" Text="Stock Register Report" TabIndex="4" CssClass="btn btn-info" ToolTip="Click here for Report" OnClick="btnRport_Click"  ValidationGroup="Submit" CausesValidation="true" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="5" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                            <asp:Button ID="btnSummary" runat="server" Text="Stock Summary Report" CssClass="btn btn-info" ToolTip="Click here to Report" OnClick="btnSummary_Click" ValidationGroup="Submit" Visible="false" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"  ValidationGroup="Submit" HeaderText="Following Fields are mandatory" />
                     
                    </div>
                      </div>
            </div>
       
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

