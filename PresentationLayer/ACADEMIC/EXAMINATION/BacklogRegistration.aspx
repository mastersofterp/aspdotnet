<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BacklogRegistration.aspx.cs" Inherits="ACADEMIC_EXAMINATION_BacklogRegistration"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">
                            BACKLOG REGISTRATION&nbsp;&nbsp
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                            <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp"
                                Enabled="True">
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC;
                    background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right;">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                        <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose"
                            Enabled="True">
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

            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset class="fieldset">
                    <legend class="legend">&nbsp;Backlog Registration</legend>
                    <table width="100%" cellpadding="2" cellspacing="2" border="0">
                        <tr>
                            <td style="width: 50px">
                            </td>
                            <td width="15%">
                                <span style="color: Red;">&nbsp</span> Session
                            </td>
                            <td style="width: 2px">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSession" runat="server" Width="140px" AppendDataBoundItems="True"
                                    OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                                    ControlToValidate="ddlSession" Display="None" SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px">
                            </td>
                            <td width="15%">
                                <span style="color: Red;">&nbsp;</span> Registration No
                            </td>
                            <td style="width: 2px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtEnrollmentNo" runat="server"></asp:TextBox>&nbsp;<asp:Button
                                    ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="Show" />
                                <asp:RequiredFieldValidator ID="rfvenroll" runat="server" ControlToValidate="txtEnrollmentNo"
                                    Display="None" ErrorMessage="Please Enter Registration No." InitialValue="" ValidationGroup="Show">
                                </asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" Height="16px" />
                            </td>
                            <td rowspan="2">
                                <asp:ListView ID="lvsession" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="titlebar">
                                                Session Details</div>
                                            <table class="datatable" cellpadding="0" cellspacing="0" width="15%">
                                                <tr class="header">
                                                    <th style="width: 5%">
                                                        Session Name
                                                    </th>
                                                    <th style="width: 20%">
                                                        Session No
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td style="width: 10%">
                                                <%#Eval("Session")%>
                                            </td>
                                            <td style="width: 5%">
                                                <%# Eval("sessionno")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px">
                            </td>
                            <td style="width: 2px">
                                :
                            </td>
                            <td>
                               
                            </td>
                            <td>
                             <asp:Label ID="lblname"  runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px">
                            </td>
                            <td width="15%">
                                <span style="color: Red;">&nbsp;</span> Scheme
                            </td>
                            <td style="width: 2px">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlScheme" runat="server" Width="190px" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px">
                            </td>
                            <td width="15%">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px">
                            </td>
                            <td width="15%">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <asp:Panel ID="pnlcourse" runat="server" Visible="false">
                                <tr>
                                    <td style="width: 50px">
                                    </td>
                                    <td width="15%">
                                        <span style="color: Red;">&nbsp; </span>Semester
                                    </td>
                                    <td style="width: 2px">
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSemester" runat="server" Width="190px" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50px">
                                    </td>
                                    <td width="15%">
                                        <span style="color: Red;">&nbsp; </span>Staus
                                    </td>
                                    <td style="width: 2px">
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlstatus" runat="server" Width="190px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50px">
                                    </td>
                                    <td width="15%">
                                        <span style="color: Red;">&nbsp; </span>Course
                                    </td>
                                    <td style="width: 2px">
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSubject" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                                            runat="server" Width="190px" AppendDataBoundItems="true" ValidationGroup="Add"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="Add"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" Height="16px" />
                                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="ddlSubject"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </tr>
                        <tr>
                            <td style="width: 50px">
                            </td>
                            <td style="width: 2px">
                            </td>
                            <td colspan="5">
                                <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Width="60px"
                                    Enabled="false" ValidationGroup="Add" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px">
                            </td>
                            <td align="center" colspan="5">
                                <asp:ListView ID="lvCourse" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="titlebar">
                                                Courses Details</div>
                                            <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                <tr class="header">
                                                    <th style="width: 6%">
                                                        Action
                                                    </th>
                                                    <th style="width: 5%">
                                                        CCODE
                                                    </th>
                                                    <th style="width: 20%">
                                                        Course Name
                                                    </th>
                                                    <th style="width: 14%">
                                                        Credits
                                                    </th>
                                                    <th style="width: 15%">
                                                        Semester
                                                    </th>
                                                    <th style="width: 10%">
                                                        Elective
                                                    </th>
                                                    <th style="width: 10%">
                                                        Status
                                                    </th>
                                                    <th style="width: 10%">
                                                        Th/Prac
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td style="width: 6%">
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText='<%# Eval("semesterno") %>'
                                                    ToolTip="Delete Record" CommandArgument='<%# Eval("COURSENO") %>' CausesValidation="False"
                                                    OnClick="btnDelete_Click" />
                                            </td>
                                            <td style="width: 5%">
                                                <%#Eval("CCODE")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("COURSENAME")%>
                                            </td>
                                            <td style="width: 14%">
                                                <%# Eval("CREDITS")%>
                                            </td>
                                            <td style="width: 15%">
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("ELECTIVE")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("STATUS")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("courseStatus")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                            <td style="width: 6%">
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("COURSENO") %>'
                                                    AlternateText='<%# Eval("semesterno") %>' ToolTip="Delete Record" CausesValidation="False" OnClick="btnDelete_Click" />
                                            </td>
                                            <td style="width: 5%">
                                                <%#Eval("CCODE")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("COURSENAME")%>
                                            </td>
                                            <td style="width: 14%">
                                                <%# Eval("CREDITS")%>
                                            </td>
                                            <td style="width: 15%">
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("ELECTIVE")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("STATUS")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("courseStatus")%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
