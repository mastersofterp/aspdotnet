<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BranchEntry.aspx.cs" Inherits="Academic_BranchEntry" %>
<%@ Register Src="~/Academic/Masters/Branch.ascx" TagName="BRANCH" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" width="100%" >
    <tr>
       <td>
           <UC:BRANCH ID="ucbranch" runat="server" />
        </td>
    </tr>
 </table>
</asp:Content>

