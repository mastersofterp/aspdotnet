<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_AnnualReport.aspx.cs" Inherits="PayRoll_Pay_AnnualReport" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
           <%-- Flash the text/border red and fade in the "close" button --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                ANNUAL SALARY SUMMARY REPORT&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
           <%-- <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll" 
                    DisplayMode="List" ShowMessageBox="false" ShowSummary="false" />--%>
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
            </td>
        </tr>
    </table>
         <br />
    <fieldset class="fieldsetPay">
        <legend class="legendPay">Annual Summary Rport</legend>
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td class="data_label" width="25%">
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" Width="150px" />
                    &nbsp;<asp:Image ID="imgCalFromDate" runat="server" 
                        Style="cursor: hand" ImageUrl="~/IMAGES/calendar.png" />
                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                    </ajaxToolKit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                        Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                        ValidationGroup="Payroll" />
                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                </td>
            </tr>
            <tr>
                <td class="data_label" width="25%">
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Width="150px" 
                        ontextchanged="txtToDate_TextChanged" />
                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                    <asp:Image ID="imgCalToDate" runat="server" 
                        Style="cursor: hand" ImageUrl="~/IMAGES/calendar.png" />
                    <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                        Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                        ValidationGroup="Payroll" />
                </td>
            </tr>
            <tr>
                <td  width="25%">
                    <asp:RadioButton ID="rdoParticularEmployee" runat="server" checked="true"
                        Text="Particular Employee" GroupName="Employee" onclick="DisableDropDownListParticularEmployee(true);"
                        TabIndex="4" />
                </td>
                <td>
                    <asp:RadioButton ID="rdoAllEmployee" runat="server" Text="All Employee" GroupName="Employee"
                    TabIndex="3" onclick="DisableDropDownListAllEmployee(true);" />
                </td>
            </tr>
            <tr>
                <td width="25%">
                    Staff No.:
                </td>
                <td>
                    <asp:DropDownList ID="ddlStaffNo" runat="server" width="150px" 
                        AppendDataBoundItems="true" TabIndex="5" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    Employee:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True" 
                        Width="150px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="25%">
                   
                      <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                       DisplayMode="List" ShowMessageBox="true" ShowSummary="false" 
                        Width="123px" />
                   
                </td>
                <td>
                    &nbsp;<asp:Button ID="btnShowReport" runat="server" Text="Show Report" 
                        onclick="btnShowReport_Click" ValidationGroup="Payroll" TabIndex="6" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                        onclick="btnCancel_Click" TabIndex="7" />
                </td>
            </tr>
        </table>
        
    </fieldset>
    <script type="text/javascript">
       function DisableDropDownListAllEmployee(disable)
        {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex=0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled=disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled=false;
            
        }
       function DisableDropDownListParticularEmployee(disable)
       {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').selectedIndex=0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled=disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled=false;
       }
        
     </script>
     <div id="divMsg" runat="server"></div>
</asp:Content>

