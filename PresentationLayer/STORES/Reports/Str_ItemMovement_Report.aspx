<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_ItemMovement_Report.aspx.cs" Inherits="STORES_Reports_Str_ItemMovement_Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--    <script src="../Scripts/jquery.js" type="text/javascript"></script>

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
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Item Movement Report
                            </h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                    <div class="row" id="divToFields" runat="server">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCategory" data-select2-enable="true" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" ToolTip="Please Select Category">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategory"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Category" SetFocusOnError="True"
                                                ValidationGroup="Store"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sub Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubCategory" data-select2-enable="true" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" ToolTip="Please Select Sub Category">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubCategory"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Sub Category" SetFocusOnError="True"
                                                ValidationGroup="Store"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Item</label>
                                            </div>
                                            <asp:DropDownList ID="ddlItem" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" ToolTip="Select Item">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItem"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Item"
                                                ValidationGroup="Store"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divItemSrNo" visible="true">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Item Sr.No.</label>
                                            </div>

                                            <asp:DropDownList ID="ddlItemSrNo" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="2" ToolTip="Select Requisition" OnSelectedIndexChanged="ddlItemSrNo_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <%--     <asp:DropDownList ID="ddlItemSrNo" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                    TabIndex="2" ToolTip="Select Requisition" OnSelectedIndexChanged="ddlItemSrNo_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>--%>
                                            <%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlItem"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Item Sr.No"
                                                    ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                        </div>


                                    </div>
                                </div>
                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnRpt" runat="server" Text="Show Report"
                                        OnClick="btnReport_Click" CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" OnClick="btnCancel_Click" />

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

