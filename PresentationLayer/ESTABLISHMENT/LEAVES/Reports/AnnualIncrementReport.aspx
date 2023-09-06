<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AnnualIncrementReport.aspx.cs" Inherits="AnnualIncrementReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<fieldset class="fieldset">--%>
   
            <table style="width: 858px">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px; width: 726px;">
                       Annual Increment Report&nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
                <tr>
                    <td style="width: 726px">
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
                        <%--<script type="text/javascript" language="javascript">
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
                        </script>--%>
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
                    <td colspan="3" style="width: 726px; text-align: center;">
                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                    </td>
                </tr>
                <tr>
                  
                    <td colspan="4" >
                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                    </td>
                </tr>
       
            </table>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
   
       
          <asp:Panel ID="pnlInfo" runat="server"   Width="50%">
        <fieldset class="fieldsetPay">
            <legend class="legendPay">Annual Increment Report</legend>
          
            <table cellpadding="1" cellspacing="1" width="100%">     
           
             <tr id="trDaily" runat="server" visible="true">       
               
                   <td class="form_left_label" style="width:20%" align="center">
                     Select Date <span style="color: #FF0000">*</span>
                   </td>
                   <td style="width:2%">
                        <b>:</b>
                    </td>
                    <td style="width:30%">
                    <asp:TextBox ID="txtdate" runat="server" Width="80px"></asp:TextBox>
                        <asp:Image ID="imgDate" runat="server" ImageUrl="~/IMAGES/calendar.png" Style="cursor: pointer" />
                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            PopupButtonID="imgDate" TargetControlID="txtdate">
                        </ajaxToolKit:CalendarExtender>
                        <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            OnInvalidCssClass="errordate" Enabled="True" Mask="99/99/9999" MaskType="Date"
                            TargetControlID="txtdate">
                        </ajaxToolKit:MaskedEditExtender>
                        <ajaxToolKit:MaskedEditValidator ID="mvFdate" runat="server" ControlExtender="meDate"
                            ControlToValidate="txtdate" Display="None" EmptyValueMessage="Please Select Date."
                            ErrorMessage="Please Select Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                            IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="show"></ajaxToolKit:MaskedEditValidator>
                     
       <%--               <asp:TextBox ID="txtdate" runat="server" Width="80px" onblur="return checkdate(this);" Enabled="false"></asp:TextBox>
                      
                                &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="MMM/yyyy" TargetControlID="txtdate"
                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>--%>
                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdate"
                                                        Display="None" ErrorMessage="Please Enter Date" SetFocusOnError="true" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                                  
                   
                    </td>
                </tr>
                   
                </table>
                <br />
                <table cellpadding="1" cellspacing="1" width="100%">              
                   <tr>
                        <td align="center" colspan="3">
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" 
                                ValidationGroup="show" Width="80px"/>
                            <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                                Text="Cancel"  Width="80px"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ValidationSummary ID="vsAvgWork" runat="server" DisplayMode="List" 
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                        </td>
                    </tr>
                   
              
            </table>
            
        </fieldset></asp:Panel>
       
        
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblHead" runat="server" Visible="False" Style="text-align: center"></asp:Label>
                </td>
            </tr>
          
        </table>
   
    <%-- </ContentTemplate>
            
    </asp:UpdatePanel>--%>
    
    <div id="divMsg" runat="server">
    </div>
    
<script>
    function checkdate(input) {
        var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
        var returnval = false
        if (!validformat.test(input.value)) {
            alert("Invalid Date Format. Please Enter in MMM/YYYY Formate")
            document.getElementById("ctl00_ContentPlaceHolder1_txtdate").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_txtdate").focus();
        }

    }
</script>
</asp:Content>
