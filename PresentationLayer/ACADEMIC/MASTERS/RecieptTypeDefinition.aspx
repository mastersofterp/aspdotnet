<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RecieptTypeDefinition.aspx.cs" Inherits="Payments_RecieptTypeDefinition"
    Title="" %>
<%@ Register Src="~/Academic/Masters/RecieptTypeDefinition.ascx" TagName="RecieptTypeDefinitionControl"
    TagPrefix="recieptTypeDefinition" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 

    <div>
        <recieptTypeDefinition:RecieptTypeDefinitionControl ID="uc" runat="server" />
    </div>   
</asp:Content>
