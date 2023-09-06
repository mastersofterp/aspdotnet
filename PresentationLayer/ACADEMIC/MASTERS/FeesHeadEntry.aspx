<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FeesHeadEntry.aspx.cs" Inherits="Academic_FeesHeadEntry"  %>
<%@ Register Src="~/Academic/Masters/FeeHeads.ascx" TagPrefix="UC" TagName="FeesHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" border="0" width="100%"> 
    <tr>
        <td>
            
            <UC:FeesHead ID="ucFeeshead" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>

