<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ManufacturerMaster.aspx.cs" Inherits="Health_StockMaintenance_ManufacturerMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script  src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%--  Shrink the info panel out of view --%>
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
               MANUFACTURE DETAILS
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Reset the sample so it can be played again --%>
        <%--  Enable the button so it can be played again --%>
        <tr>
            <td>
                Note <b>:</b> <span style="color: #FF0000">* Marked Fields Are Mandatory.</span>
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
                            <%--<asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
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
             <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                   <td>
                  <fieldset class="fieldset" style="width: 70%;">
                        <legend class="legend">Manufacture Details</legend>
                <table width="100%" cellpadding="2" cellspacing="2">
                    <tr>
                        <td class="form_left_label" style="width:20%;">
                            Manufacturer Code <span style="color: #FF0000">*</span>
                        </td>
                        <td style="width:2%;"><b>:</b></td>                      
                        <td class="form_left_text">
                       <asp:TextBox ID="txtMCode" TabIndex="1" runat="server" Width="30%" CssClass="form_text"
                                    Style="font-family: Verdana; font-size: 11px;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvVendCode" runat="server" ControlToValidate="txtMCode"
                                    Display="None" ErrorMessage="Please Enter Manufacturer Code" ValidationGroup="submit"></asp:RequiredFieldValidator>                 
                        </td>
                    </tr>
                     <tr>
                        <td class="form_left_label" style="width:20%;">
                            Manufacturer Name <span style="color: #FF0000">*</span>
                        </td>
                        <td style="width:2%;"><b>:</b></td>                      
                        <td class="form_left_text">
                       <asp:TextBox ID="txtMName" TabIndex="2" runat="server" Width="30%" CssClass="form_text"
                                    Style="font-family: Verdana; font-size: 11px;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMName" runat="server" ControlToValidate="txtMName"
                                    Display="None" ErrorMessage="Please Enter Manufacturer Name" ValidationGroup="submit"></asp:RequiredFieldValidator>             
                        </td>
                    </tr>
                     <tr>
                        <td class="form_left_label" style="width:20%;">
                           Address <span style="color: #FF0000">*</span>
                        </td>
                        <td style="width:2%;"><b>:</b></td>                      
                        <td class="form_left_text">
                       <asp:TextBox ID="txtAddress" TextMode="MultiLine" TabIndex="4" runat="server" Width="30%" CssClass="form_text"
                                    Style="font-family: Verdana; font-size: 11px;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAdd" runat="server" ControlToValidate="txtAddress"
                                    Display="None" ErrorMessage="Please Enter Manufacturer Address" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                         
                        </td>
                    </tr>
                     <tr>
                                <td class="form_left_label" style="width:20%;">
                                    Contact Person 
                                </td>
                                <td style="width:2%;"><b>:</b></td>                      
                                <td class="form_left_text">
                               <asp:TextBox ID="txtContPers" TabIndex="7" runat="server" Width="30%" CssClass="form_text"
                                    Style="font-family: Verdana; font-size: 11px;"></asp:TextBox>          
                                                        
                                </td>
                               </tr>
                   <tr>
                        <td class="form_left_label" style="width:20%;">
                           Contact No
                        </td>
                        <td style="width:2%;"><b>:</b></td>                      
                        <td class="form_left_text">
                        <asp:TextBox ID="txtPhone" TabIndex="7" runat="server" Width="30%" CssClass="form_text"
                                    Style="font-family: Verdana; font-size: 11px;"></asp:TextBox>                        
                        </td>
                    </tr>   
                     <tr>
                            <td class="form_left_label" style="height: 31px" >
                                  Email Id 
                            </td>
                             <td style="width:2%;"><b>:</b></td>  
                            <td  class="form_left_text">
                                <asp:TextBox ID="txtEmail" TabIndex="8" runat="server" Width="30%" CssClass="form_text"
                                    Style="font-family: Verdana; font-size: 11px;"></asp:TextBox>
                              </td>                            
                        </tr>
                     <tr>
                        <td class="form_left_label" style="width:20%;">
                             Remark
                        </td>
                        <td style="width:2%;"><b>:</b></td>                      
                        <td class="form_left_text">
                         <asp:TextBox ID="txtRemark" TabIndex="11" runat="server" Width="30%" CssClass="form_text"
                                    Style="font-family: Verdana; font-size: 11px;"></asp:TextBox>         
                    </tr>  
                 
                    <tr>
                       <td class="form_left_label" style="width:15%;">
                       </td>
                       <td style="width:2%;">
                       </td>
                       <td class="form_left_text">
                           <asp:Button ID="btnSave" TabIndex="12" runat="server" Text="Save" Width="50px" CssClass="form_button"
                                    ValidationGroup="submit" OnClick="btnSave_Click" />                               
                                    &nbsp;                                   
                                    &nbsp;<asp:Button ID="btnCancel" TabIndex="13" runat="server" Text="Cancel" Width="50px"
                                    CssClass="form_button" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="submit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                       </td>
                    </tr>
                </table>
                      </fieldset>
                    </td>
                  </tr>
              </table>
            <br />
            <div style="width: 80%; padding: 10px">
                <asp:ListView ID="lvManufacture" runat="server" >
                    <LayoutTemplate>
                        <div id="demo-grid" class="vista-grid">
                            <div class="titlebar">
                               MANUFACTURE ENTRY LIST
                            </div>
                            <table class="datatable" cellpadding="0" cellspacing="0">
                                <tr class="header">
                                    <th>
                                      EDIT
                                    </th>
                                      <th>
                                      M Code
                                    </th>  
                                    <th>
                                      Manufacturer Name
                                    </th>  
                                    <th>
                                     Address
                                    </th>  
                                    <th>
                                      Contact Person
                                    </th>                           
                                       </tr>
                                       <tr id="itemPlaceholder" runat="server" />
                                  </table>
                             </div>
                         </LayoutTemplate>
                 <ItemTemplate>
                                               <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                   <td>
                                                     <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("MNO")%>'
                                                      AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />

                                                     <td>
                                                        <%# Eval("MCODE")%>
                                                    </td>
                                                         <td>
                                                        <%# Eval("MNAME")%>
                                                    </td>
                                                     <td>
                                                        <%# Eval("ADDRESS")%>
                                                    </td> 
                                                         <td>
                                                       <%# Eval("CONT_PERSON")%>
                                                    </td>                                                                                                    
                                                </tr>
                                            </ItemTemplate>
                                             <AlternatingItemTemplate>
                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("MNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />                                                    
                                                    </td>
                                                   <td>
                                                        <%# Eval("MCODE")%>
                                                    </td>
                                                         <td>
                                                        <%# Eval("MNAME")%>
                                                    </td>
                                                     <td>
                                                        <%# Eval("ADDRESS")%>
                                                    </td> 
                                                         <td>
                                                       <%# Eval("CONT_PERSON")%>
                                                    </td>             
                                                </tr>
                            </AlternatingItemTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>

