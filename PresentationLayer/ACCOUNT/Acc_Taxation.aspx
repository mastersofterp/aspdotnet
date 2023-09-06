<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_Taxation.aspx.cs" Inherits="Acc_Taxation"
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
                    TAX MASTER
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
                             <fieldset class="vista-grid">
                                <legend class="titlebar">Tax Details</legend>
                                  <table width="100%" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td style="text-align: left;width:200px">
                                            Enter Tax Name:
                                        </td>
                                        <td style="width:250px">
                                            <asp:TextBox ID="txTaxName" runat="server" Width="207px" ValidationGroup="submit"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender FilterType="Custom" ID="flt1" FilterMode="InvalidChars" InvalidChars="1234567890" runat="server"  TargetControlID="txTaxName"/>
                                            
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                                ValidationGroup="submit" />
                                            &nbsp;
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
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
                                            <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="txTaxName"
                                                Display="None" ErrorMessage="Please Enter bank name..!" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top; padding: 5px">
                                                        <asp:ListView ID="lvTaxes" runat="server" >
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <div class="ui-widget-header">
                                                                        Tax List</div>
                                                                    <table id="tblHead" class="vista-grid" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                        <thead class="datatable">
                                                                        <th style="text-align: left; width: 10%">
                                                                        </th>
                                                                        <th style="text-align: left; width: 0%">
                                                                            <%--Id. No.--%>
                                                                        </th>
                                                                        <th style="text-align: left; width: 90%">
                                                                            Tax Name
                                                                        </th>
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                                <div id="demo-grid" class="vista-grid">
                                                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                    <%--</div>--%>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                    <td style="text-align: left; width: 10%">
                                                                        <asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="../images/edit.gif"
                                                                            CommandArgument='<%#Eval("TAX_ID") %>' ToolTip="Edit Record" OnClick="ImageButtonEdit_Click" />
                                                                    </td>
                                                                    <td style="text-align: left; width: 0%">
                                                                        <asp:Label ID="lblBankNo" runat="server" Text='<%# Eval("TAX_ID") %>' Visible="false" ></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left; width: 90%">
                                                                        <asp:Label ID="lblBnakName" runat="server" Text='<%# Eval("TAX_NAME") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
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
