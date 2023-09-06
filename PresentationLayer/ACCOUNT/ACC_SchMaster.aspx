<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ACC_SchMaster.aspx.cs" Inherits="ACCOUNT_ACC_SchMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
   <style type="text/css">
    .account_compname
    {
    	font-weight: bold;
    	margin-left:200px;
    }
    </style>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpan">
        <ProgressTemplate>
            <div id="IMGDIV" class="outerDivs" align="center" valign="middle" runat="server"
                style="position: absolute; left: 0%; top: 0%; visibility: visible; border-style: none;
                background-color: Transparent Black; z-index: 40; height: 100%; width: 100%;">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td valign="middle" align="center">
                            <img src="images/BarRotation.gif" style="height: 30px" /><br />
                            <%--Progressing Request....  --%>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updpan" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" style="height: 30px" colspan="6">
                        Schedule Master
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                            border: solid 1px #D0D0D0;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                            Visible="false">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 3%; vertical-align: top">
                                        <img src="../images/error.gif" align="absmiddle" alt="Error" />&nbsp;
                                    </td>
                                    <td style="width: 97%">
                                        <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                        </font>
                                        <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px;
                                            color: #CD0A0A"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                            Visible="false">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 3%; vertical-align: top">
                                        <img src="../images/confirm.gif" align="absmiddle" alt="confirm" />&nbsp;
                                    </td>
                                    <td style="width: 97%">
                                        <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana;
                                            font-size: 11px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>

                <script type="text/javascript" language="javascript">

                    function ScheduleExists() {
                        var i;
                        for (i = 0; i < document.getElementById('<%= lstSchedule.ClientID %>').length; i++) {
                            var a = document.getElementById('<%= txtSchName.ClientID %>').value;
                            var b = document.getElementById('<%= lstSchedule.ClientID %>')[i].text;
                            if (a == b) {

                                document.getElementById('<%= btnSubmit.ClientID %>').disabled = 'true';
                                alert('Name Already Exists');
                                return;
                            }
                            else {
                                document.getElementById('<%= btnSubmit.ClientID %>').disabled = false;
                            }
                        }
                    }
                </script>

                <tr>
                <td>
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset class="vista-grid" style="width: 50%">
                            <legend class="titlebar">Schedule Master</legend>
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="form_left_label" colspan="2">
                                        &nbsp; Note<span style="font-size: small">:</span><span style="font-weight: bold;
                                            font-size: x-small; color: red">* Marked is mandatory !</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 25%">
                                        Select Report Type <span style="color: #FF0000">*</span>
                                    </td>
                                    <td  style="width: 1%"><b>:</b></td>
                                    <td class="form_left_text" style="width: 79%">
                                        <asp:DropDownList ID="ddlReportType" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged"
                                            Width="60%" AutoPostBack="True">
                                            <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                            <asp:ListItem Value="IE">Income/Expenditure</asp:ListItem>
                                            <asp:ListItem Value="BS">Balance Sheet</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvReportType" runat="server" ErrorMessage="Please Select Report Type"
                                            ControlToValidate="ddlReportType" Display="None" InitialValue="0" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%" class="form_left_label">
                                       Select Schedule Type  <span style="color: #FF0000">*</span>
                                    </td>
                                    <td  style="width: 1%"><b>:</b></td>
                                    <td class="form_left_text" style="width: 79%">
                                        <asp:DropDownList ID="ddlSchType" runat="server" AppendDataBoundItems="true" Width="60%">
                                            <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                            <%-- <asp:ListItem Value="I">INCOME</asp:ListItem>
                                            <asp:ListItem Value="E">EXPENDITURE</asp:ListItem>
                                            <asp:ListItem Value="A">ASSETS</asp:ListItem>
                                            <asp:ListItem Value="L">LIABILITIES</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSchType" runat="server" ErrorMessage="Please Select Schedule Type"
                                            ControlToValidate="ddlSchType" Display="None" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr class="form_left_text">
                                    <td style="width: 25%" class="form_left_label">
                                        Schedule Name <span style="color: #FF0000">*</span>
                                    </td>
                                    <td  style="width: 1%"><b>:</b></td>
                                    <td class="form_left_text" style="width: 79%">
                                        <asp:TextBox runat="server" ID="txtSchName" Width="59%" OnTextChanged="txtSchName_TextChanged"
                                            Style="text-transform: uppercase" onblur="return ScheduleExists()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSchName" runat="server" ErrorMessage="Please Enter Schedule Name"
                                            ControlToValidate="txtSchName" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                               
                                <tr>
                                <td style="width: 25%" class="form_left_label"></td>
                                <td  style="width: 1%"><b></b></td>
                                    <td  style="width: 79%;padding-left: 5px;padding-top: 2px; padding-bottom: 2px;">
                                        <asp:Button runat="server" ID="btnSubmit" Text="Save Schedule" OnClick="btnSubmit_Click"
                                            ValidationGroup="submit" Width="100px" />
                                        &nbsp;
                                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                                            Width="100px" />
                                        <asp:ValidationSummary ID="vsSchMaster" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ValidationGroup="submit" ShowSummary="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%" class="form_left_label"></td>
                                <td  style="width: 1%"></td>
                                    <td rowspan="10" style="vertical-align: top; padding: 5px; width: 79%">
                                        <asp:ListBox ID="lstSchedule" runat="server" Style="overflow: scroll" Width="80%"
                                            Rows="10" AutoPostBack="True" OnSelectedIndexChanged="lstSchedule_SelectedIndexChanged">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" visible="false" id="trLedgerSelection" width="90%">
                            <td>
                                <fieldset class="vista-grid" style="width: 90%">
                                    <legend class="titlebar">Ledger Selection</legend>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td class="form_left_label" style="width: 11%">
                                                Search......
                                            </td>
                                            <td class="form_left_text" colspan="2" style="width: 46%">
                                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" Style="text-transform: uppercase"
                                                    Text="" Width="70%" Height="21px" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;<b> Selected Ledger </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 11%">
                                                &nbsp;
                                            </td>
                                            <td class="form_left_text" colspan="2">
                                                <asp:ListBox ID="lstLedgerName" runat="server" AutoPostBack="True" Rows="20" Width="100%"
                                                    SelectionMode="Multiple"></asp:ListBox>
                                                <%--<ajaxToolKit:ListSearchExtender ID="lstLedgerName_ListSearchExtender" runat="server"
                                                    Enabled="True" TargetControlID="lstLedgerName">
                                                </ajaxToolKit:ListSearchExtender>--%>
                                            </td>
                                            <td style="width: 2%">
                                                &nbsp;
                                                <asp:Button ID="btnAdd" runat="server" Text=">>" OnClick="btnAdd_Click" />
                                                <br />
                                                <br />
                                                &nbsp;
                                                <asp:Button ID="btnRemove" runat="server" Text="<<" OnClick="btnRemove_Click" />
                                            </td>
                                            <td class="form_left_text" colspan="3">
                                                <asp:ListBox ID="lstSelectedLedger" runat="server" AutoPostBack="True" Rows="20"
                                                    Width="100%"></asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" align="center">
                                                <asp:Button ID="btnAddLedger" runat="server" Text="Save Ledger" OnClick="btnAddLedger_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnCancelLedger" runat="server" Text="Cancel" OnClick="btnCancelLedger_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lstSelectedLedger" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
