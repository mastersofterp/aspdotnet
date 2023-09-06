<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_Validations.aspx.cs" Inherits="Acc_Validations"
    Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updBank">
        <ProgressTemplate>
            <div id="IMGDIV" class="outerDivs" align="center" valign="middle" runat="server"
                style="position: absolute; left: 0%; top: 0%; visibility: visible; border-style: none;
                background-color: Transparent Black; z-index: 40; height: 100%; width: 100%;">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td valign="middle" align="center">
                            <img src="images/BarRotation.gif" style="height: 30px" /><br />
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
        <ProgressTemplate>
            <div id="IMGDIV" class="outerDivs" align="center" valign="middle" runat="server"
                style="position: absolute; left: 0%; top: 0%; visibility: visible; border-style: none;
                background-color: Transparent Black; z-index: 40; height: 100%; width: 100%;">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td valign="middle" align="center">
                            <img src="images/BarRotation.gif" style="height: 30px" /><br />
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    ACCOUNT MASTER
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td style="padding: 10px">
                <div id="divCompName" runat="server" class="account_compname">
                    </div>
                    <asp:UpdatePanel ID="updBank" runat="server">
                        <ContentTemplate>
                            <fieldset class="fieldset">
                                <legend class="legend">Account Details</legend>
                                  <table width="100%" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td style="text-align: left">
                                            Enter Account Name:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlError_Type" runat="server" 
                                             meta:resourcekey="ddlTranTypeResource1" >
                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="--Please Select--"></asp:ListItem>
                                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource1" Text="DR-CR TOTAL MISMATCH"></asp:ListItem>
                                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource2" Text="PARTY MISMATCH"></asp:ListItem>
                                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource3" Text="OPARTY MISMATCH"></asp:ListItem>
                                                    <asp:ListItem Value="4" meta:resourcekey="ListItemResource4" Text="DATE MISMATCH"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnSubmit" Text="Show" runat="server" OnClick="btnSubmit_Click"/>
                                        </td>
                                        <td>
                                          
                                               
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="submit" />
                                        </td>
                                        <td>
                                            &nbsp;
                                            <%--<asp:RequiredFieldValidator ID="rfvAccountName" runat="server" ControlToValidate="txtAccountName"
                                                Display="None" ErrorMessage="Please Enter Account name..!" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top; padding: 5px">
                                                        <asp:GridView runat="server" ID="GvError" AutoGenerateColumns="false">
                                                           
                                                            <Columns>
                                                                <asp:BoundField DataField="TRANSACTION_TYPE" HeaderText="TRANSACTION" />
                                                                <asp:BoundField DataField="VOUCHER_NO" HeaderText="VOUCHER" />
                                                            </Columns>
                                                           
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                  </table>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
