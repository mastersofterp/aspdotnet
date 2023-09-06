<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IITMS_FRESHERS_TEST.aspx.cs" Inherits="Itle_IITMS_FRESHERS_TEST" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 70%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                ITLE SESSION STARTED
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%-- Flash the text/border red and fade in the "close" button --%>
        <%--  Shrink the info panel out of view --%>
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit1.gif" AlternateText="Edit Record" />
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

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
            <td style="padding-left: 15px;">
                <%--<div id="demo-grid" class="vista-grid">
                    <div class="titlebar">
                        ITLE WORKAREA</div>
                </div>--%>
                <fieldset class ="fieldset" >
                
                
                
             
                <table cellpadding="2" cellspacing="2" width="100%" class ="grid_bg">
                     <tr>
                       
                        <td style="width:100%; text-align :center " colspan ="5" >
                            <asp:Label ID="Label1" runat="server" Text="WELCOME TO IITMS TECHNICAL TEST" Font-Bold="True"
                                Font-Size="24pt" ForeColor="#6666FF"></asp:Label>
                        </td>
                    </tr>
                    
                        <tr>
                       
                        <td style="width:100%; text-align :center " colspan ="5" >
                            <asp:Label ID="Label2" runat="server" Text="------------------------------------------------" Font-Bold="True"
                                Font-Size="24pt" ForeColor="#6666FF"></asp:Label>
                                <br />
                               <asp:Label ID="lblLine" runat="server" Font-Bold="true" ForeColor="Red"> <b>PLEASE FILL BELOW INFORMATION</b></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                    <td style="width:25%" >&nbsp;</td>
                         <td style="width:35%">
                             &nbsp;</td>
                        <td style="width:5%" >&nbsp;</td>
                        <td style="width:40%; padding-left:300px;" align="left" colspan="2">
                        
                       <%-- <asp:Image ID="imgPhoto"  runat="server"  
                                style="height:100px;width:100px;border-width:0px;" Height="30px" Width="50px" />
                        </td>--%>
                         
                    </tr>
                    
                    <tr>
                    <td style="width:25%" ></td>
                         <td style="width:40%">
                            <b>Enter Full Name </b>
                        </td>
                        <td style="width:5%" ><b>:</b></td>
                        <td style="width:40%" colspan="2">
                        
                        <asp:TextBox ID="txtUserName" runat="server" Font-Bold="True" Width="250px"></asp:TextBox>
                            
                        </td>
                        <%--<asp:RequiredFieldValidator ID="rfUserName" runat="server" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                         <asp:ValidationSummary ID="vldSummary" runat="server" ShowMessageBox="true"
                         ShowSummary="true" />--%>
                    </tr>
                    
                     <tr>
                    <td style="width:25%" ></td>
                         <td style="width:40%">
                            <b>Highest Qualification </b>
                        </td>
                        <td style="width:5%" ><b>:</b></td>
                        <td style="width:40%" colspan="2">
                        
                        <asp:TextBox ID="txtQualification" runat="server" Font-Bold="True" Width="250px"></asp:TextBox>
                            
                        </td>
                         
                    </tr>
                    <tr>
                        <td style="width:25%;">
                        </td>
                      
                        <td style="width:40%;">
                           <b> Mobile No. </b>
                        </td>
                        <td style="width:5%;">
                         <b>:</b>
                        </td>
                         <td style="width:40%" colspan="2">
                         
                          <asp:TextBox ID="txtMobileNo" runat="server" Font-Bold="True" Width="250px"></asp:TextBox>
                            
                        </td>
                        
                    </tr>
                    
                    <tr>
                       <td style="width:25%" ></td>
                        <td style="width:40%" >
                           <b> Email </b>
                        </td>
                         <td style="width:5%;" ><b>:</b></td>
                        <td style="width:40%" colspan="2">
                         <asp:TextBox ID="txtEmail" runat="server" Font-Bold="True" Width="250px"></asp:TextBox>
                            
                        </td>
                        
                        
                    </tr>
                    <tr>
                     <td style="width:25%;" ></td>
                     <td style="width:40%" >
                           <b>Address </b>
                     </td>
                     <td style="width:5%;">
                     <b>:
                     </b>
                    </td>
                     <td style="width:40%;" colspan="2">
                           <asp:TextBox ID="txtAddress" runat="server" Font-Bold="True" TextMode="MultiLine" Width="250px" Height="100px"></asp:TextBox>
                        </td>
                        
                    </tr>
                    
                    
                    <tr>
                     <td style="width:25%;" ></td>
                     <td style="width:40%" >
                           <b>  </b>
                     </td>
                     <td style="width:5%;">
                     
                    </td>
                     <td style="width:45%;">
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                             onclick="btnSubmit_Click"/>
                        </td>
                        
                     <td style="width:45%;" align="right">
                         &nbsp;</td>
                        
                    </tr>
                    
                    
                </table>
                   </fieldset>
            </td>
        </tr>
    </table>
    
  
</asp:Content>

