<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="PrintApplicationFormReport.aspx.cs" Inherits="Reports_PrintApplicationFormReport" %>
<!DOCTYPE html>

<html xmlns ="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   
    <%--<link href="Plugin/newbootstrap4/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="<%=Page.ResolveClientUrl("Plugin/newbootstrap4/css/bootstrap.min.css") %>" rel="stylesheet" />
    <%--<link href="Plugin/newbootstrap4/css/customcss.css" rel="stylesheet" />--%>
    <link href="<%=Page.ResolveClientUrl("Plugin/newbootstrap4/css/customcss.css") %>" rel="stylesheet" />
    <%--<link href="Plugin/newbootstrap4/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="<%=Page.ResolveClientUrl("Plugin/newbootstrap4/font-awesome-4.6.3/css/font-awesome.min.css") %>" rel="stylesheet" />
    <%--<link href="Plugin/newbootstrap4/css/ajaxCalender.css" rel="stylesheet" />--%>
    <link href="<%=Page.ResolveClientUrl("Plugin/newbootstrap4/css/ajaxCalender.css") %>" rel="stylesheet" />



    <%--<link href="Plugin/datatable-responsive/css/dataTables.bootstrap4.min.css" rel="stylesheet" />--%>
    <link href="<%=Page.ResolveClientUrl("Plugin/datatable-responsive/css/dataTables.bootstrap4.min.css") %>" rel="stylesheet" />

    <%-- <link href="Plugin/datatable-responsive/css/buttons.bootstrap4.min.css" rel="stylesheet" />--%>
    <link href="<%=Page.ResolveClientUrl("Plugin/datatable-responsive/css/buttons.bootstrap4.min.css") %>" rel="stylesheet" />

    <%--<script src="Plugin/newbootstrap4/js/jquery-3.5.1.min.js"></script>--%>
    <script src="<%=Page.ResolveClientUrl("~/Plugin/newbootstrap4/js/jquery-3.5.1.min.js") %>"></script>

    <%--<script src="Plugin/datatable-responsive/js/jquery.dataTables.min.js"></script>--%>
    <script src="<%=Page.ResolveClientUrl("~/Plugin/datatable-responsive/js/jquery.dataTables.min.js") %>"></script>
    <%--<script src="Plugin/datatable-responsive/js/dataTables.bootstrap4.min.js"></script>--%>
    <script src="<%=Page.ResolveClientUrl("~/Plugin/datatable-responsive/js/dataTables.bootstrap4.min.js") %>"></script>
    <%--<script src="Plugin/datatable-responsive/js/dataTables.fixedHeader.min.js"></script>
    <script src="Plugin/datatable-responsive/js/dataTables.responsive.min.js"></script>
    <script src="Plugin/datatable-responsive/js/responsive.bootstrap4.min.js"></script>--%>


    <%--<script src="Plugin/newbootstrap4/js/popper.min.js"></script>--%>
    <script src="<%=Page.ResolveClientUrl("~/Plugin/newbootstrap4/js/popper.min.js") %>"></script>
    <%--<script src="Plugin/newbootstrap4/js/bootstrap.min.js"></script>--%>
    <script src="<%=Page.ResolveClientUrl("~/Plugin/newbootstrap4/js/bootstrap.min.js") %>"></script>

    <link href="<%=Page.ResolveClientUrl("Plugin/select2/select2.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/Plugin/select2/select2.js") %>"></script>
</head>
<body>
   <%-- <form id="form1" runat="server">
        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-outline-primary" Text="Back" OnClick="btnBack_Click" />
    </form>
  --%>
</body>
</html>