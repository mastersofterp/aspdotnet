<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TransferStudCourses.aspx.cs" Inherits="ACADEMIC_TransferStudCourses" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnl"
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
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <%--<h3 class="box-title">Student Subject Equivalence</h3>--%>
                        <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                    </div>
                    <div class="box-body">
                        <div class="nav-tabs-custom" id="Tabs" role="tabpanel">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1" onclick="clearTabContent()">Grade Pattern Student</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1" id="tab2" onclick="clearTabContent()">Marks Pattern Student</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="my-tab-content">
                                <div class="tab-pane active" id="tab_1">
                                    <div>
                                        <asp:UpdateProgress ID="updprogupdGradePattern" runat="server" AssociatedUpdatePanelID="updGradePattern"
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
                                    <asp:UpdatePanel ID="updGradePattern" runat="server">
                                        <ContentTemplate>
                                            <div class="row mt-3">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <div id="div12" runat="server"></div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Equivalence</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlEquivalence" runat="server" AppendDataBoundItems="True"
                                                                    CssClass="form-control" AutoPostBack="true"
                                                                    TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlEquivalence_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlEquivalence"
                                                                    Display="None" ErrorMessage="Please Select Equivalence" InitialValue="0" ValidationGroup="search"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvddlEquivalence" runat="server" ControlToValidate="ddlEquivalence" Display="None" ErrorMessage="Please Select Equivalence" InitialValue="0" ValidationGroup="excel" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Univ. Reg. No. / Admission No.</label>
                                                                </div>
                                                                <asp:TextBox ID="txtStudent" runat="server" CssClass="form-control" ToolTip="Enter Admission No." TabIndex="1"
                                                                    placeholder="Univ. Reg. No./ Admission No." />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <asp:Button ID="btnShow" runat="server" Text="Search" TabIndex="1" CssClass="btn btn-primary mt-3" OnClick="btnShow_Click" ValidationGroup="search" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtStudent" Display="None"
                                                                    ErrorMessage="Please Enter the Univ. Reg. No. / Admission No." SetFocusOnError="true" ValidationGroup="search" />
                                                                <asp:Button ID="btnExcel" runat="server" Text="Excel" TabIndex="1" CssClass="btn btn-info mt-3 " ValidationGroup="excel" OnClick="btnExcel_Click" />
                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                                                    ValidationGroup="search" />
                                                                <asp:ValidationSummary ID="ValidationSummarry2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="excel" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12" id="divdata" runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="col-lg-5 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Student Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Univ. Reg. No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblRegNo" runat="server" Font-Bold="True" /></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-7 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Regulation :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Admission No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblEnrollno" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Session </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlSession"
                                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0"
                                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Semester </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlSemester"
                                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12" id="divlv" runat="server" visible="false">
                                                        <div class="d-flex justify-content-between">
                                                            <div class="sub-heading">
                                                                <h5>Subject Details</h5>
                                                            </div>
                                                            <div class="mb-2">
                                                                <asp:LinkButton ID="ButtonAdd" runat="server" ToolTip="Add Row" OnClick="ButtonAdd_Click1" CssClass="btn btn-primary" TabIndex="1"
                                                                    Enabled="true" Text=" Add Row"><i class="fa fa-plus"></i> ADD </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <asp:ListView ID="lvCourse" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                                                            <LayoutTemplate>
                                                                <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblCanGradeCard" runat="server">
                                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                            <tr>
                                                                                <th>Sr. No.</th>
                                                                                <th>Subject</th>
                                                                                <th>Equivalent Subject Code</th>
                                                                                <th>Equivalent Subject Name</th>
                                                                                <th>Grade</th>
                                                                                <th>Exam Type</th>
                                                                                <th>New Grade</th>
                                                                                <th>Cancel</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>

                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="RANK" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                                                        <%--  <asp:Label ID="RANK" runat="server" Text='<%#Eval("RANK") %>' />--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlNewCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlNewCourse_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlNewCourse" SetFocusOnError="true"
                                                                            Display="None" ErrorMessage="Please Select Subject" InitialValue="0" ValidationGroup="Submit">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOldCCode" CssClass="form-control" runat="server" AutoComplete="False" MaxLength="9" TabIndex="1"
                                                                            OnTextChanged="txtOldCCode_TextChanged">
                                                                        </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOldCCode" Display="None"
                                                                            ErrorMessage="Please Enter Equivalent Subject Code" SetFocusOnError="true" ValidationGroup="Submit">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOldCourse" runat="server" CssClass="form-control" AutoComplete="False" TabIndex="1"
                                                                            OnTextChanged="txtOldCourse_TextChanged">
                                                                        </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtOldCourse" Display="None"
                                                                            ErrorMessage="Please Enter Equivalent Subject Name" SetFocusOnError="true" ValidationGroup="Submit">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOldGrade" runat="server" onkeypress="return validateDecGrade(event)" MaxLength="2" CssClass="form-control"
                                                                            AutoComplete="False" TabIndex="1" OnTextChanged="txtOldGrade_TextChanged">
                                                                        </asp:TextBox>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtOldGrade"
                                                                            ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ+-" FilterMode="ValidChars" />

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtOldGrade" Display="None"
                                                                            ErrorMessage="Please Enter Grade" SetFocusOnError="true" ValidationGroup="Submit">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>

                                                                    <td>
                                                                        <asp:DropDownList ID="ddlExamType" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true"
                                                                            TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                                                            <asp:ListItem Value="1">BackLog</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlExamType" SetFocusOnError="true"
                                                                            Display="None" ErrorMessage="Please Select Exam Type" InitialValue="-1" ValidationGroup="Submit">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>

                                                                    <td>
                                                                        <asp:DropDownList ID="ddlNewGrade" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true"
                                                                            TabIndex="1" data-select2-enable="true"
                                                                            OnTextChanged="ddlNewGrade_TextChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlNewGrade" SetFocusOnError="true"
                                                                            Display="None" ErrorMessage="Please Select New Grade" InitialValue="0" ValidationGroup="Submit">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <%--<td style="width: 148px">
                                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>--%>
                                                                    <td>
                                                                        <asp:ImageButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click" ImageUrl="~/Images/Delete.png"
                                                                            AlternateText="Remove Row" OnClientClick="return ConfirmCancel();" TabIndex="1"
                                                                            CommandArgument='<%# Container.DataItemIndex + 1%>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>

                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="1" ValidationGroup="Submit"
                                                            OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="1" Enabled="false" />
                                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" Text="Cancel" />


                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                                            ValidationGroup="Submit" />
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg" Style="color: green"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>







                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div id="divMsg" runat="server" />

                                </div>
                                <div class="tab-pane fade" id="tab_2">
                                    <div>
                                        <asp:UpdateProgress ID="UpdprogMarksPattern" runat="server" AssociatedUpdatePanelID="updMarksPattern"
                                            DynamicLayout="true" DisplayAfter="0" UpdateMode="Conditional">
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
                                    <asp:UpdatePanel ID="updMarksPattern" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row mt-3">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <div id="div1" runat="server"></div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Equivalence</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlEquivalence1" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlEquivalence1_SelectedIndexChanged"
                                                                    CssClass="form-control" AutoPostBack="true"
                                                                    TabIndex="1" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEquivalence1"
                                                                    Display="None" ErrorMessage="Please Select Equivalence" InitialValue="0" ValidationGroup="searchM"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvddlEquivalenceM" runat="server" ControlToValidate="ddlEquivalence1" Display="None" ErrorMessage="Please Select Equivalence" InitialValue="0" SetFocusOnError="true" ValidationGroup="ExcelM"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Univ. Reg. No. / Admission No.</label>
                                                                </div>
                                                                <asp:TextBox ID="txtstudent1" runat="server" CssClass="form-control" ToolTip="Enter Admission No." TabIndex="1"
                                                                    placeholder="Univ. Reg. No./ Admission No." />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <asp:Button ID="btnShow1" runat="server" Text="Search" TabIndex="1" CssClass="btn btn-primary mt-3" OnClick="btnShow1_Click" ValidationGroup="searchM" />

                                                                <asp:Button ID="btnExcelM" runat="server" Text="Excel" TabIndex="1" CssClass="btn btn-info mt-3" ValidationGroup="ExcelM" OnClick="btnExcelM_Click" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtstudent1" Display="None"
                                                                    ErrorMessage="Please Enter the Univ. Reg. No. / Admission No." SetFocusOnError="true" ValidationGroup="searchM" />
                                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                                                    ValidationGroup="searchM" />
                                                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ExcelM" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12" id="divdataM" runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="col-lg-5 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Student Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblName1" runat="server" Font-Bold="True" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Univ. Reg. No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblRegNo1" runat="server" Font-Bold="True" /></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-7 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Regulation :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblScheme1" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Admission No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblEnrollno1" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Session </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSession1" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlSession1_SelectedIndexChanged" TabIndex="1">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSession1"
                                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0"
                                                                    ValidationGroup="SubmitM"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Semester </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemester1" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSemester1_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSemester1"
                                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                                                    ValidationGroup="SubmitM"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12" id="divlvM" runat="server" visible="false">
                                                        <div class="d-flex justify-content-between">
                                                            <div class="sub-heading">
                                                                <h5>Subject Details</h5>
                                                            </div>
                                                            <div class="mb-2">
                                                                <asp:LinkButton ID="ButtonAdd1" runat="server" ToolTip="Add Row" OnClick="ButtonAdd1_Click" CssClass="btn btn-primary" TabIndex="1"
                                                                    Enabled="true" Text=" Add Row"><i class="fa fa-plus"></i> ADD </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <asp:ListView ID="lvCourseMarksPattern" runat="server" ShowFooter="true" AutoGenerateColumns="">
                                                            <LayoutTemplate>
                                                                <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="lvCourseMarksPattern" runat="server">
                                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                            <tr>
                                                                                <th>Sr. No.</th>
                                                                                <th>Subject</th>
                                                                                <th>Equivalence Subject Code</th>
                                                                                <th>Max Marks</th>
                                                                                <th>Min Marks</th>
                                                                                <th>Enter Marks</th>
                                                                                <th>Exam Type</th>
                                                                                <th>Cancel</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>

                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="RANK" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                                                        <%--  <asp:Label ID="RANK" runat="server" Text='<%#Eval("RANK") %>' />--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlNewCourse1" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlNewCourse1_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlNewCourse1" SetFocusOnError="true"
                                                                            Display="None" ErrorMessage="Please Select Subject" InitialValue="0" ValidationGroup="SubmitM">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOldCCode1" CssClass="form-control" runat="server" AutoComplete="False" MaxLength="9" TabIndex="1"
                                                                            OnTextChanged="txtOldCCode1_TextChanged">
                                                                        </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOldCCode1" Display="None"
                                                                            ErrorMessage="Please Enter Equivalent Subject Code" SetFocusOnError="true" ValidationGroup="SubmitM">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <%--<asp:TextBox ID="txtOldCourse1" runat="server" CssClass="form-control" AutoComplete="False" TabIndex="1"
                                                                            OnTextChanged="txtOldCourse1_TextChanged">
                                                                        </asp:TextBox>--%>

                                                                        <asp:TextBox ID="txtMaxMarks" runat="server" CssClass="form-control" TabIndex="1" Text="" ReadOnly="true">
                                                                        </asp:TextBox>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="" Display="None"
                                                                            ErrorMessage="Please Enter Equivalent Subject Name" SetFocusOnError="true" ValidationGroup="SubmitM">
                                                                        </asp:RequiredFieldValidator>--%>
                                                                    </td>
                                                                    <td>
                                                                        <%--    <asp:TextBox ID="txtOldGrade1" runat="server" onkeypress="return validateDecGrade(event)" MaxLength="2" CssClass="form-control"
                                                                            AutoComplete="False" TabIndex="1" OnTextChanged="txtOldGrade1_TextChanged">
                                                                        </asp:TextBox>--%>


                                                                        <asp:TextBox ID="txtMinMarks" runat="server" CssClass="form-control"
                                                                            TabIndex="1" Text="" ReadOnly="true">
                                                                        </asp:TextBox>
                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="" Display="None"
                                                                            ErrorMessage="Please Enter Grade" SetFocusOnError="true" ValidationGroup="SubmitM">
                                                                        </asp:RequiredFieldValidator>--%>
                                                                    </td>

                                                                    <td>

                                                                        <%--<asp:DropDownList ID="ddlExamType1" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true"
                                                                            TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlExamType1_SelectedIndexChanged">
                                                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                                                            <asp:ListItem Value="1">BackLog</asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                        <%--oninput="validateMarks(this)"--%>
                                                                        <asp:TextBox ID="txtMarks" runat="server" CssClass="form-control" MaxLength="3" TabIndex="1" onfocusout="validateMarks(this)"></asp:TextBox>
                                                                        <asp:Label ID="lblValidationMessage" runat="server" Text=""></asp:Label>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMarks"
                                                                            ValidChars="0123456789" FilterMode="ValidChars" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtMarks" SetFocusOnError="true"
                                                                            Display="None" ErrorMessage="Please Enter Marks" ValidationGroup="SubmitM">
                                                                        </asp:RequiredFieldValidator>

                                                                    </td>

                                                                    <td>
                                                                        <asp:DropDownList ID="ddlExamType1" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true"
                                                                            TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlExamType1_SelectedIndexChanged">
                                                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                                                            <asp:ListItem Value="1">BackLog</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlExamType1" SetFocusOnError="true"
                                                                            Display="None" ErrorMessage="Please Select Exam Type" InitialValue="-1" ValidationGroup="SubmitM">
                                                                        </asp:RequiredFieldValidator>

                                                                    </td>
                                                                    <%--<td style="width: 148px">
                                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>--%>
                                                                    <td>
                                                                        <asp:ImageButton ID="lnkRemove1" runat="server" OnClick="lnkRemove1_Click" ImageUrl="~/Images/Delete.png"
                                                                            AlternateText="Remove Row" OnClientClick="return ConfirmCancel();" TabIndex="1"
                                                                            CommandArgument='<%# Container.DataItemIndex + 1%>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>

                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmit1" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="1" ValidationGroup="SubmitM"
                                                            OnClick="btnSubmit1_Click" />
                                                        <asp:Button ID="btnReport1" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport1_Click" TabIndex="1" Enabled="false" />
                                                        <asp:Button ID="btnCancel1" runat="server" OnClick="btnCancel1_Click" CssClass="btn btn-warning" TabIndex="1" Text="Cancel" />


                                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                                            ValidationGroup="SubmitM" />
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Label ID="lblmsg1" runat="server" SkinID="lblmsg1" Style="color: green"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnExcelM" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        $(document).ready(function () {
            // Event handler for tab link clicks
            $('.nav-tabs a').click(function (e) {
                e.preventDefault(); // Prevent default anchor behavior
                $(this).tab('show'); // Show the clicked tab
            });
        });
    </script>



    <script type="text/javascript">

        function validateMarks(txt) {

            debugger

            X1 = Number(txt.value);
            var dataRowsmark = null;
            var percnt;
            if (document.getElementById('ctl00_ContentPlaceHolder1_lvCourseMarksPattern_lvCourseMarksPattern') != null) {
                dataRowsmark = document.getElementById('ctl00_ContentPlaceHolder1_lvCourseMarksPattern_lvCourseMarksPattern').getElementsByTagName('tr');
                for (j = 0; j < dataRowsmark.length - 1; j++) {
                    debugger

                    var MaxMark1 = 0;
                    var MinMark1 = 0;
                    var marks = 0;
                    var AssGrade;
                    var GDpoint;
                    var AssValu = 0;
                    //ctl00_ContentPlaceHolder1_lvCourseMarksPattern_ctrl0_txtMarks

                    MaxMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvCourseMarksPattern_ctrl' + j + '_txtMaxMarks').value.trim());
                    MinMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvCourseMarksPattern_ctrl' + j + '_txtMinMarks').value.trim());
                    marks = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvCourseMarksPattern_ctrl' + j + '_txtMarks').value.trim());

                    //if (marks < MinMark || marks > MaxMark) {
                    //if (marks > MaxMark) {
                    //    alert('Please check Enter Marks Should Not Be Greater Than Maximum Marks');
                    //    document.getElementById('ctl00_ContentPlaceHolder1_lvCourseMarksPattern_ctrl' + j + '_txtMarks').value = '';
                    //    document.getElementById('ctl00_ContentPlaceHolder1_lvCourseMarksPattern_ctrl' + j + '_txtMarks').focus();
                    //    return false;
                    //}

                    if (marks > MaxMark) {
                        if (marks == 902 || marks == 903 || marks == 904 || marks == 905 || marks == 906) {

                        }
                        else {

                            alert('Please check Enter Marks Should Not Be Greater Than Maximum Marks!!');
                            document.getElementById('ctl00_ContentPlaceHolder1_lvCourseMarksPattern_ctrl' + j + '_txtMarks').value = '';
                            document.getElementById('ctl00_ContentPlaceHolder1_lvCourseMarksPattern_ctrl' + j + '_txtMarks').focus();
                            return false;
                        }
                    }
                }
            }
        }

    </script>



    <script type="text/javascript">
        function ConfirmCancel() {
            if (confirm('Are you sure you want delete this row?')) {
                return true;
            }
            return false;
        }
    </script>

    <script type="text/javascript">
        function ConfirmSubmit() {
            if (Page_ClientValidate('submit')) {
                return confirm('Are you sure you want submit?');
            }
            return false;
        }
    </script>







</asp:Content>
