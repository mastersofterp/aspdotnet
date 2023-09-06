<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CutOffStudent.aspx.cs" Inherits="HOSTEL_CutOffStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
                Cut Off For Hostel
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
    <div>
        <fieldset class="fieldset">
            <legend class="legend">Selection criteria</legend>
            <table cellpadding="0" cellspacing="2" width="100%">
                <tr style="display:none;">
                     <td style="width: 108px">
                    </td>
                    <td style="width: 136px" >
                        Selection :
                     </td>
                     <td> 
                        <asp:RadioButtonList ID="rdoSelect" runat="server" RepeatDirection="Horizontal" 
                             AutoPostBack="True" onselectedindexchanged="rdoSelect_SelectedIndexChanged">
                            <asp:ListItem Value="1" Selected="True">Generate Cut Off List</asp:ListItem>
                            <asp:ListItem Value="2">Fee Collection Receipt</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 108px">
                    </td>
                    <td style="width: 136px">
                        Hostal Session :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Hostel Session"
                            Width="250px">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                            ControlToValidate="ddlSession" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"
                            Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td rowspan="5" valign="top" align="left">
                        <fieldset class="fieldset">
                            <legend class="legend">Report</legend>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdlEligible" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="True">Non Eligible</asp:ListItem>
                                        <asp:ListItem Value="1">Eligible</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td >
                                       &nbsp;&nbsp;&nbsp; <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" ValidationGroup="Show" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Degree :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Degree"
                            Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Branch :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Hostel Session"
                            Width="250px">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Semester :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Hostel Session"
                            Width="250px">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr >
                <td></td>
                    <td colspan ="2">
                        
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                    </td>
                    <td>
                        <asp:Button ID="btnShow" runat="server" Text="Show Students" OnClick="btnShow_Click" ValidationGroup="Show" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Show" />
                        <asp:Button ID="btnMess" runat="server" Text="Mess Receipts" 
                            ValidationGroup="Show"  Visible="false" onclick="btnMess_Click"/>
                        <asp:Button ID="btnReceipt" runat="server" Text="Hostel Receipts" ValidationGroup="Show" onclick="btnReceipt_Click" Visible="false"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
                <tr id ="trnote" runat="server">
                <td></td>
                    <td colspan="2">
                        <span style="color:Red">* Note :</span>
                         Please select students who are not eligible for hostel and then Submit.
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div>
        <table cellpadding="0" cellspacing="0" width="95%">
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center" width="95%">
                    <asp:ListView ID="lvDetails" runat="server" Visible="false">
                        <LayoutTemplate>
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    Student List</div>
                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="header">
                                        <th style="width: 5%;">
                                            Select
                                        </th>
                                        <th style="width: 50%">
                                            Student Name
                                        </th>
                                        <th style="width: 20%">
                                            Enroll. No.
                                        </th>
                                        <th style="width: 30%">
                                            Roll No.
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td style="width: 5%;">
                                    <asp:CheckBox ID="chkIdno" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                </td>
                                <td style="width: 50%; text-transform: uppercase">
                                    <%# Eval("STUDNAME") %>
                                </td>
                                <td style="width: 20%">
                                    <%# Eval("ENROLLNO")%>
                                </td>
                                <td style="width: 30%">
                                    <%# Eval("ROLLNO")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server"></div>
</asp:Content>
