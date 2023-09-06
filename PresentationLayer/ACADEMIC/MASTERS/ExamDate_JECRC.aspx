<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ExamDate_JECRC.aspx.cs" Inherits="ACADEMIC_MASTERS_ExamDate" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updExamdate"
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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">EXAM DAYS MANAGEMENT</h3>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Regular Time Table</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Global Elective Time Table</a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
                                <asp:UpdatePanel ID="updExamdate" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12" id="divbody" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Institute Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" TabIndex="1" runat="server" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select Institute">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfccolege" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfccollege" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Excel"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" TabIndex="1" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                        ValidationGroup="Show" Display="None" ErrorMessage="Please Select session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                    <asp:RequiredFieldValidator ID="rfvsession1" runat="server" ControlToValidate="ddlSession"
                                                        ValidationGroup="submit" Display="None" ErrorMessage="Please Select session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                    <asp:RequiredFieldValidator ID="rfvsession3" runat="server" ControlToValidate="ddlSession"
                                                        ValidationGroup="Excel" Display="None" ErrorMessage="Please Select session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" TabIndex="1" data-select2-enable="true" runat="server" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Branch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" TabIndex="1" runat="server" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Scheme</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlScheme"
                                                        Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Subject Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubjecttype" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlSubjecttype_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubjecttype"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfcsubject" runat="server" ControlToValidate="ddlSubjecttype"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                                        TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                        ValidationGroup="Show" Display="None" ErrorMessage="Please Select Semester"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="rfvSem1" runat="server" ControlToValidate="ddlSemester"
                                                        ValidationGroup="submit" Display="None" ErrorMessage="Please Select Semester"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Section</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                                        TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div id="divbatch" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Batch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlbatch" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True"
                                                        TabIndex="1">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Exam Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamName" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged"
                                                        AutoPostBack="True" TabIndex="1" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExamName"
                                                        ValidationGroup="Show" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="rfvExam1" runat="server" ControlToValidate="ddlExamName"
                                                        ValidationGroup="submit" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Sub Exam Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubExamName" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlSubExamName_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubExamName"
                                                        ValidationGroup="submit"
                                                        Display="None" ErrorMessage="Please Select Sub Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSubExamName"
                                                        ValidationGroup="Show"
                                                        Display="None" ErrorMessage="Please Select Sub Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>
                                                <div class="col-12">
                                                    <asp:Label ID="lblStudCnt" runat="server" Style="color: #990000"></asp:Label>
                                                    <asp:Panel ID="PanelLvDate" runat="server" ScrollBars="Auto">
                                                    </asp:Panel>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer" id="divbuttons" runat="server">
                                            <asp:Button ID="btnShow" runat="server" Text="Show Courses" OnClick="btnShow_Click" CssClass="btn btn-info" ValidationGroup="Show" TabIndex="1" />

                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-info" TabIndex="1" ValidationGroup="submit" Visible="false" />

                                            <asp:Button ID="btnViewLogin" runat="server" Text="View On Student Login" CssClass="btn btn-info" OnClick="btnViewLogin_Click" ValidationGroup="Show" TabIndex="1" Visible="false" />

                                            <asp:Button ID="btnReport" runat="server" Text="Exam Time Table Report" OnClick="btnReport_Click" ValidationGroup="Show" CssClass="btn btn-info" TabIndex="1" />

                                            <asp:Button ID="btnExcel" runat="server" Text="Exam Time Table Excel" OnClick="btnExcel_Click" CssClass="btn btn-info" TabIndex="1" ValidationGroup="Excel" />

                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" />

                                            <asp:ValidationSummary ID="valsum" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                            <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                            <asp:ValidationSummary ID="valexcel" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Excel" />

                                        </div>
                                        <div class="col-12">
                                            <asp:ListView ID="lvCourse" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Course List</h5>
                                                    </div>

                                                    <table id="ID5" class="table table-striped table-bordered " style="width: 100%">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Select<asp:CheckBox ID="cbHead" runat="server" Text="Select" Visible="false" />
                                                                </th>
                                                                <th id="Th1" runat="server">Subject Code - Subject Name </th>
                                                                <th>Student Count</th>
                                                                <th>Exam Date </th>
                                                                <th>Slot </th>
                                                                <th id="BatchTheory1" style="display: none">Mode of Exam </th>
                                                                <th>Action.</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>

                                                </LayoutTemplate>
                                                <ItemTemplate>

                                                    <tr id="trCurRow">
                                                        <td>
                                                            <asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("STATUS").ToString() == "True" ? true : false %>' ToolTip='<%#Eval("COURSENO")%>' TabIndex="1" />
                                                        </td>
                                                        <td><%# Eval("SUBJECT_NAME")%></td>
                                                        <asp:Label ID="lblCourseno" runat="server" Text='<%# Eval("COURSENO")%>' ToolTip='<%# Eval("CCODE")%>' Visible="false"></asp:Label>
                                                        <td><%# Eval("STUDENTCOUNT")%></td>
                                                        <td>

                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>

                                                                <asp:TextBox ID="txtExamDate" runat="server" Text='<%# Eval("EXAMDATE")%>' ValidationGroup="submit" ToolTip='<%# Container.DataItemIndex + 1 %>' TabIndex="1" onKeyPress="javascript:checkDate();" AutoPostBack="true" OnTextChanged="txtExamDate_TextChanged" />
                                                                <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgExamDate" TargetControlID="txtExamDate" Enabled="true" OnClientDateSelectionChanged="checkDate" />
                                                                <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" Mask="99/99/9999" MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtExamDate" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" ControlExtender="meExamDate" ControlToValidate="txtExamDate" Display="None" EmptyValueMessage="Please Enter Exam Date" ErrorMessage="Please Enter Exam Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid" IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit" />
                                                                <asp:RequiredFieldValidator ID="rfvExamDate" runat="server" ControlToValidate="txtExamDate" Display="None" ErrorMessage="Please select Exam Date!!" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            </div>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSlot" runat="server" AppendDataBoundItems="true" CausesValidation="true" TabIndex="1" OnSelectedIndexChanged="ddlSlot_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdf_slotno" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                            <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlSlot" Display="None"
                                                                ErrorMessage="Please Select Slot" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit" />
                                                        </td>
                                                        <td id="ddlmodeexamhide" style="display: none">
                                                            <asp:DropDownList ID="ddlmodeexam" runat="server" AppendDataBoundItems="true" CausesValidation="true" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdf_modeexam" runat="server" Value='<%# Eval("ModeOfEXAMNO")%>' />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlmodeexam" Display="None"
                                                                ErrorMessage="Please Select Mode Of Exam" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnEvalDelete" runat="server"
                                                                ImageUrl="~/images/delete.gif" AlternateText="CANCEL RECORD" ToolTip='<%# Eval("EXDTNO")%>'
                                                                OnClick="ibtnEvalDelete_Click" OnClientClick="return showConfirm();" CommandArgument='<%# Eval("EXDTNO")%>' TabIndex="1" />
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:ListView>
                                            <div id="divMsg" runat="Server">
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExcel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12" id="div1" runat="server">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession1" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSession1_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSession1"
                                                        ValidationGroup="Show2" Display="None" ErrorMessage="Please Select Session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlSession1"
                                                        ValidationGroup="submit1" Display="None" ErrorMessage="Please Select Session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Pattern </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlpattern" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlpattern_SelectedIndexChanged" TabIndex="1" data-select2-enable="true" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvpattern" runat="server" ControlToValidate="ddlpattern"
                                                        ValidationGroup="Show2" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlpattern"
                                                        ValidationGroup="submit1" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Subject Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubjecttype1" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlSubjecttype1_SelectedIndexChanged" Height="16px">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSubjecttype1"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show2"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSubjecttype1"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Exam Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamName1" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlExamName1_SelectedIndexChanged" TabIndex="1" data-select2-enable="true" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlExamName1"
                                                        ValidationGroup="Show2" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlExamName1"
                                                        ValidationGroup="submit1" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Sub Exam Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubexamname1" runat="server" AppendDataBoundItems="true"
                                                        TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlSubexamname1_SelectedIndexChanged" style="height: 22px" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSubexamname1"
                                                        ValidationGroup="Show2" Display="None" ErrorMessage="Please Select SubExam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlSubexamname1"
                                                        ValidationGroup="submit1" Display="None" ErrorMessage="Please Select SubExam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>
                                                <div class="col-12">
                                                    <asp:Label ID="Label1" runat="server" Style="color: #990000"></asp:Label>
                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer" id="div2" runat="server">
                                            <asp:Button ID="btnShow1" runat="server" Text="Show Courses" OnClick="btnShow1_Click" CssClass="btn btn-info" ValidationGroup="Show2" TabIndex="1" />
                                            <asp:Button ID="btnSubmit1" runat="server" Text="Submit" OnClick="btnSubmit1_Click" CssClass="btn btn-info" TabIndex="1" ValidationGroup="submit1" Enabled="false"/>
                                             <asp:Button ID="btnViewLogin1" runat="server" Text="View On Student Login" CssClass="btn btn-info" OnClick="btnViewLogin1_Click" ValidationGroup="submit1" TabIndex="1" Visible="false" />
                                            <asp:Button ID="btnCancel1" runat="server" Text="Cancel" OnClick="btnCancel1_Click" CssClass="btn btn-warning" TabIndex="1" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                                ValidationGroup="Show2" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit1" />
                                        </div>
                                        <div class="col-12">
                                            <asp:ListView ID="lvCourse1" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Course List</h5>
                                                    </div>

                                                    <table id="ID5" class="table table-striped table-bordered " style="width: 100%">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Select<asp:CheckBox ID="cbHead" runat="server" Text="Select" Visible="false" />
                                                                </th>
                                                                <th id="Th1" runat="server">Subject Code - Subject Name </th>
                                                                <th style="display: none">Student Count</th>
                                                                <th>Exam Date </th>
                                                                <th>Slot </th>
                                                                <th id="BatchTheory1" style="display: none">Mode of Exam </th>
                                                                <th>Action.</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow">
                                                        <td>
                                                            <asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("STATUS").ToString() == "True" ? true : false %>' TabIndex="1" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCourseno" runat="server" Text='<%# Eval("SUBJECT_NAME")%>' ToolTip='<%# Eval("CCODE")%>' ></asp:Label>
                                                        </td>
                                                        <td style="display: none"><%# Eval("STUDENTCOUNT")%></td>
                                                        <td>
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>

                                                                <asp:TextBox ID="txtExamDate" runat="server" Text='<%# Eval("EXAMDATE")%>' ValidationGroup="submit1" ToolTip='<%# Container.DataItemIndex + 1 %>' TabIndex="1" onKeyPress="javascript:checkDate();" AutoPostBack="true" OnTextChanged="txtExamDate1_TextChanged" />
                                                                <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgExamDate" TargetControlID="txtExamDate" Enabled="true" OnClientDateSelectionChanged="checkDate" />
                                                                <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" Mask="99/99/9999" MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtExamDate" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" ControlExtender="meExamDate" ControlToValidate="txtExamDate" Display="None" EmptyValueMessage="Please Enter Exam Date" ErrorMessage="Please Enter Exam Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid" IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit1" />
                                                                <asp:RequiredFieldValidator ID="rfvExamDate" runat="server" ControlToValidate="txtExamDate" Display="None" ErrorMessage="Please select Exam Date!!" ValidationGroup="Submit1"></asp:RequiredFieldValidator>
                                                            </div>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSlot" runat="server" AppendDataBoundItems="true" CausesValidation="true" TabIndex="1" OnSelectedIndexChanged="ddlSlot1_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdf_slotno" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                            <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlSlot" Display="None"
                                                                ErrorMessage="Please Select Slot" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit1" />
                                                        </td>
                                                        <td id="ddlmodeexamhide" style="display: none">
                                                            <asp:DropDownList ID="ddlmodeexam" runat="server" AppendDataBoundItems="true" CausesValidation="true" TabIndex="1" Visible="false">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdf_modeexam" runat="server" Value='<%# Eval("ModeOfEXAMNO")%>' />
                                                        </td>
                                                         <td>
                                                            <asp:ImageButton ID="ibtnEvalDelete1" runat="server"
                                                                ImageUrl="~/images/delete.gif" AlternateText="CANCEL RECORD" ToolTip='<%# Eval("CCODE")%>'
                                                                OnClick="ibtnEvalDelete1_Click" OnClientClick="return showConfirm();" CommandArgument='<%# Eval("CCODE")%>' TabIndex="13" />
                                                       
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">

        $(function () {
            debugger
            $("[id*=cbHead]").on("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.
                    if (cbHead.is(":checked")) {
                        $(this).prop("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");
                        $("select").removeAttr("disabled");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).prop("disabled", "disabled");
                        $("select", td).prop("disabled", "disabled");
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").on("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in the Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).prop("disabled", "disabled");
                    $("select", td).prop("disabled", "disabled");

                    // $("input[type=text]", td).val('');
                    //  $("select", td).val('');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                    $("select", td).removeAttr("disabled");

                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkAccept]", List).length == $("[id*=chkAccept]:checked", List).length) {
                    cbHead.prop("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("[id*=cbHead]").on("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.
                    if (cbHead.is(":checked")) {
                        $(this).prop("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");
                        $("select").removeAttr("disabled");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).prop("disabled", "disabled");
                        $("select", td).prop("disabled", "disabled");
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").on("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in the Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).prop("disabled", "disabled");
                    $("select", td).prop("disabled", "disabled");

                    // $("input[type=text]", td).val('');
                    //  $("select", td).val('');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                    $("select", td).removeAttr("disabled");

                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkAccept]", List).length == $("[id*=chkAccept]:checked", List).length) {
                    cbHead.prop("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });

    </script>

    <script>
        function showConfirm() {
            var ret = confirm('Do you really want to cancel Time Table?');
            if (ret == true) {
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>
    <script>
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                alert("You cannot select a day earlier than today!")
                return false;
            } else {
                return true;
            }
        }
    </script>
    <script>
        function checkDate1(sender, args) {
            if (sender._selectedDate < new Date()) {
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                alert("You cannot select a day earlier than today!")
                return false;
            } else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .btn-primary {
            /*height: 26px;*/
        }
    </style>
</asp:Content>

