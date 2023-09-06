<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ReimbursementBillReport.aspx.cs" Inherits="ACCOUNT_ReimbursementBillReport" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="ja">
        function ClearAll() {

            document.getElementById('<%=ddlDept.ClientID%>').value = 0;
            document.getElementById('<%=txtApprovalFormDate.ClientID%>').value = document.getElementById('<%=txtApprovalToDate.ClientID%>').value = '';
        }
    </script>
    <script type="text/javascript">
        function CheckDate(val) {
            debugger;
            if (val == 2) {
                var gFromDate = document.getElementById('<%=txtApprovalFormDate.ClientID%>').value.split("/");
                var gToDate = document.getElementById('<%=txtApprovalToDate.ClientID%>').value.split("/");
                var fromdate = new Date(gFromDate[2], gFromDate[1] - 1, gFromDate[0]);
                var todate = new Date(gToDate[2], gToDate[1] - 1, gToDate[0]);
                if (todate < fromdate) {
                    alert('To Date Should be GraterThan From Date');
                    document.getElementById('<%=txtApprovalToDate.ClientID%>').value = '';
                }

            }
            else {
                document.getElementById('<%=txtApprovalToDate.ClientID%>').value = '';
            }
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updreimabursementBillReport"
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

    <asp:UpdatePanel ID="updreimabursementBillReport" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Reimbursement Bill Report</h3>
                        </div>

                        <div class="box-body">
                            <%--<asp:Panel ID="Panel1" runat="server">--%>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                                    </div>
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Department </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Select Department" TabIndex="1"></asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ErrorMessage="Please Select Department" ValidationGroup="PaymentBill" ControlToValidate="ddlDept" InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Form Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="txtFormDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <%--<asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>

                                            <asp:TextBox ID="txtApprovalFormDate" runat="server" CssClass="form-control" TabIndex="2" onchange="CheckDate(1);"></asp:TextBox>

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="txtFormDate" TargetControlID="txtApprovalFormDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtApprovalFormDate"
                                                ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender3"
                                                ControlToValidate="txtApprovalFormDate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="PaymentBill">                                                            
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="txtToDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <%--<asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>

                                            <asp:TextBox ID="txtApprovalToDate" runat="server" CssClass="form-control" TabIndex="3" onchange="CheckDate(2);"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="txtToDate" TargetControlID="txtApprovalToDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtApprovalToDate"
                                                ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                                                ControlToValidate="txtApprovalToDate" EmptyValueMessage="Please Enter To Date" IsValidEmpty="false"
                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="PaymentBill">                                                            
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnPdfReport" runat="server" CssClass="btn btn-primary" Text="Report" ValidationGroup="PaymentBill" OnClick="btnPdfReport_Click" TabIndex="4" />
                                <asp:Button ID="btnExcelReport" runat="server" CssClass="btn btn-info" Text="Excel Report" ValidationGroup="PaymentBill" OnClick="btnExcelReport_Click" TabIndex="5" />
                                <asp:Button ID="btncancel" runat="server" CssClass="btn btn-warning" TabIndex="6" Text="Cancel" OnClientClick="ClearAll();" />
                                <asp:ValidationSummary ID="vssummery" runat="server" ValidationGroup="PaymentBill" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                            <%--</asp:Panel>--%>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>

