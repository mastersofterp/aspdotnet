<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ArchiveReport.aspx.cs" Inherits="ACADEMIC_REPORTS_ArchiveReport" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
               Faculty Archive Report
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
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
<%--<script type="text/javascript">

    window.onload = function() {
        var script = document.createElement("script");
        script.type = "text/javascript";
        script.src = "http://jsonip.appspot.com/?callback=DisplayIP";
        document.getElementsByTagName("head")[0].appendChild(script);
    };
    function DisplayIP(response) {
        document.getElementById("ipaddress").innerHTML = "Your Public IP Address is " + response.ip;
    } 

</script>--%>

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
    <table cellpadding="2" cellspacing="2" style="width: 100%">
        <tr>
            <td style="padding-top:5px; width:100%" >
                <fieldset class="fieldset">
                    <legend class="legend">Selection Criteria</legend>
                    <table style="width: 100%" cellpadding="2" cellspacing="2" width="100%">
                    <tr id="trFaculty" runat="server" visible="false">
                                                      
                                                        
                                                           
                                                            <td  style="width: 13%">
                                                                Faculty :
                                                            </td>
                                                            <td >
                                                                <asp:DropDownList ID="ddlFaculty" runat="server" Width="30%" TabIndex="2" 
                                                                    AppendDataBoundItems="True" AutoPostBack="true"
                                                                    ValidationGroup="auto" ToolTip="faculty" 
                                                                    onselectedindexchanged="ddlFaculty_SelectedIndexChanged"  >
                                                                    <asp:ListItem>Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            <%--    <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty" 
                                                                    Display="None" ErrorMessage="Please Select Faculty" ValidationGroup="fac" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                   
                                                        </tr>
                        <tr>
                            <td style="width: 13%">
                                Session :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSession" runat="server" Width="28%" 
                                    AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" 
                                    onselectedindexchanged="ddlSession_SelectedIndexChanged"> 
                                    
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report">--%></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       <tr>
                       <td>
                       &nbsp;
                       </td>
                       </tr>
                       <tr>
                       <td colspan="2">
                       <asp:RadioButton ID="CHK1" runat="server" Text="MarkSheet With Grade" GroupName="Chklist" Checked="true" Visible="false" />
                       <br />
                       <asp:RadioButton ID="CHK2" runat="server" Text="MarkSheet" GroupName="Chklist" Visible="false" />
                       </td>
                       </tr>
                    </table>
            </fieldset>
               
                   
                         <asp:Panel ID="pnlCourse" runat="server" Visible="false" >
            <fieldset class="fieldset">
               
                            <asp:ListView ID="lvCourse" runat="server" 
                                >
                                <LayoutTemplate>
                                    <div id="listViewGrid" class="vista-grid">
                                        <div class="titlebar">
                                           Details
                                        </div>
                                        <table id="tblSearchResults" cellpadding="0" cellspacing="0" class="datatable" width="50%">
                                            <tr class="header">
                                            <th>
                                        
                                         <asp:CheckBox ID="chkheader" runat="server"  onclick ="return totAll(this);"/>Course Name
                                    </th>
                                                
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                        <td  >
                                          <asp:CheckBox ID="chk1" runat="server" Text='<%# Eval("COURSENAME")%>' ForeColor="Green" Font-Bold="true"   ToolTip='<%# Eval("COURSENO")%>'  >
                                          </asp:CheckBox >
                                        </td>
                                        
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            </fieldset>
                           </asp:Panel> 
                   <table  style="width: 100%" >
                   
                        <tr id="tblButton" runat="server" visible="false">                 
                            <td style="width:20%">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                            </td>
                            <td>
                                &nbsp;<asp:Button ID="btnReport2" runat="server" Text="Show Report" 
                                ValidationGroup="report" onclick="btnReport2_Click"  />
                                
                                &nbsp;<asp:Button ID="btnFeedbackCount" runat="server" 
                                    Text="Feedback Count Report" onclick="btnFeedbackCount_Click" Width="165px" 
                                 />
                                
                                   &nbsp;&nbsp;<asp:Button ID="btnTimeTable" runat="server" Text="Class Time Table" 
                                ValidationGroup="report" onclick="btnTimeTable_Click"   />&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                    onclick="btnCancel_Click"  />
                                </td>
                        </tr>            
                        </table>    
               
           
    <div id="divMsg" runat="server">
    </div>

<script language="javascript" type="text/javascript">
        function totAll(headchk) {
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
    </asp:Content>

