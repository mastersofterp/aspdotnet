<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FeesHeadCurrencyDefinition.aspx.cs" 
Inherits="ACADEMIC_MASTERS_FeesHeadCurrencyDefinition" %>

<%@ Register Src="~/Academic/Masters/CurrencyHeads.ascx" TagPrefix="UC" TagName="CurrencyHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellpadding="0" cellspacing="0" border="0" width="100%"> 
    <tr>
        <td>
            <UC:CurrencyHead ID="ucCurrencyhead" runat="server" />
        </td>
    </tr>

</table>

</asp:Content>

