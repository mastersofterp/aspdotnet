<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="MainConfiguration.aspx.cs" Inherits="ACCOUNT_MainConfiguration" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div style="width: 100%">
        <asp:UpdatePanel ID="UPDLedger" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div id="div2" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">MAIN CONFIGURATION</h3>
                            </div>
                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            <div class="box-body">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Main Configuration</div>
                                        <div class="panel-body">
                                            <div class="col-md-12">
                                                <div class="form-group row">
                                                    <div class="col-md-3">
                                                        <label>AO :</label>
                                                        <asp:DropDownList ID="ddlAO" runat="server" TabIndex="1" AutoPostBack="false" CssClass="form-control" ToolTip="Please Select User for AO">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>FO :</label>
                                                        <asp:DropDownList ID="ddlFO" runat="server" TabIndex="2" AutoPostBack="false" CssClass="form-control" ToolTip="Please Select User for FO">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-12 text-center">
                                                         <asp:Button ID="btnSubmit" Text="Submit" CssClass="btn btn-primary" ToolTip="click to save" runat="server" TabIndex="3" OnClick="btnSubmit_Click" />
                                                    </div>
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
