<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="QuestionWiseMarksEntry_Global.aspx.cs" Inherits="OBE_QuestionWiseMarksEntry" %>

<script runat="server">

    
</script>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="<%#Page.ResolveClientUrl("~/OBE/Scripts/Master/MarksEntry.js")%>"></script>--%>
    <style>
        #tblStudentMarksEntry .form-control {
            padding: 0.375rem 0.15rem;
        }
    </style>
    <script>
        $(function () {
            $('.container').removeClass('container').addClass('container-fluid');
        })
    </script>

    <style>
        .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5 !important;
        }

        .list-group .list-group-item .sub-label {
            float: left;
        }

        div.dataTables_wrapper div.dataTables_filter {
            float: left;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12" id="div2">
            <div class="box box-primary">
                <div id="div3" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Global Course Question Wise Mark Entry</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12" id="divSession" runat="server">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session Name</label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfdSessionno" runat="server" InitialValue="0" ErrorMessage="Please Select Session" SetFocusOnError="true"
                                           ControlToValidate="ddlSession"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-12" id="pnlSubjectDetail" runat="server" visible="false">
                        <div class="sub-heading">
                            <h5>Subject Details</h5>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSubject">
                                <thead class="bg-light-blue">
                                    <tr>
                                        <th>SrNo</th>
                                        <th>Subject Name</th>
                                        <%--<th>Scheme</th>--%>
                                        <%--<th>Semester</th>--%>
                                        <%--<th>Section</th>--%>
                                        <%--<th>Batch</th>--%>
                                        <th>Internal</th>
                                        <th>External</th>
                                        <th>Faculty Id</th>
                                    </tr>
                                </thead>
                                <tbody id="subjBody">
                                    <asp:Repeater ID="rptCourse" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex + 1 %></td>
                                                <td>
                                                    <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("SubjectName") %>' CommandName='<%#Eval("SectionName") %>' CommandArgument='<%# Eval("SectionId")%>' OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("SchemeSubjectId")%>' />
                                                    <%--+ "+" + Eval("EXAMNO")+"+"+Eval("FLDNAME")+"+"+Eval("EXAMNAME")--%>
                                                    <asp:HiddenField ID="hdnfld_courseno" runat="server" Value='<%# Eval("SchemeSubjectId")%>' />
                                                </td>
                                                <%--          <td>

                                                    <asp:LinkButton ID="lnkbtnScheme" runat="server" Text='<%# Eval("SCHEME") %>' CommandArgument='<%# Eval("SectionId")%>' OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("SchemeSubjectId")%>' />
              
                                                  
                                                </td>--%>
                                                <%--  <td>
                                               <asp:LinkButton ID="lnkbtnSemester" runat="server" Text='<%# Eval("SEMESTER") %>' CommandArgument='<%# Eval("SectionId")%>' OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("SchemeSubjectId")%>' />
  
                                                  
                                                </td>--%>
                                                <%--     <td>
                                                    <%#Eval("SectionName") %>
                                                </td>--%>
                                                <%-- <td>
                                                    <%#Eval("BATCHNO") %>
                                                </td>--%>
                                                <td>
                                                    <asp:Label runat="server" ID="lblInter" Text='<%#Eval("INTERNAL") %>'
                                                        ForeColor='<%# Convert.ToString(Eval("INTERNAL"))=="DONE" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                                    <%--<%#Eval("INTERNAL") %>--%>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="Label3" Text='<%#Eval("EXTERNAL") %>'
                                                        ForeColor='<%# Convert.ToString(Eval("EXTERNAL"))=="DONE" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>

                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblfaculty" Text='<%#Eval("FACULTY") %>'></asp:Label>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="col-12" id="pnlStudentMarksEntry" runat="server" visible="false">
                        <%--<div class="sub-heading">
                            <h5>Student Marks Entry</h5>
                        </div>--%>
                        <div class="row">
                            <div class="form-group col-lg-6 col-md-12 col-12 mt-md-2">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <a class="sub-label">
                                            <asp:Label ID="lblSubjectName" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                </ul>

                                <asp:HiddenField ID="hdnUserSession" runat="server" Value="0" />
                                <asp:HiddenField ID="hdfSubjectId" runat="server" />
                                <asp:HiddenField ID="hdfSectionId" runat="server" />
                                <asp:HiddenField ID="hdnUserno" runat="server" />
                                <asp:HiddenField ID="hdnIpAddress" runat="server" />
                                <asp:HiddenField ID="hdnElectType" runat="server" />
                                <asp:HiddenField ID="hdnCcode" runat="server" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Exam Name</label>
                                </div>
                                <asp:DropDownList ID="ddlExamName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdfSchemeTest" runat="server" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Subject Max Marks</label>
                                </div>
                                <asp:TextBox ID="txtsubmarks" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnPrint_Click" Visible="false" />
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-warning" Text="Back" CausesValidation="false" OnClick="btnBack_Click" />
                        </div>

                        <div class="col-12 pl-0 pr-0">
                            <div class="row">
                                <div class="form-group col-lg-4 col-md-9 col-12">
                                    <div class="row" runat="server" id="divExcelExport" visible="false">


                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <asp:FileUpload ID="FileUpload1" runat="server" /><br />
                                            <%--<input type="file" id="FileUpload1" class="btn btn-default form-control" /><br />--%>
                                            <span style="color: #FF0000;" id="xls" runat="server">(.xls/.xlsx Format Only)</span>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <asp:Button ID="btnImportExcel" runat="server" CssClass="btn btn-primary" Text="Import from Excel" OnClick="btnImportExcel_Click" />
                                            <%--<button type="button" tabindex="3" class="btn btn-primary" id="btnupload">Import from Excel</button>--%>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <asp:Button ID="btnDownloadExcelFormat" runat="server" CssClass="btn btn-primary" Text="Download Excel" OnClick="btnDownloadExcelFormat_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class="col-12" id="pnltblStudentMarksEntry">
                                        <div style="font-weight: bold" id="divStatusCode" runat="server" visible="false">Status Code</div>
                                        <asp:Literal ID="ltrStatusCode" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="form-group col-lg-2 col-md-3 col-12">
                                    <div class="label-dynamic">
                                        <label>
                                            <asp:Label ID="Label1" runat="server" Text="Student Count" Font-Bold="true" Visible="false"></asp:Label></label>
                                    </div>

                                    <%--<asp:Label ID="lblTotalCount" runat="server"  Font-Size="Medium" Font-Bold="true"></asp:Label>--%>
                                    <asp:TextBox ID="lblTotalCount" Font-Size="Medium" CssClass="StudCount" Enabled="false" runat="server" Visible="false"></asp:TextBox>
                                </div>

                            </div>
                        </div>

                        <div id="divsubmarks" class="col-12" runat="server" visible="false" style="display: none">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Active</label>
                                    </div>
                                    <asp:CheckBox ID="chkIsActive" runat="server" TabIndex="3" />
                                    <%-- <input type="checkbox" tabindex="3" id="IsActive" name="IsActive" />--%>
                                    <label for="IsActive">Check If Active</label>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Lock</label>
                                    </div>
                                    <asp:CheckBox ID="chkIsLock" runat="server" TabIndex="3" />
                                    <%--<input type="checkbox" tabindex="3" id="IsLock" name="IsLock" />--%>
                                </div>
                            </div>
                        </div>

                        <div class="horizontal">
                            <asp:Label ID="lblQuestionDetails" runat="server" Visible="false" CssClass="mb-2" Font-Bold="true"></asp:Label>
                            <asp:Literal ID="ltTable" runat="server" />
                        </div>

                        <div class="col-12 btn-footer" id="btnOperation" runat="server" visible="false">
                            <%-- <asp:Button ID="btnAdd" CssClass="btn btn-primary" runat="server" Text="Submit"/>--%>
                            <%--<asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Submit" />--%>
                            <button type="button" class="btn btn-primary" id="btnSubmit" runat="server">Submit</button>

                            <asp:Button ID="btnDownloadExcel" runat="server" CssClass="btn btn-primary" Text="Download Excel" OnClick="btnDownloadExcel_Click" />
                            <%-- <button type="button" class="btn btn-primary" id="btnDownloadExcel">Download Excel</button>--%>
                            <button type="button" class="btn btn-warning" id="btnLock" runat="server" visible="false">Lock</button>
                            <button type="button" class="btn btn-warning" id="btnUnLock" runat="server" visible="false">UnLock</button>
                            <%--  <button type="button" class="btn btn-warning" id="btnClose">Cancel</button>--%>
                            <asp:Button ID="btnB" runat="server" CssClass="btn btn-warning" Text="Back" CausesValidation="false" OnClick="btnB_Click" />

                            <%--<asp:Button ID="btncancle" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnBack_Click" />--%>
                            <input type="hidden" id="hdfCourseRegistrationDetailsIdId" name="hdfCourseRegistrationDetailsIdId" value="0" />
                            <input type="hidden" id="hdfStatusCode" name="hdfStatusCode" value="0" />

                        </div>

                        <div class="form-group col-lg-6 col-md-12 col-12" id="Div1" runat="server">
                            <div class=" note-div">
                                <h5 class="heading">Note </h5>
                                <p><i class="fa fa-star" aria-hidden="true"></i><span>You won't be able to modify the MARKS after it's been locked.</span></p>
                                <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            //ExamQuestionPaper.Init();

            function IsFloatOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode;
                if (charCode == 46 && evt.srcElement.value.split('.').length > 1) {
                    return false;
                }
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            };
            jQuery(document).on('keyup', function (evt) {
                if (evt.keyCode == 27) {
                    alert('Esc key pressed.');
                }
            });

            function NumericDotOnly(ob) {
                var invalidChars = /([^0-9.])/gi
                if (invalidChars.test(ob.value)) {
                    ob.value = ob.value.replace(invalidChars, "");
                }
            }

            jQuery(document).on('keyup', function (evt) {
                if (evt.keyCode == 27) {
                    alert('Esc key pressed.');
                }
            });
        });

    </script>
    <script>
        function showLockConfirm() {
            var ret = confirm('Are you sure, do you really want to lock entered mark of students.?\nOnce locked it cannot be modified again.');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>

    <script>
        $(document).ready(function () {
            debugger;
            $(".quetext").keypress(function (e) {
                if (((e.which != 46 || (e.which == 46 && $(this).val() == '')) || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
                    e.preventDefault();
                }
            }).on('paste', function (e) {
                e.preventDefault();
            });

            $(".quetext").keyup(function () {
                // debugger
                var txtLen = 0;
                var tot = 0;
                var grp = 0;
                var pGrp = 0;
                var prvGrp = 0;
                var prvPgrp = 0;
                var hGrp = new Array();
                var subQNo = new Array();
                var hGrpVal = new Array();
                var hMaxLevel = new Array();
                var hPgId = new Array();
                var hOQ = new Array();
                var k = 0;
                var m = 0;
                var totta = new Array();
                var tx = 0;
                var oQ = 0;

                for (var i = 0; i < $(this).closest('table').find('thead').find('tr').find('[id^="hdfGroup"]').length; i++) {
                    hGrp[i] = $(this).closest('table').find('thead').find('tr').find('[id^="hdfGroup"]').eq(i).val();
                    subQNo[i] = $(this).closest('table').find('thead').find('tr').find('[id^="hdfSubQuestion"]').eq(i).val();
                    hGrpVal[i] = $(this).closest('table').find('thead').find('tr').find('[id^="hdf_"]').eq(i).val();
                    hMaxLevel[i] = $(this).closest('table').find('thead').find('tr').find('[id^="hdfMaxLevel_"]').eq(i).val();
                    hPgId[i] = $(this).closest('table').find('thead').find('tr').find('[id^="hdfPgId_"]').eq(i).val();
                    hOQ[i] = $(this).closest('table').find('thead').find('tr').find('[id^="hdfOQ_"]').eq(i).val();
                }
                //  debugger
                //alert(hGrp);
                txtLen = $(this).closest('tr').find('input[type=text]').length;
                //debugger;
                for (var i = 0; i < txtLen - 1; i++) {
                    if ($(this).closest('tr').find('input[type=text]').eq(i).prop('disabled') == false) {
                        if ($(this).parent().parent().find('input[type=text]').eq(i).val() != "") {
                            //debugger;

                            if (parseFloat($(this).parent().parent().find('input[type=text]').eq(i).val()) > parseFloat(hGrpVal[i])) {
                                alert('Marks can not be greater than ' + parseFloat(hGrpVal[i]));
                                $(this).parent().parent().find('input[type=text]').eq(i).val('');
                                return;
                            }


                            m = i;

                            pGrp = $(this).closest('table').find('thead').find('tr').find('[id^="hdfPgId_"]').eq(i).val();
                            if (prvPgrp != pGrp) {
                                prvPgrp = pGrp;

                                var idPx = $.map(hPgId, function (o, i) { if (o == pGrp) return i; })
                                if (idPx.length > 0) {
                                    debugger;
                                    while (k < idPx.length) {

                                        grp = $(this).closest('table').find('thead').find('tr').find('[id^="hdfGroup"]').eq(m).val();
                                        if (prvGrp != grp) {
                                            prvGrp = grp;

                                            var idx = $.map(hGrp, function (o, m) { if (o == grp) return m; })
                                            if (idx.length > 0) {
                                                var arr = new Array();
                                                var prevVal = 0;
                                                var totx = 0;
                                                var arrI = -1;
                                                var ii = 0;
                                                //i += idx.length;
                                                //alert(idx);
                                                for (var j = 0; j < idx.length; j++) {
                                                    ii = idx[j];
                                                    if (prevVal != subQNo[idx[j]]) {
                                                        prevVal = subQNo[idx[j]];
                                                        totx = 0;
                                                        if ($(this).parent().parent().find('input[type=text]').eq(ii).val() != "") {

                                                            totx += parseFloat($(this).parent().parent().find('input[type=text]').eq(ii).val());

                                                        }
                                                        arrI++;
                                                        arr[arrI] = totx;
                                                    }
                                                    else {

                                                        if ($(this).parent().parent().find('input[type=text]').eq(ii).val() != "") {

                                                            totx += parseFloat($(this).parent().parent().find('input[type=text]').eq(ii).val());

                                                        }
                                                        arr[arrI] = totx;
                                                    }

                                                    if (idx.length == j + 1) {
                                                        var tt = 0;
                                                        var high = 0;

                                                        //if (hMaxLevel[idx[j]].cou) {
                                                        // if (jQuery.inArray("3", hMaxLevel[idx[j]]) !== -1){ //Commnted  01/09/22

                                                        if (jQuery.inArray("3", hMaxLevel[idx[j]]) !== -1 && hOQ[idx[j]] == 0) { //addded on 010922
                                                            for (var n = 0; n < arr.length; n++) {
                                                                //tt += parseFloat(arr[n]) << 0;
                                                                tt += parseFloat(arr[n]);
                                                            }
                                                        }
                                                        else if (jQuery.inArray("2", hMaxLevel[idx[j]] !== -1) && hOQ[idx[j]] == 0) {
                                                            for (var n = 0; n < arr.length; n++) {
                                                                //tt += parseFloat(arr[n]) << 0;
                                                                tt += parseFloat(arr[n]);
                                                            }
                                                        }
                                                        else {
                                                            high = Math.max.apply(Math, arr);
                                                            //tot += parseInt(high);
                                                        }

                                                        if (high < tt) {
                                                            if (idPx.length > 1)
                                                                totta[tx] = tt;
                                                            else
                                                                tot += tt;
                                                        }
                                                        else {
                                                            if (idPx.length > 1)
                                                                totta[tx] = parseFloat(high);
                                                            else
                                                                tot += parseFloat(high);
                                                        }
                                                        tx++;
                                                        oQ = hOQ[idx[j]];
                                                    }
                                                    m++;
                                                    k++;
                                                }

                                            }
                                            else {
                                                debugger
                                                tot += parseFloat($(this).parent().parent().find('input[type=text]').eq(i).val());
                                            }
                                        }
                                        else {
                                            k++;
                                        }
                                        if (idPx.length == k) {
                                            if (totta.length != 0) {
                                                if (oQ == 0) {
                                                    totx = 0;
                                                    for (var nn = 0; nn < totta.length; nn++) {
                                                        totx += totta[nn];
                                                    }
                                                    tot += totx;
                                                }
                                                else {
                                                    debugger
                                                    high = Math.max.apply(Math, totta);
                                                    tot += parseFloat(high);
                                                }
                                            }
                                        }

                                    }
                                    k = m = tx = 0;
                                    totta = [];
                                }
                            }
                        }
                    }
                }
                debugger
                //var total = Math.round(tot);
                var total = (tot);//added on 21062023 for client request
                //$(this).parent().parent().find('input[type=text]').eq(txtLen - 1). val(tot);
                $(this).parent().parent().find('input[type=text]').eq(txtLen - 1).val(total);

            });

            $('.chkAbsentCls').click(function () {
                var txts = $(this).closest('tr').find('input[type=text]').length;
                if ($(this).prop('checked')) {
                    $(this).closest('tr').find('input[type=text]').val(0);
                    $(this).closest('tr').find('input[type=text]').prop('disabled', true);
                    $(this).closest('tr').find('input[type=text]').eq(txts - 1).prop('disabled', false);
                    $(this).closest('tr').find('input[type=text]').eq(txts - 1).val('902');

                }
                else {
                    $(this).closest('tr').find('input[type=text]').prop('disabled', false);
                    $(this).closest('tr').find('input[type=text]').val('');
                    $(this).closest('tr').find('input[type=text]').eq(txts - 1).prop('disabled', true);
                }
            });

            //$('.MarksTotCls').keyup(function () {
            //    if (("902").indexOf($(this).val()) == -1 && ("903").indexOf($(this).val()) == -1 || ("0").indexOf($(this).val()) == 0) {
            //        alert('Only 902 and 903 are allowed.');
            //        $(this).val('');
            //    }
            //});



            $('.MarksTotCls').keyup(function () {
                if (("904").indexOf($(this).val()) == -1 && ("902").indexOf($(this).val()) == -1 && ("903").indexOf($(this).val()) == -1 || ("0").indexOf($(this).val()) == 0) {
                    alert('Only 902,903 and 904 are allowed.');
                    $(this).val('');
                }
            });


        });
    </script>

    <script>
        var TempQue = "";
        var StatusData;
        var TOTval;
        var markstable;
        //var AssignSubjectExamQuestionPattern = function () {
        //    var init = function () {
        //        $(".loader-area, .loader").css("display", "block");
        //        BindDropDownLists();
        //        $(".loader-area, .loader").fadeOut('slow');

        //    };

        //Bind all drop down lists on load
        //var BindDropDownLists = function () {
        //    $.ajax({
        //        type: 'POST',
        //        url: '/MarksEntry/BindDropDown',
        //        dataType: 'json',
        //        success: function (data) {
        //            RenderDropDown($('#ctl00_ContentPlaceHolder1_ddlSession'), data["lstDropDownList"])
        //        },
        //        error: function () {
        //            toastr.error('Exception Occurred !');
        //        }
        //    });
        //};

        $('#btnClose').click(function () {
            //debugger;
            ClearData();
        });


        $('#ctl00_ContentPlaceHolder1_btnLock').click(function () {
            debugger
            var ret = confirm('Are you sure, do you really want to lock entered mark of students.?\nOnce locked it cannot be modified again.');
            if (ret == true)
                1;
            else
                return false;
            $(".loader-area, .loader").css("display", "block");
            var mrcount = 0;
            var MaxMarks = 0;
            var dataTable = '[{';
            var dataTable1 = '[{';
            $("#tblStudentMarksEntry TBODY TR").each(function () {
                var totsum = 0;
                var totalMarks = 0;
                var CourseRegistrationDetailsId = $(this).find($('[id^=hdfCourseRegistrationDetailsId]')).val();
                var ExamPatternMappingId = $(this).find($('[id^=hdfExamPatternMappingId]')).val();

                var IsLock = true;
                var ActiveStatus = true;

                $(this).find("td").each(function () {

                    var lenghtext = $(this).find('.quetext').length;
                    if (lenghtext > 0) {
                        var quemarks = ($(this).find("input[type=text]").val());

                        var questionno = $(this).find($('[id^=hdfPaperQuestionsId]')).val();
                        var submarks = $("#txtsubmarks").val();

                        if (quemarks == "" || quemarks == null) {
                            mrcount++;

                            $(this).find("input[type=text]").css("background-color", "#E5CCFF")

                            //$(this).find("input[type=text]").addClass('focus');
                            //return;
                        }
                        else {

                            $(this).find("input[type=text]").removeClass('focus');
                        }


                        totsum += Number(quemarks);

                        if (dataTable == '[{') {
                            dataTable = dataTable + '"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","PaperQuestionsId": "' + questionno + '","quemarks": "' + quemarks + '","IsLock": "' + IsLock + '","ActiveStatus": "' + ActiveStatus + '"}'
                        }
                        else {

                            dataTable = dataTable + ',{"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","PaperQuestionsId": "' + questionno + '","quemarks": "' + quemarks + '","IsLock": "' + IsLock + '","ActiveStatus": "' + ActiveStatus + '"}'

                        }

                    }
                    var TotLeng = $(this).find('.MarksTotCls').length;
                    if (TotLeng > 0) {

                        totalMarks = ($(this).find("input[type=text]").val());
                        if (totalMarks == "" || totalMarks == null) {
                            mrcount++;

                            $(this).find("input[type=text]").addClass('focus');
                            //return;
                        }
                        else {

                            $(this).find("input[type=text]").removeClass('focus');
                        }
                    }

                    var Mxmrk = $(this).find($('[id^=hdMaxMarks]')).val();

                    //var Mx2;
                    //if (Mxmrk > 0) {
                    //    Mx2 = Mxmrk.replace("/", ".00");
                    //}


                    if (parseFloat(quemarks) > parseFloat(Mxmrk)) {
                        MaxMarks++;

                        $(this).find("input[type=text]").addClass('focus');
                        //return;
                    }
                    else {

                        // $(this).find("input[type=text]").removeClass('focus');
                    }
                    //}
                });



                if (dataTable1 == '[{') {
                    dataTable1 = dataTable1 + '"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","MarksObtained": "' + totalMarks + '"}'
                }
                else {

                    dataTable1 = dataTable1 + ',{"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","MarksObtained": "' + totalMarks + '"}'
                }

            });

            dataTable = dataTable + ']';
            dataTable1 = dataTable1 + ']';

            var MarkEntryList = JSON.parse(dataTable);
            var TotMarksList = JSON.parse(dataTable1);
            var LockStatus = 1;
            $(".loader-area, .loader").fadeOut('slow');



            if (MaxMarks > 0) {

                alert('Marks can not be greater than its maximum limits marks ');

            }

            else {

                saveData(MarkEntryList, TotMarksList, LockStatus);
            }

            //if (Number(mrcount) > 0) {
            //    alert('Record Not Saved, Due to marks entry mandatory for all questions', 'Warning!');
            //}
            //else {

            //}

        });




        $('#ctl00_ContentPlaceHolder1_btnUnLock').click(function () {
            debugger
            var ret = confirm('Are you sure, do you really want to UnLock entered mark of students.');
            if (ret == true)
                1;
            else
                return false;
            $(".loader-area, .loader").css("display", "block");
            var mrcount = 0;
            var MaxMarks = 0;
            var dataTable = '[{';
            var dataTable1 = '[{';
            $("#tblStudentMarksEntry TBODY TR").each(function () {
                var totsum = 0;
                var totalMarks = 0;
                var CourseRegistrationDetailsId = $(this).find($('[id^=hdfCourseRegistrationDetailsId]')).val();
                var ExamPatternMappingId = $(this).find($('[id^=hdfExamPatternMappingId]')).val();

                var IsLock = false;
                var ActiveStatus = true;

                $(this).find("td").each(function () {

                    var lenghtext = $(this).find('.quetext').length;
                    if (lenghtext > 0) {
                        var quemarks = ($(this).find("input[type=text]").val());

                        var questionno = $(this).find($('[id^=hdfPaperQuestionsId]')).val();
                        var submarks = $("#txtsubmarks").val();

                        if (quemarks == "" || quemarks == null) {

                            mrcount++;

                            $(this).find("input[type=text]").css("background-color", "#E5CCFF")

                            //$(this).find("input[type=text]").addClass('focus');
                            //return;
                        }
                        else {

                            $(this).find("input[type=text]").removeClass('focus');
                        }


                        totsum += Number(quemarks);

                        if (dataTable == '[{') {
                            dataTable = dataTable + '"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","PaperQuestionsId": "' + questionno + '","quemarks": "' + quemarks + '","IsLock": "' + IsLock + '","ActiveStatus": "' + ActiveStatus + '"}'
                        }
                        else {

                            dataTable = dataTable + ',{"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","PaperQuestionsId": "' + questionno + '","quemarks": "' + quemarks + '","IsLock": "' + IsLock + '","ActiveStatus": "' + ActiveStatus + '"}'

                        }

                    }
                    var TotLeng = $(this).find('.MarksTotCls').length;
                    if (TotLeng > 0) {

                        totalMarks = ($(this).find("input[type=text]").val());
                        if (totalMarks == "" || totalMarks == null) {
                            mrcount++;

                            $(this).find("input[type=text]").addClass('focus');
                            //return;
                        }
                        else {

                            $(this).find("input[type=text]").removeClass('focus');
                        }
                    }

                    var Mxmrk = $(this).find($('[id^=hdMaxMarks]')).val();

                    //var Mx2;
                    //if (Mxmrk > 0) {
                    //    Mx2 = Mxmrk.replace("/", ".00");
                    //}


                    if (parseFloat(quemarks) > parseFloat(Mxmrk)) {
                        MaxMarks++;

                        $(this).find("input[type=text]").addClass('focus');
                        //return;
                    }
                    else {

                        // $(this).find("input[type=text]").removeClass('focus');
                    }
                    //}
                });



                if (dataTable1 == '[{') {
                    dataTable1 = dataTable1 + '"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","MarksObtained": "' + totalMarks + '"}'
                }
                else {

                    dataTable1 = dataTable1 + ',{"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","MarksObtained": "' + totalMarks + '"}'
                }

            });

            dataTable = dataTable + ']';
            dataTable1 = dataTable1 + ']';

            var MarkEntryList = JSON.parse(dataTable);
            var TotMarksList = JSON.parse(dataTable1);
            var LockStatus = 2;
            $(".loader-area, .loader").fadeOut('slow');



            if (MaxMarks > 0) {

                alert('Marks can not be greater than its maximum limits marks ');

            }

            else {

                saveData(MarkEntryList, TotMarksList, LockStatus);
            }

            //if (Number(mrcount) > 0) {
            //    alert('Record Not Saved, Due to marks entry mandatory for all questions', 'Warning!');
            //}
            //else {

            //}

        });



        $('#ctl00_ContentPlaceHolder1_btnSubmit').click(function () {

            debugger;
            // Console.log("ctl00_ContentPlaceHolder1_btnSubmit")
            var MaxMarks = 0;
            var ret = confirm('Are you sure, do you want to submit marks of students?');
            if (ret == true)
                1;
            else {
                SubmitHightFiled();
                return false;
            }

            $(".loader-area, .loader").css("display", "block");
            var mrcount = 0;
            var dataTable = '[{';
            var dataTable1 = '[{';

            var flag = false;
            $("#tblStudentMarksEntry TBODY TR").each(function () {
                if (flag == true) {
                    return false;
                }
                var totsum = 0;
                var totalMarks = 0;
                var CourseRegistrationDetailsId = $(this).find($('[id^=hdfCourseRegistrationDetailsId]')).val();
                var ExamPatternMappingId = $(this).find($('[id^=hdfExamPatternMappingId]')).val();

                var IsLock = false;
                var ActiveStatus = false;

                if ($("#ctl00_ContentPlaceHolder1_chkIsLock").is(":checked")) {
                    IsLock = false;
                }
                else {
                    IsLock = false;
                }
                if ($("#ctl00_ContentPlaceHolder1_chkIsActive").is(":checked")) {
                    ActiveStatus = true;
                }
                else {
                    ActiveStatus = false;
                }

                $(this).find("td").each(function () {

                    var lenghtext = $(this).find('.quetext').length;
                    if (lenghtext > 0) {
                        var quemarks = ($(this).find("input[type=text]").val());
                        // var textid = ($(this).find("input[type=text]").attr("id"));

                        var questionno = $(this).find($('[id^=hdfPaperQuestionsId]')).val();
                        var submarks = $("#txtsubmarks").val();
                        if (quemarks == "") { //|| quemarks == null

                            $(this).find("input[type=text]").css({ 'backgroundColor': '#f8d7da' });//added on 05042023 for mark not null
                            $(this).find("input[type=text]").addClass('focus');

                            flag = true;
                            return false;

                        }
                        else {

                            $(this).find("input[type=text]").removeClass('focus');
                        }



                        totsum += Number(quemarks);

                        if (dataTable == '[{') {
                            dataTable = dataTable + '"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","PaperQuestionsId": "' + questionno + '","quemarks": "' + quemarks + '","IsLock": "' + IsLock + '","ActiveStatus": "' + ActiveStatus + '"}'
                        }
                        else {

                            dataTable = dataTable + ',{"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","PaperQuestionsId": "' + questionno + '","quemarks": "' + quemarks + '","IsLock": "' + IsLock + '","ActiveStatus": "' + ActiveStatus + '"}'

                        }

                    }

                    var Mxmrk = $(this).find($('[id^=hdMaxMarks]')).val();

                    if (parseFloat(quemarks) > parseFloat(Mxmrk)) {
                        MaxMarks++;

                        $(this).find("input[type=text]").addClass('focus');
                        //return;
                    }
                    else {

                        // $(this).find("input[type=text]").removeClass('focus');
                    }


                    var TotLeng = $(this).find('.MarksTotCls').length;
                    if (TotLeng > 0)
                    {
                        totalMarks = ($(this).find("input[type=text]").val());
                        if (totalMarks == "" || totalMarks == null) {
                            mrcount++;
                            // alert("Mark entry mandatory for all questions");
                            $(this).find("input[type=text]").addClass('focus');
                            $(this).find("input[type=text]").css({ 'backgroundColor': '#f8d7da' });//added on 05042023 for total mark not null
                            flag = true; //added on 05042023 for total mark not null
                            return false; //added on 05042023 for total mark not null
                            // return;

                        }
                        else {

                            $(this).find("input[type=text]").removeClass('focus');
                        }
                        //alert(totalMarks);


                    }

                });

                if (dataTable1 == '[{') {
                    dataTable1 = dataTable1 + '"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","MarksObtained": "' + totalMarks + '"}'
                }
                else {

                    dataTable1 = dataTable1 + ',{"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","MarksObtained": "' + totalMarks + '"}'
                }

            });

            dataTable = dataTable + ']';
            dataTable1 = dataTable1 + ']';

            var MarkEntryList = JSON.parse(dataTable);
            var TotMarksList = JSON.parse(dataTable1);
            var LockStatus = 0;


            if (MaxMarks > 0) {
                alert('Marks can not be greater than its maximum limits marks ');
            }
            else {
                //alert('5368767');
                if (flag == false) {
                    saveData(MarkEntryList, TotMarksList, LockStatus);
                }
                else {
                    alert("Mark entry mandatory for all questions");

                }
                //alert('hiii');
            }

        });


        //Load PEO History Detail on Control
        function LoadCurriculumDataData(rowData, SectionName) {
            if (SectionName != "" || SectionName != null) {
                $("#lblSubjectName").html("Subject Name :" + rowData.SchemeMappingName + " (Sec-" + SectionName + ")");
            } else {
                $("#lblSubjectName").html("Subject Name :" + rowData.SchemeMappingName);
            }
            $("#hdfSubjectId").val(rowData.SchemeSubjectId);
        };

        //Bind Exam Name Drop Down
        //var BindExamNameDropDownLists = function (id, SecId) {
        //    $.ajax({
        //        type: 'POST',
        //        url: '/MarksEntry/BindExamDropDown?SchemeSubjectId='+id+'&SectionId='+SecId,
        //        dataType: 'json',
        //        success: function (data) {
        //            RenderDropDown($('#ctl00_ContentPlaceHolder1_ddlExamName'), data["lstDropDownList"])
        //        },
        //        error: function () {
        //            toastr.error('Exception Occurred !');
        //        }
        //    });
        //};

        function SubmitHightFiled() {
            debugger
            var MaxMarks = 0;
            $("#tblStudentMarksEntry TBODY TR").each(function () {
                var totsum = 0;
                var totalMarks = 0;
                var CourseRegistrationDetailsId = $(this).find($('[id^=hdfCourseRegistrationDetailsId]')).val();
                var ExamPatternMappingId = $(this).find($('[id^=hdfExamPatternMappingId]')).val();


                $(this).find("td").each(function () {

                    var lenghtext = $(this).find('.quetext').length;
                    if (lenghtext > 0) {
                        var quemarks = ($(this).find("input[type=text]").val());
                        // var textid = ($(this).find("input[type=text]").attr("id"));

                        var questionno = $(this).find($('[id^=hdfPaperQuestionsId]')).val();
                        var submarks = $("#txtsubmarks").val();
                        if (quemarks == "" || quemarks == null) {

                            $(this).find("input[type=text]").addClass('focus');
                        }
                        else {

                            $(this).find("input[type=text]").removeClass('focus');
                        }

                        var Mxmrk = $(this).find($('[id^=hdMaxMarks]')).val();
                        if (parseFloat(quemarks) > parseFloat(Mxmrk)) {
                            MaxMarks++;

                            $(this).find("input[type=text]").addClass('focus');
                            //return;
                        }
                        else {

                            // $(this).find("input[type=text]").removeClass('focus');
                        }

                    }

                });
            });

            if (MaxMarks > 0) {

                alert('Marks can not be greater than its maximum limits marks ');

            }

            else {


            }
        }


        function BindMarksEntryStudentData() {

            $("#pnltblStudentMarksEntry").css("display", "none");
            $(".loader-area, .loader").css("display", "block");
            var Obj = {};
            Obj.QuestionPaperId = $('#ctl00_ContentPlaceHolder1_ddlExamName').val();
            Obj.SchemeSubjectId = $("#ctl00_ContentPlaceHolder1_hdfSubjectId").val();
            Obj.SessionId = $("#ctl00_ContentPlaceHolder1_hdnUserSession").val();
            Obj.SectionId = $('#ctl00_ContentPlaceHolder1_hdfSectionId').val();
            $.ajax({
                url: 'MarksEntry.aspx/GetMarksEntryStudentData',
                type: "POST",
                data: JSON.stringify(Obj),
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    //console.log("DATA",data);
                    BindMarksEntryStudentTable(data);
                    $("#pnltblStudentMarksEntry").css("display", "block");
                    $(".loader-area, .loader").fadeOut('slow');
                },
                error: function (errResponse) {

                    console.log(errResponse);
                    $(".loader-area, .loader").fadeOut('slow');
                }
            });

        };

        function BindMarksEntryStudentTable(data) {
            //debugger;
            $(".loader-area, .loader").css("display", "block");
            var countr = 0, quecount = 0;;
            StatusData = data["MarkEntryStatusCode"];

            $.each(data["StudentsMult"], function () {
                countr++;

            });
            // alert(countr);
            $.each(data["SubMaxMarks"], function () {
                $("#txtsubmarks").val(this.MaxMarks);
            });
            $.each(data["Students"], function () {
                if (this.IsLock == "True" || this.IsLock == "true") {
                    $("#IsLock").prop("checked", true);
                    $("#IsLock").prop("disabled", true);
                    $("#btnAdd").prop("disabled", true);
                    $("#btnupload").prop("disabled", true);                     // Added on 24-04-2020     
                }
                else {
                    $("#IsLock").prop("checked", false);
                    $("#IsLock").prop("disabled", false);
                    $("#btnAdd").prop("disabled", false);
                    $("#btnupload").prop("disabled", false);                    // Added on 24-04-2020     
                }
                if (this.ActiveStatus == "True" || this.ActiveStatus == "true") {

                    $("#IsActive").prop("checked", true)
                }
                else {
                    $("#IsActive").prop("checked", false)
                }
            });

            var ColBody = "<tr>";

            $('#tbodyMarkEntryStatusCode').empty();
            $.each(data["MarkEntryStatusCode"], function (i, status) {
                $('#hdfStatusCode').val(status.StatusCode);
                ColBody += "<td><label>" + status.StatusCode + " : " + status.StatusCodeMeaning + "  &nbsp; ( Please check Absent checkbox to mark as a absent )<label></td>";

            });
            ColBody += "</tr>"
            ColBody += "</tr>"
            $('#tbodyMarkEntryStatusCode').append(ColBody);

            $('#tblStudentMarksEntry').empty();
            setTimeout(function () {
                var table = $('#tblStudentMarksEntry').removeAttr('width').DataTable({
                    destroy: true,
                    scrollX: true,
                    scrollY: "450px",
                    paging: false,
                    //dom: 'Bfrtip',
                    bSort: false,
                    autoWidth: true,
                    fixedColumns: false,
                    select: {
                        style: 'multi'
                    },

                });
            }, 100);
            var tblMapping = '';
            var tblHeading = '';
            tblHeading += '<thead><tr><th style="width:8%;">Registration No.</th>';

            //  tblHeading += '<th style="width:15%;">Student Name</th>';

            tblHeading += '<th style="width:3%;" >Absent</th>';
            TempQue = data["Question"];
            $.each(data["Question"], function (j, key) {
                tblHeading += '<th >' + key.QuestionWithMarks + '<input type="hidden" id="hdf_' + j + '" value="' + key.MaxMarks + '" /></th>';
                quecount++;
            });
            tblHeading += '<th >Total Marks</th>';
            tblHeading += '</tr></thead>';
            tblMapping += tblHeading;
            var tblBody = '<tbody>';
            var StatusCode = $("#hdfStatusCode").val();

            if (countr > 0) {
                $.each(data["Students"], function (ik, valHead) {

                    tblBody += '<tr><td style="width:8%;">' + valHead.EnrollmentNo + '<input type="hidden" id="hdfCourseRegistrationDetailsId" value="' + valHead.CourseRegistrationDetailsId + '" /><input type="hidden" id="hdfExamPatternMappingId" value="' + valHead.ExamPatternMappingId + '" /></td>';
                    // tblBody += '<td style="width:15%;">' + valHead.StudentName + '<input type="hidden" id="hdfCourseRegistrationDetailsId" value="' + valHead.CourseRegistrationDetailsId + '" /><input type="hidden" id="hdfExamPatternMappingId" value="' + valHead.ExamPatternMappingId + '" /></td>';
                    tblBody += '<td style="width:3%;"><div class="checkbox checkbox-primary">';

                    if (valHead.IsLock == "True" && parseInt(valHead.TOTMarks) == StatusCode) {
                        tblBody += '<input type="checkbox" class="chkAbsentCls" checked="checked" id="chkAbsent' + valHead.EnrollmentNo + '" disabled="disabled" onclick="disableRows(this);"  /><label for="chkAbsent' + valHead.EnrollmentNo + '">&nbsp;</label>';
                    } else if (valHead.IsLock == "True" && parseInt(valHead.TOTMarks) != StatusCode) {
                        tblBody += '<input type="checkbox" class="chkAbsentCls"  id="chkAbsent' + valHead.EnrollmentNo + '" disabled="disabled" onclick="disableRows(this);"  /><label for="chkAbsent' + valHead.EnrollmentNo + '">&nbsp;</label>';
                    } else if (valHead.IsLock != "True" && parseInt(valHead.TOTMarks) == StatusCode) {
                        tblBody += '<input type="checkbox" class="chkAbsentCls"  id="chkAbsent' + valHead.EnrollmentNo + '" checked="checked"  onclick="disableRows(this);"  /><label for="chkAbsent' + valHead.EnrollmentNo + '">&nbsp;</label>';
                    } else {
                        tblBody += '<input type="checkbox" class="chkAbsentCls " onclick="disableRows(this);"  id="chkAbsent' + valHead.EnrollmentNo + '"   /><label for="chkAbsent' + valHead.EnrollmentNo + '">&nbsp;</label>';
                    }

                    tblBody += '</div></td>';

                    $.each(data["Question"], function (i, verHead) {
                        var quemarks = checkmethod(verHead.PaperQuestionsId, valHead.StudentId, valHead.CourseRegistrationDetailsId, data["StudentsMult"]);

                        if (valHead.IsLock == "True" || parseInt(valHead.TOTMarks) == StatusCode) {
                            tblBody += '<td><input type="text" disabled = "disabled"  class="form-control quetext"  id="txtObtainedMarks_' + i + '_' + valHead.CourseRegistrationDetailsId + '" onkeypress="return isNumberKey(event)" value= ' + quemarks + '   maxLength="4" /><input type="hidden" id="hdfPaperQuestionsId" value="' + verHead.PaperQuestionsId + '" /></td>';
                        }
                        else {
                            tblBody += '<td><input type="text"  class="form-control quetext"  id="txtObtainedMarks_' + i + '_' + valHead.CourseRegistrationDetailsId + '" onkeypress="return isNumberKey(event)" value= ' + quemarks + '   maxLength="4" /><input type="hidden" id="hdfPaperQuestionsId" value="' + verHead.PaperQuestionsId + '" /></td>';
                        }

                    });


                    tblBody += '<td><input type="text" class="form-control MarksTotCls" id="txtToT_' + valHead.CourseRegistrationDetailsId + '" value=' + valHead.TOTMarks + ' disabled="disabled"/></td>';
                    tblBody += '</tr>';
                });

            }
            else {
                $.each(data["Students"], function (ik, valHead) {
                    tblBody += '<tr><td style="width:8%;">' + valHead.EnrollmentNo + '<input type="hidden" id="hdfCourseRegistrationDetailsId" value="' + valHead.CourseRegistrationDetailsId + '" /><input type="hidden" id="hdfExamPatternMappingId" value="' + valHead.ExamPatternMappingId + '" /></td>';
                    // tblBody += '<td style="width:15%;">' + valHead.StudentName + '<input type="hidden" id="hdfCourseRegistrationDetailsId" value="' + valHead.CourseRegistrationDetailsId + '" /><input type="hidden" id="hdfExamPatternMappingId" value="' + valHead.ExamPatternMappingId + '" /></td>';
                    tblBody += '<td style="width:3%;"><div class="checkbox checkbox-primary">';
                    tblBody += '</div></td>';
                    $.each(data["Question"], function (i, verHead) {
                        if (valHead.IsLock == "True") {
                            tblBody += '<td> <input type="text" disabled = "disabled"  class="form-control quetext"  id="txtObtainedMarks_' + i + '_' + valHead.CourseRegistrationDetailsId + '" onkeypress="return isNumberKey(event)" maxLength="4" /><input type="hidden" id="hdfPaperQuestionsId" value="' + verHead.PaperQuestionsId + '" /></td>';
                        }
                        else {
                            tblBody += '<td> <input type="text"  class="form-control quetext"  id="txtObtainedMarks_' + i + '_' + valHead.CourseRegistrationDetailsId + '" onkeypress="return isNumberKey(event)" maxLength="4" /><input type="hidden" id="hdfPaperQuestionsId" value="' + verHead.PaperQuestionsId + '" /></td>';
                        }
                    });
                    tblBody += '<td><input type="text" class="form-control MarksTotCls" id="txtToT_' + valHead.CourseRegistrationDetailsId + '" disabled="disabled"/></td>';
                    tblBody += '</tr>';
                });
            }
            tblBody += '</tbody>';
            tblMapping += tblBody;

            $('#tblStudentMarksEntry').append(tblMapping);
            markstable = $('#tblStudentMarksEntry').dataTable({
                destroy: true,
                scrollX: true,
                scrollY: "450px",
                paging: false,
                dom: 'Bfrtip',
                bSort: false,
                autoWidth: true,
                select: {
                    style: 'multi'
                },
            });


            $(".loader-area, .loader").fadeOut('slow');

        };

        /// Export Student Data Into Excel-19/09/2019
        $("#btnDownloadExcel").click(function () {
            //debugger;
            var postData = {
                QuestionPaperId: $('#ctl00_ContentPlaceHolder1_ddlExamName').val(),
                SchemeSubjectId: $("#ctl00_ContentPlaceHolder1_hdfSubjectId").val(),
                SessionId: $("#ctl00_ContentPlaceHolder1_ddlSession").val(),
                SchemeMappingName: ($('#ctl00_ContentPlaceHolder1_ddlExamName option:selected').text()),
                SectionId: $('#hdfSectionId').val()                                     // Added on 30-01-2020
            };
            DownloadMarksEntryStudentDataIntoExcel(postData);
        });

        $('#ctl00_ContentPlaceHolder1_FileUpload1').change(function () {
            var input = this;
            if (input.files && input.files[0]) {
                $("#btnUpload1").removeProp("disabled");
                var file = $('#ctl00_ContentPlaceHolder1_FileUpload1').val();
                if ((/\.(xlsx|xls)$/i).test(file)) {
                    return true;
                }
                else {
                    alert('Please Select valid Excel file(e.g. xls,xlsx)!');
                    $('#ctl00_ContentPlaceHolder1_FileUpload1').val('').clone(true);
                    return false;
                }
            }
        });

        $('#btnupload').click(function () {
            if (window.FormData !== undefined) {
                $("#btnupload").prop("disabled", true);
                $("#btnupload").val("Processing...");
                var fileupload = $("#FileUpload1");
                //If your file Is Empty :     
                if (fileupload.val() == '') {
                    alert("Please Attach File.");
                    // toastr.warning("Please Attach File.", 'Warning!');
                    $("#btnupload").prop("disabled", false).val("Upload");
                    return false;
                }
                var file = fileupload.get(0);
                var excelfile = file.files;
                // Create FormData object  
                var fileData = new FormData();
                // Looping over all files and add it to FormData object  
                for (var i = 0; i < excelfile.length; i++) {
                    fileData.append('file', excelfile[i]);
                }
                fileData.append('QuestionPaperId', $('#ctl00_ContentPlaceHolder1_ddlExamName').val());
                fileData.append('SchemeSubjectId', $('#hdfSubjectId').val());
                fileData.append('SessionId', $('#ctl00_ContentPlaceHolder1_ddlSession').val());

                $.ajax({
                    url: '/MarksEntry/Import',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data 
                    data: fileData,
                    success: function (data) {
                        //done well
                        alert("Marks entry excel file imported successfully.")
                        // toastr.success('Marks entry excel file imported succesfully.', 'Congratulations!');
                        BindMarksEntryStudentData();
                        $('#FileUpload1').val('').clone(true);
                        $("#btnupload").prop("disabled", false);
                    },
                    error: function (err) {
                        $("#btnupload").prop("disabled", false);
                        alert("File can not imported.");
                        //toastr.error("File can not imported.");
                    }
                });
            } else {
                alert("FormData is not supported.");
                //toastr.error("FormData is not supported.");
            }
        });


        //return {
        //    Init: init,
        //    LoadCurriculumDataData: LoadCurriculumDataData,
        //    BindExamNameDropDownLists: BindExamNameDropDownLists,
        //    BindMarksEntryStudentData: BindMarksEntryStudentData
        //}
        //}();



        function isNumberKey(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                var regex = new RegExp(/\./g)
                var count = $(item).val().match(regex).length;
                if (count > 1) {
                    return false;
                }
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function checkmethod(PaperQuestionsId, StudentId, CourseRegistrationDetailsId, StudentsMult) {
            var value = 0;
            $.each(StudentsMult, function (i, StudentsMultDetails) {
                if (StudentsMultDetails.PaperQuestionsId == PaperQuestionsId && StudentsMultDetails.StudentId == StudentId
                    && StudentsMultDetails.CourseRegistrationDetailsId == CourseRegistrationDetailsId) {
                    value = StudentsMultDetails.Que_marks;
                    return true;
                }
            });

            return value;
        };

        function DownloadMarksEntryStudentDataIntoExcel(Data) {
            $(".loader-area, .loader").css("display", "block");
            $.ajax({
                url: "/MarksEntry/DownloadExcel/",
                type: "POST",
                data: Data,
                success: function (data) {
                    // ;
                    if (data != null && (data.errorMessage == null || data.errorMessage === "")) {

                        // Get the file name for download
                        if (data.fileName != "") {
                            // use window.location.href for redirect to download action for download the file
                            var fileName = data.fileName;
                            var fName = fileName.replace('&', '$');
                            window.location.href = "Download/?file=" + fName;
                        }
                    } else {
                        alert("An error ocurred", data.errorMessage);
                    }
                    $(".loader-area, .loader").fadeOut('slow');
                },
                error: function (err) {
                    $('.alert-danger').css('display', 'block');
                    $('.alert-success').css('display', 'none');
                    $(".loader-area, .loader").fadeOut('slow');
                }
            });
        };

        //var saveData =
        function saveData(MarkEntryList, TotMarksList, LockStatus) {
            // alert('saveData');
            debugger;

            var Obj = {};
            Obj.MarkEntryList = JSON.stringify(MarkEntryList);
            Obj.TotMarksList = JSON.stringify(TotMarksList);
            Obj.userno = $('#ctl00_ContentPlaceHolder1_hdnUserno').val();
            Obj.LockStatus = LockStatus;
            Obj.ExamNameId = $('#ctl00_ContentPlaceHolder1_ddlExamName').val();
            Obj.IpAddress = $('#ctl00_ContentPlaceHolder1_hdnIpAddress').val();
            Obj.Flag = 1;
            Obj.ElectType = $('#ctl00_ContentPlaceHolder1_hdnElectType').val();
            Obj.Ccode = $('#ctl00_ContentPlaceHolder1_hdnCcode').val();
            Obj.schemeno = $('#ctl00_ContentPlaceHolder1_hdfSchemeTest').val();

            $.ajax({
                //url: "/QuestionWiseMarksEntry.aspx/SaveMarkEntry",
                url: "<%=Page.ResolveClientUrl("QuestionWiseMarksEntry_Global.aspx/SaveMarkEntry")%>",
               //QuestionWiseMarksEntry.aspx/SaveMarkEntry
               type: "POST",
               data: JSON.stringify(Obj),
               dataType: "json",
               contentType: "application/json;charset=utf-8",

               success: function (data) {
                   //alert('Abhi');
                   var data = data['d'];
                   CallBack(data);

                   $(".loader-area, .loader").fadeOut('slow');
               },
               error: function (errResponse) {
                   //alert(errResponse.d);
                   console.log(errResponse);
                   $(".loader-area, .loader").fadeOut('slow');
               }
           });


           $(".loader-area, .loader").fadeOut('slow');
       };

        //var CallBack =

        function CallBack(data) {

            // alert(CallBack);
            var value = parseInt(data);
            switch (value) {
                //Exception 
                case -1:
                    alert('Exception Occurred, Please Contact To Administrator!');
                    break;
                    //already exists
                case -2:
                    alert('Record already Exists!', 'Warning!');
                    break;
                    //saved
                case 1:
                    alert('Mark Entry Saved successfully.', 'Congratulations!');
                    __doPostBack('ddlExamName');
                    break;
                    //updated   
                case 2:
                    alert('Mark Entry Lock successfully.', 'Congratulations!');
                    __doPostBack('ddlExamName');
                    break;
                    //not saved
                case 3:
                    alert('Mark Entry UnLock successfully.', 'Congratulations!');
                    __doPostBack('ddlExamName');
                    break;
                default:
                    alert('Record Not Saved,Please Try Again.', 'Warning!');
                    break;
            }
        };

        function ClearData() {
            $("#tblStudentMarksEntry").empty();
            $("#tblSubject").empty();
            $("#ctl00_ContentPlaceHolder1_btnOperation").css("display", "none");
            $("#ctl00_ContentPlaceHolder1_divsubmarks").css("display", "none");
            $("#hdfCourseRegistrationDetailsIdId").val(0);
            $("#txtsubmarks").empty();
            $("#pnlSubjectDetail").css("display", "none");
            $("#ctl00_ContentPlaceHolder1_divExcelExport").css("display", "none");
            $("#pnlStudentMarksEntry").css("display", "none");
            $("#pnltblStudentMarksEntry").css("display", "none");
            //$("#ctl00_ContentPlaceHolder1_ddlSession").val(0);
            $("#hdfSubjectId").val(0);
            $('#hdfSectionId').val(0);                                  // Added on 30-01-2020
            $("#ctl00_ContentPlaceHolder1_ddlExamName").val(0);
            $("#IsLock").prop("disabled", false);
            $("#btnAdd").prop("disabled", false);
            $("#btnupload").prop("disabled", false);                    // Added on 24-04-2020
            $("#ctl00_ContentPlaceHolder1_lblQuestionDetails").css("display", "none");

        }

        function ClearSub() {
            $("#tblStudentMarksEntry").empty();
            $("#hdfCourseRegistrationDetailsIdId").val(0);
            $("#txtsubmarks").empty();
        }


    </script>

</asp:Content>

