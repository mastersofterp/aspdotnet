<%@ Control Language="C#" AutoEventWireup="true" CodeFile="feed_back.ascx.cs" Inherits="UC_feed_back" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

<table cellpadding="0" cellspacing="0" width="600px" style="border: 2px solid #3D9DDD;background-color:#FFFFFF">
            <tr>
                  <td class="vista_page_title_bar" colspan="2" style="height:30px">FEEDBACK FORM
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding:5px;font-size:9pt;color:Red;">
                    Help us make our site better! To let us know how we are doing, please fill in this form :
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding:5px;font-size:9pt;">
                    Your Comments, Suggestions and Queries :
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding:5px;font-size:9pt;color:Red;">
                    Personal Information (Your data will be kept strictly private)  (To fill all information are compulsory)
                </td>
            </tr>
            <tr>
                <td style="font-size:9pt;text-align:right">Name :</td>
                <td style="padding-left:5px;text-align:left;">
                    <asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="250px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtName" Display="None" ErrorMessage="Name Required" 
                        ValidationGroup="feedback"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="font-size:9pt;text-align:right">Address :</td>
                <td style="padding-left:5px;text-align:left;">
                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="250px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtAddress" Display="None" ErrorMessage="Address Required" 
                        ValidationGroup="feedback"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="font-size:9pt;text-align:right">Contact Number :</td>
                <td style="padding-left:5px;text-align:left;">
                    <asp:TextBox ID="txtContactNo" runat="server" Width="250px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtContactNo" Display="None" 
                        ErrorMessage="Contact No Required" ValidationGroup="feedback"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="font-size:9pt;text-align:right">Email :</td>
                <td style="padding-left:5px;text-align:left;">
                    <asp:TextBox ID="txtEmail" runat="server" Width="250px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtEmail" Display="None" ErrorMessage="Email Required" 
                        ValidationGroup="feedback"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmail" Display="None" ErrorMessage="Email Invalid" 
                        ValidationGroup="feedback" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="font-size:9pt;text-align:right" valign="top">Comments :</td>
                <td style="padding-left:5px;text-align:left;">
                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" 
                        Width="300px"  Height="150px"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtComments" Display="None" ErrorMessage="Comments Required" 
                        ValidationGroup="feedback"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="form_button" colspan="2">          
                    <asp:Button ID="btnOk" runat="server" Text="Submit" ValidationGroup="feedback" OnClick="btnOk_Click" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"  />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="feedback" />
                </td>
            </tr>
            <tr>
                <td class="form_button" colspan="2">          
                    <asp:Label ID="lblStatus" runat="server" Font-Size="9pt" ForeColor="Red" />
                </td>
            </tr>
</table>