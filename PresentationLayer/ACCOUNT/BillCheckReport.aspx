<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="BillCheckReport.aspx.cs" Inherits="ACCOUNT_BillCheckReport" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            text-align: center;
        }
    </style>
    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updChkBill"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="row">
        <asp:UpdatePanel ID="updChkBill" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div id="div2" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">BILL CHEQUE REPORT</h3>
                            </div>
                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            <div class="box-body">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Bill Cheque Report</div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Account :</label>
                                                    <asp:DropDownList ID="ddlCompAccount" AutoPostBack="true" runat="server" CssClass="form-control" ToolTip="Please Select Account"
                                                        OnSelectedIndexChanged="ddlCompAccount_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Select Bank:</label>
                                                    <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="true" CssClass="form-control" ToolTip="Please Select Bank"
                                                        OnSelectedIndexChanged="ddlBank_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Request No.</label>
                                                    <asp:DropDownList ID="ddlRequestNo" runat="server" CssClass="form-control" ToolTip="Please Select RequestNo">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnShowReport" runat="server" CssClass="btn btn-primary" ToolTip="Click to Show Report" Text="Show Report" 
                                                       OnClick="btnShowReport_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

