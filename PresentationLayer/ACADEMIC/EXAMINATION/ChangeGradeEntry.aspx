<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ChangeGradeEntry.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ChangeGradeEntry"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                CHANGE GRADE ENTRY
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
                function Cover(bottom, top, ignoreSize) 
                {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) 
                    {
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
    <asp:Panel ID="pnlMain" runat="server" Visible="true">
        <table style="width: 100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td style="padding: 5px; border: solid 1px gray;">
                        <table style="width: 100%" cellpadding="1" cellspacing="1">
                            
                            <tr>
                                <td style="width: 15%">
                                    Session&nbsp; :
                                </td>
                                <td style="width: 60%">
                                    <asp:Label ID="lblSession2" runat="server" Font-Bold="True" Visible="False"  />
                                     <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Width="85%"
                                        AutoPostBack="True" ValidationGroup="show" 
                                        onselectedindexchanged="ddlSession_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                </td>
                                <td rowspan="8" style="vertical-align: top; text-align: left; width: 25%">
                                    <fieldset class="fieldset" style="padding: 5px; color: Green">
                                        <legend class="legend">Note</legend>
                                        <span style="color: #990033; font-weight: bold">F</span> for Fail/Absent<br />
                                        <span style="color: #990033; font-weight: bold">FA</span> for Fail Due to 
                                        Shortage of Attendence<br />
                                        <span style="color: #990033; font-weight: bold">WH</span> for With Held<br />
                                        <span style="font-weight:bold;color:Red;">Please Save and Lock for Final Submission</span>
                                    </fieldset>
                                    <br />                                    
                                    <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                </td>
                            </tr>
                            <tr id="rowOperator" runat="server" visible="false">
                                <td style="width: 15%">
                                    Operator :</td>
                                <td style="width: 60%">
                                    <asp:DropDownList ID="ddlOperator" runat="server" AppendDataBoundItems="True" 
                                        ValidationGroup="show" Width="85%">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvOperator" runat="server" ControlToValidate="ddlOperator"
                                        Display="None" ErrorMessage="Please Select Operator" InitialValue="0" Visible="false"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 60%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 15%">
                                    Curriculum :
                                </td>
                                <td style="width: 60%">
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" Width="85%"
                                        AutoPostBack="true" ValidationGroup="show" 
                                        onselectedindexchanged="ddlDegree_SelectedIndexChanged" >
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDept"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">
                                    Department :
                                </td>
                                <td style="width: 60%">
                                    <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" Width="85%"
                                        AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                        Display="None" ErrorMessage="Please Select Department" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">
                                    Scheme :
                                </td>
                                <td style="width: 60%">
                                    <asp:DropDownList ID="ddlPath" runat="server" AppendDataBoundItems="True" Width="85%"
                                        AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlPath_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPath" runat="server" ControlToValidate="ddlPath"
                                        Display="None" ErrorMessage="Please Select Path" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">
                                    Course :
                                </td>
                                <td style="width: 60%">
                                    <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" Width="85%"
                                        AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                        Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="display:none;" >
                                <td style="width: 15%">
                                    Exam :
                                </td>
                                <td style="width: 60%">
                                    <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" Width="55%"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged" ValidationGroup="show">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                    <%-- <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                        Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    
                                 </td>
                            </tr>
                            <tr>
                                 <td style="width: 15%">
                                    Student :
                                </td>
                                <td style="width: 60%">
                                    <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="True" Width="55%"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlStudent_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStudent" runat="server" 
                                        ControlToValidate="ddlExam" Display="None" ErrorMessage="Please Select Student" 
                                        InitialValue="0" SetFocusOnError="True" ValidationGroup="show"></asp:RequiredFieldValidator>
                                 </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: top">
                                    &nbsp;
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                                </td>
                                <td colspan="2" style="vertical-align: top">
                                   <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" 
                                        ValidationGroup="show" Visible="False" />
                                    &nbsp;<asp:Button ID="btnSave" runat="server" Enabled="false" Text="Save" Width="60px"
                                        OnClick="btnSave_Click" Font-Bold="True" />
                                    &nbsp;
                                    <asp:Button ID="btnLock" runat="server" Enabled="false" OnClientClick="return showLockConfirm();"
                                        Text="Lock" Width="60px" OnClick="btnLock_Click" Font-Bold="True" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel2" runat="server" Text="Cancel" Width="60px" OnClick="btnCancel2_Click" />
                                    
                                    &nbsp;<asp:Button ID="btnUnlock" runat="server" Visible="false" Font-Bold="True" 
                                         OnClientClick="return showUnLockConfirm();" Text="UnLock" 
                                        Width="60px" onclick="btnUnlock_Click" />
                                     &nbsp;
                                    <asp:Button ID="btnProcess" runat="server"  Font-Bold="True" 
                                        OnClick="btnProcess_Click" Text="Process" Width="60px" />
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px; text-align: center">
                        <asp:Panel ID="pnlStudGrid" runat="server" Visible="false" Width="80%">
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    Enter Grades for following Students</div>
                                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="datatable" OnRowDataBound="gvStudent_RowDataBound">
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle BackColor="#FFFFD2" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Reg. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                    Font-Size="9pt" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"  />
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Student Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="55%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                               &nbsp; <asp:Label ID="lblStudName" runat="server" Text='<%# Bind("STUDNAME") %>' ToolTip='<%# Bind("IDNO") %>'
                                                    Font-Size="9pt" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="45%" />
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="SEATNO" HeaderText="ControlSheet No" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%" Visible="false">
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:BoundField>--%>
                                        <%--EXAM MARK ENTRY--%>
                                        <asp:TemplateField HeaderText="Old Grade"  ItemStyle-Width="10%" 
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMarks" runat="server" Text='<%# Bind("MARK") %>' Width="50px" CssClass="uppercase" Enabled="false"/>
                                                <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCK") %>'
                                                    Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                         
                                        
                                         <asp:TemplateField HeaderText="New Grade"  ItemStyle-Width="10%" 
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewMrk" runat="server" Text='<%# Bind("NEWMARK") %>' Width="50px" CssClass="uppercase" onchange="validateAlphabet(this);"/>
                                                 <asp:Label ID="lblNewMarks" runat="server" ToolTip='<%# Bind("GD_LOCK") %>'
                                                    Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>        
    </asp:Panel>
    <script language="javascript" type="text/javascript">
        function validateMark(txt,maxmrk)
        {
            //check for max marks
            //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
            if (Number(txt.value) > maxmrk || Number(txt.value) < 0 )
            {
               if (Number(txt.value) == 401 || Number(txt.value) == 402 || Number(txt.value) == 403)
               {}
               else
               {            
                    alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 401 for Absent; 402 for Debar; 403 for Copy Case ');
                    txt.value = '';
                    txt.select();
                    txt.focus();
                }
            }

            //check for numeric 
            IsNumeric(txt);
        }
        
        function IsNumeric(txt)
		{
			var ValidChars = "0123456789.-";
			var num = true;
			var mChar;
			
			for(i=0 ; i < txt.value.length && num == true;i++)
			{
				mChar = txt.value.charAt(i);
				if (ValidChars.indexOf(mChar) == -1)
				{
					num = false;
					txt.value = '';
					alert("Error! Only Numeric Values Are Allowed")
					txt.select();
					txt.focus();
				}
			}
			return num;
		}

		function showLockConfirm()
        {
            var ret = confirm('Do you want to really Lock Mark Statement for selected Exam ');
            if (ret == true)
                return true;
            else
                return false;
        }
        
         function showUnLockConfirm()
        {
            var ret = confirm('Do you really want to  UnLock Mark Statement for selected Exam ');
            if (ret == true)
                return true;
            else
                return false;
        }
//        function validateAlphabet(txt)
//        {
//        var expAlphabet= /^[A-Za-z]+$/;
//        if(txt.value.search(expAlphabet)== -1)
//        {
//            txt.value=txt.value.substring(0,(txt.value.length) -1);
//            txt.focus();
//            alert('Only Alphabets Allowed');
//            return false;
//        }
//        else
//        return true;
        
    }
    </script>

</asp:Content>
