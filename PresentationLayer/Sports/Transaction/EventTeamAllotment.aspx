<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EventTeamAllotment.aspx.cs" Inherits="Sports_Transaction_EventTeamAllotment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Flash the text/border red and fade in the "close" button --%>
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                EVENT TEAM ALLOTMENT
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Shrink the info panel out of view --%>        <%--  Reset the sample so it can be played again --%>
        <tr>
            <td>
                Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>           
                    <table cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td class="form_left_label" style="width:10%;">
                                Event Type <span style="color: #FF0000">*</span>
                            </td>
                            <td style="width:1%;">
                                <b>:</b>
                            </td>
                            <td class="form_left_text">
                              <asp:DropDownList ID="ddlEventType" runat="server" AppendDataBoundItems="true" TabIndex="1" Width="30%" AutoPostBack="true" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged"></asp:DropDownList>
                              <asp:RequiredFieldValidator ID="rfvET" runat="server" ControlToValidate="ddlEventType" Display="None" 
                                    ErrorMessage="Please Select Event Type." InitialValue="0" ValidationGroup="Submit" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label" style="width:10%;">
                                Event Name <span style="color: #FF0000">*</span>
                            </td>
                            <td style="width:1%;">
                                <b>:</b>
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlEvent" runat="server" AppendDataBoundItems="true" TabIndex="1" Width="30%"></asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="rfvEvent" runat="server" ControlToValidate="ddlEvent" Display="None" 
                                    ErrorMessage="Please Select Event" InitialValue="0" ValidationGroup="Submit" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label" style="width: 10%; height: 28px;">
                                Teams &nbsp; <span style="color: #FF0000">*</span></td>
                            <td style="width:1%; height: 28px;">
                                <b>:</b>
                            </td>
                            <td class="form_left_text" style="height: 28px">
                                <asp:DropDownList ID="ddlTeam" runat="server" AppendDataBoundItems="true" Width="30%" TabIndex="2">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTeam" runat="server"  ControlToValidate="ddlTeam" Display="None" 
                                    ErrorMessage="Please Select Team." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                  
                            </td>
                        </tr>
                           <tr>
                            <td class="form_left_label" style="width:10%;">
                                Venue <span style="color: #FF0000">*</span>
                            </td>
                            <td style="width:1%;">
                                <b>:</b>
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlVenue" runat="server" AppendDataBoundItems="True"  Width="30%" TabIndex="3">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvVenue" runat="server"  ControlToValidate="ddlVenue" Display="None" 
                                    ErrorMessage="Please Select Venue for the Event." InitialValue="0" ValidationGroup="Submit" ></asp:RequiredFieldValidator>                                                                 
                            </td>
                        </tr>                                                         
                        <tr>
                            <td class="form_left_label" style="width:10%;">
                            </td>
                            <td style="width:1%;">
                            </td>
                            <td class="form_left_text">
                                <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" 
                                    Width="80px" TabIndex="4" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click" Text="Cancel" Width="80px" TabIndex="5" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </td>
                        </tr>
                    </table>
               
              
          
               
            <br />
            <div style="width: 70%; padding: 10px">
                <asp:ListView ID="lvEventTeam" runat="server" >
                 
                    <LayoutTemplate>
                        <div id="demo-grid" class="vista-grid">
                            <div class="titlebar">
                                EVENT TEAMS ALLOTMENT
                            </div>
                            <table class="datatable" cellpadding="0" cellspacing="0">
                                <tr class="header">
                                    <th>
                                        Edit
                                    </th>                                  
                                    <th>
                                        Event Name
                                    </th>
                                    <th>
                                        Team Name
                                    </th> 
                                    <th>
                                        College
                                    </th>                  
                                   <th>
                                        Venue
                                    </th>                       
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                            <td>
                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                    CommandArgument='<%# Eval("ETALLOTID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                    OnClick="btnEdit_Click" />
                            </td>
                           <td>
                                <%# Eval("EVENTNAME")%>
                            </td>
                            <td>
                                <%# Eval("TEAMNAME")%>
                            </td>                        
                          <td>
                                <%# Eval("COLLEGE_NAME")%>
                            </td>  
                            <td>
                                <%# Eval("VENUENAME")%>
                            </td>
                               
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                            <td>
                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                    CommandArgument='<%# Eval("ETALLOTID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                    OnClick="btnEdit_Click" />
                            </td>
                             <td>
                                <%# Eval("EVENTNAME")%>
                            </td>
                            <td>
                                <%# Eval("TEAMNAME")%>
                            </td>                         
                           
                            <td>
                                <%# Eval("VENUENAME")%>
                            </td>
                          
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

