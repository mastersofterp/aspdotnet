<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="R_CreateUser.aspx.cs" Inherits="Estate_R_CreateUser" Title="Create User" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table cellpadding="0" cellspacing="0" width="70%" >
     <tr>
        <td class="vista_page_title_bar" valign="top" style="height:30px">
            CREATE REPAIR & MAINTENANCE USER
            <!-- Button used to launch the help (animation) -->
            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;" AlternateText="Page Help" ToolTip="Page Help" />
        </td>
     </tr>
      <%--PAGE HELP--%>
    <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
    <tr>
         <td>                           
            <!-- "Wire frame" div used to transition from the button to the info panel -->
            <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;"></div>
        
            <!-- Info panel to be displayed as a flyout when the button is clicked -->
            <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                    <asp:LinkButton id="btnClose" runat="server" OnClientClick="return false;" Text="X" ToolTip="Close"
                        Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                </div>
                <div>
                    <p class="page_help_head">
                       <span style="font-weight:bold;text-decoration: underline;">Page Help</span><br />
                       <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" /> Edit Record&nbsp;&nbsp;
                       <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" /> Delete Record
                    </p>
                    <p class="page_help_text"><asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
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
            
            <ajaxToolkit:AnimationExtender id="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
            </ajaxToolkit:AnimationExtender>
            <ajaxToolkit:AnimationExtender id="CloseAnimation" runat="server" TargetControlID="btnClose">
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
            </ajaxToolkit:AnimationExtender>
        </td>
    </tr>

    <tr>
       <td>
            <asp:Panel ID="pnlAdd" runat="server">
               <table cellpadding="0" cellspacing="0" style="width:100%;">
                 <tr> 
                    <td class="form_left_label">Department :</td>
                    <td class="form_left_text">
                    <asp:DropDownList ID="ddlRMDept"  AppendDataBoundItems="true"
                        runat="server"  Width="300px" 
                            onselectedindexchanged="ddlRMDept_SelectedIndexChanged"  AutoPostBack="true">
                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" 
                        ControlToValidate="ddlRMDept" Display="None" ErrorMessage="Select Department" 
                        ValidationGroup="complaint" InitialValue="-1"></asp:RequiredFieldValidator>
                </td> 
            </tr>
           
           <tr>
               <td class="form_left_label">Employee :</td>
                <td class="form_left_text">
                    <asp:DropDownList ID="ddlRMEmp"  AppendDataBoundItems="true"
                        runat="server"  Width="300px"  AutoPostBack="true"
                        onselectedindexchanged="ddlRMEmp_SelectedIndexChanged" >
                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEmp" runat="server" 
                        ControlToValidate="ddlRMEmp" Display="None" ErrorMessage="Select Employee" 
                        ValidationGroup="complaint" InitialValue="-1"></asp:RequiredFieldValidator>
                </td> 
            </tr>
            
            <tr>
               <td class="form_left_label">Entry For :</td>
                <td class="form_left_text">
                    <asp:DropDownList ID="ddlRMentryfor"  AppendDataBoundItems="true" runat="server"  Width="300px" >                        
                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEntryfor" runat="server" 
                        ControlToValidate="ddlRMentryfor" Display="None" ErrorMessage="Select Entry For" 
                        ValidationGroup="complaint" InitialValue="-1"></asp:RequiredFieldValidator>
                </td> 
            </tr>
                
            <tr>     
                <td class="form_left_label" style="padding-top:10px; padding-bottom:10px" colspan="2">
                   <asp:CheckBox ID="ChkRMAdmin" runat="server" AutoPostBack="true" OnCheckedChanged="ChkRMAdmin_CheckedChanged"/> 
                        <asp:Label ID="lblchk" runat="server" CssClass="form_left_label"></asp:Label>
                </td>
               
              </tr>
            
            <tr>               
               <td class="form_button" style="padding-top:10px; padding-bottom:10px" colspan="2">
                  <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="complaint" 
                       OnClick="btnSave_Click" Width="60px"/>
                  &nbsp;
                  <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" 
                       OnClick="btnCancel_Click" Width="60px" />
                  <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ValidationGroup="complaint" ShowMessageBox="true" ShowSummary="false" DisplayMode="List"/>
               </td>
            </tr>    
       </table>
      </asp:Panel>
        </td>
    
    </tr>
    
       <tr>
        <td colspan="2">
            <asp:Panel ID="pnlList" runat="server">
                <table cellpadding="0" cellspacing="0" style="width:100%;text-align:center">
                    <tr>
                        <td style="text-align:left; padding-left: 50px; padding-top: 10px;"> 
                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                         <td  align="center">
                            
                                   <asp:ListView ID="lvCreateUser" runat="server"> 
                                    <EmptyDataTemplate><br/> Click Add To Create Users</EmptyDataTemplate>
                                     <LayoutTemplate>
                                            <div id="demo-grid" class="vista-grid">
                                                <div class="titlebar">User Details</div>
                                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr class="header">
                                                        <th>Action</th>
                                                        <th>Admin Name</th>
                                                        <th>Emloyee Name</th>
                                                         <th>Status</th>
                                                        <th>Department</th>                                
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" /> 
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        
                                        <ItemTemplate>
                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                   
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("C_NO")%>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click"/>&nbsp;                                                    
                                                </td>
                                                                                                
                                                <td><%# Eval("Admin_Name")%></td>                                                
                                                <td><%# Eval("Employee_name")%></td>
                                                <td><%# Eval("C_STATUS")%></td>
                                                <td><%# Eval("DEPTNAME")%></td>
                                            
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                   <div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvCreateUser" PageSize="20" OnPreRender="dpPager_PreRender">
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
</asp:Content>