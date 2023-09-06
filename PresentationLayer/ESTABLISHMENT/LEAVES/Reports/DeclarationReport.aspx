<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DeclarationReport.aspx.cs" Inherits="DeclarationReport"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
              DECLARATION FORM &nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Shrink the info panel out of view --%>
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
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
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
            <td style="height: 16px">
                &nbsp;
            </td>
        </tr>
        </table>
        <asp:UpdatePanel ID="updAdd" runat="server">
        <ContentTemplate>
        
        <asp:Panel ID="pnlAdd" runat="server" Width="70%">
         <div style="text-align: left; padding-left: 10px; width: 92%">
            <fieldset class="fieldsetPay">
                <table>
                   <tr>
                        <td class="form_left_label">
                             College Name
                        </td>
                        <td style="width:1%">
                           <b>:</b>
                        </td>
                        <td class="form_left_text" >
                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems ="true" 
                                Width ="80%" AutoPostBack="true" 
                                onselectedindexchanged="ddlCollege_SelectedIndexChanged">
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                Display="None" ErrorMessage="Please Select College" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue ="0"></asp:RequiredFieldValidator>
                        </td>                                                
                    </tr> 
                    <tr>
                        <td class="form_left_label">
                             Staff Type
                        </td>
                        <td style="width:1%">
                           <b>:</b>
                        </td>
                        <td class="form_left_text" >
                            <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems ="true" AutoPostBack="true" 
                                Width ="80%" onselectedindexchanged="ddlStaffType_SelectedIndexChanged">
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStaffType"
                                Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue ="0"></asp:RequiredFieldValidator>
                        </td>                                                
                    </tr>
                    <tr>
                        <td class="form_left_label" style="height: 38px">
                            From Year
                        </td>
                       <td style="width:1%; height: 38px;">
                           <b>:</b>
                        </td>
                        <td class="form_left_text" style="height: 38px">
                            <asp:TextBox ID="txtFromdt" runat="server" Width="80px"></asp:TextBox>
                            &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="MMM/yyyy" TargetControlID="txtFromdt"
                                PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                            </ajaxToolKit:CalendarExtender>  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromdt"
                                Display="None" ErrorMessage="Please Select From Year" SetFocusOnError="true" ValidationGroup="Leaveapp">
                            </asp:RequiredFieldValidator>                                           
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; To Year <b>:</b>
                        &nbsp;
                           <asp:TextBox ID="txtTodate" runat="server" Width="80px"></asp:TextBox>
                            &nbsp;<asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MMM/yyyy" TargetControlID="txtTodate"
                                PopupButtonID="Image2" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                            </ajaxToolKit:CalendarExtender>  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTodate"
                                Display="None" ErrorMessage="Please Select To Year" SetFocusOnError="true" ValidationGroup="Leaveapp">
                            </asp:RequiredFieldValidator>   
                        </td>                                               
                    </tr>  
                    <tr id="trOption" runat="server" visible="false">
                        <td class="form_left_label">
                            
                        </td>
                       <td style="width:1%">
                            
                        </td>
                        <td class="form_left_text">
                            <asp:RadioButtonList ID="rblAllParticular" runat="server" 
                                RepeatDirection ="Horizontal" Width ="100%" 
                                onselectedindexchanged="rblAllParticular_SelectedIndexChanged" AutoPostBack ="true">
                            <asp:ListItem Enabled ="true" Selected ="True" Text ="All Employees" Value ="0"></asp:ListItem>
                            <asp:ListItem Enabled ="true" Text ="Particular Employee" Value ="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>                                               
                    </tr>
                    
                    <tr id="tremp" runat ="server" >
                        <td class="form_left_label">
                            Employee
                        </td>
                        <td style="width:1%">
                           <b>:</b>
                        </td>
                        <td class="form_left_text" >
                            <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems ="true" Width ="80%" >
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                Display="None" ErrorMessage="Please select Employee" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue ="0"></asp:RequiredFieldValidator>
                        </td>                                                
                    </tr> 
                    <tr id="trReports" runat ="server" >
                        <td class="form_left_label">
                           
                        </td>
                        <td style="width:1%">
                           
                        </td>
                        <td class="form_left_text" >
                            <asp:RadioButtonList ID="rblReport" runat="server" Width ="80%" RepeatDirection ="Horizontal" AutoPostBack="true" >
                            <asp:ListItem Text="Faculty" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Resident" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                            
                        </td>                                                
                    </tr> 
                    <tr>
                        <td class="form_left_label">                           
                        </td>
                      <td style="width:1%">     
                        </td>
                        <td class="form_left_text" >                        
                            <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Leaveapp"
                                Width="80px" onclick="btnReport_Click" />                             
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                Width="80px" onclick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                      </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        </asp:Panel>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnReport"/>
        
        </Triggers>
        </asp:UpdatePanel>
       
       
    
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
        
        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    </td>
                    <td>
                        &nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    
    <script type="text/javascript">
    //  keeps track of the delete button for the row
    //  that is going to be removed
    var _source;
    // keep track of the popup div
    var _popup;
    
    function showConfirmDel(source){
        this._source = source;
        this._popup = $find('mdlPopupDel');
        
        //  find the confirm ModalPopup and show it    
        this._popup.show();
    }
    
    function okDelClick(){
        //  find the confirm ModalPopup and hide it    
        this._popup.hide();
        //  use the cached button as the postback source
        __doPostBack(this._source.name, '');
    }
    
    function cancelDelClick(){
        //  find the confirm ModalPopup and hide it 
        this._popup.hide();
        //  clear the event source
        this._source = null;
        this._popup = null;
    }
    
//    function showLvstatus(source)
//    {   
//        this._source=source;
//        
//        //__doPostBack(this._source.name, '');
//        __doPostBack(this._source.name,'');
//        this._popup=$find('mdlPopupView');
//        
//        this._popup.show();
//    }    
//    function backClick()
//    {
//       this._popup.hide();        
//       this._source = null;
//       this._popup = null; 
//    }
//    
    </script>
    
     <div id="divMsg" runat="server">
    </div>
    
</asp:Content>

