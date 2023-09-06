<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UpdateRegistrationStatus.aspx.cs" Inherits="ACADEMIC_UpdateRegistrationStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../includes/prototype.js" type="text/javascript"></script>

    <script src="../includes/scriptaculous.js" type="text/javascript"></script>

    <script src="../includes/modalbox.js" type="text/javascript"></script>
    <%--<asp:UpdatePanel ID="updSelection" runat="server">

        <ContentTemplate>--%>
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
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <table cellpadding="0" cellspacing="0" width="100%">
        <div id="divMsg" runat="server">
        </div>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">Update Registration Status &nbsp;
                        <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
        <tr>
            <td align="center" style="color: #FF0000">
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
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
            </td>
        </tr>
    </table>

    <table cellpadding="2" cellspacing="2" width="100%">


        <tr>
            <td>
                <table cellpadding="1" cellspacing="1" width="100%">


                    <tr>
                        <%--<asp:Panel ID="pnlId" runat="server" >--%>
                        <td class="form_left_label" style="width: 232px">
                            <span class="validstar">* </span>Search Student By Registration No. :
                        </td>
                        <td class="form_left_text" width="10%">
                            <asp:TextBox ID="txtStudent" runat="server" MaxLength="100" TabIndex="1" Width="286px" Style="margin-left: 7px" />

                        </td>
                        <td align="left">
                            <a href="#" title="Search Student for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;">
                                <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="1"
                                    AlternateText="Search Student by  Name or Reg No." ToolTip="Search Student by Name or Reg No." />
                            </a>
                        </td>
                        <%-- </asp:Panel>--%>

                        <td>
                            <asp:Label ID="lblMsg" runat="server" SkinID="Errorlbl"></asp:Label>
                        </td>
                    </tr>

                </table>

            </td>
        </tr>
        <%-- <tr>
                    <td width="10%" class="form_button">
                        <asp:Button ID="btnShow" runat="server" Text="Show" Width="80px" TabIndex="2" ValidationGroup="Show" Enabled="false" />
                        <asp:RequiredFieldValidator ID="rfvStudents" runat="server" ErrorMessage="Please Enter Student Name"
                            ControlToValidate="txtStudent" Display="None" SetFocusOnError="True" ValidationGroup="Show" />
                        <asp:ValidationSummary ID="valShowSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Show" />
                    </td>
                </tr>--%>
        <tr>
            <td>
                <asp:UpdatePanel ID="updSelection" runat="server">

                    <ContentTemplate>
                        <asp:Panel ID="pnlDetails" runat="server">
                            <fieldset class="fieldset">
                                <legend class="legend">Student Details</legend>
                                <table width="100%" cellpadding="2" cellspacing="2">
                                    <tr>

                                        <td class="form_left_label" style="width: 232px">
                                            <span class="validstar">*</span>Admission batch :
                                        </td>
                                        <td class="form_left_text" width="40%">
                                            <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="true" Width="300px" TabIndex="1" Style="margin-left: 0px">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAdmbatch" runat="server" ControlToValidate="ddlAdmbatch"
                                                Display="None" ValidationGroup="Show" InitialValue="0"
                                                ErrorMessage="Please Select Admission batch"></asp:RequiredFieldValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Name :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtName" runat="server" MaxLength="100" Width="218px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter Name"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                                Display="None" ValidationGroup="Report"
                                                ErrorMessage="Please Select Name"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Father's Name :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtFatherName" runat="server" MaxLength="100" Width="216px"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtFatherName" FilterType="Custom" FilterMode="InvalidChars"
                                                 InvalidChars="1234567890,!@#$%^&*()_+"/>
                                            <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" ControlToValidate="txtFatherName"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter Father Name"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Mother's Name :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtMotherName" runat="server" MaxLength="100" Width="217px"></asp:TextBox>
                                              <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                TargetControlID="txtMotherName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890,!@#$%^&*()_+" />
                                            <asp:RequiredFieldValidator ID="rfvMotherName" runat="server" ControlToValidate="txtMotherName"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter Mother Name"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Sex :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:RadioButton ID="rdoMale" runat="server" Text="Male" GroupName="Sex" Checked="true" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoFemale" runat="server" Text="Female" GroupName="Sex" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Date Of Birth :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtDateOfBirth" runat="server" ValidationGroup="Show" />
                                            <asp:Image ID="imgCalDateOfBirth" runat="server" src="../images/calendar.png" Width="16px" />
                                            <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateOfBirth" PopupButtonID="txtDateOfBirth" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" EmptyValueMessage="Please Enter DateOfBirth"
                                                ControlExtender="meeDateOfBirth" ControlToValidate="txtDateOfBirth" IsValidEmpty="true"
                                                InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="Show" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDateOfBirth"
                                                Display="None" ErrorMessage="Please Enter Date of Birth" ValidationGroup="Show"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Class Admitted :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtadmitted" runat="server" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdmitted" runat="server" ControlToValidate="txtadmitted"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter Class Admitted"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>College Name/Department :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" Width="352px" TabIndex="1" Style="margin-left: 0px">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>

                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ValidationGroup="Show" InitialValue="0"
                                                ErrorMessage="Please Select College/Department"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Class X Roll No with Year :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtRollNo" runat="server" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRollNo" runat="server" ControlToValidate="txtRollNo"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter Roll No."></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Board/University from which last examination Passed :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtBoard" runat="server" MaxLength="50" Width="269px"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                TargetControlID="txtBoard" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890!@#$%^&*()_+" />
                                            <asp:RequiredFieldValidator ID="rfvtxtBoard" runat="server" ControlToValidate="txtBoard"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter Board/University"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>School/College/University where studied last :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtlastSchool" runat="server" MaxLength="100" Width="272px"></asp:TextBox>
                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                TargetControlID="txtlastSchool" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890!@#$%^&*()_+"/>

                                            <asp:RequiredFieldValidator ID="rfvLastSchool" runat="server" ControlToValidate="txtlastSchool"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter School/College/University"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Roll No of Last Examination passed :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtLastRoll" runat="server" MaxLength="15" Width="125px"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                TargetControlID="txtLastRoll" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars=",!@#$%^&*()_+" />
                                            <asp:RequiredFieldValidator ID="rfvLastRoll" runat="server" ControlToValidate="txtLastRoll"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter Roll No &  Year"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Year of Last Examination passed :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtlastExmYear" runat="server" MaxLength="4" Width="125px" onchange="CheckFutureDate(this);"></asp:TextBox>
                                                   <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers"
                                                TargetControlID="txtlastExmYear">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvLastExmYear" runat="server" ControlToValidate="txtlastExmYear"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter Year"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">&nbsp;&nbsp;&nbsp; Mobile phone :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtMobile" runat="server" MaxLength="12" Width="126px"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtPin" runat="server" FilterType="Numbers"
                                                TargetControlID="txtMobile">
                                            </ajaxToolKit:FilteredTextBoxExtender>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">&nbsp;&nbsp;&nbsp; Email id :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="35" Width="197px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                                ErrorMessage="*" ControlToValidate="txtEmail"
                                                ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                runat="server" ErrorMessage="Please Enter Valid Email ID"
                                                ValidationGroup="Show" ControlToValidate="txtEmail"
                                                CssClass="requiredFieldValidateStyle"
                                                ForeColor="Red"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                            </asp:RegularExpressionValidator>


                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>State of Domicile :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtDomecial" runat="server" MaxLength="50" Width="127px"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                TargetControlID="txtDomecial" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890,!@#$%^&*()_+.></{}" />
                                            <asp:RequiredFieldValidator ID="rfvDomecial" runat="server" ControlToValidate="txtDomecial"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Enter Domecial"></asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">
                                            <span class="validstar">* </span>Nationality :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:DropDownList ID="ddlNationality" runat="server" AppendDataBoundItems="true" Width="300px" TabIndex="1" Style="margin-left: 7px">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>

                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvNationality" runat="server" ControlToValidate="ddlNationality"
                                                Display="None" ValidationGroup="Show" InitialValue="0"
                                                ErrorMessage="Please Select Nationality"></asp:RequiredFieldValidator>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">&nbsp;&nbsp;&nbsp; Migration Certificate submitted? :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:RadioButtonList ID="rdoMigration" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoMigration_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>

                                    </tr>


                                    <tr>
                                        <td class="form_left_label" width="20%">&nbsp;&nbsp;&nbsp; Registration Number :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtRegistrationNo" runat="server" MaxLength="50"></asp:TextBox>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">&nbsp;&nbsp;&nbsp; Registration Status :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:RadioButtonList ID="rdoRegStatus" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">Provisional</asp:ListItem>
                                                <asp:ListItem Value="2">Permanent</asp:ListItem>
                                            </asp:RadioButtonList>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">&nbsp;&nbsp; Card Serial No :
                                        </td>
                                        <td class="form_left_label" width="40%">
                                            <asp:TextBox ID="txtCardSerialNo" runat="server" MaxLength="50"></asp:TextBox>

                                        </td>
                                    </tr>

                                    <tr>

                                        <td width="10%" class="form_button" align="center">
                                            <asp:Button ID="btnGenerateRegNo" runat="server" Text="Update" Width="110px" OnClick="btnGenerateRegNo_Click" ValidationGroup="Show" />
                                            &nbsp;
                                        <asp:Button ID="btnPrintCard" runat="server" Text="Print card" Width="80px" ValidationGroup="Report" OnClick="btnPrintCard_Click" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Show" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Report" />
                                        </td>

                                    </tr>
                                </table>


                                <%-- <tr>

                                    <td width="10%" class="form_button" align="center">
                                        <asp:Button ID="btnGenerateRegNo" runat="server" Text="Generate Registration No & Save" Width="210px" OnClick="btnGenerateRegNo_Click"  ValidationGroup="Show" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnPrintCard" runat="server" Text="Print card" Width="80px"   ValidationGroup="Show" OnClick="btnPrintCard_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Show" />
                                    </td>--%>


                                <%--  </tr>--%>
                            </fieldset>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div id="divdemo2" style="display: none; height: 550px">
        <asp:UpdatePanel ID="updEdit" runat="server">
            <ContentTemplate>
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td>Search Criteria:
                        </td>
                        <td>
                            <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                            <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="Reg No" GroupName="edit"
                                Checked="True" />
                            <%--<asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" />
                                <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" />
                                
                                <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit" Checked="True" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>Search String :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" />
                            <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
                                <ProgressTemplate>
                                    <asp:Image ID="imgProg" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                    Loading.. Please Wait!
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="100%">
                            <asp:ListView ID="lvStudent" runat="server">
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="titlebar">
                                            Login Details
                                        </div>
                                        <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <thead>
                                                <tr class="header">
                                                    <th style="width: 20%">Name
                                                    </th>
                                                    <th style="width: 20%">IdNo
                                                    </th>
                                                    <th style="width: 20%">Reg No.
                                                    </th>
                                                    <th style="width: 30%">Branch
                                                    </th>
                                                    <th style="width: 10%">Semester
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                    <div class="listview-container">
                                        <div id="demo-grid" class="vista-grid">
                                            <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td style="width: 20%">
                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                OnClick="lnkId_Click"></asp:LinkButton>
                                        </td>
                                        <td style="width: 15%">
                                            <%# Eval("idno")%>
                                        </td>
                                        <td style="width: 20%">
                                            <%# Eval("ENROLLNO")%>
                                        </td>
                                        <td style="width: 30%">
                                            <%# Eval("longname")%>
                                        </td>
                                        <td style="width: 15%">
                                            <%# Eval("semesterno")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </div>

    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript" language="javascript">

        function submitPopup(btnsearch) {
            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "name"
            else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked == true)
                rbText = "enrollmentno";

            searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

            __doPostBack(btnsearch, rbText + ',' + searchtxt);

            return true;
        }

        function ClearSearchBox(btncancelmodal) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
         __doPostBack(btncancelmodal, '');
         return true;
        }



        function CheckFutureDate(id) {
            //Created By Mr. Manish Walde
            // Return today's date and time
            ////var date = new Date();
            //var today = $.datepicker.formatDate("dd-mm-yy", date);

            // returns the month (from 0 to 11)
            ////var month = date.getMonth() + 1;
            //month = month - 1;
            //typeof month;

            ////if (month.toString().length == 1)
            ////    month = "0" + month;

            // returns the day of the month (from 1 to 31)
            //var day = date.getDate();
            //if (String(day).length == 1)
            //    day = "0" + day;

            // returns the year (four digits)

            var enterYear = id.value;
            if (String(enterYear).length < 4) {
                alert('Year is not in correct format.');
                id.value = '';

                id.focus();
            }
            else {
                // Return today's date and time
                var date = new Date();

                // returns the year (four digits)
                var year = date.getFullYear();

                if (enterYear > year) {
                    alert('Future date cannot be entered as last exam passed.');
                    id.value = '';

                    id.focus();
                }
            }
        }
    </script>

    <%--     </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnPrintCard" />
        </Triggers>
        </asp:UpdatePanel>--%>
</asp:Content>

