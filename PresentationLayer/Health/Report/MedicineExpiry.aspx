<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MedicineExpiry.aspx.cs" Inherits="Health_Report_MedicineExpiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
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
                            <h3 class="box-title">MEDICINE EXPIRY REPORT</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlMedicine" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Medicine Expiry</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="ImgBntCalc">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFDate" runat="server" TabIndex="1" CssClass="form-control"
                                                    ToolTip="Enter From Date" Style="z-index: 0;">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFDate" Display="None"
                                                    ErrorMessage="Please select from date" SetFocusOnError="true" ValidationGroup="Submit" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlToValidate="txtFDate"
                                                    ControlExtender="medt" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    IsValidEmpty="true" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit">                                                                  
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTDate" runat="server" MaxLength="10" Style="z-index: 0;" TabIndex="14"
                                                    CssClass="form-control" ToolTip="Please Select To Date" />
                                                <asp:RequiredFieldValidator ID="rfvTodt" runat="server"
                                                    ControlToValidate="txtTDate" Display="None" ErrorMessage="Please Enter To Date"
                                                    SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txtTDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt" ControlToValidate="txtTDate"
                                                    Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                                    TooltipMessage="Please Enter To Date" ValidationGroup="Leaveapp">                                                    
                                                    &#160;&#160;
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Expire within</label>
                                            </div>
                                            <div class="input-group date">
                                                <asp:TextBox ID="txtDays" runat="server" CssClass="form-control" MaxLength="5" ToolTip="Insert Number of Days"
                                                    onkeyup="validateNumeric(this);" TabIndex="3"></asp:TextBox>
                                                <div class="input-group-addon" id="Div1">
                                                    <label>days</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="4" CssClass="btn btn-info"
                                            OnClick="btnRport_Click" ToolTip="Click here to Show Report" ValidationGroup="Submit" />&nbsp;                              
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="5"
                                        CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Submit" />
                                    </div>
                            </asp:Panel>
                        </div>
                    </div>
                    </form>
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

