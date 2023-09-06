//======================================================================================
// PROJECT NAME  : RFCAMPUS OBE                                                               
// MODULE NAME   : OBE                                                             
// PAGE NAME     : MarksEntry.Cs Controller,MarksEntryModel.cs Model,MarksEntry.Js Page,MarksEntry.cshtml View Page.                                        
// CREATION DATE : 01-07-2021                                                      
// CREATED BY    : Deepali Giri                                                
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
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

$('#ctl00_ContentPlaceHolder1_btnSubmit').click(function () {
    //alert('hiihuvjbh');
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
    $("#tblStudentMarksEntry TBODY TR").each(function () {
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
                if (quemarks == "" || quemarks == null) {

                    $(this).find("input[type=text]").addClass('focus');
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
            if (TotLeng > 0) {
                totalMarks = ($(this).find("input[type=text]").val());
                if (totalMarks == "" || totalMarks == null) {
                    //mrcount++;
                    //alert("Mark entry mandatory for all questions");
                    //$(this).find("input[type=text]").addClass('focus');
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

        saveData(MarkEntryList, TotMarksList, LockStatus);
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
                alert("Marks entry excel file imported succesfully.")
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

    $.ajax({
        url: "QuestionWiseMarksEntry.aspx/SaveMarkEntry",
        type: "POST",
        data: JSON.stringify(Obj),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {

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
            alert('Mark Entry Saved Succesfully.', 'Congratulations!');
            __doPostBack('ddlExamName');
            break;
            //updated   
        case 2:
            alert('Mark Entry Lock Succesfully.', 'Congratulations!');
            __doPostBack('ddlExamName');
            break;
            //not saved  
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

