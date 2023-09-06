<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StrStockSummaryReport.aspx.cs" Inherits="STORES_Reports_StrStockSummaryReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
                            <h3 class="box-title">Stock Summary Report
                            </h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                    <div class="row" id="divToFields" runat="server">

                                        <div class="form-group col-12">
                                            <%--  <div class="form-group col-md-4 col-12"></div>
                                            <div class="form-group col-md-8 col-12">--%>
                                            <asp:RadioButtonList ID="rdlSelect" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="rdlSelect_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="Item Wise" Selected="True">&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1" Text="Department Wise">&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2" Text="Department Wise At User">&nbsp;&nbsp;</asp:ListItem>
                                                 <asp:ListItem Value="3" Text="Master Stock Register">&nbsp;&nbsp;</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <%-- </div>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCat" runat="server">
                                            <div class="label-dynamic">
                                              <%--  <label id="lblCat" runat="server" style="color: red">*</label>--%>
                                                <label>Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCategory" data-select2-enable="true" runat="server" Enabled="false" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" ToolTip="Please Select Category">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                          
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSubCat" runat="server">
                                            <div class="label-dynamic">
                                                <%--<label id="lblSubCat" runat="server" style="color: red">*</label>--%>
                                                <label>Sub Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubCategory" data-select2-enable="true" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" ToolTip="Please Select Sub Category">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divItem" runat="server">
                                            <div class="label-dynamic">
                                                <%--<label id="lblItem" runat="server" style="color: red">*</label>--%>
                                                <label>Select Item</label>
                                            </div>
                                            <asp:DropDownList ID="ddlItem" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" ToolTip="Select Item">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                           
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divItemSrNo" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Item Sr.No.</label>
                                            </div>

                                            <asp:DropDownList ID="ddlItemSrNo" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                TabIndex="2" ToolTip="Select Requisition">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>


                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divDept" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Department</label>
                                            </div>

                                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                TabIndex="2" ToolTip="Select Department">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>


                                        </div>



                                    </div>
                                </div>
                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnRpt" runat="server" Text="Show Report"
                                        OnClick="btnReport_Click" CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary runat="server" ID="vdReqField" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Store" />

                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

