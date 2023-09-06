<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ModifyFeesTransferStudentWise.aspx.cs" Inherits="ACCOUNT_ModifyFeesTransferStudentWise" %>

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

    <script type="text/javascript">

        function totAllSubjects(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
            var label = document.getElementById("<%=lblChkCount.ClientID %>");
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkFeesTransfer')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else {
                            e.checked = false;
                            label.innerHTML = "0";
                            document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
                        }

                    }
                }
            }
            label.innerHTML = hdfTot.value;
            document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
            if (headchk.checked == false) {
                hdfTot.value = "0";
                var count = $("[type='checkbox']:checked").length;
                label.innerHTML = count;
                //label.innerHTML = "0";
                document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
            }
        }

        function validateAssign() {
            debugger;
            var label = document.getElementById("<%=lblChkCount.ClientID %>");
            var count = $("[type='checkbox']:checked").length;
            label.innerHTML = count;
            document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
        }

    </script>

    <script src="../jquery/jquery-1.10.2.js" type="text/javascript"></script>

    <div style="z-index: 1; position: absolute;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updBank"
            DynamicLayout="true" DisplayAfter="0">
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
                    MODIFY FEES TRANSFER STUDENTWISE
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <%--<div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">--%>
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
    <asp:UpdatePanel ID="updBank" runat="server">
        <ContentTemplate>
            <fieldset class="vista-grid" style="width: auto">
                <legend class="titlebar">Modify Fees Transfer</legend>
                <table width="100%" cellpadding="3" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            Note : <span style="color: red; font-weight: bold">Please select only 200 student for
                                Single Voucher</span>
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
                            <asp:DropDownList ID="ddlVoucher" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit" AutoPostBack="true"
                                Width="250px"  OnSelectedIndexChanged="ddlVoucher_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--   <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />--%>
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
                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Submit" OnClick="btnShow_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <table id="lvCollection" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="vertical-align: top; padding: 5px; margin-top: 20px">
                                        <asp:Panel ID="Panel1" runat="server" Height="256px" ScrollBars="Vertical" Visible="false">
                                            <asp:ListView ID="lvFeeTransfer" runat="server">
                                                <LayoutTemplate>
                                                    <div class="vista-grid">
                                                        <table id="tblHead" class="vista-grid" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                            <thead class="datatable" style="font-size: 13px">
                                                                <th style="text-align: left; width: 4%; padding-left: 3px">
                                                                    <asp:CheckBox ID="chkBoxFeesTransfer" runat="server" onclick="totAllSubjects(this)"
                                                                        Checked="true" />
                                                                </th>
                                                                <th style="text-align: left; width: 20%">
                                                                    NAME
                                                                </th>
                                                                <th style="text-align: left; width: 10%">
                                                                    REC_NO
                                                                </th>
                                                                <th style="text-align: left; width: 10%">
                                                                    REC_DATE
                                                                </th>
                                                                <th style="text-align: left; width: 10%">
                                                                    DEGREE
                                                                </th>
                                                                <th style="text-align: left; width: 10%">
                                                                    AMOUNT
                                                                </th>
                                                                <th style="text-align: left; width: 10%">
                                                                    BRANCH
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
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                        <td style="text-align: left; width: 3%">
                                                            <asp:CheckBox ID="chkFeesTransfer" runat="server" onclick="validateAssign()" ToolTip='<%#Eval("Id") %>' Checked="true" />
                                                        </td>
                                                        <td style="text-align: left; width: 20%">
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 10%">
                                                            <asp:Label ID="lblRecptCode" runat="server" Text='<%# Eval("REC_NO") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 10%">
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("REC_DT", "{0:dd/MM/yyyy}")  %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 10%">
                                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("DEGREENAME")  %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 10%">
                                                            <asp:Label ID="lblDob" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 10%">
                                                            <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("BRANCH") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr id="trTotal" runat="server" visible="false">
                                    <td colspan="2">
                                        <asp:Label ID="lblCount" runat="server" Text="Receipt Count" Font-Bold="true" Font-Size="Medium">
                                        </asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblChkCount" runat="server" Font-Bold="true" Font-Size="Medium">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnCollect" runat="server" Text="Collect the record" Visible="false"
                                            OnClick="btnCollect_Click" />
                                    </td>
                                </tr>
                            </table>
                            <tr id="TrFees" runat="server" visible="false">
                                <td class="form_left_text" colspan="5">
                                    <div id="divTransfer" style="width: 100%; border: medium" runat="server">
                                        <div style="float: left; width: 49%">
                                            <fieldset class="vista-grid" style="width: auto">
                                                <legend class="titlebar">Fees Heads</legend>
                                                <asp:Panel ID="ScrollPanel" runat="server" Width="100%">
                                                    <asp:ListView ID="lstFees" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <table id="tblHead" class="vista-grid" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                    <thead class="datatable" style="font-size: 13px">
                                                                        <th style="text-align: left; width: 10%">
                                                                            F.NO
                                                                        </th>
                                                                        <th style="text-align: left; width: 50%">
                                                                            FEE HEAD
                                                                        </th>
                                                                        <th style="text-align: left; width: 40%">
                                                                            AMOUNT
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
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                <td style="text-align: left; width: 10%">
                                                                    <asp:Label ID="lblFeeHeadsNo" runat="server" Text='<%# Eval("FEE_HEAD") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left; width: 50%">
                                                                    <asp:Label ID="lblFeeHeads" runat="server" Text='<%# Eval("FEE_LONGNAME") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left; width: 40%">
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </fieldset>
                                        </div>
                                        <div style="float: right; width: 49%">
                                            <fieldset class="vista-grid" style="width: auto">
                                                <legend class="titlebar">Cash/Bank</legend>
                                                <table width="100%" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td class="form_left_label" style="width: 30%; height: 19px;">
                                                            Select Ledger
                                                        </td>
                                                        <td style="width: 1%">
                                                            <b>:</b>
                                                        </td>
                                                        <td class="auto-style2">
                                                            <asp:DropDownList ID="ddlLedger" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit"
                                                                Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                                            <asp:RequiredFieldValidator ID="rfvLedger" runat="server" InitialValue="0" ControlToValidate="ddlLedger"
                                                                ErrorMessage="Please Select Ledger" ValidationGroup="Save" Display="None">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td align="right">
                                                            Total Amount&nbsp;
                                                            <asp:Label ID="lblTotal" runat="server" ForeColor="Red" Text="0"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Voucher Date :
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="form_left_text" style="width: 45%">
                                                            <asp:TextBox ID="txtVoucherDate" Style="text-align: right" runat="server" Width="40%" />
                                                            &nbsp;<asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                                TargetControlID="txtVoucherDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtVoucherDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer Fees" ValidationGroup="Save"
                                        OnClick="btnTransfer_Click" Visible="false" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                        ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
                                </td>
                            </tr>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div id="divMsg" runat="server">
            </div>
            <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:BoundField DataField="NAME" HeaderText="Student Name" ControlStyle-Font-Size="Smaller">
                        <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                        <ItemStyle Wrap="False" Width="5%" HorizontalAlign="Left" Font-Size="Smaller" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REC_NO" HeaderText="Receipt No" ControlStyle-Font-Size="Smaller">
                        <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REC_DT" HeaderText="Receipt Date" ControlStyle-Font-Size="Smaller">
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
                        <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
            </asp:GridView>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>
