<%@ OutputCache Duration="5" VaryByParam="None" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_ViewFaculty.aspx.cs"
    Inherits="Itle_Test_ViewFaculty" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>University Administration and Information Management System</title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
    <link href="../css/master.css" rel="stylesheet" type="text/css" />
    <link href="../themes/start/jquery-ui-1.8.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/Theme1.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" language="javascript" src="Client_Side_Validn.js"></script>
--%>

<style type="text/css">
*{
 font-size: 100%;
 font-family: Arial;
 font-weight:normal;
}

</style>
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

    <script type="text/javascript" language="JavaScript1.2">

        if (document.all) {
            document.onkeydown = function() {
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
                window.location.href = 'Thanx.aspx';
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
            document.onkeydown = function() {
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

            myTimer.prototype.go = function() {
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
                    alert('Time Out!');

                    //                    function ShowCurrentTime() {
                    //used to call server method...
                    // PageMethods.GetCurrentTime("1");

                    //}
                    //                    function OnSuccess(response, userContext, methodName) {
                    //                        alert(response);
                    //                    }
                    //document.getElementById('RadioButtonList1').disabled = true;
                    //document.getElementById('Button1').disabled = true;
                    //document.getElementById('Button3').disabled = true;
                    //this.raiseEvent(Button3_Click, '');
                    //$("#Button3").click
                    window.location.href = 'Thanx.aspx';


                    return false;
                }



            }
            myTimer.prototype.stop = function() {
                this.stopped = true;
                if (this.currentTimeOut != null) {
                    clearTimeout(this.currentTimeout);
                }
            }
            myTimer.prototype.Hours = function(value) {
                return Math.floor(value / 3600000);
            }
            myTimer.prototype.Minutes = function(value) {
                return Math.floor((value - (this.Hours(value) * 3600000)) / 60000);
            }
            myTimer.prototype.Seconds = function(value) {
                var hoursMillSecs = (this.Hours(value) * 3600000)
                var minutesMillSecs = (this.Minutes(value) * 60000)
                var total = (hoursMillSecs + minutesMillSecs)
                var ans = Math.floor(((this.value - total) % 60000) / 1000);

                if (ans < 10)
                    return "0" + ans;

                return ans;
            }
        }



        document.onkeydown = function() {
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
    
   
</head>
<body class="body-main">
    <form id="form1" runat="server">
    <input type="hidden" name="visited" value="" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%" style="background-color: #ffffff"
                class="table-formdata">
                <tr>
                    <td style="width: 100%">
                        <table cellpadding="0" cellspacing="0" width="100%" class="table-formdata">
                            <tr>
                                <td style="width: 20%">
                                </td>
                                <td style="width: 60%">
                                    <table style="width: 100%" id="Table1">
                                        <tr>
                                            <td colspan="2" style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                Session Name<b>:</b>
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 50%; text-align: right">
                                                Course Name<b>:</b>
                                                <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <t>
                                            <td style="width: 50%">
                                                Test Name<b>:</b>
                                                <asp:Label ID="lblTestName" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 50%; text-align: right" runat="server" visible="false">
                                                Remaining Time<b>:</b>
                                                <asp:Label ID="lblTimerCount" Font-Bold="True" runat="server"></asp:Label>
                                                <%--<asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick">
            </asp:Timer>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Welcome<b>:</b>
                                                <asp:Label ID="lblUrname" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Seat No<b>:</b>
                                                <asp:Label ID="lblSeatno" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server" visible="false">
                                            <td class="style2">
                                            </td>
                                            <td style="width: 100px; height: 1px;">
                                                <asp:Label ID="lblQueNo" Font-Bold="True" runat="server" Width="232px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%;">
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%"  Font-Bold="false">
                                                    <fieldset class="fieldset">
                                                        <legend class="ui-widget-header">Online Test</legend>
                                                        <table id="table2" cellpadding="1" cellspacing="1" style="background-color:#EAF0FF;" class="table-formdata" width="100%" >
                                                            <tr>
                                                                <td style="vertical-align: top; padding-left: 10px; padding-right: 10px;">
                                                                    <asp:ListView ID="lvTest" runat="server">
                                                                        <LayoutTemplate>
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <th style="text-align: left; width: 10%">
                                                                                        Sr. No
                                                                                    </th>
                                                                                    <th style="text-align: left; width: 90%">
                                                                                        Question
                                                                                    </th>
                                                                                </tr>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr style="font-family:georgia,serif;">
                                                                                <td style="text-align: left; vertical-align: text-top; width: 10%">
                                                                                    <br />
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("QU_SRNO")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left; width: 90%">
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
                                                <asp:Button ID="btnPrintObjTest" runat="server" Text="Print" OnClientClick="javascript:window.print();"
                                                    OnClick="btnPrint_Click" />
                                                &nbsp;&nbsp;
                                                <input id="btnCloseObj" type="button" runat="server" value="Close" onclick="javascript:window.close();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20%">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
    <form id="form2" runat="server" visible="false">
    <input type="hidden" name="visited" value="" />
    <%-- <asp:ScriptManager ID="ScriptManager_Main" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel_Login" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%" style="background-color: #ffffff"
                class="datatable">
                <tr>
                    <td style="width: 100%">
                        <table cellpadding="0" cellspacing="0" width="100%" class="table-formdata">
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 90%">
                                    <table style="width: 100%" id="Table_Main">
                                        <tr>
                                            <td colspan="2" style="height: 25px">
                                                Note <b>:</b> <span style="color: #FF0000; font-weight: bold">Please don't close the
                                                    browser once you started the test else your attempt will be lost !</span><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                Session Name<b>:</b>
                                                <asp:Label ID="lblSessionfrm2" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 50%; text-align: left">
                                                Course Name<b>:</b>
                                                <asp:Label ID="lblCoursefrm2" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                Test Name<b>:</b>
                                                <asp:Label ID="lblTestNamefrm2" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 50%; text-align: left" runat="server" visible="false">
                                                Remaining Time<b>:</b>
                                                <asp:Label ID="lblTimerCountfrm2" Font-Bold="true" Visible="true" runat="server"></asp:Label>
                                                <%--<asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick">
                                                    </asp:Timer>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                Welcome<b>:</b>
                                                <asp:Label ID="lblUrnamefrm2" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 50%; text-align: left" runat="server" visible="false">
                                                Total Time for Test<b>:</b>
                                                <asp:Label ID="lblTotTime" Font-Bold="true" runat="server"></asp:Label>
                                                <%--<asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick">
                                                    </asp:Timer>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Seat No<b>:</b>
                                                <asp:Label ID="lblSeatnofrm2" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="tr" runat="server" visible="false">
                                            <td>
                                            </td>
                                            <td style="width: 100px; height: 1px;">
                                                <asp:Label ID="Label9" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                            </td>
                                            <td style="width: 50%; text-align: left">
                                                <asp:Label ID="lblTotalMarks" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <asp:Panel ID="pnlSlot" runat="server" ScrollBars="Auto" Width="100%">
                                                    <fieldset class="fieldset">
                                                        <legend class="ui-widget-header">Online Test</legend>
                                                        <table id="table3" cellpadding="1" cellspacing="1" class="table-formdata" width="100%">
                                                            <tr>
                                                                <td style="vertical-align: top; width: 50%; padding-left: 10px; padding-right: 10px">
                                                                    <asp:ListView ID="ListView1" runat="server"  OnItemCommand="lvTest_OnItemCommand">
                                                                        <LayoutTemplate>
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <th style="text-align: left; width: 15%">
                                                                                        Sr.No.
                                                                                    </th>
                                                                                    <th style="text-align: left; width: 70%">
                                                                                        Question
                                                                                    </th>
                                                                                    <th style="text-align: left; width: 10%">
                                                                                        Marks
                                                                                    </th>
                                                                                </tr>
                                                                                <br></br>
                                                                                <br></br>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: left; vertical-align: text-top; width: 15%">
                                                                                    <br />
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("QU_SRNO")%>'></asp:Label>
                                                                                </td>
                                                                                <td id="Qestion" runat="server" style="text-align: left; width: 100%; height: auto">
                                                                                    <br />
                                                                                    <%--<asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>--%>
                                                                                    <asp:Label ID="lblQuestions" runat="server" ForeColor="black" Font-Bold="true" Text='<%# Eval("QUESTIONTEXT")%>'
                                                                                        SkinID="lblQuestion"></asp:Label>
                                                                                    <hr style="margin-left: 1%" />
                                                                                    <br></br>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("MARKS_FOR_QUESTION")%>'></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:ImageButton ID="btnAnswer" runat="server" ImageUrl="../IMAGES/redArrow1.jpg" OnClick="btnAnswer_Click" Width="30px" Height="30px"
                                                                                        AlternateText='<%# Eval("MARKS_FOR_QUESTION")%>' CommandArgument='<%# Eval("QUENO")%>'
                                                                                        ToolTip='<%# Eval("QUESTIONTEXT")%>' />
                                                                                </td>
                                                                                <%--<td>
                                                                                    <asp:Image ID="imgQuesImage" runat="server" Width="70px" ImageUrl='<%# Eval("QUESTIONIMAGE")%>'
                                                                                        onclick="DisplayAns1InWidnow();" EnableTheming="False" ImageAlign="Left" />
                                                                                    <hr style="margin-left: 1%" />
                                                                                </td>--%>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </td>
                                                                <td style="width: 50%">
                                                                    <div id="divDescriptiveAnswer" runat="server" visible="false">
                                                                        <asp:Panel ID="PnlList" runat="server" Height="600px" ScrollBars="Auto">
                                                                            <div style="width: 80%; padding: 10px">
                                                                                <table cellpadding="1" cellspacing="1" width="100%">
                                                                                    <tr>
                                                                                        <td style="width: 15%">
                                                                                            <b>Question</b>
                                                                                        </td>
                                                                                        <td style="width: 5%">
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td style="width: 90%">
                                                                                            <asp:Label ID="lblQuestion" runat="server" Font-Bold="true" ForeColor="#990000" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 15%">
                                                                                            <b>Answer</b>
                                                                                        </td>
                                                                                        <td style="width: 5%">
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td style="width: 90%">
                                                                                            <FTB:FreeTextBox ID="txtAnswer" runat="server" Width="320px" Height="400px">
                                                                                            </FTB:FreeTextBox>
                                                                                            <%-- onKeyDown="return disableCtrlKeyCombination(event);"--%>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 20px">
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 15%">
                                                                                        </td>
                                                                                        <td style="width: 3%">
                                                                                        </td>
                                                                                        <td style="width: 80%">
                                                                                            <center>
                                                                                                <img id="btnSymbol" runat="server" alt="Insert Symbol" visible="true" src="../images/RedOmega.jpeg"
                                                                                                    style="width: 20px; height: 20px" onclick="javascript:InsertSymbol('divKeyBoard')" />
                                                                                                &nbsp; &nbsp; &nbsp;
                                                                                                <asp:Button ID="btnBack" runat="server" Text="Back" Enabled="false"></asp:Button>
                                                                                                &nbsp; &nbsp; &nbsp;
                                                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Enabled="false"></asp:Button>
                                                                                                &nbsp; &nbsp; &nbsp;
                                                                                                <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#ff6600" ForeColor="Black"
                                                                                                    Enabled="false"></asp:Button>
                                                                                            </center>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 15%">
                                                                                        </td>
                                                                                        <td style="width: 3%">
                                                                                        </td>
                                                                                        <td style="width: 80%">
                                                                                            <asp:Label ID="lblStatus" runat="server" Text="Answer Saved..." Visible="false" Font-Bold="true"
                                                                                                ForeColor="#006600"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <div id="divKeyBoard" style="display: none">
                                                                        <iframe src="Virtual_Keyboard.htm" width="550px" height="450px"></iframe>
                                                                    </div>
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
                                                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="javascript:window.print();"
                                                    OnClick="btnPrint_Click" />
                                                &nbsp;&nbsp;
                                                <input id="btnClose" type="button" runat="server" value="Close" onclick="javascript:window.close();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20%">
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
        popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,left=200,top=200,menuBar=0,scrollbars=0,resizable=1');
        popup.document.open();
        popup.document.write(html);
        popup.document.focus();
        popup.document.close();
        popup.document.close();
    }

    //Used to print Created Test
    function printing() {
        window.print();

    }

</script>

</html>
