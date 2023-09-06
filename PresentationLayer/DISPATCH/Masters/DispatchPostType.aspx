<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DispatchPostType.aspx.cs" Inherits="Dispatch_Masters_DispatchPostType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="70%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">
                        POST TYPE&nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                           <tr id="trmsg" runat="server">
                                      <td colspan="3" >
                                   Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
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
                <td>
                <br />
                </td>
                </tr>
                     <tr>
                    <td>
                        <asp:Panel ID="pnlAdd" runat="server">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay" style="color:Black">Add Post Type</legend>
                                 <table width="100%" cellpadding="2" cellspacing="2">
                               
                                    <tr>
                                    
                                        <td class="form_left_label" style="width: 25%;">
                                            Post Type  <span style="color: #FF0000">*</span> 
                                        </td>
                                         <td style="width: 2%;">
                                         <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtPostTypeName" runat="server" MaxLength="30" Width="200px" TabIndex="1" onkeypress="return CheckAlphabet(event,this);" />
                                            <asp:RequiredFieldValidator ID="rfvPostTypeName" runat="server" ControlToValidate="txtPostTypeName"
                                                Display="None" ErrorMessage="Please Enter Post Type Name" ValidationGroup="PostType"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 25%;">
                                         </td>
                                         
                                          <td style="width: 2%;">
                                        
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="PostType" OnClick="btnSave_Click"
                                                Width="60px" TabIndex="2" CausesValidation="true" />&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                OnClick="btnCancel_Click" Width="60px" TabIndex="3" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PostType"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset></asp:Panel>
                    </td>
                </tr>
            
                <tr>
                    <td>
                        <asp:Panel ID="pnlList" runat="server">
                            <table cellpadding="0" cellspacing="0" style="width: 70%; text-align: center">
                                <tr>
                                    <td style="text-align: left; padding-top: 10px; padding-left: 50px;">
                                        <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                                    </td>
                                </tr>
                            
                                <tr>
                                    <td align="center">
                                        <asp:ListView ID="lvPostType" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <div class="titlebar">
                                                        POST TYPE ENTRY</div>
                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr class="header">
                                                            <th>
                                                                EDIT
                                                            </th>
                                                            <th>
                                                                POST TYPE
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("POSTTYPENO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
   
                                                    </td>
                                                    <td>
                                                        <%# Eval("POSTTYPENAME")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                             <AlternatingItemTemplate>
                                              <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("POSTTYPENO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
   
                                                    </td>
                                                    <td>
                                                        <%# Eval("POSTTYPENAME")%>
                                                    </td>
                                                </tr>
                                        </AlternatingItemTemplate>
                                        </asp:ListView>
                                        <div class="vista-grid_datapager">
                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvPostType" PageSize="20"
                                                OnPreRender="dpPager_PreRender">
                                                <Fields>
                                                    <asp:NumericPagerField ButtonCount="3" ButtonType="Link" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <%--<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server" 
    TargetControlID="div" PopupControlID="div" 
    OkControlID="btnOkDel" OnOkScript="okDelClick();" 
    CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />   
<asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
    <div style="text-align:center">
        <table>
            <tr>
                <td align="center"><img align="middle" src="~/images/warning.gif" alt=""/></td>
                <td>&nbsp;&nbsp;Are you sure you want to delete this record?
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
</script>--%>
    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>
</asp:Content>

