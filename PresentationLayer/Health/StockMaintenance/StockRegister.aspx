<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StockRegister.aspx.cs" Inherits="Health_StockMaintenance_StockRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <%--<div>
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
    </div>--%>
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
                            <asp:Panel ID="pnlStock" runat="server">
                                <div class="col-12">
                                 <%--   <div class="sub-heading">
                                        <h5>Stock Register</h5>
                                    </div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Item Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlItem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Item Name" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trVen" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Vendor</label>
                                            </div>
                                            <asp:DropDownList ID="ddlVendor" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Vendor" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="ImgBntCalc">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                    Style="z-index: 0;" TabIndex="3">
                                                </asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                    ControlToValidate="txtFDate" IsValidEmpty="true" Display="None"
                                                    ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Text="*"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Submit">                                                                   
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Image1">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTDate" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                                    Style="z-index: 0;" TabIndex="4"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTDate" PopupButtonID="Image1">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtTDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                    ControlExtender="MaskedEditExtender1" ControlToValidate="txtTDate"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit"> </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnRport" runat="server" Text="Stock Register Report" TabIndex="5"
                                    CssClass="btn btn-outline-info" ToolTip="Click here to Show Stock Register Report" OnClick="btnRport_Click" />&nbsp;                              
                                 <asp:Button ID="btnSummary" runat="server" Text="Stock Summary Report" TabIndex="7"
                                     CssClass="btn btn-outline-info" ToolTip="Click here to Show Stock Summary Report" OnClick="btnSummary_Click" ValidationGroup="Submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="6"
                                    CssClass="btn btn-outline-danger" ToolTip="Click here to Reset" CausesValidation="false" />&nbsp; 
                             
                                  <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                      ShowSummary="false" ValidationGroup="Submit" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
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
</asp:Content>

