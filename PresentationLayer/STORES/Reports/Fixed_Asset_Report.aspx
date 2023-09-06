<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Fixed_Asset_Report.aspx.cs" Inherits="STORES_Reports_Fixed_Asset_Report" %>

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
                            <h3 class="box-title">Asset Register Report</h3>

                        </div>


                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                    <div class="row">
                                        <%-- <div class="sub-heading">
                                            <h5>Fixed Asset Report</h5>

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
                                                </table>
                                            </asp:Panel>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" >

                                            <asp:RadioButtonList ID="rblAssestType" runat="server" TabIndex="1" RepeatDirection="Horizontal">                                              

                                                <asp:ListItem Enabled="true" Selected="True" Text="Summary &nbsp;" Value="1"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Detailed  &nbsp;" Value="2"></asp:ListItem>

                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-12" >
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCategory" Enabled="false" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" ToolTip="Select Category">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategory"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Category"
                                                        ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Sub Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubCategory" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" ToolTip="Select Sub Category">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubCategory"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Sub Category"
                                                        ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Select Item</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlItem" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" ToolTip="Select Item">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItem"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Item"
                                                        ValidationGroup="StoreReq"></asp:RequiredFieldValidator>--%>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" >
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
                                                            InvalidValueBlurredMessage="Invalid Date"
                                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                            ValidationGroup="Store">
                                                        </ajaxToolKit:MaskedEditValidator>


                                                        <%--<asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                            Display="None" ValidationGroup="Store" SetFocusOnError="true" ErrorMessage="Please Select From Date">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" >
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="imgToDate">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter To Date"></asp:TextBox>
                                                        <%--  <div class="input-group-addon">
                                                            <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />
                                                        </div>--%>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                            Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                        </ajaxToolKit:MaskedEditExtender>

                                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                            TargetControlID="txtToDate">
                                                        </ajaxToolKit:CalendarExtender>

                                                        <ajaxToolKit:MaskedEditValidator ID="mevtodate" runat="server"
                                                            ControlExtender="meToDate" ControlToValidate="txtToDate" Display="None"
                                                            InvalidValueBlurredMessage="Invalid Date"
                                                            InvalidValueMessage="To  Date is Invalid (Enter dd/MM/yyyy Format)"
                                                            ValidationGroup="Store">
                                                        </ajaxToolKit:MaskedEditValidator>

                                                        <%--  <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select End Date" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                        <asp:CompareValidator ID="cmpvDate" runat="server" ErrorMessage="End Date Should be greater than or equal to  From Date"
                                                            ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Display="None" ValueToCompare="Date"
                                                            Type="Date" Operator="GreaterThanEqual" ValidationGroup="Store"></asp:CompareValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-12 btn-footer">

                                        <asp:Button ID="btnRpt" runat="server" Text="Show Report" OnClick="btnReport_Click"
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
    </asp:UpdatePanel>


    <div id="divMsg" runat="server">
    </div>
</asp:Content>

