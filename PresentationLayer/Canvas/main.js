const orgId = 9;
const collegeId = 0;
const apiURL = 'https://api.mastersofterp.in/RFC/CANVASLMS';
// const apiURL = 'https://api.mastersofterp.in/OBECanvas/OBECanvas';
const ipAddress = '127.0.0.1';
const macAddress = 'AA:BB:CC:DD:EE';
const userId = Number(554455);
let CanvasOrgTypeIdArr = [];
const uniqueSectionArray = [];
var studentListArray = [];
var FacultyListArray = [];
const Canvas_instance_creation_id = 1;


document.addEventListener("DOMContentLoaded", () => {
    getRefreshToken();
    $('.select2').select2();

    fetch(`${apiURL}/getCanvasId/${orgId}/${collegeId}`)
        .then(response => response.json())
        .then(data => {
            if (data.length > 0) {
                const Canvas_instance_creation_id = data[0].id;
             
                //Tab 0 Fields
                let ddlLMSNameTab0Id = '#ddlLMSNameTab0'
                let ddlLMSNameTab0 = document.querySelector(ddlLMSNameTab0Id);
                let ddlRFCollegeNameTab0Id = '#ddlRFCollegeNameTab0'
                let ddlRFCollegeNameTab0 = document.querySelector(ddlRFCollegeNameTab0Id);

                let ddlCanvasCollegeNameTab1Id = '#ddlCanvasCollegeNameTab1'
                let ddlCanvasCollegeNameTab1 = document.querySelector(ddlCanvasCollegeNameTab1Id);

                let tblCollegeMapping = document.querySelector('#tblCollegeMapping');

                let btnSubmitColleges = document.querySelector('#btnSubmitColleges');
                let btnClearColleges = document.querySelector('#btnClearColleges');
                let btnSubmitSession = document.querySelector('#btnSubmitSession');
                let btnSubmitCurriculum = document.querySelector('#btnSubmitCurriculum');
                let btnSubmitCourse = document.querySelector('#btnSubmitCourse');
                let btnSubmitUser = document.querySelector('#btnSubmitUser');

                var rfCollegeNameSelect = document.getElementById("ddlRFCollegeNameTab1");
                var ddlCanvasCollegeNameTab2 = document.getElementById("ddlCanvasCollegeNameTab2");
                var ddlRFCollegeNameTab2 = document.getElementById("ddlRFCollegeNameTab2");

                var ddlCanvasCollegeNameTab3 = document.getElementById("ddlCanvasCollegeNameTab3");
                var ddlCanvasSessionTab3 = document.getElementById("ddlCanvasSessionTab3");
                var ddlRFCollegeNameTab3 = document.getElementById("ddlRFCollegeNameTab3");
                var ddlRFCurriculumTab3 = document.getElementById("ddlRFCurriculumTab3");

                var ddlCanvasCollegeNameTab4 = document.getElementById("ddlCanvasCollegeNameTab4");
                var ddlCanvasSessionTab4 = document.getElementById("ddlCanvasSessionTab4");
                var ddlCanvasCurriculumTab4 = document.getElementById("ddlCanvasCurriculumTab4");
                var ddlRFCollegeNameTab4 = document.getElementById("ddlRFCollegeNameTab4");
                var ddlRFCurriculumTab4 = document.getElementById("ddlRFCurriculumTab4");
                var ddlCanvasCourseTab4 = document.getElementById("ddlCanvasCourseTab4");
                var ddlRFCourseNameTab4 = document.getElementById("ddlRFCourseNameTab4");
                var ddlRfSectionTab4 = document.getElementById("ddlRfSectionTab4");

                //tab 2
                // function on change rf college name
                $("#ddlRFCollegeNameTab1").on("change", function () {
                    var selectedOptions = Array.from(rfCollegeNameSelect.selectedOptions).map(function (option) {

                        return option.value;
                    });
              
                    getRfFilteredSessionName(selectedOptions[0], ddlRFSessionNameTab1);
                    
                })

                //tab 3
                // function on change canvas college name
                $("#ddlCanvasCollegeNameTab2").on("change", function () {

                    var selectedOptions = Array.from(ddlCanvasCollegeNameTab2.selectedOptions).map(function (option) {

                        return option.value;
                    });
                 
                    getCanvasSessionName(selectedOptions[0], ddlCanvasSessionNameTab2)
                })

                // function on change canvas college name
                $("#ddlRFCollegeNameTab2").on("change", function () {

                    var selectedOptions = Array.from(ddlRFCollegeNameTab2.selectedOptions).map(function (option) {

                        return option.value;
                    });
                   
                    getRfFilteredCurriculumName(selectedOptions[0], ddlRFCurriculumTab2)
                })

                //tab 4
                // function on change canvas college name
                $("#ddlCanvasCollegeNameTab3").on("change", function () {

                    var selectedOptions = Array.from(ddlCanvasCollegeNameTab3.selectedOptions).map(function (option) {

                        return option.value;
                    });
                   
                    getCanvasSessionName(selectedOptions[0], ddlCanvasSessionTab3)
                })
                // function on change canvas session name
                $("#ddlCanvasSessionTab3").on("change", function () {

                    var selectedOptions = Array.from(ddlCanvasSessionTab3.selectedOptions).map(function (option) {

                        return option.value;
                    });
                   
                    getCanvasCurriculumName(selectedOptions[0], ddlCanvasCurriculumTab3)
                })


                // function on change rf college name
                $("#ddlRFCollegeNameTab3").on("change", function () {

                    var selectedOptions = Array.from(ddlRFCollegeNameTab3.selectedOptions).map(function (option) {

                        return option.value;
                    });
                   
                    getRfSessionName(selectedOptions[0], ddlRFSessionTab3)
                    getRfCurriculumName(selectedOptions[0], ddlRFCurriculumTab3)
                })

                // function on change rf curriculum name
                $("#ddlRFCurriculumTab3").on("change", function () {

                    const rfCollegeId = $(ddlRFCollegeNameTab3).val();
                    const rfSessionId = $(ddlRFSessionTab3).val();

                    var selectedOptions = Array.from(ddlRFCurriculumTab3.selectedOptions).map(function (option) {

                        return option.value;
                    });
                   
                    getRfFilteredCourseName(rfCollegeId, rfSessionId, selectedOptions[0], ddlRFCourseNameTab3)
                })

                //tab 5
                // function on change canvas college name
                $("#ddlCanvasCollegeNameTab4").on("change", function () {

                    var selectedOptions = Array.from(ddlCanvasCollegeNameTab4.selectedOptions).map(function (option) {

                        return option.value;
                    });
               
                    getCanvasSessionName(selectedOptions[0], ddlCanvasSessionTab4)
                })
                // function on change canvas session name
                $("#ddlCanvasSessionTab4").on("change", function () {

                    var selectedOptions = Array.from(ddlCanvasSessionTab4.selectedOptions).map(function (option) {

                        return option.value;
                    });
               
                    getCanvasCurriculumName(selectedOptions[0], ddlCanvasCurriculumTab4)
                })

                // function on change canvas curriculum name
                $("#ddlCanvasCurriculumTab4").on("change", function () {

                    var selectedOptions = Array.from(ddlCanvasCurriculumTab4.selectedOptions).map(function (option) {

                        return option.value;
                    });
                  
                    getCanvasCourseName(selectedOptions[0], ddlCanvasCourseTab4)
                })

                // function on change rf college name
                $("#ddlRFCollegeNameTab4").on("change", function () {

                    var selectedOptions = Array.from(ddlRFCollegeNameTab4.selectedOptions).map(function (option) {

                        return option.value;
                    });
                 

                    getRfSessionName(selectedOptions[0], ddlRFSessionTab4)
                    getRfCurriculumName(selectedOptions[0], ddlRFCurriculumTab4)
                })

                // function on change rf curriculum name
                $("#ddlRFCurriculumTab4").on("change", function () {

                    const rfCollegeId = $(ddlRFCollegeNameTab4).val();
                    const rfSessionId = $(ddlRFSessionTab4).val();

                    var selectedOptions = Array.from(ddlRFCurriculumTab4.selectedOptions).map(function (option) {

                        return option.value;
                    });
                  
                    getRfCourseName(rfCollegeId, rfSessionId, selectedOptions[0], ddlRFCourseNameTab4)
                })

                // function on change Rf course name
                $("#ddlRFCourseNameTab4").on("change", function () {

                    var selectedOptions = Array.from(ddlRFCourseNameTab4.selectedOptions).map(function (option) {

                        return option.value;
                    });

                    getSectionName(selectedOptions[0], ddlRfSectionTab4)
                })
                // function on change canvas course name
                $("#ddlCanvasCourseTab4").on("change", function () {

                    var selectedOptions = Array.from(ddlCanvasCourseTab4.selectedOptions).map(function (option) {

                        return option.value;
                    });
                   
                    getCanvasSectionName(selectedOptions[0], ddlCanvasSectionTab4)
                })
                //
                $("#ddlRfSectionTab4").on("change", function () {

                    var selectedOptions = Array.from(ddlRfSectionTab4.selectedOptions).map(function (option) {

                        return option.value;
                    });
                 
                    getRfStudentName(selectedOptions[0], studentList)
                    getRFTeacherNames(selectedOptions[0], studentList)
                })



                //Common Calls
                /**
                 * TODO: Initialize all the tabs individually with their tables
                 */
                // Tab 1
                getCollegeNames([ddlRFCollegeNameTab1, ddlRFCollegeNameTab2, ddlRFCollegeNameTab3, ddlRFCollegeNameTab4])

                getCollegeFilteredNames([ddlRFCollegeNameTab0Id])

                fetchCanvasInstanceValues(ddlLMSNameTab0Id);

                //Tab 2
                getCanvasCollegeName([ddlCanvasCollegeNameTab1Id, ddlCanvasCollegeNameTab2, ddlCanvasCollegeNameTab3, ddlCanvasCollegeNameTab4])

                const tblTab0 = document.querySelector('#tblTab0');
                const tblTab1 = document.querySelector('#tblTab1');
                const tblTab2 = document.querySelector('#tblTab2');
                const tblTab3 = document.querySelector('#tblTab3');
                const tblTab4 = document.querySelector('#tblTab4');
                populateTab0TableData(tblTab0);
                populateTab1TableData(tblTab1);
                populateTab2TableData(tblTab2);
                populateTab3TableData(tblTab3);
                populateTab4TableData(tblTab4);
                /**
                 * TODO: Initialize submit buttons individually with different routes and tabs
                 */
                //Tab 0 Submit Button 
                btnSubmitColleges.addEventListener('click', () => {
                    
                    let postMsg = true;
                    const lmsValue = $(ddlLMSNameTab0Id).val();
                    if (lmsValue <= 0) {
                        iziToast.error({ message: `LMS Name cannot be empty.` });
                        lmsName.focus();
                        postMsg = false;
                        return;
                    }
                    const collegeValue = $(ddlRFCollegeNameTab0Id).val();
                  
                    if (collegeValue <= 0) {
                        iziToast.error({ message: `Select at-least 1 College name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }
                    if (postMsg) {
                     //   buttonStateDisabled(btnSubmitDepartment, 'Submit');

                        const selectedValues = $(ddlRFCollegeNameTab0Id).select2('data');

                        selectedValues.forEach(value => {
                            if (value.id > 0) {
                                const packet = {
                                    "Parents": [Number(lmsValue)],
                                    "CanvasCollegeId": Number(lmsValue),
                                    "ipAddress": ipAddress,
                                    "orgId": orgId,
                                    "macAddress": macAddress,
                                    "createdBy": userId,
                                    "collegeId": collegeId,
                                    "Name": value.text,
                                    "Code": value.id
                                }
                            
                                fetch(`${apiURL}/createCollegeType/${Canvas_instance_creation_id}`, {
                                    method: 'POST',
                                    headers: {
                                        'Content-Type': 'application/json'
                                    },
                                    body: JSON.stringify(packet)
                                })
                                    .then(response => response.json())
                                    .then(data => {
                                       
                                        iziToast.success({ message: `Record inserted successfully.` })

                                        getCollegeNames([ ddlRFCollegeNameTab1, ddlRFCollegeNameTab2, ddlRFCollegeNameTab3])
                                        getCollegeFilteredNames([ddlRFCollegeNameTab0Id])
                                        fetchCanvasInstanceValues(ddlLMSNameTab0Id);
                                        populateTab0TableData(tblTab0);
                                    })
                                    .catch((error) => {
                                      
                                        iziToast.error({ message: error });
                                    });
                            }
                        });
                    }
                });

                //tab 2
                btnSubmitSession.addEventListener('click', () => {
                  
                    let postMsg = true;
                    const canvasCollegeId = $(ddlCanvasCollegeNameTab1).val();
                    if (canvasCollegeId <= 0) {
                        iziToast.error({ message: `LMS Name cannot be empty.` });
                        lmsName.focus();
                        postMsg = false;
                        return;
                    }
                    const rfCollegeId = $(ddlRFCollegeNameTab1).val();
                    if (rfCollegeId <= 0) {
                        iziToast.error({ message: `Select at-least 1 RF College name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    const rfSessionId = $(ddlRFSessionNameTab1).val();
                    if (rfSessionId <= 0) {
                        iziToast.error({ message: `Select at-least 1 Session name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    if (postMsg) {
                        buttonStateDisabled(btnSubmitSession, 'Submit');

                        const selectedValues = $(ddlRFSessionNameTab1).select2('data');

                        selectedValues.forEach(value => {
                            if (value.id > 0) {
                                const packet = {
                                    "SessionId": value.id,
                                    "Code": Number(rfCollegeId),
                                    "Name": value.text,

                                    "ipAddress": ipAddress,
                                    "orgId": orgId,
                                    "macAddress": macAddress,
                                    "createdBy": userId,
                                    "collegeId": canvasCollegeId,
                                }

                                fetch(`${apiURL}/createSessionInCollege/1`, {
                                    method: 'POST',
                                    headers: {
                                        'Content-Type': 'application/json'
                                    },
                                    body: JSON.stringify(packet)
                                })
                                    .then(response => response.json())
                                    .then(data => {

                                       
                                        iziToast.success({ message: `Record inserted successfully.` })
                                        getCanvasCollegeName([ddlCanvasCollegeNameTab1Id, ddlCanvasCollegeNameTab2, ddlCanvasCollegeNameTab3, ddlCanvasCollegeNameTab4])
                                        populateTab1TableData(tblTab1);
                                    })
                                    .catch((error) => {
                                    
                                        iziToast.error({ message: error });
                                    });
                            }
                        });
                    }
                });

                //tab 3
                btnSubmitCurriculum.addEventListener('click', () => {
                    
                    let postMsg = true;
                    const canvasCollegeId = $(ddlCanvasCollegeNameTab2).val();
                    if (canvasCollegeId <= 0) {
                        iziToast.error({ message: `canvas college Name cannot be empty.` });
                        lmsName.focus();
                        postMsg = false;
                        return;
                    }
                    const canvasSessionId = $(ddlCanvasSessionNameTab2).val();
                    if (canvasSessionId <= 0) {
                        iziToast.error({ message: `canvas session Name cannot be empty.` });
                        lmsName.focus();
                        postMsg = false;
                        return;
                    }
                    const rfCollegeId = $(ddlRFCollegeNameTab2).val();
                    if (rfCollegeId <= 0) {
                        iziToast.error({ message: `Select at-least 1 RF College name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    const CurriculumId = $(ddlRFCurriculumTab2).val();
                    if (CurriculumId <= 0) {
                        iziToast.error({ message: `Select at-least 1 curriculum session name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    if (postMsg) {
                        buttonStateDisabled(btnSubmitSession, 'Submit');

                        const selectedValues = $(ddlRFCurriculumTab2).select2('data');

                        selectedValues.forEach(value => {
                            if (value.id > 0) {
                                const packet = {
                                    "canvas_session_id": canvasSessionId,
                                    "Code": Number(rfCollegeId),
                                    "CurriculumId": value.id,
                                    "Name": value.text,
                                    "canvas_college_id": canvasCollegeId,

                                    "ipAddress": ipAddress,
                                    "orgId": orgId,
                                    "macAddress": macAddress,
                                    "createdBy": userId,
                                    "collegeId": canvasCollegeId,
                                }


                                fetch(`${apiURL}/createCurriculumInCollege/1`, {
                                    method: 'POST',
                                    headers: {
                                        'Content-Type': 'application/json'
                                    },
                                    body: JSON.stringify(packet)
                                })
                                    .then(response => response.json())
                                    .then(data => {

                                        iziToast.success({ message: `Record inserted successfully.` })
                                        getCanvasCollegeName([ddlCanvasCollegeNameTab1Id, ddlCanvasCollegeNameTab2, ddlCanvasCollegeNameTab3, ddlCanvasCollegeNameTab4])
                                        populateTab2TableData(tblTab2);
                                    })
                                    .catch((error) => {
                                 
                                        iziToast.error({ message: error });
                                    });
                            }
                        });
                    }
                });

                //tab 4
                btnSubmitCourse.addEventListener('click', () => {
                 
                    let postMsg = true;
                    const canvasCollegeId = $(ddlCanvasCollegeNameTab3).val();
                    if (canvasCollegeId <= 0) {
                        iziToast.error({ message: `canvas college Name cannot be empty.` });
                        lmsName.focus();
                        postMsg = false;
                        return;
                    }
                    const canvasSessionId = $(ddlCanvasSessionTab3).val();
                    if (canvasSessionId <= 0) {
                        iziToast.error({ message: `canvas session Name cannot be empty.` });
                        lmsName.focus();
                        postMsg = false;
                        return;
                    }
                    const rfCollegeId = $(ddlRFCollegeNameTab3).val();
                    if (rfCollegeId <= 0) {
                        iziToast.error({ message: `Select at-least 1 RF College name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }
                    const rfSessionId = $(ddlRFSessionTab3).val();
                    if (rfSessionId <= 0) {
                        iziToast.error({ message: `Select at-least 1 RF College name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    const canvasCurriculumId = $(ddlCanvasCurriculumTab3).val();
                    if (canvasCurriculumId <= 0) {
                        iziToast.error({ message: `Select at-least 1 curriculum session name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }
                    const rfCurriculumId = $(ddlRFCurriculumTab3).val();
                    if (rfCurriculumId <= 0) {
                        iziToast.error({ message: `Select at-least 1 curriculum session name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    if (postMsg) {
                        buttonStateDisabled(btnSubmitSession, 'Submit');

                        const selectedValues = $(ddlRFCourseNameTab3).select2('data');

                        selectedValues.forEach(value => {
                            if (value.id > 0) {
                               
                                const split = value.text.split(" - ")
                                const courseName = split[1]
                                const courseCode = split[0]

                                const section = uniqueSectionArray.find((element) => element.course_no === parseInt(value.id));
                             

                                const packet = {
                                    "Canvas_instance_creation_id": Canvas_instance_creation_id,
                                    "canvas_session_id": canvasSessionId,
                                    "canvas_college_id": canvasCollegeId,
                                    "canvas_curriculum_id": canvasCurriculumId,
                                    "rf_college_id": Number(rfCollegeId),
                                    "rf_session_id": rfSessionId,
                                    "rf_curriculum_id": rfCurriculumId,
                                    "rf_course_id": value.id,
                                    "course_name": courseName,
                                    "course_code": courseCode,
                                    "sectionArray": section,
                                    "ipAddress": ipAddress,
                                    "orgId": orgId,
                                    "macAddress": macAddress,
                                    "createdBy": userId,
                                    "collegeId": canvasCollegeId,
                                }

                          
                                fetch(`${apiURL}/createCourseInCollege/${Canvas_instance_creation_id}`, {
                                    method: 'POST',
                                    headers: {
                                        'Content-Type': 'application/json'
                                    },
                                    body: JSON.stringify(packet)
                                })
                                    .then(response => response.json())
                                    .then(data => {

                                  
                                        iziToast.success({ message: `Record inserted successfully.` })
                                        //getCanvasCollegeName([ddlCanvasCollegeNameTab1Id, ddlCanvasCollegeNameTab2, ddlCanvasCollegeNameTab3, ddlCanvasCollegeNameTab4])
                                        populateTab3TableData(tblTab3);

                                    })
                                    .catch((error) => {
                                       
                                        iziToast.error({ message: error });
                                    });
                            }
                        });
                    }
                });

                //tab 5
                btnSubmitUser.addEventListener('click', () => {
                
                    let postMsg = true;
                    const canvasCollegeId = $(ddlCanvasCollegeNameTab4).val();
                    if (canvasCollegeId <= 0) {
                        iziToast.error({ message: `canvas college Name cannot be empty.` });

                        postMsg = false;
                        return;
                    }
                    const canvasSessionId = $(ddlCanvasSessionTab4).val();
                    if (canvasSessionId <= 0) {
                        iziToast.error({ message: `canvas session Name cannot be empty.` });

                        postMsg = false;
                        return;
                    }


                    const canvasCurriculumId = $(ddlCanvasCurriculumTab4).val();
                    if (canvasCurriculumId <= 0) {
                        iziToast.error({ message: `Select at-least 1 curriculum session name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    const canvasCourseId = $(ddlCanvasCourseTab4).val();
                    if (canvasCurriculumId <= 0) {
                        iziToast.error({ message: `Select at-least 1 canvas course name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    const canvasSectionId = $(ddlCanvasSectionTab4).val();
                    if (canvasCurriculumId <= 0) {
                        iziToast.error({ message: `Select at-least 1 canvas section name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }
                    const rfCollegeId = $(ddlRFCollegeNameTab4).val();
                    if (rfCollegeId <= 0) {
                        iziToast.error({ message: `Select at-least 1 RF College name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }
                    const rfSessionId = $(ddlRFSessionTab4).val();
                    if (rfSessionId <= 0) {
                        iziToast.error({ message: `Select at-least 1 RF College name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    const rfCurriculumId = $(ddlRFCurriculumTab4).val();
                    if (rfCurriculumId <= 0) {
                        iziToast.error({ message: `Select at-least 1 curriculum session name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }
                    const rfCourseId = $(ddlRFCourseNameTab4).val();
                    if (rfCurriculumId <= 0) {
                        iziToast.error({ message: `Select at-least 1 Rf course name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }
                    const rfSectionId = $(ddlRfSectionTab4).val();
                    if (rfCurriculumId <= 0) {
                        iziToast.error({ message: `Select at-least 1 Rf section name.` });
                        ddlDegProgMap.focus();
                        postMsg = false;
                        return;
                    }

                    if (postMsg) {
                        buttonStateDisabled(btnSubmitSession, 'Submit');

                        //  const studentList = $(studentListArray).select2('data');


                        studentListArray.forEach(student => {

                            const packet = {
                                "Canvas_instance_creation_id": Canvas_instance_creation_id,
                                "canvas_session_id": canvasSessionId,
                                "canvas_college_id": canvasCollegeId,
                                "canvas_curriculum_id": canvasCurriculumId,
                                "canvas_course_id": canvasCourseId,
                                "canvas_section_id": canvasSectionId,
                                "rf_college_id": Number(rfCollegeId),
                                "rf_session_id": rfSessionId,
                                "rf_curriculum_id": rfCurriculumId,
                                "rf_course_id": rfCourseId,
                                "rf_section_id": rfSectionId,

                                "unique_id": student.LoginId,
                                "user_type": "StudentEnrollment",
                                "user_name": student.Student_Name,
                                "user_email": student.Student_Email,

                                "ipAddress": ipAddress,
                                "orgId": orgId,
                                "macAddress": macAddress,
                                "createdBy": userId,
                                "collegeId": canvasCollegeId,
                            }

                            fetch(`${apiURL}/createStudentInCollege/${Canvas_instance_creation_id}/${canvasCollegeId}`, {
                                method: 'POST',
                                headers: {
                                    'Content-Type': 'application/json'
                                },
                                body: JSON.stringify(packet)
                            })
                                .then(response => response.json())
                                .then(data => {

                                    iziToast.success({ message: `Record inserted successfully.` })
                                    //getCanvasCollegeName([ddlCanvasCollegeNameTab1Id, ddlCanvasCollegeNameTab2, ddlCanvasCollegeNameTab3, ddlCanvasCollegeNameTab4])
                                    // populateTab3TableData(tblTab3);

                                })
                                .catch((error) => {
            
                                    iziToast.error({ message: error });
                                });

                        });


                        FacultyListArray.forEach(faculty => {

                            const packet = {
                                "Canvas_instance_creation_id": Canvas_instance_creation_id,
                                "canvas_session_id": canvasSessionId,
                                "canvas_college_id": canvasCollegeId,
                                "canvas_curriculum_id": canvasCurriculumId,
                                "canvas_course_id": canvasCourseId,
                                "canvas_section_id": canvasSectionId,
                                "rf_college_id": Number(rfCollegeId),
                                "rf_session_id": rfSessionId,
                                "rf_curriculum_id": rfCurriculumId,
                                "rf_course_id": rfCourseId,
                                "rf_section_id": rfSectionId,

                                "unique_id": faculty.LoginId,
                                "user_type": "TeacherEnrollment",
                                "user_name": faculty.Faculty_Name,
                                "user_email": faculty.Faculty_Email,

                                "ipAddress": ipAddress,
                                "orgId": orgId,
                                "macAddress": macAddress,
                                "createdBy": userId,
                                "collegeId": canvasCollegeId,
                            }

                            fetch(`${apiURL}/createStudentInCollege/${Canvas_instance_creation_id}/${canvasCollegeId}`, {
                                method: 'POST',
                                headers: {
                                    'Content-Type': 'application/json'
                                },
                                body: JSON.stringify(packet)
                            })
                                .then(response => response.json())
                                .then(data => {

                                    iziToast.success({ message: `Record inserted successfully.` })
                                    //getCanvasCollegeName([ddlCanvasCollegeNameTab1Id, ddlCanvasCollegeNameTab2, ddlCanvasCollegeNameTab3, ddlCanvasCollegeNameTab4])
                                    // populateTab3TableData(tblTab3);

                                })
                                .catch((error) => {
                         
                                    iziToast.error({ message: error });
                                });

                        });


                    }
                });

                /**
                 * All Various functions and functionalities
                 */

                


                //function to get all the Canvas Instance values
                function fetchCanvasInstanceValues(element) {
                    fetch(`${apiURL}/getCanvasInstance/${orgId}/${collegeId}`)
                        .then(response => response.json())
                        .then(data => {
                       
                            RenderDropDown($(element), data);
                        })
                        .catch(error => console.error(error));
                }


                // function  to get all the colleges in rf campus
                function getCollegeNames(elementArr) {
                    fetch(`${apiURL}/getAllCollegeNames/${Canvas_instance_creation_id}`)
                        .then(response => response.json())
                        .then(data => {
                            const modifiedData = data.map(({ College_Id, College_Name, ...rest }) => ({ Value: College_Id, Text: College_Name, ...rest }));
                           
                            elementArr.forEach(element => {
                                RenderDropDown($(element), modifiedData);
                            });
                        })
                        .catch(error => console.error(error));
                }

                //  function to get canvas college name
                function getCanvasCollegeName(elementArr) {
                    fetch(`${apiURL}/getCanvasCollegeNames/1`)
                        .then(response => response.json())
                        .then(data => {
                          
                            const modifiedData = data.map(({ id, name, ...rest }) => ({ Value: id, Text: name, ...rest }));

                            elementArr.forEach(element => {
                                RenderDropDown($(element), modifiedData);
                            });
                        })
                        .catch(error => console.error(error));
                }

                //  function to get canvas session name
                function getCanvasSessionName(canvasCollegeId, element) {

                    fetch(`${apiURL}/getCanvasSessionNames/1/${canvasCollegeId}`)
                        .then(response => response.json())
                        .then(data => {
                           
                            const modifiedData = data.map(({ id, name, ...rest }) => ({ Value: id, Text: name, ...rest }));

                            RenderDropDown($(element), modifiedData);

                        })
                        .catch(error => console.error(error));
                }

                //function to get Rf session name
                function getRfSessionName(rfCollegeId, element) {

                    fetch(`${apiURL}/getRFSessionNames/${rfCollegeId}`)
                        .then(response => response.json())
                        .then(data => {
                          
                            const modifiedData = data.map(({ SESSIONID, Session_Name, ...rest }) => ({ Value: SESSIONID, Text: Session_Name, ...rest }));

                            RenderDropDown($(element), modifiedData);

                        })
                        .catch(error => console.error(error));
                }

                //function to get Rf curriculum name
                function getRfCurriculumName(rfCollegeId, element) {

                    fetch(`${apiURL}/getRFCurriculumNames/${rfCollegeId}`)
                        .then(response => response.json())
                        .then(data => {
                           
                            const modifiedData = data.map(({ Scheme_id, Scheme_Name, ...rest }) => ({ Value: Scheme_id, Text: Scheme_Name, ...rest }));
                            RenderDropDown($(element), modifiedData);
                        })
                        .catch(error => console.error(error));
                }

                //function to get Rf curriculum name
                function getCanvasCourseName(canvasCurriculumId, element) {

                    fetch(`${apiURL}/getCanvasCourses/${Canvas_instance_creation_id}/${canvasCurriculumId}`)
                        .then(response => response.json())
                        .then(data => {
                         
                            const modifiedData = data.map(({ id, name, ...rest }) => ({ Value: id, Text: name, ...rest }));
                            RenderDropDown($(element), modifiedData);
                        })
                        .catch(error => console.error(error));
                }

                //function to get canvas curriculum name
                function getCanvasCurriculumName(canvasSessionId, element) {

                    fetch(`${apiURL}/getCanvasCurriculums/${Canvas_instance_creation_id}/${canvasSessionId}`)
                        .then(response => response.json())
                        .then(data => {
                          
                            const modifiedData = data.map(({ id, name, ...rest }) => ({ Value: id, Text: name, ...rest }));
                            RenderDropDown($(element), modifiedData);
                        })
                        .catch(error => console.error(error));
                }

                //function to get canvas section name
                function getCanvasSectionName(courseId, element) {

                    fetch(`${apiURL}/getCanvasSections/${Canvas_instance_creation_id}/${courseId}`)
                        .then(response => response.json())
                        .then(data => {
                        
                            const modifiedData = data.map(({ id, name, ...rest }) => ({ Value: id, Text: name, ...rest }));
                            RenderDropDown($(element), modifiedData);
                        })
                        .catch(error => console.error(error));
                }

                //function to get rf course name
                function getRfCourseName(rfCollegeId, rfSessionName, canvasSessionId, element) {

                    fetch(`${apiURL}/getRFCourseNames/${rfCollegeId}/${rfSessionName}/${canvasSessionId}`)
                        .then(response => response.json())
                        .then(data => {
                         
                            data.forEach(item => {
                                const existingObj = uniqueSectionArray.find(obj => obj.course_name === item.Course_Name);

                                if (existingObj) {
                                    existingObj.section.push({ group_id: item.Group_id, group_name: item.Group_Name });
                                } else {
                                    uniqueSectionArray.push({
                                        course_name: item.Course_Name,
                                        course_no: item.Course_No,
                                        section: [{ group_id: item.Group_id, group_name: item.Group_Name }]
                                    });
                                }
                            });

                            const modifiedData = data.map(({ Course_No, Course_Name }) => ({ Value: Course_No, Text: Course_Name }));

                            const uniqueArray = [];
                            const textValue = new Set();

                            modifiedData.forEach(item => {
                                if (!textValue.has(item.Text)) {
                                    textValue.add(item.Text);
                                    uniqueArray.push(item);
                                }
                            });
                           
                            RenderDropDown($(element), uniqueArray);
                        })
                        .catch(error => console.error(error));
                }

                //function to get rf section name 
                function getSectionName(rfCourseId, element) {

                    uniqueSectionArray.forEach(section => {

                        if (section.course_no == rfCourseId) {
                           
                            const modifiedData = section.section.map(({ group_id, group_name }) => ({ Value: group_id, Text: group_name }));
                            RenderDropDown($(element), modifiedData);
                        }
                    })

                }

                //function to disable the button state
                function buttonStateDisabled(element, text) {
                    const width = element.offsetWidth;
                    element.style.width = `${width}px`;
                    element.disabled = true;
                    element.innerHTML = `<i class="fa fa-circle-notch fa-spin font-18 position-relative" style="top:2px;"></i>`;
                }

                //function to enable the button state
                function buttonStateOn(element, text) {
                    const width = element.offsetWidth;
                    element.style.width = `${width}px`;
                    element.disabled = false;
                    element.innerHTML = text;
                }

                //function to add data to table0
                function populateTab0TableData(table) {
                    fetch(`${apiURL}/getAllCollegeNames/${Canvas_instance_creation_id}`)
                        .then(response => response.json())
                        .then(data => {
                            $(table).bootstrapTable('destroy').bootstrapTable(bootstrapTableSettings(data));
                        })
                        .catch(error => console.error(error));
                }

                //function to add data to table0
                function populateTab1TableData(table) {
                    fetch(`${apiURL}/getAllSessionTableValues/${Canvas_instance_creation_id}`)
                        .then(response => response.json())
                        .then(data => {
                            $(table).bootstrapTable('destroy').bootstrapTable(bootstrapTableSettings(data));
                        })
                        .catch(error => console.error(error));
                }

                //function to add data to table0
                function populateTab2TableData(table) {
                    fetch(`${apiURL}/getAllCurriculumTableValues/${Canvas_instance_creation_id}`)
                        .then(response => response.json())
                        .then(data => {
                            $(table).bootstrapTable('destroy').bootstrapTable(bootstrapTableSettings(data));
                        })
                        .catch(error => console.error(error));
                }

                //function to add data to table0
                function populateTab3TableData(table) {
                    fetch(`${apiURL}/getAllCourseTableValues/${Canvas_instance_creation_id}`)
                        .then(response => response.json())
                        .then(data => {
                            $(table).bootstrapTable('destroy').bootstrapTable(bootstrapTableSettings(data));
                        })
                        .catch(error => console.error(error));
                }

                //function to add data to table0
                function populateTab4TableData(table) {
                    fetch(`${apiURL}/getAllUserTableValues/${Canvas_instance_creation_id}`)
                        .then(response => response.json())
                        .then(data => {
                            $(table).bootstrapTable('destroy').bootstrapTable(bootstrapTableSettings(data));
                        })
                        .catch(error => console.error(error));
                }

                //function to get rf student Name
                function getRfStudentName(rfSectionId, element) {

                    const rfCollegeId = $(ddlRFCollegeNameTab4).val();
                    const rfSessionId = $(ddlRFSessionTab4).val();
                    const rfCurriculumId = $(ddlRFCurriculumTab4).val();

                    fetch(`${apiURL}/getRFStudentNames/${rfCollegeId}/${rfSessionId}/${rfCurriculumId}/${rfSectionId}`)
                        .then(response => response.json())
                        .then(data => {
                            console.log(data)
                            studentListArray = data

                            if (data.length > 0) {
                                document.getElementById('studentList').innerHTML = ''
                            }
                            data.forEach(student => {
                                var li = document.createElement('li');
                                li.innerHTML = student.Student_Name
                                document.getElementById('studentList').appendChild(li);
                            })
                        })
                        .catch(error => console.error(error));
                }

                //function to get rf teacher Name
                function getRFTeacherNames(rfSectionId, element) {

                    const rfCollegeId = $(ddlRFCollegeNameTab4).val();
                    const rfSessionId = $(ddlRFSessionTab4).val();
                    const rfCurriculumId = $(ddlRFCurriculumTab4).val();

                    fetch(`${apiURL}/getRFTeacherNames/${rfCollegeId}/${rfSessionId}/${rfCurriculumId}/${rfSectionId}`)
                        .then(response => response.json())
                        .then(data => {
                            console.log(data)
                            FacultyListArray = data

                            if (data.length > 0) {
                                document.getElementById('teacherList').innerHTML = ''
                            }
                            data.forEach(teacher => {
                                var li = document.createElement('li');
                                li.innerHTML = teacher.Faculty_Name
                                document.getElementById('teacherList').appendChild(li);
                            })
                        })
                        .catch(error => console.error(error));
                }
            }

        })
        .catch(error => console.error(error));
});

 //function to refresh token 
 function getRefreshToken(){  

    fetch(`${apiURL}/refreshAccessToken/1`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
       
            if (response.status != 200 ) {
                location.reload() 
            }        
        })
        .then(data => {
         
        })
        .catch(error => console.error(error));
}

//filtered Function 

// function  to get all the colleges in rf campus
function getCollegeFilteredNames(elementArr) {
    fetch(`${apiURL}/getAllCollegeNamesFiltered/${Canvas_instance_creation_id}`)
        .then(response => response.json())
        .then(data => {
            const modifiedData = data.map(({ College_Id, College_Name, ...rest }) => ({ Value: College_Id, Text: College_Name, ...rest }));
       
            elementArr.forEach(element => {
                RenderDropDown($(element), modifiedData);
            });
        })
        .catch(error => console.error(error));
}

 //function to get Rf session name
 function getRfFilteredSessionName(rfCollegeId, element) {
    fetch(`${apiURL}/getRFSessionNamesFiltered/${rfCollegeId}`)
        .then(response => response.json())
        .then(data => {
           
            const modifiedData = data.map(({ SESSIONID, Session_Name, ...rest }) => ({ Value: SESSIONID, Text: Session_Name, ...rest }));

            RenderDropDown($(element), modifiedData);

        })
        .catch(error => console.error(error));
}


//function to get Rf curriculum name
function getRfFilteredCurriculumName(rfCollegeId, element) {

    fetch(`${apiURL}/getRFCurriculumNamesFiltered/${rfCollegeId}`)
        .then(response => response.json())
        .then(data => {
        
            const modifiedData = data.map(({ Scheme_id, Scheme_Name, ...rest }) => ({ Value: Scheme_id, Text: Scheme_Name, ...rest }));
            RenderDropDown($(element), modifiedData);
        })
        .catch(error => console.error(error));
}


 //function to get rf course name
 function getRfFilteredCourseName(rfCollegeId, rfSessionName, canvasSessionId, element) {

    fetch(`${apiURL}/getRFCourseNamesFiltered/${rfCollegeId}/${rfSessionName}/${canvasSessionId}`)
        .then(response => response.json())
        .then(data => {
           
            data.forEach(item => {
                const existingObj = uniqueSectionArray.find(obj => obj.course_name === item.Course_Name);

                if (existingObj) {
                    existingObj.section.push({ group_id: item.Group_id, group_name: item.Group_Name });
                } else {
                    uniqueSectionArray.push({
                        course_name: item.Course_Name,
                        course_no: item.Course_No,
                        section: [{ group_id: item.Group_id, group_name: item.Group_Name }]
                    });
                }
            });

            const modifiedData = data.map(({ Course_No, Course_Name }) => ({ Value: Course_No, Text: Course_Name }));

            const uniqueArray = [];
            const textValue = new Set();

            modifiedData.forEach(item => {
                if (!textValue.has(item.Text)) {
                    textValue.add(item.Text);
                    uniqueArray.push(item);
                }
            });
          
            RenderDropDown($(element), uniqueArray);
        })
        .catch(error => console.error(error));
}




/**
 * Bootstrap table formatters
 */
function quickFormTimeFormatter(value) {
    if (value == null) {
        return '-'
    }
    else {
        return convertDateTime(value, false)
    }
}

function CanvasTypeFormatter(value) {
    if (value == 101) {
        return `Department`
    }
    else if (value == 2) {
        return `Course Template`
    }
    else if (value == 3) {
        return `Course Offered`
    }
}


function CanvasRoleFormatter(value) {
    if (value == 109) {
        return `Instructor`
    }
    else if (value == 110) {
        return `Student`
    }
}



//function to convert the utcToLocalTime
function convertDateTime(dateString) {
    const date = new Date(dateString);

    if (date == 'Invalid Date') {
        return '-';
    }
    const options = {
        month: 'long',
        day: 'numeric',
        year: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
        hour12: true,
        timeZone: 'Asia/Kolkata'
    };
    return new Intl.DateTimeFormat('en-US', options).format(date);
}

function canvasCreationFormatter(value) {
    return "MS - SIMS Canvas"
}



//remove this
var RenderDropDown = function (control, data) {
    control.empty();
    if (data.length == 0) {
        control.prepend($("<option/>").val(-1).text("No records found."));
    }
    else {
        $.each(data, function () {
            control.append($("<option />").val(this.Value).text(this.Text));
        }); control.prepend($("<option selected/>").val(0).text("Please Select"));
    }
};

//$('#tblElectiveGroup').bootstrapTable('destroy').bootstrapTable(bootstrapTableSettings(data['data']));

// Common Bootstrap Table Script with Pagination
function bootstrapTableSettings(data) {
    const tableSettings = {
        destroy: true,
        search: true,
        pagination: true,
        sortable: true,
        showColumns: true,                  //Whether to display all columns (select the columns to display)
        showRefresh: true,
        showToggle: true,
        exportDataType: "all",
        minimumCountColumns: 2,
        filterControl: true,
        showExport: true,
        showColumnsToggleAll: true,
        filterControlVisible: false,
        //filterControlContainer: true,
        showFilterControlSwitch: false,
        showFilterControlSwitch: true,
        advancedSearch: true,
        advancedSearch: true,
        data: data,
        exportTypes: ['doc', 'excel'],
        pageList: "[10, 25, 50, 100, all]",
    }
    return tableSettings;
}



