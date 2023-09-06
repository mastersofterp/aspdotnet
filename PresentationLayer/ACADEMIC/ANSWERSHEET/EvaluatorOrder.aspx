<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="EvaluatorOrder.aspx.cs" Inherits="ACADEMIC_ANSWERSHEET_EvaluatorOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <%-- <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>--%>

    <%--   <script>
        $(function () {

            $('#table2').DataTable({

            });
        });

    </script>--%>
     <script>
         $(document).ready(function () {
             var table = $('#evalutorlist').DataTable({
                 responsive: true,
                 lengthChange: true,
                 scrollY: 320,
                 scrollX: true,
                 scrollCollapse: true,

                 dom: 'lBfrtip',
                 buttons: [
                     {
                         extend: 'colvis',
                         text: 'Column Visibility',
                         columns: function (idx, data, node) {
                             var arr = [0];
                             if (arr.indexOf(idx) !== -1) {
                                 return false;
                             } else {
                                 return $('#evalutorlist').DataTable().column(idx).visible();
                             }
                         }
                     },
                     {
                         extend: 'collection',
                         text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                         buttons: [
                                 {
                                     extend: 'copyHtml5',
                                     exportOptions: {
                                         columns: function (idx, data, node) {
                                             var arr = [0];
                                             if (arr.indexOf(idx) !== -1) {
                                                 return false;
                                             } else {
                                                 return $('#evalutorlist').DataTable().column(idx).visible();
                                             }
                                         }
                                     }
                                 },
                                 {
                                     extend: 'excelHtml5',
                                     exportOptions: {
                                         columns: function (idx, data, node) {
                                             var arr = [0];
                                             if (arr.indexOf(idx) !== -1) {
                                                 return false;
                                             } else {
                                                 return $('#evalutorlist').DataTable().column(idx).visible();
                                             }
                                         }
                                     }
                                 },
                                 {
                                     extend: 'pdfHtml5',
                                     exportOptions: {
                                         columns: function (idx, data, node) {
                                             var arr = [0];
                                             if (arr.indexOf(idx) !== -1) {
                                                 return false;
                                             } else {
                                                 return $('#evalutorlist').DataTable().column(idx).visible();
                                             }
                                         }
                                     }
                                 },
                         ]
                     }
                 ],
                 "bDestroy": true,
             });
         });
         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $(document).ready(function () {
                 var table = $('#evalutorlist').DataTable({
                     responsive: true,
                     lengthChange: true,
                     scrollY: 320,
                     scrollX: true,
                     scrollCollapse: true,

                     dom: 'lBfrtip',
                     buttons: [
                         {
                             extend: 'colvis',
                             text: 'Column Visibility',
                             columns: function (idx, data, node) {
                                 var arr = [0];
                                 if (arr.indexOf(idx) !== -1) {
                                     return false;
                                 } else {
                                     return $('#evalutorlist').DataTable().column(idx).visible();
                                 }
                             }
                         },
                         {
                             extend: 'collection',
                             text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                             buttons: [
                                     {
                                         extend: 'copyHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 var arr = [0];
                                                 if (arr.indexOf(idx) !== -1) {
                                                     return false;
                                                 } else {
                                                     return $('#evalutorlist').DataTable().column(idx).visible();
                                                 }
                                             }
                                         }
                                     },
                                     {
                                         extend: 'excelHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 var arr = [0];
                                                 if (arr.indexOf(idx) !== -1) {
                                                     return false;
                                                 } else {
                                                     return $('#evalutorlist').DataTable().column(idx).visible();
                                                 }
                                             }
                                         }
                                     },
                                     {
                                         extend: 'pdfHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 var arr = [0];
                                                 if (arr.indexOf(idx) !== -1) {
                                                     return false;
                                                 } else {
                                                     return $('#evalutorlist').DataTable().column(idx).visible();
                                                 }
                                             }
                                         }
                                     },
                             ]
                         }
                     ],
                     "bDestroy": true,
                 });
             });
         });

    </script>
    <style>
        .mr {
            margin-top: -11px;
        }
    </style>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    <%--  <p class="page_help_text">
        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
    </p>--%>
    <style>
        #ctl00_ContentPlaceHolder1_ceStartDate_popupDiv, ctl00_ContentPlaceHolder1_CalendarExtender2_popupDiv {
            z-index: 100;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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
    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EVALUATOR ORDER ALLOCATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">





                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            <%--  <label>College & Scheme</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="1" CssClass="form-control"
                                            ValidationGroup="save" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="valSessin" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please select Session" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please select Session" ValidationGroup="report"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Semester</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="valSemester" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please select Semester" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please select Semester" ValidationGroup="report"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <%--     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Exam Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamType" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="7" AutoPostBack="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <asp:ListItem Value="2">RE-Valuation</asp:ListItem>
                                            <asp:ListItem Value="3">Backlog</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="valExamType" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please select Exam Type" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                            <%-- <label>Course</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="4" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:ListBox ID="ddlCourse" runat="server" SelectionMode="Multiple" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" CssClass="form-control multi-select-demo" TabIndex="6"></asp:ListBox>--%>

                                        <asp:RequiredFieldValidator ID="valCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please select Course" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please select Course" ValidationGroup="report"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                           <asp:Label ID="lblDYStaffType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlEvalutor" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="5" AutoPostBack="true" OnSelectedIndexChanged="ddlEvalutor_SelectedIndexChanged">

                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%-- <asp:ListItem Value="1">Internal</asp:ListItem>
                                <asp:ListItem Value="2">External</asp:ListItem>
                                    <asp:ListItem Value="3">Paper Setter</asp:ListItem>
                                <asp:ListItem Value="4">Moderator</asp:ListItem>
                                    <asp:ListItem Value="5">Valuer</asp:ListItem>  --%>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="valEvaluatorType" runat="server" ControlToValidate="ddlEvalutor"
                                            Display="None" ErrorMessage="Please select Evaluator Type" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="valEvaluatorTypeReport" runat="server" ControlToValidate="ddlEvalutor"
                                            Display="None" ErrorMessage="Please select Evaluator Type" ValidationGroup="report"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="dept" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Department</label>--%>
                                            <asp:Label ID="lblDYddlDeptName_Tab" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <%--<asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>

                                        <asp:ListBox ID="ddlDepartment" runat="server" TabIndex="6" SelectionMode="Multiple" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control multi-select-demo" AutoPostBack="true"></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please select Department " ValidationGroup="submit"
                                            InitialValue="" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please select Department " ValidationGroup="report"
                                            InitialValue="" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Staff Name</label>--%>
                                            <asp:Label ID="lblDYStaffName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <%--  <asp:DropDownList ID="ddlFaculty" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="10" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>


                                        <%--     <asp:ListBox ID="ddlFaculty" runat="server" SelectionMode="Multiple" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" CssClass="form-control multi-select-demo" TabIndex="6"></asp:ListBox>--%>
                                        <asp:ListBox ID="ddlFaculty" runat="server" AutoPostBack="true" SelectionMode="Multiple"  CssClass="form-control multi-select-demo" TabIndex="7" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="valFaculty" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please select Evaluator Name" ValidationGroup="report"
                                            InitialValue="" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Reporting Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgReportDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtReportDate" runat="server" TabIndex="8" ValidationGroup="submit"
                                                CssClas="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtReportDate" PopupButtonID="imgReportDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meReportDate" runat="server" TargetControlID="txtReportDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" EmptyValueMessage="Please Enter Report Date"
                                                ControlExtender="meReportDate" ControlToValidate="txtReportDate" IsValidEmpty="false"
                                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Report Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                            <asp:HiddenField runat="server" ID="HiddenField1" />

                                            <asp:RequiredFieldValidator ID="rfvReportDate" runat="server" ControlToValidate="txtReportDate"
                                                Display="None" ErrorMessage="Please Select Report Date " ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">

                                        <div class="label-dynamic">
                                            <%--<sup>*</sup>--%>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgReportDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="9" ValidationGroup="submit"
                                                CssClas="form-control" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgReportDate1" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Report Date"
                                                ControlExtender="meReportDate" ControlToValidate="txtToDate" IsValidEmpty="false"
                                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Report Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                            <asp:HiddenField runat="server" ID="HiddenField2" />
                                        </div>
                                    </div>
                                   <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYApprovalStatus" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="switch mr " onclick="clickRdActive()">
                                            <input type="checkbox" id="rblStatus" runat="server" name="switch" checked />
                                            <label data-on="Approve" data-off="Pending" for="rblStatus"></label>
                                            <asp:HiddenField ID="hdfStatus" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>--%>
                                </div>
                                <%--          <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Status</label>
                                        </div>
                                        <asp:RadioButtonList ID="rblApproved" runat="server" RepeatDirection="Horizontal"
                                            Height="16px" Width="193px">
                                            <asp:ListItem Selected="True" Text="Approved" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Not-Approved" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>--%>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">

                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="10"
                                ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />

                            <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="11" 
                                CssClass="btn btn-info" OnClick="btnReport_Click" ValidationGroup="report"/>
                            <asp:Button ID="btnDeclarationReport" runat="server" Text="Declaration Report" TabIndex="12"
                                CssClass="btn btn-info" OnClick="btnDeclarationReport_Click"  ValidationGroup="report"/>
                            <asp:Button ID="btnEvaluatorReport" runat="server" Text="Evaluator Report" TabIndex="13"
                                CssClass="btn btn-info" OnClick="btnEvaluatorReport_Click" Enabled="true" />
                            <%--<asp:Button ID="btnApproved" Visible="false" runat="server" Text="Approve/Not Approve Submit" TabIndex="12"
                                    ValidationGroup="submit" CssClass="btn btn-success" OnClick="btnApproved_Click" />--%>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="14" CssClass="btn btn-warning"
                                OnClick="btnCancel_Click" />

                            <asp:ValidationSummary ID="valSummary" DisplayMode="List" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="submit" />
                            
                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="report" />
                        </div>
                        <div class="col-12 py-3">
                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table2">

                                <asp:Panel ID="pnlSession" runat="server">
                                    <%--<asp:Repeater ID="NotinUsed" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>SESSION</h5>
                                                </div>
                                                <thead class="bg-light-blue">
                                                    <tr id="itemPlaceholder" runat="server" style="background-color: #F3F3F3;">
                                                        <th>Action.
                                                        </th>
                                                        <th>Session.
                                                        </th>
                                                        <th>Semester.
                                                        </th>
                                                        <th>Staff Name.
                                                        </th>
                                                        <th>Course Name With Code
                                                        </th>
                                                        <th>Faculty Type
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>

                                                        <asp:ImageButton ID="ibtnEvalDelete" runat="server"
                                                            ImageUrl="~/images/delete.gif" AlternateText="DELETE RECORD" ToolTip='<%# Eval("EVAL_APPID")%>'
                                                            OnClick="ibtnEvalDelete_Click" CommandArgument='<%# Eval("EVAL_APPID")%>' TabIndex="11" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SESSION_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STAFF_NAME")%>
                                                        <asp:HiddenField runat="server" ID="hdfcoursename" Value='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("COURSENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FAC_TYPE")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="bg-light-red">
                                                    <td>
                                                        <asp:ImageButton ID="ibtnEvalDelete" runat="server"
                                                            ImageUrl="~/images/delete.gif" AlternateText="DELETE RECORD" ToolTip='<%# Eval("EVAL_APPID")%>'
                                                            OnClick="ibtnEvalDelete_Click" CommandArgument='<%# Eval("EVAL_APPID")%>' TabIndex="11" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SESSION_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STAFF_NAME")%>
                                                        <asp:HiddenField runat="server" ID="hdfcoursename" Value='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("COURSENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FAC_TYPE")%>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </table>
                                            </FooterTemplate>
                                        </asp:Repeater>--%>

                                    <asp:ListView ID="lvEvaluator" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>EVALUATOR ORDER ALLOCATION LIST</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="evalutorlist">

                                                <%--<table class="table table-striped table-bordered nowrap">--%>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action.
                                                        </th>
                                                        <th>Session.
                                                        </th>
                                                        <th>Semester.
                                                        </th>
                                                        <th>Evaluator Name
                                                        </th>
                                                        <th>Course With Code
                                                        </th>
                                                        <th>Evaluator Type
                                                        </th>
                                                        <th>Approval Status
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
                                                    
                                                    <asp:ImageButton ID="ibtnEvalDelete" runat="server"
                                                        ImageUrl="~/images/delete.gif" AlternateText="CANCEL RECORD" ToolTip='<%# Eval("EVAL_APPID")%>'
                                                        OnClick="ibtnEvalDelete_Click" OnClientClick="return showConfirm();" CommandArgument='<%# Eval("EVAL_APPID")%>' TabIndex="11" />
                                                        
                                                </td>
                                                <td>
                                                    <%# Eval("SESSION_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STAFF_NAME")%>
                                                    <%--<asp:HiddenField runat="server" ID="hdfcoursename" Value='<%# Eval("COURSENO")%>' />--%>
                                                </td>
                                                <td>
                                                    <%# Eval("COURSENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FAC_TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVE_STATUS")%>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </table>
                   
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnDeclarationReport" />
            <%--<asp:PostBackTrigger ControlID="ddlCourse" />--%>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            

        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>



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
    <script>
        function showConfirm() {
            var ret = confirm('Do you really want to cancel allocation?');
            if (ret == true) {
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>
    <%--<script>
        function SetStatActive(val) {
            var examreg = document.getElementById("rblStatus");
            if (examreg.checked) {
                $('#hdfStatus').val(true);
            }

        }

        function clickRdActive() {
            if ($('#ctl00_ContentPlaceHolder1_rblStatus').is(':checked')) {
                $('#ctl00_ContentPlaceHolder1_rblStatus').prop('checked', false);
            }
            else {
                $('#ctl00_ContentPlaceHolder1_rblStatus').prop('checked', true);
            }
        }
    </script>--%>
</asp:Content>
