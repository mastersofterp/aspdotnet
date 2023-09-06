<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MessBillCalculationOB.aspx.cs" Inherits="HOSTEL_MessBillCalculationOB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2" border="0">
                <%-- PAGE HEADING --%>
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">
                        Mess Bill Calculation OB&nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                <%-- PAGE HELP --%>
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
                    <td>
                        <div>
                            <fieldset class="fieldset">
                                <legend class="legend">Mess Bill</legend>
                                <table width="100%" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td style="width: 150px">
                                        </td>
                                        <td style="width: 100px">
                                            Hostel Session :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ToolTip="Please select Session"
                                                Width="250px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                                                ControlToValidate="ddlSession" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                        <td rowspan="10" valign="top">
                                            <fieldset class="fieldset">
                                                <legend class="legend">Expenditure Details</legend>
                                                <table width="100%" cellpadding="1" cellspacing="1">
                                                    <tr>
                                                        <td>
                                                            <asp:ListView ID="lvExpend" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="Div3" class="vista-grid">
                                                                        <table cellpadding="1" cellspacing="1" runat="server" id="Table1" class="datatable"
                                                                            width="99%">
                                                                            <tr class="header">
                                                                                <th style="width: 50%; text-align: left; font-family: Verdana; font-size: 11px">
                                                                                    Heads
                                                                                </th>
                                                                                <th style="width: 25%; text-align: right; font-family: Verdana; font-size: 11px">
                                                                                    Amount
                                                                                </th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                        <td style="width: 2%; text-align: left; display: none;">
                                                                            <asp:Label ID="lblshead" runat="server" Text='<%# Eval("FEE_HEAD") %>' />
                                                                        </td>
                                                                        <td style="width: 3%; text-align: left">
                                                                            <asp:Label ID="lblhead" runat="server" Text='<%# Eval("FEE_LONGNAME") %>' />
                                                                        </td>
                                                                        <td style="width: 5%; text-align: right">
                                                                            <asp:Label ID="lblsheadA" runat="server" Style="text-align: right" Font-Bold="true" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                        </td>
                                        <td style="width: 100px">
                                            Hostel :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlHostel" runat="server" AppendDataBoundItems="True" ToolTip="Please select Hostel"
                                                Width="250px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ErrorMessage="Please Select Hostel"
                                                ControlToValidate="ddlHostel" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                        </td>
                                        <td style="width: 100px">
                                            Mess Type :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMess" runat="server" AppendDataBoundItems="True" ToolTip="Please select Mess"
                                                Width="250px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMess" runat="server" ErrorMessage="Please Select Mess Type"
                                                ControlToValidate="ddlMess" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                        </td>
                                        <td style="width: 100px">
                                            Month :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="True" ToolTip="Please select Month"
                                                Width="250px" Enabled="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMonth" runat="server" ErrorMessage="Please Select Month"
                                                ControlToValidate="ddlMonth" InitialValue="-1" SetFocusOnError="True" ValidationGroup="Show"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                        </td>
                                        <td style="width: 100px">
                                            Bill From :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" Width="90px" />
                                            <asp:Image ID="imgFDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgFDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFrom" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                OnInvalidCssClass="errordate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeFrom"
                                                ControlToValidate="txtFromDate" IsValidEmpty="False" EmptyValueMessage="Please Select From Date"
                                                InvalidValueMessage="Date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                Display="Dynamic" ValidationGroup="Show" />
                                            To
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="3" Width="90px" />
                                            <asp:Image ID="imgTDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgTDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeTo" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                OnInvalidCssClass="errordate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="meeTo"
                                                ControlToValidate="txtToDate" IsValidEmpty="False" EmptyValueMessage="Please Select To Date"
                                                InvalidValueMessage="Date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                Display="Dynamic" ValidationGroup="Show" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                        </td>
                                        <td style="width: 100px">
                                            Process Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBillDate" runat="server" TabIndex="3" />
                                            <asp:Image ID="imgCalDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtBillDate" PopupButtonID="imgCalDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeDate" runat="server" TargetControlID="txtBillDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                OnInvalidCssClass="errordate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeDate"
                                                ControlToValidate="txtBillDate" IsValidEmpty="False" EmptyValueMessage="Please Select Process Date"
                                                InvalidValueMessage="Date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                Display="Dynamic" ValidationGroup="Show" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                        </td>
                                        <td>
                                            Per Head :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPerDay" runat="server" Width="50px" Enabled="false"></asp:TextBox>&nbsp;/&nbsp;Day
                                            <asp:HiddenField ID="hdfDay" runat="server" />
                                            <asp:CheckBox ID="chkInt" runat="server" Text="Decimal" Checked="True" />
                                            &nbsp;&nbsp; Total Student :
                                            <asp:TextBox ID="txtTotalStud" runat="server" Width="50px" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdfTotalStud" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                        </td>
                                        <td style="width: 100px">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="Show" />
                                            <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click"
                                                ValidationGroup="Show" visible="false"/>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                ValidationGroup="Show" Enabled="false"/>
                                            <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
                                                ValidationGroup="Show" visible="false"/>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListView ID="lvStudents" runat="server" Visible="False" >
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">
                                        Student List</div>
                                    <table id="tblFeeEntryGrid" cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                        <tr class="header">
                                            <th>
                                                Reg. No.
                                            </th>
                                            <th>
                                                Name
                                            </th>
                                            <th>
                                                Degree
                                            </th>
                                            <th>
                                                Branch
                                            </th>
                                            <th >
                                                
                                                <asp:Button ID="btnDays" runat="server" Text="Clear" OnClick="btnDays_Click" Visible="false" />
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF1" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF2" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF3" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF4" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF5" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF6" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF7" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF8" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF9" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="lblF10" runat="server" Visible="false"></asp:Label>
                                            </th>
                                            <th>
                                                Balance Amount
                                            </th>
                                            <th>
                                                Monthly Exp.
                                            </th>
                                            <th>
                                                Balance
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                    <td>
                                        <asp:Label ID="lblregno" runat="server" Text=' <%# Eval("REGNO")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text=' <%# Eval("NAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%# Eval("DEGREE")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCHS")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDays" runat="server" Text='<%# Eval("DAYNO")%>' visible ="false" onkeyup="UpdateTotalAmounts(this)"
                                            Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF1" runat="server" Text='<%# Eval("F1")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false" MaxLength="5"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftv" TargetControlID="txtF1" runat="server"
                                            ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF2" runat="server" Text='<%# Eval("F2")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtF2"
                                            runat="server" ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF3" runat="server" Text='<%# Eval("F3")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtF3"
                                            runat="server" ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF4" runat="server" Text='<%# Eval("F4")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtF4"
                                            runat="server" ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF5" runat="server" Text='<%# Eval("F5")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtF5"
                                            runat="server" ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF6" runat="server" Text='<%# Eval("F6")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtF6"
                                            runat="server" ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF7" runat="server" Text='<%# Eval("F7")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtF8"
                                            runat="server" ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF8" runat="server" Text='<%# Eval("F8")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtF8"
                                            runat="server" ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF9" runat="server" Text='<%# Eval("F9")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="txtF9"
                                            runat="server" ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtF10" runat="server" Text='<%# Eval("F10")%>' onblur="AddTotalAmounts(this)"
                                            Width="50px" Visible="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="txtF10"
                                            runat="server" ValidChars="0987654321.">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotPaid" runat="server" Text='<%# Eval("TOTAL_AMT")%>' Width="80px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpenditure" runat="server" Text='<%# Eval("TOTAL_EXPENDITURE")%>'
                                            Width="80px" ></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftvtxt" runat="server" TargetControlID="txtExpenditure" ValidChars="1234567890." FilterType="Custom"></ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBalance" runat="server" Text='<%# Eval("BALANCE_AMT")%>' Width="80px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
            </table>
            <div id="divMsg" runat="server">
            </div>

            <script type="text/javascript" language="javascript">
    function UpdateTotalAmounts(crl)
    {       
    
        if(isNaN(crl.value))
        {
            crl.value='';       
            alert('Only Numeric Characters Allowed!');
            crl.focus();
            return;            
        } 
        
       
         
        var days=document.getElementById('ctl00_ContentPlaceHolder1_hdfDay').value;
        if(Number(days)< Number(crl.value))
        {
             crl.value='';
             alert('Number of days must be less than '+days+' days');
             crl.focus();
             return; 
        }
       // document.getElementById('ctl00_ContentPlaceHolder1_btnSubmit').disabled=true;
         var st = crl.id.split("ctrl");
         var a=st[1].split("_txtDays");
         var index=a[0];   
         var perDay=document.getElementById('ctl00_ContentPlaceHolder1_txtPerDay').value;
         var totExp=Math.round(Number(perDay).toFixed(2)* Number(crl.value).toFixed(2));
         document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl'+index+'_txtExpenditure').value=totExp.toFixed(2);
         var paid =document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl'+index+'_txtTotPaid').value;
         var expend=Math.round(Number(paid).toFixed(2)-Number(totExp).toFixed(2));
         document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl'+index+'_txtBalance').value=expend.toFixed(2);
    }
    
    function AddTotalAmounts(crl)
    {
        document.getElementById('ctl00_ContentPlaceHolder1_btnSubmit').disabled=true;
         var st = crl.id.split("ctrl");
         var a=st[1].split("_txtF1");
         var index=a[0];
         var perDay=document.getElementById('ctl00_ContentPlaceHolder1_txtPerDay').value;
         var days= document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl'+index+'_txtDays').value;
         var totExp=Math.round(Number(perDay).toFixed(2)* Number(days).toFixed(2));
         document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl'+index+'_txtExpenditure').value=totExp.toFixed(2);
         
         var paid =document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl'+index+'_txtExpenditure').value;
         var tot=Number(crl.value)+Number(paid);
         document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl'+index+'_txtExpenditure').value=tot.toFixed(2);
         
         var bal=document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl'+index+'_txtTotPaid').value;
         var expend=Math.round(Number(bal).toFixed(2)-Number(tot).toFixed(2));
         document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl'+index+'_txtBalance').value=expend.toFixed(2);
         
    }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
