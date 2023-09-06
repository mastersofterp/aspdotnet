<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelSeatQuota.aspx.cs" Inherits="HOSTEL_MASTERS_HostelSeatQuota" Title="" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                HOSTEL SEAT QUOTA MANAGEMENT&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />

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
        <%--  Reset the sample so it can be played again --%>
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

            </td>
        </tr>
        <tr>
            <td>
                <fieldset class="fieldset">
                    <legend class="legend">Add/Edit Hostel Seat Quota</legend>
                    <table cellpadding="2" cellspacing="2" border="0" width="60%">
                    <tr>
                            <td >
                                &nbsp;&nbsp;Admission Batch:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAdmbatch" runat="server" Width="200px" TabIndex="1" 
                                    AppendDataBoundItems="true" AutoPostBack="True" 
                                    >
                                    
                                    
                                    </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvadmbatch" runat="server" ControlToValidate="ddlAdmbatch"
                                    Display="None" ErrorMessage="Please select Admission Batch" ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td></td>
                               
                                    
                                    <td>
                                    <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="submit" onclick="btnShow_Click"
                                    />&nbsp;&nbsp;&nbsp;
                                     <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" Height="16px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr >
            <td>
                <br />
               <asp:ListView ID="lvQuota" runat="server" Visible="true" >
                        <LayoutTemplate>
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                   Hostel Seat Quota List</div>
                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="header">
                                        
                                        <th style="width: 20%">
                                            Seat_Quota(%)
                                        </th>
                                        <th style="width: 8%">
                                            General(%)
                                        </th>
                                        <th style="width: 8%">
                                            OBC(%)
                                        </th>
                                        <th style="width: 8%">
                                           SC(%)
                                        </th>
                                        <th style="width: 8%">
                                            ST(%)
                                        </th>
                                        <th style="width: 8%">
                                            NT(%)
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                
                                <td style="width: 20%; text-transform: uppercase" align="right">
                                    <asp:Label ID="lblSeatQuotaAllIndia" runat="server" Text="All India" ToolTip="1"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSeatQuota" runat="server" Width="20%" tooltip='<%# Eval("BATCHNO") %>'
                                        Text=""></asp:TextBox><br />
                                      
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtSeatQuota" ValidChars="1234567890." ID="ftrtxtSeatQuota"></ajaxToolKit:FilteredTextBoxExtender>
                                        
                                        <br />
                                    <br />
                                    <asp:Label ID="lblSeatQuotaStateLevel" runat="server" Text="State Level" ToolTip="2"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtSeatQuota1" runat="server" Width="20%" Text=""></asp:TextBox></td>
                                <td style="width: 8%">
                                
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtSeatQuota1" ValidChars="1234567890." ID="ftrtxtSeatQuota1"></ajaxToolKit:FilteredTextBoxExtender>
                                
                                    <asp:TextBox ID="txtGeneral" runat="server" Width="45%" ToolTip="4"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtGeneral" ValidChars="1234567890." ID="ftrtxtGeneral"></ajaxToolKit:FilteredTextBoxExtender>
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtGeneral1" runat="server" Width="45%" ToolTip="4"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtGeneral1" ValidChars="1234567890." ID="ftrtxtGeneral1"></ajaxToolKit:FilteredTextBoxExtender>
                                </td>
                                <td style="width: 8%">
                                    <asp:TextBox ID="txtObc" runat="server" Width="45%" ToolTip="3"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtObc" ValidChars="1234567890." ID="ftrtxtObc"></ajaxToolKit:FilteredTextBoxExtender>
                                    
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtObc1" runat="server" Width="45%" ToolTip="3"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtObc1" ValidChars="1234567890." ID="ftrtxtObc1"></ajaxToolKit:FilteredTextBoxExtender>
                                    
                                </td>
                                <td style="width: 8%">
                                    <asp:TextBox ID="txtSc" runat="server" Width="45%" ToolTip="2"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtSc" ValidChars="1234567890." ID="ftrtxtSc"></ajaxToolKit:FilteredTextBoxExtender>
                                    
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtSc1" runat="server" Width="45%" ToolTip="2"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtSc1" ValidChars="1234567890." ID="ftrtxtSc1"></ajaxToolKit:FilteredTextBoxExtender>
                                    
                                </td>
                                <td style="width: 8%">
                                    <asp:TextBox ID="txtSt" runat="server" Width="45%" ToolTip="1"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtSt" ValidChars="1234567890." ID="ftrtxtSt"></ajaxToolKit:FilteredTextBoxExtender>
                                    
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtSt1" runat="server" Width="45%" ToolTip="1"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtSt1" ValidChars="1234567890." ID="ftrtxtSt1"></ajaxToolKit:FilteredTextBoxExtender>
                                    
                                </td>
                                <td style="width: 8%">
                                    <asp:TextBox ID="txtNt" runat="server" Width="45%" ToolTip="5"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtNt" ValidChars="1234567890." ID="ftrtxtNt"></ajaxToolKit:FilteredTextBoxExtender>
                                    
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtNt1" runat="server" Width="45%" ToolTip="5"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" TargetControlID="txtNt1" ValidChars="1234567890." ID="ftrtxtNt1"></ajaxToolKit:FilteredTextBoxExtender>
                                    
                                </td>
                            </tr>
                           
                        </ItemTemplate>
                    </asp:ListView>
                
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

