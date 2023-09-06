<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="assignmentMaster.aspx.cs" Inherits="Itle_assignmentMaster" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../includes/modalbox.js" type="text/javascript"></script>
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
    <script src="plugins/fastclick/fastclick.min.js"></script>


    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%=txtTopic.ClientID%>').focus();
        }
        function validate() {
            document.getElementById('txtDescription').focus();
        }
    </script>
    <style type="text/css">
        .hide_img {
            display: none;
        }

        .show_img {
            display: block;
        }
    </style>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ASSIGNMENT CREATION</h3>
                </div>
                <div>

                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:Panel ID="pnlAssignmentMaster" runat="server">
                                <div class="col-md-12">
                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                    <asp:Panel ID="pnlAssignment" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel panel-heading">Assignment Creation By Faculty</div>
                                            <div class="panel panel-body">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label>Session :</label>
                                                        </div>
                                                        <div class="col-md-10">
                                                            <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-gorup">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label>Create Date :</label>
                                                        </div>
                                                        <div class="col-md-10">
                                                            <asp:Label ID="lblCurrdate" runat="server" Font-Bold="true"> </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-gorup">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label>Course Name :</label>
                                                        </div>
                                                        <div class="col-md-10">
                                                            <asp:Label ID="lblCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-gorup">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label><span style="color: Red">*</span>Assignment Topic :</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtTopic" runat="server" OnBlur="validate();" CssClass="form-control"
                                                                ToolTip="Enter Assignment Topic" TabIndex="1"></asp:TextBox>
                                                            <%--  Enable the button so it can be played again --%>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTopic"
                                                                ErrorMessage="Assignment Topic" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-gorup">
                                                    <div class="form-group col-md-12">
                                                        <div class="col-md-2">
                                                            <label>Description :</label>
                                                        </div>
                                                        <div class="col-md-10">
                                                            <CKEditor:CKEditorControl ID="txtDescription" runat="server" Height="200"
                                                                BasePath="~/ckeditor" ToolTip="Enter Description" TabIndex="2">		                        
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

                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label><span style="color: Red">*</span>Assignment Marks:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtAMarks" onkeyup="validateNumeric(this);" runat="server"
                                                                CssClass="form-control" TabIndex="3" ToolTip="Enter Assignment Marks"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAMarks"
                                                                ErrorMessage="Enter Assignment Marks" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-6">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="form-group col-md-12">
                                                        <div class="col-md-2">
                                                            <label>Assignment Files :</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:FileUpload ID="fileUploader" runat="server" TabIndex="4" ToolTip="Click here to Select File" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Button ID="btnAttachFile" runat="server" Text="Attach File" ToolTip="Click here to Upload Files"
                                                                OnClick="btnAttachFile_Click" CssClass="btn-primary" TabIndex="5" />
                                                        </div>
                                                        <div class="col-md-3">
                                                            (Max.Size<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)
                                
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
                                                                                        <th>
                                                                                        Attachments                                                                                   
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
                                                                                <td>
                                                                                    <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                                                    <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                                                        <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="form-group col-md-12">
                                                        <div class="col-md-2">
                                                            <label><span style="color: Red">*</span>Submission Date :</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgCalResultDate" runat="server" ImageUrl="~/IMAGES/calendar.png"
                                                                        Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtSubmitDate" runat="server" CssClass="form-control" TabIndex="6"
                                                                    ValidationGroup="process" ToolTip="Enter Submission Date" Style="z-index: 0;" AutoPostBack="true" OnTextChanged="txtSubmitDate_TextChanged" />
                                                                <ajaxToolKit:CalendarExtender ID="ceResultDate" runat="server" Format="dd/MM/yyyy"
                                                                    PopupButtonID="imgCalResultDate" TargetControlID="txtSubmitDate" />
                                                                <ajaxToolKit:MaskedEditExtender ID="meeResultDate" runat="server" ErrorTooltipEnabled="true"
                                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                                    TargetControlID="txtSubmitDate" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mevResultDate" runat="server" ControlExtender="meeResultDate"
                                                                    ControlToValidate="txtSubmitDate" Display="Dynamic" EmptyValueBlurredText="*"
                                                                    EmptyValueMessage="Submission Date Required" InvalidValueBlurredMessage="*"
                                                                    InvalidValueMessage="Result Date is Invalid!!"
                                                                    IsValidEmpty="False" ValidationGroup="process" />

                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:CheckBox ID="chkSendSMS" runat="server" Text="Send SMS to Students" Font-Bold="true"
                                                                Visible="false" TabIndex="7" ToolTip="Check If Send SMS to Students" />
                                                            <asp:RequiredFieldValidator ID="reqSubmitDate" runat="server" ControlToValidate="txtSubmitDate"
                                                                ErrorMessage="Please Enter Submission Date"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label><span style="color: #FF0000">*</span>Last Time of Submission:</label>
                                                        </div>

                                                        <div class="col-md-4">
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
                                                                ControlToValidate="txtLastTimeOfSubmission" IsValidEmpty="False" EmptyValueMessage="Time is required"
                                                                InvalidValueMessage="Time is invalid" Display="Dynamic" TooltipMessage="Input a time in hh:mm:ss format"
                                                                EmptyValueBlurredText="*" InvalidValueBlurredMessage="24 Hour format" ValidationGroup="submit" />

                                                        </div>

                                                        <div class="col-md-4">
                                                            24 hour format
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group col-md-12">
                                                    <div class="col-md-4" onclick="javascript:toggleExpansion(this,'divServiceDateDetails')">
                                                        <label tabindex="9">Click here to Get Student List</label>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="divServiceDateDetails" style="display: block;">
                                                    <div class="col-sm-12 form-group">
                                                        <div class="row" style="border: solid 1px #CCCCCC">
                                                            <div style="font-weight: bold; background-color: #72A9D3; color: white" class="panel-heading">Student List</div>
                                                            <div class="table-responsive">
                                                                <table class="customers">
                                                                    <tr style="font-weight: bold; background-color: #808080; color: white">
                                                                        <th style="width: 1%; padding-left: 14px; text-align: left">
                                                                            <asp:CheckBox ID="cbHead" runat="server" ToolTip="Click here to Select All Student Name"
                                                                                onclick="totAllIDs(this)" TabIndex="10" />
                                                                        </th>
                                                                        <th style="width: 9%; text-align: center">Student Name
                                                                        </th>
                                                                        <th style="width: 3%; text-align: center">Roll No                                                                  
                                                                        </th>
                                                                        <th style="width: 3%; text-align: center">Section                                                                    
                                                                        </th>
                                                                        <th style="width: 4%; text-align: center">Mobile Number                                                                 
                                                                        </th>
                                                                        <th style="width: 9%; text-align: center">Email Id                                                                     
                                                                        </th>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="DocumentList">
                                                                <asp:Panel ID="PnlList" runat="server" ScrollBars="Vertical" Height="300px" BackColor="#FFFFFF">
                                                                    <asp:ListView ID="lvStudent" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div id="demo-grid">
                                                                                <table class="table table-bordered table-hover">
                                                                                    <thead>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="width: 3%; padding-left: 14px;">
                                                                                    <asp:CheckBox ID="chkStud" runat="server" ToolTip='<%# Eval("IDNO") %>' TabIndex="11" />
                                                                                </td>
                                                                                <td style="width: 24%; text-align: left">
                                                                                    <%# Eval("STUDNAME")%>
                                                                                </td>
                                                                                <td style="width: 10%; text-align: left">
                                                                                    <%# Eval("ROLLNO")%>                                                          
                                                                                </td>
                                                                                <td style="width: 9%; text-align: left">
                                                                                    <%# Eval("SECTIONNAME")%>                                                           
                                                                                </td>
                                                                                <td style="width: 9%; text-align: left">
                                                                                    <%# Eval("STUDENTMOBILE")%>                                                            
                                                                                </td>
                                                                                <td style="width: 10%; text-align: left">
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
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <asp:UpdatePanel ID="UpdAssignment" runat="server">
                                                            <ContentTemplate>

                                                                <div class="text-center">
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="12"
                                                                        OnClick="btnSubmit_Click" ValidationGroup="submit" ToolTip="Click here to Submit" />&nbsp;&nbsp;
                                                                         <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="13"
                                                                             OnClick="btnCancel_Click" ToolTip="Click here to Reset" />
                                                                    &nbsp;&nbsp;
                                                                        <asp:Button ID="btnViewAssignment" runat="server" Text="Assignment Report" CssClass="btn btn-outline-primary"
                                                                            OnClick="btnViewAssignment_Click" ToolTip="Click here to Show Assignment Report" TabIndex="14" />
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
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <div class="text-center">
                                                            <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="form-group col-md-12">
                                    <div class="col-sm-12" id="grid">
                                        <div class="row" style="border: solid 1px #CCCCCC">
                                            <div style="font-weight: bold; background-color: #72A9D3; color: white" class="panel-heading">Assignment List</div>
                                            <div class="table-responsive">
                                                <table class="customers">
                                                    <tr style="font-weight: bold; background-color: #808080; color: white">
                                                        <th style="width: 2%; padding-left: 8px; text-align: left">Action</th>
                                                        <th style="width: 14%; text-align: left">Subject</th>
                                                        <th style="width: 7%; text-align: left">Created Date</th>
                                                        <th style="width: 11%; text-align: left">Submission Date Time</th>
                                                        <th style="width: 10%; text-align: left">Attachment</th>
                                                        <th style="width: 10%; text-align: left">Status</th>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="DocumentList">
                                                <asp:Panel ID="pnlAssignmentList" runat="server" ScrollBars="Vertical" Height="300px" BackColor="#FFFFFF">
                                                    <asp:ListView ID="lvAssignment" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="demo-grid">
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="width: 6%; padding-left: 8px;">
                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.gif" CommandArgument='<%# Eval("AS_No") %>'
                                                                        ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("AS_NO") %>'
                                                                        ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />

                                                                </td>
                                                                <td style="width: 26%; text-align: left">
                                                                    <%# Eval("SUBJECT")%>
                                                                </td>
                                                                <td style="width: 13%; text-align: left">
                                                                    <%# Eval("ASSIGNDATE", "{0:dd-MMM-yyyy}")%>
                                                                </td>
                                                                <td style="width: 20%; text-align: left">
                                                                    <%# Eval("SUBMITDATE","{0:dd-MMM-yyyy}")%>
                                                                    -
                                                            <%# Eval("SUBMITDATE", "{0:hh:mm:ss tt}")%>
                                                                </td>
                                                                <td style="width: 17%; text-align: left">
                                                                    <img alt="Attachment" src="../IMAGES/attachment.png"
                                                                        class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                                                </td>
                                                                <td style="width: 21%; text-align: left">
                                                                    <%# GetStatus(Eval("SUBMITDATE"))%>                                                          
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

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
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
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
</asp:Content>
