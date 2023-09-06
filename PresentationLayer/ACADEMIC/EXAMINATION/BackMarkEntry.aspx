<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BackMarkEntry.aspx.cs" Inherits="ACADEMIC_EXAMINATION_BackMarkEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function total(txt) {

            debugger;

            var text = txt.id.split("_");
            var IDS = text[0] + '_';
            var res = IDS.concat(text[1], '_', text[2], '_', text[3], '_');
            var b = document.getElementById('<%=btnShow.ClientID %>');

            //TEXT BOXES VALUE FIND OUT
            var textbox1 = 0, textbox3 = 0, textbox4 = 0;

            textbox1 = document.getElementById(res + 'txtTotMarks').value;
            var textbox2 = document.getElementById("txtTAMarks").style.display;
            if (textbox2 == "none") {
                textbox2 = 0;
            }
            else {

                textbox2 = document.getElementById(res + 'txtTAMarks').value;
            }
            //textbox3 = document.getElementById(res + 'txtESPRMarks').value;

            textbox4 = document.getElementById(res + 'txtESMarks').value;


            document.getElementById(res + 'txtTotMarksAll').value =
              (Number(textbox1) + Number(textbox2) + Number(textbox3) + Number(textbox4));


        }
        function Validate(txt) {
            debugger;
            var b = document.getElementById('<%=txtregcredit.ClientID %>').value;
            var c = document.getElementById('<%=txtearncredit.ClientID %>').value;
            if (b < c) {
                txt.value = '';

                alert("Total registered credit should be greater .")
                txt.select(); txt.focus();


            }
        }

    </script>

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">INTERNAL &amp; EXTERNAL
                BACK MARKS ENTRY
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />

            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <div style="color: red;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;* mark is mandatory fields</div>
                </b>
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
                            Edit Record
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
    <table width="100%">


        <tr>
            <td valign="top" align="left" style="width: 100%; padding: 10px">
                <asp:Panel ID="pnlSelection" runat="server" Width="100%" Visible="false">
                    <fieldset class="fieldset">
                        <legend class="legend">Student Wise Mark Entry</legend>
                        <table style="width: 100%" cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="width: 30%; height: 22px;">&nbsp;
                                </td>
                                <td class="form_left_text" style="height: 22px">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" style="width: 30%; text-align: right;"><span style="color: red">*</span> Session :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlsession" TabIndex="1" runat="server" AppendDataBoundItems="true" Font-Bold="true" Width="200px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddlsession" ValidationGroup="submit"
                                        Display="None" InitialValue="0" ErrorMessage="Please select session."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" style="width: 30%; text-align: right;"><span style="color: red">*</span> Exam Roll No :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRollNo" TabIndex="2" runat="server" Font-Bold="true" Width="200px" AutoCompleteType="LastName" Style="text-align: left"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvrollno" runat="server" ControlToValidate="txtRollNo" ValidationGroup="submit"
                                        Display="None" ErrorMessage="Please enter RollNo.">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" style="width: 30%; text-align: right;"><span style="color: red">*</span> Semester :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSemester" TabIndex="3" runat="server" AppendDataBoundItems="true" Font-Bold="true" Width="200px">
                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvsemetser" runat="server" ControlToValidate="ddlSemester" ValidationGroup="submit"
                                        Display="None" InitialValue="0" ErrorMessage="Please select semester.">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%; vertical-align: top;">&nbsp;
                                </td>
                                <td style="vertical-align: top">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" />
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3" style="padding: 10px; text-align: center;">
                                    <asp:Button ID="btnShow" runat="server" TabIndex="4" ValidationGroup="submit" ToolTip="SUBMIT" Text="Show" Width="80px" Font-Bold="true" OnClick="btnShow_Click" />&nbsp;
                                     <asp:Button ID="btnCancel" runat="server" TabIndex="5" ToolTip="CANCEL" Text="Cancel" Width="80px" Font-Bold="true" OnClick="btnCancel_Click" />&nbsp;
                       <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true"
                           ValidationGroup="submit" ShowSummary="false" />

                                </td>
                            </tr>

                            <tr runat="server" id="trinfo" visible="false">
                                <td colspan="2">
                                    <table width="100%" style="border-color: black; border: solid">
                                        <tr>
                                            <td align="right" style="width: 30%;">Student name:</td>
                                            <td>
                                                <asp:Label runat="server" Font-Bold="true" ID="lblStudname"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 30%;">Degree:</td>
                                            <td>
                                                <asp:Label runat="server" Font-Bold="true" ID="lblDegree"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 30%;">Branch:</td>
                                            <td>
                                                <asp:Label runat="server" Font-Bold="true" ID="lblBranch"></asp:Label></td>


                                        </tr>
                                        <tr>
                                            <td style="width: 30%;" align="right">Section:</td>
                                            <td>
                                                <asp:Label runat="server" Font-Bold="true" ID="lblsection"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 30%;">Admbatch:</td>
                                            <td>
                                                <asp:Label runat="server" Font-Bold="true" ID="lbladmbatch"></asp:Label></td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>


                            <tr>
                                <td colspan="2" style="padding: 10px; text-align: center">
                                    <asp:Panel ID="pnlCourse" runat="server" Visible="false">
                                        <div id="Div1" class="vista-grid">
                                            <div class="titlebar">
                                                Enter Marks for Courses
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
                                                    <asp:TemplateField Visible="false" HeaderText="Class Roll No."
                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                Font-Size="9pt" />

                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />

                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Course code -- Course Name"
                                                        ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCoursename" runat="server" Text='<%# Eval("ccode") +" -- "+ Eval("COURSENAME") %>' ToolTip='<%# Bind("COURSENo") %>'
                                                                Font-Size="9pt" />
                                                            <asp:HiddenField runat="server" ID="hdnlock" Value='<%# Bind("lock") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                    </asp:TemplateField>

                                                    <%--EXAM MARK ENTRY--%>
                                                    <asp:TemplateField HeaderText="C1" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTotMarks" runat="server" Text='<%# Bind("S1MARK")  %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);" Onkeyup="return total(this);"
                                                                Font-Bold="true" Style="text-align: center; text-align-last: center" ToolTip='<%# Bind("S1MARK") %>' Visible='<%# (Convert.ToInt32(Eval("S1MAX"))==0 ? false : true) %>' />
                                                            <asp:HiddenField ID="hdS1max" runat="server" Value='<%# Bind("S1MAX") %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txtTotMarks">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("S1MAX") %>' ControlToValidate="txtTotMarks"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("S1MAX")+")" %>'
                                                                ValidationGroup="CHK" Text="*"></asp:CompareValidator>




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
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("S2MAX"))==0 ? false : true) %>' />
                                                            <asp:HiddenField ID="hdS2max" runat="server" Value='<%# Bind("S2MAX") %>'  />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMidsemMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txtTAMarks">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comMidsemMarks" ValueToCompare='<%# Bind("S2MAX") %>' ControlToValidate="txtTAMarks"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("S2MAX")+")" %>'
                                                                Text="*" ValidationGroup="CHK"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comMidsemMarks" ID="cecomMidsemMinMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="C3" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtESPRMarks" runat="server" Text='<%# Bind("S3MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("S3MAX"))==0 ? false : true) %>' />
                                                            <asp:HiddenField ID="hdS3max" runat="server" Value='<%# Bind("S3MAX") %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAttMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txtESPRMarks">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comAttMarks" ValueToCompare='<%# Bind("S3MAX") %>' ControlToValidate="txtESPRMarks"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("S3MAX")+")" %>' Text="*" ValidationGroup="CHK"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAttMarks" ID="cecomAttMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C4" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txts4mark" runat="server" Text='<%# Bind("S4MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("S4MAX"))==0 ? false : true) %>' />
                                                            <asp:HiddenField ID="hdS4max" runat="server" Value='<%# Bind("S4MAX") %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt1Marks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txts4mark">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comAtt1Marks" ValueToCompare='<%# Bind("S4MAX") %>' ControlToValidate="txts4mark"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("S4MAX")+")" %>'
                                                                Text="*" ValidationGroup="CHK"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt1Marks" ID="cecom1AttMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C5" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txts5mark" runat="server" Text='<%# Bind("S5MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("S5MAX"))==0 ? false : true) %>' />
                                                            <asp:HiddenField ID="hdS5max" runat="server" Value='<%# Bind("S5MAX") %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt2Marks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txts5mark">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comAtt2Marks" ValueToCompare='<%# Bind("S5MAX") %>' ControlToValidate="txts5mark"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("S5MAX")+")" %>'
                                                                Text="*" ValidationGroup="CHK"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt2Marks" ID="cecom2AttMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C6" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txts6mark" runat="server" Text='<%# Bind("S6MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("S6MAX"))==0 ? false : true) %>' />
                                                            <asp:HiddenField ID="hdS6max" runat="server" Value='<%# Bind("S6MAX") %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt6Marks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txts6mark">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comAtt6Marks" ValueToCompare='<%# Bind("S6MAX") %>' ControlToValidate="txts6mark"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("S6MAX")+")" %>'
                                                                Text="*" ValidationGroup="CHK"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt6Marks" ID="cecom6AttMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C7" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txts7mark" runat="server" Text='<%# Bind("S7MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("S7MAX"))==0 ? false : true) %>' />
                                                            <asp:HiddenField ID="hdS7max" runat="server" Value='<%# Bind("S7MAX") %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt7Marks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txts7mark">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comAtt7Marks" ValueToCompare='<%# Bind("S7MAX") %>' ControlToValidate="txts7mark"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("S7MAX")+")" %>'
                                                                Text="*" ValidationGroup="CHK"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt7Marks" ID="cecom7AttMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C8" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txts8mark" runat="server" Text='<%# Bind("S8MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("S8MAX"))==0 ? false : true) %>' />
                                                            <asp:HiddenField ID="hdS8max" runat="server" Value='<%# Bind("S8MAX") %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt8Marks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txts8mark">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comAtt8Marks" ValueToCompare='<%# Bind("S8MAX") %>' ControlToValidate="txts8mark"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("S8MAX")+")" %>'
                                                                Text="*" ValidationGroup="CHK"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt8Marks" ID="cecom8AttMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C11" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txts9mark" runat="server" Text='<%# Bind("S9MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("S9MAX"))==0 ? false : true) %>' />
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
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("S10MAX"))==0 ? false : true) %>' />
                                                            <asp:HiddenField ID="hdS10max" runat="server" Value='<%# Bind("S10MAX") %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt10Marks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txts10mark">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comAtt10Marks" ValueToCompare='<%# Bind("S10MAX") %>' ControlToValidate="txts10mark"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("S10MAX")+")" %>'
                                                                Text="*" ValidationGroup="CHK"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt10Marks" ID="cecom10AttMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="External Mark" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtESMarks" runat="server" Text='<%# Bind("EXTERMARK") %>' onblur="return CheckMark(this);" Width="50px" MaxLength="5"
                                                                Font-Bold="true" Style="text-align: center" Visible='<%# (Convert.ToInt32(Eval("MAXMARKS_E"))==0 ? false : true) %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAttEXTMARK" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txtESMarks">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comAtt2EXTERNARK" ValueToCompare='<%# Bind("MAXMARKS_E") %>' ControlToValidate="txtESMarks"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage='<%# "Marks should be less than or equal to max marks ("+Eval("MAXMARKS_E")+")" %>'
                                                                Text="*" ValidationGroup="CHK"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt2EXTERNARK" ID="cecom2AttEXTMARK" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TOTAL MARKS" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTotMarksAll" runat="server" Enabled="false" Text='<%# Bind("MARKTOT") %>' Width="50px" MaxLength="5"
                                                                Font-Bold="true" Style="text-align: center" />
                                                            <asp:HiddenField ID="hidTotMarksAll" runat="server" Value='<%# Bind("MARKTOT") %>' />

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="6%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>


                                </td>
                            </tr>
                            <tr runat="server" id="trresult" visible="false">
                                <td colspan="4">
                                    <div>
                                        <table>
                                            <tr>
                                                <td>Total Reg.Credits</td>
                                                <td>
                                                    <asp:TextBox runat="server" TabIndex="6" ID="txtregcredit" Width="90%" onkeyup="return IsNumeric(this);"></asp:TextBox>
                                                    <%--  <asp:RequiredFieldValidator ID="rfvregcredit" runat="server" ControlToValidate="txtregcredit"
                                                        Display="None" ErrorMessage="Please enter Registerd Credit" ValidationGroup="CHK"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                </td>
                                                <td>Earn Credits</td>
                                                <td>
                                                    <asp:TextBox runat="server" TabIndex="7" ID="txtearncredit" Width="90%" onblur="return Validate(this);" onkeyup="return IsNumeric(this);"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvEarncredit" runat="server" ControlToValidate="txtearncredit"
                                                        Display="None" ErrorMessage="Please enter Earn Credit" ValidationGroup="CHK"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>--%></td>
                                                <td>Result</td>
                                                <td>
                                                    <asp:DropDownList runat="server" TabIndex="8" ID="ddlResult">
                                                        <asp:ListItem Value="P">PASS</asp:ListItem>
                                                        <asp:ListItem Value="F">FAIL</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvResult" runat="server" ControlToValidate="ddlResult"
                                                        Display="None" ErrorMessage="Please Select Result" ValidationGroup="CHK"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>SGPA</td>
                                                <td>
                                                    <asp:TextBox runat="server" TabIndex="9" ID="txtsgpa" Width="90%" onkeyup="return IsNumeric(this);"></asp:TextBox>
                                                    <%--  <asp:RequiredFieldValidator ID="RFVSGPA" runat="server" ControlToValidate="txtsgpa"
                                                        Display="None" ErrorMessage="Please enter SGPA" ValidationGroup="CHK"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>--%></td>
                                                <td>CGPA</td>
                                                <td>
                                                    <asp:TextBox runat="server" TabIndex="10" ID="txtCgpa" Width="90%" onkeyup="return IsNumeric(this);"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvCGPA" runat="server" ControlToValidate="txtCgpa"
                                                        Display="None" ErrorMessage="Please enter CGPA" ValidationGroup="CHK"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>--%></td>
                                                <td>Total Obtained Mark</td>
                                                <td>
                                                    <asp:TextBox runat="server" TabIndex="11" ID="txttotobtmrk" Width="90%" onkeyup="return IsNumeric(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtotobtmrk" runat="server" ControlToValidate="txttotobtmrk"
                                                        Display="None" ErrorMessage="Please enter Total Obtained mark" ValidationGroup="CHK"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                <td>Out of Mark</td>
                                                <td>
                                                    <asp:TextBox runat="server" TabIndex="12" ID="txtOutOfmark" Width="90%" onkeyup="return IsNumeric(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvOutofmark" runat="server" ControlToValidate="txtOutOfmark"
                                                        Display="None" ErrorMessage="Please enter Out of mark" ValidationGroup="CHK"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr runat="server" id="trevent" visible="false">
                                <td colspan="3" style="padding: 10px; text-align: center;">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="true" Width="80px" TabIndex="13"
                                        Enabled="false" ValidationGroup="CHK" OnClientClick="return showSubConfirm();" OnClick="btnSubmit_Click" />&nbsp; &nbsp; &nbsp;
                                     <asp:Button ID="btnlock" runat="server" Text="Lock" Width="80px" OnClientClick="return showLockConfirm();" TabIndex="14"
                                         Enabled="false" OnClick="btnlock_Click" Font-Bold="true" />&nbsp; &nbsp; &nbsp;
                                    <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="CHK" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>

            <td valign="top" style="width: 100%; padding: 10px"></td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
    <asp:HiddenField ID="hdfDegree" runat="server" Value="" />

    <script language="javascript" type="text/javascript">

        function IsNumeric(txt) {
            var ValidChars = "0123456789.";
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

        function showSubConfirm() {
            if (Page_ClientValidate("CHK")) {
                var ret = confirm('Do you really want to submit marks for selected exam?');
                if (ret == true)
                    return true;
                else
                    return false;
            }
        }

        function showLockConfirm() {
            if (Page_ClientValidate("CHK")) {
                var ret = confirm('Do you really want to lock marks for selected exam?');
                if (ret == true)
                    return true;
                else
                    return false;
            }
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
</asp:Content>

