<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="PhotoReval_Response.aspx.cs" Inherits="PhotoReval_Response" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style>

        input {
  background-color: #4CAF50; /* Green */
  border: none;
  color: white;
  padding: 8px 10px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
}

    </style>


</head>
    <body>
          <form id="form1" runat="server">
    <table style="text-align:center" width="100%">
        <tr style="text-align:center">
           
            <td colspan="3" style="padding-left: -12%; margin-top: 30px; text-align:center">
                <%--                <div style="width: 500px; margin: 0px auto; border: groove; margin-top: 30px; height: 200px;">--%>
                <asp:Label ID="lblmessage" Font-Size="200%" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnReceiptCode" runat="server" />
                <%--                </div>--%>
            </td>
        </tr>
        <br />
        <tr style="margin-top:10px;">
            <td style="text-align:center">
                <asp:Button ID="btnReports" runat="server" Text="Print Receipt" Visible="false" OnClick="btnReports_Click" 
                    class="buttonStyle ui-corner-all btn btn-success" /> &nbsp;&nbsp;&nbsp;
            
                <asp:Button ID="btnRegistrationSlip" runat="server" Text="Print Registration Slip" Visible="false" 
                    OnClick="btnRegistrationSlip_Click" class="buttonStyle ui-corner-all btn btn-success" />
           
             <%--  <i class="fa fa-home text-primary" ></i>--%> <asp:LinkButton ID="lbtnGoBack" runat="server" Text="Go To Back"
                  OnClick="lbtnGoBack_Click" Visible="false" Font-Bold="true" style="font-weight:bold;margin-left: 35px;"></asp:LinkButton>
            </td>
         
        </tr>
         
    </table>
    <div id="divMsg" runat="server">
    </div>


               </form>
    </body>
    </html>

