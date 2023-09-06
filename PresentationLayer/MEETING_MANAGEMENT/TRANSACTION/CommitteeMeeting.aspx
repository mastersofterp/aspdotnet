<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CommitteeMeeting.aspx.cs" 
Inherits="MEETING_MANAGEMENT_TRANSACTION_CommitteeMeeting" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Flash the text/border red and fade in the "close" button --%>
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                Meeting Search
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr id="msgcomp" runat="server">
                                      <td>
                                           <asp:Label ID="Label1" runat="server" SkinID="Msglbl"> Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br /></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                            DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false"    ValidationGroup="btnReport"/>
                            
                                      </td>
        </tr>
        <%--  Shrink the info panel out of view --%> <%--  Reset the sample so it can be played again --%>
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
                            <%--  Enable the button so it can be played again --%>
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

             
                    <table width="100%" cellpadding="2" cellspacing="2">
               <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnlSearch" runat="server" Width="1116px" >
                           <fieldset class="fieldset">            
                        <legend class="legend"> Search Criteria</legend>
                            <table width="100%" cellpadding="2" cellspacing="2">
                            <tr>
                    <td class="form_left_label" style="width: 15%;">
                        Committee <span style="color: #FF0000">*</span> 
                    </td>
                    <td style="width: 2%;">
                        <b>:</b>
                    </td>
                    <td class="form_left_text">
                   <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" 
                            AutoPostBack="true" width="250px" >
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                   <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Commitee"
                                            ValidationGroup="btnReport" InitialValue="0" 
                            ControlToValidate="ddlCommitee" Display="None"
                                            Text="*"></asp:RequiredFieldValidator>
                   </td> 
                   </tr>
                  
                  <tr>
                    <td class="form_left_label" style="width: 15%;">
                        Meeting Period </td>
                    <td style="width: 2%;">
                        <b>:</b></td>
                    <td class="form_left_text">
                        From <b>:</b> &nbsp; <asp:TextBox ID="txtstartdate" runat="server" TabIndex="3"   Width="80px"  ></asp:TextBox>
                           <asp:RequiredFieldValidator ID="rfvFrmDt" runat="server" ControlToValidate="txtstartdate"
                                                    Display="None" 
                            ErrorMessage="Please Select Meeting Start Date" ValidationGroup="Date"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>                                             
                                            
                                                <asp:Image ID="imgFrmDt" runat="server" ImageUrl="~/images/calendar.png" 
                                                    Style="cursor: pointer" />
                                                <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtstartdate"
                                                    PopupButtonID="imgFrmDt" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeFrmDt" runat="server" TargetControlID="txtstartdate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevFrmDt" runat="server" ControlExtender="meeFrmDt"
                                                    ControlToValidate="txtstartdate" 
                                                    EmptyValueMessage="Please Enter Date" InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Date" 
                                                    SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>                  
                 
                           
                        &nbsp;
                        
                        
                        To <b>:</b> &nbsp;
                        
                        <asp:TextBox ID="txtenddate" runat="server" Width="80px" TabIndex="3"   ></asp:TextBox>
                           <asp:RequiredFieldValidator ID="rfvTodt" runat="server" 
                                                    ControlToValidate="txtenddate" Display="None" 
                                                    ErrorMessage="Please Select Meeting End Date" SetFocusOnError="true" 
                                                    ValidationGroup="Date"></asp:RequiredFieldValidator>
                                                <asp:Image ID="imgTodt" runat="server" ImageUrl="~/images/calendar.png" 
                                                    Style="cursor: pointer" />
                                                <ajaxToolKit:CalendarExtender ID="ceTodt" runat="server" Enabled="true" 
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgTodt" 
                                                    TargetControlID="txtenddate">
                                                </ajaxToolKit:CalendarExtender>
                                               <ajaxToolKit:MaskedEditExtender ID="meeToDt" runat="server" TargetControlID="txtenddate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevToDt" runat="server" ControlExtender="meeToDt"
                                                    ControlToValidate="txtenddate" 
                                                    EmptyValueMessage="Please Enter To Date" InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Date" 
                                                    SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator> 
                                                    
                                                 <asp:CompareValidator ID="CampCalExtDate" 
                            runat="server" ControlToValidate="txtenddate"
                                                CultureInvariantValues="true" Display="None" 
                            ErrorMessage="From Date Must Be Equal To Or Less Than To Date." 
                            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" 
                                                ValidationGroup="btnReport" 
                            ControlToCompare="txtstartdate" />
                       
                         
                            </td>
                </tr>
        

                </table>
                </fieldset></asp:Panel>
                       
                    </td>
                </tr> 
                            
              </table>
                    
       
    
                <br />
             <table width="100%" cellpadding="2" cellspacing="2">
                <tr id="trbtn" runat="server" visible="true" >
                
                    <td style="padding-top: 1px;padding-left:180px" colspan="3">
                      <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="btnReport"
                            Width="80px"  CausesValidation="true" onclick="btnReport_Click"/>&nbsp;&nbsp;
                       <asp:Button ID="btncancel" runat="server" Text="Cancel" 
                            Width="80px" onclick="btncancel_Click"/>  
                            
                    </td>
                </tr>
            </table>
            <div style="width: 90%; padding: 10px">
               
            </div>
       
    
      
      
    </asp:Content>

