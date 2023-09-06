<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="RefundReport.aspx.cs" Inherits="ACADEMIC_RefundReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
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

    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Refund Report</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Criteria for Report Generation</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="1" ToolTip="Please Select Session">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Printreport"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="2" ToolTip="Please Select Degree">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="3" ToolTip="Please Select Branch">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptType" runat="server"  AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="4" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlReceiptType" Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlReceiptType" Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" ValidationGroup="Printreport"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="fromDSpan" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalFromDate">
                                                <%-- <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: pointer" /> --%>
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" CssClass="form-control" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                            <asp:RequiredFieldValidator ID="rfvFromdate" runat="server" ControlToValidate="txtFromDate" Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="report"></asp:RequiredFieldValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFromDate" Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Printreport"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtFromDate"
                                            IsValidEmpty="False" InvalidValueMessage="From date is invalid" ForeColor="red"
                                            InvalidValueBlurredMessage="*" Display="None" Enabled="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalToDate">
                                                <%-- <asp:Image ID="imgCalToDate" runat="server" src="../images/calendar.png" Style="cursor: pointer" />--%>
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" CssClass="form-control" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                            <asp:RequiredFieldValidator ID="rfvTodate" runat="server" ControlToValidate="txtToDate" Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="report"></asp:RequiredFieldValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtToDate" Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Printreport"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeToDate" ControlToValidate="txtToDate"
                                            IsValidEmpty="False" InvalidValueMessage="To date is invalid" ForeColor="red"
                                            InvalidValueBlurredMessage="*" Display="None" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" ValidationGroup="Printreport" OnClick="btnReport_Click" Text="Report" />
                                <asp:Button ID="btnPrintReport" runat="server" CssClass="btn btn-info" OnClick="btnPrintReport_Click" TabIndex="9" Text="Refund-Report (Excel)" ToolTip="Print Report under Selected Criteria." ValidationGroup="report" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="10" Text="Cancel" ToolTip="Cancel Selected Under Selected Criteria." />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Printreport" />
                                <div id="divMsg" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrintReport" />
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
