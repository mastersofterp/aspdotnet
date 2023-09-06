<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Fees_Collect_Report1.aspx.cs" Inherits="CourseWise_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="99%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                &nbsp;FEES REPORT
                <!-- Button used to launch the help (animation) -->
                <%--<asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />--%>
            </td>
        </tr>
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/Images/delete.png" AlternateText="Delete Record" />
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
        <tr >
            <td style="padding-left: 15px;">
                <fieldset class="fieldset">
                    <legend class="legend">Fees Report</legend>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" width="90%">
                                <tr>
                                    <td class="form_left_label" style="width:20%">
                                        Session :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            Width="300px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlSession" 
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </td>
                                      <td rowspan="8" style="vertical-align: top; text-align: left; width: 25%">
                          
                                <br />
                                <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                            </td>
                                </tr>
                                <tr style="display:none">
                                    <td class="form_left_label">
                                        Degree :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            Width="300px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td class="form_left_label">
                                        Branch :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                             Width="300px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">
                                        Semester:
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            ValidationGroup="Show" Width="300px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">
                                        &nbsp;</td>
                                    <td class="form_left_text">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">
                                        Report :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:RadioButtonList ID="rdbReport" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbReport_SelectedIndexChanged"
                                            AutoPostBack="true" Width="100%" RepeatColumns="2">
                                            <asp:ListItem Selected="True" Value="1">DD Student List</asp:ListItem>
                                            <asp:ListItem Value="2">SBI Collect Student List</asp:ListItem>
                                            <asp:ListItem Value="3">Cash Student List</asp:ListItem>
                                            
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table cellpadding="2" cellspacing="2" width="90%">
                        <tr>
                            <td align="center">
                                <%-- <asp:Button ID="btnShow" runat="Server" Text="Show Data" ValidationGroup="Show" OnClick="btnShow_Click"
                                    Width="150px" />--%>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnReport" runat="Server" Text="Report" ValidationGroup="Show" OnClick="btnReport_Click"
                                    Width="150px" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnExcel" runat="Server" Text="Excel Report" ValidationGroup="Show" 
                                    Width="150px" onclick="btnExcel_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="150px" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" width="90%">
        <tr>
            <td style="padding-left: 15px;">
                &nbsp;</td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
    <script language="javascript" type="text/javascript">
        function onreport()
        {
            
            var a=document.getElementById("ctl00_ContentPlaceHolder1_rdbReport_4");
            if(a.checked)
            {
            document.getElementById("ctl00_ContentPlaceHolder1_rfvDegree").enabled=false;
            document.getElementById("ctl00_ContentPlaceHolder1_rfvBranch").enabled=false;
            document.getElementById("ctl00_ContentPlaceHolder1_rfvProgram").enabled=false;
            document.getElementById("ctl00_ContentPlaceHolder1_rfvSemester").enabled=false;
            }
        }
    </script>
</asp:Content>
