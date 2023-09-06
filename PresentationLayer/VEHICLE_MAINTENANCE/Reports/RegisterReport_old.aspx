
<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RegisterReport_old.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Report_RegisterReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                REPAIR / SERVICING REGISTER &nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%-- Flash the text/border red and fade in the "close" button --%>        <%--  Shrink the info panel out of view --%>
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
                            <%--  Reset the sample so it can be played again --%>                            <%--  Enable the button so it can be played again --%>
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
            <td><br />
                <asp:Panel ID="pnlCourtName_Top" runat="server">
                    <%--Row no 1--%>
                    <div style="padding-left: 10px; width: 70%">
                        <fieldset class="fieldsetPay">
                            <legend class="legendPay">Search Criteria</legend>
                            <br />
                            <table border="0" cellpadding="0" cellspacing="0" width="70%">
                                <tr>
                                    <td class="form_left_label ">
                                        <asp:CheckBox id="chkvehical" runat="server" Text="Vehicle" Checked="true" 
                                            oncheckedchanged="chkvehical_CheckedChanged" AutoPostBack="true" 
                                            Visible="false" />
                                        </td>
                                    <td>:</td>
                                    
                                
                                    <td class="form_left_label ">
                                        <asp:DropDownList ID="ddlvehical" AppendDataBoundItems="true" runat="server" Width="200px" Enabled="true" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                             </tr> 
                                
                                <tr>
                                    <td class="form_left_label ">
                                        <asp:CheckBox id="chkworkshop" runat="server" Text="Workshop" Checked="true" 
                                            oncheckedchanged="chkworkshop_CheckedChanged" AutoPostBack="true"  Visible="false"/>
                                    </td>
                                    <td>:</td>
                                    <td class="form_left_label ">  
                                        
                                        <asp:DropDownList ID="ddlworkshop" AppendDataBoundItems="true" runat="server" Width="200px" Enabled="false" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                    
                                    </td>
                                </tr>
                               
                                
                                <tr>
                                    <td class="form_left_label ">
                                        <asp:CheckBox id="chkbill" runat="server" Text="Bill No" Checked="true" 
                                            oncheckedchanged="chkbill_CheckedChanged" AutoPostBack="true" Visible="false" />
                                    </td>
                                    <td>:</td>
                                
                                    <td class="form_left_label ">
                                        
                                       
                                        <asp:DropDownList ID="ddlbillno" AppendDataBoundItems="true" runat="server" Width="200px" Enabled="false" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                   
                                    
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="form_left_label ">
                                        <asp:CheckBox id="chkdate" runat="server" Text="Date Range" Checked="true" 
                                            oncheckedchanged="chkdate_CheckedChanged" AutoPostBack="true" Visible="false" />
                                    </td>
                                    <td>:</td>
                                
                                   
                                        
                                       
                                        <td class="form_left_label ">
                                        From Date:<asp:TextBox ID="txtfrmDate" runat="server" Width="80px" />
                                        <asp:Image ID="ImgBntCalc" runat="server" ImageUrl="~/images/calendar.png" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtfrmDate" PopupButtonID="ImgBntCalc">
                                        </ajaxToolKit:CalendarExtender>
                                  <%--  </td>
                                    <td style="padding-left: 10px;"  valign="top">--%>
                                          To Date:<asp:TextBox ID="txttoDate" runat="server" Width="80px" />
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txttoDate" PopupButtonID="Image1">
                                        </ajaxToolKit:CalendarExtender>
                                    </td>
                                   
                                    
                                   
                                </tr>
                                
                                
                                <%-- Row No 3--%>
                                <tr>
                                    <td colspan="3" align="center" class="form_button">
                                        <br />
                                        <asp:Button ID="btnReport" Text="Report" Width="100px" runat="server" 
                                            onclick="btnReport_Click"  />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" Text="Cancel" Width="100px" runat="server"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Label ID="lblReq" runat="server" SkinID="Errorlbl" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </fieldset>
                    </div>
                </asp:Panel>
            </td>
        </tr>
       <tr>
                                <td align="center">
                                    <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                </td>
                                
                            </tr>
                       
  
     <div id="divMsg" runat="server">
    </div>
    </table> 
</asp:Content>