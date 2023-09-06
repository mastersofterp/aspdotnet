<%@ OutputCache Duration="5" VaryByParam="None" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_Descriptive.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="Itle_Test_Descriptive" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<!DOCTYPE html >
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link rel="shortcut icon" href="Images/nophoto.jpg" type="image/x-icon" id="lnklogo">

    <link href="../plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="../plugins/newbootstrap/css/newcustom.css" rel="stylesheet" />

    <script src="../plugins/newbootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="../plugins/newbootstrap/js/popper.min.js"></script>
    <script src="../plugins/newbootstrap/js/bootstrap.min.js"></script>

    <%-- <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css")%>" rel="stylesheet" />
    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/newcustom.css")%>" rel="stylesheet" />

    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>--%>


    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

    <%--<script src="../plugins/ckeditor/ckeditor_basic.js"></script>--%>
    <script src="../plugins/ckeditor/config.js"></script>


    <style type="text/css">
        * {
            font-size: 100%;
            font-family: Arial;
            font-weight: normal;
        }

        .note {
            border: 1px solid#ccc;
            margin-bottom: 20px;
            padding: 20px;
        }
    </style>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <style type="text/css">
        .visibleDiv, #topLeft, #topRight, #bottomLeft, #bottomRight {
            position: fixed;
            width: 210px;
            height: 100px;
            vertical-align: middle;
            /*background: #ffdab9;*/
            background: #e8effa;
            text-align: center;
            border: 1px solid #ccc;
            border-radius: 6px;
            box-shadow: rgb(0 0 0 / 20%) 0px 0px 5px;
        }

        #topLeft {
            top: 10px;
            left: 10px;
        }

        #topRight {
            top: 20px;
            right: 10px;
        }

        #bottomLeft {
            bottom: 10px;
            left: 10px;
        }

        #bottomRight {
            bottom: 0px;
            right: 0px;
        }
    </style>

    <style>
        .new {
            width: 100px;
            background: #255282;
            padding: 10px;
            color: #fff;
        }

        #Label2, #lblTimer, #lblTestDuration, #starttime, #endtime, #showtime {
        }
    </style>
    <script type="text/javascript">
      
       
    </script>
    <script type="text/javascript">
        //This is used for virtual keyboard
        function InsertSymbol(divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";

            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";

            }
        }



    </script>


    <script type="text/javascript">
        //handle switching window and alert popup
        //var count = 0;
        window.onblur = function () {
            debugger;
            if (document.getElementById("hdnMalfunction").value > 0) {
                if (document.getElementById("hdnMalfunction").value == document.getElementById("hdnMalfunctionDesStud").value) {
                    document.getElementById("lblAlert").innerHTML = "You have Cross the limit of malpractice.Your test will be Ended!";
                    $('#myCommonAlertModal').modal('show');
                    __doPostBack('btnSubmit', 'OnClick');

                } else {

                    __doPostBack('btnUpdateMalFunction', 'OnClick');

                }
            }


        }





    </script>
    <script type="text/javascript">
        /*CODE TO DISPLAY AND HIDE MODAL POPUP*/
        function OpenModal() {
            debugger;
            $('#myCommonAlertModal').modal('show');
        }

        function CloseModal() {
            debugger;
            $('#myCommonAlertModal').modal('hide');
        }


    </script>


    <script type="text/javascript" language="JavaScript1.2">

        if (document.all) {
            document.onkeydown = function () {
                var key_f5 = 18; // 116 = F5

                if (key_f5 == event.keyCode) {
                    event.keyCode = 8;

                    return false;
                }
            }
        }

    </script>

    <script type="text/javascript" language="javascript">
        function checkKeyCode(evt) {

            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (event.keyCode == 116) {
                evt.keyCode = 0;
                return false
            }
        }
        document.onkeydown = checkKeyCode;
    </script>

    <script type="text/javascript">
        //        function onPostBack() {
        //            var y = generateRandomSequence();
        //            var hdnGuid = document.getElementById("hdnGuid");
        //            hdnGuid.value = y;
        //        }


        //        function generateRandomSequence() {
        //            var g = "";
        //            for (var i = 0; i < 32; i++)
        //                g += Math.floor(Math.random() * 0xF).toString(0xF)
        //            return g;
        //        } 






        function checkRefresh() {
            if (document.form1.visited.value == "") {
                // This is a fresh page load
                document.form1.visited.value = "1";

                // You may want to add code here special for
                // fresh page loads
            }
            else {
                // This is a page refresh
                window.close();
                // Insert code here representing what to do on
                // a refresh
            }
        }

        //window.onbeforeunload = 
        //function () 
        //{
        //window.location.href = 'Result.aspx';
        //window.close();


        //}

        //        window.document.statusbar.enable = false;
        window.locationbar.visible = false;
        function checkRefresh() {
            if (document.form1.visited.value == "") {
                // This is a fresh page load
                document.form1.visited.value = "1";
                alert('test');

                // You may want to add code here special for
                // fresh page loads
            }
            else {
                alert('test');

                window.close();
                window.location.href = 'Result.aspx';
                // This is a page refresh

                // Insert code here representing what to do on
                // a refresh
            }
        }



        function disableF5(e) { if (e.which == 116) e.preventDefault(); };
        // To disable f5
        $(document).bind("keydown", disableF5);
        // To re-enable f5
        $(document).unbind("keydown", disableF5);

        if (document.all) {
            document.onkeydown = function () {
                var key_f5 = 18; // 116 = F5

                if (key_f5 == event.keyCode) {
                    event.keyCode = 27;

                    return false;
                }
            }
        }


        function checkKeyCode(evt) {

            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (event.keyCode == 116) {
                evt.keyCode = 0;
                return false
            }
        }
        document.onkeydown = checkKeyCode;




        function disableRefresh() {
            if (event.keyCode == 116) {
                return false;
            }
            else {
                return true;
            }
        }



        function checkKeyCode(evt) {

            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (event.keyCode == 116) {
                evt.keyCode = 0;
                return false
            }
        }
        document.onkeydown = checkKeyCode;


        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);

        function myTimer(startVal, interval, outputId, dataField) {
            this.value = startVal;
            this.OutputCntrl = document.getElementById(outputId);
            this.currentTimeOut = null;
            this.interval = interval;
            this.stopped = false;
            this.data = null;
            var formEls = document.form1.elements;
            if (dataField) {
                for (var i = 0; i < formEls.length - 1; i++) {
                    if (formEls[i].name == dataField) {
                        this.data = formEls[i];
                        i = formEls.length + 1;
                        //alert(i);
                    }
                    //alert(i);
                }
            }

            myTimer.prototype.go = function () {
                if (this.value > 0 && this.stopped == false) {
                    this.value = (this.value - this.interval);
                    if (this.data) {
                        this.data.value = this.value;
                        //                        Session["inttime"] = this.value;
                        // alert(this.value);
                        //this.currentTimeOut = setTimeout("Timer.go()", this.interval);
                    }
                    var current = this.value;
                    this.OutputCntrl.innerHTML = this.Hours(current) + ':' + this.Minutes(current) + ':' + this.Seconds(current);
                    if (this.OutputCntrl.innerHTML == '0:0:59')
                        alert('One minute remaining');

                    this.currentTimeOut = setTimeout("Timer.go()", this.interval);
                }
                else {

                    __doPostBack('btnSubmit', 'OnClick');
                    alert('Time Out!');

                    //                    function ShowCurrentTime() {
                    //used to call server method...
                    PageMethods.GetCurrentTime();
                    //}
                    //                    function OnSuccess(response, userContext, methodName) {
                    //                        alert(response);
                    //                    }
                    //document.getElementById('RadioButtonList1').disabled = true;
                    //document.getElementById('Button1').disabled = true;
                    //document.getElementById('Button3').disabled = true;
                    //this.raiseEvent(Button3_Click, '');
                    //$("#Button3").click
                    window.location.href = 'Thanks.aspx';


                    return false;
                }



            }
            myTimer.prototype.stop = function () {
                this.stopped = true;
                if (this.currentTimeOut != null) {

                    clearTimeout(this.currentTimeout);
                }
            }
            myTimer.prototype.Hours = function (value) {
                return Math.floor(value / 3600000);
            }
            myTimer.prototype.Minutes = function (value) {
                return Math.floor((value - (this.Hours(value) * 3600000)) / 60000);
            }
            myTimer.prototype.Seconds = function (value) {
                var hoursMillSecs = (this.Hours(value) * 3600000)
                var minutesMillSecs = (this.Minutes(value) * 60000)
                var total = (hoursMillSecs + minutesMillSecs)
                var ans = Math.floor(((this.value - total) % 60000) / 1000);

                if (ans < 10)
                    return "0" + ans;

                return ans;
            }
        }



        document.onkeydown = function () {
            if (event.keyCode == 115) alert('ctrl Key is disabled');
            if (event.keyCode == 18) alert('');
            if (event.keyCode == 17 && event.keyCode == 09) alert('');

        };


        document.onkeydown = function (e) {
            if (e.ctrlKey && (e.keyCode === 67 || e.keyCode === 86 || e.keyCode === 85 || e.keyCode === 117)) {//Alt+c, Alt+v will also be disabled sadly.
                alert('not allowed');
            }
            return false;
        };

    </script>

    <script type="text/javascript"> 
<!--
    //Disable right click script 
    //visit http://www.rainbow.arch.scriptmania.com/scripts/ 
    var message = "Sorry, right-click has been disabled";
    /////////////////////////////////// 
    function clickIE() { if (document.all) { (message); return false; } }
    function clickNS(e) {
        if
(document.layers || (document.getElementById && !document.all)) {
            if (e.which == 2 || e.which == 3) { (message); return false; }
        }
    }
    if (document.layers)
    { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
    else { document.onmouseup = clickNS; document.oncontextmenu = clickIE; }
    document.oncontextmenu = new Function("return false")


    // -->     

    </script>

    <style type="text/css">
        .style4 {
        }
    </style>

    <script type="text/javascript" language="JavaScript">

        function disableCtrlKeyCombination(e) {

            //list all CTRL + key combinations you want to disable

            var forbiddenKeys = new Array('a', 'n', 'c', 'x', 'v', 'j');

            var key;

            var isCtrl;



            if (window.event) {


                key = window.event.keyCode;     //IE

                if (window.event.ctrlKey) {

                    alert('Do not Try to Copy any Content !');
                    isCtrl = false;
                }

                else

                    isCtrl = false;

            }

            else {


                key = e.which;     //firefox

                if (e.ctrlKey) {

                    alert('Do not Try to Copy any Content !');
                    isCtrl = false;
                }

                else

                    isCtrl = false;

            }



            //if ctrl is pressed check if other key is in forbidenKeys array

            if (isCtrl) {

                for (i = 0; i < forbiddenkeys.length; i++) {

                    //case-insensitive comparation

                    if (forbiddenKeys[i].toLowerCase() == String.fromCharCode(key).toLowerCase()) {

                        alert('Key combination CTRL + '

                    + String.fromCharCode(key)

                    + ' has been disabled.');

                        return false;

                    }

                }

            }

            return true;

        }




    </script>

    <%--Sachin Ghagre 22 March 2017--%>

    <script type='text/javascript'>
        $(function () {
            $('*').keypress(function (e) { e.preventDefault(); });

        });
    </script>

    <%--End of Sachin Ghagre 22 March 2017--%>

    <script language="JavaScript" type='text/javascript'>
        function TriggeredKey(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (window.event.keyCode = 8) return false;
            if (window.event.keyCode = 45) return false;
            if (window.event.keyCode = 18) return false;
            if (window.event.keyCode = 17) return false;

        }
    </script>
</head>
<body class="body-main" onload="f1()">
    <form id="form1" runat="server">
        <input type="hidden" name="visited" value="" />
        <asp:ScriptManager ID="ScriptManager_Main" runat="server" EnablePartialRendering="true"
            EnablePageMethods="true">
        </asp:ScriptManager>
        <div class="container-fluid pl-3 pr-3" id="Table_Main">

            <div class="row mt-3">
                <div class="col-12 mb-2" style="padding: 15px; background: #fff; box-shadow: 0 0 8px #ccc; box-shadow: rgb(0 0 0 / 20%) 0px 0px 5px; border-radius: 10px;">
                    <div class="form-group col-lg-8 col-md-12 col-12">
                        <div class=" note-div">
                            <h5 class="heading">Note </h5>
                            <p>
                                <i class="fa fa-star" aria-hidden="true"></i><span style="color: #FF0000;">Please don't close the
                                            browser once you started the test else your attempt will be lost !</span>
                            </p>
                        </div>
                    </div>

                    <div class="col-12 mt-3 mb-3">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Session Name :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>

                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Course Name :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>

                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Test Name :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblTestName" runat="server" Font-Bold="True"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>



                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Welcome :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblUrname" runat="server" Font-Bold="True"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>

                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Total Time for Test :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblTotTime" Font-Bold="true" runat="server"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>

                            <div class="col-lg-6 col-md-6 col-12">
                                <asp:Label ID="lblSeatno" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                <div id="tr" runat="server" visible="false">
                                    <asp:Label ID="lblQueNo" Font-Bold="true" runat="server"></asp:Label>
                                </div>
                                <div visible="false">
                                    <asp:Label ID="lblTimerCount" Font-Bold="true" Visible="false" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="col-lg-6 col-md-6 col-12">
                                <asp:Label ID="lblTotalMarks" Font-Bold="true" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlSlot" runat="server" Width="100%">
                            <div class="sub-heading">
                                <h5>Online Test</h5>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel_Login" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row" id="table3">
                                        <div class="col-md-6 col-12">
                                            <asp:ListView ID="lvTest" runat="server" OnItemCommand="lvTest_OnItemCommand">
                                                <LayoutTemplate>
                                                    <div class="table-responsive" style="height: 400px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                            <thead class="" style="position: sticky; top: 0; z-index: 1; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>
                                                                    <th style="text-align: left; width: 15%">Sr.No.
                                                                    </th>
                                                                    <th style="text-align: left; width: 70%">Question
                                                                    </th>
                                                                    <th style="text-align: left; width: 10%">Marks
                                                                    </th>
                                                                    <th></th>
                                                                    <th></th>
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
                                                        <td style="width: 15%">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("QU_SRNO")%>'></asp:Label>
                                                        </td>
                                                        <td id="Qestion" runat="server" style="width: 100%; height: auto">
                                                            <%--<asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>--%>
                                                            <asp:Label ID="lblQuestions" runat="server" ForeColor="black" Font-Bold="true" Text='<%# Eval("QUESTIONTEXT")%>'
                                                                SkinID="lblQuestion"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("MARKS_FOR_QUESTION")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnAnswer" runat="server" ImageUrl="../IMAGES/redArrow1.jpg"
                                                                Width="30px" Height="30px" AlternateText='<%# Eval("MARKS_FOR_QUESTION")%>' CommandArgument='<%# Eval("QUENO")%>'
                                                                ToolTip='<%# Eval("QUESTIONTEXT")%>' OnClick="btnAnswer_Click" />
                                                            <asp:HiddenField ID="hdQueno" runat="server" Value='<%# Eval("QUENO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgQuesImage" runat="server" Width="70px" ImageUrl='<%# Eval("QUESTIONIMAGE")%>'
                                                                onclick="DisplayAns1InWidnow();" EnableTheming="False" ImageAlign="Left" Visible="false" />
                                                            <hr style="margin-left: 1%" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="col-md-6 col-12">

                                            <div id="divDescriptiveAnswer" runat="server" visible="false">
                                                <asp:Panel ID="PnlList" runat="server">
                                                    <div class="" style="height: 400px; overflow: scroll; width: 100%;">
                                                        <div class="row">
                                                            <div class="col-md-2 col-12">
                                                                <span style="font-weight: 600; color: #444;">Question</span>
                                                            </div>
                                                            <div class="col-md-10 col-12">
                                                                <asp:Label ID="lblQuestion" runat="server" Font-Bold="true" ForeColor="#990000" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-2 col-12">
                                                                <span style="font-weight: 600; color: #444;">Answer</span>
                                                            </div>
                                                            <div class="col-md-10 col-12">
                                                                <div class="table-responsive">
                                                                    <CKEditor:CKEditorControl ID="txtAnswer" runat="server" Width="580px" Height="100px" oncopy="return false" onpaste="return false" oncut="return false"
                                                                        BasePath="~/plugins/ckeditor" ToolTip="Enter Description" TabIndex="2"></CKEditor:CKEditorControl>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnSymbol" runat="server" Text="Math Equation" OnClientClick="MathEditor();" CssClass="btn btn-outline-primary"
                                                                ToolTip="Click here for Math Equation" />

                                                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-outline-info"></asp:Button>

                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger"></asp:Button>

                                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"></asp:Button>
                                                            <asp:LinkButton ID="btnUpdateMalFunction" runat="server" OnClick="btnUpdateMalFunction_Click" CssClass="btn btn-default" Visible="false"></asp:LinkButton>

                                                            <asp:HiddenField ID="hdnMalfunction" runat="server" />
                                                            <asp:HiddenField ID="hdnMalfunctionDesStud" runat="server" />

                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Label ID="lblStatus" runat="server" Text="Answer Saved..." Visible="false" Font-Bold="true"
                                                                ForeColor="#006600"></asp:Label>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divKeyBoard col-12" style="display: none">
                                        <iframe src="Virtual_Keyboard.htm" width="550px" height="450px"></iframe>
                                    </div>

                                    <div class="modal fade" id="myCommonAlertModal" tabindex="-1" role="dialog" aria-labelledby="myModalReport" aria-hidden="true">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header" style="background-color: red">
                                                    <h4 class="modal-title" id="myModalReport"><span class="glyphicon glyphicon-exclamation-sign"></span>Alert !</h4>
                                                </div>
                                                <div class="modal-body">
                                                    <asp:Label ID="lblAlert" runat="server"></asp:Label>
                                                </div>

                                                <div class="modal-footer">
                                                    <asp:Button ID="btnCloseModalPopup" runat="server" Text="Close" OnClick="btnCloseModalPopup_Click"
                                                        CssClass="btn btn-outline-danger" data-dismiss="modal" TabIndex="8" />
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />

                    </div>

                    <div id="topRight">
                        <h6>
                            <asp:Label ID="Label2" Font-Bold="true" runat="server"></asp:Label>
                            <asp:Label ID="lblTimer" runat="server"></asp:Label>
                            <asp:Label ID="lblTestDuration" runat="server" Visible="false"></asp:Label>

                            <br />
                            <div id="starttime"></div>

                            <div id="endtime" runat="server" visible="false"></div>
                            <br />
                            <div id="showtime"></div>
                        </h6>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function DisplayAns1InWidnow() {

            var img = document.getElementById(' imgQuesImage.ClientID ').src;
            var l = (screen.width - 475) / 2;
            var t = (screen.height - 500) / 2;

            html = "<HTML><HEAD><TITLE>Answer1</TITLE>"
            + "</HEAD><BODY LEFTMARGIN=0 "
            + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
            + "<IMG src='"
            + img
            + "' BORDER=0 NAME=image "
             + "onload='window.resizeTo(document.image.width,document.image.height)'>"
            + "</CENTER>"
            + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=no,location=0,directories=0,left=200,top=200,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
            popup.document.close();
        }



    </script>


    <%-- sachin 01july2017 --%>


    <script type="text/javascript">
        var tim;
        var min = '<%=Session["totMin"]%>';
    min = min - 1;
    var sec = 60;
    var f = new Date();
    function f1() {
        f2();
        document.getElementById("starttime").innerHTML = "Start Exam at : " + f.getHours() + ":" + f.getMinutes();

        //document.getElementById("endtime").innerHTML = "Your  time is :" + f.toLocaleTimeString();
    }
    function f2() {
        if (parseInt(sec) > 0) {
            sec = parseInt(sec) - 1;
            document.getElementById("showtime").innerHTML = "Left Time:" + min + " Min ," + sec + " Sec";
            tim = setTimeout("f2()", 1000);
        }
        else {
            if (parseInt(sec) == 0) {
                min = parseInt(min) - 1;
                if (parseInt(min) < 0) {
                    clearTimeout(tim);
                    location.href = "Thanks.aspx";
                }
                else {
                    sec = 60;
                    document.getElementById("showtime").innerHTML = "Left Time  is :" + min + " Minutes ," + sec + " Seconds";
                    tim = setTimeout("f2()", 1000);
                }
            }

        }
    }
    </script>

    <script type="text/javascript">
        //For Server 
        function MathEditor() {
            window.open("../ITLE_MathEditor.aspx", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=800, height=400");

        }

        //For Local Uncomment This
        //function MathEditor() {
        //    window.open("MathEditor.aspx", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=800, height=400");

        //}
    </script>





    <script language="javascript" type="text/javascript">
        //Function to disable Cntrl key/right click
        function DisableControlKey(e) {
            // Message to display
            var message = "";
            // Condition to check mouse right click / Ctrl key press
            if (e.which == 17 || e.button == 2) {
                alert(message);
                return false;
            }
        }
    </script>
</body>
</html>
