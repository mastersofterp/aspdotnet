﻿<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeaveConfiguration.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_LeaveConfiguration" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:UpdatePanel ID="updPanel" runat="server" >
   <ContentTemplate>
   
   
   <table width="100%" cellpadding="1" cellspacing="1" class="table-formdata">
               <div id="divMsg" runat="server"> </div>
                <tr>
                    <td class="vista_page_title_bar" style="border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;" colspan="6">
                        LEAVE CONFIGURATION
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                            border: solid 1px #D0D0D0;">
                        </div>
                    </td>
                </tr>
       
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
                            Edit Record
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
                    <td style="width: 5%">
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td colspan="4">
                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                    </td>
                </tr>
            </table>
       <center>
      
                <asp:Panel ID="pnlAdd" runat="server" Width="90%">
                 
                        <fieldset class="fieldset">
                            <legend class="legend">Leave Configuration</legend>                            
                              <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                <tr>
                                            <td class="form_left_label" style="width: 37%; font-weight:bold">
                                                No. of Days allowed Before Current date For OD Slip :
                                            
                                            </td>
                                            <td class="form_left_text">
                                                <asp:TextBox ID="txtODDays" runat="server" onkeypress="return CheckNumeric(event,this);" Width="60px"></asp:TextBox>
                                            
                                            </td>
                                             <td class="form_left_label" style="width: 30%;padding-left:20px; font-weight:bold">
                                              
                                                No. of Days allowed Before Current date For OD application :
                                            </td>
                                            <td class="form_left_text">
                                                <%--<asp:TextBox ID="txtLWPNo" runat="server" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>--%>
                                           
                                              <asp:TextBox ID="txtODAppDays" runat="server" onkeypress="return CheckNumeric(event,this);" Width="60px"></asp:TextBox>       
                                            </td>
                                            </tr>
                                            
                                             <tr>
                                                    <td class="form_left_label" style="width: 37%; font-weight:bold">
                                                    Casual Leave No  &nbsp; &nbsp; &nbsp;   :
                                                    </td>
                                                    <td class="form_left_text">
                                                       <asp:DropDownList ID="ddlCasual" runat="server" AppendDataBoundItems="true" Width="200px"></asp:DropDownList>
                                                    </td>
                                                    <td class="form_left_label" style="width: 30%;padding-left:20px; font-weight:bold">
                                                         Earned Leave No:                                            
                                                     </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlEarned" runat="server" AppendDataBoundItems="true" Width="200px"></asp:DropDownList>
                                                    
                                                    </td>
                                                </tr>
                                            
                                            <tr>
                                                <td class="form_left_label" style="width: 37%; font-weight:bold">
                                                    Medical Leave No  &nbsp;:    
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:DropDownList ID="ddlMedical" runat="server" AppendDataBoundItems="true" Width="200px"></asp:DropDownList>
                                                                                                                                                  
                                                </td>
                                                <td class="form_left_label" style="width: 30%;padding-left:20px; font-weight:bold">
                                                           LWP Leave No  &nbsp;:                                   
                                                </td>
                                                <td class="form_left_text">
                                                   <asp:DropDownList ID="ddlLWP" runat="server" AppendDataBoundItems="true" Width="200px"></asp:DropDownList>
                                                     
                                                </td>
                                            </tr>
                                   <tr>
                                                <td class="form_left_label" style="width: 37%; font-weight:bold">
                                                    Maternity Leave No  &nbsp;:    
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:DropDownList ID="ddlMaternity" runat="server" AppendDataBoundItems="true" Width="200px"></asp:DropDownList>
                                                                                                                                                  
                                                </td>
                                                <td class="form_left_label" style="width: 30%;padding-left:20px; font-weight:bold">
                                                           Paternity Leave No  &nbsp;:                                   
                                                </td>
                                                <td class="form_left_text">
                                                   <asp:DropDownList ID="ddlPaternity" runat="server" AppendDataBoundItems="true" Width="200px"></asp:DropDownList>
                                                     
                                                </td>
                                            </tr>
                                </table>
                        </fieldset>                  
                </asp:Panel>               
                <center>
                    <asp:Panel ID="pnlbutton" runat="server" Width="90%">
                        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Submit" ValidationGroup="LeaveName" Width="80px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Visible="false" CausesValidation="false" OnClick="btnCancel_Click" Text="Cancel" Width="100px" />
                        &nbsp;
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="LeaveName" />
                    </asp:Panel>
                </center>
                <br>
                <br>
                <br></br>
                <br></br>
                </br>
                </br>
                </center>
      </ContentTemplate>
      <Triggers>
      <asp:PostBackTrigger ControlID="btnSave"/>
      </Triggers>
   </asp:UpdatePanel>
        <%--  Enable the button so it can be played again --%>            <%# Eval("Leave_Name")%>
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

                function showConfirmDel(source) {
                    this._source = source;
                    this._popup = $find('mdlPopupDel');

                    //  find the confirm ModalPopup and show it    
                    this._popup.show();
                }

                function okDelClick() {
                    //  find the confirm ModalPopup and hide it    
                    this._popup.hide();
                    //  use the cached button as the postback source
                    __doPostBack(this._source.name, '');
                }

                function cancelDelClick() {
                    //  find the confirm ModalPopup and hide it 
                    this._popup.hide();
                    //  clear the event source
                    this._source = null;
                    this._popup = null;
                }
                function CheckNumeric(event, obj) {
                    var k = (window.event) ? event.keyCode : event.which;
                    //alert(k);
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                        obj.style.backgroundColor = "White";
                        return true;
                    }
                    if (k > 45 && k < 58) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter numeric Value');
                        obj.focus();
                    }
                    return false;
                }

                function CheckAlphabet(event, obj) {

                    var k = (window.event) ? event.keyCode : event.which;
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter Alphabets Only!');
                        obj.focus();
                    }
                    return false;
                }

            </script>
    </asp:Content>

