<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PracticalFacultyEntry.aspx.cs" Inherits="ACADEMIC_EXAMINATION_PRACTICALFACULTYENTRY"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../JAVASCRIPTS/jquery.min_1.js" language="javascript" type="text/javascript"></script>

    <script src="../../JAVASCRIPTS/jquery-ui.min_1.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" src="../../JAVASCRIPTS/AutoComplete.js"></script>

    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            //City
            $(function() {
                $(".tbFaculty").autocomplete({
                    source: function(request, response) {
                        $.ajax({
                            url: "../../WebService.asmx/GetData",
                            data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_PRAC_VALUER','col1': 'VALUER_UA_NO','col2': 'VALUER_UA_NAME','col3': '' }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function(data) { return data; },
                            success: function(data) {
                                response($.map(data.d, function(item) {
                                    return {
                                        value: item
                                    }
                                }))
                            },
                            error: function(XMLHttpRequest, textStatus, errorThrown) {
                                alert(errorThrown);
                            }
                        });
                    },
                    minLength: 1
                });
            });
        }
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                &nbsp;PRACTICAL FACULTY ENTRY
                <!-- Button used to launch the help (animation) -->
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
            <td style="padding-top: 5px; width: 100%">
                <asp:UpdatePanel ID="updValuer" runat="server">
                    <ContentTemplate>
                        <fieldset class="fieldset">
                            <legend class="legend">Practical Faculty Entry</legend>
                            <table style="width: 100%" width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>Session :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSession" runat="server" Width="15%" AppendDataBoundItems="true"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Term" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>Degree :</td>
                                    <td style="width: 40%; vertical-align: top;">
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" 
                                            AutoPostBack="True" onselectedindexchanged="ddlDegree_SelectedIndexChanged" 
                                            Width="50%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" 
                                            ControlToValidate="ddlDegree" Display="None" 
                                            ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True" 
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>Branch :
                                    </td>
                                    <td style="width: 40%; vertical-align: top;">
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" 
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" 
                                            Width="50%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" 
                                            ControlToValidate="ddlBranch" Display="None" 
                                            ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True" 
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>Scheme :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            Width="50%" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>Semester :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            Width="15%" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>Course :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCourse" runat="server" Width="50%" AppendDataBoundItems="true"
                                            TabIndex="4" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr runat="server" visible="false" id="trSection">
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>Section :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSection" runat="server" Width="15%" AppendDataBoundItems="true"
                                            TabIndex="4" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<asp:RequiredFieldValidator ID="rfvddlSection" runat="server" ControlToValidate="ddlSection"
                                            Enabled="false" Display="None" ErrorMessage="Please Select Section" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="report"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr id="trBatch" visible="false" runat="server">
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>Batch :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBatch" runat="server" Width="15%" AppendDataBoundItems="true"
                                            TabIndex="4" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                            Enabled="false" Display="None" ErrorMessage="Please Select Batch" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="report"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>Internal Faculty :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" Width="50%" AppendDataBoundItems="true"
                                            TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<asp:RequiredFieldValidator ID="rfvddlFaculty" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <span>&nbsp;</span>External Faculty :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExtFaculty" Width="50%" runat="server" class="tbFaculty"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtExtFaculty" runat="server" FilterType="Custom"
                                            InvalidChars="!@#$%^*()_+-\|';,./:?/" TargetControlID="txtExtFaculty" FilterMode="InvalidChars">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="report"
                                            Width="80px" OnClick="btnSubmit_Click" TabIndex="11"  />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click"
                                            TabIndex="12" />
                                        &nbsp; &nbsp;
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <%--   <table style="width: 80%" cellpadding="2" cellspacing="2" width="80%">
                    <tr>
                        <td colspan="2" style="vertical-align: top;" align="center">
                                <asp:Repeater ID="lvCourse" runat="server">
                                    <HeaderTemplate>
                                                <div id="demo-grid" class="vista-grid" style="width: 100%">
                                                    <div class="titlebar">
                                                        Valuer List</div>
                                                </div>
                                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                                    <thead>
                                                        <tr class="header">
                                                            <th style="width: 35%; text-align:left">
                                                                   Course Name
                                                                </th>
                                                                <th style="width: 20%; text-align:left">
                                                                    Valuer Name
                                                                </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                        <thead>
                                                            <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                        <td style="width: 20%; text-align:left">
                                                            <%# Eval("COURSE_NAME")%>
                                                        </td>
                                                        <td style="width: 15%; text-align:left">
                                                            <%# Eval("UA_FULLNAME")%>
                                                        </td>
                                                    </tr>
                                            </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody></table></FooterTemplate>
                                </asp:Repeater>
                        </td>
                    </tr>
                </table>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function totAllSubjects(headchk) {

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
