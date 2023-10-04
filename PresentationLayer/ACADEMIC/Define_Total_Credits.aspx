<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Define_Total_Credits.aspx.cs" Inherits="ACADEMIC_Define_Total_Credits" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <style>
        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div16" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-6 col-lg-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AppendDataBoundItems="true" ToolTip="Select College">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCollege"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please select College."
                                                    InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>
                                                        <asp:Label runat="server" ID="lblDYddlScheme"></asp:Label></label>
                                                </div>
                                                <asp:ListBox ID="lstbxSession" runat="server" AppendDataBoundItems="true" TabIndex="6"
                                                    CssClass="multi-select-demo" SelectionMode="multiple" AutoPostBack="true"
                                                    OnSelectedIndexChanged="lstbxSession_SelectedIndexChanged"></asp:ListBox>
                                                <%--  <asp:RequiredFieldValidator ID="rfvlstbxSession" runat="server" ControlToValidate="lstbxSession"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please select Scheme."
                                            InitialValue="0" />--%>
                                                <asp:RequiredFieldValidator ID="rfvlstbxSession1" ControlToValidate="lstbxSession" InitialValue=""
                                                    Display="None" ValidationGroup="Submit" runat="server" ErrorMessage="Please select Scheme."></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>
                                                        <asp:Label ID="lblDYddlSemester" runat="server"></asp:Label></label>
                                                </div>
                                                <asp:ListBox ID="lstbxSemester" runat="server" AppendDataBoundItems="true" TabIndex="6"
                                                    CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="lstbxSemester"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please select Semester." InitialValue="" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="Div11" visible="false" runat="server">
                                                <div class="label-dynamic" id="Div8" visible="false" runat="server">
                                                    <sup>*</sup>
                                                    <label>Minimum Scheme Limit</label>
                                                </div>
                                                <asp:CheckBox ID="chkMinimumSchemeLimit" runat="server" AutoPostBack="true" OnCheckedChanged="chkMinimumSchemeLimit_CheckedChanged" />Minimum Scheme Limit
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Minimum Credits Limit</label>
                                                </div>
                                                <asp:TextBox ID="txtFromCredit" runat="server" MaxLength="2" ondrop="return false;" onpaste="return false;" AutoComplete="OFF" CssClass="form-control" placeholder="Minimum Credits" ToolTip="Enter Minimum Credit Limit" />
                                                <asp:RequiredFieldValidator ID="rfvFromCredit" runat="server" ControlToValidate="txtFromCredit"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Minimum credits." />

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtFromCredit" ValidChars="0123456789" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:CompareValidator ID="cmpvCredit" ControlToCompare="txtFromCredit" ControlToValidate="txtToCredits" runat="server" Operator="GreaterThan" Type="Integer" Display="None" ValidationGroup="Submit" ErrorMessage="Minimum Credit should be less than Maximum Credit"></asp:CompareValidator>

                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="Divmaxschem" visible="false" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Maximum Scheme Limit</label>
                                                </div>
                                                <asp:CheckBox ID="chkMaximumSchemeLimit" runat="server" AutoPostBack="true" OnCheckedChanged="chkMaximumSchemeLimit_CheckedChanged" />
                                                Maximum Scheme Limit
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divmaxcreditlimit" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Maximum Credits Limit</label>
                                                </div>
                                                <asp:TextBox ID="txtToCredits" runat="server" MaxLength="2" ondrop="return false;" onpaste="return false;" AutoComplete="OFF" CssClass="form-control" placeholder="Maximum Credits" ToolTip="Enter Maximum Credit Limit" />

                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div12" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Maximum Core Credits Limit</label>
                                                </div>
                                                <asp:TextBox ID="txtCoreCredits" runat="server" MaxLength="2" ondrop="return false;" onpaste="return false;" AutoComplete="OFF" CssClass="form-control" placeholder="Maximum Credits" ToolTip="Enter Maximum Credit Limit" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCoreCredits"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Maximum Core credits." />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCoreCredits" ValidChars="0123456789" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div13" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Maximum Elective Credits Limit</label>
                                                </div>
                                                <asp:TextBox ID="txtElectiveCredits" runat="server" MaxLength="2" ondrop="return false;" onpaste="return false;" AutoComplete="OFF" CssClass="form-control" placeholder="Maximum Credits" ToolTip="Enter Maximum Credit Limit" />
                                                <%-- <span id="error" style="color: Red; display: none">* Enter Numeric value</span>--%>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtElectiveCredits"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Maximum Elective credits." />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtElectiveCredits" ValidChars="0123456789" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <%--<asp:CompareValidator ID="cvToCredits" ControlToCompare="txtFromCredit" ControlToValidate="txtToCredits" runat="server"  Operator="GreaterThan" Type="Integer" Display="None"  ValidationGroup="Submit" ErrorMessage="Minimum Credit should be less than Maximum Credit"></asp:CompareValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div14" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Maximum Global Credits Limit</label>
                                                </div>
                                                <asp:TextBox ID="txtGlobalCredits" runat="server" MaxLength="2" ondrop="return false;" onpaste="return false;" AutoComplete="OFF" CssClass="form-control" placeholder="Maximum Credits" ToolTip="Enter Maximum Credit Limit" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtGlobalCredits"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Maximum Global credits." />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtGlobalCredits" ValidChars="0123456789" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" style="display: none">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Elective</label>
                                                </div>
                                                <asp:ListBox ID="lstbxElective" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit" TabIndex="6"
                                                    CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="true"
                                                    OnSelectedIndexChanged="lstbxElective_SelectedIndexChanged"></asp:ListBox>
                                               <%-- <asp:RequiredFieldValidator ID="rfvElective" runat="server" ControlToValidate="lstbxElective"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please select Elective."
                                                    InitialValue="" />--%>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" style="display: none">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Choice for</label>
                                                </div>
                                                Choices for Elective
                                                <asp:TextBox ID="txtElectiveChoiseFor" runat="server" MaxLength="2" ondrop="return false;"
                                                    onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);"
                                                    onpaste="return false;" AutoComplete="OFF" CssClass="form-control" placeholder="Choice For Elective"
                                                    ToolTip="Enter Choice For selected Elective" />
                                               <%-- <asp:RequiredFieldValidator ID="rfvElectiveChoise" runat="server" ControlToValidate="txtElectiveChoiseFor" SetFocusOnError="true"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please select Elective Choice For." />--%>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div15" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Maximum Overload Credits Limit</label>
                                                </div>
                                                <asp:TextBox ID="txtOverloadCreditLimit" runat="server" MaxLength="2" ondrop="return false;" onpaste="return false;" AutoComplete="OFF" CssClass="form-control" placeholder="Maximum Credits" ToolTip="Enter Maximum Credit Limit" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtOverloadCreditLimit" ValidChars="0123456789" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12 d-none">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Select Degree">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDegree"
                                                    ValidationGroup="a" Display="None" ErrorMessage="Please select Degree."
                                                    InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="Div1" visible="false" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Admission Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlAdmissionType" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Regular</asp:ListItem>
                                                    <asp:ListItem Value="2">Direct Second Year</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAdmissionType"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please select Admission type."
                                                    InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="Div2" visible="false" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Student  Result</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStudentType" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">All Pass</asp:ListItem>
                                                    <asp:ListItem Value="2">Backlog</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvStudentType" runat="server" ControlToValidate="ddlStudentType"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please select Student Result."
                                                    InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="Div3" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>From  CGPA</label>
                                                </div>
                                                <asp:TextBox ID="txtFromRange" runat="server" MaxLength="4" CssClass="form-control" placeholder=" Range CGPA" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteFromrange" runat="server" TargetControlID="txtFromRange" ValidChars="0123456789." FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rfvFromrange" runat="server" ControlToValidate="txtFromRange"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter From  CGPA." />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>To  CGPA</label>
                                                </div>
                                                <asp:TextBox ID="txtToRange" runat="server" MaxLength="4" placeholder="Range CGPA" CssClass="form-control" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtToRange" ValidChars="0123456789." FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToRange"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter To  CGPA." />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="mincredregular" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Minimum Regular Credit limit</label>
                                                </div>
                                                <asp:TextBox ID="txtMinRegCredit" runat="server" MaxLength="4" placeholder="Minimum Regular Credit limit" CssClass="form-control" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMinRegCredit" ValidChars="0123456789." FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <%--            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMinRegCredit"
                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Minimum Regular Credit Limit." />--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div7" visible="false" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Additional Courses Learning</label>
                                                </div>
                                                <asp:CheckBox ID="chkAddionalCourse" runat="server" AutoPostBack="true" OnCheckedChanged="chkAddionalCourse_CheckedChanged" />
                                                Additional Courses Learning
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="trAdditionalCourses">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Additional courses Degree Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlAdditionalCourseDegree" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">UG</asp:ListItem>
                                                    <asp:ListItem Value="2">UG + PG</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAdditionalCourseDegree"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please select Additional courses Degree Type."
                                                    InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-12" id="Div10" runat="server" visible="false">
                                                <div class=" note-div">
                                                    <h5 class="heading">Note</h5>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Total Credit Definition for Course Registration </span></p>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12 ">
                                        <div class="sub-heading">
                                            <h5>Choices for Elective</h5>
                                        </div>
                                        <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanel">
                                            <asp:ListView ID="lvElectiveGrpChoice" runat="server" ItemPlaceholderID="PlaceHolder1">
                                                <LayoutTemplate>
                                                    <div class="table-responsive" style="height: 360px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>
                                                                    <th>Elective
                                                                    </th>
                                                                    <th>Choice For Elective
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <%--   <tr id="itemPlaceholder" runat="server" />--%>
                                                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("GROUPNAME")%>
                                                            <asp:HiddenField ID="hdfElectiveGroupNo" runat="server" Value=' <%# Eval("GROUPNO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtElectiveChoiseFor1" runat="server" MaxLength="2" ondrop="return false;"
                                                                onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);"
                                                                onpaste="return false;" AutoComplete="OFF" CssClass="form-control" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtElectiveChoiseFor1" ValidChars="0123456789" FilterMode="ValidChars">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div style="text-align: center; font-family: Arial; font-size: medium">
                                                        No Record Found
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group col-md-6" id="Div6" runat="server" visible="false">
                                <label><span style="color: red; font: bold">*</span> Semester </label>
                                <div>
                                    <asp:CheckBoxList ID="chkListTerm" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" ToolTip="Select Semester">
                                    </asp:CheckBoxList>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Submit" />
                                <asp:Button ID="btnLock" runat="server" Text="Lock" OnClick="btnLock_Click" CssClass="btn btn-warning" Visible="false" ToolTip="Lock" />
                                <asp:Button ID="btnExcel" runat="server" Text="Excel Report" OnClick="btnExcel_Click" CssClass="btn btn-info" ToolTip="Export to Excel" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Cancel" />

                            </div>

                            <div class="col-12" id="divTracking" runat="server">
                                <asp:Panel ID="Panel3" runat="server">
                                    <asp:Panel ID="pnlCredit" runat="server">
                                        <asp:ListView ID="lvCredit" runat="server" OnItemDataBound="lvCredit_ItemDataBound">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Credit Definition</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue mb-0">
                                                        <tr>
                                                            <th style="text-align: center; width: 5%">Edit
                                                            </th>
                                                            <th style="text-align: center; width: 5%">Show
                                                            </th>
                                                            <th style="text-align: center; width: 10%">Unique Group No</th>
                                                            <th style="text-align: center; width: 15%">College Name
                                                            </th>
                                                            <%-- <th style="text-align: center; width: 10%">Additional Course
                                                            </th>--%>
                                                            <th style="text-align: center; width: 8%">Min.Cr. Limit
                                                            </th>
                                                            <th style="text-align: center; width: 8%">Max. Cr. Limit
                                                            </th>
                                                            <th style="text-align: center; width: 12%">Core Credit Limit
                                                            </th>
                                                            <th style="text-align: center; width: 12%">Elective Credit Limit
                                                            </th>
                                                            <th style="text-align: center; width: 12%">Global Credit Limit
                                                            </th>
                                                            <th style="text-align: center; width: 12%">Overload Credit Limit
                                                            </th>
                                                            <th style="text-align: center; width: 8%">Lock
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <table class="table table-hover table-bordered mb-0">
                                                    <tr id="MAIN" runat="server" class="col-md-12">
                                                        <td>
                                                            <tr>
                                                                <td style="text-align: center; width: 5%">
                                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                        CommandArgument='<%# Eval("GROUPID") %>'
                                                                        ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click1" TabIndex="10" ToolTip='<%# Eval("GROUPID") %>' />
                                                                </td>
                                                                <td style="text-align: center; width: 5%">
                                                                    <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                        <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />
                                                                    </asp:Panel>
                                                                </td>
                                                                <td style="text-align: center; width: 10%">
                                                                    <%# Eval("GROUPID") %>
                                                                </td>
                                                                <td style="text-align: center; width: 15%">
                                                                    <%# Eval("COLLEGE_NAME") %>
                                                                </td>
                                                                <%--<td style="text-align: center; width: 10%">
                                                                    <%# Eval("ADDITIONAL_COURSE_NAME") %>
                                                                </td>--%>
                                                                <td style="text-align: center; width: 8%">
                                                                    <%# Eval("FROM_CREDIT")%>
                                                                </td>
                                                                <td style="text-align: center; width: 8%">
                                                                    <%# Eval("TO_CREDIT")%>
                                                                </td>
                                                                <td style="text-align: center; width: 12%">
                                                                    <%# Eval("CORE_CREDIT")%>
                                                                </td>
                                                                <td style="text-align: center; width: 12%">
                                                                    <%# Eval("ELECTIVE_CREDIT")%>
                                                                </td>
                                                                <td style="text-align: center; width: 12%">
                                                                    <%# Eval("GLOBAL_CREDIT")%>
                                                                </td>
                                                                <td style="text-align: center; width: 12%">
                                                                    <%# Eval("OVERLOAD_CREDIT")%>
                                                                </td>
                                                                <td style="text-align: center; width: 8%">
                                                                    <%# Eval("LOCK")%>
                                                                </td>
                                                                <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails"
                                                                    Collapsed="true" CollapsedImage="~/Images/action_down.png" ExpandControlID="pnlDetails"
                                                                    ExpandedImage="~/Images/action_up.png" ImageControlID="imgExp" TargetControlID="pnlShowCDetails">
                                                                </ajaxToolKit:CollapsiblePanelExtender>
                                                            </tr>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">

                                                        <asp:ListView ID="lvDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Scheme Name
                                                                                </th>
                                                                                <th>Semester
                                                                                </th>
                                                                                <th>Elective
                                                                                </th>
                                                                                <th>Choice For Elective
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server">
                                                                            </tr>
                                                                        </tbody>

                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                    No Record Found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%# Eval("SCHEMENAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TERM_TEXT")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("GROUPNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# ((Eval("ELECTIVE_CHOISEFOR").ToString() != string.Empty) ? Eval("ELECTIVE_CHOISEFOR") : "0") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>

                                                    </asp:Panel>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>

                            <%-- <div class="col-12">
                                <asp:Panel ID="Panel3" runat="server">

                                    <asp:ListView ID="lvCredit" runat="server" OnLayoutCreated="OnLayoutCreated">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Credit Definition</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th style="text-align: center">Scheme Name</th>
                                                        
                                                        <th id="addth" runat="server" visible="false">Additional Course</th>
                                                        <th>Min.Cr. Limit</th>
                                                        <th>Max. Cr. Limit</th>
                                                        <th>Core Credit Limit</th>
                                                        <th>Elective Credit Limit</th>
                                                        <th>Global Credit Limit</th>
                                                        <th id="minschemth" runat="server" visible="false">Min. Scheme Limit </th>
                                                        <th id="maxschemth" runat="server" visible="false">Max. Scheme Limit </th>
                                                        <th>Semester </th>
                                                        <th>Lock </th>
                                                        <th>Elective </th>
                                                        <th>Choice For Elective </th>
                                                        <th id="minregcredth" runat="server" visible="false">Min. Reg. Cred. Lim. </th>
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
                                                    <asp:ImageButton ID="ddlse" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("IDNO") %>' AlternateText="Edit Record"
                                                        OnClick="btnEdit_Click" TabIndex="10" />
                                                </td>

                                                <td><%# ((Eval("SCHEMENAME").ToString() != string.Empty) ? Eval("SCHEMENAME") : "--") %></td>
                                               
                                                <td id="addtd" runat="server" visible="false">
                                                    <%# ((Eval("ADDITIONAL_COURSE_NAME").ToString() != string.Empty) ? Eval("ADDITIONAL_COURSE_NAME") : "--") %>
                                                </td>
                                               
                                                <td>
                                                    <%# ((Eval("FROM_CREDIT").ToString() != string.Empty) ? Eval("FROM_CREDIT") : "--") %>
                                                </td>
                                                <td>
                                                    <%# ((Eval("TO_CREDIT").ToString() != string.Empty) ? Eval("TO_CREDIT") : "--") %>
                                                </td>
                                                <td>
                                                    <%# ((Eval("CORE_CREDIT").ToString() != string.Empty) ? Eval("CORE_CREDIT") : "--") %>
                                                </td>
                                                <td>
                                                    <%# ((Eval("ELECTIVE_CREDIT").ToString() != string.Empty) ? Eval("ELECTIVE_CREDIT") : "--") %>
                                                </td>
                                                 <td>
                                                    <%# ((Eval("GLOBAL_CREDIT").ToString() != string.Empty) ? Eval("GLOBAL_CREDIT") : "--") %>
                                                </td>

                                                <td id="minschemtd" runat="server" visible="false">
                                                    <%# ((Eval("MIN_SCHEME_LIMIT").ToString() != string.Empty) ? Eval("MIN_SCHEME_LIMIT") : "--") %>
                                                </td>

                                                <td id="maxschemtd" runat="server" visible="false">
                                                    <%# ((Eval("MAX_SCHEME_LIMIT").ToString() != string.Empty) ? Eval("MAX_SCHEME_LIMIT") : "--") %>
                                                </td>
                                                <td>
                                                    <%# ((Eval("TERM_TEXT").ToString() != string.Empty) ? Eval("TERM_TEXT") : "--") %>
                                                </td>
                                                <td>
                                                    <%# ((Eval("LOCK").ToString() != string.Empty) ? Eval("LOCK") : "--") %>
                                                </td>
                                                <td>
                                                    <%# ((Eval("GROUPNAME").ToString() != string.Empty) ? Eval("GROUPNAME") : "--") %>
                                                </td>
                                                <td>
                                                    <%# ((Eval("ELECTIVE_CHOISEFOR").ToString() != string.Empty) ? Eval("ELECTIVE_CHOISEFOR") : "0") %>
                                                </td>
                                                <td id="minregcredtd" runat="server" visible="false">
                                                    <%# ((Eval("MIN_REG_CREDIT_LIMIT").ToString() != string.Empty) ? Eval("MIN_REG_CREDIT_LIMIT") : "--") %>
                                                </td>
                                            </tr>

                                            <%--  </tbody>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }

        function Compare() {
            if (document.getElementById('<%=txtFromCredit.ClientID%>').value < document.getElementById('<%=txtToCredits.ClientID%>').value) {
                alert('From credit can not be greater than to credit');
            }
        }

        function clearSearchKey() {
            $('#<%=txtToCredits.ClientID%>').val('');
        }

        //function eye(id) {
        //    var btnid = "" + id.id + "";
        //    var myArray = new Array();
        //    myArray = btnid.split('_');
        //    var index = myArray[3];
        //    var str = document.getElementById("ctl00_ContentPlaceHolder1_lstbxSession").value; //+ index + "_lstbxSections"

        //    if (str.length <= 0) {
        //        alert("Please select section.");
        //        btnid.disabled = true;
        //        return false
        //    }
        //    else
        //        return true;           
        //}
    </script>

    <%--<script>
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });

        //$(document).ready(function () {
        //    $('.multi-select-demo').multiselect();
        //    var prm = Sys.WebForms.PageRequestManager.getInstance();
        //    prm.add_endRequest(function () {
        //        $('.multi-select-demo').multiselect({
        //            includeSelectAllOption: true,
        //            enableFiltering: true,
        //            filterPlaceholder: 'Search',
        //            enableCaseInsensitiveFiltering: true,
        //            enableHTML: true,
        //            templates: {
        //                filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
        //                filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 20px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
        //            }
        //            //dropRight: true,
        //            //search: true,
        //        });

        //    });
        //});
    </script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
</asp:Content>

