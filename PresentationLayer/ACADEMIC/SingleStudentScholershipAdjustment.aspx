<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SingleStudentScholershipAdjustment.aspx.cs" Inherits="ACADEMIC_SingleStudentScholershipAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .table-responsive > .table-bordered {
            border: 1px solid #f8ecec;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSingleStud"
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

    <asp:UpdatePanel ID="updSingleStud" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <div class="box-body">
                                <asp:Panel ID="pnlbody" runat="server">
                                    <div class="col-12" id="divsearchbar" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Search Criteria</label>
                                                </div>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true"
                                                    data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                <asp:Panel ID="pnltextbox" runat="server">
                                                    <div id="divtxt" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Search String</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStudent" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlDropdown" runat="server">
                                                    <div id="divDropDown" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Search String</label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </asp:Panel>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="Show" />
                                        <asp:Label ID="Label5" runat="server" Font-Bold="true" Visible="false" />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlLV" runat="server">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <h5>Student List</h5>
                                                        </div>

                                                        <%-- <asp:Panel ID="Panel2" runat="server">
                                                            <div class="table-responsive">
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr. No.
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Adm. Status
                                                                            </th>
                                                                            <th style="display: none">IdNo
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Father Name
                                                                            </th>
                                                                            <th>Mother Name
                                                                            </th>
                                                                            <th>Mobile No.
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Container.DataItemIndex + 1%>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAdmcan" Font-Bold="true" runat="server" ForeColor='<%# Eval("ADMCANCEL").ToString().Equals("ADMITTED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ADMCANCEL")%>'></asp:Label>
                                                        </td>
                                                        <td style="display: none">
                                                            <%# Eval("idno")%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                        </td>
                                                        <td>
                                                            <%# Eval("SEMESTERNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FATHERNAME") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("MOTHERNAME") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("STUDENTMOBILE") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>--%>

                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <div class="table-responsive">
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr. No.
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Scholarship  Type
                                                                            </th>
                                                                            <th>Scholarship Applied Amount
                                                                            </th>
                                                                            <th>Scholarship  Paid Amount
                                                                            </th>
                                                                            <th>Year
                                                                            </th>
                                                                            <th>Academic Year
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Container.DataItemIndex + 1%>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                OnClick="lnkId_Click" ToolTip='<%# Eval("SCHOLARSHIP_ID")%>' CommandName='<%# Eval("SEMESTERNO")%>'></asp:LinkButton>

                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblScholershipType" runat="server" Text='<%# Eval("SCHOLORSHIPNAME")%>' ToolTip='<%# Eval("SCHOLARSHIP_ID")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblSchlAppliedAmt" runat="server" Text='<%# Eval("SCHL_AMOUNT")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblSchlPaidAmt" runat="server" Text='<%# Eval("SCH_ADJ_AMT")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblSchlYear" runat="server" Text='<%# Eval("YEAR")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblSchlAcdYear" runat="server" Text='<%# Eval("ACADEMIC_YEAR_NAME")%>' ToolTip='<%# Eval("ACADEMIC_YEAR_ID")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>



                                <div class="col-12" runat="server" id="divSingleStudDetail">

                                    <div class="sub-heading mt-3">
                                        <h5>Student Information </h5>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="false" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="false"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYlvAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="false"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblSchlName" runat="server" Font-Bold="true" Text="Scholership Type"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblschlType" runat="server" Font-Bold="false"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblAcadYear" runat="server" Font-Bold="true" Text="Academic Year"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblAcademicYear" runat="server" Font-Bold="false"></asp:Label></a>
                                                </li>



                                            </ul>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblYearhead" Text="Year" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblYear" runat="server" Font-Bold="false"></asp:Label>
                                                    </a>
                                                </li>

                                                <li class="list-group-item d-none"><b>
                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="false"></asp:Label>
                                                    </a>
                                                </li>

                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>-<asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="false"></asp:Label>
                                                        <asp:Label ID="lblDegrreno" runat="server" Font-Bold="false"></asp:Label>
                                                    </a>
                                                </li>

                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="false"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSingCollege" runat="server" Font-Bold="false"></asp:Label>
                                                    </a>
                                                </li>

                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblSemster" runat="server" Font-Bold="true" Text="Semester"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemsterName" runat="server" Font-Bold="false"></asp:Label>
                                                    </a>
                                                </li>

                                            </ul>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <%-- <div class="form-group col-lg-3 col-md-6 col-12" >
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Academic Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select  Academic Year" ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                        </div>--%>

                                        <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">

                                                <asp:Label ID="lblYearMandatory" runat="server" Style="color: red" Visible="false">*</asp:Label>

                                                 

                                                <asp:Label ID="lblDYddlYear" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Year" CssClass="form-control" data-select2-enable="true" TabIndex="5" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>--%>

                                        <%--  <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        <%-- </div>--%>


                                        <%--                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSemester" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemesterSing" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester" CssClass="form-control" data-select2-enable="true" TabIndex="5" OnSelectedIndexChanged="ddlSemesterSing_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>

                                        <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Scholorship Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlScholorshipType" runat="server" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlScholorshipType_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Receipt Type</label>
                                            </div>

                                            <asp:DropDownList ID="ddlReceiptSing" runat="server" CssClass="form-control" AppendDataBoundItems="True" ToolTip="Please Select Receipt Type" TabIndex="7" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Bank Name</label>
                                            </div>
                                            <%--<asp:TextBox ID="txtBankName" runat="server" TabIndex="8" MaxLength="64"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlBankName" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Bank" TabIndex="8" data-select2-enable="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divtodate" runat="server">
                                            <div class="label-dynamic">
                                                <label>Date</label>
                                            </div>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="dvcal2" runat="server" class="fa fa-calendar text-green"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="Show" TabIndex="9"
                                                    ToolTip="Please Enter To Date" CssClass="form-control" Style="z-index: 0;" />
                                                <%-- <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                AlternateText="Select Date" Style="cursor: pointer" />--%>
                                                <ajaxToolkit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDate" PopupButtonID="dvcal2" />
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" SetFocusOnError="True"
                                                    ErrorMessage="Please Enter To Date" ControlToValidate="txtToDate" Display="None"
                                                    ValidationGroup="Report" />
                                                <ajaxToolkit:MaskedEditExtender ID="meeToDate" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeToDate"
                                                    ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date"
                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Report" SetFocusOnError="True" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rbRegEx" runat="server" RepeatDirection="Horizontal" TabIndex="11" AutoPostBack="true">
                                                <asp:ListItem Value="0" Selected="True">&nbsp;Regular Student &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1">&nbsp;Ex Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="backsem"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <%--                                    <asp:Button ID="btnShowForSingle" runat="server" CssClass="btn btn-info" Font-Bold="true" OnClick="btnShowForSingle_Click" Text="Show" ValidationGroup="SingleSubmit" Visible="false" />--%>
                                    <asp:Button ID="btnSubmitForSingleStu" runat="server" CssClass="btn btn-primary" Font-Bold="true" OnClick="btnSubmitForSingleStu_Click" Text="Submit" ValidationGroup="SingleSubmit" Visible="false" Enabled="false" />
                                    <asp:Button ID="btnCancel1" runat="server" CssClass="btn btn-warning" Font-Bold="true" OnClick="btnCancel1_Click" Text="Cancel" Visible="false" />
                                    <asp:HiddenField ID="hftot" runat="server" />
                                    <asp:HiddenField ID="txtTotStud" runat="server" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvStudentRecords" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student Scholarship Information </h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this)" ToolTip="Select or Deselect All Records" Visible="true" />
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <%--   <th>Roll No.</th>--%>
                                                            <th>Student Name</th>

                                                            <th>
                                                                <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>

                                                            <th>Scholarship Applied Amount</th>
                                                            <th>Scholarship Due Amount</th>
                                                            <th>Scholarship Paid Amount</th>
                                                            <th>Scholarship Excess Amount</th>
                                                            <th>Adjusted Amount</th>
                                                            <th>DD Number/TransactionID</th>
                                                            <th>Scholarship Type</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>

                                            <ItemTemplate>
                                                <%--  <asp:UpdatePanel runat="server" ID="updList">
                                                <ContentTemplate>--%>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkReport" runat="server" onclick="totStudents(this);" ToolTip='<%# Eval("IDNO") %>' />
                                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <%--  <td><%# Eval("ROLLNO")%></td>--%>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                        <asp:HiddenField ID="hdfBranchno" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                        <asp:Label ID="lblname" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("ENROLLNO") %>' Visible="false"></asp:Label>
                                                        <%--<asp:HiddenField ID="hiddenBranch" runat="server" Value='<%#Eval ("BRANCHNO") %>' />--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>

                                                    <td>
                                                        <asp:HiddenField ID="hdfDegree" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                        <asp:Label ID="lblschamt" runat="server" Text='<%# Eval("SCHL_AMOUNT") %>' ToolTip='<%# Eval("SEMESTERNO") %>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <%--<%# Eval("SCHL_AMOUNT") %>--%>
                                                        <%-- <asp:HiddenField ID="hdfDegree" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                        <asp:Label ID="lblschamt" runat="server" Text='<%# Eval("SCHL_AMOUNT") %>' ToolTip='<%# Eval("SEMESTERNO") %>'></asp:Label>--%>
                                                        <asp:Label ID="lblAdjSchAmt" runat="server" Text='<%# Eval("DUEAMOUNT") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfSchAmt" runat="server" Value='<%# Eval("DUEAMOUNT") %>' />
                                                    </td>

                                                    <td>
                                                        <asp:TextBox ID="txtAdjAmount" runat="server" onkeyup="return IsNumeric(this);" MaxLength="6" Visible="false"></asp:TextBox>
                                                         <asp:TextBox ID="txtAdjustAmount" runat="server" Text='<%# Eval("TOBEPAIDAMT") %>' onkeyup="return IsNumeric(this);" MaxLength="9" Visible="false"></asp:TextBox>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblRemainingAmt" runat="server" Text='<%#Eval("SCHL_EXCESS_AMOUNT") %> ' />
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblschajmt" runat="server" Text='<%#Eval("SCH_ADJ_AMT") %> '></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:TextBox ID="txtDDNumber" runat="server"></asp:TextBox>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblDDLData" runat="server" Text='<%# Eval("SCHOLARSHIP_ID") %>' ToolTip='<%# Eval("LONGNAME") %>' Visible="false"></asp:Label>
                                                        <%#Eval("SCHOLORSHIPNAME") %>
                                                    </td>

                                                </tr>
                                                <%--</ContentTemplate>
                                                <Triggers>
                                                        <asp:PostBackTrigger ControlID="rdoYes" />
                                                        <asp:PostBackTrigger ControlID="rdoNo" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        //////////added by Amit B. on date 16/01/22

        $(document).ready(function () {
            debugger
            //$("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });

        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please select Criteria as you want search...')
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtStudent.ClientID %>').value;
                        if (searchtxt == "" || searchtxt == null) {
                            alert('Please Enter Data you want to search..');
                        }
                        else {
                            __doPostBack(btnsearch, rbText + ',' + searchtxt);
                            //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtStudent.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtStudent.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 32)) {
                return true;
            }
            else {
                return false;
            }

        }
    }

    ///////////
    </script>

</asp:Content>

