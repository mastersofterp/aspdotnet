<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ErrPage.aspx.cs" Inherits="ACADEMIC_RuntimeErrorHandling_ErrPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center><h1><b>Oops !</b></h1></center>
    <center><h3><b>Sorry, Something went wrong !</b></h3></center>
    <center><h5>Error has been occured during process.</h5></center>
    <center><h5>Please contact us and we will solve this issue ASAP.</h5></center>
    <%--<center><a class="btn btn-primary"><i class="fa fa-reply" aria-hidden="true"></i> Back to Page</a></center>--%>
    <center>
        <asp:LinkButton ID="lbtnBackToPage" runat="server" CssClass="btn btn-primary" OnClick="lbtnBackToPage_Click"><i class="fa fa-reply" aria-hidden="true"></i>  Back to Page</asp:LinkButton>
    </center>
</asp:Content>

