<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="~/ACCOUNT/Acc_PaymentType.aspx.cs" Inherits="Acc_PaymentType" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            margin-left: 250px;
        }
    </style>
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
                    Payment Type
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
                            <fieldset class="vista-grid" style="width: 100%;">
                                <legend class="titlebar">Payment Details</legend>
                                <table width="100%" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td style="text-align: left; width: 15%">
                                            Enter Group Name
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td colspan="2" >
                                            <asp:TextBox ID="txtGrpName" runat="server" Width="207px" ValidationGroup="submit"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender FilterType="Custom" ID="ftbGrpName" FilterMode="InvalidChars"
                                                InvalidChars="1234567890" runat="server" TargetControlID="txtGrpName" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                                OnClick="btnSubmit_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RequiredFieldValidator ID="rfvGrpName" runat="server" ControlToValidate="txtGrpName"
                                                Display="None" ErrorMessage="Please Enter Group name..!" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top; padding: 5px">
                                                        <asp:ListView ID="lvPaymentParty" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <div class="ui-widget-header">
                                                                        Select Payment Type</div>
                                                                    <table id="tblHead" class="vista-grid" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                        <thead class="datatable">
                                                                            <th style="text-align: left; width: 10%">
                                                                            </th>
                                                                            <th style="text-align: left; width: 45%">
                                                                                Payment Type Name
                                                                            </th>
                                                                            <th style="text-align: left; width: 45%">
                                                                                Payment Type Full Name
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
                                                                        <%--<asp:CheckBoxList ID="ChkPayment" runat="server" ToolTip='<%#Eval("PAYTYPENO") %>' AutoPostBack="True"></asp:CheckBoxList>--%>
                                                                        <asp:CheckBox ID="ChkPayment" runat="server" ToolTip='<%#Eval("PAYTYPENO") %>' />
                                                                        <%--
                                                                        <asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="../images/edit.gif"
                                                                            CommandArgument='<%#Eval("ACC_ID") %>' ToolTip="Edit Record" OnClick="ImageButtonEdit_Click" />--%>
                                                                    </td>
                                                                    <td style="text-align: left; width: 45%">
                                                                        <asp:Label ID="lblPartyName" runat="server" Text='<%# Eval("PAYTYPENAME") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left; width: 45%">
                                                                        <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top">
                                            <table cellpadding="0" cellspacing="0" style="width: 60%">
                                                <tr>
                                                    <td style="vertical-align: top; padding: 5px">
                                                        <asp:ListView ID="lvPartyDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <div class="ui-widget-header">
                                                                        Edit Group Payment Type Details</div>
                                                                    <table id="tblHead" class="vista-grid" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                        <thead class="datatable">
                                                                            <th style="text-align: left; width: 10%">
                                                                                Edit
                                                                            </th>
                                                                            <th style="text-align: left; width: 90%">
                                                                                Group Name
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
                                                                        <%--<asp:CheckBoxList ID="ChkPayment" runat="server" ToolTip='<%#Eval("PAYTYPENO") %>' AutoPostBack="True"></asp:CheckBoxList>--%>
                                                                        <%--<asp:CheckBox ID="ChkPayment" runat="server" ToolTip='<%#Eval("PAYTYPENO") %>' />--%>
                                                                        <asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="../images/edit.gif"
                                                                            CommandArgument='<%#Eval("PGROUP_NO") %>' ToolTip="Edit Record" OnClick="ImageButtonEdit_Click" />
                                                                    </td>
                                                                    <td style="text-align: left; width: 90%">
                                                                        <asp:Label ID="lblPartyName" runat="server" Text='<%# Eval("GROUPNAME") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 25%">
                                            &nbsp;
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="submit" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
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
