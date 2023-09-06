//CREATED BY    : MRUNAL SINGH
//CREATED DATE  : 05-01-2015
//DESCRIPTION   : IT IS USED TO VALIDATE INPUTS.

function CheckAlphabet(event, obj) {

    var k = (window.event) ? event.keyCode : event.which;
    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46) {
        obj.style.backgroundColor = "White";
        return true;

    }
    if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
        obj.style.backgroundColor = "White";
        return true;

    }
    else {
        alert('Please Enter Alphabets Only!');
        obj.focus();
    }
    return false;
}


function CheckNumeric(event, obj) {
    var k = (window.event) ? event.keyCode : event.which;
    //alert(k);
    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
        obj.style.backgroundColor = "White";
        return true;
    }
    if (k > 45 && k < 58) {
        obj.style.backgroundColor = "White";
        return true;

    }
    else {
        alert('Please Enter numeric Value');
        obj.focus();
    }
    return false;
}

function CheckAlphaNumeric(event, obj) {

    var k = (window.event) ? event.keyCode : event.which;

    // alert(k);

    if (k == 8 || k == 9 || k == 16 || k == 0 || k == 32 || k == 45) {
        obj.style.backgroundColor = "White";
        return true;

    }
    if (k > 45 && k < 58 || k > 95 && k < 106 || k > 64 && k < 91 || k > 96 && k < 123) {
        obj.style.backgroundColor = "White";
        return true;

    }
    else {
        alert('Please Enter Alphabets & Numbers Only!');
        obj.focus();
    }
    return false;
}