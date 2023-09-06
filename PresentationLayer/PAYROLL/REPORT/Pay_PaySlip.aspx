<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_PaySlip.aspx.cs" Inherits="PayRoll_Pay_PaySlip" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">
                            EMPLOYEE PAYSLIP&nbsp;&nbsp
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC;
                    background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right;">
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

                <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp"
                    Enabled="True">
                    <Animations>
                        <OnClick>
                            <Sequence>
                                <%-- Disable the button so it can't be clicked again --%>
                                <EnableAction Enabled="false" />
                                
                                <%-- Position the wire frame on top of the button and show it --%>
                                
                                
                                
                                <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                
                                <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                                
                                
                                
                                <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="-50" />
                                    <Resize Width="260" Height="280" />
                                    <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                                </Parallel>
                                
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
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose"
                    Enabled="True">
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
            <td align="center">
                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
               <div id="divPaySlip" style="padding-left:15px;width:90%" runat="server">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Employee PaySlip Information</legend>
                    <br />
                         <table width="100%" cellpadding="2" cellspacing="2" border="0">
                            <tr>
                                <td class="form_left_label">
                                    Month / Year:</td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlMonthYear" runat="server" Width="200px" 
                                        AppendDataBoundItems="True" TabIndex="1">
                                    </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    Staff:</td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlStaffNo" runat="server" Width="300px" 
                                        AppendDataBoundItems="True" TabIndex="2" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Staff"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">                                    
                                </td>
                                <td class="form_left_text">
                                     <asp:RadioButton ID="rdoSelectEmployee" runat="server" Checked="true" 
                                        Text="Select Employee" GroupName="A" 
                                        TabIndex="3" onclick="DisableDropDownList(false);" />
                                    <asp:RadioButton ID="rdoAllEmployee" runat="server" Text="All Employee" GroupName="A" 
                                        onclick="DisableDropDownList(true);" TabIndex="4" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">                                    
                                    Employee:
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True" 
                                        TabIndex="5" Width="300px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    Salary Certificate Issued to :
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtBankName" runat="server" Width="30%" TabIndex="6"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                            <td class="form_left_label">
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                
                            </td>
                                <td class="form_left_label">
                                <br />
                                    <asp:Button ID="btnShow" runat="server" Text="Show" 
                                        onclick="btnShow_Click" Width="79px" TabIndex="7" 
                                        ValidationGroup="Payroll" />
                                    &nbsp; 
                                    <asp:Button ID="btnSalaryCertificate" runat="server" Text="Salary Certificate" 
                                        onclick="btnSalaryCertificate_Click" TabIndex="8" 
                                        ValidationGroup="Payroll" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="79px" 
                                        onclick="btnCancel_Click" TabIndex="9" />
                                </td>
                            </tr>
                        </table>
                        <br />
                </fieldset>
                </div>
            </td>
        </tr>
     </table>
     <div id="divMsg" runat="server"></div>
       <script type="text/javascript">
       function DisableDropDownList(disable)
        {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex=0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
        }
     </script>
</asp:Content>

