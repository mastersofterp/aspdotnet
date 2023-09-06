<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CL_45_Appointment.aspx.cs" Inherits="CL_45_Appointment"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
               CL-45 APPOINTMENT&nbsp;
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
                            <%--<asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
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

                    function totAllSubjects(headchk) {
                        var frm = document.forms[0]
                        for (i = 0; i < document.forms[0].elements.length; i++) {
                            var e = frm.elements[i];
                            if (e.type == 'checkbox') {
                                if (headchk.checked == true)
                                    e.checked = true;
                                else
                                    e.checked = false;
                            }
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
        </table>    
           <asp:Panel ID="pnlView" runat="server" Width="90%" Visible="true">
   <fieldset id="Fieldset1" class="fieldsetPay" runat="server">
  <%-- <legend class="legendPay">Transfer Attendance Record</legend>--%>
   <table width="100%" cellpadding="1" cellspacing="1">
        <tr>
            <td align="center">
            <b>Select College :</b>
               <asp:DropDownList ID="ddlCollege" runat="server" Width="230px" AppendDataBoundItems ="true" AutoPostBack="true" onselectedindexchanged="ddlCollege_SelectedIndexChanged">                                               
                                                      </asp:DropDownList>  
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select College Name " ValidationGroup="Leaveapp"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>    
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="lvList" runat="server">
                    <EmptyDataTemplate>
                       <br />
                       <center>
                       <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" /></center>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                           <div class="vista-grid">                                                   
                               <div class="titlebar">
                                   Appointment List
                               </div>
                               <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                   <thead>
                                    <tr class="header">  
                                                   <th style="width:2%">
                                                        Sr.No
                                                    </th>
                                                     <th style="width:9%">
                                                        <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />
                                                        Select
                                                    </th>
                                                    <th style="width:22%">
                                                        Appointment Name
                                                    </th>                                                                                                          
                                    </tr>
                                    </thead>
                                 </table>
                             </div>
                             <div class="listview-container">
                                                    <div id="demo-grid" class="vista-grid">
                                                        <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                             </div>
                     </LayoutTemplate>
                    <ItemTemplate>
                           <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                           <td style="width:4%">
                                                <%#Container.DataItemIndex+1 %>
                                            </td>
                                            <td style="width:10%">
                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("APPOINTNO") %>' />
                                                <asp:HiddenField ID="hidAppointNo" runat="server" Value='<%# Eval("APPOINTNO") %>' />
                                            </td>
                                           <td style="width:30%">
                                                <%# Eval("APPOINT")%>
                                            </td>
                                           
                          </tr>                  
                     </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                           <td style="width:4%">
                                                <%#Container.DataItemIndex+1 %>
                                            </td>
                                            <td style="width:10%">
                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("APPOINTNO") %>' />
                                                <asp:HiddenField ID="hidAppointNo" runat="server" Value='<%# Eval("APPOINTNO") %>' />
                                            </td>
                                           <td style="width:30%">
                                                <%# Eval("APPOINT")%>
                                            </td>
                                           
                          </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
            </td>
        </tr>  
   </table>   
 </fieldset>   
   </asp:Panel>
       <asp:Panel ID="Panel1" runat="server" Height="450px" Width="90%">
          <table width="100%" cellpadding="1" cellspacing="1">                     
               <tr id="trShow" runat="server">
              <td align="center">
                  &nbsp;<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" 
                  Text="Save" ValidationGroup="Leaveapp" Visible="False" Width="80px" />                 
                  &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                  Text="Cancel" Width="80px" />
                  <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Leaveapp"
                           ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
              </td>
         </tr>
          <tr>
             <td class="form_button" align="center">
                &nbsp; &nbsp;&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server"
                ValidationGroup="Leave" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
             </td>
        </tr>
         </table>
       </asp:Panel>
        
    
</asp:Content>
