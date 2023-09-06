<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LabOPDRegister.aspx.cs" Inherits="Health_LaboratoryTest_LabOPDRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <div>
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
                            <h3 class="box-title">LAB OPD REGISTER</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlOPDRegister" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="ImgBntCalc">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                    TabIndex="1" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                    ControlToValidate="txtFDate" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    IsValidEmpty="true" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit"> </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTDate" runat="server" MaxLength="10" Style="z-index: 0;" TabIndex="14"
                                                    CssClass="form-control" ToolTip="Please Select To Date" />
                                                <%--<asp:RequiredFieldValidator ID="rfvTodt" runat="server"
                                                    ControlToValidate="txtTDate" Display="None" ErrorMessage="Please Enter To Date"
                                                    SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>--%>
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
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divReport" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Report In</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdoReportType" runat="server"
                                                RepeatDirection="Horizontal" TabIndex="11">
                                                <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                                                <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                                                <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="5"
                                    CssClass="btn btn-outline-info" ToolTip="Click here to Show Report" OnClick="btnRport_Click" />&nbsp;                              
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="4"
                                    CssClass="btn btn-outline-danger" ToolTip="Click here to Reset" CausesValidation="false" />
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

