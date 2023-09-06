<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_ServiceBook_Report.aspx.cs" Inherits="PayRoll_Pay_ServiceBook_Report"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                ServiceBook Report&nbsp;
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
                            function Cover(bottom, top, ignoreSize)
                            {
                                var location = Sys.UI.DomElement.getLocation(bottom);
                                top.style.position = 'absolute';
                                top.style.top = location.y + 'px';
                                top.style.left = location.x + 'px';
                                if (!ignoreSize)
                                {
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
                            &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlSelection" runat="server" Style="text-align: left; width: 90%;
                    padding-left: 20px;">
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Select Department and Employee</legend>
                        <br />
                        <asp:UpdatePanel ID="upEmployee" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            Department:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlDept" AppendDataBoundItems="true" runat="server" Width="300px"
                                                OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                Display="None" ErrorMessage="Please select Department" ValidationGroup="ServiceBookReport"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            Employee Name :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" Width="300px"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                              <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" ErrorMessage="Please select  Employee Name " ValidationGroup="ServiceBookReport"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td class="form_left_label">
                                            IdNo :
                                        </td>
                                        <td class="form_left_text" colspan="4">
                                            <asp:TextBox ID="txtIdno" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            &nbsp
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Style="text-align: left; width: 90%; padding-left: 20px;">
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Select Criteria</legend>
                        <br />
                        <%--<asp:UpdatePanel ID="SelectCriteria" runat="server">
                            <ContentTemplate>--%>
                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr id="trDept" runat="server" visible="false">
                                <td class="form_left_label" colspan="4" align="center">
                                    Department :<asp:Label ID="lblDepartment" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkPersonalMemoranda" runat="server" Text="Personal Memoranda"
                                        Checked="true" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkFamilyParticulars" runat="server" Text="Family Information" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkEducationalQualification" runat="server" Text="Qualification Information" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkMatter" runat="server" Text="Matter" />
                                </td>
                                <%--  <td class="form_left_label">
                                            <asp:CheckBox ID="chkDetailsOfServiceBook" runat="server" Text="Details Of ServiceBook" />
                                        </td>--%>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkDeptExamAndOtherDetails" runat="server" Text="Departmental Exam Information" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkPreviousQualifyingService" runat="server" Text="Previous Service Information" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkForeginService" runat="server" Text="Foreign Service Information" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="ChkDetailsOfLoansAndAdvances" runat="server" Text="Loans And Advances Information" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="ChkNomination" runat="server" Text="Employee Nomination Information" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkLeaveRecords" runat="server" Text="Leave Records" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkTraning" runat="server" Text="Training Information" />
                                </td>
                                <%-- <td class="form_left_label">
                                            <asp:CheckBox ID="chkGoodServiceAdvIncrcPunishMent" runat="server" Text="Good Service,Adv.Incrc.,PunishMent etc" />
                                        </td>--%>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkPayRevisionOrPromotion" runat="server" Text="Pay Revision Information" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" colspan="4">
                                    <asp:CheckBox ID="chkIncetmentTermination" runat="server" Text="Other Details Entries Like Increment termination etc" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report" ValidationGroup="ServiceBookReport"
                                        OnClick="btnShowReport_Click" Width="100px" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" Width="85px" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBookReport"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                        </table>
                        <div id="divMsg" runat="server">
                        </div>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
    </table>
</asp:Content>
