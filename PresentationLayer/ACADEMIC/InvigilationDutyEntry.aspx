<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="InvigilationDutyEntry.aspx.cs" Inherits="ACADEMIC_InvigilationDutyEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updInvigDuty"
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
    <asp:UpdatePanel ID="updInvigDuty" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">INVIGILATION DUTY ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                         <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>College & Scheme</label>--%>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlclgScheme" OnSelectedIndexChanged="ddlclgScheme_SelectedIndexChanged" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlclgScheme"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlclgScheme"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Session</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" runat="server"
                                            AutoPostBack="true" ValidationGroup="Show" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" ToolTip="Please Select Session" class="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ValidationGroup="Show" InitialValue="0" ErrorMessage="Please Select Session">
                                        </asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ValidationGroup="Report" InitialValue="0" ErrorMessage="Please Select Session">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Exam Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExTTType" runat="server" AppendDataBoundItems="true"
                                            ToolTip="Please Select Exam Name" class="form-control" data-select2-enable="true" AutoPostBack="true"
                                            TabIndex="2" OnSelectedIndexChanged="ddlExTTType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExTTType" runat="server" ControlToValidate="ddlExTTType"
                                            ValidationGroup="Show" Display="None" ErrorMessage="Please Select Exam Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlExTTType"
                                            ValidationGroup="Report" Display="None" ErrorMessage="Please Select Exam Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Exam Date </label>
                                            <%--<asp:Label ID="lblDYtxtExamDate" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <%--<div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtExamDate" TabIndex="6" ToolTip="Please Enter Date" OnTextChanged="txtExamDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            
                                            <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Date of Exam"
                                                ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Date of Exam"
                                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExamDate"
                                                Display="None" ErrorMessage="Please Select/Enter Date" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>--%>
                                        <asp:DropDownList ID="ddlDate" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" TabIndex="2" data-select2-enable="true" OnSelectedIndexChanged="ddlDate_SelectedIndexChanged" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="ddlDate"
                                            Display="None" ErrorMessage="Please Select Date" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Slot</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSlot" AppendDataBoundItems="true" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="ddlSlot_SelectedIndexChanged" ToolTip="Please Select Slot" class="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlSlot"
                                            ValidationGroup="Show" Display="None" ErrorMessage="Please Select Slot"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Extra Invigilator</label>
                                        </div>
                                        <asp:TextBox ID="txtExtraInv" runat="server" Text="0" onblur="IsNumeric(this)" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>View Report In </label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="pdf"> Adobe Reader &nbsp;</asp:ListItem>
                                            <asp:ListItem Value="xls"> MS-Excel &nbsp;</asp:ListItem>
                                            <asp:ListItem Value="doc"> MS-Word</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <%--  <div class="form-group col-lg-3 col-md-6 col-12" style="visibility: hidden">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Day No</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDay" AppendDataBoundItems="true" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlDay_SelectedIndexChanged" ToolTip="Please Select Day No" class="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDay" runat="server" ControlToValidate="ddlDay"
                                            ValidationGroup="Show" Display="None" ErrorMessage="Please Select Day"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>--%>
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12" style="visibility: hidden">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Exam Date</label>
                                        </div>
                                        <asp:Label ID="lblExamDate" Font-Bold="false" runat="server">
                                        </asp:Label>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Selection Session-> Exam Name-> Exam Date </span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Generate Duty"
                                    ValidationGroup="Show" class="btn btn-primary" />
                                <%--OnClientClick="return ConfirmSubmit();"--%>
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report"
                                    class="btn btn-info"  ValidationGroup="Report"/>
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                    ValidationGroup="none" class="btn btn-warning" />
                                <asp:ValidationSummary ID="vsShow" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false"  ValidationGroup="Report" />
                                <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />--%>
                                <%-- <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <%--<asp:PostBackTrigger ControlID="btnExcelReport" />--%>
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript" language="javascript">
        function IsNumeric(txt) {
            if (txt != null && txt.value != "") {
                if (isNaN(txt.value)) {
                    alert("Please Enter only Numeric Characters");
                    txt.value = "";
                    txt.focus();
                }
            }
        }
        function Total() {
            var txtInvig = document.getElementById("ctl00_ContentPlaceHolder1_txtInvig");
            var txtReliver = document.getElementById("ctl00_ContentPlaceHolder1_txtReliver");
            var txtTotal = document.getElementById("ctl00_ContentPlaceHolder1_txtTotal");
            txtTotal.value = Number(txtInvig.value) + Number(txtReliver.value);
        }
    </script>

</asp:Content>
