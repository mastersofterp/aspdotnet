

function generateGreetings() {
    var currentHour = moment().format("HH");
    if (currentHour >= 3 && currentHour < 12) {
        return "Good Morning";
    } else if (currentHour >= 12 && currentHour < 15) {
        return "Good Afternoon";
    } else if (currentHour >= 15 && currentHour < 20) {
        return "Good Evening";
    } else if (currentHour >= 20 && currentHour < 3) {
        return "Good Night";
    } else {
        return "Hello";
    }
}

function welcomeIntro(userType) {
    var greeting = generateGreetings();
    return {
        title: 'Welcome',
        intro: `<b>${greeting}, ${userType}</b> to a guided tour of the <b>Mastersoft ERP Portal</b>!`,
       // element: document.querySelector('.introjs-card'),
    }
}

var mainPanelIntro = {
    title: 'Main Panel',
    element: document.querySelector('#ctl00_mainMenu'),
    intro: 'Use this panel to toggle between various menus.'
};
var consolidatedIntro = {
    title: 'Consolidated View',
    element: document.querySelector('.mybox-main'),
    intro: 'This is the specific analysis of <b>Total Students</b>, <b>Gender wise Student</b>, <b>Overall Active Users</b>'
}
var admissionYearIntro = {
    title: 'Admission Year',
    element: document.querySelector('.admissionYearCard'),
    intro: 'These is the specific analysis of <b> Admission Year wise Students</b>',
};

var generalAnalysisIntro = {
    title: 'General Analysis',
    element: document.querySelector('.generalAnalysis'),
    intro: 'These are the general specific analysis for your institute.',
};
var AcademicActivitiesIntro = {
    title: 'Academic Activities',
    element: document.querySelector('.academicActivities'),
    intro: 'These are the <b>Academic Activities</b> of your institute.',
};
var ActiveNoticeIntro = {
    title: 'Active Notice/News',
    element: document.querySelector('.activeNotice'),
    intro: 'These are the <b>Active Notice/News</b> of your institute.',
};
var themeIntro = {
    title: 'Change Theme',
    element: document.querySelector('#theme-setting'),
    intro: `You can use this panel to change the theme based on your preference.`,    
}
var infoIntro = {
    title: 'Help - Information',
    element: document.querySelector('#info'),
    intro: 'You can use this panel to get some <b> Help</b>',    
}
var RecentLinksIntro = {
    title: 'Recent Links',
    element: document.querySelector('#recLink'),
    intro: 'You can use this panel to find <b>Recent Links </b>',    
}
var toggleMenuIntro = {
    title: 'Toggle Menu',
    element: document.querySelector('.collapse-btn'),
    intro: 'Toggle the menu as being full or partial.',
};
var fullscreenIntro = {
    title: 'Fullscreen',
    element: document.querySelector('.fullscreen-btn'),
    intro: 'Need more space, use this to make the app fullscreen.',
};
var splitscreenIntro = {
    title: 'Spilitscreen',
    element: document.querySelector('.splitscreen-btn'),
    intro: 'Work simultaneously on two different pages',
};
var bookMarkIntro = {
    title: 'Bookmark',
    element: document.querySelector('.bookmarkPanelToogle'),
    intro: 'Save your favorite pages',
};

var moreAppsIntro = {
    title: 'More Apps',
    element: document.querySelector('.options-menu'),
    intro: 'Explore more apps for additional utilities',
};

var notificationIntro = {
    title: 'Notifications',
    element: document.querySelector('.notificationPanelToggle'),
    intro: 'See new notifications here',
};
var searchIntro = {
    title: 'Search',
    element: document.querySelector('.search-box'),
    intro: `Search the entire website for pages you wish to navigate to.`
};
var userPanelIntro = {
    title: 'User Panel',
    element: document.querySelector('.user-menu'),
    intro: `Find it here along with other resources like <b>Profile Details</b>, <b>Change Password</b> and <code><b>Logout</b></code>`,
};



    function startIntro(user) {

    var intro = introJs()
    switch (user) {
        case 'admin':
            var optionsAdmin = {
                showProgress: true,
                showBullets: false,
                disableInteraction: true,
                steps: [
                    welcomeIntro('Admin'),
                    mainPanelIntro,
                    searchIntro,
                    moreAppsIntro,
                    userPanelIntro,
                    consolidatedIntro,
                    generalAnalysisIntro,
                    AcademicActivitiesIntro,
                    ActiveNoticeIntro,
                    themeIntro,
                    infoIntro,
                    RecentLinksIntro
                ]
            }
            intro.setOptions(optionsAdmin);
            break;
        case 'teacher':
            var tourSelection = {
                title: 'Selections',
                element: document.querySelector('#tourSelection'),
                intro: 'Select the <b>Session</b> and <b>Course</b> you want to see the data for.',
            }
            var tourProgress = {
                title: 'Progress Status',
                element: document.querySelector('#tourProgress'),
                intro: 'Here you can see the progress status of your course.',
            }
            var tourCoverage = {
                title: 'Marks Coverage',
                element: document.querySelector('#tourCoverage'),
                intro: 'CO wise marks coverage is shown here.',
            }
            var tourCOAttainment = {
                title: 'CO Attainment',
                element: document.querySelector('#tourCOAttainment'),
                intro: 'This the CO attainment for all the courses.',
            }
            var tourBloomCoverage = {
                title: `Bloom's Coverage`,
                element: document.querySelector('#tourBloomCoverage'),
                intro: 'Blooms Taxonomy wise marks coverage.',
            }
            var tourPOAttainment = {
                title: 'PO Attainment',
                element: document.querySelector('#tourPOAttainment'),
                intro: 'This the PO attainment for all the courses.',
            }
            var openSpecific = {
                title: 'Start Journey',
                element: document.getElementById('OBE Process').parentElement,
                intro: `Begin your journey by articulating course outcomes`,
            };
            var optionsTeacher = {
                showProgress: true,
                showBullets: false,
                disableInteraction: true,
                steps: [
                    welcomeIntro('Teacher'),
                    mainPanelIntro,
                    searchIntro,
                    tourSelection,
                    tourProgress,
                    tourCoverage,
                    tourCOAttainment,
                    tourBloomCoverage,
                    tourPOAttainment,
                    themeIntro,
                    toggleMenuIntro,
                    fullscreenIntro,
                    splitscreenIntro,
                    bookMarkIntro,
                    moreAppsIntro,
                    notificationIntro,
                    userPanelIntro,
                    openSpecific
                ]
            }
            intro.setOptions(optionsTeacher);
            intro.oncomplete(function () {
                Swal.fire({
                    title: 'Redirect?',
                    icon: 'warning',
                    text: 'Would you like to be redirected to the Course Outcomes!',                    
                    confirmButtonText: 'Yes',
                    showCancelButton: true,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        var selectedNavForMSMenu = [];
                        selectedNavForMSMenu.push('Course Outcomes (CO)');
                        localStorage.setItem('selectedNavForMSOBEMenu', JSON.stringify(selectedNavForMSMenu));
                        window.location.href = "/CO/CO";
                    } else {
                        Swal.fire({
                            title: 'Cool!!!',
                            text: `You can explore the other features before continuing!`,
                            icon: 'success',
                            timer: 2000
                        });
                    }
                })                
            });
            break;
        case 'hod':
            var tourSelection = {
                title: 'Selections',
                element: document.querySelector('#tourSelections'),
                intro: 'Desc Here',
            }
            var tourMail = {
                title: 'Mail',
                element: document.querySelector('#tourMail'),
                intro: 'Send email to faculty for pending process status',
            }
            var tourProgressStatus = {
                title: 'Progress Status',
                element: document.querySelector('#tourProgressStatus'),
                intro: 'Here you can see the progress status of all the courses.',
            }
            var tourObjectStatus = {
                title: 'Object Status',
                element: document.querySelector('#tourObjectStatus'),
                intro: 'This will showcase the OBE framework configuration status of a department.',
            }
            var tourCOAttainment = {
                title: 'CO Attainment',
                element: document.querySelector('#tourCOAttainment'),
                intro: 'This the CO attainment for all the courses.',
            }
            var tourCourseStatus = {
                title: 'Course Status',
                element: document.querySelector('#tourCourseStatus'),
                intro: 'This will showcase the status of courses offered, teacher allotment and course registration.',
            }
            var tourPOAttainment = {
                title: 'PO Attainment',
                element: document.querySelector('#tourPOAttainment'),
                intro: 'This the PO attainment for all the courses.',
            }
            var openSpecific = {
                title: 'Start Journey',
                element: document.getElementById('OBE Process').parentElement,
                intro: `Begin your journey by articulating the institute <b>Vision</b>.`,
            };            
            var optionsHOD = {
                showProgress: true,
                showBullets: false,
                disableInteraction: true,
                steps: [
                    welcomeIntro('HOD'),
                    mainPanelIntro,
                    searchIntro,
                    tourSelection,
                    tourMail,
                    tourProgressStatus,
                    //tourObjectStatus,
                    tourCOAttainment,
                    tourPOAttainment,
                    tourCourseStatus,
                    themeIntro,
                    toggleMenuIntro,
                    fullscreenIntro,
                    splitscreenIntro,
                    bookMarkIntro,
                    moreAppsIntro,
                    notificationIntro,
                    userPanelIntro,
                    openSpecific
                ]
            }
            intro.setOptions(optionsHOD);
            intro.oncomplete(function () {
                Swal.fire({
                    title: 'Redirect?',
                    icon: 'warning',
                    text: 'Would you like to be redirected to the Vision Creation!',
                    confirmButtonText: 'Yes',
                    showCancelButton: true,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        var selectedNavForMSMenu = [];
                        selectedNavForMSMenu.push('Vision');
                        localStorage.setItem('selectedNavForMSOBEMenu', JSON.stringify(selectedNavForMSMenu));
                        window.location.href = "/Vision/Index";
                    } else{
                        Swal.fire({
                            title: 'Cool!!!',
                            text: `You can explore the other features before continuing!`,
                            icon: 'success',
                            timer: 2000
                        });
                    }
                });                  
            });
            break;

        case 'teacherb2c':
            var tourSelection = {
                title: 'Selections',
                element: document.querySelector('#tourSelection'),
                intro: 'Select the <b>Session</b> and <b>Course</b> you want to see the data for.',
            }
            var tourProgress = {
                title: 'Progress Status',
                element: document.querySelector('#tourProgress'),
                intro: 'Here you can see the progress status of your course.',
            }
            var tourCoverage = {
                title: 'Marks Coverage',
                element: document.querySelector('#tourCoverage'),
                intro: 'CO wise marks coverage is shown here.',
            }
            var tourCOAttainment = {
                title: 'CO Attainment',
                element: document.querySelector('#tourCOAttainment'),
                intro: 'This the CO attainment for all the courses.',
            }
            var tourBloomCoverage = {
                title: `Bloom's Coverage`,
                element: document.querySelector('#tourBloomCoverage'),
                intro: 'Blooms Taxonomy wise marks coverage.',
            }
            var tourPOAttainment = {
                title: 'PO Attainment',
                element: document.querySelector('#tourPOAttainment'),
                intro: 'This the PO attainment for all the courses.',
            }
            var openSpecific = {
                title: 'Start Journey',
                element: document.getElementById('OBE Step').parentElement,
                intro: `Begin your journey by articulating course outcomes`,
            };
            var optionsTeacher = {
                showProgress: true,
                showBullets: false,
                disableInteraction: true,
                steps: [
                    welcomeIntro('Teacher'),
                    mainPanelIntro,
                    searchIntro,
                    tourSelection,
                    tourProgress,
                    tourCoverage,
                    tourCOAttainment,
                    tourBloomCoverage,
                    tourPOAttainment,
                    themeIntro,
                    toggleMenuIntro,
                    fullscreenIntro,
                    splitscreenIntro,
                    bookMarkIntro,
                    moreAppsIntro,
                    notificationIntro,
                    userPanelIntro,
                    openSpecific
                ]
            }
            intro.setOptions(optionsTeacher);
            intro.oncomplete(function () {
                Swal.fire({
                    title: 'Redirect?',
                    icon: 'warning',
                    text: 'Would you like to be redirected to the Create Program!',
                    confirmButtonText: 'Yes',
                    showCancelButton: true,

                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        var selectedNavForMSMenu = [];
                        selectedNavForMSMenu.push('Course Outcomes (CO)');
                        localStorage.setItem('selectedNavForMSOBEMenu', JSON.stringify(selectedNavForMSMenu));
                        window.location.href = "/B2C/ProgramB2C/Index";
                    } else {
                        Swal.fire({
                            title: 'Cool!!!',
                            text: `You can explore the other features before continuing!`,
                            icon: 'success',
                            timer: 2000
                        });
                    }
                });   
               
            });
            break;
    }   
    intro.start();
}


    function checkTheIntro(user) {
        console.log(user)
    switch (user) {
        case "1":
            //localStorage.removeItem("selectedNavForMSRFAdminIntro");

            var selectedNavForMSRFAdminIntro = JSON.parse(localStorage.getItem('selectedNavForMSRFAdminIntro')) || [];
            console.log(selectedNavForMSRFAdminIntro)
            if (selectedNavForMSRFAdminIntro[0] != true) {
                selectedNavForMSRFAdminIntro = [];
                //console.log(selectedNavForMSObeAdminIntro[0], 'check', selectedNavForMSObeAdminIntro[0] != true);                              
                selectedNavForMSRFAdminIntro.push(true);
                localStorage.setItem('selectedNavForMSRFAdminIntro', JSON.stringify(selectedNavForMSRFAdminIntro));
                console.log(document.querySelector('#ctl00_mainMenu'), "ddd");
                startIntro('admin');
            }
            break;
        case 'teacher':
            var selectedNavForMSObeTeacherIntro = JSON.parse(localStorage.getItem('selectedNavForMSObeTeacherIntro')) || [];
            if (selectedNavForMSObeTeacherIntro[0] != true) {
                selectedNavForMSObeTeacherIntro = [];
                selectedNavForMSObeTeacherIntro.push(true);
                localStorage.setItem('selectedNavForMSObeTeacherIntro', JSON.stringify(selectedNavForMSObeTeacherIntro));
                startIntro('teacher');
            }
            break;
        case 'hod':
            var selectedNavForMSObeHODIntro = JSON.parse(localStorage.getItem('selectedNavForMSObeHODIntro')) || [];
            if (selectedNavForMSObeHODIntro[0] != true) {
                selectedNavForMSObeHODIntro = [];
                selectedNavForMSObeHODIntro.push(true);
                localStorage.setItem('selectedNavForMSObeHODIntro', JSON.stringify(selectedNavForMSObeHODIntro));
                startIntro('hod');
            }
            break;

        case 'teacherb2c':
            var selectedNavForMSObeTeacherb2cIntro = JSON.parse(localStorage.getItem('selectedNavForMSObeTeacherb2cIntro')) || [];
            if (selectedNavForMSObeTeacherb2cIntro[0] != true) {
                selectedNavForMSObeTeacherb2cIntro = [];
                selectedNavForMSObeTeacherb2cIntro.push(true);
                localStorage.setItem('selectedNavForMSObeTeacherb2cIntro', JSON.stringify(selectedNavForMSObeTeacherb2cIntro));
                startIntro('teacherb2c');
            }
            break;
    }


}

    function checkTheIntroWelcome(user) {
        console.log(user)
        switch (user) {
            case "1":
                //localStorage.removeItem("selectedNavForMSRFAdminIntro");
                startIntro('admin');             
                break;
        }


    }

////toggle tourBtn
//$(document).ready(function () {
//    const tourBtn = document.querySelector('#tourBtn');
//    if (window.location.href.includes('/Homepage/Index') || window.location.href.includes('/FacultyDashboard/Index') || window.location.href.includes('/FacultyDashboardNew/Index') || window.location.href.includes('/HODDashboard/Index') || window.location.href.includes('/HODDashboard/Index')) {
//        tourBtn.style.display = 'block';
//    }
//    else {
//        tourBtn.style.display = 'none';
//    }
//});


