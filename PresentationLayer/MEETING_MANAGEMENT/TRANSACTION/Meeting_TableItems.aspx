<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Meeting_TableItems.aspx.cs" Inherits="MEETING_MANAGEMENT_TRANSACTION_Meeting_TableItems" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                Meeting Agenda Entry
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
          <tr id="msgcomp" runat="server">
                                      <td>
                                           <asp:Label ID="Label1" runat="server" SkinID="Msglbl"> Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br /></asp:Label>
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <%--  Reset the sample so it can be played again --%>
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

             <fieldset class="fieldset">              
                    <table width="100%" cellpadding="2" cellspacing="2">
               <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnlMeetingInfo" runat="server" Visible="true" >
                            <table width="100%" cellpadding="2" cellspacing="2">
                  <tr>
                    <td class="form_left_label" style="width: 15%;">
                        Committee <span style="color: #FF0000">*</span></td>
                    <td style="width: 2%;">
                        <b>:</b></td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" 
                            AutoPostBack="true" width="250px" 
                            onselectedindexchanged="ddlCommitee_SelectedIndexChanged" TabIndex="1">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Committee"
                                            ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None"
                                            Text="*">
                           </asp:RequiredFieldValidator>
                        
                   </td>
                   </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 15%;">
                                        Previous Meeting </td>
                                    <td style="width: 2%;">
                                        <b>:</b></td>
                                    <td class="form_left_text">
                                       <asp:DropDownList ID="ddlpremeeting" runat="server" AutoPostBack="true" 
                                            AppendDataBoundItems="true" Width="250px"
                                            onselectedindexchanged="ddlpremeeting_SelectedIndexChanged" TabIndex="2" >
                                        
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                 <tr>
                    <td class="form_left_label" style="width: 15%;">
                        Meeting Code</td>
                    <td style="width: 2%;">
                        <b>:</b></td>
                    <td class="form_left_text">
                          <asp:TextBox ID="txtcode" runat="server" Enabled="false" width="250px" TabIndex="3" ></asp:TextBox>  
                    </td>
                </tr>
                 <tr>
                    <td class="form_left_label" style="width: 15%;">
                        Meeting Date <span style="color: #FF0000">*</span></td>
                    <td style="width: 2%;">
                        <b>:</b></td>
                    <td class="form_left_text">
                          <asp:TextBox ID="txtdate"  runat="server" TabIndex="4" 
                                            ValidationGroup="ScheduleDtl"  width="80px" ></asp:TextBox>      
                       <asp:Label runat="server" ID="lbldate" Visible="false" ></asp:Label>  
                      
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" 
                                            Style="cursor: pointer" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                            Enabled="true" EnableViewState="true" Format="dd/MM/yyyy" 
                                            PopupButtonID="Image2" TargetControlID="txtdate">
                                        </ajaxToolKit:CalendarExtender>
                                        
                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtdate"
                                            ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" 
                                            ControlExtender="medt" ControlToValidate="txtdate" Display="None" 
                                            EmptyValueMessage="Please Enter Meeting  Date" 
                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" 
                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" 
                                            IsValidEmpty="false" Text="*" ValidationGroup="Submit">
                                        </ajaxToolKit:MaskedEditValidator> 
                                        &nbsp;<b>Time : </b>
                                                    <asp:TextBox ID="txttime" runat="server" width="80px" meta:resourcekey="txttimeResource1" TabIndex="5"></asp:TextBox>
                                                    <ajaxToolKit:MaskedEditExtender ID="meinTime" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        OnInvalidCssClass="errordate" Enabled="True" Mask="99:99:99" MaskType="Time" TargetControlID="txttime"
                                                        AcceptAMPM="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="mevinTime" runat="server" ControlExtender="meinTime"
                                                        ControlToValidate="txttime" Display="None" EmptyValueMessage="Please Enter Time."
                                                        ErrorMessage="Please Select Time" InvalidValueBlurredMessage="*" InvalidValueMessage="Time is invalid"
                                                        IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Schedule"></ajaxToolKit:MaskedEditValidator>
                                                    <asp:Label ID="lblTipp" runat="server" Text="Tip: Type 'A' or 'P' to switch AM/PM"
                                                        meta:resourcekey="lblTippResource1"></asp:Label> 
                                                           <asp:RequiredFieldValidator ID="rfvtime" runat="server" ControlToValidate="txttime"
                                                    Display="None" ErrorMessage="Please Enter Meeting Time" ValidationGroup="Submit"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>       
                        
                    </td>
                </tr> 
                <tr>
                                    <td class="form_left_label" style="width: 15%;" valign="top">
                                        Venue <span style="color: #FF0000">*</span></td>
                                    <td style="width: 2%;" valign="top">
                                        <b>:</b></td>
                                    <td class="form_left_text">
                                       <asp:TextBox ID="txtvenue" runat="server" TextMode="MultiLine" onkeypress="return CheckAlphaNumeric(event,this);" width="250px" TabIndex="6" MaxLength="300"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="rfvvenue" runat="server" ControlToValidate="txtvenue"
                                                    Display="None" ErrorMessage="Please Enter Meeting Venue" ValidationGroup="Submit"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                </table>
                </asp:Panel>
                       
                    </td>
                </tr> 
                 <tr>
                 <td colspan="3">
                  <asp:Panel ID="pnlAgenda" runat="server" Visible="true" >
                            <table width="100%" cellpadding="2" cellspacing="2">
                                  <tr>
                                    <td class="form_left_label" style="width: 15%;">
                                        Agenda Number</td>
                                    <td style="width: 2%;">
                                        <b>:</b></td>
                                    <td class="form_left_text">
                                       <asp:TextBox ID="txtnumber" runat="server" Enabled="false" width="250px" TabIndex="7"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="form_left_label" style="width: 15%; height: 32px;">
                                        Agenda Title <span style="color: #FF0000">*</span></td>
                                    <td style="width: 2%; height: 32px;">
                                        <b>:</b></td>
                                    <td class="form_left_text" style="height: 32px">
                                       <asp:TextBox ID="txttitle" runat="server" onkeypress="return CheckAlphabet(event,this);" width="250px" TabIndex="8" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                                                    Display="None" ErrorMessage="Please Enter Agenda Title" ValidationGroup="Submit"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="trlock" runat="server" visible="false" >
                    <td class="form_left_label" style="width: 15%;">
                       Lock</td>
                    <td style="width: 2%;">
                        <b>:</b></td>
                    <td class="form_left_text">
                        <asp:CheckBox ID="chklock" runat="server" Text="Lock"  AutoPostBack="true" 
                            oncheckedchanged="chklock_CheckedChanged" /> 
                    </td>
                </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 15%;">
                                       Attach File</td>
                                    <td style="width: 2%;">
                                        <b>:</b></td>
                                    <td class="form_left_text">
                                        <asp:FileUpload ID="FileUpload1" runat="server" ValidationGroup="submit" ToolTip="Select file to upload"  TabIndex="9"/>
                                          &nbsp;<asp:Button ID="btnAdd" runat="server" Text="Add" 
                                            onclick="btnAdd_Click" TabIndex="10" />
                                                 
                                    </td>
                                </tr>
                        </table>
               </asp:Panel>
                 <asp:Panel ID="pnlFile" runat="server" Visible="false" >             
                    <table cellpadding="0" cellspacing="0" class="datatable" width="100%">    
                       <tr>
                  <td> 
                   <div style="width: 90%; padding: 10px">
                    <asp:ListView ID="lvfile" runat="server"> 
                       <%-- <EmptyDataTemplate><br/><font color="red"><b>No Data For DownLoad</b> </font> </EmptyDataTemplate>--%>
                         <LayoutTemplate>
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">Download files</div>
                                    <table class="datatable"  cellpadding="0" cellspacing="0" width="50%">
                                        <tr class="header">   
                                               <th>Action</th> 
                                            <th>File Name</th>                                            
                                            <th>Download</th> 
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" /> 
                                    </table>
                            </div>
                         </LayoutTemplate>
                         <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                     <td>                                                  
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" 
                                        CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>' AlternateText="Delete Record" ToolTip="Delete Record" 
                                        OnClick="btnDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                    </td>
                                    <td>
                                       <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                    </td>                                                                                                                                         
                                    <td>
                                      <asp:ImageButton ID="imgFile" runat="Server" Imageurl="~/images/action_down.gif" AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>' OnClick="imgdownload_Click" />
                                   </td>
                                </tr>
                      </ItemTemplate>
             </asp:ListView>
                   
               </div> 
                                    </td>
                                </tr>        
                            
                    </table>
                 </asp:Panel>
                 <asp:Panel ID="pnlCllaimInfo" runat="server" Visible="true" >                        
                  <table cellpadding="0" cellspacing="0" class="datatable" width="100%">                  
                   <tr>
                  <td> 
                   <div style="width: 90%; padding: 10px">
                                <asp:ListView ID="lvAgenda" runat="server" Visible="true" >
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="titlebar">
                                                MEETING AGENDA ENTRY
                                            </div>
                                            <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                                <tr class="header">
                                                    <th>
                                                        SELECT
                                                    </th>
                                                    <th>
                                                       AGENDA NUMBER
                                                    </th>
                                                    <th>
                                                        AGENDA TITLE
                                                    </th>
                                                    <th>
                                                         VENUE
                                                    </th>
                                                    <th>
                                                        Date
                                                    </th>
                                                    <th>
                                                        Time
                                                    </th>
                                                </tr>
                                                <tr ID="itemPlaceholder" runat="server" />
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" 
                                                onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                 <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" 
                                                        CausesValidation="false" CommandArgument='<%# Eval("PK_AGENDA") %>' 
                                                        ImageUrl="~/images/edit.gif"  ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                 <td>
                                                    <%# Eval("AGENDANO")%>
                                                </td>    
                                                <td>
                                                    <%# Eval("AGENDATITAL")%>
                                                </td> 
                                                <td>
                                                    <%# Eval("VENUE")%>
                                                </td> 
                                                <td>
                                                    <%# Eval("MEETINGDATE","{0:dd-MMM-yyyy}")%>
                                                </td> 
                                                <td>
                                                    <%# Eval("MEETINGTIME")%>
                                                </td> 
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFD2'" 
                                                onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" 
                                                        CausesValidation="false" CommandArgument='<%# Eval("PK_AGENDA") %>' 
                                                        ImageUrl="~/images/edit.gif"  ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                  <td>
                                                    <%# Eval("AGENDANO")%>
                                                </td>    
                                                <td>
                                                    <%# Eval("AGENDATITAL")%>
                                                </td> 
                                                <td>
                                                    <%# Eval("VENUE")%>
                                                </td> 
                                                <td>
                                                    <%# Eval("MEETINGDATE","{0:dd-MMM-yyyy}")%>
                                                </td> 
                                                <td>
                                                    <%# Eval("MEETINGTIME")%>
                                                </td>  
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                </div>
                                </td>
                                </tr>                 
                                </table>                         
                            
                           </asp:Panel>
                 </td>
                 </tr>           
              </table>
              </fieldset>           
              
        
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr id="Tr1" runat="server" visible="true" >
                    <td>
                        &nbsp;
                    </td>
                    <td style="padding-top: 10px;padding-left:150px" colspan="3">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                            Width="80px"  onclick="btnSubmit_Click" TabIndex="11" CausesValidation="true" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                            Width="80px" onclick="btnCancel_Click" TabIndex="12" CausesValidation="false" />                                   
                       <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit"  HeaderText="Following Field(s) are mandatory" />
                   
                    </td>
                </tr>
            </table>
            
    
            <br />
            <div style="width: 90%; padding: 10px">
               
            </div>

    
    <script type="text/javascript" language="javascript">
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            } //../images/action_up.gif
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }

        
    </script>
    </asp:Content>
    

