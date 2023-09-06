
$(document).ready(function () {

    $(".dark-thm").click(function () {
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").addClass("dark-thm-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("default-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("three-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("two-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("four-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("one-c");
        $(".new-nav-container").css({ "background-color": "#353c48", "color": "#fff" });
        $(".new-nav-container .fa-search").css({ "color": "#353c48" });
        localStorage.setItem("color", "dark-thm-c");
    });

    $(".light-thm").click(function () {
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").addClass("default-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("dark-thm-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("three-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("two-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("four-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("one-c");
        $(".new-nav-container").css({ "background-color": "#fff", "color": "#85879c" });
        $(".new-nav-container .fa-search").css({ "color": "#85879c" });
        localStorage.setItem("color", "default-c");
    });

    $(".default").click(function () {
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").addClass("default-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("dark-thm-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("three-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("two-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("four-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("one-c");
        $(".new-nav-container").css({ "background-color": "#fff", "color": "#85879c" });
        $(".new-nav-container .fa-search").css({ "color": "#85879c" });
        localStorage.setItem("color", "default-c");
    });

    $(".one").click(function () {
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").addClass("one-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("dark-thm-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("three-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("two-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("four-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("default-c");
        $(".new-nav-container").css({ "background-color": "#3dc9b3", "color": "#fff" });
        $(".new-nav-container .fa-search").css({ "color": "#85879c" });
        localStorage.setItem("color", "one-c");
    });

    $(".two").click(function () {
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").addClass("two-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("dark-thm-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("three-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("one-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("four-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("default-c");
        $(".new-nav-container").css({ "background-color": "#28c76f", "color": "#fff" });
        $(".new-nav-container .fa-search").css({ "color": "#85879c" });
        localStorage.setItem("color", "two-c");
    });

    $(".three").click(function () {
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").addClass("three-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("dark-thm-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("two-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("one-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("four-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("default-c");
        $(".new-nav-container").css({ "background-color": "#5864bd", "color": "#fff" });
        $(".new-nav-container .fa-search").css({ "color": "#85879c" });
        localStorage.setItem("color", "three-c");
    });

    $(".four").click(function () {
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").addClass("four-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("dark-thm-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("three-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("two-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("one-c");
        $("body, #ctl00_mainMenu .level1.nav li, .navbar-custom-menu, #sidebar, #show-sidebar, .page-content, .daterangepicker").removeClass("default-c");
        $(".new-nav-container").css({ "background-color": "#ea5455", "color": "#fff" });
        $(".new-nav-container .fa-search").css({ "color": "#85879c" });
        localStorage.setItem("color", "four-c");
    });


    if (localStorage.getItem("color") == "default-c") {
        $(".default").click();
    }
    else if (localStorage.getItem("color") == "default-c") {
        $(".light-thm").click();
    }
    else if (localStorage.getItem("color") == "dark-thm-c") {
        $(".dark-thm").click();
    }
    else if (localStorage.getItem("color") == "one-c") {
        $(".one").click();
    }
    else if (localStorage.getItem("color") == "two-c") {
        $(".two").click();
    }
    else if (localStorage.getItem("color") == "three-c") {
        $(".three").click();
    }
    else if (localStorage.getItem("color") == "four-c") {
        $(".four").click();
    }

});
