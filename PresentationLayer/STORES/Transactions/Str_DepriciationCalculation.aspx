<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_DepriciationCalculation.aspx.cs" Inherits="STORES_Transactions_Str_DepriciationCalculation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--    <script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>

    <script lang="javascript" type="text/javascript">
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
                            <h3 class="box-title">Depreciation Calculation</h3>

                        </div>


                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">

                                    <div class="sub-heading">
                                        <h5>Depreciation Calculation</h5>

                                    </div>

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


                                    <div class="form-group col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Category</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCategory" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true"
                                                    TabIndex="2">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Sub Category</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                                    TabIndex="2" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" ToolTip="Select Sub Category">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlSubCategory"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Sub Category" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                 <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlSubCategory"
                                                Display="None" ErrorMessage="Please Select Sub Category" InitialValue="0" Text="*"
                                                ValidationGroup="Store"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Item</label>
                                                </div>
                                                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                                    TabIndex="2" OnSelectedIndexChanged=" ddlItem_SelectedIndexChanged" ToolTip="Select Item">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="ddlItem" InitialValue="0"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Item" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Serial No.</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSerialNo" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                    TabIndex="2" ToolTip="Select Serial No.">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="rfvSerial" runat="server" ControlToValidate="ddlSerialNo" InitialValue="0"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Serial No." ValidationGroup="Store"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*  </sup>
                                                    <label>Up To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="imgToDate">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter Up To Date"></asp:TextBox>
                                                    <%-- <div class="input-group-addon">
                                            <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />
                                        </div>--%>
                                                    <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                        Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                    </ajaxToolKit:MaskedEditExtender>




                                                    <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server"
                                                        ControlExtender="meToDate" ControlToValidate="txtToDate" Display="None"
                                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Select Up To Date"
                                                        InvalidValueBlurredMessage="Invalid Date"
                                                        InvalidValueMessage=" Up To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        SetFocusOnError="true" TooltipMessage="Please Select Up To Date" IsValidEmpty="false"
                                                        ValidationGroup="Store">
                                                    </ajaxToolKit:MaskedEditValidator>


                                                    <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                        TargetControlID="txtToDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <%--<asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Up To Date" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>







                                    </div>




                                    <div class="col-12 btn-footer">

                                        <asp:Button ID="btnCalDepreciation" runat="server" Text="Calculate Depreciation" OnClick="btnCalDepreciation_Click"
                                            CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" />

                                    </div>




                                    <div class="col-12">
                                        <asp:ListView ID="lvDepreciation" runat="server" Visible="true">
                                            <EmptyDataTemplate>

                                                <center>
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <%--  <table class="table table-bordered table-hover table-responsive" style="width:100%;"> 
                                                        <tr>
                                                            <td style="padding-left: 10px; padding-right: 10px">
                                                                <div id="demo-grid" class="vista-grid">--%>
                                                <div class="titlebar">
                                                    <div class="sub-heading">
                                                        <h5>Depreciation</h5>
                                                    </div>


                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>

                                                                <th>Item Serial No.
                                                                </th>
                                                                <th>Sub Category
                                                                </th>
                                                                <th>Purchase Amount
                                                                </th>
                                                                <th>Depr Percentage
                                                                </th>
                                                                <th>Depr From Date
                                                                </th>
                                                                <th>Depr To Date
                                                                </th>
                                                                <th>Depr Cal On Amt
                                                                </th>
                                                                <th>Days
                                                                </th>
                                                                <th>Depr Cal Amt
                                                                </th>
                                                                <th>Depr Balance Amt
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <%-- </div>
                                                            </td>
                                                        </tr>
                                                    </table>--%>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <%--<td style="width:2%;">
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("DEP_CAL_ID") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click1" />&nbsp;
                                                        </td>--%>
                                                    <td>
                                                        <%# Eval("DSR_NUMBER")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("MISGNO")%>  <%--28/02/2022--%>
                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("MISGNAME")%>         <%--28/02/2022--%>
                                                   <%-- </td>--%>
                                                    <td>
                                                        <%# Eval("PURCHASE_AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPR_PER")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPR_FROM_DATE","{0:dd/MM/yyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPR_TO_DATE","{0:dd/MM/yyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPR_CAL_ON_AMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DAYS")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPR_CAL_AMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPR_BAL_AMT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>

    <div>
        <asp:ValidationSummary runat="server" ID="vdReqField" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="Store" />
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

