<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="qualificationMas.aspx.cs" Inherits="Masters_qualificationMas" Title="" %>
<%@ Register Src="~/ACADEMIC/MASTERS/qualificationMas.ascx" TagName="qualification" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:qualification ID="ucQualification" runat="server" />
</asp:Content>