<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdvancePaymentEmail.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_AdvancePaymentEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

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
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row pl-3 mb-3">
                            <div style="color: Red; font-weight: bold">
                                Note : Only the student for selected Degree configured from Fee Payment Configuration with Active status will be displayed.
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Admission Batch </label>
                                </div>
                                <select id="ddlAdmissionBatch" name="ddlAdmissionBatch" class="form-control" data-select2-enable="true" tabindex="0" required>
                                    <option value="0">Please Select</option>
                                </select>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Program Type </label>
                                </div>
                                <select id="ddlProgramType" name="ddlProgramType" class="form-control" data-select2-enable="true" tabindex="0" required>
                                    <option value="0">Please Select</option>
                                    <option value="1">UG</option>
                                    <option value="2">PG</option>
                                </select>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Degree </label>
                                </div>
                                <select id="ddlDegree" name="ddlDegree" class="form-control" data-select2-enable="true" tabindex="0" required>
                                    <option value="0">Please Select</option>
                                </select>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Program/ Branch </label>
                                </div>
                                <select id="lstProgram" class="form-control multi-select-demo" tabindex="0" multiple="multiple" required>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <input type="button" value="Show Student List" id="btnShow" class="btn btn-primary" tabindex="0" />
                        <input type="button" value="Send Email" id="btnSendEmail" class="btn btn-primary" tabindex="0" disabled="disabled" />
                        <input type="button" value="Cancel" id="btnCancel" class="btn btn-warning" tabindex="0" />
                    </div>
                    <div class="col-12 d-none" id="divStudentDetails">
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            BindAdmissionBatch();
        });

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

        function BindAdmissionBatch() {
            $('#ddlAdmissionBatch').empty();
            $("#ddlAdmissionBatch").append($("<option></option>").val('0').html('Please Select'));
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("AdvancePaymentEmail.aspx/GetAdmissionBatch") %>',
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);
                    if (data != '') {
                        $.each(data, function (key, value) {
                            $("#ddlAdmissionBatch").append($("<option></option>").val(value.BATCHNO).html(value.BATCHNAME));
                        });
                    }
                },
                failure: function (response) {

                },
                error: function (response) {
                    alert('Error Occurred !!!');
                }
            });
        }
        $('select[id*="ddlAdmissionBatch"]').on('change', function () {
            $('#ddlProgramType').val('0').change();
            $('#ddlDegree').empty();
            $("#ddlDegree").append($("<option></option>").val('0').html('Please Select'));
            $("#divStudentDetails").addClass('d-none');
            $("#btnSendEmail").prop("disabled", true);
        });

        $('select[id*="ddlProgramType"]').on('change', function () {
            var ugpgot = $(this).val();
            $('#ddlDegree').empty();
            $("#ddlDegree").append($("<option></option>").val('0').html('Please Select'));
            $("#divStudentDetails").addClass('d-none');
            $("#btnSendEmail").prop("disabled", true);

            if (ugpgot > 0) {
                BindDegree(ugpgot);
            }
        });

        function BindDegree(ugpgot) {
            var ugpgot = $('#ddlProgramType').val();
            $('#ddlDegree').empty();
            $("#ddlDegree").append($("<option></option>").val('0').html('Please Select'));
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("AdvancePaymentEmail.aspx/GetDegree") %>',
                data: JSON.stringify({ ugpgot: ugpgot }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);
                    if (data != '') {
                        $.each(data, function (key, value) {
                            $("#ddlDegree").append($("<option></option>").val(value.DEGREENO).html(value.DEGREENAME));
                        });
                    }
                },
                failure: function (response) {

                },
                error: function (response) {
                    alert('Error Occurred !!!');
                }
            });
        }

        $('select[id*="ddlDegree"]').on('change', function () {
            var degreeno = $(this).val();
            $("#divStudentDetails").addClass('d-none');
            $("#btnSendEmail").prop("disabled", true);
        });

        $("[id*=btnShow]").click(function () {
            if (ValidateControls() == true) {
                var batchno = $('#ddlAdmissionBatch').val();
                var ugpgot = $('#ddlProgramType').val();
                var degreeno = $('#ddlDegree').val();
                var branchnos = 0;
                BindStudentDetails(batchno, ugpgot, degreeno, branchnos);
            }
        });

        function BindStudentDetails(batchno, ugpgot, degreeno, branchnos) {
            $("#divStudentDetails").empty();
            $("#divStudentDetails").addClass('d-none');
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("AdvancePaymentEmail.aspx/GetStudentDetails") %>',
                data: JSON.stringify({ BatchNo: batchno, UgPgOt: ugpgot, DegreeNo: degreeno, BranchNos: branchnos }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                success: function (response) {
                    var data = JSON.parse(response.d);
                    if (data != '') {
                        var _str = '';
                        var _tbody = '';
                        var rownum = 0;
                        str = '<table class="table table-striped table-bordered nowrap" id="tblStudentData">'
                        str = str + '<thead><tr><th id = "classall"><input type="checkbox" id="SelectAll" onclick="ToggleAllCheckbox();" /></th><th>Admission Batch</th><th>Application Id</th><th>Student Name</th><th>Mobile No</th><th>Email Id</th><th>Degree</th></tr></thead><tbody>';
                        $.each(data, function (a, b) {

                            rownum = rownum + 1;
                            str = str + '<tr>'
                            str = str + '<td><input type="checkbox"/></td><input type = "hidden" id="hdntblUserNo" value = "' + b.USERNO + '" />'
                            str = str + '<td>' + b.BATCHNAME + '</td>'
                            str = str + '<td>' + b.APPLICATIONID + '</td>'
                            str = str + '<td>' + b.STUDENTNAME + '</td>'
                            str = str + '<td>' + b.MOBILENO + '</td>'
                            str = str + '<td>' + b.EMAILID + '</td>'
                            str = str + '<td>' + b.DEGREENAME + '</td>'
                            str = str + '</tr>'

                        });
                        str = str + '</tbody></table>'
                        $("#divStudentDetails").append(str);

                        commonDatatable(tblStudentData);


                        $("#divStudentDetails").removeClass('d-none');
                        $("#btnSendEmail").prop("disabled", false);
                        $("[id*=preloader]").hide();
                    }
                    else {
                        alert('Record not found');
                        $("#divStudentDetails").addClass('d-none');
                        $("#btnSendEmail").prop("disabled", true);
                        $("[id*=preloader]").hide();
                        return false;
                    }
                },
                failure: function (response) {
                    $("[id*=preloader]").hide();
                },
                error: function (response) {
                    alert('Error Occurred !!!');
                    $("[id*=preloader]").hide();
                }
            });
        }

        function ValidateControls() {
            if ($('#ddlAdmissionBatch').val() == 0) {
                alert('Please Select Admission Batch');
                return false;
            }
            else if ($('#ddlProgramType').val() == 0) {
                alert('Please Select Program Type');
                return false;
            }
            else if ($('#ddlDegree').val() == 0) {
                alert('Please Select Degree');
                return false;
            }
            return true;
        }

        function ToggleAllCheckbox() {
            var selectAllCheckbox = $('#SelectAll');
            var checkboxes = $('#tblStudentData tbody input[type="checkbox"]');
            checkboxes.each(function () {
                $(this).prop('checked', selectAllCheckbox.prop('checked'));
            });
        }

        $("[id*=btnSendEmail]").click(function () {
            var Count = 0;
            var usernoxml = "<NewDataSet>";
            $("#tblStudentData tr").each(function () {
                var checkbox = $(this).find("[type='checkbox']");
                if (checkbox.prop('checked')) {
                    Count++;
                    var UserNo = $(this).find('#hdntblUserNo').val();
                    if (UserNo !== undefined) {
                        usernoxml += "<DatatableUserNo><UserNo>" + UserNo + "</UserNo></DatatableUserNo>";
                    }
                }
            });
            usernoxml += "</NewDataSet>";
            if (Count > 0) {
                SendEmailToStudent(usernoxml);
            }
            else {
                alert('Please select atleast one record');
                return false;
            }
        });

        function SendEmailToStudent(usernoxml) {
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("AdvancePaymentEmail.aspx/GetSendEmailStudentDetails") %>',
                data: JSON.stringify({ UsernoXml: usernoxml }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                success: function (response) {
                    var data = JSON.parse(response.d);
                    if (data != "") {
                        if (data.trim() == '1') {
                            alert('Email Sent Successfully');
                            $("[id*=preloader]").hide();
                        }
                        else {
                            alert('Failed to send email !!!');
                            $("[id*=preloader]").hide();
                        }
                    }
                    else {
                        $("[id*=preloader]").hide();
                    }
                },
                failure: function (response) {
                    $("[id*=preloader]").hide();
                },
                error: function (response) {
                    alert('Error Occurred !!!');
                    $("[id*=preloader]").hide();
                }
            });
        }

        $("[id*=btnCancel]").click(function () {
            $('#ddlAdmissionBatch').val('0').trigger('change');
            $('#ddlProgramType').val('0').trigger('change');
            $('#ddlDegree').empty();
            $("#ddlDegree").append($("<option></option>").val('0').html('Please Select'));
            $("#divStudentDetails").addClass();
            $("#btnSendEmail").prop("disabled", true);
        });


        function commonDatatable(tableID) {
            setTimeout(function () {
                var table = $(tableID).DataTable({
                    responsive: true,
                    lengthChange: true,
                    paging: false,
                    fixedColumns: {
                        left: 0,
                        right: 1
                    },
                    colReorder: true,
                    fixedHeader: true,
                    scrollX: true,
                    dom: 'lBfrtip',
                    buttons: [
                    ],
                    "bDestroy": true,
                });
                $("[id*=preloader]").hide();

                $(tableID).wrap("<div class='table-responsive'></div>");
            }, 10);
        }
    </script>
</asp:Content>

