<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="InternalDoubleMarkEntryByAdmin.aspx.cs" Inherits="ACADEMIC_InternalDoubleMarkEntryByAdmin" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updinternal"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <img src="../IMAGES/ajax-loader.gif" alt="Loading" />
                <br />
               <b> Please wait..</b>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

   
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>
    <script type="text/javascript">
        function chkmax(txt) {
            debugger;
            var id = 0; var s1 = 0;
            id = document.getElementById('hdS1max').value;
            s1 = document.getElementById('txtTotMarks').value;
            if (s1 > id) {
                alert('mark should not be greather than max mark');
            }

        }

    </script>
    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });

    </script>--%>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:Panel ID="pnlMain" runat="server">
        <asp:UpdatePanel ID="updinternal" runat="server">
            <ContentTemplate>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="2">INTERNAL EXAM MARK ENTRY BY ADMIN
                            <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                </td>
            </tr>
            <%--PAGE HELP--%>
            <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
            <tr>
                <td colspan="2">
                    <!-- "Wire frame" div used to transition from the button to the info panel -->
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                    </div>
                    <!-- Info panel to be displayed as a flyout when the button is clicked -->
                    <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                        <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                            <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
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
        <br />
        <%-- <div style="color: Red; font-weight: bold">
            &nbsp;Note : * marked fields are Mandatory
        </div>--%>
        <div style="width: 98%; padding-left: 10px">
                    <fieldset class="fieldset">
                        <legend class="legend">INTERNAL EXAM MARK ENTRY BY ADMIN</legend>
                        <table width="100%">
                            <tr>
                                <td class="form_left_label" style="width: 20%">
                                    <span class="validstar">*</span>Session :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" Font-Bold="True">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <span class="validstar">&nbsp;</span>College / School Name:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot"
                                        Width="30%" AutoPostBack="true" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlColg"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Degree :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot"
                                        Width="30%" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Branch :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot"
                                        Width="30%" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Scheme :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5" Width="30%"
                                        ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Semester :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Subject Type:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" TabIndex="7" AutoPostBack="true" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged1"
                                        ValidationGroup="teacherallot">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Subject Type" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            

                            <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Course :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" TabIndex="8" ValidationGroup="teacherallot"   Width="40%">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                        Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>


                            <tr>
                                <td class="form_left_label">&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label></td>
                            </tr>
                           
                        </table>
                    </fieldset>
                </div>
        <br />
          <table width="100%">
                <tr>
                    <td align="center" style="width: 50%;">&nbsp;<asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="teacherallot" 
                                Width="100px" ToolTip="SAVE" OnClick="btnShow_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnSave" runat="server" Text="Save" Enabled="false" ValidationGroup="teacherallot" OnClick="btnSave_Click" 
                                Width="100px" ToolTip="SAVE" />&nbsp;&nbsp;
                            <asp:Button ID="btnLock" runat="server"  Text="Lock" Enabled="false" ValidationGroup="teacherallot" OnClick="btnLock_Click"
                                Width="100px" ToolTip="Lock" />&nbsp;&nbsp;
                                    <asp:Button ID="btnClear" runat="server" Text="Cancel"  Width="100px" OnClick="btnClear_Click" />
                       <%-- &nbsp;&nbsp;
                                    <asp:Button ID="btnReport" runat="server" Text="Report" Visible="false" Width="80px" OnClick="btnReport_Click" />--%>


                    </td>
                    <td align="center">

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </td>
                </tr>
            </table>
        <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
            <div style="color: Red; font-weight: bold">
                        &nbsp;Note:1) -1 for Absent mark entry
                            
            </div>
            <div id="demo-grid" style="background: #fff; border: solid 2px #A7BAC5; font-family: verdana; font-size: 11px; width: 60%;">
                <div class="titlebar" runat="server" id="title" style="font-family: 'Courier New'; font-weight: bold; font-size: medium; text-align: center;">
                </div>
                <asp:GridView ID="gvStudent" runat="server" OnRowDataBound="gvStudent_RowDataBound" AutoGenerateColumns="False" Width="100%">
                    <HeaderStyle CssClass="gv_header" />
                    <AlternatingRowStyle BackColor="#FFFFD2" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No."
                            ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center"> 
                            <ItemTemplate>
                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1%>' Font-Size="9pt" />

                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" />

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class Roll No."
                            ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">                
                            <ItemTemplate>
                                <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                    Font-Size="9pt" />
                                <asp:HiddenField  runat="server" ID="hdnlock" Value='<%# Bind("lock") %>'/>
                            </ItemTemplate>
                            <ItemStyle Width="15%" HorizontalAlign="Center" />

                        </asp:TemplateField>
                        <asp:TemplateField Visible="false" HeaderText="Student Name"
                            ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblCoursename" runat="server"
                                    Font-Size="9pt" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>

                        <%--EXAM MARK ENTRY--%>
                        <asp:TemplateField HeaderText="C1" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTotMarks" runat="server" Text='<%# Bind("S1MARK")  %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);" 
                                    Font-Bold="true" Style="text-align: center; text-align-last: center" ToolTip='<%# Bind("S1MARK") %>' />
                                <asp:HiddenField ID="hdS1max" runat="server" Value='<%# Bind("S1MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txtTotMarks">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("S1MAX") %>' ControlToValidate="txtTotMarks"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    ValidationGroup="teacherallot" Text="*"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comTutMarks" ID="cecomTutMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>

                                <asp:HiddenField runat="server" ID="hdnpaternno" Value='<%#Eval("patternno")%>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C2" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>

                                <asp:TextBox ID="txtTAMarks" runat="server" Text='<%# Bind("S2MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS2max" runat="server" Value='<%# Bind("S2MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMidsemMarks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txtTAMarks">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comMidsemMarks" ValueToCompare='<%# Bind("S2MAX") %>' ControlToValidate="txtTAMarks"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comMidsemMarks" ID="cecomMidsemMinMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>

                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C3" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtESPRMarks" runat="server" Text='<%# Bind("S3MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS3max" runat="server" Value='<%# Bind("S3MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAttMarks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txtESPRMarks">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAttMarks" ValueToCompare='<%# Bind("S3MAX") %>' ControlToValidate="txtESPRMarks" 
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks" Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAttMarks" ID="cecomAttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>

                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="C4" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts4mark" runat="server" Text='<%# Bind("S4MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS4max" runat="server" Value='<%# Bind("S4MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt1Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts4mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt1Marks" ValueToCompare='<%# Bind("S4MAX") %>' ControlToValidate="txts4mark" 
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks" 
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt1Marks" ID="cecom1AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>

                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C5" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts5mark" runat="server" Text='<%# Bind("S5MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS5max" runat="server" Value='<%# Bind("S5MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt2Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts5mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt2Marks" ValueToCompare='<%# Bind("S5MAX") %>' ControlToValidate="txts5mark" 
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks" 
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt2Marks" ID="cecom2AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>

                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                       

                        
                        <asp:TemplateField HeaderText="C6" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts6mark" runat="server" Text='<%# Bind("S6MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS6max" runat="server" Value='<%# Bind("S6MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt6Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts6mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt6Marks" ValueToCompare='<%# Bind("S6MAX") %>' ControlToValidate="txts6mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt6Marks" ID="cecom6AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C7" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts7mark" runat="server" Text='<%# Bind("S7MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS7max" runat="server" Value='<%# Bind("S7MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt7Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts7mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt7Marks" ValueToCompare='<%# Bind("S7MAX") %>' ControlToValidate="txts7mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt7Marks" ID="cecom7AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C8" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts8mark" runat="server" Text='<%# Bind("S8MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS8max" runat="server" Value='<%# Bind("S8MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt8Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts8mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt8Marks" ValueToCompare='<%# Bind("S8MAX") %>' ControlToValidate="txts8mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt8Marks" ID="cecom8AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C11" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts9mark" runat="server" Text='<%# Bind("S9MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS11max" runat="server" Value='<%# Bind("S9MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt11Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts9mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt11Marks" ValueToCompare='<%# Bind("S9MAX") %>' ControlToValidate="txts9mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt11Marks" ID="cecom11AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C10" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts10mark" runat="server" Text='<%# Bind("S10MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS10max" runat="server" Value='<%# Bind("S10MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt10Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts10mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt10Marks" ValueToCompare='<%# Bind("S10MAX") %>' ControlToValidate="txts10mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt10Marks" ID="cecom10AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
          
        </asp:Panel>


        <%--STUDENT LIST--%>
        </ContentTemplate>
        </asp:UpdatePanel>
     </asp:Panel>
    <div id="divMsg" runat="server"></div>
    <script language="javascript" type="text/javascript">
        //function validateMark(txt, maxmrk, txt1, txt2, txt3) {
        //    //alert('vijay');
        //    //alert(txt.value);
        //    //alert(txt3.value);
        //    //check for max marks
        //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
        //    if (Number(txt.value) > maxmrk || Number(txt.value) < 0) {
        //        if (Number(txt.value) == 401 || Number(txt.value) == 402 || Number(txt.value) == 403)
        //        { }
        //        else {
        //            alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 401 for Absent; 402 for Debar; 403 for Malpractice ');
        //            txt.value = '';
        //            txt.select();
        //            txt.focus();
        //        }
        //    }

        //    //check for numeric
        //    IsNumeric(txt);

        //    if (Number(txt1.value) > maxmrk || Number(txt1.value) < 0) {
        //        if (Number(txt1.value) == 401 || Number(txt1.value) == 402 || Number(txt1.value) == 403)
        //        { }
        //        else {
        //            alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 401 for Absent; 402 for Debar; 403 for Malpractice ');
        //            txt1.value = '';
        //            txt1.select();
        //            txt1.focus();
        //        }
        //    }

        //    //check for numeric
        //    IsNumeric(txt1);

        //    if (Number(txt2.value) > maxmrk || Number(txt2.value) < 0) {
        //        if (Number(txt2.value) == 401 || Number(txt2.value) == 402 || Number(txt2.value) == 403)
        //        { }
        //        else {
        //            alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 401 for Absent; 402 for Debar; 403 for Malpractice ');
        //            txt2.value = '';
        //            txt2.select();
        //            txt2.focus();
        //        }
        //    }

        //    //check for numeric
        //    IsNumeric(txt2);

        //    if (Number(txt3.value) > maxmrk || Number(txt3.value) < 0) {
        //        if (Number(txt3.value) == 401 || Number(txt3.value) == 402 || Number(txt3.value) == 403)
        //        { }
        //        else {
        //            alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 401 for Absent; 402 for Debar; 403 for Malpractice ');
        //            txt3.value = '';
        //            txt3.select();
        //            txt3.focus();
        //        }
        //    }

        //    //check for numeric
        //    IsNumeric(txt3);
        //}

        function IsNumeric(txt) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

        function IsNumeric(txt1) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt1.value.length && num == true; i++) {
                mChar = txt1.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt1.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt1.select();
                    txt1.focus();
                }
            }
            return num;
        }

        function IsNumeric(txt2) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt2.value.length && num == true; i++) {
                mChar = txt2.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt2.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt2.select();
                    txt2.focus();
                }
            }
            return num;
        }

        function IsNumeric(txt3) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt3.value.length && num == true; i++) {
                mChar = txt3.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt3.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt3.select();
                    txt3.focus();
                }
            }
            return num;
        }


        function showLockConfirm() {
            var ret = confirm('Do you want to really Lock Mark Statement for selected Exam ');
            if (ret == true)
                return true;
            else
                return false;
        }
        function CheckMark(id) {
            if (id.value < 0) {
                if (id.value == -1) {
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1 can be entered.");
                    id.value = '';
                    id.focus();
                }
            }
        }

    </script>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            debugger;
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
