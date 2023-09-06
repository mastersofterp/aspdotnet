<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SeatingPlanNew_RCPIT.aspx.cs" Inherits="ACADEMIC_SEATINGARRANGEMENT_SeatingPlanNew" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--    <script src="../../INCLUDES/prototype.js" type="text/javascript"></script>

    <script src="../../INCLUDES/scriptaculous.js" type="text/javascript"></script>

    <script src="../../INCLUDES/modalbox.js" type="text/javascript"></script>--%>

    <style>
        #tblroomcapacity.table-bordered > thead > tr > th,
        #divsessionlist.table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblExamCourses').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblExamCourses').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblExamCourses').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblExamCourses').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblExamCourses').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblExamCourses').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblExamCourses').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblExamCourses').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>

    <script type="text/javascript">

        function CheckAll(txtmain, headid) {
            AsignValue(txtmain, headid);
        }
        function chkIndividualRoom(txtchild, headid) {
            AsignValue(txtchild, headid);
        }
        function AsignValue(txt, headid) {
            try {
                var txtSelectedRoomCap = document.getElementById('<%= lblroomcapacity.ClientID %>');
                var hdfSelectedRommCap = document.getElementById('<%= hfroomcapacity.ClientID %>');
                var rmcap = 0;

                tbl = document.getElementById('tblroomcapacity');
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        debugger;
                        var chkid = 'ctl00_ContentPlaceHolder1_lvRoomDetails_ctrl' + i + '_chckroom';
                        var hfActualCapacity = 'ctl00_ContentPlaceHolder1_lvRoomDetails_ctrl' + i + '_hfActualCapcity';

                        if (headid == 1) {

                            if (txt.checked) {
                                document.getElementById(chkid).checked = true;
                                rmcap += Number(document.getElementById(hfActualCapacity).value)
                            }
                            else {
                                document.getElementById(chkid).checked = false;
                            }
                        }
                        else if (headid == 2) {

                            if (document.getElementById(chkid).checked == true) {
                                rmcap += Number(document.getElementById(hfActualCapacity).value)
                            }
                        }
                    }
                    txtSelectedRoomCap.value = rmcap;
                    hdfSelectedRommCap.value = rmcap;
                }
            }
            catch (e) {
                alert(e);
            }
        }



        function DuplicateNoNotAllowed(element) {
            debugger;
            alert('dupnofun');
            var textValues = new Array();
            $("input[name='ctl00$ContentPlaceHolder1$lvRoomDetails$ctrl0$txtRoomSrNo']").each(function () {
                debugger;
                doesExisit = ($.inArray($(this).val(), textValues) == -1) ? false : true;
                console.log(textValues)
                if (!doesExisit) {
                    textValues.push($(this).val())
                } else {
                    alert('dup');
                    return false;
                }
            });
        };




        //function CheckIfSequenceEntered() {
        //    debugger

        //    $("#tblroomcapacity TBODY TR").each(function () {

        //        var MainPerc = $(this).find('td').find("input").eq(5).val();
        //        alert(MainPerc);
        //    });

        //try {
        //    var seqvalues = "";
        //    var tbl = document.getElementById('tblroomcapacity');
        //    var dataRows = tbl.getElementsByTagName('tr');
        //    var lv = 'lvRoomDetails';
        //    if (dataRows != null) {
        //        for (i = 0; i < dataRows.length - 1; i++) {
        //            //debugger;
        //            var chkroom = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_chckroom';
        //            var txtRoomId = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_txtRoomSrNo';
        //            if (document.getElementById(chkroom).checked == true) {
        //                seqvalues = document.getElementById(txtRoomId).value;
        //            }
        //            else {
        //                seqvalues = "";
        //            }
        //        }
        //    }
        //    if (seqvalues.trim().length > 0)
        //        return true;
        //    else
        //        return false;
        //}
        //catch (e) {
        //    alert(e);
        //}
        //  }

        //Ebnable text box sequence
        function EnableTextBox(txt) {
            try {


                var tbl = document.getElementById('tblroomcapacity');
                var dataRows = tbl.getElementsByTagName('tr');
                var lv = 'lvRoomDetails';
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        debugger;
                        var chkroom = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_chckroom';
                        var txtRoomId = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_txtRoomSrNo';
                        if (document.getElementById(chkroom).checked == true) {
                            document.getElementById(txtRoomId).disabled = false;
                        }
                        else {
                            document.getElementById(txtRoomId).disabled = true;
                        }
                    }

                }
            }

            catch (e) {
                alert(e);
            }
        }


        function IsNumberExist(txt, headid) {
            debugger
            try {
                //if (headid == 1) {
                //    var tbl = document.getElementById('tblExamCourses');
                //    var dataRows = tbl.getElementsByTagName('tr');
                //    var lv = 'lvExamCoursesOnDate';
                //    var txtboxid = 'txtSrNo';
                //}
                if (headid == 2) {
                    var tbl = document.getElementById('tblroomcapacity');
                    var dataRows = tbl.getElementsByTagName('tr');
                    var lv = 'lvRoomDetails';
                    var txtboxid = 'txtRoomSrNo';
                }
                var SeqExist = "";
                if (dataRows != null) {

                    //forEach (i = 0; i < dataRows.length - 1; i++)
                    for (var i = 0; i < dataRows.length - 1; i++) {

                        debugger;
                        var SrNo = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_' + txtboxid;
                        var SrNoValue = document.getElementById(SrNo).value;
                        if (SeqExist == SrNoValue && SeqExist != "") {
                            alert('Sequence ' + SeqExist + ' already exist.')
                            document.getElementById(SrNo).value = '';
                            // break;
                        }
                        else {
                            SeqExist = SrNoValue;
                        }
                    }
                }
                SeqExist = "";
            }
            catch (e) {
                alert(e);
            }
        }


        


        //$("txtRoomSrNo").change(function () {
        //    alert("The text has been changed.");
        //});


        ////Check Duplicate Sequence
      


        //////Check Duplicate Sequence
        //function IsNumberExist(txt, headid) {
        //   // debugger
        //    try {
        //        //if (headid == 1) {
        //        //    var tbl = document.getElementById('tblExamCourses');
        //        //    var dataRows = tbl.getElementsByTagName('tr');
        //        //    var lv = 'lvExamCoursesOnDate';
        //        //    var txtboxid = 'txtSrNo';
        //        //}
        //        if (headid == 2) {
        //            var tbl = document.getElementById('tblroomcapacity');
        //            var dataRows = tbl.getElementsByTagName('tr');
        //            var lv = 'lvRoomDetails';
        //            var txtboxid = 'txtRoomSrNo';
        //        }
        //        var SeqExist = "";
        //        if (dataRows != null) {

        //            for (i = 0; i < dataRows.length-1; i++) {
        //                debugger;
        //                var SrNo = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_' + txtboxid;
        //                var SrNoValue = document.getElementById(SrNo).value;
        //                for (j = 1; j < dataRows.length-1; j++) {
        //                    var SrNo2 = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + j + '_' + txtboxid;
        //                    var SrNoValue2 = document.getElementById(SrNo2).value;
        //                    if (SrNoValue == SrNoValue2 && SrNoValue != '' && SrNoValue2 !='')
        //                    {
        //                        alert('Sequence ' + SeqExist + ' already exist.')
        //                        document.getElementById(SrNo2).value = '';
        //                    }
        //                }
        //                //if (SeqExist == SrNoValue && SeqExist != "") {
        //                //    alert('Sequence ' + SeqExist + ' already exist.')
        //                //    document.getElementById(SrNo).value = '';
        //                //    //break;
        //                //}
        //                //else {
        //                //    SeqExist = SrNoValue;
        //                //}
        //            }
        //        }
        //        SeqExist = "";
        //    }
        //    catch (e) {
        //        alert(e);
        //    }
        //}


        //validate form
        function validateForm() {
            //  debugger;
            var hdStudCount = document.getElementById('<%= hdStudCount.ClientID %>');
            var RoomStrength = document.getElementById('<%=hfroomcapacity.ClientID %>')
            //if (Number(hdStudCount.value) >= Number(RoomStrength.value)) {
            //    alert('Students Strength is exceeding the room capacity.')
            //    return false;
            //}
            //else {
            counter = 0;
            var StudOnBench = document.getElementsByName("ctl00$ContentPlaceHolder1$rbOnBench")
            var x = document.getElementsByName("ctl00$ContentPlaceHolder1$rbOnBench").length;
            if (x > 0) {
                for (i = 0; i < x ; i++) {
                    var SeatingCount = 'ctl00_ContentPlaceHolder1_rbOnBench_' + i;
                    var ChkIfSelected = document.getElementById(SeatingCount);
                    if (ChkIfSelected.checked) {
                        counter++;
                    }
                }
            }
            if (counter == 0) {
                alert('Please select number of students seats on bench.');
                return false;
            }
            else {
                if (IfSelected) {
                    return true;
                }
                else {
                    alert('Please Select Seating Arrangement Type.');
                }
            }
            //}
        };


        //check if Seating Arrangement Type Selected
        //var IfSelected = function CheckSeatingArrType() {
        //    counter = 0;
        //    var ArrType = document.getElementsByName("ctl00$ContentPlaceHolder1$rbSeatingType")
        //    var x = document.getElementsByName("ctl00$ContentPlaceHolder1$rbSeatingType").length;
        //    if (x > 0) {
        //        for (i = 0; i < x ; i++) {
        //            var SeatingCount = 'ctl00_ContentPlaceHolder1_rbSeatingType_' + i;
        //            var ChkIfSelected = document.getElementById(SeatingCount);
        //            if (ChkIfSelected.checked) {
        //                counter++;
        //            }
        //        }
        //    }
        //    if (counter == 0) {
        //        return false;
        //    } else {
        //        return true;
        //    }
        //}
    </script>
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>



    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updplRoom"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updplRoom" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDYtxtSeating" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged1" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="2" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYtxtExamDate" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamdate" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlExamdate_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>


                                        <asp:RequiredFieldValidator ID="rfvExamdat" runat="server" ControlToValidate="ddlExamdate"
                                            Display="None" ErrorMessage="Please Select Exam Date" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYlvExamSlot" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlslot" runat="server" TabIndex="4" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlslot_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlslot"
                                            Display="None" ErrorMessage="Please Select Slot" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>

                                        <asp:TextBox ID="txtSequence" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="true"
                                            TabIndex="5" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true" Visible="false">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Repeater</asp:ListItem>
                                            <asp:ListItem Value="2">Regular/Repeater</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvexamtype" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please Select Regular/Repeater" InitialValue="-1" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <div class="row">
                                            <div class="col-lg-3 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item pl-md-0 pr-md-0"><b>Total Student :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lbltotcount" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            <asp:HiddenField ID="hdStudCount" runat="server" />
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item pl-md-0 pr-md-0"><b>Room Capacity :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblroomcapacity" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            <asp:HiddenField ID="hfroomcapacity" runat="server" />
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item pl-md-0 pr-md-0"><b>Seating Plan:</b>
                                                        <a class="sub-label">
                                                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="single" Text="Single" />&nbsp;&nbsp;
                                                   <asp:RadioButton ID="RadioButton2" runat="server" GroupName="single" Text="Double" />&nbsp;&nbsp;
                                                   <asp:RadioButton ID="RadioButton3" runat="server" GroupName="single" Text="Triple" Visible="false" />&nbsp;&nbsp;
                                                        </a>
                                                    </li>
                                                </ul>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <asp:Panel ID="PnlMatrix" runat="server" Visible="false" Style="overflow-y: scroll;">
                                            <div class="sub-heading">
                                                <h5>Matrix Room Details</h5>
                                            </div>
                                            <div class="table-responsive">
                                                <asp:ListView ID="lvMatrix" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblMatrix">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px; background:#fff !important;">
                                                                    <tr>
                                                                        <th>Room Name</th>
                                                                        <th>Row </th>
                                                                        <th>Column </th>
                                                                        <th>Actual Capacity</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item">
                                                            <td>

                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ROOM_NAME")%>' ToolTip='<%# Eval("ROOMNO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblExamCourse1" runat="server" Text='<%# Eval("ROW_INDEX")%>' />

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("COLUMN_INDEX")%>' ToolTip='<%# Eval("ROOMNO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("ACTUAL_CAPACITY")%>' ToolTip='<%# Eval("ROOMNO")%>' />
                                                            </td>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="configure" Visible="false" OnClientClick="return confirm('Are you sure? Do you Want to Submit Data ')" OnClick="btnSave_Click"
                                    TabIndex="6" CssClass="btn btn-primary" />

                                <asp:Button ID="btnClear" runat="server" TabIndex="7" CssClass="btn btn-warning" OnClick="btnClear_Click" Text="Cancel" Visible="false" />
                                <asp:ValidationSummary ID="vsRoomNm" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlExamCourse" runat="server" Visible="false">
                                    <div class="sub-heading">
                                        <h5>Exam Course List on Date</h5>
                                    </div>
                                    <asp:ListView ID="lvExamCoursesOnDate" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <table id="tblExamCourses" class="table table-striped table-bordered nowrap" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Branch </th>
                                                        <th>Course </th>
                                                        <th>Degree </th>
                                                        <th>Semester  </th>
                                                        <%--<th>Seating Sequence</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <%-- <asp:Label ID="lblbranch" runat="server" Text='<%# Eval("CODE")%>' ToolTip='<%# Eval("BRANCHNO")%>' />--%>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("SHORTNAME")%>' ToolTip='<%# Eval("BRANCHNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblExamCourse" runat="server" Text='<%# Eval("COURSES")%>' />
                                                    <asp:Label ID="lblCcode" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("DEGREENO")%>' Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("DEGREENAME")%>' ToolTip='<%# Eval("DEGREENO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                                </td>

                                                <%--  <td>
                                                                    <asp:TextBox ID="txtSrNo" runat="server" Onblur="IsNumberExist(this,1);" MaxLength="2" Visible="false"></asp:TextBox>

                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FTEtxtColumns" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtSrNo">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                               
                                <div class="col-12 mt-3">
                                <asp:Panel ID="pnlRoomDetails" runat="server" Visible="false">
                                    <div class="sub-heading">
                                        <h5>Room Details</h5>
                                    </div>
                                
                                    <asp:ListView ID="lvRoomDetails" runat="server" >
                                        <LayoutTemplate>
                                            <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblroomcapacity">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px; background:#fff !important;">
                                                        <tr>
                                                            <th>Select Room
                                                                   <asp:CheckBox ID="chckallroom" runat="server" Checked="false" onclick="CheckAll(this,1),EnableTextBox(this);" />
                                                            </th>
                                                            <th>SrNo</th>
                                                            <th>Floor Name</th>
                                                            <th>Block Name</th>
                                                            <th>Room Name </th>
                                                            <th>Room Capacity </th>
                                                            <th>Actual Capcity </th>
                                                            <th>Not in Use Seats </th>
                                                            <th>Room Sequence </th>
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
                                                <td class="text-center">
                                                    <asp:CheckBox ID="chckroom" runat="server" onclick="chkIndividualRoom(this,2),EnableTextBox(this);" />
                                                    <asp:HiddenField ID="hfActualCapcity" runat="server" Value='<%# Eval("ACTUAL_CAPACITY") %>' />
                                                    <asp:HiddenField ID="hfRoom" runat="server" Value='<%# Eval("ROOMNO") %>' />
                                                </td>
                                                <td><%# Container.DataItemIndex+1 %>
                                                    <asp:HiddenField ID="hfsrno" runat="server" Value='<%# Container.DataItemIndex+1 %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBlock" runat="server" Text='<%#Eval("BLOCKNAME")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFloor" runat="server" Text='<%#Eval("FLOORNAME")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRoomname" runat="server" Text='<%# Eval("ROOMNAME")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBlockCapcity" runat="server" Text='<%# Eval("ROOMCAPACITY")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRoomCapcity" runat="server" Text='<%# Eval("ACTUAL_CAPACITY")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSeatNotUse" runat="server" Text='<%# Eval("DISABLED_IDS")%>' />
                                                </td>
                                                  <asp:UpdatePanel ID="UPRoomDetails" runat="server">
                                               <ContentTemplate>
                                                <td>
                                                    <asp:TextBox ID="txtRoomSrNo" runat="server" MaxLength="2"  Onblur="IsNumberExist(this,2);" Text='<%# Eval("SEQUENCENO")%>'
                                                        ></asp:TextBox>
                                                    <%--Onblur="IsNumberExist(this,2);"Onblur="IsNumberExist(this,2);" IsNumberExist()--%>
                                                    <%--Text='<%#Eval("SEQUENCENO") %>'--%>

                                                    <%-- <asp:HiddenField runat="server" value ="" name ="pointDistanceMapping.point.id" id ="point_id_0" class="point_id" />--%>


                                                    <%--   <asp:TextBox ID="txtRoomSrNo" Class="destination_points" runat="server" MaxLength="2" onchange = "DuplicateNoNotAllowed(this);" Text='<%# Eval("SEQUENCENO")%>'
                                                                    AutoPostBack="true" ></asp:TextBox>--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FTEtxtColumns" runat="server" FilterType="Numbers" TargetControlID="txtRoomSrNo">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                </ContentTemplate>
                                             <Triggers>
                                             
                                                 <asp:AsyncPostBackTrigger ControlID="txtRoomSrNo" />
                                             </Triggers>
                                             </asp:UpdatePanel>
                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                     
                                </asp:Panel>
                            </div>
                                         

                            <div class="col-md-12 mt-3">
                                <asp:Panel ID="pnldetails" runat="server" Visible="false">
                                    <asp:ListView ID="lvdetails" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>STUDENTS LIST</h5>
                                            </div>
                                            <div class="table-responsive">
                                                <table id="divsessionlist" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr No. </th>
                                                            <th>Enrollment No </th>
                                                            <th>Student Name </th>
                                                            <th>Branch </th>
                                                            <th>Bench No </th>
                                                            <th>Floor</th>
                                                            <th>Block Name</th>
                                                            <th>Room Name </th>
                                                            <th>Semester </th>
                                                            <th>Course Code</th>
                                                            <th>Exam Slot </th>
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
                                                <td><%#Container.DataItemIndex+1 %></td>
                                                <td><%# Eval("REGNO")%></td>
                                                <td><%# Eval("STUDNAME")%></td>
                                                <td><%# Eval("LONGNAME")%></td>
                                                <td><%# Eval("SEATNO")%></td>
                                                <td><%# Eval("FLOORNAME")%></td>
                                                <td><%# Eval("BLOCKNAME")%></td>
                                                <td><%# Eval("ROOMNAME")%></td>
                                                <td><%# Eval("SEMESTERNO")%></td>
                                                <td><%# Eval("ccode")%></td>
                                                <td><%# Eval("SLOTNAME")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnConfigure" runat="server" Text="Configure" ValidationGroup="configure" OnClientClick="return confirm('Are you sure? Do you Want to Configure Seating Plan ? ')" Visible="false"
                                    TabIndex="8" CssClass="btn btn-primary" OnClick="btnConfigure_Click" />

                                <asp:Button ID="btnDeallocate" runat="server" Text="Deallocate" ValidationGroup="configure" Visible="false"
                                    TabIndex="9" CssClass="btn btn-primary" OnClick="btnDeallocate_Click" />
                                <ajaxToolKit:ConfirmButtonExtender ID="PublichCon" runat="server" ConfirmText="Do You Want to Deallocate Seating Plan ?" TargetControlID="btnDeallocate">
                                </ajaxToolKit:ConfirmButtonExtender>

                                <asp:Button ID="btnCancel" runat="server" TabIndex="10" CssClass="btn btn-warning" OnClick="btnClear_Click" Text="Cancel" Visible="false" />

                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="configure" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%--<Triggers>--%>
        <%--<asp:PostBackTrigger ControlID="ddlRoom" />  --%>
        <%-- <asp:PostBackTrigger ControlID="lvdetails" /> 
            <asp:PostBackTrigger ControlID="lvRoomDetails"/> --%>
        <%--</Triggers> --%>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

