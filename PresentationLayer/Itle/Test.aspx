<%@ OutputCache Duration="5" VaryByParam="None" %>

<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="Test.aspx.cs" Inherits="Itle_test" %>



<%--<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>--%>
<%--<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>--%>
<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>University Administration and Information Management System</title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
    <link href="../css/master.css" rel="stylesheet" type="text/css" />
    <link href="../themes/start/jquery-ui-1.8.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/Theme1.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" language="javascript" src="Client_Side_Validn.js"></script>
    --%>
    <style type="text/css">
        * {
            font-size: 100%;
            font-family: Arial;
            font-weight: normal;
        }
    </style>
    <script type="text/javascript" language="JavaScript1.2">

        if (document.all) {
            document.onkeydown = function () {
                var key_f5 = 116; // 116 = F5

                if (key_f5 == event.keyCode) {
                    event.keyCode = 27;

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
                var key_f5 = 116; // 116 = F5

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
            debugger;
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
                    }
                }
            }

            myTimer.prototype.go = function () {
                debugger;
                if (this.value > 0 && this.stopped == false) {
                    this.value = (this.value - this.interval);
                    if (this.data) {
                        this.data.value = this.value;
                        //                        Session["inttime"] = this.value;
                        //                        alert(this.value);
                    }
                    var current = this.value;
                    this.OutputCntrl.innerHTML = this.Hours(current) + ':' + this.Minutes(current) + ':' + this.Seconds(current);
                    if (this.OutputCntrl.innerHTML == '0:0:59')
                        alert('One minute remaining');

                    this.currentTimeOut = setTimeout("Timer.go()", this.interval);
                }
                else {
                    //                    $(document).reaDDOdy(function() {
                    //
                    //                    $('#btnSubmit').click();
                    //                   document.getElementById('<%=btnSubmit.ClientID%>').Click();
                    __doPostBack('btnSubmit', 'OnClick');
                    alert('Time Out!');

                    //                    function ShowCurrentTime() {
                    //used to call server method...
                    // PageMethods.GetCurrentTime();
                    //}
                    //                    function OnSuccess(response, userContext, methodName) {
                    //                        alert(response);
                    //                    }
                    //document.getElementById('RadioButtonList1').disabled = true;
                    //document.getElementById('Button1').disabled = true;
                    //document.getElementById('Button3').disabled = true;
                    //this.raiseEvent(Button3_Click, '');
                    //$("#Button3").click
                    //window.location.href = 'Thanks.aspx';


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

    <script type="text/javascript">
        function fullscrren() {

            var elem = document.getElementById("form1");
            if (elem.requestFullscreen) {
                elem.requestFullscreen();
            } else if (elem.msRequestFullscreen) {
                elem.msRequestFullscreen();
            } else if (elem.mozRequestFullScreen) {
                elem.mozRequestFullScreen();
            } else if (elem.webkitRequestFullscreen) {
                elem.webkitRequestFullscreen();
            }




        }
    </script>

    <style type="text/css">
        .style5 {
            width: 50%;
            height: 16px;
        }
    </style>
    <style type="text/css">
        .visibleDiv, #topLeft, #topRight, #bottomLeft, #bottomRight {
            position: fixed;
            width: 190px;
            height: 100px;
            border: solid 1px #e1e1b1;
            vertical-align: middle;
            /*background: #ffdab9;*/
            background: #C2F0C2;
            text-align: center;
            border:2px solid black;
        }

        #topLeft {
            top: 10px;
            left: 10px;
        }

        #topRight {
            top: 10px;
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




</head>
<body class="body-main" onload="f1()">
    <form id="form1" runat="server">
        <input type="hidden" name="visited" value="" />
        <asp:ScriptManager ID="ScriptManager_Main" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel_Login" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%" style="background-color: #ffffff"
                    class="datatable">
                    <tr>

                        <td style="width: 100%">
                            <table cellpadding="0" cellspacing="0" width="100%" class="table-formdata">
                                <tr>
                                    <td style="width: 20%"></td>
                                    <td style="width: 60%">
                                        <table style="width: 100%" id="Table_Main">
                                            <tr>
                                                <td colspan="2" style="height: 25px">Note <b>:</b> <span style="color: #FF0000; font-weight: bold">Please don't close the browser once you started the test else your attempt will be lost !</span><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Session Name<b>:</b>
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="width: 50%; text-align: right">Course Name<b>:</b>
                                                    <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style5">Test Name<b>:</b>
                                                    <asp:Label ID="lblTestName" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="text-align: right" class="style5">

                                                    <%--<asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick">
            </asp:Timer>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Welcome<b>:</b>
                                                    <asp:Label ID="lblUrname" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Seat No<b>:</b>
                                                    <asp:Label ID="lblSeatno" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="tr" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblid" runat="server"></asp:Label>
                                                </td>
                                                <td colspan="3" style="width: 100px; height: 1px;">
                                                    <asp:Label ID="lblQueNo" Font-Bold="true" runat="server" Width="232px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 20px"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100%">
                                                    <asp:Panel ID="pnlSlot" runat="server" ScrollBars="Auto" Width="100%">
                                                        <fieldset class="fieldset">
                                                            <legend class="ui-widget-header">Online Test</legend>
                                                            <table id="table3" cellpadding="1" cellspacing="1" class="table-formdata" width="100%">
                                                                <tr>
                                                                    <td style="vertical-align: top; padding-left: 10px; padding-right: 10px">
                                                                        <asp:ListView ID="lvTest" runat="server">
                                                                            <LayoutTemplate>
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <th style="text-align: left; width: 10%">Sr. No
                                                                                        </th>
                                                                                        <th style="text-align: left; width: 90%">Question
                                                                                        </th>
                                                                                    </tr>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr style="border:1px solid black;">
                                                                                    <td style="text-align: left; vertical-align: text-top; width: 10%">
                                                                                        <br />
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("QU_SRNO")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left; width: 90%"; style="border:1px solid black">
                                                                                        <br />
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("QUESTIONTEXT")%>'></asp:Label>
                                                                                        <asp:Image ID="imgQuesImage" runat="server" Width="200px" ImageUrl='<%# Eval("QUESTIONIMAGE")%>'
                                                                                            onclick="DisplayAns1InWidnow();" EnableTheming="False" />
                                                                                        <br />
                                                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="false" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                                                        </asp:RadioButtonList>
                                                                                        <hr style="margin-left: 1%" />
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="font-family: Verdana; font-weight: normal; text-align: center">
                                                    <%--<asp:Button ID="Button1"
                                                    runat="server" Enabled="false" />--%>
                                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                                        OnClientClick="return confirm('Are you sure you want to Submit Test, Once it is submmited you will not be able to modify it?');" />
                                                    <%-- <asp:Button ID="btnhdn"
                                                    runat="server" OnClick="btnhdn_Click"/>--%>
                                                    <%--   <asp:HiddenField ID="btnhdn"
                                                    runat="server" OnClick="btnhdn_Click" Text="hdn" />--%>

                                                    <%--<input id="btnGetTime" type="button" value="Show Current Time" onclick="ShowCurrentTime()"/>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 20%">
                                        <div id="topRight">
                                        <%--   <h1> <b>Time :</b>--%>
                                            <asp:Label ID="lblTimerCount" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                           <%-- <asp:Timer ID="timer1" runat="server" 
                                                Interval="1000" OnTick="timer1_Tick1">
                                            </asp:Timer>--%>
                                            <br />

                                            <asp:Label ID="lblTimer" runat="server"></asp:Label>

                                            <asp:Label ID="lblTestDuration" runat="server" Visible="false"></asp:Label>
                                            <div id="starttime"></div>
                                           
                                            <div id="endtime" runat="server" visible="false"></div>
                                            <br />
                                            <div id="showtime"></div> </h1>


                                        </div>

                                    </td>
                                </tr>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>

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
        popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,left=200,top=200,menuBar=0,scrollbars=0,resizable=1,status=0');
        popup.document.open();
        popup.document.write(html);
        popup.document.focus();
        popup.document.close();
        popup.document.close();
    }



</script>


<%-- sachin 24june2017 --%>


<script type="text/javascript">
    var tim;
    var min = '<%=Session["totMin"]%>';
     min = min - 1;
    var sec = 60;
    var f = new Date();
    function f1() {
        f2();
        var txt = "Start Exam at :";
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
                    //location.href = "Thanks.aspx";
                    //location.href = "Result.aspx";
                    __doPostBack('btnSubmit', 'OnClick');
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

<%--  --%>
</html>
