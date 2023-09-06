<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SeatingPlanFixed.aspx.cs" Inherits="ADMINISTRATION_SeatingPlanFixed" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">


        function CheckAllProgram(txtmain, headid) {
            CheckAllChild(txtmain, headid);
        }

        function CheckAllChild(txtmain, headid) {
            debugger;
            var studCount = 0;
            var txtSelectedStudsCount = document.getElementById('<%= txtSelectedStudStrengh.ClientID %>');
                var items = document.getElementsByClassName('item');
                var headerchk = document.getElementById('headeritem');
                var headerFirstChild = headerchk.firstElementChild.nextElementSibling.firstElementChild;
                if (headerFirstChild.checked) {
                    for (var i = 0; i < items.length; i++) {
                        items[i].firstElementChild.nextElementSibling.firstElementChild.checked = true;
                        items[i].lastElementChild.firstElementChild.disabled = false;
                        var CellData = items[i].firstElementChild.nextElementSibling.nextElementSibling.nextElementSibling.firstElementChild.innerHTML;
                        studCount += Number(CellData);
                    }
                    txtSelectedStudsCount.value = studCount
                }
                else {
                    for (var i = 0; i < items.length; i++) {
                        items[i].firstElementChild.nextElementSibling.firstElementChild.checked = false;
                        items[i].lastElementChild.firstElementChild.disabled = true;
                        items[i].lastElementChild.firstElementChild.value = "";
                        var CellData = items[i].firstElementChild.nextElementSibling.nextElementSibling.nextElementSibling.firstElementChild.innerHTML;
                        txtSelectedStudsCount.value = Number(txtSelectedStudsCount.value) - Number(CellData);

                    }
                    // txtSelectedStudsCount.value = studCount
                }


            }

            function chkIndividualProgram(txtmain, headid) {
                var txtSelectedStudsCount = document.getElementById('<%= txtSelectedStudStrengh.ClientID %>');
            var studCount = 0;
            debugger;
            var items = document.getElementsByClassName('item')
            for (var i = 0; i < items.length; i++) {
                if (items[i].firstElementChild.nextElementSibling.firstElementChild.checked) {
                    items[i].lastElementChild.firstElementChild.disabled = false;
                    var CellData = items[i].firstElementChild.nextElementSibling.nextElementSibling.nextElementSibling.firstElementChild.innerHTML;
                    console.log(CellData);
                    studCount += Number(CellData);
                    // alert('I am checked');
                }
                else {
                    items[i].lastElementChild.firstElementChild.disabled = true;
                    items[i].lastElementChild.firstElementChild.value = "";
                    // alert('I am not checked');
                }
                txtSelectedStudsCount.value = studCount
            }
        }

        function SeqExist(txt) {
            debugger;
            var items = document.getElementsByClassName('item')

            var chkexist = 0;
            var counter = 0;
            var addSeqValue = [];
            chkValue(items, addSeqValue);


            for (var i = 0; i < addSeqValue.length; i++) {
                var seqValue = items[i].lastElementChild.firstElementChild.value;
                if (Number(seqValue) == Number(addSeqValue[i])) {
                    counter++;
                    if (counter >= 2) {
                        alert('Sequence ' + seqValue + ' already exist.');
                        items[i].lastElementChild.firstElementChild.value = "";
                        break;
                    }
                }
            }


        }

        var chkValue = function FindSequnce(items, addSeqValue) {
            debugger;

            for (var i = 0; i < items.length; i++) {
                if (items[i].firstElementChild.nextElementSibling.firstElementChild.checked && items[i].lastElementChild.firstElementChild.value.trim() != "") {
                    addSeqValue[i] = items[i].lastElementChild.firstElementChild.value;
                }
            }
        }


        //------------End---------------



        function CheckAll(txtmain, headid) {
            AsignValue(txtmain, headid);
        }

        function chkIndividualRoom(txtchild, headid) {
            AsignValue(txtchild, headid);
        }

        function AsignValue(txt, headid) {
            try {


                var txtSelectedRoomCap = document.getElementById('<%= txtSelctedRommCap.ClientID %>');
                var hdfSelectedRommCap = document.getElementById('<%= hfroomcapacity.ClientID %>');
                var rmcap = 0;

                tbl = document.getElementById('tblroomcapacity');
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        debugger;
                        var chkid = 'ctl00_ContentPlaceHolder1_lvRoomDetails_ctrl' + i + '_chckroom';
                        var txtValue = 'ctl00_ContentPlaceHolder1_lvRoomDetails_ctrl' + i + '_txtRoomSrNo';
                        var hfActualCapacity = 'ctl00_ContentPlaceHolder1_lvRoomDetails_ctrl' + i + '_hfActualCapcity';

                        if (headid == 1) {

                            if (txt.checked) {
                                document.getElementById(chkid).checked = true;
                                rmcap += Number(document.getElementById(hfActualCapacity).value)

                            }
                            else {
                                document.getElementById(chkid).checked = false;
                                document.getElementById(txtValue).value = "";
                            }
                        }
                        else if (headid == 2) {

                            if (document.getElementById(chkid).checked == true) {
                                rmcap += Number(document.getElementById(hfActualCapacity).value)
                            }
                            else {
                                document.getElementById(txtValue).value = "";
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

        //Check Duplicate Sequence
        function IsNumberExist(txt, headid) {
            try {

                if (headid == 1) {
                    var tbl = document.getElementById('tblProgramesName');
                    var dataRows = tbl.getElementsByTagName('tr');
                    var lv = 'lvProgramNames';
                    var txtboxid = 'txtSrNo';
                }
                else if (headid == 2) {
                    var tbl = document.getElementById('tblroomcapacity');
                    var dataRows = tbl.getElementsByTagName('tr');
                    var lv = 'lvRoomDetails';
                    var txtboxid = 'txtRoomSrNo';
                }

                var SeqExist = "";
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        debugger;

                        var SrNo = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_' + txtboxid;
                        var SrNoValue = document.getElementById(SrNo).value;

                        if (SeqExist == SrNoValue && SeqExist != "") {
                            alert('Sequence ' + SeqExist + ' already exist.')
                            document.getElementById(SrNo).value = '';
                            break;
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


        //validate form
        function validateForm() {
            debugger;



            var hdStudCount = document.getElementById('<%= hdStudCount.ClientID %>');
            var RoomStrength = document.getElementById('<%=hfroomcapacity.ClientID %>')
            var e = document.getElementById('<%=ddlslot.ClientID %>')
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0) {
                alert('Please select exam slot.')
                return false;
            }
            //if (Number(hdStudCount.value) >= Number(RoomStrength.value)) {
            if (document.getElementById('<%= hdStudCount.ClientID %>').innerText != '' && document.getElementById('<%=hfroomcapacity.ClientID %>').innerText != '') {
                if (Number(hdStudCount.value) >= Number(RoomStrength.value)) {
                    alert('Students Strength is exceeding the room capacity.')
                    return false;
                }
                else {

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
                        alert('Please select single or dual type seating Arrangement.');
                        return false;
                    }
                    else {
                        if (IfSelected) {
                            return true;
                        }
                        else {
                            alert('Please select continue or alternate type seating arrangement.');
                        }
                    }
                }
            }
        };
        //check if Seating Arrangement Type Selected
        var IfSelected = function CheckSeatingArrType() {
            counter = 0;
            var ArrType = document.getElementsByName("ctl00$ContentPlaceHolder1$rbSeatingType")
            var x = document.getElementsByName("ctl00$ContentPlaceHolder1$rbSeatingType").length;

            if (x > 0) {
                for (i = 0; i < x ; i++) {

                    var SeatingCount = 'ctl00_ContentPlaceHolder1_rbSeatingType_' + i;
                    var ChkIfSelected = document.getElementById(SeatingCount);
                    if (ChkIfSelected.checked) {
                        counter++;
                    }

                }
            }
            if (counter == 0) {
                return false;
            } else {
                return true;
            }
        }

    </script>
    
    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblProgramesName').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0, 9];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblProgramesName').DataTable().column(idx).visible();
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
                                            var arr = [0, 9];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblProgramesName').DataTable().column(idx).visible();
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
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                            var arr = [0, 9];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblProgramesName').DataTable().column(idx).visible();
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
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0, 9];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblProgramesName').DataTable().column(idx).visible();
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
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                var table = $('#tblProgramesName').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0, 9];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblProgramesName').DataTable().column(idx).visible();
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
                                                var arr = [0, 9];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblProgramesName').DataTable().column(idx).visible();
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
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                                                var arr = [0, 9];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblProgramesName').DataTable().column(idx).visible();
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
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0, 9];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblProgramesName').DataTable().column(idx).visible();
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
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                            <h3 class="box-title">FIXED SEATING ARRANGEMENT</h3>
                            <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                     <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session</label>
                                                </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </div>

                                     <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Institute</label>
                                                </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College/School" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College/School" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Report"></asp:RequiredFieldValidator>--%>
                                    </div>

                                     <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Semester</label>
                                                </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--    <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                     <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam  Slot</label>
                                                </div>
                                        <asp:DropDownList ID="ddlslot" runat="server" TabIndex="1" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlslot_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlslot"
                                            Display="None" ErrorMessage="Please Select Slot" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </div>

                                     <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam Name</label>
                                                </div>
                                        <asp:DropDownList ID="ddlExamName" runat="server" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">MID SEM</asp:ListItem>
                                            <asp:ListItem Value="2">END SEM</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExamType" runat="server" ControlToValidate="ddlExamName"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </div>

                                     <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Students On Bench</label>
                                                </div>
                                        <asp:RadioButtonList ID="rbOnBench" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbOnBench_SelectedIndexChanged" CellPadding="12">
                                            <%--  <asp:ListItem Value="1">Single</asp:ListItem>--%>
                                            <asp:ListItem Value="2">Dual</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                     <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Arrangement Type</label>
                                                </div>
                                        <asp:RadioButtonList ID="rbSeatingType" runat="server" RepeatDirection="Horizontal" CellPadding="12">
                                            <%-- <asp:ListItem Value="1">Continue Seating</asp:ListItem>
                                            <asp:ListItem Value="2">Alternate Seating</asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                    </div>
                                      <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Total Reg Std for Selected Program</label>
                                                </div>
                                            <asp:TextBox ID="txtSelectedStudStrengh" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdStudCount" runat="server" />
                                       
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Selected Room Capacity</label>
                                                </div>
                                            <asp:TextBox ID="txtSelctedRommCap" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hfroomcapacity" runat="server" />
                                       
                                    </div>
                                </div>
                            </div>

                          
                                    <div class="col-12">
                                        <asp:Panel ID="pnlProgramName" runat="server" Visible="false">
                                          
                                                <asp:ListView ID="lvProgramNames" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                    <h5>Programs Name</h5>
                                                </div>
                                                          <table class="table table-striped table-bordered nowrap" style="width: 100%"  id="tblProgramesName">
                                                                <thead>
                                                                    <tr class="bg-light-blue" id="headeritem">
                                                                        <th>Sr No</th>
                                                                        <th>Select All
                                                                    <asp:CheckBox ID="chckallProgram" runat="server" Checked="false" onclick="CheckAllProgram(this,1);" />
                                                                        </th>
                                                                        <th>Program Name
                                                                        </th>
                                                                        <th>Registered Students
                                                                        </th>
                                                                        <th>Seating Sequence
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                       
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item">
                                                            <td><%#Container.DataItemIndex+1 %></td>
                                                            <td class="text-center">
                                                                <asp:CheckBox ID="chckProgram" runat="server" onclick="chkIndividualProgram(this,2);" />
                                                                <asp:HiddenField ID="hfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSchemeName" runat="server" Text='<%# Eval("SCHEMENAME")%>' ToolTip='<%# Eval("SCHEMENO") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStudCount" runat="server" Text='<%# Eval("STUDCOUNT")%>' />
                                                            </td>
                                                            <td>
                                                                <%--  <asp:TextBox ID="txtSrNo" runat="server" Onblur="return SeqExist();" MaxLength="2" Enabled="false"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtSrNo" runat="server" Onblur="IsNumberExist(this,1);" MaxLength="2" Enabled="false"></asp:TextBox>

                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FTEtxtColumns" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtSrNo">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                             
                                        </asp:Panel>
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlRoomDetails" runat="server" Visible="false">
                                          
                                                <%--<div class="table-overflow">--%>
                                                <asp:ListView ID="lvRoomDetails" runat="server">
                                                    <LayoutTemplate>
                                                          <div class="sub-heading">
                                                    <h5>Block Details</h5>
                                                </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblroomcapacity">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Select All
                                                                    <asp:CheckBox ID="chckallroom" runat="server" Checked="false" onclick="CheckAll(this,1),EnableTextBox(this);" />
                                                                        </th>
                                                                        <th>Block Name
                                                                        </th>
                                                                        <th>Block Capacity
                                                                        </th>
                                                                        <th>Actual Capcity
                                                                        </th>
                                                                        <th>Not in Use Seats
                                                                        </th>
                                                                        <th>Room Sequence
                                                                        </th>
                                                                    </tr>
                                                                       </thead>
                                                                    <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                  </tbody>
                                                            </table>
                                                      
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center">
                                                                <asp:CheckBox ID="chckroom" runat="server" onclick="chkIndividualRoom(this,2),EnableTextBox(this);" />
                                                                <asp:HiddenField ID="hfActualCapcity" runat="server" Value='<%# Eval("ACTUALCAPACITY") %>' />
                                                                <asp:HiddenField ID="hfRoom" runat="server" Value='<%# Eval("ROOMNO") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRoomname" runat="server" Text='<%# Eval("ROOMNAME")%>' />

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblBlockCapcity" runat="server" Text='<%# Eval("ROOMCAPACITY")%>' />

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRoomCapcity" runat="server" Text='<%# Eval("ACTUALCAPACITY")%>' />

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSeatNotUse" runat="server" Text='<%# Eval("DISABLED_IDS")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRoomSrNo" runat="server" Enabled="false" Onblur="IsNumberExist(this,2);" MaxLength="2"></asp:TextBox>

                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtRoomSrNo">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <%--</div>--%>
                                          
                                        </asp:Panel>


                                    </div>
                               
                      
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnConfigure" runat="server" Text="Configure" ValidationGroup="configure"
                                    TabIndex="8" CssClass="btn btn-primary" OnClick="btnConfigure_Click" OnClientClick="return validateForm(this);" />
                                <asp:Button ID="btnStastical" runat="server" Text="Statistical Report" ValidationGroup="process"
                                    TabIndex="15" Visible="false" />
                                <asp:Button ID="btnMasterSeating" runat="server" CssClass="btn btn-primary" Text="Master Seating Plan" ValidationGroup="process"
                                    TabIndex="15" Visible="false" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-warning" OnClick="btnClear_Click"
                                    Text="Cancel" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="configure" />
                                <asp:ValidationSummary ID="vsRoomNm" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                            </div>



                            <div class="col-12">
                                <asp:Panel ID="pnldetails" runat="server"  Visible="false">
                                    <asp:ListView ID="lvdetails" runat="server">
                                        <LayoutTemplate>
                                             <div class="sub-heading">
                                                    <h5>STUDENTS LIST</h5>
                                                </div>
                                               <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr No. </th>
                                                            <th>Enrollment No </th>
                                                            <th>Student Name </th>
                                                            <%-- <th>Branch </th>--%>
                                                            <th>Seat No </th>
                                                            <th>Block Name </th>
                                                            <%--<th>Semester </th>--%>
                                                            <%--  <th>Course Code </th>--%>
                                                            <th>Exam Slot </th>
                                                            <th>Exam Name </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <td><%#Container.DataItemIndex+1 %></td>
                                            <td><%# Eval("REGNO")%></td>
                                            <td><%# Eval("STUDNAME")%></td>
                                            <%--  <td><%# Eval("SHORTNAME")%></td>--%>
                                            <td><%# Eval("SEATNO")%></td>
                                            <td><%# Eval("ROOMNAME")%></td>
                                            <%--<td><%# Eval("SEMESTERNO")%></td>--%>
                                            <%--<td><%# Eval("CCODE")%></td>--%>
                                            <td><%# Eval("SLOTNAME")%></td>
                                            <td><%# Eval("EXAMNAME")%></td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>





        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="ddlRoom" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

