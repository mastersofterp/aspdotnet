<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Comp_Off_Leave_ListNew.aspx.cs" Inherits="Comp_Off_Leave_ListNew" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Flash the text/border red and fade in the "close" button --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                TRANSFER ATTENDANCE/LWP RECORD&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%-- <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll" 
                    DisplayMode="List" ShowMessageBox="false" ShowSummary="false" />--%>
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
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) 
                {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) 
                    {
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
            </td>
        </tr>
    </table>
    <br />   
   <asp:Panel ID="pnlView" runat="server" Width="70%" Visible="true">
   <fieldset class="fieldsetPay" runat="server">
  <%-- <legend class="legendPay">Transfer Attendance Record</legend>--%>
   <table width="100%" cellpadding="1" cellspacing="1">
        <tr>
            <td>
                <asp:ListView ID="lvEmpList" runat="server" >
                    <EmptyDataTemplate>
                       <br />
                       <center>
                       <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" /></center>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                       <div class="vista-grid">                                                   
                           <div class="titlebar">
                                 Absent Days Record
                           </div>
                           <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                               <thead>
                                <tr class="header">  
                                                   <th>
                                                        Sr.No
                                                    </th>
                                                    <th>
                                                        <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />
                                                        Select
                                                    </th>
                                                    <th>
                                                        Employee Name
                                                    </th>
                                                    <th>
                                                        Working Date
                                                    </th>
                                                   <%-- <th>
                                                        In Time
                                                    </th>
                                                    <th>
                                                        Out Time
                                                    </th>--%>
                                                    <th >
                                                        Hour
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
                                                 <td >
                                                <%#Container.DataItemIndex+1 %>
                                            </td>
                                            <td >
                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("RNO") %>' onclick="EnableCredit(this)"/>
                                                <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("RNO") %>' />
                                            </td>
                                            <td > 
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td >
                                                <%# Eval("WORKING_DATE","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <%--<td>
                                                <%# Eval("IN_TIME")%>
                                            </td>
                                            <td>
                                                <%# Eval("OUT_TIME")%>
                                            </td>--%>
                                            <td>
                                                <%# Eval("WORKING_HOUR")%>
                                            </td>
                         </tr>                    
                     </ItemTemplate>
                    <AlternatingItemTemplate>
                                              <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                 <td >
                                                <%#Container.DataItemIndex+1 %>
                                            </td>
                                            <td >
                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("RNO") %>' onclick="EnableCredit(this)"/>
                                                <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("RNO") %>' />
                                            </td>
                                            <td > 
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td >
                                                <%# Eval("WORKING_DATE","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <%--<td>
                                                <%# Eval("IN_TIME")%>
                                            </td>
                                            <td>
                                                <%# Eval("OUT_TIME")%>
                                            </td>--%>
                                            <td>
                                                <%# Eval("WORKING_HOUR")%>
                                            </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                </asp:ListView>
            </td>
        </tr>  
   </table>
   
 </fieldset>
   
   </asp:Panel>
  
     
   <asp:Panel ID="Panel1" runat="server" Width="70%" Visible="true">  
   <table width="100%" cellpadding="1" cellspacing="1">
   <tr>
   <td align="center">
   
  
          
    
     <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="10%" Visible="false"
                            ValidationGroup="Payroll" TabIndex="6" onclick="btnEdit_Click"  />
     <asp:Button ID="btnUpdate" runat="server" Text="Update"  OnClientClick="showConfirmUpdate(this); return false;"
           Width="10%" Enabled="false" Visible="false" onclick="btnUpdate_Click"/>
     <%--<asp:Button ID="btnReport" runat="server" Text="Report" 
           Width="10%" Visible="false" onclick="btnReport_Click" />--%>
        
  
    </td>
    </tr>
    </table>    
    </asp:Panel>
   
                  
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
                    &nbsp;&nbsp;Are you sure you want to Update Attendance record?
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

     function showConfirmUpdate(source) {
         this._source = source;
         this._popup = $find('mdlPopupDel');

         //  find the confirm ModalPopup and show it    
         this._popup.show();
     }
     ;debugger
     function okDelClick() {
         //  find the confirm ModalPopup and hide it    
//         this._popup.hide();
//         //  use the cached button as the postback source
//         //         __doPostBack(this._source.name, '');
//         var st = vall.id.split("lvEmpList_ctrl");
//         var i = st[1].split("_txtleave");
//         var index = i[0];
//         document.getElementById('ctl00_ContentPlaceHolder1_lvEmpList_ctrl' + index + '_txtleave').disabled = false;
//         document.getElementById('ctl00_ContentPlaceHolder1_btnUpdate').value = "Update";


         
//         var frm = document.forms[0];
//         for (i = 0; i < document.forms[0].elements.length; i++) {
//             var e = frm.elements[i];
//             if (e.type == 'textbox') {

//                 e.disabled = false;
//             }
//         }
//         document.getElementById('ctl00_ContentPlaceHolder1_btnUpdate').text = "Update";

         this._popup.hide();
         __doPostBack(this._source.name, '');
     }

     function cancelDelClick() {
         //  find the confirm ModalPopup and hide it 
         this._popup.hide();
         //  clear the event source
         this._source = null;
         this._popup = null;
     }


     function validateNumeric(txt) {
         if (isNaN(txt.value)) {
             txt.value = txt.value.substring(0, (txt.value.length) - 1);
             txt.value = '';
             txt.focus = true;
             alert("Only Numeric Characters allowed !");
             return false;
         }
         else
             return true;
     }

     function validateAlphabet(txt) {
         var expAlphabet = /^[A-Za-z]+$/;
         if (txt.value.search(expAlphabet) == -1) {
             txt.value = txt.value.substring(0, (txt.value.length) - 1);
             txt.value = '';
             txt.focus = true;
             alert("Only Alphabets allowed!");
             return false;
         }
         else
             return true;
     }
</script>
    <div id="divMsg" runat="server">
    </div>
    
   
</asp:Content>

