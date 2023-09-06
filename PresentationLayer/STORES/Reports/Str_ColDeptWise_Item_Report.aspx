<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_ColDeptWise_Item_Report.aspx.cs" Inherits="STORES_Reports_Str_ColDeptWise_Item_Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>

    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">College Or Department Wise Item Report</h3>
                        </div>


                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                    <div class="row">
                                       <%-- <div class="sub-heading">
                                            <h5>Stock Report</h5>
                                        </div>--%>

                                        <div class="col-12">
                                            <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                                Visible="false">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDelivered" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Item In </label>
                                            </div>
                                            <asp:RadioButtonList ID="rblItemType" runat="server" TabIndex="1" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rblItemType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Enabled="true" Selected="True" Text="Main Store &nbsp;" Value="1"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="College  &nbsp;" Value="2"></asp:ListItem>

                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCollege" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" data-select2-enable="true" CssClass="form-control" TabIndex="4" ToolTip="Select Report" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem  Value="0">Please Select</asp:ListItem>  <%--Text="Please Select"--%>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDepartment" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" data-select2-enable="true" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Select Report" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgFromDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter From Date" TabIndex="1" Text=""></asp:TextBox>

                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" DisplayMoney="Left"
                                                    Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="mevFrom" runat="server"
                                                    ControlExtender="meFromDate" ControlToValidate="txtFromDate" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                    InvalidValueBlurredMessage="Invalid From Date"
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true"
                                                    ValidationGroup="Store">
                                                </ajaxToolKit:MaskedEditValidator>

                                                <%--  <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                            Display="None" ValidationGroup="Store" SetFocusOnError="true" ErrorMessage="Please Select From Date">
                                                        </asp:RequiredFieldValidator>--%>
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
                                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter To Date"></asp:TextBox>

                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                    Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                    TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevtodate" runat="server"
                                                    ControlExtender="meToDate" ControlToValidate="txtToDate" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true"
                                                    ValidationGroup="Store">
                                                </ajaxToolKit:MaskedEditValidator>


                                                <%-- <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select End Date" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ID="cmpvDate" runat="server" ErrorMessage="To Date Should be greater than or equal to  From Date"
                                                    ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Display="None" ValueToCompare="Date"
                                                    Type="Date" Operator="GreaterThanEqual" ValidationGroup="Store"></asp:CompareValidator>
                                            </div>
                                        </div>




                                    </div>

                                    <div class="col-12 btn-footer">

                                        <asp:Button ID="btnSummaryRpt" runat="server" Text="Summary Report" OnClick="btnSummaryRpt_Click"
                                            CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                        <asp:Button ID="btnDetailRpt" runat="server" Text="Detail Report" OnClick="btnDetailRpt_Click"
                                            CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" />

                                    </div>
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSummaryRpt" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnDetailRpt" EventName="Click" />
        </Triggers>


    </asp:UpdatePanel>


    <div id="divMsg" runat="server">
    </div>
</asp:Content>



