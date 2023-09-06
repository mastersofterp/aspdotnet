<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="GradeAllotment.aspx.cs" Inherits="ACADEMIC_EXAMINATION_GradeAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeAllotment"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; background-color: Aqua; padding-left: 5px">
                    <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                GRADE ALLOTMENT
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
                <asp:UpdatePanel ID="updGradeAllotment" runat="server">
                    <ContentTemplate>
                        <fieldset class="fieldset">
                            <legend class="legend">Grade Allotment</legend>
                            <table style="width: 100%;" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        Session :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Width="40%"
                                            Font-Bold="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td rowspan="9" style="vertical-align: top">
                                        <asp:ListView ID="lvGrades" runat="server">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        Grades</div>
                                                    <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                        <thead>
                                                            <tr class="header">
                                                                <th>
                                                                    Grade
                                                                </th>
                                                                <th>
                                                                    Min. Marks
                                                                </th>
                                                                <th>
                                                                    Max. Marks
                                                                </th>
                                                                <th>
                                                                    Tot. Students
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </thead>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#BBFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <%# Eval("GRADE_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MINMARK") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MAXMARK")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TOTAL_STU")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Degree :
                                    </td>
                                    <td style="width: 62%">
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" Width="90%"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="5">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Branch :
                                    </td>
                                    <td style="width: 62%">
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" Width="90%"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="6">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Scheme :
                                    </td>
                                    <td style="width: 62%;">
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" Width="90%"
                                            AutoPostBack="True" TabIndex="7" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Semester :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            Width="200px" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Course :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCourse" runat="server" Width="90%" AppendDataBoundItems="true"
                                            TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Section :
                                    </td>
                                    <td><%--section commented on 07-05-2012 as Grade allotment is alwayz to all students in common.--%>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" Enabled="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ErrorMessage="Please Select Section"
                                            ControlToValidate="ddlSection" Display="None" ValidationGroup="show" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        For Old Grade Allotment : Min. Marks :
                                        <asp:TextBox ID="txtMinMarks" runat="server" Font-Bold="True" Style="text-align: center"
                                            Width="30px" />
                                        <asp:RequiredFieldValidator ID="rfvMinMarks" runat="server" ControlToValidate="txtMinMarks"
                                            Display="None" ErrorMessage="Please Enter Min. Marks" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtMinMarks"
                                            Display="None" ErrorMessage="Please Enter Numeric Value" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Integer" ValidationGroup="Submit"></asp:CompareValidator>
                                        &nbsp;&nbsp;&nbsp; Max. Marks :
                                        <asp:TextBox ID="txtMaxMarks" runat="server" Font-Bold="True" Style="text-align: center"
                                            Width="30px" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaxMarks"
                                            Display="None" ErrorMessage="Please Enter Max. Marks" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtMaxMarks"
                                            Display="None" ErrorMessage="Please Enter Numeric Value" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Integer" ValidationGroup="Submit"></asp:CompareValidator>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        For New Grade Allotment :&nbsp;Offset :
                                        <asp:TextBox ID="txtOffset" runat="server" Font-Bold="True" MaxLength="3" Style="text-align: center"
                                            Width="30px">0</asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtOffset"
                                            Display="None" ErrorMessage="Please Enter Numeric Value" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Integer" ValidationGroup="Submit"></asp:CompareValidator>
                                        &nbsp; Analysis by Factor :
                                        <asp:TextBox ID="txtFactor" runat="server" Font-Bold="True" MaxLength="3" Style="text-align: center"
                                            Width="30px">0</asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtFactor"
                                            Display="None" ErrorMessage="Please Enter Numeric Value" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Integer" ValidationGroup="Submit"></asp:CompareValidator>
                                            &nbsp;&nbsp;<br />
                                            <asp:Label ID ="lblStudents" runat="server" Font-Bold="true" ></asp:Label>&nbsp;&nbsp;<br />
                                        <asp:Label ID="lblMarksNotEnteredStudents" runat="server" Font-Bold="true"></asp:Label> 

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <fieldset class="fieldset" style="text-align: center;padding:10px">
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                            &nbsp;
                                            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" TabIndex="12"
                                                Text="Show Top 10 Min./Max. Marks Range" Width="250px" Visible="False" />
                                            &nbsp;
                                            <asp:Button ID="btnReport2" runat="server" OnClick="btnReport2_Click" TabIndex="13"
                                                Text="Show All Marks Range" Width="200px" Visible="False" />
                                            &nbsp;
                                            <asp:Button ID="btnGraph" runat="server" OnClick="btnGraph_Click" TabIndex="13" Text="Graph"
                                                Width="80px" Visible="False" />
                                            &nbsp;
                                            <asp:Button ID="btnLock" runat="server" OnClick="btnLock_Click" TabIndex="13" Text="Lock"
                                                Visible="False" Width="80px" />
                                            <br /> <br />
                                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="11" 
                                                Text="Show Students" Width="110px" />
                                            &nbsp;
                                            <asp:Button ID="btnSubmit" runat="server" Text="Allot Grade (Old)" ValidationGroup="Submit"
                                                Width="120px" OnClick="btnSubmit_Click" TabIndex="11" />
                                            &nbsp;
                                            <asp:Button ID="btnSubmit2" runat="server" Text="Allot Grade (New)" Width="120px"
                                                TabIndex="11" OnClick="btnSubmit2_Click" />
                                            &nbsp;
                                            <asp:Button ID="btnAnalysis" runat="server" Text="Analysis" Width="120px" TabIndex="11"
                                                OnClick="btnAnalysis_Click" Visible="False" />
                                            &nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click"
                                                TabIndex="12" />
                                                <asp:Timer ID="timUpdate" runat="server" Enabled="False" Interval="1000" OnTick="timUpdate_Tick1">
                                        </asp:Timer>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>


                                    <td colspan="3" style="width: 10%">
                                        <table cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr>
                                                <td style="width: 70%; vertical-align: top">
                                                    <asp:ListView ID="lvAnalysis" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    Analysis</div>
                                                                <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                                    <thead>
                                                                        <tr class="header">
                                                                            <th>
                                                                                Section/Roll. No.
                                                                            </th>
                                                                            <th>
                                                                                TAE
                                                                            </th>
                                                                            <th>
                                                                                CAE
                                                                            </th>
                                                                            <th>
                                                                                Grade
                                                                            </th>
                                                                            <th>
                                                                                MinMark
                                                                            </th>
                                                                        </tr>
                                                                        <tr ID="itemPlaceholder" runat="server" />
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" 
                                                                    onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                    <td>
                                                                        <%# Eval("SECTIONNAME") %> / <%# Eval("ROLL_NO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TAE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("CAE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("GRADE_NAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("MINMARK") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </td>
                                                    <td style="width: 30%; vertical-align: top">
                                                        <asp:ListView ID="lvGrade" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <div class="titlebar">
                                                                        Grade</div>
                                                                    <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                                        <thead>
                                                                            <tr class="header">
                                                                                <th>
                                                                                    Grade
                                                                                </th>
                                                                                <th>
                                                                                    Min Mark
                                                                                </th>
                                                                                <th>
                                                                                    Count
                                                                                </th>
                                                                            </tr>
                                                                            <tr ID="itemPlaceholder" runat="server" />
                                                                            </thead>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" 
                                                                        onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                        <td>
                                                                            <%# Eval("GRADE_NAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MINMARK") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("COUNT")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                                                            <tr>
                                        <td colspan="3" style="padding-top: 20px; text-align: center">
                                            <div style="width: 100%; text-align: center">
                                                <asp:ListView ID="lvMarksNotEntered" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <div class="titlebar">
                                                                Marks Not Entered Students
                                                            </div>
                                                            <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                                                <tr class="header">
                                                                    <th>
                                                                        REGNO.
                                                                    </th>
                                                                    <th>
                                                                        SECTION.
                                                                    </th>
                                                                    <th>
                                                                        ROLL NO.
                                                                    </th>
                                                                    <th>
                                                                        CCODE
                                                                    </th>
                                                                    <th>
                                                                        COURSE_NAME
                                                                    </th>
                                                                    <th>
                                                                        TU
                                                                    </th>
                                                                    <th>
                                                                        TAE
                                                                    </th>
                                                                    <th>
                                                                        CAE
                                                                    </th>
                                                                    <th>
                                                                        PU
                                                                    </th>
                                                                    <th>
                                                                        PI
                                                                    </th>
                                                                    <th>
                                                                        GRADE
                                                                    </th>
                                                                    <th>
                                                                        BUNDLE
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                            <td>
                                                                <asp:HiddenField ID="hdfIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                                                <%# Eval("REGNO")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SECTION")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ROLL_NO")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("CCODE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("COURSE_NAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("TU")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("TAE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("CAE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("PU")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("PI")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("GR")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BUNDLENO")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </td>
                                    </tr>

                                        <tr>
                                            <td colspan="3" style="padding-top: 10px">
                                                <asp:ListView ID="lvStudent" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <div class="titlebar">
                                                                Student Marks/Grade List</div>
                                                            <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                                <thead>
                                                                    <tr class="header">
                                                                        <th>
                                                                            Section/Roll. No.
                                                                        </th>
                                                                        <th>
                                                                            Name
                                                                        </th>
                                                                        <th>
                                                                            Marks
                                                                        </th>
                                                                        <th>
                                                                            Grade
                                                                        </th>
                                                                    </tr>
                                                                    <tr ID="itemPlaceholder" runat="server" />
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" 
                                                                onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                <td>
                                                                    <%# Eval("SECTIONNAME") %> / <%# Eval("ROLL_NO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("STUDNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MARKTOT") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("GRADE") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </td>
                                            </tr>
                            </table>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
