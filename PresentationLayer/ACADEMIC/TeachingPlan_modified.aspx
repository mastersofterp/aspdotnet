<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TeachingPlan_modified.aspx.cs" Inherits="ACADEMIC_TeachingPlan_modified" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <%--<asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>--%>
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Core/Elective Teaching Plan</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Open/Global Elective Teaching Plan</a>
                            </li>

                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updTeach"
                                        DynamicLayout="true" DisplayAfter="0">
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
                                <asp:UpdatePanel ID="updTeach" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12 col-md-12 col-lg-9" style="border: 1px solid #ccc; border-radius: 5px; padding: 15px;">

                                                    <div class="row">

                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="adminScheme" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%-- <label>Scheme</label>--%>
                                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="True"
                                                                TabIndex="1" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlScheme" Display="None"
                                                                ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvddlScheme1" runat="server" ControlToValidate="ddlScheme" Display="None"
                                                                ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True" ValidationGroup="upload">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvddlScheme2" runat="server" ControlToValidate="ddlScheme" Display="None"
                                                                ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True" ValidationGroup="Report">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" ValidationGroup="Submit" Display="None"
                                                                ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" />
                                                            <asp:RequiredFieldValidator ID="rfvSession1" runat="server" ControlToValidate="ddlSession" ValidationGroup="Report" Display="None"
                                                                ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" />
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12" style="display: none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>School/Institute</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSchoolInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvinstitute" runat="server" ControlToValidate="ddlSchoolInstitute" ValidationGroup="Submit" Display="None"
                                                                ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true" Enabled="false" />
                                                            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlSchoolInstitute" ValidationGroup="Report" Display="None"
                                                                ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true" Enabled="false" />
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="trTeacher" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Teacher</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="True" TabIndex="1" ValidationGroup="course"
                                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvteacher" runat="server" ControlToValidate="ddlTeacher"
                                            Display="None" ErrorMessage="Please Select Course teacher." InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="admindegree" runat="server" style="display: none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%-- <label>Degree</label>--%>
                                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree" Display="None" Enabled="false"
                                                                ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit">
                                                            </asp:RequiredFieldValidator>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvddlDegree1" runat="server" ControlToValidate="ddlDegree" Display="None"
                                            ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True" ValidationGroup="upload">
                                        </asp:RequiredFieldValidator>--%>
                                                            <asp:RequiredFieldValidator ID="rfvddlDegree2" runat="server" ControlToValidate="ddlDegree" Display="None" Enabled="false"
                                                                ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True" ValidationGroup="Report">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="adminBranch" runat="server" style="display: none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%-- <label>Program/Branch</label>--%>
                                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch" Display="None" Enabled="false"
                                                                ErrorMessage="Please Select Program/Branch" InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit">
                                                            </asp:RequiredFieldValidator>
                                                            <%--<asp:RequiredFieldValidator   ID="rfvddlBranch1" runat="server" ControlToValidate="ddlBranch" Display="None"
                                            ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True" ValidationGroup="upload">
                                        </asp:RequiredFieldValidator>--%>
                                                            <asp:RequiredFieldValidator ID="rfvddlBranch2" runat="server" ControlToValidate="ddlBranch" Display="None" Enabled="false"
                                                                ErrorMessage="Please Select Program/Branch" InitialValue="0" SetFocusOnError="True" ValidationGroup="Report">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblddlSemester" runat="server" Font-Bold="true">Semester/Course</asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None"
                                                                ErrorMessage="Please Select Semester/Course" InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvSemester1" runat="server" ControlToValidate="ddlSemester" Display="None"
                                                                ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True" ValidationGroup="Report">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Subject Type</label>--%>
                                                                <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType" Display="None"
                                                    ErrorMessage="Please Select  Subject Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvSubjectType1" runat="server" ControlToValidate="ddlSubjectType" Display="None"
                                                    ErrorMessage="Please Select  Course Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="Report">
                                                </asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%-- <label>Course Name</label>--%>
                                                                <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                            <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse" Display="None"
                                                    ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvCourse1" runat="server" ControlToValidate="ddlCourse" Display="None"
                                                    ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True" ValidationGroup="Report">
                                                </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="dvTutorial" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Teaching Plan For</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlTutorial" runat="server" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlTutorial_SelectedIndexChanged">
                                                                <%--<asp:ListItem Selected="True" Value="1">Theory</asp:ListItem>
                                                                <asp:ListItem Value="2">Tutorial</asp:ListItem>
                                                                <asp:ListItem Value="3">Practical</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>



                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="spanSection" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%-- <label>Section</label>--%>
                                                                <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSection" runat="server" ErrorMessage="Please Select Section" ControlToValidate="ddlSection"
                                                                Display="None" ValidationGroup="Submit" InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="lblBatch" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Batch</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="True" Visible="false" CssClass="form-control" data-select2-enable="true"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ErrorMessage="Please Select Batch" Visible="false"
                                                                ControlToValidate="ddlBatch" Display="None" ValidationGroup="Submit" InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="ddlUnitNodiv" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Unit No.</label>--%>
                                                                <asp:Label ID="lblDYddlUnitNo" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlUnitNo" runat="server" TabIndex="1" AutoPostBack="True" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlUnitNo_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvUnitNo" runat="server" ControlToValidate="ddlUnitNo" ValidationGroup="Submit" Display="None"
                                                                ErrorMessage="Please Select Unit No" SetFocusOnError="true" InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="ddlLectureNodiv" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Topic No.</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlLectureNo" runat="server" TabIndex="1" AutoPostBack="False" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvLectureNo" runat="server" ControlToValidate="ddlLectureNo" ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Topic Code "
                                                                SetFocusOnError="true" InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-8 col-md-6 col-12" id="txtLectureTopicdiv" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Topic Description</label>
                                                            </div>
                                                            <asp:TextBox ID="txtLectureTopic" runat="server" TabIndex="1" TextMode="MultiLine" CssClass="form-control" Rows="1">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvLectureTopic" runat="server" ControlToValidate="txtLectureTopic" ValidationGroup="Submit"
                                                                Display="None" ErrorMessage="Please Enter Lecture Topic." SetFocusOnError="true" />
                                                        </div>

                                                        <div id="Div5" class="form-group col-lg-4 col-md-6 col-12" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>No. of Session Required</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSessionReq" runat="server" MaxLength="2" TabIndex="1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSessionReq" ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Session Required"
                                                                SetFocusOnError="true" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                FilterMode="ValidChars" FilterType="Custom" ValidChars="0123456789" TargetControlID="txtSessionReq" />
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="datelbl" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Lecture Date</label>
                                                            </div>
                                                            <asp:Label ID="lblDate" runat="server" Visible="False"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 col-md-12 col-lg-3" visible="true" id="pnlUP" runat="server" style="border: 1px solid #ccc; border-radius: 5px; padding: 15px;">

                                                    <div class="sub-heading">
                                                        <h5>Upload Teaching Plan</h5>
                                                    </div>
                                                    <asp:UpdatePanel ID="updPanel" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-12 col-md-6 col-12 d-none">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Degree</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlDegreeEX" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                            OnSelectedIndexChanged="ddlDegreeEX_SelectedIndexChanged" AutoPostBack="true" TabIndex="13">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--    <asp:RequiredFieldValidator ID="rfvddlDegreeEX" runat="server" ControlToValidate="ddlDegreeEX"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="upload"></asp:RequiredFieldValidator>--%>
                                                                    </div>



                                                                    <div class="col-12 btn-footer text-left">
                                                                        <asp:LinkButton ID="btnBlankDownld" runat="server" OnClick="btnBlankDownld_Click" TabIndex="2" CssClass="btn btn-primary"
                                                                            Text="Blank" ToolTip="Click to download blank excel format file">Download Template</asp:LinkButton>

                                                                        <div class="form-group col-lg-12 col-md-12 col-12 mt-2 pl-0 pr-0">
                                                                            <div class="label-dynamic">
                                                                                <label>Excel File</label>
                                                                            </div>
                                                                            <asp:FileUpload ID="btnBrowse" runat="server" ToolTip="Select file to Import" TabIndex="2" />
                                                                        </div>
                                                                        <asp:LinkButton ID="btnUpload" runat="server" ValidationGroup="upload" OnClick="btnUpload_Click" CssClass="btn btn-primary"
                                                                            TabIndex="16" Text="Upload" ToolTip="Click to Upload"> Upload Excel</asp:LinkButton>

                                                                        <asp:LinkButton ID="btnDownload" runat="server" OnClick="btnDownload_Click" TabIndex="2" CssClass="btn btn-primary d-none" Enabled="false"
                                                                            Text="Download" ToolTip="Click to download the excel format file"> Download Uploaded Data</asp:LinkButton>

                                                                        <asp:Button ID="btnCancel1" runat="server" TabIndex="18" Text="Clear" ToolTip="Click To Clear" CssClass="btn btn-warning d-none" OnClick="btnCancel1_Click" />
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnBlankDownld" />
                                                            <asp:PostBackTrigger ControlID="btnUpload" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>


                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-12 mt-3" runat="server" id="divTpSlots" visible="false">
                                            <div class="row">
                                                <div class="form-group col-lg-4 col-md-6 col-12" id="Div2" runat="server">
                                                    <div class="label-dynamic">
                                                        <%--<sup>* </sup>--%>
                                                        <label>Time Table Date</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlTimeTable" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlTimeTable_SelectedIndexChanged" Enabled="false" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Time Table Date"
                                                        ControlToValidate="ddlTimeTable" Display="None" ValidationGroup="Submit" InitialValue="0" />--%>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-12 col-12" id="Lecturedates" runat="server">
                                                    <div class="sub-heading">
                                                        <h5>Proposed Dates / Lecture Dates</h5>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Monday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlMon" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlMon_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Tuesday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlTues" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlTues_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Wednesday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlWed" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlWed_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Thursday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlThurs" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlThurs_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Friday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlFri" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlFri_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Saturday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSat" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSat_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-6 col-md-12 col-12" id="lectureSlot" runat="server">
                                                    <div class="sub-heading">
                                                        <h5>Lecture Slots
                                                <asp:Label ID="lblSlot" runat="server" Visible="False"></asp:Label></h5>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Monday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlMonSlot" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlMonSlot_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Tuesday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlTueSlot" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlTueSlot_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Wednesday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlWedSlot" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlWedSlot_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Thursday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlThusSlot" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlThusSlot_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Friday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlFriSlot" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlFriSlot_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Saturday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSatSlot" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlSatSlot_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" TabIndex="4"
                                                CssClass="btn btn-primary"> Submit</asp:LinkButton>

                                            <asp:LinkButton ID="btnLock" runat="server" Enabled="False" OnClick="btnLock_Click" CssClass="btn btn-primary" TabIndex="4" Visible="false">
                                     Lock</asp:LinkButton>
                                            <asp:LinkButton ID="btnReport" Visible="false" runat="server" OnClick="btnReport_Click" CssClass="btn btn-info" TabIndex="4" ValidationGroup="Report" CommandArgument="1"> Teaching Plan Schedule Report</asp:LinkButton>
                                            <asp:LinkButton ID="btnExecutedReport" runat="server" OnClick="btnReport_Click" CssClass="btn btn-info" TabIndex="4" ValidationGroup="Report" CommandArgument="2"> Planned vs Executed Lecture Report</asp:LinkButton>
                                            <asp:LinkButton ID="btnExcelReport" runat="server" OnClick="btnExcelReport_Click" CssClass="btn btn-info" TabIndex="4" ValidationGroup="Report" CommandArgument="3"> Planned vs Executed Report Excel</asp:LinkButton>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="4" />
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Submit" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                                            <asp:ValidationSummary ID="vsUpload" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="upload" />
                                        </div>

                                        <div class=" col-12">
                                            <asp:ListView ID="lvTeachingPlan" Visible="false" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading">
                                                            <h5>TEACHING PLAN SCHEDULE</h5>
                                                        </div>

                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit </th>
                                                                        <th>Delete </th>
                                                                        <th>Unit No </th>
                                                                        <th>Topic Code </th>
                                                                        <th>Session Required </th>
                                                                        <th>Topic </th>
                                                                        <th>Proposed Date </th>
                                                                        <th>Conduct Date </th>
                                                                        <th>Course </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("TP_NO") %>'
                                                                ImageUrl='<%# "~/images/" + (Eval("TEACHINGPLAN_NO").ToString() == "0" ? "edit.png" : "disable.png") %>' OnClick="btnEdit_Click" TabIndex="5" ToolTip="Edit Record"
                                                                Enabled='<%# Convert.ToInt32(Eval("TEACHINGPLAN_NO"))== 0 ? true : false %>' />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("TP_NO") %>'
                                                                ImageUrl='<%# "~/images/" + (Eval("TEACHINGPLAN_NO").ToString() == "0" ? "delete.png" : "disable.png") %>' OnClick="btnDelete_Click"
                                                                OnClientClick="return ConfirmToDelete(this);" TabIndex="5" ToolTip='<%# Eval("TP_NO") %>' Enabled='<%# Convert.ToInt32(Eval("TEACHINGPLAN_NO"))== 0 ? true : false %>' />
                                                        </td>
                                                        <td><%# Eval("UNIT_NO")%></td>
                                                        <td><%# Eval("LECTURE_NO")%></td>
                                                        <td><%# Eval("SESSION_PLAN")%></td>
                                                        <td>
                                                            <asp:Label ID="lblTopicDetail" runat="server" Text='<%# Eval("Topic")%>' ToolTip='<%# Eval("Topic_detail")%>' />
                                                        </td>
                                                        <td><%# Eval("LectureDate")%></td>
                                                        <td><%# Eval("CONDUCT_DATE")%></td>
                                                        <td><span style="font-weight: bold"><%# Eval("CCODE")%></span>- <%# Eval("COURSE_NAME")%><span style="color: Red; font-weight: bold">[Section : <%# Eval("SECTIONNAME")%>]</span><%--&nbsp; <span style="color: Red; font-weight: bold">[Batch :
                    <%# Eval("BATCHNAME")%>]</span>--%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>


                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updGlobal"
                                        DynamicLayout="true" DisplayAfter="0">
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
                                <asp:UpdatePanel ID="updGlobal" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12 col-md-12 col-lg-12" style="border: 1px solid #ccc; border-radius: 5px; padding: 15px;">

                                                    <div class="row">

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSessionGlobal" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlSessionGlobal_SelectedIndexChanged" TabIndex="2">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSessionGlobal" ValidationGroup="SubmitGlobal" Display="None"
                                                                ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSessionGlobal" ValidationGroup="ReportGlobal" Display="None"
                                                                ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="true">Semester/Course</asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSemesterGlobal" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlSemesterGlobal_SelectedIndexChanged" TabIndex="6">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlSemesterGlobal" Display="None"
                                                                ErrorMessage="Please Select Semester/Course" InitialValue="0" SetFocusOnError="True" ValidationGroup="SubmitGlobal">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSemesterGlobal" Display="None"
                                                                ErrorMessage="Please Select Semester/Course" InitialValue="0" SetFocusOnError="True" ValidationGroup="ReportGlobal">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div8" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Time Table Date</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlTimeTableDateGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlTimeTableDateGlobal_SelectedIndexChanged" TabIndex="10">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please Select Time Table Date"
                                                                ControlToValidate="ddlTimeTableDateGlobal" Display="None" ValidationGroup="SubmitGlobal" InitialValue="0" />
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div11" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Unit No.</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlUnitNoGlobal" runat="server" TabIndex="11" AutoPostBack="True" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlUnitNoGlobal_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlUnitNoGlobal" ValidationGroup="SubmitGlobal" Display="None"
                                                                ErrorMessage="Please Select Unit No" SetFocusOnError="true" InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div12" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Topic No.</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlLectureNoGlobal" runat="server" TabIndex="12" AutoPostBack="False" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlLectureNoGlobal" ValidationGroup="SubmitGlobal" Display="None" ErrorMessage="Please Select Topic Code "
                                                                SetFocusOnError="true" InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="Div13" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Topic Description</label>
                                                            </div>
                                                            <asp:TextBox ID="txtLectureTopicGlobal" runat="server" TabIndex="13" TextMode="MultiLine" CssClass="form-control" Rows="1">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtLectureTopicGlobal" ValidationGroup="SubmitGlobal"
                                                                Display="None" ErrorMessage="Please Enter Lecture Topic." SetFocusOnError="true" />
                                                        </div>

                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-12 col-12" id="Div3" runat="server">
                                                    <div class="sub-heading">
                                                        <h5>Proposed Dates / Lecture Dates</h5>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Monday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlMonGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlMonGlobal_SelectedIndexChanged" TabIndex="19">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Tuesday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlTuesGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlTuesGlobal_SelectedIndexChanged" TabIndex="20">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Wednesday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlWedGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlWedGlobal_SelectedIndexChanged" TabIndex="21">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Thursday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlThursGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlThursGlobal_SelectedIndexChanged" TabIndex="22">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Friday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlFriGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlFriGlobal_SelectedIndexChanged" TabIndex="23">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Saturday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSatGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSatGlobal_SelectedIndexChanged" TabIndex="24">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-6 col-md-12 col-12" id="Div4" runat="server">
                                                    <div class="sub-heading">
                                                        <h5>Lecture Slots
                                                <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></h5>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Monday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlMonSlotGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlMonSlotGlobal_SelectedIndexChanged" TabIndex="25">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Tuesday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlTueSlotGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlTueSlotGlobal_SelectedIndexChanged" TabIndex="26">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Wednesday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlWedSlotGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlWedSlotGlobal_SelectedIndexChanged" TabIndex="27">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Thursday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlThusSlotGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlThusSlotGlobal_SelectedIndexChanged" TabIndex="28">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Friday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlFriSlotGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlFriSlotGlobal_SelectedIndexChanged" TabIndex="29">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Saturday</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSatSlotGlobal" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" Enabled="False"
                                                                AutoPostBack="False" OnSelectedIndexChanged="ddlSatSlotGlobal_SelectedIndexChanged" TabIndex="30">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitGlobal" runat="server" Text="Submit" ValidationGroup="SubmitGlobal" OnClick="btnSubmitGlobal_Click" TabIndex="31"
                                                CssClass="btn btn-primary"> Submit</asp:LinkButton>

                                            <asp:LinkButton ID="btnReportGlobal" runat="server" OnClick="btnReportGlobal_Click" CssClass="btn btn-info" TabIndex="34" ValidationGroup="ReportGlobal" CommandArgument="1" Visible="false"> Teaching Plan Schedule Report</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="btnReportGlobal_Click" CssClass="btn btn-info" TabIndex="34" ValidationGroup="ReportGlobal" CommandArgument="2" Visible="false"> Planned vs Executed Lecture Report</asp:LinkButton>
                                            <asp:Button ID="btnCancelGlobal" runat="server" Text="Cancel" OnClick="btnCancelGlobal_Click" CssClass="btn btn-warning" TabIndex="32" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="SubmitGlobal" />

                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ReportGlobal" />
                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="uploadGobal" />
                                        </div>
                                        <div class=" col-12">
                                            <asp:ListView ID="lvTeachingPlanGlobalElective" Visible="false" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading">
                                                            <h5>TEACHING PLAN SCHEDULE</h5>
                                                        </div>
                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit </th>
                                                                        <th>Delete </th>
                                                                        <th>Unit No </th>
                                                                        <th>Topic Code </th>
                                                                        <th>Topic </th>
                                                                        <th>Proposed Date </th>
                                                                        <th>Conduct Date </th>
                                                                        <th>Course </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditGlobalElective" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("TP_NO") %>'
                                                                ImageUrl='<%# "~/images/" + (Eval("TEACHINGPLAN_NO").ToString() == "0" ? "edit.png" : "disable.png") %>' OnClick="btnEditGlobalElective_Click" TabIndex="35" ToolTip="Edit Record"
                                                                Enabled='<%# Convert.ToInt32(Eval("TEACHINGPLAN_NO"))== 0 ? true : false %>' />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnDeleteGlobalElective" runat="server" CommandArgument='<%# Eval("TP_NO") %>'
                                                                ImageUrl='<%# "~/images/" + (Eval("TEACHINGPLAN_NO").ToString() == "0" ? "delete.png" : "disable.png") %>' OnClick="btnDeleteGlobalElective_Click"
                                                                OnClientClick="return ConfirmToDelete(this);" TabIndex="36" ToolTip='<%# Eval("TP_NO") %>' Enabled='<%# Convert.ToInt32(Eval("TEACHINGPLAN_NO"))== 0 ? true : false %>' />
                                                        </td>
                                                        <td><%# Eval("UNIT_NO")%></td>
                                                        <td><%# Eval("LECTURE_NO")%></td>
                                                        <td>
                                                            <asp:Label ID="lblTopicDetail" runat="server" Text='<%# Eval("Topic")%>' ToolTip='<%# Eval("Topic_detail")%>' />
                                                        </td>
                                                        <td><%# Eval("LectureDate")%></td>
                                                        <td><%# Eval("CONDUCT_DATE")%></td>
                                                        <td><span style="font-weight: bold"><%# Eval("CCODE")%></span>- <%# Eval("COURSE_NAME")%><span style="color: Red; font-weight: bold"></span>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExcelReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
    <%-- </ContentTemplate>

        <Triggers>
           
        </Triggers>
    </asp:UpdatePanel>--%>
    <div id="divMsg" runat="Server">
    </div>

    <script type="text/javascript" language="javascript">
        function ConfirmToDelete(me) {
            if (me != null) {
                var ret = confirm('Are you sure to Delete this Teaching Plan Entry?');
                if (ret == true)
                    return true;
                else
                    return false;
            }
        }
    </script>

    <%-- FOLLOWING CODE ALLOWS THE AUTOCOMPLETE TO BE FIRED IN UPDATEPANEL--%>

    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            ConfirmToDelete();
        }
    </script>

    <%--END : FOLLOWING CODE ALLOWS THE AUTOCOMPLETE TO BE FIRED IN UPDATEPANEL--%>
</asp:Content>
