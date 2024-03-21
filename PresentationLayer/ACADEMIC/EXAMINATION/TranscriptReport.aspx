<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TranscriptReport.aspx.cs" Inherits="ACADEMIC_EXAMINATION_TranscriptReportUG" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="upupdDetained" runat="server" AssociatedUpdatePanelID="updpnlExam">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TRANSCRIPT/CONSOLIDATED GRADE CARD REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="submit" ToolTip="Enter Exam Roll No." CssClass="form-group"></asp:TextBox>
                                            <div class="">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click"
                                                    ValidationGroup="submit" />
                                                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch"
                                                    Display="None" ErrorMessage="Please Enter Reg no." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                    ShowMessageBox="True" Width="20%" ShowSummary="False" ValidationGroup="submit" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                    ShowMessageBox="True" Width="20%" ShowSummary="False" ValidationGroup="Submit" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSession" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True"
                                            CssClass="form-control" AutoPostBack="true"
                                            TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="SubmitTranscript"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="MarkEntryStatus"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Status"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lbSpecilization" runat="server" Text="" Visible="false"> <sup>* </sup>Specilization : </asp:Label>
                                        </div>
                                        <%--<b>Specilization :</b><a class="pull-right" style="padding-left:10px;"></a>--%>

                                        <asp:TextBox ID="txtSpecilization" runat="server" ValidationGroup="Submit" ToolTip="Enter Exam Roll No." Visible="false" placeholder="Specilization"></asp:TextBox><br />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSpecilization"
                                               Display="None" ErrorMessage="Please Enter Specilization " ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="txtMob_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txtSpecilization" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="0123456789 ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lbResult" runat="server" Text="" Visible="false"> <sup>* </sup>Overall Result : </asp:Label>
                                        </div>

                                        <asp:TextBox ID="txtresult" runat="server" ValidationGroup="Submit" ToolTip="Enter Exam Roll No." Visible="false" placeholder="Overall Result"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtresult"
                                            Display="None" ErrorMessage="Please Enter Result" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            Enabled="True" TargetControlID="txtresult" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="0123456789 ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divdate" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDateofIssue" runat="server" Text="Date of Issue " Visible="false"> </asp:Label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue" id="imgFromDate1"></i>
                                            </div>
                                            <asp:TextBox ID="txtDateofIssue" runat="server" CssClass="form-control"
                                                ValidationGroup="Submit" TabIndex="7" ToolTip="Please Select Date"></asp:TextBox>

                                            <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgFromDate1" TargetControlID="txtDateofIssue" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                                TargetControlID="txtDateofIssue" Mask="99/99/9999" MessageValidatorTip="true"
                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                ErrorTooltipEnabled="True" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnTranscriptWithHeader" runat="server" Text="Transcript Report Format 1" ValidationGroup="SubmitTranscript"
                                    OnClick="btnTranscriptWithHeader_Click" Visible="false" CssClass="btn btn-info" />
                                
                                <asp:Button ID="btnTranscriptWithFormat" runat="server" Text="Transcript Report Format 2" ValidationGroup="SubmitTranscript"
                                     OnClick="btnTranscriptWithFormat_Click" Visible="false" CssClass="btn btn-info" />

                                <asp:Button ID="btntranscripwithoutheader" Visible="false" runat="server" Text="Transcript Report without Header" ValidationGroup="submit" OnClick="btntranscripwithoutheader_Click"
                                    CssClass="btn btn-info" />
                                <asp:Button ID="btnReport" runat="server" Text="All Result" Visible="false" CssClass="btn btn-info" ValidationGroup="submit" OnClick="btnReport_Click" />

                                
<asp:Button ID="btnTransB1" runat="server" Text="Transcript Report Format 1(Excel)" ValidationGroup="SubmitTranscript"
                                     Visible="false" CssClass="btn btn-info" OnClick="btnTransB1_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="VSTranscript" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="SubmitTranscript" />

                            </div>

                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>STUDENT INFORMATION</h5>
                                </div>
                                <div id="divStudentInfo" style="display: block;">
                                    <div class="row">
                                        <div class="col-lg-5 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Exam Roll No :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRegNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Father's Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblMName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Semester :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Landline No :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblLLNo" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Category :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCategory" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Nationality :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("") %>' Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>

                                                <li class="list-group-item"><b>Local Address :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblLAdd" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>City :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCity" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-5 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Branch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblBranch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Mobile No :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblMobNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Date of Birth :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblDOB" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Caste :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCaste" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Religion :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblReligion" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Permanent Address :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPAdd" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>City :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPCity" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-2 col-md-6 col-12">
                                            <p>Photo :</p>

                                            <asp:Image ID="imgPhoto" runat="server" Height="120px" Width="128px" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnTransB1" />
        </Triggers>
    </asp:UpdatePanel>


    <div id="divMsg" runat="server">
    </div>
</asp:Content>

