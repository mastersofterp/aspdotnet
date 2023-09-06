<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Thanks.aspx.cs" Inherits="Itle_Thanks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style2 {
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
                    <div id="divCollegename" runat="server" style="background-color: #0A5B9A; padding-top: 10px; font-weight: bold; height: 45px; color: White; font-size: 25px; text-align: center">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 30%"></td>
                <td style="width: 40%; padding-top: 20px">

                    <asp:UpdatePanel ID="UpdatePanel_Login" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlCourse" runat="server" Width="100%">
                                <fieldset style="padding: 5%; border-color: Green">
                                    <legend style="font-weight: bold; background-color: #C13732; color: White"><b>Thanks</b></legend>
                                    <table cellpadding="0" cellspacing="3" width="100%">
                                        <caption>
                                            <br />
                                            <tr>
                                                <td colspan="2" style="color: #C13732; font-size: 20px">
                                                    <center>
                                                        <b>WELCOME </b>&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblUrname" runat="server" Font-Bold="True"></asp:Label>
                                                    </center>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Session <b>:</b>
                                                </td>
                                                <td style="color: #0A5B9A">&nbsp;<asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <b>Course Name <b>:</b>
                                                </td>
                                                <td style="color: #0A5B9A">&nbsp;<asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <hr style="border-color: Black" />
                                                    &nbsp;</td>

                                            </tr>

                                            <tr>
                                                <td colspan="2" style="font-family: Verdana; font-weight: normal; text-align: center; color: #0A5B9A">
                                                    <b>Thank You For Appearing In Test</b>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                            </tr>


                                            <tr>
                                                <td colspan="2" style="font-family: Verdana; font-weight: normal; text-align: center">
                                                    <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" OnClientClick="window.close()" />
                                                </td>
                                            </tr>
                                        </caption>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 30%"></td>
            </tr>
        </table>

    </form>
</body>
</html>

