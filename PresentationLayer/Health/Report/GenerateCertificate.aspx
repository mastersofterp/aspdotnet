<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GenerateCertificate.aspx.cs" Inherits="Health_Report_GenerateCertificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style2 {
            padding-left: 5px;
            padding-top: 2px;
            padding-bottom: 2px;
            width: 58%;
        }
    </style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../includes/prototype.js" type="text/javascript"></script>

    <script src="../../includes/scriptaculous.js" type="text/javascript"></script>

    <script src="../../includes/modalbox.js" type="text/javascript"></script>

    <table cellpadding="0" cellspacing="0" border="0">
        <%-- Flash the text/border red and fade in the "close" button --%>
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">CERTIFICAT DETAILS
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Shrink the info panel out of view --%>        <%--  Reset the sample so it can be played again --%>
        <tr>
            <td>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
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
                            <%--  Enable the button so it can be played again --%>
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

    <%-- Panel for the select certificate drop down list--%>
    <asp:UpdatePanel ID="upSelectCertificate" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <fieldset class="fieldset" style="width: 60%;">
                            <legend class="legend">Certificate</legend>
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td class="form_left_label" style="width: 15%;">Select Certificate <span style="color: #FF0000">*</span>
                                    </td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSelectCertificate" runat="server" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" Width="255px" OnSelectedIndexChanged="ddlSelectCertificate_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">CERTIFICATE OF RECOMMENDED,EXTENSION OR COMMUTATION OF LEAVE</asp:ListItem>
                                            <asp:ListItem Value="2">CERTIFICATE OF APPOINTMENT OF GAZETTED/NON GAZETTED CANDIDATE</asp:ListItem>
                                            <asp:ListItem Value="3">CERTIFICATE OF FITNESS TO RETURN TO DUTY</asp:ListItem>
                                            <asp:ListItem Value="4">CERTIFICATE OF FITNESS</asp:ListItem>
                                            <asp:ListItem Value="5">MEDICAL CERTIFICATE REFER 1</asp:ListItem>
                                            <asp:ListItem Value="6">MEDICAL CERTIFICATE REFER 2</asp:ListItem>
                                            <asp:ListItem Value="7">MEDICAL CERTIFICATE REFER 3</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSelectCertificate" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please Select the Certificate."
                                            ValidationGroup="Submit" ControlToValidate="ddlSelectCertificate" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- Panel for the common fields which will gonna used in certificates--%>
    <asp:UpdatePanel ID="upCommonFields" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <fieldset class="fieldset" style="width: 60%;">
                            <legend class="legend">Create Certificate</legend>
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td class="form_left_label" style="width: 25%;">Patient Name<%--<span style="color: #FF0000">*</span>--%></td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>

                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtPatientName" runat="server" ToolTip="Enter Patient Name" TabIndex="2" MaxLength="100" Width="250px"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbePatientName" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtPatientName" InvalidChars="0123456789" ValidChars="()-  ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter patient name."
                                            ValidationGroup="Submit" ControlToValidate="txtPatientName"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label" style="width: 15%;">Doctor Name
                                    </td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtDrName" runat="server" ToolTip="Enter Doctor Name" TabIndex="3" MaxLength="100" Width="250px"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeDrName" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtDrName" InvalidChars="0123456789" ValidChars="()-  ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvDrName" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter doctor name."
                                            ValidationGroup="Submit" ControlToValidate="txtDrName"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label" style="width: 15%;">Suffering From
                                    </td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtSufferingFrom" runat="server" ToolTip="Enter diseases name" TabIndex="3" MaxLength="100" Width="250px"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbSufferingFrom" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtSufferingFrom" InvalidChars="0123456789" ValidChars="()-  ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvSufferingFrom" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter disease name."
                                            ValidationGroup="Submit" ControlToValidate="txtSufferingFrom"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label" style="width: 15%;">Department
                                    </td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtDepartment" runat="server" ToolTip="Enter department name" TabIndex="4" MaxLength="100" Width="250px"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeDepartment" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtDepartment" InvalidChars="0123456789" ValidChars="()-  ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter department name."
                                            ValidationGroup="Submit" ControlToValidate="txtDepartment"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label" style="width: 15%;">Issued Date
                                    </td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtIssuedDate" runat="server" Width="50%" ToolTip="Enter the issued date" TabIndex="5"></asp:TextBox>
                                                    <asp:Image ID="ImgBntCalc" runat="server" ImageUrl="~/images/calendar.png" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                                    </ajaxToolKit:CalendarExtender>

                                                    <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                        MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt" ControlToValidate="txtFDate" EmptyValueMessage="Please Enter Date"
                                                        IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                        Display="None" Text="*" ValidationGroup="Submit">
                                                    </ajaxToolKit:MaskedEditValidator>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label" style="width: 15%;">Authorized Medical Attendant
                                    </td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtAMAttentant" runat="server" ToolTip="Enter authorized medical attendant name" TabIndex="6" MaxLength="100" Width="250px"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAMAttentant" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtAMAttentant" InvalidChars="0123456789" ValidChars="()-  ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvtxtAMAttentant" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter authorized medical attendant name."
                                            ValidationGroup="Submit" ControlToValidate="txtDepartment"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <br />

                                <%--For cetrificate Recommended,extension.commutations of leave--%>
                                <tr id="RECLeave" runat="server" visible="false">
                                    <%--<tr>--%>
                                    <td class="form_left_label" style="width: 15%;">Signature of Government Servant
                                    </td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtSignature" runat="server" ToolTip="Enter Signature of Government Servant" TabIndex="7" MaxLength="100" Width="250px"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeSignature" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtSignature" InvalidChars="0123456789" ValidChars="()-  ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvSignature" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter signature of government servant."
                                            ValidationGroup="Submit" ControlToValidate="txtSignature"></asp:RequiredFieldValidator>
                                    </td>
                                    <%--</tr>--%>
                                    <%--<tr>--%>
                                    <td class="form_left_label" style="width: 15%;">Absence Days
                                    </td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtAbsenceDays" runat="server" ToolTip="Enter Number of Absence Days" TabIndex="8" MaxLength="100" Width="250px"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAbsenceDays" runat="server" TargetControlID="txtAbsenceDays" InvalidChars="" ValidChars="(0123456789)-  ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvAbsenceDays" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter number of absence days."
                                            ValidationGroup="Submit" ControlToValidate="txtAbsenceDays"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="form_left_label" style="width: 15%;">From Date
                                    </td>
                                    <td style="width: 2%;">
                                        <b>:</b>
                                    </td>
                                    <td class="form_left_text">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtFromDate" runat="server" Width="50%" ToolTip="Enter the From date" TabIndex="9"></asp:TextBox>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                        Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFromDate">
                                                    </ajaxToolKit:CalendarExtender>

                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                        MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate" ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="medt" ControlToValidate="txtFDate" EmptyValueMessage="Please Enter Date"
                                                        IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                        Display="None" Text="*" ValidationGroup="Submit">
                                                    </ajaxToolKit:MaskedEditValidator>

                                                </td>
                                                <%--</tr>--%>
                                            </tr>
                                            <br />
                                            <%--For certificate of appointment/confirmation of gazetted/non gazetted candidate--%>
                                            <tr id="appointmentCandidate" runat="server" visible="false">
                                                <%--<tr>--%>
                                                <td class="form_left_label" style="width: 15%;">Post of
                                                </td>
                                                <td style="width: 2%;">
                                                    <b>:</b>
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtPostOf" runat="server" ToolTip="Enter Post of " TabIndex="10" MaxLength="100" Width="250px"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbePostOf" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtPostOf" InvalidChars="LowerCaseLetters,UpperCaseLetters" ValidChars="(0123456789)-  ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvPostOf" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter post of."
                                                        ValidationGroup="Submit" ControlToValidate="txtPostOf"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="form_left_label" style="width: 15%;">Age According to Own Statement
                                                </td>
                                                <td style="width: 2%;">
                                                    <b>:</b>
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtAge" runat="server" ToolTip="Enter Age According to Own Statement" TabIndex="11" MaxLength="100" Width="250px"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAge" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtAge" InvalidChars="LowerCaseLetters,UpperCaseLetters" ValidChars="(0123456789)-  ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvAge" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter age according to own statement."
                                                        ValidationGroup="Submit" ControlToValidate="txtAge"></asp:RequiredFieldValidator>
                                                </td>
                                                <%--</tr>--%>
                                                <%--<tr>--%>
                                                <td class="form_left_label" style="width: 15%;">Age by Appearance
                                                </td>
                                                <td style="width: 2%;">
                                                    <b>:</b>
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtAgeAppearance" runat="server" ToolTip="Enter age by appearance" TabIndex="12" MaxLength="100" Width="250px"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAgeAppearance" runat="server" TargetControlID="txtAgeAppearance" InvalidChars="LowerCaseLetters,UpperCaseLetters" ValidChars="(0123456789)-  ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvAgeAppearance" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter Age by Appearance."
                                                        ValidationGroup="Submit" ControlToValidate="txtAgeAppearance"></asp:RequiredFieldValidator>
                                                </td>
                                                <%--</tr>--%>
                                            </tr>

                                            <br />
                                            <%--Certificate for fitness to return to duty--%>
                                            <tr id="fitnessToReturn" runat="server" visible="false">
                                                <%--<tr>--%>
                                                <td class="form_left_label" style="width: 15%;">Signature of Government Servant
                                                </td>
                                                <td style="width: 2%;">
                                                    <b>:</b>
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtSignatureGov" runat="server" ToolTip="Enter Signature of Government Servant" TabIndex="13" MaxLength="100" Width="250px"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeSignatureGov" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtSignatureGov" InvalidChars="0123456789" ValidChars="()-  ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvSignatureGov" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter signature of government servant."
                                                        ValidationGroup="Submit" ControlToValidate="txtSignatureGov"></asp:RequiredFieldValidator>
                                                </td>
                                                <%--</tr>--%>
                                            </tr>

                                            <br />

                                            <%--Certificate for refer to--%>
                                            <tr id="referTo1" runat="server" visible="false">
                                                <%--<tr>--%>
                                                <td class="form_left_label" style="width: 15%;">Refer To
                                                </td>
                                                <td style="width: 2%;">
                                                    <b>:</b>
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtReferTO" runat="server" ToolTip="Enter Refer TO Doctor Name" TabIndex="14" MaxLength="100" Width="250px"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeReferTO" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtReferTO" InvalidChars="0123456789" ValidChars="()-  ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvReferTO" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter refer to doctor name."
                                                        ValidationGroup="Submit" ControlToValidate="txtReferTO"></asp:RequiredFieldValidator>
                                                </td>
                                                <%--</tr>--%>
                                            </tr>

                                            <br />
                                            <%--for refer to expenditure cost--%>
                                            <tr id="referTo3" runat="server" visible="false">
                                                <%-- <tr>--%>
                                                <td class="form_left_label" style="width: 15%;">Expenditure
                                                </td>
                                                <td style="width: 2%;">
                                                    <b>:</b>
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtExpenditure" runat="server" ToolTip="Enter the Expenditure in Rupees" TabIndex="15" MaxLength="100" Width="250px"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeExpenditure" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtExpenditure" InvalidChars="0123456789" ValidChars="()-  ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvExpenditure" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please enter the expenditure."
                                                        ValidationGroup="Submit" ControlToValidate="txtExpenditure"></asp:RequiredFieldValidator>
                                                </td>
                                                <%--</tr>--%>
                                            </tr>
                                        </table>
                                        <br />
                        </fieldset>

                        <br />
                        <table width="100%" cellpadding="2" cellspacing="2">
                            <tr>
                                <td class="form_left_label" style="width: 15%;"></td>
                                <td style="width: 2%;"></td>
                                <td class="form_left_text">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="16" OnClick="btnSubmit_Click" Width="80px" CausesValidation="true" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="17" Width="80px" CausesValidation="false" />
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                    <%--<asp:ValidationSummary ID="vsAdd" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </td>
        </tr>
     </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

