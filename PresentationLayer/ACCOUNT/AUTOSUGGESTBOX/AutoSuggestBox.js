
//Global variables
var g_nSelMenuItem = 0;
var g_sTextBoxID;
var g_bCancelSubmit;
var g_sOldTextBoxValue = "";


function asbGetXmlHttp() {
    var oXmlHttp = false;

    // -----> This method was provided from Jim Ley's website 
    /*@cc_on@*/
    /*@if (@_jscript_version >= 5)
    // JScript gives us Conditional compilation, we can cope with old IE versions.
    // and security blocked creation of the objects.
    try {
        oXmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (e) {
        try {
            oXmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (E) {
            oXmlHttp = false;
        }
    }
    /*@end@*/


    if (!oXmlHttp && typeof XMLHttpRequest != 'undefined') {
        oXmlHttp = new XMLHttpRequest();
    }

    return oXmlHttp;
}


// Over here we make a call back to our server side page and return the results from our query 
// to a DIV tag sitting under the text box
function asbGetDataFromServer(sValue, sDivID, sDataType) {
    var oXmlHttp;
    oXmlHttp = asbGetXmlHttp();

    var sUrl;
    sUrl = ASB_GET_DATA_URL + "?TextBoxID=" + g_sTextBoxID + "&DivID=" + sDivID + "&DataType=" + sDataType + "&Keyword=" + sValue
    oXmlHttp.open("GET", sUrl, true);
    oXmlHttp.onreadystatechange = function() {
        if (oXmlHttp.readyState == 4) {
            if (oXmlHttp.responseText != "") {
                asbShowDiv(sDivID, oXmlHttp.responseText);
            }
            else {
                asbHideDiv(sDivID)
            }
        }
    }

    oXmlHttp.send(null)
}


function asbSetSelectedValue(sValue) {
    asbTRACE("asbSetSelectedValue: " + sValue);

    var hdnSelectedValue = document.getElementById(g_sTextBoxID + "_SelectedValue");
    hdnSelectedValue.value = sValue;
}


function asbSetTextBoxValue() {
    var divMenuItem;
    divMenuItem = asbGetSelMenuItemDiv();

    asbTRACE("divMenuItem " + divMenuItem);

    if (divMenuItem) {
        var sMenuItemValueID;
        sMenuItemValueID = GetDivMenuItemID(g_nSelMenuItem) + "_value";
        var hdnMenuItemValue = document.getElementById(sMenuItemValueID);

        asbTRACE("Set selected item to " + hdnMenuItemValue.value);
        if (hdnMenuItemValue) {
            //Set selected value of control to the value of selected menu item
            asbSetSelectedValue(hdnMenuItemValue.value);
        }

        var txtCtrl;
        txtCtrl = document.getElementById(g_sTextBoxID);


        var sPath = window.location.pathname;
        var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
        //alert(sPage);

    
        

        if (sPage == 'AccountingVouchersNew.aspx') {
            AccountingVouchersScript(txtCtrl);
        }
        
        if (sPage == 'AccountingVouchers.aspx') {
            AccountingVouchersScript(txtCtrl);

        }
        if (sPage == 'AccountingVouchersModifications.aspx') {
            AccountingVouchersModificationsScript(txtCtrl);

        }
        if (sPage == 'BankReconcilation.aspx') {
            AccountingVouchersModificationsScript(txtCtrl);

        }
        if (sPage == 'LedgerReport.aspx') {
            //alert('ram');
            AccountingVouchersModificationsScript(txtCtrl);

        }
        if (sPage == 'PaymentChequePrinting.aspx') {
            PaymentBankChequeEntry(txtCtrl);

        }
        //			newly added for merge account
        if (sPage == 'MergeLedger.aspx') {
            AccountingMergeLedger(txtCtrl);
        }
        if (sPage == 'ChequePrinting.aspx') {
            SetPayee(txtCtrl);

        }



        if (sPage == 'BankEntry.aspx') {

            BankEntry(txtCtrl);

        }
        if (sPage == 'AccountMaster.aspx') {
            BankEntry(txtCtrl);
        }
        if (sPage == 'ledgerhead.aspx') {
            SetBankAccount(txtCtrl);
        }
        if (sPage == 'CostCenterMaster.aspx') {
            SetBankAccount(txtCtrl);
        }

        if (sPage == 'AbstractBillVouchers.aspx') {
            AccountingVouchersScript(txtCtrl);

        }
        if (sPage == 'AbstractBillModification.aspx') {
            AccountingVouchersModificationsScript(txtCtrl);

        }
        if (sPage == 'AbstractBillVouchersPayment.aspx') {
            AccountingVouchersModificationsScript(txtCtrl);            
        }
        if (sPage == 'IsolatedCHequePrint.aspx') {
            BankEntry(txtCtrl);
        }        
    }
}


function BankEntry(txtCtrl) {
    var divMenuItem;
    divMenuItem = asbGetSelMenuItemDiv();
    asbTRACE("divMenuItem " + divMenuItem);
    txtCtrl.value = replaceAll(divMenuItem.innerHTML, '¯', '');
}

function AccountingVouchersModificationsScript(txtCtrl) {


    var divMenuItem;
    divMenuItem = asbGetSelMenuItemDiv();
    asbTRACE("divMenuItem " + divMenuItem);

    var Result = new Array();
    Result = divMenuItem.innerHTML.split('Bal:');

    txtCtrl.value = replaceAll(Result[0], '¯', '');

    txtCtrl.value = replaceAll(txtCtrl.value, '&amp;', '&');

    var ResultMode = new Array();
    ResultMode = Result[1].split(':');

    document.getElementById('ctl00_ContentPlaceHolder1_lblCurBal').value = ResultMode[0];
    document.getElementById('ctl00_ContentPlaceHolder1_hdnBal').value = ResultMode[0];
    document.getElementById('ctl00_ContentPlaceHolder1_txtmd').value = ResultMode[1];
    document.getElementById('ctl00_ContentPlaceHolder1_hdnMode').value = ResultMode[1];
    document.getElementById('ctl00_ContentPlaceHolder1_btnShow').disabled = false;



}

function PaymentBankChequeEntry(txtCtrl) {
    var divMenuItem;
    divMenuItem = asbGetSelMenuItemDiv();

    asbTRACE("divMenuItem " + divMenuItem);
    var Result = new Array();
    Result = divMenuItem.innerHTML.split('Bal:');
    txtCtrl.value = replaceAll(Result[0], '¯', '');
    txtCtrl.value = replaceAll(txtCtrl.value, '&amp;', '&');
    var ResultMode = new Array();
    ResultMode = Result[1].split(':');

    document.getElementById('ctl00_ContentPlaceHolder1_lblCurBal').value = ResultMode[0];
    document.getElementById('ctl00_ContentPlaceHolder1_hdnBal').value = ResultMode[0];
    document.getElementById('ctl00_ContentPlaceHolder1_hdnMode').value = ResultMode[1];
    //document.getElementById('ctl00_ContentPlaceHolder1_btnShow').disabled=true;



}



function SetPayee(txtCtrl) {
    var divMenuItem;
    divMenuItem = asbGetSelMenuItemDiv();

    asbTRACE("divMenuItem " + divMenuItem);
    var Result = new Array();
    Result = divMenuItem.innerHTML.split('A/cNo:');
    txtCtrl.value = replaceAll(Result[0], '¯', '');
    txtCtrl.value = replaceAll(txtCtrl.value, '&amp;', '&');
    var ResultMode = new Array();
    ResultMode = Result[1].split('Add:');

    document.getElementById('ctl00_ContentPlaceHolder1_txtAccountCode').value = ResultMode[0];
    document.getElementById('ctl00_ContentPlaceHolder1_txtAddress').value = ResultMode[1];
    //		    document.getElementById('ctl00_ContentPlaceHolder1_hdnBal').value=ResultMode[0];
    //		    document.getElementById('ctl00_ContentPlaceHolder1_txtmd').value=ResultMode[1];
    //		    document.getElementById('ctl00_ContentPlaceHolder1_hdnMode').value=ResultMode[1];
    //		    document.getElementById('ctl00_ContentPlaceHolder1_btnShow').disabled=true;
    //		    


}





function SetBankAccount(txtCtrl) {
    var divMenuItem;
    divMenuItem = asbGetSelMenuItemDiv();

    asbTRACE("divMenuItem " + divMenuItem);
    var Result = new Array();
    Result = divMenuItem.innerHTML.split('Bank:');
    txtCtrl.value = replaceAll(Result[0], '¯', '');
    txtCtrl.value = replaceAll(txtCtrl.value, '&amp;', '&');




}


function AccountingMergeLedger(txtCtrl) {
    var divMenuItem;
    divMenuItem = asbGetSelMenuItemDiv();
    asbTRACE("divMenuItem " + divMenuItem);
    var Result = new Array();
    Result = divMenuItem.innerHTML.split('Bal:');
    txtCtrl.value = replaceAll(Result[0], '¯', '');
    txtCtrl.value = replaceAll(txtCtrl.value, '&amp;', '&');
}


function AccountingVouchersScript(txtCtrl) {
    var divMenuItem;
    divMenuItem = asbGetSelMenuItemDiv();

    asbTRACE("divMenuItem " + divMenuItem);
    var Result = new Array();
    Result = divMenuItem.innerHTML.split('Bal:');
    txtCtrl.value = replaceAll(Result[0], '¯', '');
    txtCtrl.value = replaceAll(txtCtrl.value, '&amp;', '&');
    var ResultMode = new Array();
    ResultMode = Result[1].split(':');
    var id1 = '';
    var id2 = '';
    var id3 = '';
    var id4 = '';

    if (txtCtrl.id == 'ctl00_ContentPlaceHolder1_txtAgainstAcc') {
        id1 = 'ctl00_ContentPlaceHolder1_lblCurbal1';
        id2 = 'ctl00_ContentPlaceHolder1_lblCrDr1';
        id3 = 'ctl00_ContentPlaceHolder1_hdnCurBalAg';

        //newcode
        id4 = 'ctl00_ContentPlaceHolder1_lblCur1';
        if (document.getElementById('ctl00_ContentPlaceHolder1_lblCurbal1').value < 0) {
            document.getElementById('ctl00_ContentPlaceHolder1_lblCur1').value = -(document.getElementById('ctl00_ContentPlaceHolder1_lblCurbal1').value);
        }
     
        var User_Name = document.getElementById('ctl00_ContentPlaceHolder1_txtAgainstAcc').value
        //document.getElementById('' + id4 + '').innerHTML = ResultMode[0];
        //PageMethods.LookupProduct(User_Name, OnSuccess, OnFailure);
        //PageMethods.LookupProduct(User_Name, OnSuccess, OnFailure);


    }
    else {
        id1 = 'ctl00_ContentPlaceHolder1_lblCurBal2';
        id2 = 'ctl00_ContentPlaceHolder1_lblCrDr2';
        id3 = 'ctl00_ContentPlaceHolder1_hdnCurBal';
        
        //newcode
        id4 = 'ctl00_ContentPlaceHolder1_lblCur2';
        if (document.getElementById('ctl00_ContentPlaceHolder1_lblCurBal2').value < 0) {
            document.getElementById('ctl00_ContentPlaceHolder1_lblCur2').value = -(document.getElementById('ctl00_ContentPlaceHolder1_lblCurBal2').value);

            if (ResultMode[0] < 0) {
                ctl00_ContentPlaceHolder1_lblCurBal2.innerHTML = -ResultMode[0];
            }
            else {
                ctl00_ContentPlaceHolder1_lblCurBal2.innerHTML = ResultMode[0];
            }
        }
    }


    document.getElementById('' + id1 + '').innerHTML = ResultMode[0]; //
    document.getElementById('' + id2 + '').innerHTML = ResultMode[1];
    document.getElementById('' + id3 + '').value = ResultMode[0];
    document.getElementById('ctl00_ContentPlaceHolder1_hdnbal2').value = ResultMode[0];
    //oldka
    //newcode
    if (ResultMode[0] < 0) {
        document.getElementById('' + id1 + '').innerHTML = -(ResultMode[0]); 
    }
    else
        document.getElementById('' + id1 + '').innerHTML = ResultMode[0];
        
   


}










function replaceAll(yourstring, from, to) {

    var str = new String(yourstring);

    var idx = str.indexOf(from);


    while (idx > -1) {
        str = str.replace(from, to);
        idx = str.indexOf(from);
    }

    return str;
}



function asbGetTextBoxValue() {
    var txtCtrl;
    txtCtrl = document.getElementById(g_sTextBoxID);
    return (txtCtrl.value);
}




function asbOnMouseClick(nMenuIndex, sTextBoxID, sDivID) {
    g_nSelMenuItem = nMenuIndex;
    g_sTextBoxID = sTextBoxID;

    asbSetTextBoxValue();
    asbHideDiv(sDivID);
}



function asbOnMouseOver(nMenuIndex, sTextBoxID) {
    g_sTextBoxID = sTextBoxID;

    asbSelectMenuItem(nMenuIndex);
}


function asbOnKeyPress(evt) {
    asbTRACE("asbOnKeyPress : " + asbGetKey(evt));
    if ((asbGetKey(evt) == 13) && (g_bCancelSubmit))
        return false;

    return true;
}


function asbOnKeyUp(sTextBoxID, sDiv, sDataType, evt) {
    g_sTextBoxID = sTextBoxID;

    var nKey;
    nKey = asbGetKey(evt);

    asbTRACE("asbOnKeyUp : " + nKey);


    //Skip up/down/enter
    if ((nKey != 38) && (nKey != 40) && (nKey != 13)) {
        var sNewValue;
        sNewValue = asbGetTextBoxValue();
        asbTRACE("asbOnKeyUp : New text box value '" + sNewValue + "'");

        if ((sNewValue.length <= 20) && (sNewValue.length > 0)) {
            asbTRACE("asbOnKeyUp : Getting data for '" + sNewValue + "'");
            asbGetDataFromServer(sNewValue, sDiv, sDataType)
        }

        if (g_sOldTextBoxValue != sNewValue) {
            asbSetSelectedValue("");
        }
    }
}



function asbOnKeyDown(sTextBoxID, sDiv, evt) {
    asbTRACE("asbOnKeyDown : " + asbGetKey(evt));

    g_sTextBoxID = sTextBoxID;

    //Save current text box value before key press takes affect
    g_sOldTextBoxValue = asbGetTextBoxValue();
    asbTRACE("asbOnKeyDown : old text box value='" + g_sOldTextBoxValue + "'");

    var nKey;
    nKey = asbGetKey(evt);

    //Detect if the user is using the down button
    if (nKey == 38) //Up arrow
    {
        asbMoveDown()
    }
    else if (nKey == 40) //Down arrow
    {
        asbMoveUp()
    }
    else if (nKey == 13) //Enter
    {
        asbTRACE("asbOnKeyDown : asbIsVisibleDiv - " + asbIsVisibleDiv(sDiv));
        if (asbIsVisibleDiv(sDiv)) {
            asbHideDiv(sDiv);
            asbTRACE("asbOnKeyDown : asbHideDiv");

            //Only works in IE
            evt.cancelBubble = true;

            if (evt.returnValue) evt.returnValue = false;
            if (evt.stopPropagation) evt.stopPropagation();

            g_bCancelSubmit = true;
        }
        else {
            g_bCancelSubmit = false;
        }
    }
    else {
        asbHideDiv(sDiv);
    }

    return true;
}


function asbGetSelMenuItemDiv() {
    return asbGetMenuItemDiv(g_nSelMenuItem);
}


function GetDivMenuItemID(nMenuItem) {
    return (g_sTextBoxID + "_mi_" + nMenuItem);
}



function asbGetMenuItemDiv(nMenuItem) {
    var sDivMenuItemID;
    sDivMenuItemID = GetDivMenuItemID(nMenuItem);

    return document.getElementById(sDivMenuItemID)
}


function asbMoveUp() {
    var nMenuItem;
    nMenuItem = g_nSelMenuItem + 1;

    //Check if menu item exists
    if (asbGetMenuItemDiv(nMenuItem)) {
        asbSelectMenuItem(nMenuItem)
    }
}


function asbMoveDown() {
    var nMenuItem;
    nMenuItem = g_nSelMenuItem - 1;

    if (nMenuItem != 0) {
        asbSelectMenuItem(nMenuItem)
    }
}


//Highlights a div
function asbSelectMenuItem(nMenuItem) {
    var divMenuItem;
    divMenuItem = asbGetMenuItemDiv(nMenuItem)

    if (divMenuItem) {
        if (nMenuItem != g_nSelMenuItem) {
            asbUnhighlightSelMenuItem();

            g_nSelMenuItem = nMenuItem;
            asbSetTextBoxValue();

            divMenuItem.className = "asbSelMenuItem"
        }
    }
}


//unhighlights a div
function asbUnhighlightSelMenuItem() {
    var divMenuItem;
    divMenuItem = asbGetSelMenuItemDiv()

    if (divMenuItem) {
        divMenuItem.className = "asbMenuItem"
    }
}


//Detects what key was pressed
function asbGetKey(evt) {
    evt = (evt) ? evt : (window.event) ? event : null;
    if (evt) {
        var cCode = (evt.charCode) ? evt.charCode :
					((evt.keyCode) ? evt.keyCode :
					((evt.which) ? evt.which : 0));
        return cCode;
    }
}



function asbHideDiv(sDivID) {
    document.getElementById(sDivID).style.visibility = 'hidden';
    g_nSelMenuItem = 0;
}


function asbIsVisibleDiv(sDivID) {
    if (document.getElementById(sDivID).style.visibility == 'hidden') {
        return false;
    }
    else {
        return true;
    }
}


function asbShowDiv(sDivID, sDivContent) {
    var divMenu;
    divMenu = document.getElementById(sDivID);
    var sInnerHtml;
    //Use IFrame of the same size as div		
    if (asbIsIE()) {
        sInnerHtml = "<div id='" + sDivID + "_content' style='z-index:9050; position:asbolute;'>";
        sInnerHtml += sDivContent;
        sInnerHtml += "</div><iframe id='" + sDivID + "_iframe' src='about:blank' frameborder='1' scrolling='no'></iframe>";
    }
    else {
        sInnerHtml = sDivContent;
    }


    divMenu.innerHTML = sInnerHtml;


    if (asbIsIE()) {
        var divContent;
        divContent = document.getElementById(sDivID + "_content");

        var divIframe;
        divIframe = document.getElementById(sDivID + "_iframe");

        //Remember display type
        divContent.className = "asbMenu";
        divMenu.className = "asbMenuBase";

        divIframe.style.width = divContent.offsetWidth + 'px';
        divIframe.style.height = divContent.offsetHeight + 'px';
        divIframe.marginTop = "-" + divContent.offsetHeight + 'px';

    }

    divMenu.style.visibility = 'visible';
}


function asbIsIE() {
    return (navigator.appName == "Microsoft Internet Explorer");
}


function asbIsNav() {
    return (navigator.appName == "Netscape");
}


function asbTRACE(sText) {
    //var sMessage = window.document.forms[0].txtTRACE.value;
    //sMessage = sMessage + sText + "\n";;
    //window.document.forms[0].txtTRACE.value = sMessage;
}