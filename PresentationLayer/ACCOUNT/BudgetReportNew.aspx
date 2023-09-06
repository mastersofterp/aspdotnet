<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BudgetReportNew.aspx.cs" Inherits="ACCOUNT_Default" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPBudget"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <script type="text/javascript">
        function chek() {
            var a = document.getElementById("<%=txtFrmDate.ClientID%>").value;
            var b = document.getElementById("<%=txtUptoDate.ClientID%>").value;
            if (a == null || a == "" || b == null || b == "") {
                alert("Please Enter the FromDate,ToDate");
                return false;
            }
        }
    </script>
    <asp:UpdatePanel ID="UPBudget" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BUDGET REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" class="account_compname" style="font-size: x-large; text-align: center">
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Budget Report</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>From Date :</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="rfvfrom" runat="server" Display="None" ErrorMessage="Please Enter From Date" ControlToValidate="txtFrmDate" ValidationGroup="budget"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>Upto Date :</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtUptoDate" CssClass="form-control" runat="server" />
                                                        <ajaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                            TargetControlID="txtUptoDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="txtUptoDate_MaskedEditExtender" runat="server"
                                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtUptoDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator ID="rfvupdate" runat="server" ControlToValidate="txtUptoDate" ErrorMessage="Please Enter Upto Date" ValidationGroup="budget" Display="None"></asp:RequiredFieldValidator>
                                                        <input id="hdnBal" runat="server" type="hidden" />
                                                        <input id="hdnMode" runat="server" type="hidden" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <asp:RadioButton ID="rdbApplied" runat="server" Text="Applied Budget" GroupName="Budget"
                                                        AutoPostBack="true" OnCheckedChanged="rdbApplied_CheckedChanged" Checked="true" />
                                                    <asp:RadioButton ID="rdbApproved" runat="server" Text="Approved Budget" GroupName="Budget"
                                                        AutoPostBack="true" OnCheckedChanged="rdbApproved_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div id="dvBudget" class="row" runat="server" visible="false">
                                                <div class="col-md-2">
                                                    <label>Budget Head :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlBudgetList" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="form-group col-md-12">
                                                <div class="col-md-2"></div>
                                                <asp:Button ID="btnrpt" runat="server" Text="REPORT" Visible="false" CssClass="btn btn-primary" ValidationGroup="budget" OnClick="btnrpt_Click" />
                                                <asp:Button ID="btnExcelReport" runat="server" CssClass="btn btn-primary" Text="Excel Report" Enabled="true" OnClick="btnExcelReport_Click" ValidationGroup="bhn" />
                                            </div>
                                            <asp:ValidationSummary ID="vsbudget" ValidationGroup="budget" DisplayMode="List" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                        </div>
                                    </div>
                                </asp:Panel>
                              
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMsg" runat="server">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

