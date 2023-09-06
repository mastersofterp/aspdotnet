<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Payroll_LIC_Report.aspx.cs" Inherits="PayRoll_Payroll_LIC_Report" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
           <%-- Flash the text/border red and fade in the "close" button --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                EMPLOYEE LIC&nbsp; REPORT&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
           <%--<asp:CheckBox id="chkPartiularColumn" runat="server" Text="Particular Column" 
                     TabIndex="9" onclick="DisableListboxOnParticularColumn(true);" />--%>
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
            <legend class="legendPay">LIC Report</legend>
                <table width="100%" cellpadding="2" cellspacing="2" border="0">
                    <tr>
                        <td width="20%">
                            select Month:
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="true" 
                           width="170px" TabIndex="1"></asp:DropDownList>
                           <asp:RequiredFieldValidator ID="rfvMonth" runat="server" Display="None" ControlToValidate="ddlMonth"
                           ErrorMessage="Please Select Month." SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <asp:RadioButton ID="rdoSingleEMployee" runat="server" Text="Single Employee" 
                                Checked="true" GroupName="P" onclick="EnabledEmpDropdown()" TabIndex="2"/>
                            
                        </td>
                        <td>
                            <asp:RadioButton ID="rdoAllEMployee" runat="server" Text="All Employee" 
                                GroupName="P" onclick="DisabledEmpDropdown();" TabIndex="3" />
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            
                            Employee:</td>
                        <td>
                            <asp:DropDownList ID="ddlEmployeeNo" runat="server" 
                                width="170px" AppendDataBoundItems="True" TabIndex="4" ></asp:DropDownList>
                                
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            
                            Staff Wise:
                                                    
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStaffNo" runat="server" Width="170px" 
                                AppendDataBoundItems="True" TabIndex="5">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btnLICReport" runat="server" Text="LIC" 
                                 Width="70px" TabIndex="6" onclick="btnLICReport_Click" />
                                &nbsp;
                                <asp:Button ID="btnRdReport" runat="server" Text="RD" TabIndex="7" 
                                onclick="btnRdReport_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" 
                                onclick="btnCancel_Click" />
                            </td>
                    </tr>
                    <tr>
                        <td width="20%">
                        </td>
                        <td>
                            &nbsp;
                            </td>
                    </tr>
                </table>
         </fieldset>
<script type="text/javascript">
    function DisabledEmpDropdown()
    {
        if(document.getElementById('ctl00_ContentPlaceHolder1_rdoAllEMployee').checked)
            {
                document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled=true;
                document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex=0;
                document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled=false;
            }
    }
    function EnabledEmpDropdown()
    {
        document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled=false;
        document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled=true;
        document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').selectedIndex=0;
    }
</script>
<div id="divMsg" runat="server"></div>
</asp:Content>

