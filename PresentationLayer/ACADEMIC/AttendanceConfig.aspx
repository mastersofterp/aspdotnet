<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" CodeFile="AttendanceConfig.aspx.cs" Inherits="ACADEMIC_AttendanceConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <asp:HiddenField ID="hfdSms" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdEmail" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdCourse" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdTeaching" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdHide" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnGlobalClgIds" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnGlobalDeg" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnEditMode" runat="server" ClientIDMode="Static" />





    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <script>
        function BindTable() {
            var table = $('#BindDynamicTable').DataTable({
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
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#BindDynamicTable').DataTable().column(idx).visible();
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
                                           return $('#BindDynamicTable').DataTable().column(idx).visible();
                                       }
                                   },
                                   format: {
                                       body: function (data, column, row, node) {
                                           var nodereturn;
                                           if ($(node).find("input:text").length > 0) {
                                               nodereturn = "";
                                               nodereturn += $(node).find("input:text").eq(0).val();
                                           }
                                           else if ($(node).find("input:checkbox").length > 0) {
                                               nodereturn = "";
                                               $(node).find("input:checkbox").each(function () {
                                                   if ($(this).is(':checked')) {
                                                       nodereturn += "On";
                                                   } else {
                                                       nodereturn += "Off";
                                                   }
                                               });
                                           }
                                           else if ($(node).find("a").length > 0) {
                                               nodereturn = "";
                                               $(node).find("a").each(function () {
                                                   nodereturn += $(this).text();
                                               });
                                           }
                                           else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                               nodereturn = "";
                                               $(node).find("span").each(function () {
                                                   nodereturn += $(this).text();
                                               });
                                           }
                                           else if ($(node).find("select").length > 0) {
                                               nodereturn = "";
                                               $(node).find("select").each(function () {
                                                   var thisOption = $(this).find("option:selected").text();
                                                   if (thisOption !== "Please Select") {
                                                       nodereturn += thisOption;
                                                   }
                                               });
                                           }
                                           else if ($(node).find("img").length > 0) {
                                               nodereturn = "";
                                           }
                                           else if ($(node).find("input:hidden").length > 0) {
                                               nodereturn = "";
                                           }
                                           else {
                                               nodereturn = data;
                                           }
                                           return nodereturn;
                                       },
                                   },
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
                                           return $('#BindDynamicTable').DataTable().column(idx).visible();
                                       }
                                   },
                                   format: {
                                       body: function (data, column, row, node) {
                                           var nodereturn;
                                           if ($(node).find("input:text").length > 0) {
                                               nodereturn = "";
                                               nodereturn += $(node).find("input:text").eq(0).val();
                                           }
                                           else if ($(node).find("input:checkbox").length > 0) {
                                               nodereturn = "";
                                               $(node).find("input:checkbox").each(function () {
                                                   if ($(this).is(':checked')) {
                                                       nodereturn += "On";
                                                   } else {
                                                       nodereturn += "Off";
                                                   }
                                               });
                                           }
                                           else if ($(node).find("a").length > 0) {
                                               nodereturn = "";
                                               $(node).find("a").each(function () {
                                                   nodereturn += $(this).text();
                                               });
                                           }
                                           else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                               nodereturn = "";
                                               $(node).find("span").each(function () {
                                                   nodereturn += $(this).text();
                                               });
                                           }
                                           else if ($(node).find("select").length > 0) {
                                               nodereturn = "";
                                               $(node).find("select").each(function () {
                                                   var thisOption = $(this).find("option:selected").text();
                                                   if (thisOption !== "Please Select") {
                                                       nodereturn += thisOption;
                                                   }
                                               });
                                           }
                                           else if ($(node).find("img").length > 0) {
                                               nodereturn = "";
                                           }
                                           else if ($(node).find("input:hidden").length > 0) {
                                               nodereturn = "";
                                           }
                                           else {
                                               nodereturn = data;
                                           }
                                           return nodereturn;
                                       },
                                   },
                               }
                           },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        }
    </script>



    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server"
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
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">

                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-12">

                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1"
                                            AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="lstbxSchool" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="1"
                                            onchange="getSelectedIds(this);"></asp:ListBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true">Degree</asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlDegree" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="1"></asp:ListBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Scheme Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchemeType" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlSemester" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="1"></asp:ListBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Attendance Start Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="txtStartDate1" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" TabIndex="1" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtStartDate" PopupButtonID="txtStartDate1" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Attendance Start Date" IsValidEmpty="false"
                                                InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" ErrorMessage="Start Date is Invalid (Enter dd/mm/yyyy Format)"
                                                TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="submit" SetFocusOnError="True" />
                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" SetFocusOnError="True"
                                                ValidationGroup="submit" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Attendance End Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="txtEndDate1" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" TabIndex="1" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtEndDate" PopupButtonID="txtEndDate1" />

                                            <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Attendance End Date"
                                                InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" IsValidEmpty="false"
                                                TooltipMessage="Please Enter Attendance End Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />

                                            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                ControlToValidate="txtEndDate" Display="None"
                                                ValidationGroup="submit" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Attendance Lock By Day</label>
                                        </div>
                                        <asp:TextBox ID="txtAttLockDay" runat="server" CssClass="form-control" TabIndex="1" MaxLength="3" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                            TargetControlID="txtAttLockDay" />
                                    </div>

                                </div>
                            </div>
                            <%--(d-none) added by vipul T on date 04-03-2024 as per TNO:-55726 --%>
                            <div class="form-group col-lg-2 col-md-6 col-6 d-none">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>SMS Facility</label>
                                </div>
                                <div class="switch form-inline">
                                    <input type="checkbox" id="rdSMSYes" tabindex="1" name="switch" checked />
                                    <label data-on="Yes" data-off="No" for="rdSMSYes"></label>
                                </div>
                            </div>

                            <div class="form-group col-lg-2 col-md-6 col-6 d-none">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Email Facility</label>
                                </div>
                                <div class="switch form-inline">
                                    <input type="checkbox" id="rdEmailYes" tabindex="1" name="switch" checked />
                                    <label data-on="Yes" data-off="No" for="rdEmailYes"></label>
                                </div>
                            </div>
                            <%-- end --%>
                            <div class="form-group col-lg-2 col-md-6 col-6">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Teaching Plan</label>
                                </div>
                                <div class="switch form-inline">
                                    <input type="checkbox" id="rdTeachYes" tabindex="1" name="switch" checked />
                                    <label data-on="Yes" data-off="No" for="rdTeachYes"></label>
                                </div>
                            </div>

                            <div class="form-group col-lg-2 col-md-6 col-6">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Check For Active</label>
                                </div>
                                <div class="switch form-inline">
                                    <input type="checkbox" id="rdActive" tabindex="1" name="switch" checked />
                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <input id="btnSubmit" type="button" title="submit" value="Submit" class="btn btn-primary" tabindex="1" />
                        <input id="btnCancel" type="button" title="Cancel" value="Cancel" class="btn btn-warning" tabindex="1" />
                        <input id="btnReport" type="button" title="Report" value="Excel Report" class="btn btn-primary" tabindex="1" />
                    </div>
                    <div class="col-12">
                        <table class="table table-striped table-bordered nowrap" id="BindDynamicTable">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Edit</th>
                                    <th>Session</th>
                                    <th>School/Institute</th>
                                    <th>Scheme Type</th>
                                    <th>Start Date</th>
                                    <th>End Date</th>
                                    <th>Att Lock Days</th>
                                    <th class="d-none">SMS Facility</th>
                                    <th class="d-none">Email Facility</th>
                                    <th>Teaching Plan</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>


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
        $(document).ready(function () {
            $('#<%=ddlSession.ClientID %>').change(function () {
                //debugger;
                var sessionids = $(this).val();

                $.ajax({
                    type: "POST",
                    url: '<%=Page.ResolveUrl("~/ACADEMIC/AttendanceConfig.aspx/BindColleges")%>',
                    data: '{ SessionId: "' + sessionids + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var lstbxSchool = $('#<%=lstbxSchool.ClientID %>');
                        lstbxSchool.empty();
                        $.each(data.d, function (index, item) {
                            lstbxSchool.append($('<option>', {
                                value: item.College_Id,
                                text: item.College_Name
                            }));
                        });
                        lstbxSchool.multiselect('refresh');
                        lstbxSchool.multiselect('rebuild');
                        //var test = $("#hdnGlobalClgIds").val();
                        if ($("#hdnGlobalClgIds").val() != '') {

                            $("#ctl00_ContentPlaceHolder1_lstbxSchool").multiselect('select', $("#hdnGlobalClgIds").val()).val();
                            $("#ctl00_ContentPlaceHolder1_lstbxSchool").val($("#hdnGlobalClgIds").val()).change();

                        }
                        $("#hdnGlobalClgIds").val("");

                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            });
        });
    </script>

    <script>
        function getSelectedIds(chk) {
            //debugger;
            var collegeIds = $(chk).val();

            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("~/ACADEMIC/AttendanceConfig.aspx/BindDegrees")%>',
                data: '{ CollegeIds: "' + collegeIds + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var ddlDegree = $('#<%=ddlDegree.ClientID %>');
                    ddlDegree.empty();
                    $.each(data.d, function (index, item) {
                        ddlDegree.append($('<option>', {
                            value: item.DegreeNo,
                            text: item.Degree_Name
                        }));
                    });
                    ddlDegree.multiselect('refresh');
                    ddlDegree.multiselect('rebuild');
                    //var test = $("#hdnGlobalDeg").val();


                    if ($("#hdnGlobalDeg").val() != '') {
                        //$("#ctl00_ContentPlaceHolder1_ddlDegree").multiselect('select', $("#hdnGlobalDeg").val()).val();


                        var degreenos = $("#hdnGlobalDeg").val().split(',');

                        degreenos.forEach(function (value) {
                            $("#ctl00_ContentPlaceHolder1_ddlDegree").multiselect('select', value);
                        });
                    }
                    $("#hdnGlobalDeg").val("");
                },
                error: function (error) {
                    console.log(error);
                }
            });
            }
    </script>

    <script>

        $('#btnSubmit').on('click', function () {
            var msg = ''; var Obj = {}; var Colg = ''; var Deg = ''; var Sem = ''
            var str = '';

            //debugger;
            Obj.Sessionno = $("#ctl00_ContentPlaceHolder1_ddlSession").val();

            $.each($("#ctl00_ContentPlaceHolder1_lstbxSchool").find("option:selected"), function (index, value) {
                Colg = Colg + $(value).val() + ",";
            });
            Colg = Colg.slice(0, -1);
            Obj.College_ids = Colg;


            $.each($("#ctl00_ContentPlaceHolder1_ddlDegree").find("option:selected"), function (index, value) {
                Deg = Deg + $(value).val() + ",";
            });
            Deg = Deg.slice(0, -1);
            Obj.Degreenos = Deg;

            Obj.SchemeType = $("#ctl00_ContentPlaceHolder1_ddlSchemeType").val();


            $.each($("#ctl00_ContentPlaceHolder1_ddlSemester").find("option:selected"), function (index, value) {
                Sem = Sem + $(value).val() + ",";
            });
            Sem = Sem.slice(0, -1);
            Obj.Semesternos = Sem;

            Obj.txtStartDate = document.getElementById("<%=txtStartDate.ClientID%>").value;
            Obj.txtEndDate = document.getElementById("<%=txtEndDate.ClientID%>").value;
            Obj.lockbyday = document.getElementById("<%=txtAttLockDay.ClientID%>").value;
            Obj.SmsFac = ($('#rdSMSYes').prop('checked'));
            Obj.EmailFac = ($('#rdEmailYes').prop('checked'));
            Obj.TeachingPlan = ($('#rdTeachYes').prop('checked'));
            Obj.IsActive = ($('#rdActive').prop('checked'));

            if (Obj.Sessionno == 0) {
                alert("Please select Session !!");
                return false;
            }

            if (Obj.College_ids == "") {
                alert("Please select School/Institute !!");
                return false;
            }

            if (Obj.Degreenos == "") {
                alert("Please select Degree !!");
                return false;
            }

            if (Obj.SchemeType == 0) {
                alert("Please select Scheme Type !!");
                return false;
            }

            if (Obj.Semesternos == "") {
                alert("Please select Semester !!");
                return false;
            }

            if (Obj.txtStartDate == '') {
                alert('Please Enter Start Date');
                document.getElementById("<%=txtStartDate.ClientID%>").focus();
                return false;
            }
            if (Obj.txtEndDate == '') {
                alert('Please Enter End Date');
                document.getElementById("<%=txtEndDate.ClientID%>").focus();
                return false;
            }
            if (Obj.lockbyday == '') {
                alert('Please Enter Attendance Lock By Day');
                //document.getElementById("<%=txtAttLockDay.ClientID%>").focus();
                return false;
            }

            var startDate = new Date(Obj.txtStartDate);
            var endDate = new Date(Obj.txtEndDate);

            if (Obj.txtStartDate != '' && Obj.txtEndDate != '') {


                var startDateParts = Obj.txtStartDate.split('/');
                var endDateParts = Obj.txtEndDate.split('/');

                var startDate = new Date(startDateParts[2], startDateParts[1] - 1, startDateParts[0]);
                var endDate = new Date(endDateParts[2], endDateParts[1] - 1, endDateParts[0]);


                if (startDate > endDate) {
                    alert('Attendance End Date should be greater than Attendance Start Date');
                    return false;
                }
            }
            var i = JSON.stringify(Obj);
            //alert(i);
            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("~/ACADEMIC/AttendanceConfig.aspx/SaveAttendanceConfig")%>',
                data: JSON.stringify(Obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {


                    if ($("#hdnEditMode").val() == "1") {
                        alert('Attendance Configuration Updated Successfully !!!');
                        $('#btnCancel').click(); // Added by vipul t on date 08/03/2024 as per Tno:- 55726
                    }
                    else {
                        alert('Attendance Configuration Added Successfully !!!');
                        $('#btnCancel').click(); // Added by vipul t on date 08/03/2024 as per Tno:- 55726
                    }


                    if (response.d == '') {

                        return false;
                    } else {

                        $("#BindDynamicTable").find("tbody").remove();

                        //debugger;
                        //str = '<thead class="bg-light-blue"><tr><th>Edit</th><th>Session</th><th>School/Institute</th><th>Scheme Type</th><th>Start Date</th><th>End Date</th><th>Att Lock Days</th><th>SMS Facility</th><th>Email Facility</th><th>Teaching Plan</th><th>Status</th></tr></thead><tbody>';
                        str = '<tbody id="BindDynamicTable">';
                        $.each(response.d, function (index, GetValue) {

                            str = str + '<tr>'
                            str = str + '<td><a id="btnEdit" class="fas fa-edit" title="Edit Record" href="#" onclick="EditClick(this)"></a>' +

                            '<input type="hidden" id="hdnSessionId" value="' + GetValue.SessionId + '"/><input type="hidden" id="hdnSemesterNos" value="' + GetValue.SemesterNos + '"/><input type="hidden" id="hdnSchemetyp" value="' + GetValue.SchemeType + '"/><input type="hidden" id="hdnCollegeIds" value="' + GetValue.College_Ids + '"/><input type="hidden" id="hdnDegreenos" value="' + GetValue.DegreeNos + '"/></td>'
                            str = str + '<td>' + GetValue.SessionName + '</td>'
                            str = str + '<td>' + GetValue.CollegeName + '</td>'
                            str = str + '<td>' + GetValue.SchemetypeName + '</td>'
                            str = str + '<td>' + GetValue.StartDateN + '</td>'
                            str = str + '<td>' + GetValue.EndDateN + '</td>'
                            str = str + '<td>' + GetValue.AttLockDays + '</td>'

                            // Commented by Vipul T on Date 04-03-2024 as per Tno:-55726
                            //if (GetValue.SMSFacility == true) {
                            //    str = str + '<td><span class="badge badge-success">Yes</span>' +
                            //        '<input type="hidden" id="idSmsFacility" value="true"/></td>'
                            //}
                            //else {
                            //    str = str + '<td><span class="badge badge-danger">No</span>' +
                            //        '<input type="hidden" id="idSmsFacility" value="false"/></td>'
                            //}
                            //if (GetValue.EmailFacility == true) {
                            //    str = str + '<td><span class="badge badge-success">Yes</span>' +
                            //        '<input type="hidden" id="idEmailFacility" value="true"/></td>'
                            //}
                            //else {
                            //    str = str + '<td><span class="badge badge-danger">No</span>' +
                            //        '<input type="hidden" id="idEmailFacility" value="false"/></td>'
                            //}

                            if (GetValue.TeachingPlan == true) {
                                str = str + '<td><span class="badge badge-success">Yes</span>' +
                                    '<input type="hidden" id="idteachingFacility" value="true"/></td>'
                            }
                            else {
                                str = str + '<td><span class="badge badge-danger">No</span>' +
                                    '<input type="hidden" id="idteachingFacility" value="false"/></td>'
                            }

                            if (GetValue.ActiveStatus == true) {
                                str = str + '<td><span class="badge badge-success">Active</span>' +
                                    '<input type="hidden" id="idIsActive" value="true"/></td>'
                            }
                            else {
                                str = str + '<td><span class="badge badge-danger">Inactive</span>' +
                                    '<input type="hidden" id="idIsActive" value="false"/></td>'
                            }
                            str = str + '</tr>'

                        });
                        str = str + '</tbody>';

                        $("#BindDynamicTable").append(str);
                        BindTable();
                        $('#btnCancel').click();
                    }

                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            //debugger;
            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("~/ACADEMIC/AttendanceConfig.aspx/BindList")%>',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d == '') {
                        return false;
                    } else {

                        // str = '<thead class="bg-light-blue"><tr><th>Edit</th><th>Session</th><th>School/Institute</th><th>Scheme Type</th><th>Start Date</th><th>End Date</th><th>Att Lock Days</th><th>SMS Facility</th><th>Email Facility</th><th>Teaching Plan</th><th>Status</th></tr></thead><tbody>';
                        str = '<tbody>';
                        $.each(response.d, function (index, GetValue) {

                            str = str + '<tr>'
                            str = str + '<td><a id="btnEdit" class="fas fa-edit" title="Edit Record" href="#" onclick="EditClick(this)"></a>' +

                            '<input type="hidden" id="hdnSessionId" value="' + GetValue.SessionId + '"/><input type="hidden" id="hdnSemesterNos" value="' + GetValue.SemesterNos + '"/><input type="hidden" id="hdnSchemetyp" value="' + GetValue.SchemeType + '"/><input type="hidden" id="hdnCollegeIds" value="' + GetValue.College_Ids + '"/><input type="hidden" id="hdnDegreenos" value="' + GetValue.DegreeNos + '"/></td>'
                            str = str + '<td>' + GetValue.SessionName + '</td>'
                            str = str + '<td>' + GetValue.CollegeName + '</td>'
                            str = str + '<td>' + GetValue.SchemetypeName + '</td>'
                            str = str + '<td>' + GetValue.StartDateN + '</td>'
                            str = str + '<td>' + GetValue.EndDateN + '</td>'
                            str = str + '<td>' + GetValue.AttLockDays + '</td>'

                            // Commented by Vipul T on Date 04-03-2024 as per Tno:-55726
                            //if (GetValue.SMSFacility == true) {
                            //    str = str + '<td><span class="badge badge-success">Yes</span>' +
                            //        '<input type="hidden" id="idSmsFacility" value="true"/></td>'
                            //}
                            //else {
                            //    str = str + '<td><span class="badge badge-danger">No</span>' +
                            //        '<input type="hidden" id="idSmsFacility" value="false"/></td>'
                            //}
                            //if (GetValue.EmailFacility == true) {
                            //    str = str + '<td><span class="badge badge-success">Yes</span>' +
                            //        '<input type="hidden" id="idEmailFacility" value="true"/></td>'
                            //}
                            //else {
                            //    str = str + '<td><span class="badge badge-danger">No</span>' +
                            //        '<input type="hidden" id="idEmailFacility" value="false"/></td>'
                            //}

                            if (GetValue.TeachingPlan == true) {
                                str = str + '<td><span class="badge badge-success">Yes</span>' +
                                    '<input type="hidden" id="idteachingFacility" value="true"/></td>'
                            }
                            else {
                                str = str + '<td><span class="badge badge-danger">No</span>' +
                                    '<input type="hidden" id="idteachingFacility" value="false"/></td>'
                            }

                            if (GetValue.ActiveStatus == true) {
                                str = str + '<td><span class="badge badge-success">Active</span>' +
                                    '<input type="hidden" id="idIsActive" value="true"/></td>'
                            }
                            else {
                                str = str + '<td><span class="badge badge-danger">Inactive</span>' +
                                    '<input type="hidden" id="idIsActive" value="false"/></td>'
                            }
                            str = str + '</tr>'

                        });
                        str = str + '</tbody>';

                        $("#BindDynamicTable").append(str);
                        BindTable();
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    </script>

    <script>
        function EditClick(ClickValue) {
            try {
                //debugger;
                $("#hdnEditMode").val("1");
                //alert($("#hdnEditMode").val());
                var td = $("td", $(ClickValue).closest("tr"));

                $("#ctl00_ContentPlaceHolder1_ddlSession").val($("[id*=hdnSessionId]", td).val()).change();

                $("#hdnGlobalClgIds").val($("[id*=hdnCollegeIds]", td).val());

                var degreenos = $("[id*=hdnDegreenos]", td).val();
                var degreenosArray = degreenos.split(',');
                var uniqueDegreenosArray = $.unique(degreenosArray);
                //var uniqueDegreenosString = uniqueDegreenosArray.join(',');

                $("#hdnGlobalDeg").val(uniqueDegreenosArray);

                $("#ctl00_ContentPlaceHolder1_ddlSchemeType").val($("[id*=hdnSchemetyp]", td).val()).change();

                var semesternos = $("[id*=hdnSemesterNos]", td).val();
                var semesternosArray = semesternos.split(',');
                var uniquesemesternosArray = $.unique(semesternosArray);

                uniquesemesternosArray.forEach(function (value) {
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").multiselect('select', value);
                });
                $('#<%= txtStartDate.ClientID %>').val(td[4].innerText);
                $('#<%= txtEndDate.ClientID %>').val(td[5].innerText);
                $('#<%= txtAttLockDay.ClientID %>').val(td[6].innerText);

                if ($("[id*=idSmsFacility]", td).val() == 'true') {
                    $('#rdSMSYes').prop('checked', true);
                }
                else {
                    $('#rdSMSYes').prop('checked', false);
                }

                if ($("[id*=idEmailFacility]", td).val() == 'true') {
                    $('#rdEmailYes').prop('checked', true);
                }
                else {
                    $('#rdEmailYes').prop('checked', false);
                }


                if ($("[id*=idteachingFacility]", td).val() == 'true') {
                    $('#rdTeachYes').prop('checked', true);
                }
                else {
                    $('#rdTeachYes').prop('checked', false);
                }


                if ($("[id*=idIsActive]", td).val() == 'true') {
                    $('#rdActive').prop('checked', true);
                }
                else {
                    $('#rdActive').prop('checked', false);
                }


            }
            catch (ex) {
            }
        }

    </script>

    <script>
        $('#btnCancel').on('click', function () {
            //debugger;
            $("#ctl00_ContentPlaceHolder1_ddlSession").val(0).change();
            $("#ctl00_ContentPlaceHolder1_ddlSchemeType").val(0).change();

            $('#ctl00_ContentPlaceHolder1_lstbxSchool').multiselect('deselectAll', false);
            $("#ctl00_ContentPlaceHolder1_ddlSemester").multiselect('deselectAll', false);
            $("#ctl00_ContentPlaceHolder1_ddlDegree").multiselect('deselectAll', false);
            $('#ctl00_ContentPlaceHolder1_lstbxSchool').multiselect('updateButtonText');
            $('#ctl00_ContentPlaceHolder1_ddlSemester').multiselect('updateButtonText');
            $('#ctl00_ContentPlaceHolder1_ddlDegree').multiselect('updateButtonText');

            $('#ctl00_ContentPlaceHolder1_ddlDegree').empty();

            $('#<%= txtStartDate.ClientID %>').val('');
            $('#<%= txtEndDate.ClientID %>').val('');
            $('#<%= txtAttLockDay.ClientID %>').val('');


            $('#rdSMSYes').prop('checked', true);
            $('#rdEmailYes').prop('checked', true);
            $('#rdTeachYes').prop('checked', true);
            $('#rdActive').prop('checked', true);

            $("#ctl00_ContentPlaceHolder1_lstbxSchool").val('').change();

            $("#hdnEditMode").val("");
            //alert($("#hdnEditMode").val());
        });
    </script>


    <script>

        $('#btnReport').on('click', function () {
            //debugger;
            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("~/ACADEMIC/AttendanceConfig.aspx/AttendanceConfigExcel")%>',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d == '') {
                        alert('Data Not Found !!!');
                        return false;
                    } else {
                        var jsonData = response.d;
                        // Prepare the Excel content
                        var excelContent = "Session\tSchool/Institute\tDegree\tSemester\tScheme Type\tStart Date\tEnd Date\tAttendance Lock Days\tSMS Facility\tEmail Facility\tTeaching Plan\tActive Status\t\n";
                        var marks = "";
                        jsonData.forEach(function (item) {
                            excelContent += item.SessionName + "\t" + item.CollegeName + "\t" + item.DegreeName + "\t" + item.SemesterName + "\t" + item.SchemetypeName + "\t" + item.StartDateN + "\t" + item.EndDateN + "\t" + item.AttLockDays + "\t" + item.SmsFacility_Str + "\t" + item.EmailFacility_Str + "\t" + item.TeachingPlan_Str + "\t" + item.ActiveStatus_Str + "\n";
                        });
                        var blob = new Blob([excelContent], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });

                        // Create a download link and trigger the download
                        var excelFileName = "AttendanceConfiguration.xlsx";
                        var link = document.createElement("a");
                        link.href = URL.createObjectURL(blob);
                        link.download = excelFileName;
                        link.click();
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });

        });
    </script>

</asp:Content>

