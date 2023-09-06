<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="EndSemMarkEntry.aspx.cs" Inherits="Academic_EndSemMarkEntry" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                MARK ENTRY(END SEM)
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
                            Edit Record
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
    <asp:Panel ID="pnlSelection" runat="server">
        <fieldset class="fieldset">
            <legend class="legend">Selection Criteria for Marks Entry</legend>
            <table style="width: 100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td style="width: 15%">
                        &nbsp;
                    </td>
                    <td class="form_left_text">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Session :
                    </td>
                    <td style="width: 60%">
                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                            Width="20%">
                        </asp:DropDownList>
                    </td>
                    
                </tr>
                <tr id="trdegree" runat="server" visible="false">
                    <td style="width: 15%">
                        Degree :
                    </td>
                    <td style="width: 60%">
                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" Width="25%"
                            AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                            ValidationGroup="show"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trbranch" runat="server" visible="false">
                    <td style="width: 15%">
                        Branch :
                    </td>
                    <td style="width: 60%">
                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" Width="50%"
                            AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                            ValidationGroup="show"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trscheme" runat="server" visible="false">
                    <td style="width: 15%">
                        Scheme :
                    </td>
                    <td style="width: 60%">
                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" Width="50%"
                            ValidationGroup="show" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                            ValidationGroup="show"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trSemester" runat="server" visible="false">
                    <td style="width: 15%">
                        Semester :
                    </td>
                    <td style="width: 60%">
                        <asp:DropDownList ID="ddlSemester" runat="server" Width="25%" AppendDataBoundItems="True"
                            AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                            ValidationGroup="show"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trExam" runat="server" visible="false">
                    <td style="width: 15%">
                        Exam :
                    </td>
                    <td style="width: 60%">
                        <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" Width="25%"
                            ValidationGroup="show" AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                            Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                            ValidationGroup="show"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 10px; text-align: center">
                        <asp:ListView ID="lvCourse" runat="server" >
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">
                                        Course List</div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="header">
                                            <th>
                                                Course Name
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td>
                                        <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>'
                                            CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO") %>' ToolTip='<%# Eval("COURSENO")%>'
                                            OnClick="lnkbtnCourse_Click"  />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td>
                                        <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>'
                                            CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO")%>' ToolTip='<%# Eval("COURSENO")%>'
                                            OnClick="lnkbtnCourse_Click"  />
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlMarkEntry" runat="server">
        <fieldset class="fieldset">
            <legend class="legend">Marks Entry</legend>
            <table style="width: 100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td style="padding: 5px; border: solid 1px gray;">
                        <table style="width: 100%" cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="width: 15%">
                                    Session :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSession2" runat="server" AppendDataBoundItems="true" Font-Bold="true">
                                    </asp:DropDownList>
                                </td>
                                <td rowspan="4" style="width: 2%; vertical-align: toP;height:6%">
                                    <%--<fieldset class="fieldset" style="padding: 5px; color: Green">
                                        <legend class="legend">Note</legend>Please Enter :<br />
                                        <b>401</b> for Absent<br />
                                        <b>403</b> for Copy Case (UFM)<br />
                                        <span style="font-weight: bold; color: Red;">Please Save and Lock for Final Submission
                                            of Marks</span>
                                    </fieldset>--%>
                          <asp:ListView ID="lvGrades" runat="server" >
                                        
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                
                                    <table id="lvGrades" class="datatable" cellpadding="0" cellspacing="0" width="10%" runat="server">
                                        <tr class="header">
                                            <th align="center">
                                                Grade
                                            </th>
                                            <th align="center">
                                                Max
                                            </th>
                                             <th align="center">
                                                Min
                                            </th>
                                             <th align="center">
                                               Point
                                            </th>
                                             <th align="center">
                                               Total
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td align="center">
                                        <asp:TextBox ID="txtGrades" runat="server" Text='<%# Bind("GRADE") %>' Font-Bold="true" Enabled="false" 
                                        Width="25px" Height="10px" Style="text-align: center" />
                                    </td>
                                    <td align="center">
                                       <asp:TextBox ID="txtMax" runat="server" Text='<%# Bind("MAXMARK") %>' Width="25px" Height="10px" 
                                                    Style="text-align: center"  onkeyUP="changeMaxMarksRange(this);" />
                                    </td>
                                    <td align="center">
                                       <asp:TextBox ID="txtMin" runat="server" Text='<%# Bind("MINMARK") %>' Width="25px" Height="10px"
                                                     Style="text-align: center" onkeyUP="changeMinMarksRange(this);" />
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtGradePoints" runat="server" Text='<%# Bind("GRADEPOINT") %>' Font-Bold="true" 
                                        Enabled="false" Height="10px" Width="25px"  Style="text-align: center" />
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtTotalStudent" runat="server" Text='<%# Bind("TOTAL_STU") %>' Font-Bold="true" 
                                       Enabled="false" Height="10px" Width="25px"  Style="text-align: center" />
                                       <asp:HiddenField ID="hidTotalStudent" runat="server" Value='<%# Bind("TOTAL_STU") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            </asp:ListView>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: top;">
                                    Course Name :
                                </td>
                                <td style="vertical-align: top">
                                    <asp:Label ID="lblCourse" runat="server" Font-Bold="true" />
                                    <asp:HiddenField ID="hdfSection" runat="server" />
                                    <asp:HiddenField ID="hdfBatch" runat="server" />
                                       <asp:HiddenField ID="hdfSchemeNo" runat="server" />
                                        <asp:HiddenField ID="hdfExamType" runat="server" />
                                         <asp:HiddenField ID="hdfSubid" runat="server" />
                                </td>
                            </tr>
                            <tr runat="server" id="trTitle" visible="false">
                                <td style="width: 15%; vertical-align: top;">
                                    Title :
                                </td>
                                <td style="vertical-align: top">
                                    <asp:TextBox runat="server" MaxLength="60" ID="txtTitle"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTitle" runat="server" FilterType="Custom"
                                        InvalidChars="!@#$%^&*()_+-=\|';,./:? /<>" TargetControlID="txtTitle">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: top;">
                                    &nbsp;
                                </td>
                                <td style="vertical-align: top">
                                    <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                    
                                </td>
                                
                              
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: top">
                                    &nbsp;
                                </td>
                                <td style="vertical-align: top">
                                    <asp:Button ID="btnBack" runat="server" Font-Bold="true" OnClick="btnBack_Click"
                                        Text="Back" Width="60px" />
                                    &nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" Enabled="false" Font-Bold="true"
                                        OnClick="btnSave_Click" Text="Save" Width="60px" />
                                    &nbsp;&nbsp;<asp:Button ID="btnLock" runat="server" Enabled="false" Font-Bold="true"
                                        OnClick="btnLock_Click" OnClientClick="return showLockConfirm();" Text="Lock"
                                        Width="60px" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel2" runat="server" Font-Bold="true" OnClick="btnCancel2_Click"
                                        Text="Cancel" Width="60px" Visible="False" />
                                    &nbsp;
                                    <asp:Button ID="btnReport" runat="server" Font-Bold="true" Text="Report" Width="60px"
                                        OnClick="btnReport_Click" />
                                        &nbsp;
                                    <asp:Button ID="btnExcelReport" runat="server" Font-Bold="true" 
                                        Text="Excel Report" Width="100px" onclick="btnExcelReport_Click"
                                       />
                                       &nbsp;
                                    <asp:Button ID="btnGraphReport" runat="server" Font-Bold="true" 
                                        Text="Graph Report" Width="102px" onclick="btnGraphReport_Click"
                                         />
                                        <br />
                                        <br />
                                        
                                        <fieldset class="fieldset" style="padding: 5px; color: Green">
                                        <legend class="legend">Note</legend>
                                        <b>401</b> for Absent<br />
                                        <b>402</b> for Withheld<br />
                                        <b>403</b> for Copy Case (UFM)<br />
                                        <span style="font-weight: bold; color: Red;">Please Save and Lock for Final Submission
                                            of Marks</span><br />
                                            <span style="font-weight: bold; color: Red;">After Change in Grade Range,Please Press Tab Button In End Sem Marks Column. </span>
                                    </fieldset>
                                </td>
                                
                            </tr>
                            <tr>
                            <td>
                            &nbsp;
                            </td>
                             <td>
                            &nbsp; 
                            </td>
                            
                                <td>
                                 Total Students :
                                        <asp:TextBox ID="txtTotalAllStudent" runat="server" Text='' Font-Bold="true" 
                                        Enabled="false" Height="10px" Width="35px"  Style="text-align: center" />
                                </td>
                            
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px; text-align: center">
                        <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    Enter Marks for following Students</div>
                                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="datatable">
                                    <HeaderStyle CssClass="gv_header" />
                                    <AlternatingRowStyle BackColor="#FFFFD2" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Class Roll No." 
                                            ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                    Font-Size="9pt" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="9%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="STUDNAME" HeaderText="Student Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="45%">
                                            <ItemStyle HorizontalAlign="Left" Width="32%" />
                                        </asp:BoundField>
                                        <%--EXAM MARK ENTRY--%>
                                       <asp:TemplateField HeaderText="MINOR" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotMarks" runat="server" Text='<%# Bind("S1MARK") %>' Width="50px"
                                                    Enabled="false" Font-Bold="true" Style="text-align: center" ToolTip='<%# Bind("S1MARK") %>' />
                                                <asp:Label ID="lblTotMarks" runat="server" Text='<%# Bind("S1MAX") %>' ToolTip='<%# Bind("LOCKS1") %>'
                                                    Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MID" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                         
                                                <asp:TextBox ID="txtTAMarks" runat="server" Text='<%# Bind("S2MARK") %>' Width="50px"
                                                    Font-Bold="true" Style="text-align: center"  Enabled="false"/>
                                                <asp:Label ID="lblTAMarks" runat="server" Text='<%# Bind("S2MAX") %>' ToolTip='<%# Bind("LOCKS2") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblTAMinMarks" runat="server" Text='<%# Bind("S2MIN") %>' Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TH" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtESMarks" runat="server" Text='<%# Bind("EXTERMARK") %>' Width="50px"
                                                    Font-Bold="true" Style="text-align: center" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtESMarks" runat="server" FilterType="Custom"
                                                    ValidChars="0123456789." TargetControlID="txtESMarks">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:Label ID="lblESMarks" runat="server" Text='<%# Bind("MAXMARKS_E") %>' ToolTip='<%# Bind("LOCKE") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblESMinMarks" runat="server" Text='<%# Bind("MINMARKS") %>' Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ESEM-PR" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtESPRMarks" runat="server" Text='<%# Bind("S4MARK") %>' Width="50px" 
                                                    Font-Bold="true" Style="text-align: center" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtESPRMarks" runat="server" FilterType="Custom"
                                                    ValidChars="0123456789." TargetControlID="txtESPRMarks">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:Label ID="lblESPRMarks" runat="server" Text='<%# Bind("S4MAX") %>' ToolTip='<%# Bind("LOCKS4") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblESPRMinMarks" runat="server" Text='<%# Bind("S4MIN") %>' Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTAL MARKS" Visible="true" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotMarksAll" runat="server" Text='<%# Bind("MARKTOT") %>' Width="50px" Enabled="false"
                                                    Font-Bold="true" Style="text-align: center"  />
                                                <asp:HiddenField ID="hidTotMarksAll" runat="server" Value='<%# Bind("MARKTOT") %>' />
                                                    
                                            </ItemTemplate>
                                            
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTAL %" Visible="true" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotPer" runat="server" Text='<%# Bind("SCALEDN_PERCENT") %>' Width="50px"  Enabled="false"
                                                     Font-Bold="true" Style="text-align: center"  />
                                                <asp:HiddenField ID="hidTotPer" runat="server" Value='<%# Bind("SCALEDN_PERCENT") %>' />
                                            </ItemTemplate>
                                            
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GRADE" Visible="true" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGrade" runat="server" Text='<%# Bind("GRADE") %>' Width="50px" Enabled="false"
                                                  Font-Bold="true" Style="text-align: center"  />
                                                <asp:HiddenField ID="hidGrade" runat="server" Value='<%# Bind("GRADE") %>' />
                                            </ItemTemplate>
                                            
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="6%" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="GD POINT" Visible="true" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGradePoint" runat="server" Text='<%# Bind("GDPOINT") %>' Width="20px" Enabled="false"
                                                  Font-Bold="true" Style="text-align: center"  />
                                                 <asp:HiddenField ID="hidGradePoint" runat="server" Value='<%# Bind("GDPOINT") %>' />
                                            </ItemTemplate>
                                            
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="1%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
    
         function ControlGrade(txt)
          {
             var id=0;
             id= document.getElementById('txtMax').value;
             alert(id);
          }
        
      //This Method for Theory Marks.  
        function validateMarkTH(metxt, maxmrk, minmark,txttot,txtmid,txttotall,totper,txtgrade,txtgradepoint,hidTotMarks,hidTotPer,hidGrade,hidGradePoint,Scale,totalmarks) 
        {
           if (metxt.value != '')
             {
                
                if (metxt.value != '' & (Number(metxt.value) > maxmrk)) 
                {  
                    var totfail = 0;
                    var totPfail = 0;
                    var totP1fail = 0;
                    if (Number(metxt.value) == 401 || Number(metxt.value) == 402 || Number(metxt.value) == 403)
                    {
                       if (Number(metxt.value) == 401 || Number(metxt.value) == 402 || Number(metxt.value) == 403)
                       {
                        if (txttot == '401' || txttot == '402' || txttot == '403')
                        {
                            if (txtmid == '401' || txtmid == '402' || txtmid == '403') 
                            {
                                totfail = 0;
                                totPfail=0.00;
                                txttotall.value = totfail;
                                hidTotMarks.value = totfail;
                                totper.value = totPfail;
                                hidTotPer.value = totPfail;
                                var dataRows32 = null;
                                
                            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
                            {
                             dataRows32 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                            }

                            if (dataRows32.length == "10") 
                            {
							    if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'F';
                                hidGrade.value = 'F';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'W';
                                hidGrade.value = 'W';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                txtgradepoint.value = 0;
                                hidGradePoint.value = 0;
                                var dataRows44 = null;
                                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                    dataRows44 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                                }

                                for (i = 0; i < dataRows44.length - 1; i++) {
                                    MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                    MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                    GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                    Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                }
                                studTotal();
                            }
                            else
                            {
                                 if (dataRows32.length == "11") 
                                 {
                                     if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'FF';
                                hidGrade.value = 'FF';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'FA';
								hidGrade.value = 'FA';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                     txtgradepoint.value = 0;
                                     hidGradePoint.value = 0;
                                     var dataRows45 = null;
                                     if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                         dataRows45 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                     }

                                     for (i = 0; i < dataRows45.length - 1; i++) {
                                         MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                         MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                         GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                         Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                         document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                         document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                     }
                                     studTotal();
                                 }
                            }  
                            }
                            else 
                            {
                                totfail = txtmid;
                                txttotall.value = totfail;
                                hidTotMarks.value = totfail;
                                if (Scale == '0') 
                                {
                                    totPfail = totalmarks * totfail / totalmarks;
                                    totPfail = parseFloat(totPfail).toFixed(2);
                                   
                                }
                                else 
                                {
                                    totPfail = Scale * totfail / totalmarks;
                                    totPfail = parseFloat(totPfail).toFixed(2);
                                    
                                }
                                totper.value = totPfail;
                                hidTotPer.value = totPfail;
                                var dataRows33 = null;
                          
                            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
                            {
                             dataRows33 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                            }

                            if (dataRows33.length == "10") {
                                if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'F';
                                hidGrade.value = 'F';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'W';
                                hidGrade.value = 'W';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                txtgradepoint.value = 0;
                                hidGradePoint.value = 0;
                                var dataRows46 = null;
                                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                    dataRows46 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                }

                                for (i = 0; i < dataRows46.length - 1; i++) {
                                    MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                    MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                    GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                    Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                }
                                studTotal();
                            }
                            else
                             {
                                if (dataRows33.length == "11") 
                                {
                                    if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'FF';
                                hidGrade.value = 'FF';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'FA';
                                hidGrade.value = 'FA';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                    txtgradepoint.value = 0;
                                    hidGradePoint.value = 0;
                                    var dataRows47 = null;
                                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                        dataRows47 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                    }

                                    for (i = 0; i < dataRows47.length - 1; i++) {
                                        MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                        MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                        GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                        Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                    }
                                    studTotal();
                                }
                            }
                                
                            }
                        }
                        if (txtmid == '401' || txtmid == '402' || txtmid == '403') 
                        {
                            if (txttot == '401' || txttot == '402' || txttot == '403') 
                            {
                                totfail = 0;
                                totPfail = 0.00;
                                txttotall.value = totfail;
                                hidTotMarks.value = totfail;
                                totper.value = totPfail;
                                hidTotPer.value = totPfail;
                                var dataRows34 = null;
                                if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
                            {
                             dataRows34 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                            }

                            if (dataRows34.length == "10") 
                            {
                                if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'F';
                                hidGrade.value = 'F';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'W';
                                hidGrade.value = 'W';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                txtgradepoint.value = 0;
                                hidGradePoint.value = 0;
                                var dataRows48 = null;
                                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                    dataRows48 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                }

                                for (i = 0; i < dataRows48.length - 1; i++) {
                                    MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                    MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                    GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                    Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                }
                                studTotal();
                            }
                            else 
                            {
                                if (dataRows34.length == "11") 
                                {
                                   if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'FF';
                                hidGrade.value = 'FF';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'FA';
                                hidGrade.value = 'FA';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                    txtgradepoint.value = 0;
                                    hidGradePoint.value = 0;
                                    studTotal();
                                }
                            }
                            }
                            else 
                            {
                                totfail = txttot;
                                txttotall.value = totfail;
                                hidTotMarks.value = totfail;
                                if (Scale == '0') 
                                {
                                    totPfail = totalmarks * totfail / totalmarks;
                                    totPfail = parseFloat(totPfail).toFixed(2);
                                   
                                }
                                else 
                                {
                                    totPfail = Scale * totfail / totalmarks;
                                    totPfail = parseFloat(totPfail).toFixed(2);
                                  
                                }
                                totper.value = totPfail;
                                hidTotPer.value = totPfail;
                                  var dataRows35 = null;
                                if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
                            {
                             dataRows35 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                            }

                            if (dataRows35.length == "10") {
                                if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'F';
                                hidGrade.value = 'F';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'W';
                                hidGrade.value = 'W';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                txtgradepoint.value = 0;
                                hidGradePoint.value = 0;
                                var dataRows49 = null;
                                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                    dataRows49 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                }

                                for (i = 0; i < dataRows49.length - 1; i++) {
                                    MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                    MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                    GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                    Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                }
                                studTotal();
                            }
                            else {
                                if (dataRows35.length == "11") 
                                {
                                    if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'FF';
                                hidGrade.value = 'FF';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'FA';
                                hidGrade.value = 'FA';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                    txtgradepoint.value = 0;
                                    hidGradePoint.value = 0;
                                    var dataRows50 = null;
                                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                        dataRows50 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                    }

                                    for (i = 0; i < dataRows50.length - 1; i++) {
                                        MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                        MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                        GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                        Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                    }
                                    studTotal();
                                }
                            }
                            }
                        }
                        if (txtmid != '401' && txttot != '401') 
                        {
                            if (txtmid != '403' && txttot != '403') 
                            {
							 if (txtmid != '402' && txttot != '402') 
                            {
                                totfail = txtmid + txttot;
                                txttotall.value = totfail;
                                hidTotMarks.value = totfail;
                                if (Scale == '0')
                                 {
                                
                                    totPfail = totalmarks * totfail / totalmarks;
                                    totPfail = parseFloat(totPfail).toFixed(2);

                                }
                                else
                                {
                                    totPfail = Scale * totfail / totalmarks;
                                    totPfail = parseFloat(totPfail).toFixed(2);

                                }
                                totper.value = totPfail;
                                hidTotPer.value = totPfail;
                                var dataRows36 = null;
                                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                    dataRows36 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                                }

                                if (dataRows36.length == "10") {
                                    if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'F';
                                hidGrade.value = 'F';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'W';
                                hidGrade.value = 'W';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                    txtgradepoint.value = 0;
                                    hidGradePoint.value = 0;
                                    var dataRows51 = null;
                                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                        dataRows51 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                    }

                                    for (i = 0; i < dataRows51.length - 1; i++) {
                                        MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                        MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                        GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                        Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                    }
                                    studTotal();
                                }
                                else {
                                    if (dataRows36.length == "11") {

                                        if (Number(metxt.value) == 401 )
                                {
                                txtgrade.value = 'FF';
                                hidGrade.value = 'FF';
								}
								if (Number(metxt.value) == 402)
                                {
								txtgrade.value = 'FA';
                                hidGrade.value = 'FA';
								}
								if (Number(metxt.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                        txtgradepoint.value = 0;
                                        hidGradePoint.value = 0;
                                        var dataRows60 = null;
                                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                            dataRows60 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                        }

                                        for (i = 0; i < dataRows60.length - 1; i++) {
                                            MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                            MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                            GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                            Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                        }
                                        studTotal();
                                    }
                                }

                            }
							
							}
                        }
                    }
                    
                    }  
                    else 
                    {
                        alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' and Note : 401 for Absent and 402 for withheld and 403 for Copy case ');
                        metxt.value = '';
                    }
                    IsNumericTest(metxt);
                }
                else
                {
                  
                  var me=0;
                  var tot=0;
                  var totP=0;
                  var totP1=0;
                      
                  me = parseInt(metxt.value); 
                  if(txttot!='401' && txttot!='402' && txttot!='403' && txtmid!='401' && txtmid!='402' && txtmid!='403')
                  {
                  tot = txttot + txtmid + me;
                  }
                  else
                  {
                  if(txttot=='401' || txttot=='402' || txttot=='403')
                   {
                     if(txtmid=='401' || txtmid=='402' || txtmid=='403')
                        {
                        tot = me;
                        }
                        else
                        {
                         tot = txtmid + me;
                        }
                   }
                   else
                   {
                       if(txtmid=='401' || txtmid=='402' || txtmid=='403')
                        {
                         tot = txttot + me;
                        }
                        else
                        {
                        tot = txttot + txtmid + me;
                        }
                   }
                  } 
                  txttotall.value=tot;
                  hidTotMarks.value=tot;
                 if(Scale=='0')
                 {
                   totP=totalmarks*tot/totalmarks;
                   totP=parseFloat(totP).toFixed(2);
               
                  totP1=Math.round(totP);
                 }
                 else
                 {
                  totP=Scale*tot/totalmarks;
                  totP=parseFloat(totP).toFixed(2);
             
                  totP1=Math.round(totP);
                 }
                  totper.value=totP;
                  hidTotPer.value=totP;
                  var MaxAmt = 0;
                  var MinAmt = 0;
                  var dataRows = null;
            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
            {
             dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
            }
               for(i = 0; i < dataRows.length-1; i++)
                {
                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtMax').value.trim());
                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtMin').value.trim());
                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtGrades').value.trim();
                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtGradePoints').value.trim(); 
               
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtTotalStudent').value=0;
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_hidTotalStudent').value=0;
                if(totP1>=MinAmt && totP1<=MaxAmt)
                {
//                if(examType=='1')
//                {
               
                txtgrade.value=GradeName;
                hidGrade.value=GradeName;
                txtgradepoint.value=Point;
                hidGradePoint.value=Point;
//                }
//                else
//                {
//                if(schemeNo>='9')
//                {
                
//                txtgrade.value=GradeName;
//                hidGrade.value=GradeName;
//                txtgradepoint.value=Point;
//                hidGradePoint.value=Point;
//                }
//                else
//                {
//                if(GradeName=='FF')
//                {
//                
//                txtgrade.value=GradeName;
//                hidGrade.value=GradeName;
//                txtgradepoint.value=Point;
//                hidGradePoint.value=Point;
//                }
//                else
//                {
//        
//                txtgrade.value='DD';
//                hidGrade.value='DD';
//                txtgradepoint.value='4';
//                hidGradePoint.value='4';
//                }
//                }
//                }
                }
                }
                
                studTotal();
                
                }
         
            }
            else
            {
             txttotall.value='';
             totper.value='';
             txtgrade.value='';
             txtgradepoint.value = '';
             hidTotMarks.value= '';
             hidTotPer.value = '';
             hidGrade.value = '';
             hidGradePoint.value = '';
                   var dataRows = null;
            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
            {
             dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
             
            }
             for(i = 0; i < dataRows.length-1; i++)
                {
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtTotalStudent').value=0;
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_hidTotalStudent').value=0;
                }
              studTotal()
            }
           
        }
        //This Method for Calculating Students which done mark entry 
        function studTotal()
         {
        
                var Grade1=null;
                var Grade2=null;
                var totStud=0;
                var countstud=0;
                var sumtotstud=0;
                var sum=0;
                
                 var dataRows = null;
         if(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent') != null)
         {
            dataRows = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent').getElementsByTagName('tr');
            
         }
        
                for(j=2; j <= dataRows.length; j++)
                {
                if(j<10)//for less than 10 length of Students.. 
                {
                 Grade1 = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0'+j+'_txtGrade');

                var dataRows1 = null;
                if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
                {
                 dataRows1 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                }
                 for(k=0; k < dataRows1.length-1; k++)
                  {
                  Grade2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtGrades'); 
             
                 // Here Check GDPOINT of lvgrade & lvStudent 
                 if(Grade1.value!=null && Grade2.value!=null)
                 {
                   if(Grade1.value==Grade2.value)
                   {
               
                  
                    totStud=parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtTotalStudent').value.trim()); 
                   
                    countstud=totStud+1;

                     document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtTotalStudent').value=countstud;
                     document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_hidTotalStudent').value=countstud;
                   }
                   
                   }
                  }
                }
                else
                {
                //for graeter than 10 length of Students.. 
                 Grade1 =document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl'+j+'_txtGrade');

                  var dataRows2 = null;
                if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
                {
                 dataRows2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                 
                }
                 for(k=0; k < dataRows2.length-1; k++)
                  {
                  Grade2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtGrades'); 

                   if(Grade1.value!=null && Grade2.value!=null)
                 {  if(Grade1.value==Grade2.value)
                   {
                    totStud=parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtTotalStudent').value.trim()); 

                    countstud=totStud+1;

                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtTotalStudent').value=countstud;
                     document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_hidTotalStudent').value=countstud;
                   }
                   }
                  }
                }
                
                }
            var dataRows3 = null;
            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
            {
             dataRows3 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
             
            }
          
                for(k=0; k < dataRows3.length-1; k++)
                  {
                      sumtotstud=parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtTotalStudent').value.trim()); 
                      sum=sumtotstud + sum;
                      document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAllStudent').value=sum;
                  }
                   
         }
         //This Method is for Practical marks entry
         function validateMark(metxtPr, maxmrkPR, minmarkPr, totper, txtgrade, txtgradepoint, hidTotPer, hidGrade, hidGradePoint, Scale, totalmarks) 
        {
           
         
         if (metxtPr.value != '')
          {
            if (metxtPr.value != '' & (Number(metxtPr.value) > maxmrkPR)) 
             {
                 var totfail = 0;
                 var totPfail = 0;
                 var totP1fail = 0;
                 if (Number(metxtPr.value) == 401 || Number(metxtPr.value) == 402 || Number(metxtPr.value) == 403 ) 
                    {
                       totPfail = 0.00;
                        
                       totper.value = totPfail;
                       hidTotPer.value = totPfail;
                        
                       var dataRows40 = null;
                       if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) 
                        {
                            dataRows40 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                        }
                      //Here Check Length of grades & put grade 'F' when length 8 & grade 'FF' when Length 9.
                       if (dataRows40.length == "10") 
                        {
						        if (Number(metxtPr.value) == 401 )
                                {
                                txtgrade.value = 'F';
                                hidGrade.value = 'F';
								}
								if (Number(metxtPr.value) == 402)
                                {
								txtgrade.value = 'W';
                                hidGrade.value = 'W';
								}
								if (Number(metxtPr.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                            
                            txtgradepoint.value = 0;
                            hidGradePoint.value = 0;
            var dataRows41 = null;
            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
            {
             dataRows41 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');             
            }

            for (i = 0; i < dataRows41.length - 1; i++) {
                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
            }
            
            studTotal();
                        }
                        else
                        {
                            if (dataRows40.length == "11") 
                            {
                                if (Number(metxtPr.value) == 401 )
                                {
                                txtgrade.value = 'FF';
                                hidGrade.value = 'FF';
								}
								if (Number(metxtPr.value) == 402)
                                {
								txtgrade.value = 'FA';
                                hidGrade.value = 'FA';
								}
								if (Number(metxtPr.value) == 403)
                                {
								txtgrade.value = 'I';
                                hidGrade.value = 'I';
								}
                                txtgradepoint.value = 0;
                                hidGradePoint.value = 0;
                                var dataRows42 = null;
                                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                    dataRows42 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                }

                                for (i = 0; i < dataRows42.length - 1; i++) {
                                    MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                    MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                    GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                    Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                }
            
                                studTotal();
                            }
                        }
                    }
                    else
                    {
                        alert("Please Enter Marks in the Range of 0 to " + maxmrkPR.toString() + ' and Note : 401 for Absent and 402 for Withheld and 403 for Copycase');
                        metxtPr.value = '';
                    }
                    IsNumericTest(metxtPr);
                }
                else
                {
                  var me=0;
                  var totP=0;
                  var totP1=0;
                  me = parseInt(metxtPr.value); 
                 //Here Check Scaling 
                  if(Scale=='0')
                 {
                        totP=totalmarks*me/totalmarks;
                        totP=parseFloat(totP.toFixed(2));
                        totP1=Math.round(totP);
                 }
                 else
                 {
                        totP=Scale*me/totalmarks;
                        totP=parseFloat(totP).toFixed(2);
                        totP1=Math.round(totP);
                 }
                   
                  totper.value=totP;
                  hidTotPer.value=totP;
                  var MaxAmt = 0;
                  var MinAmt = 0;
                  var dataRows = null;
            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
            {
             dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
             
            }
           
               for(i = 0; i < dataRows.length-1; i++)
                {
                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtMax').value.trim());
                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtMin').value.trim());
                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtGrades').value.trim();
                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtGradePoints').value.trim(); 
             
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtTotalStudent').value=0;
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_hidTotalStudent').value=0;
                //Here Check range and allot grade . 
                if(totP1 >= MinAmt && totP1<= MaxAmt)
                {
//                if(examType=='1')
//                {
               
                txtgrade.value=GradeName;
                hidGrade.value=GradeName;
                txtgradepoint.value=Point;
                hidGradePoint.value=Point;
//                }
//                else
//                {
//                if(schemeNo>='9')
//                {
//                
//                txtgrade.value=GradeName;
//                hidGrade.value=GradeName;
//                txtgradepoint.value=Point;
//                hidGradePoint.value=Point;
//                }
//                else
//                {
//                if(GradeName=='FF')
//                {
//                
//                txtgrade.value=GradeName;
//                hidGrade.value=GradeName;
//                txtgradepoint.value=Point;
//                hidGradePoint.value=Point;
//                }
//                else
//                {
//                 
//                txtgrade.value='DD';
//                hidGrade.value='DD';
//                txtgradepoint.value='4';
//                hidGradePoint.value='4';
//                }
//                }
//                }
                }
                }
                //This is for calculating Students of mark entry done.
                studTotal();
                
                }
                
             }
             else
             {
      
             totper.value='';
             txtgrade.value='';
             txtgradepoint.value ='';
             hidTotPer.value ='';
             hidGrade.value='';
             hidGradePoint.value='';
             var dataRows = null;
            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
            {
             dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');             
            }
             for(i = 0; i < dataRows.length-1; i++)
                {
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+i+'_txtTotalStudent').value=0;
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_hidTotalStudent').value=0;
                }
              studTotal()
             }
        }


        function IsNumericTest(txt) {
            var ValidChars = "0123456789.";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (mChar == '.') cnt++;
                if (cnt > 1) { alert("Please Check the value."); txt.value = ""; return }
                else
                    if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }
        function IsNumeric(txt) {
            var ValidChars = "0123456789";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

        function showLockConfirm() {
            var ret = confirm('Do you really want to lock marks for selected exam?');
            if (ret == true)
                return true;
            else
                return false;
        }
           //This method for changing Max Marks Range.
       function changeMaxMarksRange(txt)
       {
       
            X1 = Number(txt.value);
             var dataRows20 = null;
                if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
                {
                 dataRows20 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                }
                	
            xMax1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl0_txtMax').value);
           
            //Here Check Grades lv length 
            if (dataRows20.length=="10")
          	{   						
			xMin1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl6_txtMin').value);   
			 
			}
			else 
			{
			    if (dataRows20.length=="11")
			    {
			     xMin1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl7_txtMin').value);   
			    
			    }
			}
			//Here Check entered range is in between max and min  range or not.
				if(X1 < xMax1 & X1 > xMin1) 
				{
			    var dataRows7 = null;
                if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
                {
                 dataRows7 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                
                }	
					
				for(k = 0 ; k < dataRows7.length-1 ; k++)
				{
					       
					xMax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value);    						
					xMin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value);    
					if (X1 == xMax)
					{			
					  if (k == 0 ) 
					  {
					    break;
					  } 
					  else 
					  {	
					  if(Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value)<=Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value))
					      {
					      if(Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value)>9)
					      {
					      if(Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value)>=10)
					         {
					         alert("No. Should not Less then  (" + xMin + ") ")
					         
					         document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+(k-1)+'_txtMin').value=null;
					         document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value=null;
					         break;
					         }
					         else
					         {
					         document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+(k-1)+'_txtMin').value = X1 + 1;    
						    break;
					         }
					         }
					         else
					         {
					         if(Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value)<10)
					         {
					        if(Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value)<=Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value))
                             {
                             alert("No. Should not Less then  (" + xMin + ") ")
					         
					         break;
                             }
                             else
					         {
							document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+(k-1)+'_txtMin').value = X1 + 1;    
						    break;
						     }	
					         }
					         else
					         {
							document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+(k-1)+'_txtMin').value = X1 + 1;    
						    break;
						     }	
					      }
					      }
					      else
					      {
							document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+(k-1)+'_txtMin').value = X1 + 1;    
						    break;
						  }		  		
						
					  }					  
					}
				}
				AdjustGrades();
				}
				else
				{
				 alert("Please Enter Value Between "+ xMin1 +" To "+ xMax1 +" ");
				  var dataRows10 = null;
            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
            {
             dataRows10 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
             
            }	
					
				for(k = 0 ; k < dataRows10.length-1 ; k++) 
				{
					       
					xMax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value);    						
					xMin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value);    
					// Here If entered value is out of range then do it's  min  max  null
					if (X1 == xMax)
					{
						document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value =null;
						document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+(k-1)+'_txtMin').value =null;
					}	
					}	
				}
				
       }
       //This method for changing Min Marks Range. 
       function changeMinMarksRange(txt)
       {
            X1 = Number(txt.value);

            var dataRows20 = null;
            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                dataRows20 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
            }

            xMax1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl0_txtMax').value);
            //Here Check Grades lv length 
            if (dataRows20.length == "10") {
                xMin1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl6_txtMin').value);
            }
            else {
                if (dataRows20.length == "11") {
                    xMin1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl7_txtMin').value);
                }
            }  
            //Here Check entered range is in between max and min  range or not.
			if(X1 < xMax1 & X1 > xMin1 )
			{
		    var dataRows8 = null;
            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
            {
             dataRows8 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
             
            }	
         	for(k = 0 ; k < dataRows8.length-1 ; k++) 
				{
   					xMax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value);    						
					xMin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value);    
					
					if (X1 == xMin)
					{			
					  if (k == 7 ) // if first record ie. 'S' grade
					  {
					    break;
					  } 
					  else 
					  {	
					  //Here Checking of Entered Range is correct or not in max & min values.
					  if(Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value)>=Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value))
					      {
					         alert("No. Should not Greater then  (" + xMax + ") ")
					         document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value=null;
					         document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+(k+1)+'_txtMax').value=null;
					         break;
					      }
					      else
					      {
					      
							document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+(k+1)+'_txtMax').value = X1 - 1;    
						    break;
						  }	
					  						  		
						
					  }					  
					}
				}
				//Calling method AdjustGrades(); .
		AdjustGrades();
		}
		else
				{
				 alert("Please Enter Value Between "+ xMin1 +" To "+ xMax1 +"  ");
				  var dataRows10 = null;
            if(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null)
            {
             dataRows10 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');            
            }	
					
				for(k = 0 ; k < dataRows10.length-1 ; k++) 
				{
					       
					xMax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMax').value);    						
					xMin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value);    
					// Here If entered value is out of range then do it's  min  max  null
					if (X1 == xMin)
					{
						document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+k+'_txtMin').value =null;
						document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl'+(k+1)+'_txtMax').value =null;
					}	
				}
			  }
       }
       // This Method Adjust or Re-entered Grades Every time whenever marks entered.
      function AdjustGrades()
       {
       
        var dataRows4 = null;
        if(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent') != null)
        {
           dataRows4 = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent').getElementsByTagName('tr');
        }
        // For loop For student Gridview list 
            for(j=2; j <= dataRows4.length; j++)
             {
                  
                if(j<10)//This if check length less than 10 
                {
                    var totP = 0;
                    var EsemMark = 0;
                    var subId = 0;           
                 subId = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_hdfSubid').value.trim());
                 
                 totP = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0'+j+'_txtTotPer').value.trim());
                 if(subId == 1)
                 {
                 EsemMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtESMarks').value.trim());
                 }
                 if(subId == 2)
                 {
                 EsemMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtESPRMarks').value.trim());
                 }
                 if (EsemMark != '401' && EsemMark != '402' && EsemMark != '403') {
                     if (totP != '') {

                         var MaxAmt = 0;
                         var MinAmt = 0;
                         var dataRows5 = null;

                         if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                             dataRows5 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                         }
                           //This For Loop of lvGrades check range of tot percent & fill Grades & grade Point  of that range which will match.
                         for (i = 0; i < dataRows5.length - 1; i++) {
                             MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                             MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                             GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                             Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();
                             //This is fill zero every time before filling grades
                             document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                             document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                           //if condition for checking min - max range of tot percent .
                             if (totP >= MinAmt && totP <= MaxAmt)
                              {
                               ExamType= document.getElementById('ctl00_ContentPlaceHolder1_hdfExamType').value.trim();
                               SchemeNo= document.getElementById('ctl00_ContentPlaceHolder1_hdfSchemeNo').value.trim();
//                                  if(ExamType=='1')
//                                    {
               
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
//                                    }
//                            else
//                                    {
//                                  if(SchemeNo>='9')
//                                    {
//                
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
//                                    }
//                                    else
//                                    {
//                                    if(GradeName=='FF')
//                                    {
//                
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
//                                     }
//                                    else
//                                    {
//                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = 'DD';
//                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = 'DD';

//                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = '4';
//                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = '4';
//                                    }
//                                    }
//                                    }
                                 
                             }
                         }



                     }
                     else {
                         break;
                     }
                 }
                 
               }
               else// This for above 10 length 
               {
                var totP=0;
                var subId = 0;           
                 subId = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_hdfSubid').value.trim());
               totP = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl'+j+'_txtTotPer').value.trim());
               if(subId == 1)
                 {
                 EsemMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtESMarks').value.trim());
                 }
                 if(subId == 2)
                 {
                 EsemMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtESPRMarks').value.trim());
                 }
               
              if (EsemMark != '401' && EsemMark != '402' && EsemMark != '403') {
                  if (totP != '') {
                      var MaxAmt = 0;
                      var MinAmt = 0;
                      var dataRows6 = null;

                      if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                          dataRows6 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                      }
                      //This For Loop of lvGrades check range of tot percent & fill Grades & grade Point  of that range which will match.
                      for (i = 0; i < dataRows6.length - 1; i++) {
                          MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                          MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                          GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                          Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();
                        //This is fill zero every time before filling grades
                          document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                          document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                          //if condition for checking min - max range of tot percent .
                          if (totP >= MinAmt && totP <= MaxAmt)
                           {
                               ExamType= document.getElementById('ctl00_ContentPlaceHolder1_hdfExamType').value.trim();
                               SchemeNo= document.getElementById('ctl00_ContentPlaceHolder1_hdfSchemeNo').value.trim();
//                                  if(ExamType=='1')
//                                    {
//               
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
//                                    }
//                            else
//                                    {
//                                  if(SchemeNo>='9')
//                                    {
//                
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
//                                    }
//                                    else
//                                    {
//                                    if(GradeName=='FF')
//                                    {
//                
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
//                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
//                                     }
//                                    else
//                                    {
//                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = 'DD';
//                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = 'DD';

//                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = '4';
//                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = '4';
//                                    }
//                                    }
//                                    }
                          }
                      }


                  }
                  else {
                      break;
                  }
              }
               }
            }
               studTotal();
       }
    </script>

</asp:Content>
