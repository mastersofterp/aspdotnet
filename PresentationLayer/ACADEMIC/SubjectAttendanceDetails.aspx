<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubjectAttendanceDetails.aspx.cs" Inherits="ACADEMIC_SubjectAttendanceDetails" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%--===== Data Table Script added by gaurav =====--%>
    <script>
             $(document).ready(function () {
                 var table = $('#example2').DataTable({
                     responsive: true,
                     lengthChange: true,
                     scrollY: 320,
                     scrollX: true,
                     scrollCollapse: true,
                     paging: false, // Added by Gaurav for Hide pagination

                     dom: 'lBfrtip',
                     buttons: [
                         {
                             extend: 'colvis',
                             text: 'Column Visibility',
                             columns: function (idx, data, node) {
                                 //var arr = [0];
                                 //if (arr.indexOf(idx) !== -1) {
                                 //    return false;
                                 //} else {
                                 return $('#example2').DataTable().column(idx).visible();
                                 //}
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
                                                 //var arr = [0];
                                                 //if (arr.indexOf(idx) !== -1) {
                                                 //    return false;
                                                 //} else {
                                                 return $('#example2').DataTable().column(idx).visible();
                                                 //}
                                             }
                                         }
                                     },
                                     {
                                         extend: 'excelHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 //var arr = [0];
                                                 //if (arr.indexOf(idx) !== -1) {
                                                 //    return false;
                                                 //} else {
                                                 return $('#example2').DataTable().column(idx).visible();
                                                 //}
                                             }
                                         }
                                     },
                                     {
                                         extend: 'pdfHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 //var arr = [0];
                                                 //if (arr.indexOf(idx) !== -1) {
                                                 //    return false;
                                                 //} else {
                                                 return $('#example2').DataTable().column(idx).visible();
                                                 //}
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
                  var table = $('#example2').DataTable({
                      responsive: true,
                      lengthChange: true,
                      scrollY: 320,
                      scrollX: true,
                      scrollCollapse: true,
                      paging: false, // Added by Gaurav for Hide pagination

                      dom: 'lBfrtip',
                      buttons: [
                          {
                              extend: 'colvis',
                              text: 'Column Visibility',
                              columns: function (idx, data, node) {
                                  //var arr = [0];
                                  //if (arr.indexOf(idx) !== -1) {
                                  //    return false;
                                  //} else {
                                  return $('#example2').DataTable().column(idx).visible();
                                  //}
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
                                                  //var arr = [0];
                                                  //if (arr.indexOf(idx) !== -1) {
                                                  //    return false;
                                                  //} else {
                                                  return $('#example2').DataTable().column(idx).visible();
                                                  //}
                                              }
                                          }
                                      },
                                      {
                                          extend: 'excelHtml5',
                                          exportOptions: {
                                              columns: function (idx, data, node) {
                                                  //var arr = [0];
                                                  //if (arr.indexOf(idx) !== -1) {
                                                  //    return false;
                                                  //} else {
                                                  return $('#example2').DataTable().column(idx).visible();
                                                  //}
                                              }
                                          }
                                      },
                                      {
                                          extend: 'pdfHtml5',
                                          exportOptions: {
                                              columns: function (idx, data, node) {
                                                  //var arr = [0];
                                                  //if (arr.indexOf(idx) !== -1) {
                                                  //    return false;
                                                  //} else {
                                                  return $('#example2').DataTable().column(idx).visible();
                                                  //}
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Attendence Details</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" AutoPostBack="true"
                                            ValidationGroup="Submit" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" ToolTip="Please Select School/Institute" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select School/Institute" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Department</label>--%>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Submit" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfddlScheme" runat="server" ControlToValidate="ddlDepartment" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="true" ValidationGroup="Submit" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server"
                                            ControlToValidate="ddlFaculty" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="cal1" style="cursor: pointer"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                                TabIndex="4" ToolTip="Please Enter From Date" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="cal1" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                ValidationGroup="submit" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Start Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Start Date" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="submit" SetFocusOnError="True" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="cal2" style="cursor: pointer"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                                TabIndex="5" ToolTip="Please Enter Session Start Date" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="cal2" />
                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="True"
                                                ValidationGroup="submit" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date"
                                                InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="submit" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Show" ValidationGroup="submit"
                                    TabIndex="6" CssClass="btn btn-info" OnClick="btnReport_Click" />
                                <asp:Button ID="btnExcel" runat="server" Text="Faculty wise Attendance Report" ValidationGroup="submit"
                                    TabIndex="6" CssClass="btn btn-info" OnClick="btnExcel_Click" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click1" Text="Cancel"
                                    TabIndex="7" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="vsStud" runat="server" DisplayMode="List" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="submit" />
                            </div>
                            <div class=" col-12">
                                <div class="sub-heading" id="divlvStudentHeading" runat="server" visible="false">
                                    <h5>Student Attendance Details</h5>
                                </div>
                                <asp:ListView ID="lvStudAttendance" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="example2">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label></th>
                                                    <th>Student Name</th>
                                                    <th>
                                                        <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label></th>
                                                    <th>Course Code</th>
                                                    <th>Course Name</th>
                                                    <th>Total Classes (Available Slots)</th>
                                                    <th>Present Classes (Marked Slots)</th>
                                                    <th>Absent Classes (Unmarked Slots)</th>
                                                    <th>OD Classes</th>
                                                    <th>Without OD %</th>
                                                    <th>With OD %</th>
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
                                                <%# Eval("REGNO") %>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("DEGREE") %>
                                            </td>
                                            <td>
                                                <%# Eval("BRANCH") %>
                                            </td>
                                             <td>
                                                <%# Eval("CCODE") %>
                                            </td>
                                            <td>
                                                <%# Eval("COURSENAME") %>
                                            </td>

                                            <td>
                                                <%# Eval("TOTAL_LECTURE") %>
                                            </td>
                                            <td>
                                                <%# Eval("Present") %>
                                            </td>
                                            <td>
                                                <%# Eval("Absent") %>
                                            </td>
                                            <td>
                                                <%# Eval("OD_Classes") %>
                                            </td>
                                            <td>
                                                <%# Eval("Without_OD") %>
                                            </td>
                                            <td>
                                                <%# Eval("With_OD") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyItemTemplate>
                                        <p>No record found! </p>
                                    </EmptyItemTemplate>
                                </asp:ListView>

                            </div>

                        </div>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
