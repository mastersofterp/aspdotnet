<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="quarterMaster.aspx.cs" Inherits="Masters_quarterMaster" Title="" %>
<%@ Register Src="~/PayRoll/Masters/quarterMas.ascx" TagName="quarter" TagPrefix="uc1" %>
<%--------%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:quarter ID="ucQuarter" runat="server" />
</asp:Content>
