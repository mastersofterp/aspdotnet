<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ELEnCashment_Report.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Reports_ELEnCashment_Report"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EL ENCASHMENT REPORT</h3>
                        </div>
                        <div class="box-body">
                            <%-- <asp:Panel ID="pnlAdd" runat="server">--%>
                            <div class="form-group col-md-12" id="pnlAdd" runat="server">
                                <asp:UpdatePanel ID="updAdd" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>College Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" data-select2-enable="true"
                                                        ToolTip="Please Select College" CssClass="form-control" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    <label>From Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalFromDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="2"
                                                            ToolTip="Enter From Date" AutoPostBack="true" />
                                                       <%-- <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                                            Display="None" ErrorMessage="Please Enter Holiday Date" ValidationGroup="submit"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                        <ajaxToolKit:CalendarExtender ID="ceFromDt" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtFromDt" PopupButtonID="imgCalFromDt" Enabled="true"
                                                            EnableViewState="true" PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDt" runat="server" TargetControlID="txtFromDt"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true"  OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevFromDt" runat="server" ControlExtender="meeFromDt"
                                                            ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter From Date"
                                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                             InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="submit" SetFocusOnError="true" InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="3"
                                                            ToolTip="Enter To Date"
                                                            AutoPostBack="true" />
                                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                                            Display="None" ErrorMessage="Please Enter Holiday Date" ValidationGroup="submit"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                        <ajaxToolKit:CalendarExtender ID="ceToDt" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeToDt" runat="server" TargetControlID="txtToDt"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true"  OnInvalidCssClass="errordate"> 
                                                           </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevToDt" runat="server" ControlExtender="meeToDt"
                                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter To Date"
                                                            InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Out Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="submit" SetFocusOnError="true" InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>
   
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Report Type</label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdbType" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Text="Approved" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="Pending" Value="P"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="row">
                                                            <div class="form-group col-sm-12">
                                                                <div class="form-group col-sm-2">
                                                                    <label>College Name :</label>
                                                                </div>
                                                                <div class="form-group col-sm-4">
                                                                    
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                        <%-- <div class="row">
                                                            <div class="form-group col-sm-12">
                                                                <div class="form-group col-sm-2">
                                                                    <label>From Date :</label>
                                                                </div>
                                                                <div class="form-group col-sm-4">
                                                                 
                                                                </div>
                                                                <div class="form-group col-sm-2">
                                                                    <label>To Date :</label>
                                                                </div>
                                                                <div class="form-group col-sm-4">
                                                                    
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                        <%--  <div class="row">
                                                            <div class="form-group col-sm-12">
                                                                <div class="form-group col-sm-2">
                                                                    <label>Report Type :</label>
                                                                </div>
                                                                <div class="form-group col-sm-4">
                                                                  
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" TabIndex="6"
                                                OnClick="btnShowReport_Click" ToolTip="Click to Report" ValidationGroup="submit"/>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning " TabIndex="7"
                                                OnClick="btnCancel_Click" ToolTip="Click to Cancel" />
                                            <asp:ValidationSummary ID="vlsReport" runat="server" ValidationGroup="submit"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <%--</asp:Panel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

