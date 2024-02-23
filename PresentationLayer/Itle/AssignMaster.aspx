<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AssignMaster.aspx.cs" Inherits="Itle_assignmentMaster" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../includes/modalbox.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>
    <script src="plugins/jQuery/jQuery-2.2.0.min.js"></script>
    <script src="plugins/jQuery/jquery.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="plugins/jQuery/jquery_ui_min/jquery-ui.min.js"></script>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="plugins/jQuery/jQuery-2.2.0.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="bootstrap/font-awesome-4.6.3/css/font-awesome.min.css" />
    <link href="dist/css/AdminLTE.min.css" rel="stylesheet" />
    <script src="dist/js/app.min.js"></script>
    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css">
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>
    <script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <script src="plugins/fastclick/fastclick.min.js"></script>--%>
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>


    <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%=txtTopic.ClientID%>').focus();
        }
        function validate() {
            document.getElementById('txtDescription').focus();
        }
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             window.history.replaceState('', '', window.location.href) // it prevent page refresh to firing the event again
         })
</script>
    <style type="text/css">
        .hide_img {
            display: none;
        }

        .show_img {
            display: block;
        }

        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ASSIGNMENT CREATION</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlAssignmentMaster" runat="server">
                        <asp:Panel ID="pnlAssignment" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <%--<div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Assignment Creation By Faculty</h5>
                                        </div>
                                    </div>--%>
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Create Date :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCurrdate" runat="server" Font-Bold="true"> </asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-12 mt-2">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Course Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCourseName" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Assignment Topic</label>
                                        </div>
                                        <asp:TextBox ID="txtTopic" runat="server" OnBlur="validate();" CssClass="form-control" TextMode="MultiLine"
                                            ToolTip="Enter Assignment Topic" TabIndex="1"></asp:TextBox>
                                        <%--  Enable the button so it can be played again --%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTopic"
                                            ErrorMessage="Please Enter Assignment Topic" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <label>Description</label>
                                        </div>
                                        <div class="table table-responsive">
                                            <CKEditor:CKEditorControl ID="txtDescription" runat="server" Height="150"
                                                BasePath="~/plugins/ckeditor" ToolTip="Enter Description" TabIndex="2">		                        
                                            </CKEditor:CKEditorControl>
                                            <%--<FTB:FreeTextBox ID="txtDescription" runat="server" Height="250px" Width="100%" Focus="true"
                                                    Text="&nbsp;">
                                                </FTB:FreeTextBox>--%>
                                            <%--<ajaxToolkit:CascadingDropDown ID="cddDept" runat="server" TargetControlID="ddlBranch"
                                                Category="City" PromptText="Please Select" LoadingText="[Loading...]" 
                                                ServicePath="~/WebService.asmx"
                                                ServiceMethod="GetBranch" ParentControlID="ddlDept" />--%>
                                        </div>
                                    </div>


                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Submission From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtsubfromdate" runat="server" CssClass="form-control" TabIndex="3"
                                                ValidationGroup="process" ToolTip="Enter Due Date" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgCalResultDate" TargetControlID="txtsubfromdate" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                TargetControlID="txtsubfromdate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
                                                ControlToValidate="txtsubfromdate" Display="None"
                                                EmptyValueMessage="Submission From Date Required"
                                                InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy]"
                                                IsValidEmpty="False" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Submission From  Time<small style="color: red;">(24 hour format)</small></label>
                                        </div>
                                        <asp:TextBox ID="txtsubfromtime" runat="server" CssClass="form-control" TabIndex="4"
                                            Text="" ToolTip="Enter Due Time Of Submission" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                            TargetControlID="txtsubfromtime" WatermarkText="HH:MM:SS" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                            TargetControlID="txtsubfromtime"
                                            Mask="99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" ErrorTooltipEnabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                                            ControlExtender="MaskedEditExtender4"
                                            ControlToValidate="txtsubfromtime" IsValidEmpty="False" EmptyValueMessage="Submission From Time Is  Required"
                                            InvalidValueMessage=" Submission From Time is invalid" Display="Dynamic" TooltipMessage="Input a time in hh:mm:ss format"
                                            EmptyValueBlurredText="*" InvalidValueBlurredMessage="24 Hour format" ValidationGroup="submit" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Due Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" TabIndex="5"
                                                ValidationGroup="process" ToolTip="Enter Due Date" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgCalResultDate" TargetControlID="txtDueDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                TargetControlID="txtDueDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtDueDate" Display="Dynamic" EmptyValueBlurredText="*"
                                                EmptyValueMessage="Due Date Required" InvalidValueBlurredMessage="*"
                                                InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy]"
                                                IsValidEmpty="False" ValidationGroup="submit" />
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Due Time<small style="color: red;">(24 hour format)</small></label>
                                        </div>
                                        <asp:TextBox ID="txtduetime" runat="server" CssClass="form-control" TabIndex="6"
                                            Text="" ToolTip="Enter Due Time Of Submission" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                            TargetControlID="txtduetime" WatermarkText="HH:MM:SS" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                            TargetControlID="txtduetime"
                                            Mask="99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" ErrorTooltipEnabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                            ControlExtender="MaskedEditExtender2"
                                            ControlToValidate="txtduetime" IsValidEmpty="False" EmptyValueMessage="Due Time is required"
                                            InvalidValueMessage="Due Time is invalid" Display="Dynamic" TooltipMessage="Input a time in hh:mm:ss format"
                                            EmptyValueBlurredText="*" InvalidValueBlurredMessage="24 Hour format" ValidationGroup="submit" />

                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Cut-Off Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtSubmitDate" runat="server" CssClass="form-control" TabIndex="7"
                                                ValidationGroup="process" ToolTip="Enter Cut-Off  Date" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="ceResultDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgCalResultDate" TargetControlID="txtSubmitDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeResultDate" runat="server" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                TargetControlID="txtSubmitDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevResultDate" runat="server" ControlExtender="meeResultDate"
                                                ControlToValidate="txtSubmitDate" Display="Dynamic"
                                                InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy]"
                                                ValidationGroup="submit" />
                                            <%--EmptyValueBlurredText="*"
                                                EmptyValueMessage="Submission Date Required" InvalidValueBlurredMessage="*"  IsValidEmpty="False"--%>
                                        </div>
                                        <asp:CheckBox ID="chkSendSMS" runat="server" Text="Send SMS to Students" Font-Bold="true"
                                            Visible="false" TabIndex="7" ToolTip="Check If Send SMS to Students" />
                                        <%-- <asp:RequiredFieldValidator ID="reqSubmitDate" runat="server" ControlToValidate="txtSubmitDate"
                                            ErrorMessage="Please Enter Submission Date"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Cut-Off Time <small style="color: red;">(24 hour format)</small></label>
                                        </div>
                                        <asp:TextBox ID="txtLastTimeOfSubmission" runat="server" CssClass="form-control" TabIndex="8"
                                            Text="" ToolTip="Enter Last Time Of Submission" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                                            TargetControlID="txtLastTimeOfSubmission" WatermarkText="HH:MM:SS" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtenderEndTime" runat="server"
                                            TargetControlID="txtLastTimeOfSubmission"
                                            Mask="99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" ErrorTooltipEnabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidatorEndTime" runat="server"
                                            ControlExtender="MaskedEditExtenderEndTime"
                                            ControlToValidate="txtLastTimeOfSubmission" IsValidEmpty="True"
                                            InvalidValueMessage="Time is invalid" Display="Dynamic" TooltipMessage="Input a time in hh:mm:ss format"
                                            EmptyValueBlurredText="*" InvalidValueBlurredMessage="24 Hour format" ValidationGroup="submit" />
                                        <%--EmptyValueMessage="Time is required"--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Assignment Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtAMarks" onkeyup="validateNumeric(this);" runat="server"
                                            CssClass="form-control" TabIndex="9" ToolTip="Enter Assignment Marks"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAMarks"
                                            Display="None" ErrorMessage="Enter Assignment Marks" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Assignment Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddldoc" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                            TabIndex="10" CssClass="form-control" data-select2-enable="true" ToolTip="Select Document Type" OnSelectedIndexChanged="ddldoc_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Description based</asp:ListItem>
                                            <asp:ListItem Value="2">Upload Based</asp:ListItem>
                                        </asp:DropDownList>



                                    </div>

                                </div>
                            </div>



                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divmaxnooffile" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>
                                                file Types</label>

                                        </div>
                                        <asp:ListBox ID="ddlextension" runat="server" AppendDataBoundItems="true" TabIndex="11" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>


                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divfiletype" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>
                                                Max No Of File
                                            </label>
                                            <asp:TextBox ID="txtmaxnooffile" runat="server" CssClass="form-control" ToolTip="Enter Max No Of File." MaxLength="1" TabIndex="12"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtmaxnooffile" ValidChars="123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Remind  Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtRdate" runat="server" CssClass="form-control" TabIndex="13"
                                                ValidationGroup="process" ToolTip="Enter Remind  Date" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgCalResultDate" TargetControlID="txtRdate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeResultDate1" runat="server" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                TargetControlID="txtRdate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="meeResultDate1"
                                                ControlToValidate="txtRdate" Display="None" EmptyValueMessage="Remind  Date Is  Required"
                                                InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy]" IsValidEmpty="false"
                                                ValidationGroup="submit" />

                                        </div>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Send SMS to Students" Font-Bold="true"
                                            Visible="false" TabIndex="7" ToolTip="Check If Send SMS to Students" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Remind  Time <small style="color: red;">(24 hour format)</small></label>
                                        </div>
                                        <asp:TextBox ID="txtRtime" runat="server" CssClass="form-control" TabIndex="14"
                                            Text="" ToolTip="Enter Remind  Time" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server"
                                            TargetControlID="txtLastTimeOfSubmission" WatermarkText="HH:MM:SS" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                            TargetControlID="txtRtime"
                                            Mask="99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" ErrorTooltipEnabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                            ControlExtender="MaskedEditExtender5"
                                            ControlToValidate="txtRtime" IsValidEmpty="false" EmptyValueMessage="Remind  Time Is  Required"
                                            InvalidValueMessage="Time is invalid" Display="None" TooltipMessage="Input a time in hh:mm:ss format"
                                            InvalidValueBlurredMessage="24 Hour format" ValidationGroup="submit" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">

                                        <div class="label-dynamic">
                                            <label>Assignment Files <small style="color: red;">(Max.Size<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)</small></label>
                                        </div>
                                        <asp:FileUpload ID="fileUploader" runat="server" ToolTip="Click here to Select File" />

                                        <asp:Button ID="btnAttachFile" runat="server" Text="Attach File" ToolTip="Click here to Upload Files"
                                            OnClick="btnAttachFile_Click" CssClass="btn btn-primary mt-1" TabIndex="15" />

                                    </div>
                                </div>
                            </div>


                            <div id="divAttch" runat="server" style="display: none">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Panel ID="pnlAttachmentList" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvCompAttach" runat="server">
                                                    <LayoutTemplate>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>Action
                                                                    </th>
                                                                    <th id="divattach" runat="server">Attachments  
                                                                    </th>
                                                                    <th id="divattachblob" runat="server" visible="false">Attachments
                                                                    </th>
                                                                    <th id="divDownload" runat="server" visible="false">Download
                                                                    </th>
                                                                    <th id="divBlobDownload" runat="server" visible="false">Download
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkRemoveAttach" runat="server" CommandArgument='<%# Eval("ATTACH_ID")%>'
                                                                    OnClick="lnkRemoveAttach_Click" CssClass="mail_pg">Remove</asp:LinkButton>

                                                                <ajaxToolKit:ConfirmButtonExtender ID="CnfDrop" runat="server"
                                                                    ConfirmText="Are you Sure, Want to Remove.?" TargetControlID="lnkRemoveAttach">
                                                                </ajaxToolKit:ConfirmButtonExtender>
                                                            </td>
                                                            <td id="attachfile" runat="server">
                                                                <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILE_PATH"))%>'><%# Eval("FILE_NAME")%></asp:HyperLink>
                                                                --%>

                                                                <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                                <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                                    <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                            </td>
                                                            <td id="attachblob" runat="server" visible="false">
                                                                <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILE_PATH"))%>'><%# Eval("FILE_NAME")%></asp:HyperLink>
                                                                --%>

                                                                <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                                <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                                --%>      <%# Eval("FILE_PATH")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                            </td>


                                                            <td id="tdDownloadLink" runat="server" visible="false">


                                                                <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                                <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                                --%>      <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                            
                                                            </td>

                                                            <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                                <asp:UpdatePanel ID="updPreview" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILE_NAME") %>'
                                                                            data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("FILE_NAME") %>' Visible='<%# Convert.ToString(Eval("FILE_NAME"))==string.Empty?false:true %>'></asp:ImageButton>

                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Select Section</label>
                                </div>
                                <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" TabIndex="16"
                                    AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Section" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                            </div>

                            <div class="col-12" id="divServiceDateDetails">
                                <div class="sub-heading">
                                    <h5>Student List</h5>
                                </div>
                                <asp:Panel ID="PnlList" runat="server">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" ToolTip="Click here to Select All Student Name"
                                                                onclick="totAllIDs(this)" TabIndex="17" />
                                                        </th>
                                                        <th>RRN</th>
                                                        <th>Student Name</th>
                                                        <th>Section</th>
                                                        <th>Mobile Number</th>
                                                        <th>Email Id</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkStud" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>                                                          
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SECTIONNAME")%>                                                           
                                                </td>
                                                <td>
                                                    <%# Eval("STUDENTMOBILE")%>                                                            
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMailTo" runat="server" Text='<%# Eval("EMAILID")%>'></asp:Label>
                                                </td>
                                                <%-- <td>                                        
                                            <asp:LinkButton ID="lnkResendSms" runat="server" OnClick="lnkResendSms_Click"  
                                            ToolTip='<%# Eval("IDNO") %>'></asp:LinkButton>                                       
                                        </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                                <asp:UpdatePanel ID="UpdAssignment" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 btn-footer mt-4">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="18"
                                                OnClick="btnSubmit_Click" ValidationGroup="submit" ToolTip="Click here to Submit" />
                                            <asp:Button ID="btnViewAssignment" runat="server" Text="Assignment Report" CssClass="btn btn-info" TabIndex="19"
                                                OnClick="btnViewAssignment_Click" ToolTip="Click here to Show Assignment Report" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="20"
                                                OnClick="btnCancel_Click" ToolTip="Click here to Reset" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnCancel" />
                                        <asp:PostBackTrigger ControlID="lvStudent" />
                                        <asp:PostBackTrigger ControlID="btnSubmit" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                            </div>
                        </asp:Panel>

                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Assignment List</h5>
                            </div>

                            <asp:Panel ID="pnlAssignmentList" runat="server">
                                <asp:ListView ID="lvAssignment" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action</th>
                                                    <th>Subject</th>
                                                    <th>Created Date</th>
                                                    <th>Submission Date Time</th>
                                                    <%--<th>Attachment</th>--%>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("AS_No") %>'
                                                    ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("AS_NO") %>'
                                                                        ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />

                                            </td>
                                            <td>
                                                <%# Eval("SUBJECT")%>
                                            </td>
                                            <td>
                                                <%# Eval("ASSIGNDATE", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBMITDATE","{0:dd-MMM-yyyy}")%>
                                                                    -
                                                            <%# Eval("SUBMITDATE", "{0:hh:mm:ss tt}")%>
                                            </td>
                                            <%--<td>
                                                <img alt="Attachment" src="../IMAGES/attachment.png"
                                                    class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                            </td>--%>
                                            <td>
                                                <%# GetStatus(Eval("SUBMITDATE"))%>                                                            
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function totAllIDs(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;

                    }
                    else {
                        e.checked = false;

                    }
                }
            }
        }

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });


    </script>
    <script>
        function checkout() {
            var checkBoxes = document.getElementsByClassName('ddlextension');
            var nbChecked = 0;
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    nbChecked++;
                };
            };
            if (nbChecked > 3) {
                alert('Please Select Maximun 3 Group.');
                return false;
            } else if (nbChecked == 0) {
                alert('Please, select at least one Group!');
                return false;
            } else {
                //Do what you need for form submission, if needed...
            }
        }

    </script>
    <script type="text/javascript">
        function CloseModal() {
            $("#preview").modal("hide");
        }
        function ShowModal() {
            $("#preview").modal("show");
        }
</script>

    <div class="modal fade" id="preview" role="dialog" style="display: none; margin-left: -100px;">
        <div class="modal-dialog text-center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content" style="width: 700px;">
                        <div class="modal-header">
                            <%--   <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            <h4 class="modal-title">Document</h4>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12">

                                <asp:Literal ID="ltEmbed" runat="server" />

                                <%--<iframe runat="server" style="width: 100; height: 100px" id="iframe2"></iframe>--%>

                                <%--<asp:Image ID="imgpreview" runat="server" Height="530px" Width="600px"  />--%>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%-- <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>--%>
                            <asp:HiddenField ID="hdnfilename" runat="server" />
                            <asp:Button ID="BTNCLOSE" runat="server" Text="CLOSE" OnClick="BTNCLOSE_Click" OnClientClick="CloseModal();return true;" CssClass="btn btn-outline-danger" />
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
