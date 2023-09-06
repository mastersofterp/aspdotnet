<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeesRefundReport.aspx.cs" Inherits="ACCOUNT_FeesRefundReport" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            margin-left: 150px;
        }
        .auto-style1
        {
            width: 161px;
        }
        .auto-style2
        {
            width: 192px;
        }
    </style>
    <link href="../Css/UpdateProgress.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script src="../jquery/jquery-1.10.2.js" type="text/javascript"></script>

    <div style="z-index: 1; position: absolute;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 1024px; padding-left: 5px; background-color: Black; height: 950px;
                    padding: 250px 50px 50px 450px; opacity: 0.4; filter: alpha(opacity=40);">
                    <div style="">
                        <img src="../IMAGES/anim_loading_75x75.gif" />
                        Please Wait..
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="99%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    FEES REFUND STUDENTWISE REPORT
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" colspan="2">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <fieldset class="vista-grid" style="width: auto">
        <legend class="titlebar">Fees Transfer Report</legend>
        <table width="100%" cellpadding="3" cellspacing="0">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td class="form_left_label" style="width: 10%; height: 19px;">
                    From Date
                </td>
                <td style="width: 1%">
                    <b>:</b>
                </td>
                <td class="form_left_text" style="width: 45%">
                    <asp:TextBox ID="txtFromDate" Style="text-align: right" runat="server" Width="20%" />
                    &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate">
                    </ajaxToolKit:MaskedEditExtender>
                    &nbsp;&nbsp;&nbsp;To Date :
                    <asp:TextBox ID="txtTodate" Style="text-align: right" runat="server" Width="20%" />
                    &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft"
                        TargetControlID="txtTodate">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTodate">
                    </ajaxToolKit:MaskedEditExtender>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="form_left_label" style="width: 10%; height: 19px;">
                    Degree
                </td>
                <td style="width: 1%">
                    <b>:</b>
                </td>
                <td class="auto-style2" colspan="2">
                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit"
                        Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                    <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" InitialValue="0" ControlToValidate="ddlDegree"
                        ErrorMessage="Please Select Degree" ValidationGroup="Submit" Display="None">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="form_left_label" style="width: 10%; height: 19px;">
                    Voucher No
                </td>
                <td style="width: 1%">
                    <b>:</b>
                </td>
                <td class="auto-style2" colspan="2">
                    <asp:DropDownList ID="ddlVoucher" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit"
                        Width="250px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvVoucher" runat="server" InitialValue="Please Select"
                        ControlToValidate="ddlVoucher" ErrorMessage="Please Select Voucher" ValidationGroup="Submit"
                        Display="None">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click"
                        ValidationGroup="Submit" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                        ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <div id="divMsg" runat="server">
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <asp:BoundField DataField="NAME" HeaderText="Student Name" ControlStyle-Font-Size="Smaller">
                <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                <ItemStyle Wrap="False" Width="5%" HorizontalAlign="Left" Font-Size="Smaller" />
            </asp:BoundField>
            <asp:BoundField DataField="PADDRESS" HeaderText="Address" ControlStyle-Font-Size="Smaller">
                <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
            </asp:BoundField>
            <asp:BoundField DataField="PPINCODE" HeaderText="Pincode" ControlStyle-Font-Size="Smaller">
                <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
            </asp:BoundField>
            <asp:BoundField DataField="REC_NO" HeaderText="Receipt No" ControlStyle-Font-Size="Smaller">
                <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
            </asp:BoundField>
            <asp:BoundField DataField="REC_DT" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Receipt Date"
                ControlStyle-Font-Size="Smaller">
                <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%" Font-Size="Smaller" />
            </asp:BoundField>
            <asp:BoundField DataField="DEGREENAME" HeaderText="Degree" ControlStyle-Font-Size="Smaller">
                <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                <ItemStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
            </asp:BoundField>
            <asp:BoundField DataField="TOTAL_AMT" HeaderText="Amount" ControlStyle-Font-Size="Smaller">
                <HeaderStyle HorizontalAlign="Left" Width="3%" Font-Size="Smaller" />
                <ItemStyle HorizontalAlign="Left" Width="3%" Font-Size="Smaller" />
            </asp:BoundField>
            <asp:BoundField DataField="BRANCH" HeaderText="Branch" ControlStyle-Font-Size="Smaller">
                <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle HorizontalAlign="Center" BackColor="AliceBlue" />
    </asp:GridView>
</asp:Content>
