<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DocCopyMove.aspx.cs" 
Inherits="DOCUMENTANDSCANNING_DCMNTSCN_DocCopyMove" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../JAVASCRIPTS/jquery-1.4.min.js" type="text/javascript"></script>

    <script src="../JAVASCRIPTS/jquery.MultiFile.pack.js" type="text/javascript"></script>

    <table cellpadding="0" cellspacing="0" style="width:100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                COPY AND MOVE DOCUMENTS
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
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
                            
                            <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                            <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                <Move Horizontal="150" Vertical="-50" />
                                <Resize Width="260" Height="280" />
                                <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                            </Parallel>
                            
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
    
          <tr id="trmsg" runat="server">
                                      <td colspan="3" >
                                   Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                     </td>
        </tr>
        <tr>
        <td>
        <br />
        </td>
        </tr>
        <tr>
            <td style="padding-left: 15px;">
                <asp:Panel ID="pnlAdd" runat="server">
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Copy And Move Documents</legend>
                        <br />
                        <table width="700" cellpadding="0" cellspacing="0" border="0">
                                                
                            <tr>
                                <td class="form_left_label" style="width: 28%">
                                    From Copy/Move Location <span style="color: #FF0000">*</span> 
                                </td>
                                <td style="width:2%;"><b>:</b></td>
                                <td class="form_left_text" style="width: 240px">
                                    <asp:DropDownList ID="ddlSource" runat="server" Width="50%" AppendDataBoundItems="true"
                                        AutoPostBack="true" 
                                        onselectedindexchanged="ddlSource_SelectedIndexChanged" >
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddl" runat="server" ControlToValidate="ddlSource"
                                        Display="None" ErrorMessage="Please Select Source Files Location" ValidationGroup="Submit"
                                        SetFocusOnError="true" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                    <asp:Label ID="lblSource" runat="server" />
                                    <asp:HiddenField ID="hdnSource" runat="server" />
                                </td>               
                               
                              
                            </tr>
                            <tr>
                            <td class="form_left_label" style="width: 28%">
                            To Copy/Move Location <span style="color: #FF0000">*</span> 
                            </td>
                            <td style="width:2%;"><b>:</b></td>
                             <td class="form_left_text" style="width: 240px">
                                <asp:DropDownList ID="ddlTarget" runat="server" Width="50%" AppendDataBoundItems="true"
                                        AutoPostBack="true" 
                                     onselectedindexchanged="ddlTarget_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDest" runat="server" ControlToValidate="ddlTarget"
                                        Display="None" ErrorMessage="Please Select Target" ValidationGroup="Submit"
                                        SetFocusOnError="true" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                     <asp:Label ID="lblTarget" runat="server" />
                                     &nbsp;<asp:HiddenField ID="hdnTarget" runat="server" />
                                </td>
                                
                                   
                               
                            </tr>    
                            <tr>
                            <td>
                                <br>
                                                             
                                </br>                            
                            </td>
                            </tr>                
                          
                            <tr>
                                <td class="form_left_label" style="width: 28%" valign="top">
                                   
                                </td>
                                 <td style="width:2%;"><b>:</b></td>                             
                                <td class="form_left_text" style="width: 370px">
                                    <asp:Button ID="btnCopy" runat="server" Text="Copy Files" onclick="btnCopy_Click" 
                                        Width="73px" />
                                        &nbsp;&nbsp;<asp:Button ID="btnCut" runat="server" Text="Move Files" 
                                        Width="90px" onclick="btnCut_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnCopyFolder" runat="server" Text="Copy & Move Folder" 
                                        Width="142px" onclick="btnCopyFolder_Click" />
                                </td>
                            </tr>                    
                          </table>
                    </fieldset>
                </asp:Panel>
                
               
                        
         
                
            </td>
        </tr>
    </table>
    
   
  
 
   

    <script type="text/javascript">
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
    </script>

</asp:Content>
