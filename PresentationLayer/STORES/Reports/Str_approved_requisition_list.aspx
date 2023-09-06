<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_approved_requisition_list.aspx.cs" Inherits="STORES_Reports_Str_approved_requisition_list"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>
    <script type="text/javascript">
        function chk(sender, args) {
            var T_date = sender.get_selectedDate();
            var date = $find("_fromdate");
            var F_date = date.get_selectedDate();
            if (T_date < F_date) {
                alert("End Date Should be greater than or equal to  From Date");
                document.getElementById('<%=txtToDate.ClientID%>').value = "";
            }

        }
    </script>
    <%--    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div3" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Approved Requisition Summary Report</h3>
                </div>
                <div class="box-body">
                        <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                            <div class="col-12">
                              <%--  <div class="sub-heading">
                                    <h5>Approved Requisition List</h5>
                                </div>--%>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgFromDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="1" Text=""
                                                ToolTip="Enter From Date"></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" DisplayMoney="Left"
                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgFromDate" TargetControlID="txtFromDate" BehaviorID="_fromdate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevtodate" runat="server" ControlExtender="meFromDate" ControlToValidate="txtFromDate"
                                                 InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                ValidationGroup="Store" IsValidEmpty="true"> </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgToDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Text=""
                                                CssClass="form-control" ToolTip="Enter To Date"></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate" AcceptNegative="Left"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" ErrorTooltipEnabled="True">
                                            </ajaxToolKit:MaskedEditExtender>

                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                TargetControlID="txtToDate" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meToDate" ControlToValidate="txtToDate"
                                                 InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter Quotation opening date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                ValidationGroup="Store" IsValidEmpty="true" ErrorMessage="MaskedEditValidator1"> </ajaxToolKit:MaskedEditValidator>

                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="3" ToolTip="Select Department">
                                            <asp:ListItem Selected="True" Value="0" Text="--Please Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnRpt" runat="server" Text="Show Report" OnClick="btnRpt_Click"
                                    CssClass="btn btn-info" TabIndex="4" ToolTip="Click To Report" ValidationGroup="Store" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" TabIndex="5" ToolTip="Click To Reset" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        
    <div>
        <asp:ValidationSummary runat="server" ID="vdReqField" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="Store" />
    </div>
    <div id="divMsg" runat="server">
    </div>
    <asp:UpdatePanel ID="updMain" runat="server">
    </asp:UpdatePanel>
</asp:Content>
