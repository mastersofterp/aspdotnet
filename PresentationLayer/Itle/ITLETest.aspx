<%@ OutputCache Duration="5" VaryByParam="None" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ITLETest.aspx.cs" Inherits="Itle_ITLETest" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<!DOCTYPE html >
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link rel="shortcut icon" href="Images/nophoto.jpg" type="image/x-icon" id="lnklogo">
    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css")%>" rel="stylesheet" />


    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>
    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/newcustom.css")%>" rel="stylesheet" />

    <script type="text/javascript">
        //handle switching window and alert popup
        //var count = 0;
        window.onblur = function () {
             debugger;
             if (document.getElementById("hdnMalfunction").value > 0) {
                 if (document.getElementById("hdnMalfunction").value == document.getElementById("hdnMalfunctionStudent").value) {
                     document.getElementById("lblAlert").innerHTML = "You have Cross the limit of malpractice.Your test will be Ended!";
                     $('#myCommonAlertModal').modal('show');
                     __doPostBack('btnSubmit', 'OnClick');
                     document.getElementById("btnsubNEw").style.visibility = "visible";
                      document.getElementById("<%=btnsubNEw.ClientID%>").click();
                 } else {

                     __doPostBack('btnUpdateMalFunction', 'OnClick');
                     
                 }
             }
            

        }
       


        this.WindowState = FormWindowState.Maximized;
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

        function OpenFinishModal() {
            debugger;
            $('#myModel').modal('show');
        }

        function CloseFinishModal() {
            $('#myModel').modal('hide');
        }

    </script>

    <script type="text/javascript">
        /*Disble Refresh,F12,select*/
        function disableF5(e) { if ((e.which || e.keyCode) == 116) e.preventDefault(); };
        $(document).on("keydown", disableF5);

        function disableR(e) { if ((e.which || e.keyCode) == 82) e.preventDefault(); };
        $(document).on("keydown", disableR);

        function disableF12(e) { if ((e.which || e.keyCode) == 123) e.preventDefault(); };
        $(document).on("keydown", disableF12);

        //function disablePrint(e) {
        //    if ((e.which || e.keyCode) == 44) e.preventDefault();
        //    stopPrntScr();
        //    document.getElementById("lblAlert").innerHTML = "Disable Print!";
        //    $('#myCommonAlertModal').modal('show');
        //};
        //$(document).on("keyup", disablePrint);



        function Disable_Control_C() {
            var keystroke = String.fromCharCode(event.keyCode).toLowerCase();
            if (event.ctrlKey && (keystroke == 'c' || keystroke == 'v')) {
                event.returnValue = false; // disable Ctrl+C
            }
        }
        $(document).on("keydown", Disable_Control_C);


        if (document.addEventListener !== undefined) {
            // Not IE
            document.addEventListener('click', checkSelection, false);
        } else {
            // IE
            document.attachEvent('onclick', checkSelection);
        }

        function checkSelection() {
            var sel = {};
            if (window.getSelection) {
                // Mozilla
                sel = window.getSelection();
            } else if (document.selection) {
                // IE
                sel = document.selection.createRange();
            }

            // Mozilla
            if (sel.rangeCount) {
                sel.removeAllRanges();
                return;
            }

            // IE
            if (sel.text > '') {
                document.selection.empty();
                return;
            }
        }

    </script>


    <script type="text/javascript">


        function myTimer(startVal, interval, outputId, dataField) {
            ////debugger;
            this.value = startVal;
            this.OutputCntrl = document.getElementById(outputId);
            this.currentTimeOut = null;
            this.interval = interval;
            this.stopped = false;
            this.data = null;
            //var formEls =document.getElementsByTagName("form")
            var formEls = document.getElementById("form1");
            //var formEls = document.form1.elements;
            if (dataField) {

                for (var i = 0; i < formEls.length - 1; i++) {
                    if (formEls[i].name == dataField) {
                        this.data = formEls[i];
                        i = formEls.length + 1;
                        //  alert('1');
                    }
                }
            }
            var lastfive = 0;
            var lastone = 0;
            myTimer.prototype.go = function () {
                ////debugger;
                if (this.value > 0 && this.stopped == false) {
                    this.value = (this.value - this.interval);
                    if (this.data) {
                        this.data.value = this.value;

                    }
                    var current = this.value;


                    var countdowntimer = document.getElementById("lblTimerCount");
                    countdowntimer.innerHTML = this.Hours(current) + ':' + this.Minutes(current) + ':' + this.Seconds(current);



                    //if (countdowntimer.innerHTML == '0:4:59')

                    if (countdowntimer.innerHTML == '0:4:59') {

                        document.getElementById("lblAlert").innerHTML = "Last Five Minutes Remaining !!";
                        $('#myCommonAlertModal').modal('show');

                    }

                    if (countdowntimer.innerHTML == '0:1:00') {
                        document.getElementById("lblAlert").innerHTML = "Last One Minutes Remaining !!";
                        $('#myCommonAlertModal').modal('show');

                    }
                    //alert('Last One Minutes Remaining !!');



                    this.currentTimeOut = setTimeout("Timer.go()", this.interval);

                    document.getElementById("btnsubNEw").style.visibility = "hidden";

                    // alert('3');
                }
                else {


                    // alert('Time Out!');
                    document.getElementById("lblAlert").innerHTML = "Time Out!";
                    $('#myCommonAlertModal').modal('show');
                    __doPostBack('btnSubmit', 'OnClick');
                    document.getElementById("btnsubNEw").style.visibility = "visible";
                    document.getElementById("<%=btnsubNEw.ClientID %>").click();

                    // window.location.href = 'Thanks.aspx';


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



    </script>

    <script type="text/javascript">

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




    </script>

    <script type="text/javascript">
        function DisableButton() {
            //debugger;
            document.getElementById("<%=btnSave.ClientID%>").disabled = true;
            document.getElementById("<%=btnNext.ClientID%>").disabled = true;
            document.getElementById("<%=btnReview.ClientID%>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>


    <%--<script type="text/javascript">
    var submit = 0;
    function CheckDouble() {
        if (++submit > 1) {
            alert('This sometimes takes a few seconds - please be patient.');
            return false;
        }
    }
    OnClientClick="return CheckDouble();"
 </script>--%>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <style>
        .fieldset {
            border: 1px solid orange;
        }

        .myCheckBoxList input {
            font: inherit;
            font-size: 0.875em; /* 14px / 16px */
            color: #494949;
            margin-bottom: 12px;
            margin-top: 5px;
            margin-right: 10px !important;
        }

        .myRadioButtonList input {
            font: inherit;
            font-size: 0.875em; /* 14px / 16px */
            color: #494949;
            margin-bottom: 12px;
            margin-top: 5px;
            margin-right: 10px !important;
        }
    </style>

    <style>
        .q-pad {
            display: flex;
            justify-content: space-between;
            flex-wrap: wrap;
        }

            .q-pad .btn {
                border-radius: 50%;
                height: 40px;
                width: 40px;
                text-align: center;
                margin-bottom: 5px;
                line-height: 2;
            }

        @media only screen and (max-width:768px) {
            .q-pad {
                display: flex;
                justify-content: space-between;
                flex-wrap: wrap;
            }

                .q-pad .btn {
                    width: 20%;
                    border-radius: 0;
                    margin: 2px;
                }
        }

        @media only screen and (max-width:767px) {
            .box-footer .btn {
                margin-bottom: 5px;
                width: 49%;
            }
        }
    </style>

</head>


<body class="content-wrapper" id="body1">

    <form id="form1" runat="server"  >
        <input type="hidden" name="visited" value="" />
        <asp:ScriptManager ID="ScriptManager_Main" runat="server" EnablePartialRendering="true"
            EnablePageMethods="true">
        </asp:ScriptManager>
        <div class="container">

            <div class="row mt-3">
                <asp:UpdatePanel ID="UpdatePanel_Login" runat="server">
                    <ContentTemplate>
                        <div class="col-12">
                            <div class="row">
                                <div class="col-md-9 col-12">
                                    <div class="col-12 mb-2" style="padding: 15px; background:#fff; box-shadow: 0 0 8px #ccc; box-shadow: rgb(0 0 0 / 20%) 0px 0px 5px; border-radius: 10px;">
                                        <div class="sub-heading">
                                            <h5>Candidate Details</h5>
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Student Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblUrname" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-12 col-md-12 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Session Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="col-lg-12 col-md-12 col-12 ">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Course Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>


                                                <div class="col-lg-6 col-md-12 col-12" style="display: none">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Enrollment No:</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblEnroll" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>


                                                <div id="Div1" class="form-group col-md-12" runat="server" visible="false">
                                                    <asp:Label ID="lblid" runat="server"></asp:Label>
                                                    <asp:Label ID="lblQueNo" Font-Bold="true" runat="server" Width="232px"></asp:Label>
                                                    <asp:HiddenField ID="hdnlastfive" runat="server" />
                                                </div>

                                                <div class="col-lg-4 col-md-4 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Seat No :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblSeatno" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="col-lg-4 col-md-4 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Test Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblTestName" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Time Remaining:</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblTimerCount" Font-Bold="true" runat="server" Style="font-size: large;"></asp:Label>

                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-12 mt-2">
                                            <div class="form-group col-md-12">
                                                Instruction <b>:</b> <span style="color: #FF0000; font-weight: bold">Please don't close the
                                                    browser once you started the test else your attempt will be lost !</span>
                                            </div>
                                        </div>


                                        <div class="col-12 mt-4">
                                            <div class="sub-heading">
                                                <h5>
                                                    <asp:Label runat="server" ID="Label896" Font-Size="Medium"> Question No :&nbsp; </asp:Label>
                                                    <asp:Label ID="Label1" Font-Size="Medium" runat="server" Text="1"></asp:Label></h5>
                                            </div>
                                            <hr />
                                            <div class="box-tools pull-right">
                                                <b class="label label-success p-2" style="font-size: large; background: #1d9335; color: #fff;">
                                                    <asp:Label ID="Label3" runat="server" Text="1"></asp:Label>/<asp:Label ID="Label4" runat="server" Text="1"></asp:Label></b>
                                            </div>

                                            <div class="col-12">
                                                <h4>
                                                    <asp:Label ID="Label2" runat="server" Text="What is Chemistry?"></asp:Label></h4>
                                                <asp:Image ID="imgQuesImage" runat="server" CssClass="img-responsive"
                                                    onclick="DisplayAns1InWidnow();" EnableTheming="False" />
                                            </div>

                                            <%--<div class="form-group col-md-3 text-right">Answer</div>--%>
                                            <div class="col-12">
                                                <asp:RadioButtonList ID="rdBtnQuesList" runat="server" CssClass="myRadioButtonList" RepeatDirection="Vertical">
                                                    <asp:ListItem Text="Ans1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Ans2"></asp:ListItem>
                                                    <asp:ListItem Text="Ans3"></asp:ListItem>
                                                    <asp:ListItem Text="Ans4"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:CheckBoxList ID="ckBtnQuesList" CssClass="myCheckBoxList" runat="server" RepeatDirection="Vertical">
                                                </asp:CheckBoxList>
                                            </div>

                                        </div>

                                        <div class="col-12 btn-footer mt-2">
                                            <asp:LinkButton ID="btnPrev" runat="server" CssClass="btn btn-info" OnClick="btnPrev_Click"><i class="fa fa-backward"></i> Previous</asp:LinkButton>
                                            <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click"><i class="fa fa-save"></i> Save & Next</asp:LinkButton>
                                            <asp:LinkButton ID="btnNext" runat="server" CssClass="btn btn-warning" OnClick="btnNext_Click"><i class="fa fa-forward"></i> Skip</asp:LinkButton>
                                            <asp:LinkButton ID="btnClear" runat="server" CssClass="btn btn-outline-danger" OnClick="btnClear_Click"><i class="fa fa-eraser"></i> Clear</asp:LinkButton>
                                            <asp:LinkButton ID="btnReview" runat="server" CssClass="btn btn-outline-primary" OnClick="btnReview_Click"><i class="fa fa-eye"></i> Mark As Review</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-success" OnClick="btnSubmit_Click"><i class="fa fa-lock"></i> Finish Test</asp:LinkButton>
                                            <asp:LinkButton ID="btnsubNEw" runat="server" OnClick="btnSubmitnew_Click" CssClass="btn btn-default"></asp:LinkButton>
                                            <asp:LinkButton ID="btnUpdateMalFunction" runat="server" OnClick="btnUpdateMalFunction_Click" CssClass="btn btn-default" Visible="false"></asp:LinkButton>
                                            <asp:HiddenField ID="hdnMalfunction" runat="server" />
                                            <asp:HiddenField ID="hdnMalfunctionStudent" runat="server" />

                                        </div>

                                    </div>
                                </div>

                                <div class="col-md-3 col-12">
                                    <div class="col-12 mb-2" style="padding: 15px; background:#fff; box-shadow: 0 0 8px #ccc; box-shadow: rgb(0 0 0 / 20%) 0px 0px 5px; border-radius: 10px;">
                                        <div class="sub-heading">
                                            <h5>Preview</h5>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="q-pad">
                                                <asp:ListView ID="lvPreview" runat="server">
                                                    <LayoutTemplate>
                                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnQuesStatus" runat="server" OnClick="btnQuesStatus_Click" CssClass='<%# Eval("ANS_STAT") %>' CommandName='<%# Eval("SRNO") %>' CommandArgument='<%# Eval("QUESTIONNO") %>'><%# Eval("SRNO") %></asp:LinkButton>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <%--</div>--%>

                                        <div class="col-12 mt-3">
                                            <span style="height: 25px; width: 25px; background-color: green; border-radius: 50%; display: inline-block;"></span>
                                            Answer :
                                                <asp:Label ID="lblAnsCount" runat="server" Style="font-weight: 600; font-size: large"></asp:Label>
                                        </div>

                                        <div class="col-12">
                                            <span style="height: 25px; width: 25px; background-color: #3399ff; border-radius: 50%; display: inline-block;"></span>
                                            Review :
                                                <asp:Label ID="lblRevCount" runat="server" Style="font-weight: 600; font-size: large"></asp:Label>
                                        </div>

                                        <div class="col-12">
                                            <span style="height: 25px; width: 25px; background-color: orange; border-radius: 50%; display: inline-block;"></span>
                                            Skip :
                                                <asp:Label ID="lblSkipCount" runat="server" Style="font-weight: 600; font-size: large;"></asp:Label>
                                        </div>

                                        <div class="col-12">
                                            <span style="height: 25px; width: 25px; background-color: red; border-radius: 50%; display: inline-block;"></span>
                                            Not Answer :
                                                <asp:Label ID="lblNotAnsCount" runat="server" Style="font-weight: 600; font-size: large"></asp:Label>
                                        </div>

                                        <div class="col-12 text-right">
                                            <asp:LinkButton ID="btnCloseModal" Visible="false" runat="server" CssClass="btn btn-outline-danger" Style="border-radius: 10px;" OnClick="btnCloseModal_Click"><i class="fa fa-close"></i> Close</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="dvIntermediate" visible="false" runat="server" style="position: fixed; z-index: 50; width: 100%; min-height: 100%; top: 0%; left: 0%; background: rgba(0, 0, 0, 0.3);"></div>
                        <div id="dvShowTestStatus" class="box box-info col-md-12 bg-aqua-gradient" visible="false" runat="server" style="position: fixed; z-index: 100; border-radius: 20px; width: 52%; top: 20%; left: 24%; box-shadow: 0 0 30px black;">
                        </div>
                        <div id="divMsg" runat="server"></div>

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


                        <div class="modal fade" id="myModel" tabindex="-1" role="dialog" aria-labelledby="myModelTest" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: red">
                                        <h4 class="modal-title" id="myModelTest"><span class="glyphicon glyphicon-exclamation-sign"></span>Alert !</h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblFinishTest" runat="server">Are you sure you want to Submit Test? Once it is submitted, you will not be able to modify</asp:Label>
                                    </div>

                                    <div class="modal-footer">
                                        <asp:Button ID="btnOk" runat="server" Text="Finish Test" OnClick="btnOk_Click"
                                            CssClass="btn btn-success" TabIndex="8" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            CssClass="btn btn-outline-danger" data-dismiss="modal" TabIndex="8" />
                                    </div>

                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <asp:PostBackTrigger ControlID="btnNext" />
                        <asp:PostBackTrigger ControlID="btnReview" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

        </div>
    </form>
</body>

</html>
