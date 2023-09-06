<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPExam_ReSchedule.aspx.cs" Inherits="Exam_ReSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <asp:UpdatePanel ID="updReSchedule" runat="server">
        <ContentTemplate>
            <div class="row">
                    <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Reschedule</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmissionBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            <asp:ListItem Value="1">UG</asp:ListItem>
                                            <asp:ListItem Value="2">PG</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program/Branch </label>
                                        </div>
                                        <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" OnSelectedIndexChanged="lstProgram_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Slot </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamSlot" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" OnSelectedIndexChanged="ddlExamSlot_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 mt-3 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Show Student List" TabIndex="6" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                         <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    </div>

                                </div>
                            </div>

                             <asp:Panel runat="server" ID="pnlReschedule" Width="100%" Visible="False">
                            <div class="col-12 mt-3">
                                <div class="row">                              
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>New Exam Schedule </label>
                                        </div>
                                        <asp:DropDownList ID="ddlNewExamSchedule" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="11">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">28-10-20222 09:00 AM</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-5 col-md-12 col-12 btn-footer mt-lg-3">
                                        <asp:Button ID="btnRescheduleExam" runat="server" Text="Re-Schedule Exam" TabIndex="7" CssClass="btn btn-primary" OnClick="btnRescheduleExam_Click" />
                                        <asp:Button ID="btnAttendance" runat="server" Text="Attendance Sheet" TabIndex="8" CssClass="btn btn-primary" Visible="false" />                                       
                                    </div>
                                </div>
                            </div>
                                 </asp:Panel>

                            <asp:Panel runat="server" ID="pnlCount" Width="100%" Visible="False">
                                <div class="form-group form-inline col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label style="color: red">Total Students :- </label>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtTotalCount" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnllistView" Width="100%" Visible="False">
                                <div class="col-md-12">
                                    <asp:ListView ID="lvSchedule" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <table id="tbllist" class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" /></th>
                                                            <th style="width: 10%">Admission Batch</th>
                                                            <th style="width: 10%">Application ID</th>
                                                            <th style="width: 15%">Student Name</th>
                                                            <th style="width: 10%">Degree</th>
                                                            <th style="width: 20%">Program/Branch</th>
                                                            <th>Current Schedule</th>
                                                            <th>Old Schedule</th>
                                                            <%-- <th style="width: 10%">Status</th>--%>
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
                                                    <asp:CheckBox ID="chkRecon" runat="server" Checked='<%#(Convert.ToString(Eval("IsAttend"))== "True" ? true : false) %>' />
                                                </td>
                                                <td><%# Eval("ADMBATCH") %></td>
                                                <td><%# Eval("APPLICATION_ID") %></td>
                                                <td><%# Eval("FIRSTNAME") %></td>
                                                <td><%# Eval("DEGREENAME") %></td>
                                                <td><%# Eval("BRANCHNAME") %></td>
                                                <td><%# Eval("ExamSchedule") %></td>
                                                <td><%# Eval("PreSchedule") %></td>                              
                                                <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />
                                                <asp:HiddenField ID="hdnACNO" Visible="false" runat="server" Value='<%# Eval("ACNO") %>' />
                                                <asp:HiddenField ID="hdnAttendaceNo" Visible="false" runat="server" Value='<%# Eval("ATTENDANCE_NO") %>' />
                                                <%--<td>--%>
                                                    <%--   <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?"Active":"In Active" %>' ForeColor='<%#Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {
            // alert('Hii');
            $('#hfdShowStatus').val($('#rdActive').prop('checked'));
            //alert($('#hfdShowStatus').val());
            //if (Page_ClientValidate()) {
            //    alert('Inside');
            //    //$('#hfdShowStatus').val($('#rdActive').prop('checked'));
            //    alert($('#hfdShowStatus').val());
            //}
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
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
        function totAllSubjects(headchk) {
            debugger;
            var sum = 0;
            var frm = document.forms[0]
            try {
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true) {
                            // SumTotal();
                            // var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                            //// alert(j);
                            // sum += parseFloat(j);
                            if (e.disabled == false) {
                                e.checked = true;
                            }
                        }
                        else {
                            if (e.disabled == false) {
                                e.checked = false;
                                headchk.checked = false;
                            }
                        }

                        // x = sum.toString();
                    }

                }
            }
            catch (err) {
                alert("Error : " + err.message);
            }
        }
    </script>
</asp:Content>

