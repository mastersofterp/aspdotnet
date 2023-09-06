<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudBiodata.aspx.cs" Inherits="TRAININGANDPLACEMENT_Reports_StudBiodata"
    Title="" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                STUDENTS BIODATA &nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" 
                    meta:resourcekey="btnHelpResource1" />
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
                            ToolTip="Close" 
                            Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" 
                            meta:resourcekey="btnCloseResource1" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <%--<asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record--%>
                            <%--<asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" 
                                meta:resourcekey="lblHelpResource1" /></p>
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

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" 
                    TargetControlID="btnHelp" Enabled="True">
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
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" 
                    TargetControlID="btnClose" Enabled="True">
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

                <%--<script src="../../JAVASCRIPTS/jquery-1.5.1.js" type="text/javascript"></script>--%>
                <script language="javascript" type="text/javascript">
                    //                    $(document).ready(function() {
                    //                        $("#<%=radlStudentType.ClientID%>").change(function() {
                    //                            var rbvalue = $("input[name='<%=radlStudentType.UniqueID%>']:radio:checked").val();
                    //                            alert(rbvalue);
                    //                            if (rbvalue == "Confirm") {
                    //                                alert("Woohoo, Thanks!");
                    //                            } else if (rbvalue == "Postpone") {
                    //                                alert("Well, I really hope it's soon");
                    //                            } else if (rbvalue == "Decline") {
                    //                                alert("Shucks!");
                    //                            } else {
                    //                                alert("How'd you get here? Who sent you?");
                    //                            }
                    //                        });
                    //                    });
                    //                    function radioClick() {
                    //                        $('#<%=radlStudentType.ClientID%> input[type="radio"]').each(function() {
                    //                        alert((this).value);
                    //                            $(this).click(function() {
                    //                                alert((this).value);
                    //                            });
                    //                        });
                    //                    }
                </script>

            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Panel ID="pnlSelect" runat="server">
                    <div style="text-align: left; width: 87%; padding-left: 10px;">
                        <fieldset class="fieldsetPay" style="width: 662px">
                            <legend class="legendPay">Selection Criteria</legend>
                                    <table>
                                        <tr>
                                            <td class="form_left_text" colspan="2">
                                                <asp:RadioButtonList ID="radlStudentType" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="radlStudentType_SelectedIndexChanged" 
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="R" Selected="True">Regular</asp:ListItem>
                                                    <asp:ListItem Value="P">Pass out</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trBatch" runat="server" visible="False">
                                            <td class="form_left_label" runat="server">
                                                Batch :
                                            </td>
                                            <td class="form_left_text" runat="server">
                                                <asp:DropDownList ID="ddlBatch" runat="server" Width="250px" AppendDataBoundItems="True"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="refBatch" runat="server" ControlToValidate="ddlBatch"
                                                    InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select Batch" ValidationGroup="Select"
                                                    Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                Degree :
                                            </td>
                                            <td class="form_left_text">
                                                <asp:DropDownList ID="ddlDegree" runat="server" Width="250px" AppendDataBoundItems="True"
                                                    AutoPostBack="True" 
                                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                    InitialValue="0" SetFocusOnError="True" 
                                                    ErrorMessage="Please Select Degree" ValidationGroup="Select"
                                                    Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                Branch :
                                            </td>
                                            <td class="form_left_text">
                                                <asp:DropDownList ID="ddlBranch" runat="server" Width="250px" AppendDataBoundItems="True"
                                                    AutoPostBack="True" 
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                    InitialValue="0" SetFocusOnError="True" 
                                                    ErrorMessage="Please Select Branch" ValidationGroup="Select"
                                                    Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                Student Name :
                                            </td>
                                            <td class="form_left_text">
                                                <asp:DropDownList ID="ddlStudName" runat="server" Width="250px" 
                                                    AppendDataBoundItems="True">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlStudName"
                                                    InitialValue="0" SetFocusOnError="True" 
                                                    ErrorMessage="Please Select Student" ValidationGroup="Select"
                                                    Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_button" colspan="2" align="center">
                                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Select" OnClick="btnShow_Click"
                                                    Width="80px" />
                                                &nbsp;<asp:Button ID="btnCan" runat="server" Text="Cancel" OnClick="btnCan_Click"
                                                    Width="100px" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Select"
                                                    ShowMessageBox="True" ShowSummary="False" DisplayMode="List" 
                                                     />
                                            </td>
                                        </tr>
                                    </table>
                        </fieldset>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
