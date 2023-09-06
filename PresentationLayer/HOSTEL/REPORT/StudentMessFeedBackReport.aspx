<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentMessFeedBackReport.aspx.cs" Inherits="ACADEMIC_StudentMessFeedBackReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="1" cellspacing="1" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                STUDENT MESS FEEDBACK REPORT
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
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
    </table>
    <%--<asp:UpdatePanel ID="updFeed" runat="server">
        <ContentTemplate>--%>
    <table cellpadding="1" cellspacing="1" width="98%">
        <tr>
            <td>
                <fieldset class="fieldset1">
                    <legend class="legend">Filters</legend>
                    <table cellpadding="1" cellspacing="1" width="100%">
                        <tr>
                            <td style="width: 97px" class="form_left_label">
                                Session :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                    ToolTip="Please Select Session" Width="250px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 97px" class="form_left_label">
                                Degree :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    TabIndex="2" ToolTip="Please Select Degree" Width="250px" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 97px" class="form_left_label">
                                Branch :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    TabIndex="3" ToolTip="Please Select Branch" Width="250px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 97px" class="form_left_label">
                                Semester :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    TabIndex="5" ToolTip="Please Select Semester" Width="250px" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvsem" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                           <td style="width: 97px" class="form_left_label">
                                Hostel Name :</td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlHostelNo" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" Width="250px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelNo" runat="server" ErrorMessage="Please Select Hostel Name"
                                    ControlToValidate="ddlHostelNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </td>
                        </tr>
                        <tr style="display: none" >
                            <td style="width: 97px" class="form_left_label">
                                Feedback :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlfeedback" runat="server" AppendDataBoundItems="True" TabIndex="5"
                                    ToolTip="Please Select Course" Width="250px">
                                    <%--AutoPostBack="True"--%>
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                        </tr>
                        <tr style="display: none" >
                            <td class="form_left_label">
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rdoReport" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">FeedBack Count</asp:ListItem>
                                    <asp:ListItem Value="2">FeedBack Percentage</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>
                                Total Student :
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotChk" runat="server" ValidationGroup="courseLink" Width="30px"
                                    Enabled="False" />&nbsp<asp:Label ID="lblStud" runat="server"></asp:Label><asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <table cellpadding="2" cellspacing="2" width="100%">
                   
                    <tr>
                        <td style="width: 97px" class="form_left_label">
                        </td>
                        <td align="center">
                            <asp:Button ID="btnnmessfeedback" runat="server" Text="Mess FeedBack"  onclick="btnnmessfeedback_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                          </td>
               
                    <td style="width: 97px" class="form_left_label">
                        </td>
                        <td align="center">
                        
                       </td>
                            </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

    <script language="javascript" type="text/javascript">
        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

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

            if (headchk.checked == true)
                txtTot.value = hdfTot.value;
            else
                txtTot.value = 0;
        }
        function hidebutton() 
        {
            
        } 	
    </script>

</asp:Content>
