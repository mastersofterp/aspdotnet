<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="masters.aspx.cs" Inherits="masters" Title="" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolKit" %>
<%@ Register Src="~/Masters/masters.ascx" TagName="masters" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <%--<asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upMasters" DynamicLayout="true" DisplayAfter="0">--%>
        <asp:UpdateProgress ID="updProg" runat="server" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
 </div>
<%--<asp:UpdatePanel ID="upMasters" runat="server">
    <ContentTemplate>--%>
        <uc1:masters ID="ucmaster" runat="server" />        
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>