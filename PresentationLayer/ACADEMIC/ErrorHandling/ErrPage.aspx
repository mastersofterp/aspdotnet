<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ErrPage.aspx.cs" Inherits="ACADEMIC_RuntimeErrorHandling_ErrPage" %>

<%--/*                                  
---------------------------------------------------------------------------------------------------------------------------                                  
Created By :                                  
Created On :                            
Purpose    :                      
Version    :                        
---------------------------------------------------------------------------------------------------------------------------                                  
Version   Modified On   Modified By      Purpose                                  
---------------------------------------------------------------------------------------------------------------------------                                  
1.0.1     12-03-2024    Anurag Baghele   [52380]-Added massage with email
--------------------------------------------------------------------------------------------------------------------------                                           
*/ --%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center><h1><b>Oops !</b></h1></center>
    <center><h3><b>Sorry, Something went wrong !</b></h3></center>
    <center><h5>Error has been occured during process.</h5></center>
    <center><h5>Please contact us and we will solve this issue ASAP.</h5></center>
    <%--<1.0.1>--%>
     <center ><h5 runat="server" id="errormsg">Please send an Email with rror details to <asp:Label style="color: #007bff;" ID="lblemail" runat="server"></asp:Label></h5></center>
    <%--<center><a class="btn btn-primary"><i class="fa fa-reply" aria-hidden="true"></i> Back to Page</a></center>--%>
    <center>
        <asp:LinkButton ID="lbtnBackToPage" runat="server" CssClass="btn btn-primary" OnClick="lbtnBackToPage_Click"><i class="fa fa-reply" aria-hidden="true"></i>  Back to Page</asp:LinkButton>
    </center>
</asp:Content>

