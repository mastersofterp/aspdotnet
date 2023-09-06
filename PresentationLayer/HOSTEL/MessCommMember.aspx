<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MessCommMember.aspx.cs" Inherits="HOSTEL_MessCommMember" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
                Hostel Mess Committee Member
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Shrink the info panel out of view --%>
        <%--  Reset the sample so it can be played again --%>
        <tr>
            <td colspan="2">
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
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                            1. Uncheck Indicate Absent<br>
                            2. Check Indicate Present
                        </p>
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
    <div>
        <fieldset class="fieldset">
            <legend class="legend">Selection criteria</legend>
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td style="width: 108px">
                    </td>
                    <td style="width: 136px">
                        Hostal Session :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Hostel Session"
                            Width="200px" AutoPostBack="True">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                            ControlToValidate="ddlSession" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"
                            Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 108px">
                    </td>
                    <td>
                        Hostel :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlHostel" runat="server" Width="200px" TabIndex="1" 
                            AppendDataBoundItems="True" AutoPostBack="True" 
                            onselectedindexchanged="ddlHostel_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                            Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="Show" SetFocusOnError="True"
                            InitialValue="0" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 108px">
                    </td>
                    <td>
                        Mess Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMess" runat="server" Width="200px" TabIndex="1" AppendDataBoundItems="True"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlMess_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator ID="rfvMess" runat="server" ControlToValidate="ddlMess"
                            Display="None" ErrorMessage="Please Select Mess" ValidationGroup="Show" SetFocusOnError="True"
                            InitialValue="0" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="width: 108px"></td>
                    <td colspan="4">
                    
                   
            <fieldset class="fieldset" >
                <legend class="legend">Add Commitee member</legend>
                <table>
                    <tr>
                        <td style="width: 110px">
                            Member 1 :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMember1" runat="server" Width="200px" TabIndex="1" AppendDataBoundItems="True"
                                >
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvMember1" runat="server" ControlToValidate="ddlMember1"
                                Display="None" ErrorMessage="Please Select Member1" ValidationGroup="Show" SetFocusOnError="True"
                                InitialValue="0" />
                        </td>
                        <td>
                        </td>
                        <td style="width: 88px">
                            Member 2 :
                        </td>
                        <td style="width: 213px">
                            <asp:DropDownList ID="ddlMember2" runat="server" Width="200px" TabIndex="1" AppendDataBoundItems="True"
                                
                                >
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvMember2" runat="server" ControlToValidate="ddlMember2"
                                Display="None" ErrorMessage="Please Select Member2" ValidationGroup="Show" SetFocusOnError="True"
                                InitialValue="0" />
                        </td>
                    </tr>
                    <tr>
                       
                        <td style="width: 110px">
                            Member 3 :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMember3" runat="server" Width="200px" TabIndex="1" AppendDataBoundItems="True"
                                
                                >
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvMember3" runat="server" ControlToValidate="ddlMember3"
                                Display="None" ErrorMessage="Please Select Member3" ValidationGroup="Show" SetFocusOnError="True"
                                InitialValue="0" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 88px">
                            Member 4:
                        </td>
                        <td style="width: 213px">
                            <asp:DropDownList ID="ddlMember4" runat="server" Width="200px" TabIndex="1" AppendDataBoundItems="True"
                                
                                >
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvMember4" runat="server" ControlToValidate="ddlMember4"
                                Display="None" ErrorMessage="Please Select Member4" ValidationGroup="Show" SetFocusOnError="True"
                                InitialValue="0" />
                        </td>
                    </tr>
                    <tr>
                       
                        <td style="width: 110px">
                            Member 5:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMember5" runat="server" Width="200px" TabIndex="1" AppendDataBoundItems="True"
                                 >
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvMember5" runat="server" ControlToValidate="ddlMember5"
                                Display="None" ErrorMessage="Please Select Member5" ValidationGroup="Show" SetFocusOnError="True"
                                InitialValue="0" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 88px">
                            &nbsp;
                        </td>
                        <td style="width: 213px">
                            &nbsp;
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="6">
                        </td>
                    </tr>
                </table>
            </fieldset>
             </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="width: 108px">
                        &nbsp;
                    </td>
                    <td style="width: 110px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" BackColor="#CCCCCC" OnClick="btnSubmit_Click"
                            Text="Submit" ValidationGroup="Show" />
                        &nbsp;<asp:Button ID="btnReport" runat="server" BackColor="#CCCCCC" Text="Report"
                            OnClick="btnReport_Click" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" BackColor="#CCCCCC" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 88px">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </fieldset>
        <table>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <asp:ListView ID="lvDetails" runat="server">
            <LayoutTemplate>
                <div id="demo-grid" class="vista-grid">
                    <div class="titlebar">
                        Student List</div>
                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                        <tr class="header">
                            <th>
                                Edit
                            </th>
                            <th>
                                Hostel
                            </th>
                            <th>
                                Mess
                            </th>
                            <th>
                                Member1
                            </th>
                            <th>
                                Member2
                            </th>
                            <th>
                                Member3
                            </th>
                            <th>
                                Member4
                            </th>
                            <th>
                                Member5
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'"
                    style="font-size: small">
                    <td style="width: 5%;">
                        <asp:ImageButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CommandArgument='<%# Eval("COMM_NO") %>'
                            ImageUrl="~/images/edit.gif" ToolTip='<%# Eval("COMM_NO") %>' />
                    </td>
                    <td>
                        <%# Eval("HOSTEL_NAME")%>
                    </td>
                    <td>
                        <%# Eval("MESS_NAME")%>
                    </td>
                    <td>
                        <%# Eval("MEMBER1")%>
                    </td>
                    <td>
                        <%# Eval("MEMBER2")%>
                    </td>
                    <td>
                        <%# Eval("MEMBER3")%>
                    </td>
                    <td>
                        <%# Eval("MEMBER4")%>
                    </td>
                    <td>
                        <%# Eval("MEMBER5")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
