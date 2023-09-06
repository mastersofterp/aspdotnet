<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="ITLE_Result" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 185px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager_Main" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td colspan="3">
    <div id="divCollegename" runat="server" style="background-color:#0A5B9A; padding-top:10px; font-weight:bold; height:45px; color:White; font-size:25px; text-align:center">
    </div>
    </td>
    </tr>
    <tr>
    <td style="width:30%"></td>
    <td style="width:40%; padding-top:20px">
    
    <asp:UpdatePanel ID="UpdatePanel_Login" runat="server">
     <ContentTemplate>
    <asp:Panel ID="pnlCourse" runat="server" Width="100%">
        <fieldset  style="padding:5%;border-color:Green">
            <legend style="font-weight:bold; background-color:#C13732;color:White"><b>Test Result</b></legend>
            <table cellpadding="0" cellspacing="3" width="100%">
                <caption>
                    <br />
                    <tr>
                        <td colspan="2" style="color:#C13732; font-size:20px">
                        <center>
                           <b>WELCOME </b>  &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblUrname" runat="server" Font-Bold="True"></asp:Label>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                          <b>  Session <b>:</b>
                            </td>
                        <td style="color:#0A5B9A">
                            &nbsp;<asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>
                   
                    <tr>
                        <td >
                          <b>  Course Name <b>:</b>
                        </td>
                        <td style="color:#0A5B9A">
                           &nbsp;<asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                       <hr style="border-color:Black" />
                            &nbsp;</td>
                        
                    </tr>
                   
                    
                    <tr>
                        <td >
                           &nbsp;No. of Questions <b>:</b> </td>
                        <td >
                            &nbsp;&nbsp;<asp:Label ID="lblTotQue" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                           &nbsp;Answered <b>:</b>
                        </td>
                        <td>
                           &nbsp;&nbsp;<asp:Label ID="lblAnsQue" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                           &nbsp;Not Answered <b>:</b>&nbsp;
                        </td>
                        <td >
                            &nbsp;&nbsp;<asp:Label ID="lblUnAnsQue" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Correct <b>:</b> </td>
                        <td  style="color:Green">
                           &nbsp;&nbsp;<asp:Label ID="lblWriAns" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;Wrong <b>:</b></td>
                        <td  style="color:Red">
                            &nbsp;&nbsp;<asp:Label ID="lblWrongAns" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                     <tr style="background-color :#0A5B9A; border-color:#0A5B9A; height:25px">
                        <td  style="color:White;background-color :#0A5B9A;border-color:#0A5B9A">
                            &nbsp;<b>Total Score:</b></td>
                        <td  style="color:White;background-color :#0A5B9A;border-color:#0A5B9A">
                            &nbsp;&nbsp;<asp:Label ID="lblScore" runat="server" Font-Bold="True" Width="232px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;<asp:Label ID="lblQueNo" runat="server" Font-Bold="true" Width="232px" Visible="false"></asp:Label></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   
                    <tr>
                        <td colspan="2" 
                            style="font-family: Verdana; font-weight: normal; text-align: center">
                            <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" 
                                OnClientClick="window.close()" Text="OK" 
                                style="height: 26px; width: 50px" />
                        </td>
                    </tr>
                    <%--<asp:Timer ID="Timer1" runat="server" Interval="1000">
            </asp:Timer>--%>
                </caption>
            </table>
        </fieldset></asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    </td>
    <td style="width:30%"></td>
    </tr>
    </table>
    
    </form>
</body>
</html>
