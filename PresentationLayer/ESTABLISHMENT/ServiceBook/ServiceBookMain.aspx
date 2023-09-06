<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="ServiceBookMain.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_ServiceBookMain" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" Runat="Server">

     <script type="text/javascript">
         function pageLoad() {
             $('img').bind('contextmenu', function (e) {
                 return false;
             });
         }
    </script>

</asp:Content>

