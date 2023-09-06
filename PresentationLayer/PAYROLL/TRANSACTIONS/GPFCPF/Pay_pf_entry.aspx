<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_pf_entry.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">
                        PF ENTRY&nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
                <tr>
                    <td>
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                            border: solid 1px #D0D0D0;">
                        </div>
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                            font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                    ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                    font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                            </div>
                            <div>
                                <p class="page_help_head">
                                    <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                    <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                    Edit Record
                                </p>
                                <p class="page_help_text">
                                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                            </div>
                        </div>

                        <script type="text/javascript" language="javascript">
                            // Move an element directly on top of another element (and optionally
                            // make it the same size)
                            function Cover(bottom, top, ignoreSize) {
                                var location = Sys.UI.DomElement.getLocation(bottom);
                                top.style.position = 'absolute';
                                top.style.top = location.y + 'px';
                                top.style.left = location.x + 'px';
                                if (!ignoreSize) {
                                    top.style.height = bottom.offsetHeight + 'px';
                                    top.style.width = bottom.offsetWidth + 'px';
                                }
                            }
                        </script>

                        <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                            <Animations>
                                <OnClick>
                                    <Sequence>
                                        <%-- Disable the button so it can't be clicked again --%>
                                        <EnableAction Enabled="false" />
                                        
                                        <%-- Position the wire frame on top of the button and show it --%>
                                        <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                        
                                        <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                                        <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                        <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                        <FadeIn AnimationTarget="info" Duration=".2"/>
                                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                        
                                        <%-- Flash the text/border red and fade in the "close" button --%>
                                        <Parallel AnimationTarget="info" Duration=".5">
                                            <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                            <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                        </Parallel>
                                        <Parallel AnimationTarget="info" Duration=".5">
                                            <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                            <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                            <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                                        </Parallel>
                                    </Sequence>
                                </OnClick>
                            </Animations>
                        </ajaxToolKit:AnimationExtender>
                        <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                            <Animations>
                                <OnClick>
                                    <Sequence AnimationTarget="info">
                                        <%--  Shrink the info panel out of view --%>
                                        <StyleAction Attribute="overflow" Value="hidden"/>
                                        <Parallel Duration=".3" Fps="15">
                                            <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                            <FadeOut />
                                        </Parallel>
                                        
                                        <%--  Reset the sample so it can be played again --%>
                                        <StyleAction Attribute="display" Value="none"/>
                                        <StyleAction Attribute="width" Value="250px"/>
                                        <StyleAction Attribute="height" Value=""/>
                                        <StyleAction Attribute="fontSize" Value="12px"/>
                                        <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                        
                                        <%--  Enable the button so it can be played again --%>
                                        <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                                    </Sequence>
                                </OnClick>
                                <OnMouseOver>
                                    <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                                </OnMouseOver>
                                <OnMouseOut>
                                    <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                                </OnMouseOut>
                            </Animations>
                        </ajaxToolKit:AnimationExtender>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px">
                        <div style="text-align: left; width: 95%;">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay">Selection Criteria</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            Employee :
                                        </td>
                                        <td class="form_left_text" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true"
                                                Width="300px" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            Eligible For :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            Contribution Entry :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:CheckBox AutoPostBack="true" runat="server" ID="chkpfcontribution" Text="PF Contribution Entry"
                                                OnCheckedChanged="pfcontribution_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px;" colspan="5">
                        <br />
                        <div id="divPfContribution" runat="server" style="text-align: left; width: 95%;"
                            visible="false">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay">PF Contribution</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            Month Year :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtMonthYearContributionAmount" runat="server" Width="80px"></asp:TextBox>
                                            &nbsp;<asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="ceContributionAmount" runat="server" Format="MM/yyyy"
                                                TargetControlID="txtMonthYearContributionAmount" PopupButtonID="Image3" Enabled="true"
                                                EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtMonthYearContributionAmount" runat="server"
                                                ControlToValidate="txtMonthYearContributionAmount" Display="None" ErrorMessage="Month Year in (MM/yyyy Format)"
                                                ValidationGroup="PF" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            Deduction Amount1 :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount1" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount1" runat="server" ControlToValidate="txtDeductionAmount1"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount1" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount1" runat="server" Display="None" ErrorMessage="Deduction Amount1 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount1"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            Deduction Amount2 :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount2" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount2" runat="server" ControlToValidate="txtDeductionAmount2"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount2" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount2" runat="server" Display="None" ErrorMessage="Deduction Amount2 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount2"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            Deduction Amount3 :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount3" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount3" runat="server" ControlToValidate="txtDeductionAmount3"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount3" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount3" runat="server" Display="None" ErrorMessage="Deduction Amount3 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount3"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            Deduction Amount4 :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount4" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount4" runat="server" ControlToValidate="txtDeductionAmount4"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount4" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount4" runat="server" Display="None" ErrorMessage="Deduction Amount4 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount4"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="padding-left: 5px">
                                            <br />
                                            <asp:Panel ID="PnlGpfCpfContibutionEntry" runat="server">
                                                <table cellpadding="0" cellspacing="0" style="width: 99%;">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:ListView ID="lvItemGpfCpfContibutionEntry" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <br />
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid" class="vista-grid">
                                                                        <div class="titlebar">
                                                                            GPF/CPF Contribution</div>
                                                                        <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr class="header">
                                                                                <th>
                                                                                    Action
                                                                                </th>
                                                                                <th>
                                                                                    Month Year
                                                                                </th>
                                                                                <th>
                                                                                    G.P.F
                                                                                </th>
                                                                                <th>
                                                                                    G.P.F ADD
                                                                                </th>
                                                                                <th>
                                                                                    G.P.F Loan
                                                                                </th>
                                                                                <%-- <th>
                                                                                    Deduction Amount4
                                                                                </th>--%>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MONYEAR")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H1")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H2")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H3")%>
                                                                        </td>
                                                                        <%--<td>
                                                                            <%# Eval("H4")%>
                                                                        </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MONYEAR")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H1")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H2")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("H3")%>
                                                                        </td>
                                                                        <%-- <td>
                                                                            <%# Eval("H4")%>
                                                                        </td>--%>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                            <%--<div class="vista-grid_datapager">
                                                                            <asp:DataPager ID="dpPagerGroupMaster" runat="server" PagedControlID="GpfContibutionEntry"
                                                                                PageSize="5" OnPreRender="dpPagerGroupMaster_PreRender">
                                                                                <Fields>
                                                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                                                </Fields>
                                                                            </asp:DataPager>
                                                                        </div>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px;">
                        <br />
                        <ajaxToolKit:TabContainer runat="server" ID="Tabs" OnActiveTabChanged="ActiveTabChanged"
                            AutoPostBack="true" ActiveTabIndex="0" Width="750px" CssClass="linkedin linkedin-tab">
                            <ajaxToolKit:TabPanel runat="server" ID="OpeningBalance" HeaderText="Opening Balance">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="updOpeningBalance" runat="server">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label">
                                                        Fin.Year Start Date :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:TextBox ID="txtFromDate" runat="server" Width="80px" OnTextChanged="txtFromDate_TextChanged"
                                                            AutoPostBack="true"></asp:TextBox>
                                                        &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                            PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                            Display="None" ErrorMessage="Please Select Fin.Year Start Date in (dd/MM/yyyy Format)"
                                                            ValidationGroup="PF" SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                            ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Fin.Year Start Date"
                                                            InvalidValueMessage="Fin.Year Start Date is Invalid (Enter mm/dd/yyyy Format)"
                                                            Display="None" TooltipMessage="Please Enter Fin.Year Start Date" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="PF" SetFocusOnError="True" />
                                                    </td>
                                                    <td class="form_left_label">
                                                        Fin.Year End Date :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:TextBox ID="txtToDate" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                        &nbsp;<%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label">
                                                        Opening Balance :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:TextBox runat="server" ID="txtOB" Width="80px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtOB" runat="server" ControlToValidate="txtOB"
                                                            Display="None" ErrorMessage="Please Enter Opening Balance" ValidationGroup="PF"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvtxtOB" runat="server" Display="None" ErrorMessage="Opening Balance Should be Numeric"
                                                            ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtOB" Type="Double"></asp:CompareValidator>
                                                    </td>
                                                    <td class="form_left_label">
                                                        Loan Opening Balance:
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:TextBox runat="server" ID="txtLoanOB" Width="80px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvLoanOB" runat="server" ControlToValidate="txtOB"
                                                            Display="None" ErrorMessage="Please Enter Loan OB" ValidationGroup="PF" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CvLoanOB" runat="server" Display="None" ErrorMessage="Loan OB Should be Numeric"
                                                            ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtLoanOB" Type="Double"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" style="padding-left: 10px">
                                                        <asp:Panel ID="pnlOpeningBalance" runat="server">
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ListView ID="lvOpeningBalance" runat="server">
                                                                            <EmptyDataTemplate>
                                                                                <br />
                                                                                <center>
                                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                                </center>
                                                                            </EmptyDataTemplate>
                                                                            <LayoutTemplate>
                                                                                <div id="demo-grid" class="vista-grid">
                                                                                    <div class="titlebar">
                                                                                        Opening Balance</div>
                                                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr class="header">
                                                                                            <th>
                                                                                                Action
                                                                                            </th>
                                                                                            <th>
                                                                                                Month Year
                                                                                            </th>
                                                                                            <th>
                                                                                                Fin.Year Start Date
                                                                                            </th>
                                                                                            <th>
                                                                                                Fin.Year End Date
                                                                                            </th>
                                                                                            <th>
                                                                                                Opening Balance
                                                                                            </th>
                                                                                            <th>
                                                                                                Loan Bal
                                                                                            </th>
                                                                                        </tr>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                                    <td>
                                                                                        <asp:ImageButton ID="btnEditOB" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                                            AlternateText="Edit Record" OnClick="btnEdit_Click" ToolTip="Edit Record" />&nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("MONYEAR")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("FSDATE","{0:dd/MM/yyyy}")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("FEDATE","{0:dd/MM/yyyy}")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("OB")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("LOANBAL")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <AlternatingItemTemplate>
                                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                                    <td>
                                                                                        <asp:ImageButton ID="btnEditItemMaster" runat="server" ImageUrl="~/images/edit.gif"
                                                                                            CommandArgument='<%# Eval("PFTRXNO") %>' OnClick="btnEdit_Click" AlternateText="Edit Record"
                                                                                            ToolTip="Edit Record" />&nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("MONYEAR")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("FSDATE", "{0:dd/MM/yyyy}")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("FEDATE", "{0:dd/MM/yyyy}")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("OB")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("LOANBAL")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </AlternatingItemTemplate>
                                                                        </asp:ListView>
                                                                        <%--                                                             <div class="vista-grid_datapager">
                                                                    <asp:DataPager ID="dpPagerItemMaster" runat="server" PagedControlID="lvOpeningBalance"
                                                                        PageSize="5" OnPreRender="dpPagerItemMaster_PreRender">
                                                                        <Fields>
                                                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                                                        </Fields>
                                                                    </asp:DataPager>
                                                                </div>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </ajaxToolKit:TabPanel>
                            <ajaxToolKit:TabPanel runat="server" ID="ProcessPF" HeaderText="Process PF">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="updProcessPF" runat="server">
                                        <ContentTemplate>
                                            <br />
                                            <table>
                                                <tr>
                                                    <td class="form_left_label">
                                                        Month Year :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:TextBox ID="txtMonYearProcess" runat="server" Width="80px"></asp:TextBox>
                                                        &nbsp;<asp:Image ID="Image4" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        <ajaxToolKit:CalendarExtender ID="cetxtMonYearProcess" runat="server" Format="MM/yyyy"
                                                            TargetControlID="txtMonYearProcess" PopupButtonID="Image4" Enabled="true" EnableViewState="true"
                                                            PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvtxtMonYearProcess" runat="server" ControlToValidate="txtMonYearProcess"
                                                            Display="None" ErrorMessage="Month Year in (MM/yyyy Format)" ValidationGroup="PF"
                                                            SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" style="padding-left: 10px">
                                                        <asp:Panel ID="pnlProcessPF" runat="server">
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ListView ID="lvProcessPF" runat="server">
                                                                            <EmptyDataTemplate>
                                                                                <br />
                                                                                <center>
                                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                                </center>
                                                                            </EmptyDataTemplate>
                                                                            <LayoutTemplate>
                                                                                <div id="demo-grid" class="vista-grid">
                                                                                    <div class="titlebar">
                                                                                        Process Result</div>
                                                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr class="header">
                                                                                            <%--<th>
                                                                                                Action
                                                                                            </th>--%>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblMonYear" Text="Month Year"></asp:Label>
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblPf" Text="PF"></asp:Label>
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblPfAdd" Text="PF Add"></asp:Label>
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblPfLoan" Text="PF Loan"></asp:Label>
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblstatus" Text="Status"></asp:Label>
                                                                                            </th>
                                                                                        </tr>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                                    <%--<td>
                                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("MISGNO") %>'
                                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditSubGroup_Click" />&nbsp;
                                                                                    </td>--%>
                                                                                    <td>
                                                                                        <%# Eval("Monyear")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("H1")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("H2")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("H3")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("status")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <AlternatingItemTemplate>
                                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                                    <%--<td>
                                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("MISGNO") %>'
                                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditSubGroup_Click" />&nbsp;
                                                                                    </td>--%>
                                                                                    <td>
                                                                                        <%# Eval("Monyear")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("H1")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("H2")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("H3")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("status")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </AlternatingItemTemplate>
                                                                        </asp:ListView>
                                                                        <%--<div class="vista-grid_datapager">
                                                                            <asp:DataPager ID="dpPagerSubGroupMaster" runat="server" PagedControlID="lvProcessPF"
                                                                                PageSize="5" OnPreRender="dpPagerSubGroupMaster_PreRender">
                                                                                <Fields>
                                                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                                                </Fields>
                                                                            </asp:DataPager>
                                                                        </div>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </ajaxToolKit:TabPanel>
                            <%--  <ajaxToolKit:TabPanel runat="server" ID="Panel2" OnClientClick="PanelClick" HeaderText="Item Group Master">--%>
                            <ajaxToolKit:TabPanel runat="server" ID="LoanRepayment" HeaderText="Loan Repayment">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="updLoanRepayment" runat="server">
                                        <ContentTemplate>
                                            <br />
                                            <table>
                                                <tr>
                                                    <td class="form_left_label">
                                                        Month Year :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:TextBox ID="txtMonthYear" runat="server" Width="80px"></asp:TextBox>
                                                        &nbsp;<asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/yyyy"
                                                            TargetControlID="txtMonthYear" PopupButtonID="Image2" Enabled="true" EnableViewState="true"
                                                            PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" ControlToValidate="txtMonthYear"
                                                            Display="None" ErrorMessage="Month Year in (MM/yyyy Format)" ValidationGroup="PF"
                                                            SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label">
                                                        Amount :
                                                    </td>
                                                    <td class="form_left_text" colspan="3">
                                                        <asp:TextBox runat="server" ID="txtamount" Text="0.00" Width="80px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rvftxtamount" runat="server" ControlToValidate="txtamount"
                                                            Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="PF" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvtxtamount" runat="server" Display="None" ErrorMessage="Amount Should be Numeric"
                                                            ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtamount" Type="Double"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" style="padding-left: 10px">
                                                        <asp:Panel ID="pnlLoanRepayment" runat="server">
                                                            <table cellpadding="0" cellspacing="0" style="width: 99%;">
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:ListView ID="lvLoanRepayment" runat="server">
                                                                            <EmptyDataTemplate>
                                                                                <br />
                                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></EmptyDataTemplate>
                                                                            <LayoutTemplate>
                                                                                <div id="demo-grid" class="vista-grid">
                                                                                    <div class="titlebar">
                                                                                        Loan Repayment</div>
                                                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr class="header">
                                                                                            <th>
                                                                                                Action
                                                                                            </th>
                                                                                            <th>
                                                                                                MonYear
                                                                                            </th>
                                                                                            <th>
                                                                                                Amount
                                                                                            </th>
                                                                                        </tr>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                                    <td>
                                                                                        <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("MONYEAR")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("H3")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <AlternatingItemTemplate>
                                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                                    <td>
                                                                                        <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("MONYEAR")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("H3")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </AlternatingItemTemplate>
                                                                        </asp:ListView>
                                                                        <%--<div class="vista-grid_datapager">
                                                                            <asp:DataPager ID="dpPagerGroupMaster" runat="server" PagedControlID="lvLoanRepayment"
                                                                                PageSize="5" OnPreRender="dpPagerGroupMaster_PreRender">
                                                                                <Fields>
                                                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                                                </Fields>
                                                                            </asp:DataPager>
                                                                        </div>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </ajaxToolKit:TabPanel>
                        </ajaxToolKit:TabContainer>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <br />
                        <asp:Button ID="butSubmit" Text="Submit" runat="server" Width="70px" ValidationGroup="PF"
                            OnClick="butSubmit_Click" />
                        <asp:Button ID="Button2" Text="Cancel" runat="server" Width="70px" OnClick="butCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
