<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="reference.aspx.cs" Inherits="error" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdShowErr" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdTableStatus" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdFeedback" runat="server" ClientIDMode="Static" />
    <%--<div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReff"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
     </div>--%>

    <style>
        .show-error .switch label {
            background: #3c8dbc;
        }

            .show-error .switch label:hover {
                background: #3c8dbc;
            }

        .switch.Size label {
            width: 120px !important;
        }

        .switch.Size input:checked + label:after {
            transform: translateX(106px);
        }
    </style>
    <style>
        .multiselect-native-select .btn-group .btn {
            width: 150px;
        }

        .checkbox, .radio {
            margin-bottom: 9px;
        }

        input[type=radio] {
            margin-top: 3px;
        }

        input[type=checkbox] {
            margin-top: 3px;
        }

        table tbody td label {
            font-weight: 400;
        }

        .label-check label {
            font-weight: 400;
        }
    </style>

    <%--     <asp:UpdatePanel ID="updReff" runat="server">
       <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">CONFIGURATION DETAILS</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-12">
                                <asp:Label ID="Label2" runat="server">
                                    <div class=" note-div">
                                        <h5 class="heading">Note (Please Select)</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>End Sem by - 
                                            <span style="color: green;font-weight:bold">Check box check then showing End Marks Entry in Decode No. Wise if Uncheck then showing End Sem marks Entry Enroll No./Roll No. Wise.</span></span>  </p>
                                    </div>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-12">
                                <div class="row">
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Show Error</label>
                                            </div>
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="rdbDeveloper" runat="server" Text="" TextAlign="Left"
                                                        GroupName="ShowError" ValidationGroup="reference" />
                                                    Developer
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID="rdbClient" runat="server" Text="" TextAlign="Left" GroupName="ShowError"
                                                        ValidationGroup="reference" />
                                                    Client
                                                </label>
                                            </div>
                                        </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Show Error</label>
                                        </div>
                                        <div class="switch form-inline Size">
                                            <input type="checkbox" id="rdShowErr" name="switch" checked />
                                            <label data-on="Developer" data-off="Client" for="rdShowErr"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>College Code</label>
                                        </div>
                                        <asp:TextBox ID="txtCollegeCode" runat="server" MaxLength="15" ValidationGroup="reference"
                                            CssClass="form-control" Wrap="False" />
                                        <asp:RequiredFieldValidator ID="rfvCollegeCode" runat="server" ControlToValidate="txtCollegeCode"
                                            Display="None" ErrorMessage="College Code Required" ValidationGroup="reference"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Govt.</label>
                                        </div>
                                        <asp:TextBox ID="txtGovt" runat="server" MaxLength="200" ValidationGroup="reference"
                                            CssClass="form-control" Wrap="False" />
                                        <asp:RequiredFieldValidator ID="rfvGovt" runat="server" ControlToValidate="txtGovt"
                                            Display="None" ErrorMessage="Govt. Name Required"
                                            ValidationGroup="reference"></asp:RequiredFieldValidator>
                                    </div>


                                </div>

                            </div>

                            <div class="col-lg-12 col-md-12 col-12">

                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>College Name</label>
                                        </div>
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="250" ValidationGroup="reference"
                                            CssClass="form-control" onkeypress="return isSpecialKey(event)" TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="rfvCollegeName" runat="server" ControlToValidate="txtName"
                                            Display="None" ErrorMessage="College Name Required" ValidationGroup="reference"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>College Addres</label>
                                        </div>
                                        <asp:TextBox ID="txtCollegeAddress" runat="server" MaxLength="250" ValidationGroup="reference"
                                            CssClass="form-control" TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="rfvCollegeAddress" runat="server" ControlToValidate="txtCollegeAddress"
                                            Display="None" ErrorMessage="College Address Required" ValidationGroup="reference"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>



                        </div>
                    </div>



                    <div class="col-12">
                        <div class="row">
                            <div class="clearfix"></div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Phone</label>
                                </div>
                                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" Wrap="False" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>E-Mail</label>
                                </div>
                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Financial Year From</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon" id="imgCalDDDate">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtStartYear" runat="server" TabIndex="8" CssClass="form-control" />
                                    <ajaxToolKit:CalendarExtender ID="celStartYear" runat="server" Format="dd/MMM/yyyy" TargetControlID="txtStartYear"
                                        PopupButtonID="imgCalDDDate" />
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Financial Year To</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon" id="imgCalDDDate1">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtEndYear" runat="server" CssClass="form-control"></asp:TextBox>
                                    <%--<asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                    <ajaxToolKit:CalendarExtender ID="celEndYear" runat="server" Format="dd/MMM/yyyy" TargetControlID="txtEndYear"
                                        PopupButtonID="imgCalDDDate1" />
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Define Late Fee(Institute Fee)</label>
                                </div>
                                <asp:TextBox ID="txtLateFee" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Faculty User Type</label>
                                </div>
                                <asp:TextBox ID="txtFacUserType" runat="server" MaxLength="250" ValidationGroup="reference"
                                    CssClass="form-control" Wrap="False" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFacUserType"
                                    Display="None" ErrorMessage="Faculty User Type Required" ValidationGroup="reference"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Login Failure Attempt</label>
                                </div>
                                <asp:TextBox ID="txtAttempt" runat="server" MaxLength="2"
                                    ValidationGroup="reference" Wrap="False" CssClass="form-control" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftv" runat="server"
                                    FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtAttempt"
                                    ValidChars="0123456789">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Back Days Allow for Attendence</label>
                                </div>
                                <asp:TextBox ID="txtNumBckAttensAllow" runat="server" MaxLength="2"
                                    ValidationGroup="reference" Wrap="False" CssClass="form-control" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtNumBckAttensAllow"
                                    ValidChars="0123456789">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>




                            <div class="form-group col-lg-5 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <label>Communication Facility</label>
                                </div>
                                <asp:RadioButtonList ID="rdbFascility" runat="server" Height="16px"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">&nbsp;EMAIL&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2">&nbsp;SMS&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="3">&nbsp;BOTH( EMAIL &amp; SMS)&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="0">&nbsp;None&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="form-group col-lg-4 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Enrollment/ Registration No.</label>
                                </div>
                                <div class="label-check">
                                    <asp:CheckBox ID="chkEnroll" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="chkEnroll_CheckedChanged"
                                        Text="Manual Enrollment/Registration No." TextAlign="Right" Font-Bold="false" />
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12 show-error">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Enrollment/ Registration No.</label>
                                </div>
                                <div class="switch form-inline Size">
                                    <input type="checkbox" id="rdEnrollment" name="switch" checked />
                                    <label data-on="Manual" data-off="Automatic" for="rdEnrollment"></label>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12 show-error">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Reset Counter</label>
                                </div>
                                <div class="switch form-inline Size">
                                    <input type="checkbox" id="chkResetCounter" name="switch" checked />
                                    <label data-on="Yes" data-off="No" for="chkResetCounter"></label>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>IA Consolidated Marks (<span style="color: red">With Average</span>)</label>
                                </div>
                                <asp:TextBox ID="txtIAMarks" runat="server" CssClass="form-control" MaxLength="1"></asp:TextBox>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" ValidChars="1234"
                                    FilterMode="ValidChars" TargetControlID="txtIAMarks">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>PCA Consolidated Marks (<span style="color: red">With Average</span>)</label>
                                </div>
                                <asp:TextBox ID="txtPCAMarks" runat="server" CssClass="form-control" MaxLength="1"></asp:TextBox>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="12"
                                    FilterMode="ValidChars" TargetControlID="txtPCAMarks">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Admin Level Marks Entry </label>
                                </div>
                                <asp:DropDownList ID="ddlAdminLevelMarksEntry" TabIndex="2" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Please Select Admin Level Marks Entry">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Update Old Exam Data Migration </label>
                                </div>
                                <asp:DropDownList ID="ddlUpdMigrationExamData" TabIndex="3" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Please Select Update Old Exam Data Migration">
                                </asp:DropDownList>
                            </div>

                            <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Time Table Status</label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <asp:RadioButton ID="rdbHorizontal" runat="server" Text="" TextAlign="Right"
                                            GroupName="Time" ValidationGroup="reference" />
                                        Horizontal
                                    </label>
                                    <label>
                                        <asp:RadioButton ID="rdbVerticle" runat="server" Text="" TextAlign="Right" GroupName="Time"
                                            ValidationGroup="reference" />
                                        Vertical
                                    </label>
                                </div>
                            </div>--%>

                            <div class="form-group col-lg-3 col-md-6 col-12 show-error">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Time Table Status</label>
                                </div>
                                <div class="switch form-inline Size">
                                    <input type="checkbox" id="rdTableStatus" name="switch" checked />
                                    <label data-on="Horizontal" data-off="Vertical" for="rdTableStatus"></label>
                                </div>
                            </div>

                            <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Feedback Compulsory for fees</label>
                                </div>
                                <asp:RadioButtonList ID="rdbFeedback" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="True">&nbsp;&nbsp;&nbsp;Yes&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="False">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>--%>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Feedback Compulsory for fees</label>
                                </div>
                                <div class="switch form-inline">
                                    <input type="checkbox" id="rdFeedback" name="switch" checked />
                                    <label data-on="Yes" data-off="No" for="rdFeedback"></label>
                                </div>
                            </div>



                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Allow Logout Popup</label>
                                </div>
                                <%-- <div class="">
                                  <asp:RadioButtonList ID="rdbPopup" runat="server" Height="16px"
                                      RepeatDirection="Horizontal" Width="120px">
                                      <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;Yes&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                      <asp:ListItem Value="0">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                  </asp:RadioButtonList>
                              </div>--%>

                                <asp:DropDownList ID="ddlLogpop" runat="server">
                                    <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Popup Duration(in Seconds)</label>
                                </div>
                                <asp:TextBox ID="txtPopup" runat="server" MaxLength="3"
                                    ValidationGroup="reference" CssClass="form-control" Wrap="False" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Email Service ID</label>
                                </div>
                                <asp:TextBox ID="txtEmailsvc" runat="server" CssClass="form-control" Wrap="False" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>PASSWORD</label>
                                </div>
                                <asp:TextBox ID="txtEmailsvcpwd" runat="server" CssClass="form-control"
                                    Wrap="False" TextMode="Password" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>SMS Service ID</label>
                                </div>
                                <asp:TextBox ID="txtSMSsvc" runat="server" CssClass="form-control" Wrap="False" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>PASSWORD</label>
                                </div>
                                <asp:TextBox ID="txtSMSsvcpwd" runat="server" TextMode="Password" CssClass="form-control" Wrap="False" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>User Profile Sender Name</label>
                                </div>
                                <asp:TextBox ID="txtSender" MaxLength="100" runat="server" CssClass="form-control" Wrap="False" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>User Profile Subject</label>
                                </div>
                                <asp:TextBox ID="txtSubject" runat="server" MaxLength="100" CssClass="form-control" Wrap="False" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Receipt Cancelation</label>
                                </div>
                                <asp:DropDownList ID="ddlReceiptCancel" TabIndex="3" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Please Select Update Old Exam Data Migration">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Course Registration before Time Table</label>
                                </div>
                                <asp:CheckBox ID="chkCRBTimeTable" runat="server" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Active msg after Login</label>
                                </div>
                                <asp:CheckBox ID="chkpopup" runat="server" />
                            </div>

                            <div class="form-group col-lg-6 col-md-6 col-12" id="divpop" runat="server">
                                <div class="label-dynamic">
                                    <label>Active POPUP After Login</label>
                                </div>
                                <asp:TextBox ID="txtpopupmsg" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <b>
                                    <asp:Label ID="lblDecodeNumOrEnrollNo" Text="DecodeNumOrEnrollNo" runat="server"></asp:Label></b><br />
                                <asp:CheckBox ID="chkDecodeNumOrEnrollNo" Text="" onclick="chkEndSembyEnrollOrDecode(this)" runat="server" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <b>
                                    <asp:Label ID="lblCancelLateFineAuthorityPerson" Text="Cancel Late Fine Authority Person" runat="server"></asp:Label></b><br />
                                <%--<asp:TextBox ID="txtCancelLateFineAuthorityPerson" MaxLength="100" runat="server" CssClass="form-control" Wrap="False" />--%>
                                <asp:DropDownList ID="ddlCancelLateFineAuthorityPerson" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Please Select Cancel Late Fine Authority Person">
                                </asp:DropDownList>
                            </div>

                            <%--Added by Anurag Baghele on 15-02-2024--%>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <b>
                                    <asp:Label ID="lblErroLogEmail" Text="Error Log Email" runat="server"></asp:Label></b><br />
                                <asp:TextBox ID="txtErrorLogEmail" MaxLength="100" runat="server" CssClass="form-control" />
                            </div>

                        </div>
                    </div>

                    <div class="col-lg-12 col-md-12 col-12" style="display: none;">
                        <div class="sub-heading">
                            <h5>MARK ENTRY CONFIGURATION</h5>
                        </div>
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <label>Mark Entry OTP</label>
                            </div>
                            <div class="">
                                <asp:RadioButtonList ID="rdobtnMarkOTP" runat="server" Height="16px"
                                    RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;Yes&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <label>Mark Entry Save/Lock Email</label>
                            </div>
                            <div class="">
                                <asp:RadioButtonList ID="rdomarkentrysaveLockemail" runat="server" Height="16px"
                                    RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;Yes&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <label>Mark Entry Save/Lock SMS</label>
                            </div>
                            <div class="">
                                <asp:RadioButtonList ID="rdomarkentrysaveLockSMS" runat="server" Height="16px"
                                    RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;Yes&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                    </div>


                    <asp:UpdatePanel ID="updMaintenance" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maintenance Web Portal</label>
                                        </div>
                                        <asp:CheckBox ID="chkMaintenance" runat="server" Visible="false" />
                                        <asp:RadioButtonList ID="rdbMaintenance" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbMaintenance_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="YES"></asp:ListItem>
                                            <asp:ListItem Selected="True" Value="1" Text="NO"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maintenance Start Date Time</label>
                                        </div>
                                        <%-- <asp:TextBox ID="txtMaintananceDateTime" runat="server" TabIndex="6" CssClass="form-control" Width="100%"
                                            ToolTip="Please Enter Exam Time" />--%>
                                        <input type="text" id="txtMaintananceDateTime" class="form-control" disabled />
                                        <asp:HiddenField ID="hdfStartTIme" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maintenance Time Span</label>
                                        </div>
                                        <asp:DropDownList ID="ddlMainTimeSpan" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="15">15 minutes</asp:ListItem>
                                            <asp:ListItem Value="30">30 minutes</asp:ListItem>
                                            <asp:ListItem Value="45">45 minutes</asp:ListItem>
                                            <asp:ListItem Value="60">1 hour 00 min</asp:ListItem>
                                            <asp:ListItem Value="75">1 hour 15 min</asp:ListItem>
                                            <asp:ListItem Value="90">1 hour 30 min</asp:ListItem>
                                            <asp:ListItem Value="105">1 hour 45 min</asp:ListItem>
                                            <asp:ListItem Value="120">2 hour 00 min</asp:ListItem>
                                            <asp:ListItem Value="135">2 hour 15 min</asp:ListItem>
                                            <asp:ListItem Value="150">2 hour 30 min</asp:ListItem>
                                            <asp:ListItem Value="165">2 hour 45 min</asp:ListItem>
                                            <asp:ListItem Value="180">3 hour 00 min</asp:ListItem>
                                            <asp:ListItem Value="210">3 hour 30 min</asp:ListItem>
                                            <asp:ListItem Value="240">4 hour 00 min</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--<asp:TextBox ID="txtMainTimeSpan"  runat="server" CssClass="form-control" MaxLength="3" Visible="false" placeholder="Maintenance Time In Minutes" ToolTip="Time Duration for Maintenance In Minutes From Start Date Time (default:60 min if not set)"></asp:TextBox>--%>
                                        <%--<ajaxToolKit:FilteredTextBoxExtender ID="fltTimeSpan" FilterType="Numbers, Custom" ValidChars="." runat="server" TargetControlID="txtMainTimeSpan"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                    </div>
                                    <%-- <div class="col-lg-1 col-md-1">
                                            </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Alert Time Difference</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTimeDiff" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="5">5 minutes</asp:ListItem>
                                            <asp:ListItem Value="10">10 minutes</asp:ListItem>
                                            <asp:ListItem Value="15">15 minutes</asp:ListItem>
                                            <asp:ListItem Value="20">20 minutes</asp:ListItem>
                                            <asp:ListItem Value="25">25 minutes</asp:ListItem>
                                            <asp:ListItem Value="30">30 minutes</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox ID="txtTimeDiff" runat="server" Visible="false" CssClass="form-control" MaxLength="2" placeholder="Alert Time Difference In Minutes" ToolTip="Time Difference between Alert Message(Default is 15 min if not set)"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="flttxtTimeDiff" FilterType="Numbers" runat="server" TargetControlID="txtTimeDiff"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rdbMaintenance" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <%-- added by tanu for clg banner 08/12/2022--%>
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>College Logo</label>
                                </div>
                                <div class="image">
                                    <asp:Image ID="imgCollegeLogo" runat="server" ImageUrl="~/images/nophoto.jpg" BorderColor="#0099FF"
                                        BorderStyle="Solid" BorderWidth="1px" Height="105px" Width="105px" />
                                </div>
                                <asp:FileUpload ID="fuCollegeLogo" runat="server" onchange="previewCollegeLogo()" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>College Banner</label>
                                </div>
                                <div class="image">
                                    <asp:Image ID="Imagebenner" runat="server" BorderColor="#0099FF"
                                        BorderStyle="Solid" BorderWidth="1px" Height="105px" Width="105px" />
                                </div>
                                <asp:FileUpload ID="fuCollegeBanner" runat="server" onchange="previewCollegeBanner()" />
                            </div>

                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" CauseValidation="true" OnClick="btnSubmit_Click"
                            Text="Submit" ValidationGroup="reference" CssClass="btn btn-primary" OnClientClick="return showvalidate();" />
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                            Text="Cancel" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="reference" />
                    </div>
                </div>

            </div>
        </div>
    </div>
    <%--     </ContentTemplate>--%>
    <%-- <Triggers>
             <asp:PostBackTrigger ControlID="fuCollegeLogo" />
         </Triggers>--%>
    <%--   </asp:UpdatePanel>--%>
    <script>
        $(document).ready(function () {

            $("#rdbMaintenance input[type='radio']").change(function () {
                __doPostBack("rdbMaintenance", "");
            });

        });

    </script>

    <%-- <script>
        $(document).ready(function () {
            // add an event listener for when the user changes the textbox value
            $('#ctl00_ContentPlaceHolder1_txtMainTimeSpan').on('keypress', function (event) {
                // Check if the text box contains more than two characters
                alert('dd');
                if (!$.isNumeric(event.which)) {
                    event.preventDefault();
                }
                if (event.which === 46) {
                    event.preventDefault();
                } else if (!$.isNumeric(String.fromCharCode(event.which)) && event.which !== 46) {
                    event.preventDefault();
                } else {
                    var input = $(this).val() + String.fromCharCode(event.which);
                    if (Number(input) > Number(240)) {
                        event.preventDefault();
                    }

                }
            });
        });
    </script>--%>
    <script language="javascript" type="text/javascript">
        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgCollegeLogo").src = document.getElementById("ctl00_ContentPlaceHolder1_fuCollegeLogo").value;
        }
        function enterTextBox() {
            var txt = document.getElementById('ctl00_ContentPlaceHolder1_txtEmailsvcpwd');
            //txt.value = "";
        }
        function Data(val) {
            var txt = document.getElementById('ctl00_ContentPlaceHolder1_txtEmailsvcpwd');
            if (txt.value == "") {
                //txt.value = val;
            }
        }

        function showvalidate() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_chkpopup").checked == true && document.getElementById("ctl00_ContentPlaceHolder1_txtpopupmsg").value.length == 0) {
                alert("Please enter popup message!!!");
                return false;
            }


            {

                $('#hfdShowErr').val($('#rdShowErr').prop('checked'));
                $('#hfdTableStatus').val($('#rdTableStatus').prop('checked'));
                $('#hfdFeedback').val($('#rdFeedback').prop('checked'));

            }
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(function () {
                    $('#btnSubmit').click(function () {
                        showvalidate();
                    });
                });
            });
        }
        function enterTextBoxsms() {
            var txt = document.getElementById('ctl00_ContentPlaceHolder1_txtSMSsvcpwd');
            // txt.value = "";
        }
        function Datasms(val) {
            var txt = document.getElementById('ctl00_ContentPlaceHolder1_txtSMSsvcpwd');
            if (txt.value == "") {
                //txt.value = val;
            }
        }

        function chkEndSembyEnrollOrDecode(chk) {
            if (chk.checked == true) {
                //  lbl.value = chk.checked ? "End Sem by Decode No. Wise" : "End Sem by Enrollment No. / Roll No. Wise";
                $('#<%= lblDecodeNumOrEnrollNo.ClientID %>').text("End Sem by Decode No. Wise");
            } else {
                $('#<%= lblDecodeNumOrEnrollNo.ClientID %>').text("End Sem Mark Entry Enrollment No. / Roll No. Wise");
                //document.getElementById('ctl00_ContentPlaceHolder1_lblDecodeNumOrEnrollNo').value = "";
            }
        }

        ///Added Mahesh on Dated 23/06/2021
        function noCopyMouse(e) {
            var isRight = (e.button) ? (e.button == 2) : (e.which == 3);

            if (isRight) {
                alert('You are prompted to type this twice for a reason!');
                return false;
            }
            return true;
        }


        function noCopyKey(e) {
            var forbiddenKeys = new Array('c', 'x', 'v');
            var keyCode = (e.keyCode) ? e.keyCode : e.which;
            var isCtrl;


            if (window.event)
                isCtrl = e.ctrlKey
            else
                isCtrl = (window.Event) ? ((e.modifiers & Event.CTRL_MASK) == Event.CTRL_MASK) : false;


            if (isCtrl) {
                for (i = 0; i < forbiddenKeys.length; i++) {
                    if (forbiddenKeys[i] == String.fromCharCode(keyCode).toLowerCase()) {
                        alert('You are prompted to type this twice for a reason!');
                        return false;
                    }
                }
            }
            return true;
        }

    </script>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript">
        $(function () {
            $("#ctl00_ContentPlaceHolder1_chkpopup").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_divpop").show();
                } else {
                    $("#ctl00_ContentPlaceHolder1_divpop").hide();
                }
            });
        });
    </script>

    <script>
        function isSpecialKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 44 || charCode == 32)
                return true;

            return false;
        }

    </script>
    <%--Added By Rishabh On 08/11/2021--%>
    <script>
        function SetShowError(val) {
            $('#rdShowErr').prop('checked', val);
        }
        function SetTableStatus(val) {
            $('#rdTableStatus').prop('checked', val);
        }
        function SetFeedback(val) {
            $('#rdFeedback').prop('checked', val);
        }

        //added by tanu for clg banner 08/12/2022
        function LoadBanner() {
            document.getElementById("ctl00_ContentPlaceHolder1_Imagebenner").src = document.getElementById("ctl00_ContentPlaceHolder1_fuCollegeBanner").value;
        }
    </script>

    <%-- Added by Shahbaz Ahmad on 15-02-2023 --%>
    <script>
        $(document).ready(function () {

            console.log('state=' + '<%= ViewState["startTime"] %>');
            $('#txtMaintananceDateTime').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                startDate: '<%= ViewState["startTime"] == string.Empty ? "2023-02-14 12:00 AM" : ViewState["startTime"] %>',
                locale: {
                    format: 'DD/MM/YYYY hh:mm A'
                }
            });

            //$('#ctl00_ContentPlaceHolder1_txtMaintananceDateTime').daterangepicker({
            //    startDate:'< %= ViewState["startTime"]==string.Empty?"2023-02-14 12:00:00":ViewState["startTime"] %>',
            //   // endDate: '< %= ViewState["endTime"]==string.Empty?"2023-02-14 13:00:00": ViewState["endTime"] %>',
            //    DatePicker: true,
            //    singleDatePicker: true,
            //    timePicker: true,
            //    locale: {
            //        format: 'DD/MM/YYYY hh:mm A'
            //    },
            //startDate: '< %= ViewState["startTime"]==string.Empty?"2023-02-14 12:00:00": ViewState["startTime"] %>',//'2023-02-14 12:00:00',
            //endDate: '< %= ViewState["endTime"]==string.Empty?"2023-02-14 13:00:00": ViewState["endTime"] %>',
            //timePicker: true,
            //timePicker24Hour: true,
            //timePickerIncrement: 1,
            //locale: {
            //    format: 'YYYY/MM/DD HH:mm:ss'
            //},
        }
            //function (start, label) {
            //    $('#ctl00_ContentPlaceHolder1_txtMaintananceDateTime').val(start.format('DD/MM/YYYY hh:mm A'));

            //}
            );
        $("#txtMaintananceDateTime").on('apply.daterangepicker', function (ev, Picker) {
            $('#ctl00_ContentPlaceHolder1_hdfStartTIme').val(Picker.startDate.format('DD/MM/YYYY hh:mm A'));
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#txtMaintananceDateTime').daterangepicker({
                    singleDatePicker: true,
                    timePicker: true,
                    startDate: '<%= ViewState["startTime"] == string.Empty ? "2023-02-14 12:00 AM" : ViewState["startTime"] %>',
                    locale: {
                        format: 'DD/MM/YYYY hh:mm A'
                    }
                });

                //$('#ctl00_ContentPlaceHolder1_txtMaintananceDateTime').daterangepicker({
                //    // startDate: ' < %= ViewState["startTime"]==string.Empty?"2023-02-14 12:00:00":ViewState["startTime"] %>',//'2023-02-14 12:00:00',
                //    // endDate: '< %= ViewState["endTime"]==string.Empty?"2023-02-14 13:00:00": ViewState["endTime"] %>',
                //    DatePicker: true,
                //    singleDatePicker: true,
                //    timePicker: true,
                //    locale: {
                //        format: 'DD/MM/YYYY hh:mm A'
                //    },
                //startDate: '< %= ViewState["startTime"]==string.Empty?"2023/02/14 12:00:00": ViewState["startTime"] %>',//'2023-02-14 12:00:00',
                //endDate: '< %= ViewState["endTime"]==string.Empty?"2023/02/14 13:00:00": ViewState["endTime"] %>',
                //timePicker: true,
                //timePicker24Hour: true,
                //timePickerIncrement: 1,
                //locale: {
                //    format: 'YYYY/MM/DD HH:mm:ss'
                //},
            }
                //function (start, label) {
                //    $('#ctl00_ContentPlaceHolder1_txtMaintananceDateTime').val(start.format('DD/MM/YYYY hh:mm A'));

                //}
                );
            $("#txtMaintananceDateTime").on('apply.daterangepicker', function (ev, Picker) {
                $('#ctl00_ContentPlaceHolder1_hdfStartTIme').val(Picker.startDate.format('DD/MM/YYYY hh:mm A'));
            });
        });
    </script>

    <script>
        function previewCollegeLogo() {
            var preview = document.querySelector('#<%= imgCollegeLogo.ClientID %>');
            var file = document.querySelector('#<%= fuCollegeLogo.ClientID %>').files[0];

            if (file) {
                var objectURL = URL.createObjectURL(file);
                preview.src = objectURL;
            } else {
                preview.src = '~/images/nophoto.jpg';
            }
        }
    </script>

    <script>
        function previewCollegeBanner() {
            var preview = document.querySelector('#<%= Imagebenner.ClientID %>');
            var file = document.querySelector('#<%= fuCollegeBanner.ClientID %>').files[0];

            if (file) {
                var objectURL = URL.createObjectURL(file);
                preview.src = objectURL;
            } else {
                preview.src = '~/images/nophoto.jpg';
            }
        }
    </script>

</asp:Content>
