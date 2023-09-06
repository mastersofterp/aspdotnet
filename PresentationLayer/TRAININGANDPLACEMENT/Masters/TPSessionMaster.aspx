<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TPSessionMaster.aspx.cs" Inherits="TRAININGANDPLACEMENT_Masters_TPSessionMaster"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%--  Shrink the info panel out of view --%>
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                SESSION MASTER
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Reset the sample so it can be played again --%>
        <%--  Enable the button so it can be played again --%>
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
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record
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

                <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
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
    </table>
    <br />
    <table cellpadding="2" cellspacing="2" width="90%">
        <tr>
            <td style="padding-left: 10px">
                <asp:UpdatePanel ID="updActivity" runat="server">
                    <ContentTemplate>
                        <fieldset class="fieldsetPay" style="width: 90%">
                            <legend class="legendPay">Add / Edit Session </legend>
                            <br />
                            <table width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td width="25%" class="form_left_label">
                                        Session Name :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtSession" runat="server" TabIndex="1" MaxLength="200" Width="50%" />
                                        <asp:RequiredFieldValidator ID="rvalSession" runat="server" ControlToValidate="txtSession"
                                            Display="None" ErrorMessage="Please enter session name." SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                    </td>
                                </tr>
                                <%--<tr>
                                <td class="form_left_label">
                                    Session Code :
                                </td>
                                <td class="form_left_text">
                                <asp:TextBox ID ="txtSessioncode" runat="server" MaxLength="5"  Width="60px" TabIndex="2"/>
                                <asp:RequiredFieldValidator ID="rfvSessioncode" runat="server" ControlToValidate="txtSessioncode"
                                display="None" ErrorMessage="Please Enter Sesion Code" SetFocusOnError="false" 
                                ValidationGroup="Submit" />
                                </td>
                                </tr>--%>
                                <tr>
                                    <td class="form_left_label">
                                        Start Date :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtStartDate" Width="80px" runat="server" TabIndex="3" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd-MM-yyyy" PopupButtonID="imgCalStartDate" TargetControlID="txtStartDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeReceivedDT" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" TargetControlID="txtStartDate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevReceivedDT" runat="server" ControlExtender="meeReceivedDT"
                                            ControlToValidate="txtStartDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Date" ValidationGroup="Submit">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                        </ajaxToolKit:MaskedEditValidator>
                                        <asp:Image ID="imgCalStartDate" runat="server" src="../../images/calendar.png" Style="cursor: hand" />
                                        <asp:RequiredFieldValidator ID="rvalStartDate" runat="server" ControlToValidate="txtStartDate"
                                            Display="None" ErrorMessage="Please enter Start Date." SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                        <asp:CompareValidator ID="valStartDateType" runat="server" ControlToValidate="txtStartDate"
                                            CultureInvariantValues="true" Display="None" ErrorMessage="Please enter a valid date."
                                            Operator="DataTypeCheck" SetFocusOnError="true" Type="Date" ValidationGroup="Submit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">
                                        End Date :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtEndDate" Width="80px" runat="server" TabIndex="4" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd-MM-yyyy" PopupButtonID="imgCalEndDate" TargetControlID="txtEndDate">
                                        </ajaxToolKit:CalendarExtender>
                                         <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" TargetControlID="txtEndDate" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                            ControlToValidate="txtEndDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Date" ValidationGroup="Submit">
                                        </ajaxToolKit:MaskedEditValidator>
                                        <asp:Image ID="imgCalEndDate" runat="server" src="../../images/calendar.png" Style="cursor: hand" />
                                        <asp:RequiredFieldValidator ID="rvalEndDate" runat="server" ControlToValidate="txtEndDate"
                                            Display="None" ErrorMessage="Please enter End Date." SetFocusOnError="true" ValidationGroup="Submit" />
                                        <asp:CompareValidator ID="valEndDateType" runat="server" ControlToValidate="txtEndDate"
                                            CultureInvariantValues="true" Display="None" ErrorMessage="Please enter a valid date."
                                            Operator="DataTypeCheck" SetFocusOnError="true" Type="Date" ValidationGroup="Submit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="padding-top: 10px">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                            OnClick="btnSubmit_Click" TabIndex="5" Width="80px" />
                                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6" Width="80px"
                                            OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <div style="width: 90%;">
                            <table cellpadding="1" cellspacing="1" width="100%">
                                <tr>
                                    <td>
                                        <asp:ListView ID="lvSession" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <div class="titlebar">
                                                        T&P-Session
                                                    </div>
                                                    <table class="datatable" cellpadding="0" cellspacing="0">
                                                        <tr class="header">
                                                           
                                                            <th>
                                                                Session Name
                                                            </th>
                                                            <th>
                                                                Start Date
                                                            </th>
                                                            <th>
                                                                End Date
                                                            </th>
                                                             <th>
                                                                Edit
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    
                                                    <td>
                                                        <%# Eval("SESSIONNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# (Eval("FROMDATE").ToString() != string.Empty) ? (Eval("FROMDATE", "{0:dd-MMM-yyyy}")) : Eval("FROMDATE", "{0:dd-MMM-yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# (Eval("TODATE").ToString() != string.Empty) ? (Eval("TODATE", "{0:dd-MMM-yyyy}")) : Eval("TODATE", "{0:dd-MMM-yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("SESSIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEdit_Click" TabIndex="10" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("SESSIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEdit_Click" TabIndex="10" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SESSIONNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# (Eval("FROMDATE").ToString() != string.Empty) ? (Eval("FROMDATE", "{0:dd-MMM-yyyy}")) : Eval("FROMDATE", "{0:dd-MMM-yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# (Eval("TODATE").ToString() != string.Empty) ? (Eval("TODATE", "{0:dd-MMM-yyyy}")) : Eval("TODATE", "{0:dd-MMM-yyyy}")%>
                                                    </td>
                                                     <td>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("SESSIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEdit_Click" TabIndex="10" />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
