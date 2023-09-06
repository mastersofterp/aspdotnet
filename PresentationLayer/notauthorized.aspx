<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="notauthorized.aspx.cs" Inherits="notauthorized" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="text-align:center;padding-top:10px;color:red">
    <h1><%= username %> - Authorization Failed!!!</h1>
</div>
<div style="width:100%;height:400px;padding-top:50px;text-align:center">
    <asp:Label ID="lblMsg" runat="server" />
</div>
</asp:Content>