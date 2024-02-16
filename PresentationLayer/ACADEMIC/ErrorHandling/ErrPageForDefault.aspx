<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrPageForDefault.aspx.cs" Inherits="ACADEMIC_ErrorHandling_ErrPageForDefault" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content-wrapper">
            <div class="container">
                <div class="row" style="padding-top:15%">
                    <center><h1><b>Oops !</b></h1></center>
                    <center><h3><b>Sorry, Something went wrong !</b></h3></center>
                    <center><h5>Error has been occured during process.</h5></center>
                    <center><h5>Please contact us and we will solve this issue ASAP.</h5></center>
                    <center ><h5 runat="server" id="errormsg">Please send an email with error details to <asp:Label style="color: #007bff;" ID="lblemail" runat="server"></asp:Label></h5></center>
                    <%--<center><a class="btn btn-primary"><i class="fa fa-reply" aria-hidden="true"></i> Back to Page</a></center>--%>
                    <center>
                        <asp:LinkButton ID="lbtnBackToPage" runat="server" CssClass="btn btn-primary" OnClick="lbtnBackToPage_Click"><i class="fa fa-reply" aria-hidden="true"></i>  Go to Login Page</asp:LinkButton>
                    </center>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
