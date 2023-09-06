<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SendSmstoStudents_Faculty.aspx.cs" Inherits="ACADEMIC_SendSmstoStudents_Faculty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        /*#lstAttSecondsms .dataTables_scrollHeadInner {
            width: max-content !important;
        }*/
        #ctl00_ContentPlaceHolder1_pnlsecond .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <script type="text/javascript">

        function checkEmail() {

            var txtsub = $("[id$=txtsub]").attr("id");
            var txtsub = document.getElementById(txtsub);
            if (txtsub.value.length == 0) {
                alert('Please Enter Subject for Email!', 'Warning!');
                $(txtsub).focus();
                return false;
            }

            var txtMessage = $("[id$=txtMessage]").attr("id");
            var txtMessage = document.getElementById(txtMessage);
            if (txtMessage.value.length == 0) {
                alert('Please Enter Message for Email!', 'Warning!');
                $(txtMessage).focus();
                return false;
            }
        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }

    </script>
    <asp:UpdatePanel runat="server" ID="UpdRole" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom" id="Tabs">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Send Bulk Email & SMS</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Bulk Email Attendance Sending</a>
                                    </li>

                                    <li class="nav-item">
                                        <a class="nav-link " data-toggle="tab" href="#tab_3" tabindex="1">Send SMS/Email to Parents</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCollege"
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

                                        <asp:UpdatePanel ID="updCollege" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-4 col-md-6 col-12 mb-3">
                                                                <asp:RadioButtonList ID="rdbEmplyeStud" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" onclick="RadioClicked()" OnSelectedIndexChanged="rdbEmplyeStud_SelectedIndexChanged">

                                                                    <asp:ListItem Value="1">Employee Wise &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                    <asp:ListItem Value="2">Student Wise</asp:ListItem>
                                                                    <asp:ListItem Value="3">Parents</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div class="col-lg-8 col-md-12 col-12">
                                                                <asp:Panel ID="empPanel" runat="server">
                                                                    <div class="sub-heading">
                                                                        <h5>Employee Section</h5>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-14 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <%--<label>Department</label>--%>
                                                                                <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlDepartment" runat="server" TabIndex="1" AppendDataBoundItems="true" ValidationGroup="ShowEmployee" CssClass="form-control" data-select2-enable="true">
                                                                                <asp:ListItem>Please Select</asp:ListItem>
                                                                                <asp:ListItem></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment" SetFocusOnError="true"
                                                                                ErrorMessage="Select atleast one Department !" Display="None" InitialValue="0" ValidationGroup="ShowEmployee"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <asp:Button ID="btnShowEmploy" runat="server" class="btn btn-primary mt-3" ValidationGroup="ShowEmployee" OnClick="btnShowEmploy_Click" TabIndex="1" Text="Show Employee" ToolTip="Show Employee" />
                                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                                ShowSummary="false" ValidationGroup="ShowEmployee" />
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-12">
                                                        <asp:Panel ID="Studpanel" runat="server">
                                                            <div class="sub-heading">
                                                                <h5>Student Section</h5>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>School/Institute Name</label>--%>
                                                                        <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlSchool" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSchool" SetFocusOnError="true"
                                                                        ErrorMessage="Please Select School/Institute Name" InitialValue="0" Display="None" ValidationGroup="ShowStudent"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>Degree</label>--%>
                                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        <asp:ListItem></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                        ErrorMessage="Select atleast one Degree" InitialValue="0" Display="None" ValidationGroup="ShowStudent"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>Programme/Branch</label>--%>
                                                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        <asp:ListItem></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvBranch1" runat="server" ControlToValidate="ddlBranch"
                                                                        ErrorMessage="Select atleast one Branch" Display="None" InitialValue="0" ValidationGroup="ShowStudent"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>Semester</label>--%>
                                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlsemester" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        <asp:ListItem></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvSem1" runat="server" ControlToValidate="ddlsemester"
                                                                        ErrorMessage="Select atleast one Semester " Display="None" InitialValue="0" ValidationGroup="ShowStudent"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <asp:RadioButtonList ID="rboStudent" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rboStudent_SelectedIndexChanged">
                                                                        <asp:ListItem Value="-1" style="display: none"> </asp:ListItem>
                                                                        <asp:ListItem Value="1">Fees not Paid &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                        <asp:ListItem Value="2">Installment Wise dues not paid</asp:ListItem>
                                                                        <asp:ListItem Value="3">Sem Promotion Admission Form</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <asp:Panel ID="pnldate" runat="server" Visible="false">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>*</sup>
                                                                                <label>From Date</label>
                                                                            </div>
                                                                            <div class="input-group">
                                                                                <div class="input-group-addon" id="txtStartDate1" runat="server">
                                                                                    <i class="fa fa-calendar"></i>
                                                                                </div>
                                                                                <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" TabIndex="5" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                                                                <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtStartDate" PopupButtonID="txtStartDate1" />
                                                                                <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                                                    TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                                <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                                                    ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" ErrorMessage="Start Date is Invalid (Enter dd/mm/yyyy Format)"
                                                                                    TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                                    ValidationGroup="submit" SetFocusOnError="True" />
                                                                                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                                                    Display="None" SetFocusOnError="True" ErrorMessage="Please Enter From Date"
                                                                                    ValidationGroup="ShowStudent" />
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>*</sup>
                                                                                <label>To Date </label>
                                                                            </div>
                                                                            <div class="input-group">
                                                                                <div class="input-group-addon" id="txtEndDate1" runat="server">
                                                                                    <i class="fa fa-calendar"></i>
                                                                                </div>
                                                                                <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" TabIndex="6" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtEndDate" PopupButtonID="txtEndDate1" />

                                                                                <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                                                    TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                                <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                                                    ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter To Date"
                                                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" IsValidEmpty="false"
                                                                                    TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />
                                                                                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                                                    ControlToValidate="txtEndDate" Display="None" ValidationGroup="ShowStudent" ErrorMessage="Please Enter To Date" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnShowStudent" runat="server" class="btn btn-primary" TabIndex="1" Text="Show Student" ValidationGroup="ShowStudent" ToolTip="Show Student" OnClick="btnShowStudent_Click" />
                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="ShowStudent" />
                                                            </div>

                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-lg-12 col-md-12 col-12">
                                                        <asp:Panel ID="TemplatePanel" runat="server">
                                                            <div class="sub-heading">
                                                                <h5>Template</h5>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group col-lg-12 col-md-6 col-12">
                                                                    <asp:RadioButtonList ID="rdbEmailSms" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbEmailSms_SelectedIndexChanged" AutoPostBack="true" onclick="RadioClicked()">
                                                                        <asp:ListItem Value="0">Email &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                        <asp:ListItem Value="1">SMS</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divTtype" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Template Type</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlTemplateType" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlTemplateType_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        <asp:ListItem></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divmbody" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Message Body</label>
                                                                    </div>
                                                                    <%--<asp:TextBox ID="txtTemplate" runat="server" TextMode="MultiLine"  Visible="false" ></asp:TextBox>--%>
                                                                    <asp:Label ID="lblTemplate" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12 mt-3" id="trTemplate" runat="server">
                                                        <asp:Panel runat="server" ID="Panel">
                                                            <asp:ListView ID="lvTemplate" runat="server" Visible="false">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Var ID
                                                                                </th>
                                                                                <th>Var Count
                                                                                </th>
                                                                                <th>Input Var
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server">
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblVarCount" runat="server" Text='<%# Eval("VARIABLE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtVarTemplate" runat="server" onchange="return ReplaceValue(this);" MaxLength="20"></asp:TextBox>
                                                                            <%--onblur="return getdisabled(this);"--%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                            <asp:HiddenField ID="hftt" runat="server" />
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12 mt-3" id="trEmployee" runat="server">
                                                        <asp:Panel ID="employeepanel" runat="server" Visible="true">
                                                            <asp:ListView ID="lvEmployee" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <br />
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Student To Shows" />
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Employee List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkSelect" TabIndex="1" runat="server" onclick="totAllSubjects(this)" />Select
                                                                                </th>
                                                                                <th>Employee Name
                                                                                </th>
                                                                                <th>Email ID
                                                                                </th>
                                                                                <th>Phone No.
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
                                                                            <asp:CheckBox ID="chkSelect" TabIndex="1" runat="server" ToolTip='<%# Eval("UA_NO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblFname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("UA_EMAIL")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblPhone" runat="server" Text=' <%# Eval("UA_MOBILE")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("UA_NO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblFname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("UA_EMAIL")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblPhone" runat="server" Text=' <%# Eval("UA_MOBILE")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12" id="trStudent" runat="server">
                                                        <asp:Panel ID="pnlstud" runat="server" Visible="true">
                                                            <asp:ListView ID="lvStudent" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <br />
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Student To Shows" />
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkSelect1" TabIndex="1" runat="server" onclick="totAllSubjects(this)" />Select
                                                                                </th>
                                                                                <th>Student Name
                                                                                </th>
                                                                                <th>Email ID
                                                                                </th>
                                                                                <th>Phone No.
                                                                                </th>
                                                                                 <th>Adm.Status 
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
                                                                            <asp:CheckBox ID="chkSelect1" TabIndex="1" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudname" runat="server" Text=' <%# Eval("STUDNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmailid" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudmobile" runat="server" Text=' <%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text=' <%# Eval("Admission_status")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkSelect1" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudname" runat="server" Text=' <%# Eval("STUDNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmailid" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudmobile" runat="server" Text=' <%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text=' <%# Eval("Admission_status")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12" id="Div2" runat="server">
                                                        <asp:Panel ID="pnlStudentInstallment" runat="server" Visible="true">
                                                            <asp:ListView ID="lvPaidStudentInstallment" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <br />
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Student To Shows" />
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkSelect2" TabIndex="1" runat="server" onclick="totAllSubjects(this)" />Select
                                                                                </th>
                                                                                <th>Student Name
                                                                                </th>
                                                                                <th>Email ID
                                                                                </th>
                                                                                <th>Phone No.
                                                                                </th>
                                                                                <th>Installment Due Date
                                                                                </th>
                                                                                <th>Installment No
                                                                                </th>
                                                                                <th>Installment Ammount
                                                                                </th>
                                                                                 <th>Adm.Status 
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
                                                                            <asp:CheckBox ID="chkSelect2" TabIndex="1" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudname" runat="server" Text=' <%# Eval("STUDNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmailid1" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudmobile" runat="server" Text=' <%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblduedate" runat="server" Text=' <%# Eval("DUE_DATE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblInstallmentno" runat="server" Text=' <%# Eval("INSTALMENT_NO")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblInstallmentamount" runat="server" Text=' <%# Eval("INSTALL_AMOUNT")%>'></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text=' <%# Eval("Admission_status")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkSelect2" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudname" runat="server" Text=' <%# Eval("STUDNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmailid1" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudmobile" runat="server" Text=' <%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblduedate" runat="server" Text=' <%# Eval("DUE_DATE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblInstallmentno" runat="server" Text=' <%# Eval("INSTALMENT_NO")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblInstallmentamount" runat="server" Text=' <%# Eval("INSTALL_AMOUNT")%>'></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text=' <%# Eval("Admission_status")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12" id="Div3" runat="server">
                                                        <asp:Panel ID="PnlNotPaidStudent" runat="server" Visible="true">
                                                            <asp:ListView ID="lvnotpaid" runat="server">
                                                                <%--<EmptyDataTemplate>
                                                                    <br />
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Student To Shows" />
                                                                </EmptyDataTemplate>--%>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkSelect3" TabIndex="1" runat="server" onclick="totAllSubjects(this)" />Select
                                                                                </th>
                                                                                <th>Student Name
                                                                                </th>
                                                                                <th>Email ID
                                                                                </th>
                                                                                <th>Phone No.
                                                                                </th>
                                                                                <th>Paid Amount
                                                                                </th>
                                                                                <th>Total Amount
                                                                                </th>
                                                                                 <th>Adm.Status 
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
                                                                            <asp:CheckBox ID="chkSelect3" TabIndex="1" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudname" runat="server" Text=' <%# Eval("STUDNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmailid3" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudmobile" runat="server" Text=' <%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPaid" runat="server" Text=' <%# Eval("TOTAL_AMOUNT_DCR")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblNotpaid" runat="server" Text=' <%# Eval("TOTAL_AMOUNT_DEMAND")%>'></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text=' <%# Eval("Admission_status")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkSelect3" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudname" runat="server" Text=' <%# Eval("STUDNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmailid3" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudmobile" runat="server" Text=' <%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPaid" runat="server" Text=' <%# Eval("TOTAL_AMOUNT_DCR")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblNotpaid" runat="server" Text=' <%# Eval("TOTAL_AMOUNT_DEMAND")%>'></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text=' <%# Eval("Admission_status")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>


                                                    <div class="col-12 mt-3">

                                                        <div id="divEmail" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Subject</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtsub" TabIndex="1" runat="server" ToolTip="Please enter subject for Mail"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group col-lg-8 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Message</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtMessage" TabIndex="1" runat="server" TextMode="MultiLine" ToolTip="Please Enter  message" Height="50px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMessage"
                                                                        ErrorMessage="Please Enter Message !" Display="None" SetFocusOnError="true" ValidationGroup="SendEmail"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSubject" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <label>Attach File</label>
                                                                    </div>
                                                                    <asp:FileUpload ID="fuAttachment" TabIndex="1" runat="server" AllowMultiple="true" />
                                                                    <%--<asp:FileUpload ID="FileUpload1" TabIndex="1" runat="server" AllowMultiple="true" />--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnSndMessg" runat="server" TabIndex="1" Enabled="false" CssClass="btn btn-primary" Text="Send Email" ValidationGroup="SendEmail" ToolTip="Send Email" Style="margin-left: 0px" OnClientClick="return checkEmail();" OnClick="btnSndMessg_Click" />&nbsp;
                                                            <asp:Button ID="btnSndSms" runat="server" TabIndex="1" Enabled="false" CssClass="btn btn-primary" Text="Send SMS" ValidationGroup="SendEmail" ToolTip="Send SMS" Style="margin-left: 0px"
                                                                OnClientClick="return ValidateMsg();" OnClick="btnSndSms_Click" />&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" TabIndex="1" CssClass="btn btn-warning" Text="Cancel" ToolTip="Clear All" Style="margin-left: 0px" OnClientClick="validcan()" OnClick="btnCancel_Click" />

                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="SendEmail" />
                                                        </div>

                                                    </div>
                                                </div>


                                                <div id="divMsg" runat="server">
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSndMessg" />
                                                <asp:PostBackTrigger ControlID="btnSndSms" />
                                                <asp:PostBackTrigger ControlID="btnCancel" />
                                                <%-- <asp:PostBackTrigger ControlID="btnShowStudent" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updReport"
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

                                        <asp:UpdatePanel ID="updReport" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--  <label>College & Scheme</label>--%>
                                                                    <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                                    ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="SubPercentage">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Session</label>--%>
                                                                    <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSession" runat="server" Font-Bold="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfSession" runat="server" ControlToValidate="ddlSession"
                                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                                            </div>



                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="dvFaculty" runat="server" visible="false">
                                                                <div class="label-dynamic" id="faculty" runat="server">
                                                                    <sup>* </sup>
                                                                    <label>Faculty</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True" TabIndex="3">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Faculty" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Semester</label>--%>
                                                                    <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" TabIndex="4" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<label>Subject Type</label>--%>
                                                                    <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSubjectType" runat="server" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                    ValidationGroup="Submit" TabIndex="5" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                            ErrorMessage="Please Select Subject Type" InitialValue="0" Display="None" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator> --%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<label>Section</label>--%>
                                                                    <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <%--<asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    ValidationGroup="teacherallot" TabIndex="6">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>--%>
                                                                <asp:ListBox ID="lboddlSection" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                                <%--  Reset the sample so it can be played again --%>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%> <%--ValidationGroup="Daywise"--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Theory/Practical/Tutorial</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddltheorypractical" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddltheorypractical_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Theory</asp:ListItem>
                                                                    <asp:ListItem Value="2">Practical</asp:ListItem>
                                                                    <asp:ListItem Value="3">Tutorial</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--  <asp:RequiredFieldValidator ID="rfvLTP" runat="server" ControlToValidate="ddltheorypractical"
                                         Display="None" ErrorMessage="Please Select Theory or Practical or Tutorial Type course" InitialValue="0" ValidationGroup="SubPercentage" ></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>From Date</label>
                                                                </div>
                                                                <div class="input-group">
                                                                    <div class="input-group-addon" id="txtFromDate1">
                                                                        <i class="fa fa-calendar text-blue"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit" CssClass="form-control"
                                                                        TabIndex="7" />
                                                                    <ajaxToolKit:CalendarExtender ID="cetxtFromDate" runat="server" Format="dd/MM/yyyy"
                                                                        PopupButtonID="txtFromDate1" TargetControlID="txtFromDate" Animated="true" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                                        MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                        TargetControlID="txtFromDate" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                                        ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                                        ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meFromDate"
                                                                        ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                                        ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Report" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>To Date</label>
                                                                </div>
                                                                <div class="input-group">
                                                                    <div class="input-group-addon" id="txtTodate1">
                                                                        <i class="fa fa-calendar text-blue"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTodate" runat="server" TabIndex="8" ValidationGroup="submit" CssClass="form-control" />
                                                                    <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtTodate1"
                                                                        TargetControlID="txtTodate" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                                        ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                                        ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />

                                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meToDate"
                                                                        ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                                        ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Report" />

                                                                    <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                                        ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                                        ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Operator</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOperator" runat="server" TabIndex="9" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                                    <asp:ListItem>&lt;=</asp:ListItem>
                                                                    <asp:ListItem Selected="True">&gt;=</asp:ListItem>
                                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                                    <asp:ListItem>=</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Percentage</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPercentage" runat="server" MaxLength="3" Text="0" CssClass="form-control"
                                                                    TabIndex="10"></asp:TextBox>
                                                                <asp:RangeValidator ID="rvPercentage" runat="server" ControlToValidate="txtPercentage"
                                                                    Display="None" ErrorMessage="Please Enter Valid Percentage." MaximumValue="101"
                                                                    MinimumValue="0" Type="Integer"></asp:RangeValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Subject</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSubject" runat="server" MaxLength="3" TextMode="MultiLine" CssClass="form-control" Placeholder="Please Enter Email Subject here"
                                                                    TabIndex="10"></asp:TextBox>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                                                                    Display="None" ErrorMessage="Please Enter Subject." ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Message</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMessageAtdEmail" runat="server" MaxLength="3" TextMode="MultiLine" CssClass="form-control" Placeholder="Please Enter Email Message here"
                                                                    TabIndex="10"></asp:TextBox>
                                                                <%--      <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtMessageAtdEmail"
                                                                    Display="None" ErrorMessage="Please Enter Email." ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="dvBatch" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Batch</label>--%>
                                                                    <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" ValidationGroup="SubPercentage" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Batch" ValidationGroup="SubPercentage">
                                        </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:TextBox ID="lblMailSendTo" runat="server" TextMode="MultiLine" Visible="false" ForeColor="Green" Font-Bold="true" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:TextBox ID="lblMailNorSendTo" runat="server" TextMode="MultiLine" Visible="false" ForeColor="Red" Font-Bold="true" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnShow" runat="server" Text="Show Students List" TabIndex="12"
                                                            ValidationGroup="SubPercentage" CssClass="btn btn-primary" OnClick="btnShow_Click" />

                                                        <asp:Button ID="btnSmsToParents" runat="server" Text="Send SMS to Parents" OnClick="btnSmsToParents_Click" Visible="false"
                                                            TabIndex="13"
                                                            ValidationGroup="SubPercentage" CssClass="btn btn-primary" />

                                                        <asp:Button ID="btnSmsToStudent" runat="server" Text="Send SMS to Parents" OnClick="btnSmsToStudent_Click"
                                                            Width="160px" CssClass="btn btn-primary" ValidationGroup="SubPercentage" />

                                                        <asp:Button ID="btnEmail" runat="server" Text="Send Email" OnClick="btnEmail_Click" Width="100px" CssClass="btn btn-primary" />

                                                        <asp:Button ID="btnWhatsapp" runat="server" Text="Send Whatsapp" OnClick="btnWhatsapp_Click" Enabled="false" CssClass="btn btn-primary" Visible="false" />

                                                        <asp:Button ID="btnCancelAtdEmail" runat="server" Text="Cancel" CausesValidation="false"
                                                            TabIndex="14" CssClass="btn btn-warning" OnClick="btnCancelAtdEmail_Click" />

                                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="SubPercentage" Style="text-align: center" />

                                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="True" ShowSummary="False" Style="text-align: center" ValidationGroup="Report" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:ListView ID="lvStudents" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <asp:Panel ID="pnlStudent" runat="server">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkSelect1" runat="server" onclick="totAllSubjects(this)" />Select</th>
                                                                                <th>Sr No. </th>
                                                                                <th><%--Reg No.--%>
                                                                                    <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Roll. No. </th>
                                                                                <th>Student Name </th>
                                                                                <th>Total Class</th>
                                                                                <th>Attended Class</th>
                                                                                <th>Percentage</th>
                                                                                <%-- <th>Previous Enrollment No. </th>--%>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNo")%>' />
                                                                    </td>
                                                                    <td><%# Container.DataItemIndex + 1 %></td>
                                                                    <td>
                                                                        <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("USNNO")%>'></asp:Label>
                                                                    </td>

                                                                    <td><%# Eval("ROLLNO")%></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblStudname" Text='<%# Eval("STUDENT_NAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTotalclass" runat="server" Text='<%# Eval("TOTAL_CLASSES") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTotalattendance" runat="server" Text='<%# Eval("TOTAL_ATTENDED_CLASSES") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblPercentage" runat="server" Text='<%# Eval(" TOTAL_PERCENTAGE") %>'></asp:Label>
                                                                    </td>
                                                                    <%--<td style="width: 15%" align="center">
                                                                <%--  <asp:Label runat="server" ID="lblMail" Text='<%# Eval("EMAILID")%>'></asp:Label>
                                                            </td>--%>

                                                                    <%--<td><%# Eval("REGNO")%></td>--%>

                                                                    <%--<td><%# Eval("ENROLLNO")%></td>--%>
                                                                    <%-- <td><%# Eval("ADM_TYPE")%></td>--%>
                                                                    <%--   <td> <asp:Label ID="lblAdmType" runat="server" Text='<%# Convert.ToInt32(Eval("IDTYPE"))==1 ? "REGULAR" : "D2D" %>'></asp:Label></td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%-- <asp:PostBackTrigger ControlID="btnShow" />--%>
                                              <%--  <asp:PostBackTrigger ControlID="btnEmail" />--%>
                                                <asp:AsyncPostBackTrigger ControlID="btnCancelAtdEmail" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div id="divMsg2" runat="server">
                                        </div>
                                    </div>

                                    <div class="tab-pane fade" id="tab_3">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updDetained"
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

                                        <asp:UpdatePanel ID="updDetained" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Format</label>
                                                                </div>
                                                                <asp:RadioButtonList ID="rdbFormat" runat="server" TabIndex="1" RepeatDirection="Horizontal"
                                                                    OnSelectedIndexChanged="rdbFormat_SelectedIndexChanged1" AutoPostBack="true">
                                                                    <asp:ListItem Value="1">&nbsp;SMS/Email - First Hour Absent&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                    <asp:ListItem Value="2">&nbsp;SMS/Email - Attendance Percentage (Subject Wise)&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                    <%--<asp:ListItem Value="3">&nbsp;SMS - CAT Marks&nbsp;&nbsp;&nbsp;</asp:ListItem>--%>
                                                                    <asp:ListItem Value="4">&nbsp;SMS/Email - Todays Students Attendance list </asp:ListItem>
                                                                    <asp:ListItem Value="5">&nbsp;SMS/Email - Parent Teacher Meeting </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="rfvformat" runat="server" ControlToValidate="rdbFormat"
                                                                    Display="None" ErrorMessage="Please Select SMS Format" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-6 col-md-12 col-12"></div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Session</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSessn" runat="server" AppendDataBoundItems="True" TabIndex="2" Font-Bold="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSessn_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSessn" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>College</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlcollege" runat="server" AppendDataBoundItems="True" TabIndex="3" ToolTip="Please Select Institute" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlcollege" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DIVDEG" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Degree</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDeg" runat="server" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                                                    ValidationGroup="report" OnSelectedIndexChanged="ddlDeg_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDeg" runat="server" ControlToValidate="ddlDeg" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DIVBRANCH">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Branch</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBranches" runat="server" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                                                    ValidationGroup="report" OnSelectedIndexChanged="ddlBranches_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranches" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DIVSEM">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemestr" runat="server" AppendDataBoundItems="True" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlSemestr_SelectedIndexChanged" ValidationGroup="report" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemestr" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivSemPM">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemPm" runat="server" AppendDataBoundItems="True" TabIndex="6" AutoPostBack="true" ValidationGroup="report" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemPm_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSemPm" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divscheme" runat="server" visible="false">
                                                                <label><span style="color: red;">*</span>Regulation</label>
                                                                <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged1" AutoPostBack="true" TabIndex="6" ValidationGroup="report">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DIVSECTION">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Section</label>
                                                                </div>

                                                                <asp:DropDownList ID="ddlSect" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSect_SelectedIndexChanged" TabIndex="6" ValidationGroup="report" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlSection" runat="server" ControlToValidate="ddlSect" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divFrmDate" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label for="city" id="lblFrmDate" runat="server">Attendance Date : </label>
                                                                    <label for="city" id="lblFrmDate1" runat="server" visible="false">Attendance From Date : </label>
                                                                </div>

                                                                <asp:TextBox ID="txtFromDat" runat="server" TabIndex="6" ValidationGroup="report"
                                                                    CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFromDat_TextChanged" />
                                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtFromDat" PopupButtonID="txtFromDat" OnClientDateSelectionChanged="selectfromdate" />

                                                                <ajaxToolKit:MaskedEditExtender ID="metxtFromDat" runat="server" TargetControlID="txtFromDat"
                                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                    MaskType="Date" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mevtxtFromDat" runat="server" EmptyValueMessage="Please Enter Attendance Date"
                                                                    ControlExtender="metxtFromDat" ControlToValidate="txtFromDat" IsValidEmpty="false"
                                                                    InvalidValueMessage=" Date is invalid" Display="None" ErrorMessage="Please Enter Attendance From Date"
                                                                    InvalidValueBlurredMessage="*" ValidationGroup="report" SetFocusOnError="true" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divslots" runat="server" visible="false">

                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Selected Date Slots</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSlot" runat="server" AppendDataBoundItems="True" TabIndex="6" ValidationGroup="report" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlSlot" runat="server" ControlToValidate="ddlSlot" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Slot" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divtodate" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label for="city">Attendance To Date : </label>
                                                                </div>
                                                                <asp:TextBox ID="txtAttToDate" runat="server" TabIndex="7" ValidationGroup="report"
                                                                    CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAttToDate_TextChanged" />
                                                                <ajaxToolKit:CalendarExtender ID="CEtxtAttToDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtAttToDate" PopupButtonID="txtAttToDate" OnClientDateSelectionChanged="selectfromdate" />

                                                                <ajaxToolKit:MaskedEditExtender ID="metxtAttToDate" runat="server" TargetControlID="txtAttToDate"
                                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                    MaskType="Date" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mevtxtAttToDate" runat="server" EmptyValueMessage="Please Enter Attendance To Date"
                                                                    ControlExtender="metxtAttToDate" ControlToValidate="txtAttToDate" IsValidEmpty="false"
                                                                    InvalidValueMessage=" Date is invalid" Display="None" ErrorMessage="Please Enter Attendance To Date"
                                                                    InvalidValueBlurredMessage="*" ValidationGroup="report" SetFocusOnError="true" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="DivDate" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Meeting Date</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMdate" runat="server" CssClass="form-control" MaxLength="90"></asp:TextBox>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="DivTime" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Meeting Time</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMTime" runat="server" CssClass="form-control" MaxLength="90"></asp:TextBox>
                                                            </div>

                                                            <div id="Div4" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <label>Subject</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSubjectsms" runat="server" MaxLength="3" TextMode="MultiLine" CssClass="form-control" Placeholder="Please Enter Email Subject here"
                                                                    TabIndex="10"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubjectsms"
                                                                    Display="None" ErrorMessage="Please Enter Subject." ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div id="Div5" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <label>Email</label>
                                                                </div>
                                                                <asp:TextBox ID="txtEmailsms" runat="server" MaxLength="3" TextMode="MultiLine" CssClass="form-control" Placeholder="Please Enter Email Message here"
                                                                    TabIndex="10"></asp:TextBox>
                                                                <%--                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPercentage"
                                                                    Display="None" ErrorMessage="Please Enter Email." ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divexam" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Exam Name</label>
                                                                </div>

                                                                <asp:DropDownList ID="ddlexam" runat="server" AppendDataBoundItems="True" TabIndex="6" CssClass="form-control" data-select2-enable="true" ValidationGroup="report">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">CAT - I</asp:ListItem>
                                                                    <asp:ListItem Value="2">CAT - II</asp:ListItem>
                                                                    <asp:ListItem Value="3">CAT - III</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlexam"
                                                                    Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divexamname" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Exam Name</label>
                                                                </div>

                                                                <asp:DropDownList ID="ddlIAMarks" runat="server" AppendDataBoundItems="True" TabIndex="6" CssClass="form-control" ValidationGroup="report">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="S1">CIE-1</asp:ListItem>
                                                                    <asp:ListItem Value="S2">CIE-2</asp:ListItem>
                                                                    <asp:ListItem Value="S3">CIE-3</asp:ListItem>
                                                                    <asp:ListItem Value="S4">EVENT-1</asp:ListItem>
                                                                    <asp:ListItem Value="S5">EVENT-2</asp:ListItem>
                                                                    <asp:ListItem Value="ALL">CIE MARKS</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvIAMark" runat="server" ControlToValidate="ddlIAMarks"
                                                                    Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:TextBox ID="txtSmsSend" runat="server" TextMode="MultiLine" Visible="false" ForeColor="Green" Font-Bold="true" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:TextBox ID="txtSmsNotSend" runat="server" TextMode="MultiLine" Visible="false" ForeColor="Red" Font-Bold="true" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnShowStudentlist" runat="server" Text="Show Students" ValidationGroup="report" TabIndex="7"
                                                            CssClass="btn btn-primary" OnClick="btnShowStudentlist_Click1" />
                                                        <asp:Button ID="Button1" runat="server" Text="demo" TabIndex="7"
                                                            CssClass="btn btn-primary" OnClick="Button1_Click" Visible="false" />



                                                        <asp:Button ID="btnSubmitSmsEmail" runat="server" Text="Send SMS" CssClass="btn btn-info" TabIndex="8"
                                                            OnClick="btnSubmitSmsEmail_Click" Visible="false" />
                                                        <asp:Button ID="btnWhatsAppAtt" runat="server" Text="Send WhatsApp" CssClass="btn btn-info" TabIndex="9" OnClick="btnWhatsAppAtt_Click" Visible="false" />

                                                        <asp:Button ID="btnEmailSms" runat="server" Text="Send Email" OnClick="btnEmailSms_Click" OnClientClick="return validateEmailForAttd();"
                                                            Width="100px" CssClass="btn btn-primary" Visible="false" />
                                                        <%-- <asp:Button ID="btnAttSms" runat="server" Text="Send SMS" OnClick="btnAttSms_Click" Width="100px" CssClass="btn btn-primary" Visible="false"/>--%>

                                                        <asp:Button ID="butCancelSmsEmail" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="9" OnClick="butCancelSmsEmail_Click" />

                                                        <asp:ValidationSummary ID="ValidationSummary6" runat="server" DisplayMode="List"
                                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <div class="label-dynamic">
                                                                    <label>Total Selected</label>
                                                                </div>
                                                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                                                    Style="text-align: center;" Font-Bold="True" Font-Size="Small"></asp:TextBox>
                                                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                                                    WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                                                <asp:HiddenField ID="hftot" runat="server" />
                                                            </div>
                                                            <div id="Div6" class="form-group col-md-3" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <label>SMS Balance : &nbsp</label>
                                                                </div>
                                                                <asp:Label ID="lblBalance" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="form-group col-md-3" id="divAttStatus" runat="server" style="display:none">
                                                                <div class="label-dynamic">
                                                                    <label>Attendance Type : &nbsp</label>
                                                                </div>
                                                                <asp:Label ID="lblAttStatus" ForeColor="#00cc00" Font-Bold="true" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12" runat="server" id="divFirstsms">
                                                        <asp:Panel ID="pnlfirst" runat="server" Visible="false">
                                                            <asp:ListView ID="lvfirstsms" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Sr. No.</th>
                                                                                <th align="center">
                                                                                    <asp:CheckBox ID="cbHead" runat="server" Text="Select"
                                                                                        onClick="SelectAllFirst(this);" TabIndex="10" />
                                                                                </th>
                                                                                <th>Univ. Reg. No.</th>
                                                                                <th>Student Name </th>
                                                                                <th>Father/Mother Mobile Number</th>
                                                                                <th>Father/Mother Email id</th>
                                                                                <th>StatusSMS</th>
                                                                                <th>StatusEMAIL</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td style="text-align: center"><%#Container.DataItemIndex+1 %></td>
                                                                        <td style="text-align: center">
                                                                            <asp:CheckBox ID="cbRow" runat="server" onClick="totSubjects(this);" TabIndex="11" />
                                                                            <%--                                                                                Enabled='<%#(Eval("STATUS").ToString()=="1"?"0":(
                                                                String.IsNullOrEmpty(Eval("FATHERMOBILE").ToString()) ? "0" : Eval("FATHERMOBILE")))  == "0" ?  false : true%>' />--%>  <%--comment by jay takalkhede as discussed with umesh sir 05/011/2022--%>
                                                                        </td>

                                                                        <td><%# Eval("ENROLLNO")%></td>
                                                                        <asp:HiddenField ID="hdnidno" Value='<%# Eval("IDNO")%>' runat="server" />
                                                                        <asp:HiddenField ID="hdnenroll" Value='<%# Eval("ENROLLNO")%>' runat="server" />
                                                                        <td><%# Eval("STUDNAME")%>
                                                                            <asp:HiddenField ID="hdnStuName" Value='<%# Eval("STUDNAME")%>' runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblParMobile" Text='<%# Eval("FATHERMOBILE")%>'>  </asp:Label>
                                                                            <asp:HiddenField ID="hdnDeptname" Value='<%# Eval("DEPTNAME")%>' runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblParEmail" Text='<%# Eval("FATHER_EMAIL")%>'>  </asp:Label>
                                                                        </td>
                                                                        <td><%# Eval("STATUS_Remark")%>
                                                                            <asp:HiddenField ID="hdnStatus" Value='<%# Eval("STATUS")%>' runat="server" />
                                                                        </td>
                                                                        <td><%# Eval("STATUS_Remark_EMAIL")%>
                                                                            <asp:HiddenField ID="hdstatusEmail" Value='<%# Eval("STATUS_EMAIL")%>' runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>


                                                    <div class="col-12" runat="server" id="divStudentmeeting">
                                                        <asp:Panel ID="PnlStudentmeeting" runat="server" Visible="false">
                                                            <asp:ListView ID="lvStudentMeeting" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Sr. No.</th>
                                                                                <th align="center">
                                                                                    <asp:CheckBox ID="cbHead" runat="server" Text="Select"
                                                                                        onClick="SelectAllFifth(this);" TabIndex="10" />
                                                                                </th>
                                                                                <th>Univ. Reg. No.</th>
                                                                                <th>Student Name </th>
                                                                                <th>Father/Mother Mobile Number</th>
                                                                                <th>Father/Mother Email id</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td style="text-align: center"><%#Container.DataItemIndex+1 %></td>
                                                                        <td style="text-align: center">
                                                                            <asp:CheckBox ID="cbRow" runat="server" onClick="totSubjects(this);" TabIndex="11" />
                                                                        </td>

                                                                        <td><%# Eval("REGNO")%>
                                                                            <asp:HiddenField ID="hdnidno" Value='<%# Eval("IDNO")%>' runat="server" />
                                                                            <asp:HiddenField ID="hdnenroll" Value='<%# Eval("REGNO")%>' runat="server" />
                                                                        </td>
                                                                        <td><%# Eval("STUDNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblParMobile" Text='<%# Eval("FATHERMOBILE")%>'>  </asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblParEmail" Text='<%# Eval("FATHER_EMAIL")%>'>  </asp:Label>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12 mt-3" runat="server" id="divattendancesecondsms">
                                                        <asp:Panel ID="pnlsecond" runat="server" Visible="false">
                                                            <asp:ListView ID="lstAttSecondsms" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Sr. No.</th>
                                                                                <th align="center">
                                                                                    <asp:CheckBox ID="cbHead" runat="server"
                                                                                        Text="Select" onClick="SelectAllSecond(this);" TabIndex="10" />
                                                                                </th>
                                                                                <th>Univ. Reg. No.</th>
                                                                                <th>Student Name </th>
                                                                                <th>List Of Courses (CCode - [Att. %])</th>
                                                                                <th>Father/Mother Mobile Number</th>
                                                                                <th>Father/Mother Email id</th>
                                                                                <th>StatusSMS</th>
                                                                                <th>StatusEMAIL</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td style="text-align: center"><%#Container.DataItemIndex+1 %></td>

                                                                        <td style="text-align: center">
                                                                            <asp:CheckBox ID="cbRow" runat="server" onClick="totSubjects(this);" TabIndex="11" />
                                                                            <%-- Enabled='<%#(Eval("STATUS").ToString()=="1"?"0":( 
                                                                String.IsNullOrEmpty(Eval("FATHERMOBILE").ToString()) ? "0" : Eval("FATHERMOBILE")))  == "0" ?  false : true%>' />--%>   <%--comment by jay takalkhede as discussed with umesh sir 05/011/2022--%>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblEnrollmentNo" Text='<%# Eval("ENROLLMENT_NO")%>'></asp:Label></td>

                                                                        <asp:HiddenField ID="hdnidno" Value='<%# Eval("IDNO")%>' runat="server" />
                                                                        <asp:HiddenField ID="HiddenField1" Value='<%# Eval("ENROLLMENT_NO")%>' runat="server" />
                                                                        <td><%# Eval("STUDENT_NAME")%>
                                                                            <asp:HiddenField ID="hdnStuName" Value='<%# Eval("STUDENT_NAME")%>' runat="server" />
                                                                        </td>
                                                                        <td><%# Eval("CCODES")==null?null:Eval("CCODES") %>
                                                                            <asp:HiddenField ID="hdnCode" Value='<%# Eval("CCODES")==null?null:Eval("CCODES") %>' runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblParMobile" Text='<%# Eval("FATHERMOBILE")%>'>  </asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblParEmail" Text='<%# Eval("FATHER_EMAIL")%>'>  </asp:Label>
                                                                        </td>
                                                                        <td><%# Eval("STATUS_Remark")%>
                                                                            <asp:HiddenField ID="hdnStatus" Value='<%# Eval("STATUS")%>' runat="server" />
                                                                        </td>
                                                                        <td><%# Eval("STATUS_Remark_EMAIL")%>
                                                                            <asp:HiddenField ID="hdstatusEmail" Value='<%# Eval("STATUS_EMAIL")%>' runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12" runat="server" id="divIAMarks">
                                                        <asp:Panel ID="pnlthird" runat="server" ScrollBars="Vertical" Height="550px" Visible="false">
                                                            <asp:ListView ID="lvmarks" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Sr.No.</th>
                                                                                <th align="center">
                                                                                    <asp:CheckBox ID="cbHead" runat="server" Text="Select" onClick="SelectAllThird(this);" TabIndex="11" />
                                                                                </th>
                                                                                <th>University No</th>

                                                                                <th>Student Name </th>
                                                                                <th>Father Mobile Number</th>
                                                                                <th>Registered Theory Subject(s) List</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td><%#Container.DataItemIndex+1 %></td>
                                                                        <td align="center">
                                                                            <asp:CheckBox ID="cbRow" runat="server" onClick="totSubjects(this);" Enabled='<%#(String.IsNullOrEmpty(Eval("FATHERMOBILE").ToString()) ? "0" : Eval("FATHERMOBILE"))  == "0" ?  false : true%>' TabIndex="11" />
                                                                        </td>

                                                                        <td><%# Eval("ENROLLNO")%></td>
                                                                        <asp:HiddenField ID="hdnidno" Value='<%# Eval("IDNO")%>' runat="server" />
                                                                        <asp:HiddenField ID="hdnenroll" Value='<%# Eval("ENROLLNO")%>' runat="server" />
                                                                        <asp:HiddenField ID="hdnsemesterno" Value='<%# Eval("SEMESTERNO")%>' runat="server" />
                                                                        <asp:HiddenField ID="hdnshortbname" Value='<%# Eval("BRANCHSHORTNAME")%>' runat="server" />
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblname" Text='<%# Eval("STUDNAME")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblParMobile" Text='<%# Eval("FATHERMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblIAMarks" Text='<%# Eval("DETAILED_COURSE")%>'></asp:Label></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>


                                                    <div class="col-12" runat="server" id="divToday">
                                                        <asp:Panel ID="pnltoday" runat="server" Visible="false">
                                                            <asp:ListView ID="lvTodayAtt" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Sr. No.</th>
                                                                                <th align="center">
                                                                                    <asp:CheckBox ID="cbHead" runat="server" Text="Select"
                                                                                        onClick="SelectAllFourth(this);" TabIndex="10" />
                                                                                </th>
                                                                                <th>Univ. Reg. No.</th>
                                                                                <th>Student Name </th>
                                                                                <th>Father/Mother Mobile Number</th>
                                                                                <th>Attendance Date
                                                                                </th>
                                                                                <th>Present</th>
                                                                                <th>Absent</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td style="text-align: center"><%#Container.DataItemIndex+1 %></td>
                                                                        <td style="text-align: center">
                                                                            <asp:CheckBox ID="cbRow" runat="server" onClick="totSubjects(this);" TabIndex="11" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblregno" Text='<%# Eval("REGNO")%>'>  </asp:Label></td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblnametoday" Text='<%# Eval("STUDNAME")%>'>  </asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblParMobiletoday" Text='<%# Eval("FATHERMOBILE")%>'>  </asp:Label>
                                                                            <asp:HiddenField ID="hdnIDNO" Value='<%# Eval("idno")%>' runat="server" />
                                                                            <asp:HiddenField ID="hdnDEPT" Value='<%# Eval("DEPTNAME")%>' runat="server" />
                                                                            <asp:HiddenField ID="hdnCourse" Value='<%# Eval("COURSE_NAME")%>' runat="server" />
                                                                        </td>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lbltodayatt" Text='<%# Eval("ATT_DATE")%>'>  </asp:Label>
                                                                        </th>
                                                                        <td><%# Eval("PRESENT")%>
                                                                        </td>
                                                                        <td><%# Eval("ABSENT")%>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <asp:Label ID="Label7" runat="server" SkinID="Msglbl"></asp:Label>
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
        </ContentTemplate>

    </asp:UpdatePanel>

    <script>

        function ReplaceValue(txt) {
            try {
                var id = "" + txt.id + "";
                var myarray = new Array();
                myarray = id.split("_");
                var index = myarray[3];
                var txttemp = document.getElementById("ctl00_ContentPlaceHolder1_lblTemplate").innerText;
                //alert(txttemp)
                var varCount = document.getElementById("ctl00_ContentPlaceHolder1_lvTemplate_" + index + "_lblVarCount").innerText;
                //alert(varCount)
                var vartxtinput = document.getElementById("ctl00_ContentPlaceHolder1_lvTemplate_" + index + "_txtVarTemplate").value;
                //alert(vartxtinput)
                var vartxtSearch = "{#var#}";
                document.getElementById("ctl00_ContentPlaceHolder1_lblTemplate").innerText = txttemp.replace(vartxtSearch, vartxtinput);
                this.getdisabled(txt);
            }
            catch (err) {
                //alert(err);
            }

        }

        function getVarindex() {
            var sel = rangy.getSelection();
            if (sel.rangeCount > 0) {
                var range = sel.getRangeAt(0);

                // Expand the range to contain the whole word
                range.expand("word");

                // Count all the preceding words in the document
                var precedingWordCount = 0;
                while (range.moveStart("word", -1)) {
                    precedingWordCount++;
                }

                // Display results
                alert("Selection starts in word number " + (precedingWordCount + 1));
            }
        }

        function getdisabled(txt) {
            var hftt = document.getElementById('<%= hftt.ClientID %>');
            var id = "" + txt.id + "";
            var myarray = new Array();
            myarray = id.split("_");
            var index = myarray[3];
            for (i = 0; i < hftt.value; i++) {
                var idsa = "ctrl" + i
                var ind = index.replace(index, idsa)
                //alert(ind)
                var vartxtinput = document.getElementById("ctl00_ContentPlaceHolder1_lvTemplate_" + index + "_txtVarTemplate").value;
                //alert(vartxtinput)
                if (vartxtinput.value != null) {
                    document.getElementById("ctl00_ContentPlaceHolder1_lvTemplate_" + index + "_txtVarTemplate").disabled = true;
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_lvTemplate_" + ind + "_txtVarTemplate").disabled = false;
                }

            }

        }

    </script>
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
    <script type="text/javascript">
        function selectfromdate(sender, args) {
        }
    </script>
    <%--        <script language="javascript" type="text/javascript">
            function totAll(headchk) {
                var frm = document.forms[0]
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
    </script>--%>
    <script>


        function SelectAllFirst(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvfirstsms_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        if (lst.disabled == true) {
                            lst.checked = false;
                        }
                        else {
                            lst.checked = true;
                            txtTot.value++;
                        }
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0
                    }
                }
            }
        }


        function SelectAllSecond(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {
                //ctl00_ContentPlaceHolder1_lvattendancesecondsms_ctrl0_cbRow
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lstAttSecondsms_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        if (lst.disabled == true) {
                            lst.checked = false;
                        }
                        else {
                            lst.checked = true;
                            txtTot.value++;
                        }
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0
                    }
                }
            }
        }


        function SelectAllThird(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvmarks_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        if (lst.disabled == true) {
                            lst.checked = false;
                        }
                        else {
                            lst.checked = true;
                            txtTot.value++;
                        }
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0
                    }
                }
            }
        }
        function SelectAllFourth(chk) {
            //alert('hi');
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            alert
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvTodayAtt_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        if (lst.disabled == true) {
                            lst.checked = false;
                        }
                        else {
                            lst.checked = true;
                            txtTot.value++;
                        }
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0
                    }
                }
            }
        }

        function SelectAllFifth(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentMeeting_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        if (lst.disabled == true) {
                            lst.checked = false;
                        }
                        else {
                            lst.checked = true;
                            txtTot.value++;
                        }
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0
                    }
                }
            }
        }


        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }




        function RadioClicked() {
            debugger;
            var radios = document.getElementById('ctl00_ContentPlaceHolder1_rdbEmplyeStud').getElementsByTagName('input');
            for (i = 0; i < radios.length; i++) {
                if (radios[i].checked) {
                    if (radios[i].value == 1) {
                        document.getElementById('ctl00_ContentPlaceHolder1_ddlDegree').value = "0";
                        document.getElementById('ctl00_ContentPlaceHolder1_ddlBranch').value = "0";
                        document.getElementById('ctl00_ContentPlaceHolder1_ddlsemester').value = "0";
                        document.getElementById('ctl00_ContentPlaceHolder1_ddlSchool').value = "0";

                    }
                    else {
                        document.getElementById('ctl00_ContentPlaceHolder1_ddlDepartment').value = "0";

                    }
                }
            }
        }
        function totAllSubjects(headchk) {
            debugger;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }

        }
        function validcan() {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlsemester').value = "0";
            document.getElementById('ctl00_ContentPlaceHolder1_pnlstud').visible = false;

        }
    </script>

    <script>
        function validateEmailForAttd() {

            var BrName = $("[id$=txtSubject]").attr("id");
            var BrName = document.getElementById(BrName);
            if (BrName.value.length == 0) {
                alert('Please Enter Subject.', 'Warning!');
                $(BrName).focus();
                return false;
            }

            var BrName = $("[id$=txtMessageAtdEmail]").attr("id");
            var BrName = document.getElementById(BrName);
            if (BrName.value.length == 0) {
                alert('Please Enter Email.', 'Warning!');
                $(BrName).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnEmail').click(function () {
                    validateEmailForAttd();
                });
            });
        });
    </script>

    <script>
        function validateEmailForAttd() {

            var BrName = $("[id$=txtSubjectsms]").attr("id");
            var BrName = document.getElementById(BrName);
            if (BrName.value.length == 0) {
                alert('Please Enter Subject.', 'Warning!');
                $(BrName).focus();
                return false;
            }

            var BrName = $("[id$=txtEmailsms]").attr("id");
            var BrName = document.getElementById(BrName);
            if (BrName.value.length == 0) {
                alert('Please Enter Email.', 'Warning!');
                $(BrName).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnEmail').click(function () {
                    validateEmailForAttd();
                });
            });
        });
    </script>


    <script>
        function ValidateMsg() {

            var ddlTemplateType = $("[id$=ddlTemplateType]").attr("id");
            var ddlTemplateType = document.getElementById(ddlTemplateType);
            if (ddlTemplateType.value == 0) {
                alert('Please Select Template Type.', 'Warning!');
                $(ddlTemplateType).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSndSms').click(function () {
                    ValidateMsg();
                });
            });
        });
    </script>
</asp:Content>

