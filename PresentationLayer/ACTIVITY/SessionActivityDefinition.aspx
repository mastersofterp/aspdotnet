<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SessionActivityDefinition.aspx.cs" Inherits="Activity_SessionActivityDefinition"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <%--<link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />--%>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdShowStatus" runat="server" ClientIDMode="Static" />
    <script type="text/javascript" language="javascript">
        function SelectAllInstitute() {
            debugger;

            var CHK = document.getElementById("<%=chkInstituteList.ClientID%>");

            var checkbox = CHK.getElementsByTagName("input");
            var chkInstitute = document.getElementById('ctl00_ContentPlaceHolder1_chkInstitute');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkInstituteList_' + i);
                if (chkInstitute.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }


        function SelectAllDegree() {
            debugger;

            var CHK = document.getElementById("<%=chkDegreeList.ClientID%>");
            //var CHK = document.getElementById('ctl00_ContentPlaceHolder1_chkDegreeList');
            var checkbox = CHK.getElementsByTagName("input");


            var chkDeg = document.getElementById('ctl00_ContentPlaceHolder1_chkDegree');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkDegreeList_' + i);
                if (chkDeg.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }

        function SelectAllBranch() {

            var CHK = document.getElementById("<%=chkBranchList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");


            var chkBranch = document.getElementById('ctl00_ContentPlaceHolder1_chkBranch');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkBranchList_' + i);
                if (chkBranch.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }


        function SelectAllSem() {
            var CHK = document.getElementById("<%=chkSemesterList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");

            var chksem = document.getElementById('ctl00_ContentPlaceHolder1_chkSemester');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkSemesterList_' + i);
                if (chksem.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }

        function SelectAllUsers() {
            var CHK = document.getElementById("<%=chkUserRightsList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");

            var chkUser = document.getElementById('ctl00_ContentPlaceHolder1_chkUserRights');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkUserRightsList_' + i);
                if (chkUser.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>


    <script type="text/javascript">

        function CountCharacters() {
            var maxSize = 150;

            if (document.getElementById('<%= txttitle.ClientID %>')) {
                var ctrl = document.getElementById('<%= txttitle.ClientID %>');
                var len = document.getElementById('<%= txttitle.ClientID %>').value.length;
                if (len <= maxSize) {
                    var diff = parseInt(maxSize) - parseInt(len);

                    if (document.getElementById('<%= txtRemain.ClientID %>')) {
                        document.getElementById('<%= txtRemain.ClientID %>').value = diff;
                    }
                }
                else {
                    ctrl.value = ctrl.value.substring(0, maxSize);
                }
            }

            return false;
        }

        function CountCharactersNotificationDetailsBox() {
            var maxSize = 300;

            if (document.getElementById('<%= txtDetailsNM.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtDetailsNM.ClientID %>');
                var len = document.getElementById('<%= txtDetailsNM.ClientID %>').value.length;
                if (len <= maxSize) {
                    var diff = parseInt(maxSize) - parseInt(len);

                    if (document.getElementById('<%= txtRemain1.ClientID %>')) {
                        document.getElementById('<%= txtRemain1.ClientID %>').value = diff;
                    }
                }
                else {
                    ctrl.value = ctrl.value.substring(0, maxSize);
                }
            }

            return false;
        }
    </script>

    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvDetail$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvDetail$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('#tblSessionAct').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

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
                                    return $('#tblSessionAct').DataTable().column(idx).visible();
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
                                                return $('#tblSessionAct').DataTable().column(idx).visible();
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
                                                return $('#tblSessionAct').DataTable().column(idx).visible();
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
                    var table = $('#tblSessionAct').DataTable({
                        responsive: true,
                        lengthChange: true,
                        scrollY: 320,
                        scrollX: true,
                        scrollCollapse: true,
                        paging: false, // Added by Gaurav for Hide pagination

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
                                        return $('#tblSessionAct').DataTable().column(idx).visible();
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
                                                   return $('#tblSessionAct').DataTable().column(idx).visible();
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
                                                   return $('#tblSessionAct').DataTable().column(idx).visible();
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="nav-tabs-custom mt-1">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item"><a class="nav-link active" href="#tabLC" data-toggle="tab" aria-expanded="true">Activity Definition</a></li>
                            <li class="nav-item"><a class="nav-link" href="#idNotificationMaster" data-toggle="tab" aria-expanded="false">Notification Master</a></li>
                            <li class="nav-item"><a class="nav-link" href="#idNotificationDetails" data-toggle="tab" aria-expanded="false">Notification Details</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tabLC">
                                <div class="col-12 mt-3">
                                    <div class="sub-heading">
                                        <h5>
                                            <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h5>
                                    </div>
                                </div>
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSesActivity"
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
                                <asp:UpdatePanel ID="updSesActivity" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>College</label>--%>
                                                        <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <%--<asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" TabIndex="1">
                                                    </asp:DropDownList>--%>
                                                    <%--Added for Multiple Selection on 2022 Aug 30--%>

                                                    <asp:ListBox ID="ddlCollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged1" AutoPostBack="true"></asp:ListBox>


                                                    <%--Added for Multiple Selection on 2022 Aug 30 End--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollege" SetFocusOnError="true"
                                                        Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                    <%--add by maithili [07-09-2022] runat="server" visible="false"--%>
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Session</label>--%>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>

                                                    <%--<asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" runat="server" TabIndex="1" data-select2-enable="true"
                                                        CssClass="form-control">
                                                        <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>

                                                    <asp:ListBox ID="ddlSession" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                        AppendDataBoundItems="true"></asp:ListBox>

                                                    <%-- <asp:RequiredFieldValidator ID="valSession" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please select session." SetFocusOnError="true" ValidationGroup="submit"
                                                        InitialValue="0" />--%>  <%--comment --%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Exam Pattern</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamPattern" runat="server" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="3"
                                                        CssClass="form-control" OnSelectedIndexChanged="ddlExamPattern_SelectedIndexChanged" AutoPostBack="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                     <%--   <label>Exam No.</label>--%>
                                                         <label>Exam Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamNo" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="True" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlExamNo_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Sub-Exam No.</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubExamNo" runat="server" CssClass="form-control" TabIndex="4" AppendDataBoundItems="True" data-select2-enable="true" OnSelectedIndexChanged="ddlSubExamNo_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Activity</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlActivity" AppendDataBoundItems="true" runat="server" TabIndex="2" data-select2-enable="true"
                                                        CssClass="form-control" onclick="validate()">
                                                        <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <%--  <asp:ListBox ID="ddlActivity" runat="server" SelectionMode="Multiple"
                                                        CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                    --%>
                                                    <asp:RequiredFieldValidator ID="valActivity" runat="server" ControlToValidate="ddlActivity"
                                                        Display="None" ErrorMessage="Please Select Activity." SetFocusOnError="true"
                                                        ValidationGroup="submit" InitialValue="0" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Start Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtStartDate" runat="server" TabIndex="3" CssClass="form-control" AutoComplete="off" onchange="return ValidateDate()" />
                                                        <%-- <asp:Image ID="imgCalStartDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                        <asp:RequiredFieldValidator ID="valStartDate" runat="server" ControlToValidate="txtStartDate"
                                                            Display="None" ErrorMessage="Please enter start date." SetFocusOnError="true"
                                                            ValidationGroup="submit" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtStartDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>End Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEndDate" runat="server" TabIndex="4" CssClass="form-control" AutoComplete="off" onchange="return ValidateDate()" />
                                                        <%--<asp:Image ID="imgCalEndDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtEndDate" PopupButtonID="imgCalEndDate" Enabled="true" EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="valEndDate" runat="server" ControlToValidate="txtEndDate"
                                                            Display="None" ErrorMessage="Please enter end date." SetFocusOnError="true" ValidationGroup="submit" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column d-none">
                                                    <div class="label-dynamic">
                                                        <label>Institute</label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="chkInstitute" runat="server" Text="All Institute" AutoPostBack="true" onClick="SelectAllInstitute()" CssClass="select-all-checkbox" TabIndex="5" />
                                                        <asp:CheckBoxList ID="chkInstituteList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkInstitute_SelectedIndexChanged"
                                                            RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Degree </label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="chkDegree" runat="server" Text="All Degree" AutoPostBack="true" onClick="SelectAllDegree()" CssClass="select-all-checkbox" TabIndex="6" />
                                                        <asp:CheckBoxList ID="chkDegreeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkDegreeList_SelectedIndexChanged"
                                                           RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                        </asp:CheckBoxList>

                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Branch</label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="chkBranch" runat="server" Text="All Branches" onClick="SelectAllBranch()" CssClass="select-all-checkbox" TabIndex="7" />
                                                        <asp:CheckBoxList ID="chkBranchList" runat="server"
                                                            RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                        </asp:CheckBoxList>

                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <label>Semester</label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="chkSemester" runat="server" Text="All Semesters" onclick="SelectAllSem()" CssClass="select-all-checkbox" TabIndex="8" />
                                                        <asp:CheckBoxList ID="chkSemesterList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <label>User Rights</label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="chkUserRights" runat="server" Text="All User" onclick="SelectAllUsers()" CssClass="select-all-checkbox" TabIndex="9" />
                                                        <asp:CheckBoxList ID="chkUserRightsList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <label>Activity Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                         <input type="checkbox" id="switch" name="switch" checked/>
                                                         <label data-on="Started" data-off="Stopped" for="switch"></label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoStart" runat="server" Text="Started" GroupName="act_status"
                                                        TabIndex="5" visible="false"/>&nbsp&nbsp
                                                    <asp:RadioButton ID="rdoStop" runat="server" Text="Stopped" Checked="true" GroupName="act_status"
                                                        TabIndex="10" visible="false" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <label>Show Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                         <input type="checkbox" id="switches" name="switch" checked/>
                                                         <label data-on="Yes" data-off="No" for="switches"></label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoYes" runat="server" Text="Yes" GroupName="sh_status"  TabIndex="11" visible="false"/>&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdoNo" runat="server" Text="No" GroupName="sh_status" Checked="true"  visible="false"/>

                                                </div>--%>



                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                          <sup>* </sup>
                                                        <label>Activity Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdActive" name="switch" checked />
                                                        <label data-on="Started" data-off="Stopped" for="rdActive"></label>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                         <sup>* </sup>
                                                        <label>Show Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdStatus" name="switch" checked />
                                                        <label data-on="Yes" data-off="No" for="rdStatus"></label>
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                           <%--  <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validate();"
                                                OnClick="btnSubmit_Click" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="submit" />--%>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"  OnClientClick="return validate();"
                                                OnClick="btnSubmit_Click" TabIndex="12" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="13" OnClick="btnCancel_Click"
                                                CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />

                                        </div>
                                        <div class="col-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvSessionActivities" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Session Activities</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblSessionAct">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit </th>
                                                                    <th>Activity Status </th>
                                                                    <th>College </th>
                                                                    <th>Session </th>
                                                                    <th>Activity </th>
                                                                    <th>Exam Pattern</th>
                                                                    <th>Exam Name</th>
                                                                    <th>Sub Exam Name</th>
                                                                    <th>Start Date </th>
                                                                    <th>End Date </th>
                                                                    <th>Show Status </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" TabIndex="10" ToolTip="Edit Record" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblActive" Text='<%# Eval("STATUS")%>' ForeColor='<%# Eval("STATUS").ToString().Equals("Started")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td><%# Eval("COLLEGE_NAME")%></td>
                                                            <td><%# Eval("SESSION_NAME")%></td>
                                                            <td><%# Eval("ACTIVITY_NAME")%></td>
                                                            <td><%# Eval("PATTERN_NAME")%></td>
                                                            <td><%# Eval("EXAMNAME")%></td>
                                                            <td><%# Eval("SUBEXAMNAME")%></td>
                                                            <td><%# (Eval("START_DATE").ToString() != string.Empty) ? (Eval("START_DATE","{0:dd-MMM-yyyy}")) : Eval("START_DATE" ,"{0:dd-MMM-yyyy}") %></td>
                                                            <td><%# (Eval("END_DATE").ToString() != string.Empty) ? (Eval("END_DATE", "{0:dd-MMM-yyyy}")) : Eval("END_DATE", "{0:dd-MMM-yyyy}")%></td>
                                                            <%--<td><%# Eval("STATUS")%></td>--%>

                                                            <td><%# Eval("SHOWSTATUS")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane" id="idNotificationMaster">
                                <div class="col-12 mt-3">
                                    <div class="sub-heading">
                                        <h5>Notification Master</h5>
                                    </div>
                                </div>
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updnotificationmaster"
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
                                <asp:UpdatePanel ID="updnotificationmaster" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlStudInfo" runat="server" Visible="true">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Expiry Date :</label>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon" id="Div2" runat="server">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>

                                                            <asp:TextBox ID="txtDate" runat="server" TabIndex="1" CssClass="form-control" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" PopupButtonID="txtDate" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" Enabled="true" EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" Display="None" ErrorMessage="Please Select Expiry Date" SetFocusOnError="True" ValidationGroup="Notification"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Notification Title</label>
                                                        </div>
                                                        <asp:TextBox ID="txttitle" runat="server" TabIndex="2" TextMode="MultiLine" CssClass="form-control" onkeyup="return CountCharacters();" ToolTip="Please Enter Notification Title" MaxLength="150"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttitle" Display="None" ErrorMessage="Please Enter Notification Title" SetFocusOnError="True" ValidationGroup="Notification"></asp:RequiredFieldValidator>

                                                        <span class="form-inline mt-1">Characters Remaining : &nbsp;&nbsp;
                                                            <asp:TextBox ID="txtRemain" runat="server" Text="150" ForeColor="Red" Width="22%" Enabled="False" class="form-control"></asp:TextBox>
                                                        </span>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Notification Details</label>
                                                        </div>
                                                        <%-- <asp:UpdatePanel ID="details" runat="server">
                                                        <ContentTemplate>--%>
                                                        <%--<FTB:FreeTextBox ID="txtDetailsNM" runat="Server" OnTextChanged="txtDetailsNM_TextChanged">
                                                                </FTB:FreeTextBox>--%>
                                                        <asp:TextBox ID="txtDetailsNM" runat="server" TabIndex="3" TextMode="MultiLine" CssClass="form-control" onkeyup="return CountCharactersNotificationDetailsBox();" ToolTip="Please Enter Notification Details" MaxLength="50"></asp:TextBox>
                                                        <%-- </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="txtDetails" />
                                                                    </Triggers>
                                                                    </asp:UpdatePanel>--%>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDetailsNM" Display="None" ErrorMessage="Please Enter Notification Details" ValidationGroup="Notification"></asp:RequiredFieldValidator>

                                                        <span class="form-inline mt-1">Characters Remaining : &nbsp;&nbsp;
                                                                        <asp:TextBox ID="txtRemain1" runat="server" Text="300" ForeColor="Red" Width="22%" Enabled="False" class="form-control"></asp:TextBox>
                                                        </span>
                                                        <%-- <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtDetails" Display="None" ErrorMessage="Please Enter Notification Details" SetFocusOnError="True" ValidationGroup="Notification">
                                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="Button1" runat="server" TabIndex="4" Text="Submit" CssClass="btn btn-primary"
                                                    ValidationGroup="Notification" OnClick="btnSubmitNM_Click" />
                                                <asp:Button ID="Button2" runat="server" TabIndex="5" Text="Cancel" OnClick="btnCancelNM_Click"
                                                    CssClass="btn btn-warning" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                                    ValidationGroup="Notification" />
                                            </div>
                                            <div class="col-12">
                                                <asp:ListView ID="lvNotification" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Notification Details</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <th>Title</th>
                                                                    <th>Details</th>
                                                                    <th>Expiry Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                                    CommandArgument='<%# Eval("NOTIFICATIONID") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditNM_Click" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("TITLE") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MESSAGE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("EXPIRYDATE")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSubmit" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane " id="idNotificationDetails">
                                <div class="col-12 mt-3">
                                    <div class="sub-heading">
                                        <h5>Notification Details</h5>
                                    </div>
                                </div>
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updNotificationDetails"
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
                                <asp:UpdatePanel ID="updNotificationDetails" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divut" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>User Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlUserMD_SelectedIndexChanged" class="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvuser" runat="server" ErrorMessage="Please Select User Type"
                                                        ControlToValidate="ddlUser" Display="None" SetFocusOnError="True" ValidationGroup="Show"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Title</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddltitle" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="2"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddltitleMD_SelectedIndexChanged" class="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select Title"
                                                        ControlToValidate="ddltitle" Display="None" SetFocusOnError="True" ValidationGroup="Show"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Details</label>
                                                    </div>
                                                    <asp:TextBox ID="txtdetails" runat="server" TextMode="MultiLine" Enabled="false" class="form-control" TabIndex="3">

                                                    </asp:TextBox>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="trDept" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <label>Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" class="form-control" data-select2-enable="true" TabIndex="4">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divfactype" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <label>Faculty Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlfactype" runat="server" AppendDataBoundItems="True" class="form-control" data-select2-enable="true" TabIndex="5">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <asp:Panel ID="pnlStudent" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Degree</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegreeMD_SelectedIndexChanged" class="form-control" data-select2-enable="true" TabIndex="6">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Branch</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" class="form-control" data-select2-enable="true" TabIndex="7">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Semester</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" class="form-control" data-select2-enable="true" TabIndex="8">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShowMD_Click" ValidationGroup="Show" CssClass="btn btn-primary" TabIndex="9" />
                                            <asp:Button ID="btnSubmitMD" runat="server" Enabled="false" Text="Submit" OnClick="btnSubmitMD_Click" CssClass="btn btn-primary" TabIndex="10"
                                                OnClientClick="return validateAssign()" />
                                            <asp:Button ID="Button4" runat="server" Text="Cancel" OnClick="btnCancelMD_Click" CssClass="btn btn-warning" TabIndex="11" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                        </div>
                                        <div class="col-12">
                                            <asp:Panel ID="pnlDetail" runat="server" Visible="false">
                                                <div class="col-md-12 table-responsive">
                                                    <asp:ListView ID="lvDetail" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Details</h5>
                                                            </div>
                                                            <table id="tblHead" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr id="trRow">
                                                                        <th id="thHead">
                                                                            <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                                        </th>
                                                                        <th>User Name
                                                                        </th>
                                                                        <th>Full Name
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="Tr1" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("UA_NO") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NAME")%>' ToolTip='<%# Eval("UA_NO") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStatMandat(val) {
            $('#rdStatus').prop('checked', val);
        }

        function validate() {

            $('#hfdShowStatus').val($('#rdStatus').prop('checked'));
            $('#hfdActive').val($('#rdActive').prop('checked'));
              
            var rfvCollege = (document.getElementById("<%=lblDYddlSchool.ClientID%>").innerHTML);

            var ddlCollege = $("[id$=ddlCollege]").attr("id");
            var ddlCollege = document.getElementById(ddlCollege);
            if (ddlCollege.value == 0) {
                alert('Please Select ' + rfvCollege + ' \n', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(ddlCollege).focus();
                return false;
            }

            var idtxtweb = $("[id$=ddlActivity]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value == 0) {
                alert('Please Select Activity', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }


            var idtxtweb = $("[id$=txtStartDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Start Date', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }


            var idtxtweb = $("[id$=txtEndDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter End Date', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
                  
            }

                  
            var rfvSession = (document.getElementById("<%=lblDYddlSession.ClientID%>").innerHTML);
            
            
            var idtxtweb = $("[id$=ddlSession]").attr("id");
            if (document.getElementById("<%=ddlSession.ClientID%>").value == "0") {
                alert('Please Select ' + rfvSession + ' \n', 'Warning!');
                document.getElementById("<%=ddlSession.ClientID%>").focus();
                return false;
            }
           
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                $($.fn.dataTable.tables(true)).DataTable()
                    .columns.adjust()
                    .responsive.recalc();
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });

        //$(document).ready(function () {
        //    $('#ctl00_ContentPlaceHolder1_lvSessionActivities_ctrl0_btnEdit').click(function () {
        //        alert("vfdfsgsfbvs");
        //        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').prop('disabled', true);
        //    });
        //});

        //var parameter = Sys.WebForms.PageRequestManager.getInstance();
        //parameter.add_endRequest(function () {
        //    $(document).ready(function () {
        //        $('#ctl00_ContentPlaceHolder1_lvSessionActivities_ctrl0_btnEdit').click(function () {
        //            alert("vfdfsgsfbvs");
        //            $('#ctl00_ContentPlaceHolder1_ddlExamPattern').prop('disabled', true);
        //        });
        //    });
        //});

        //called on Client Edit click event
        //function disableCollegeDDL() {
        //    //alert("hi");
        //    document.getElementById("ddlCollege").disabled = true;
        //}
    </script>

    <script>
        function ValidateDate() {
            var Fromdate = document.getElementById('<%=txtStartDate.ClientID%>').value;
        var Todate = document.getElementById('<%=txtEndDate.ClientID%>').value;
        var From_date = moment(Fromdate, 'DD/MM/YYYY');
        var To_date = moment(Todate, 'DD/MM/YYYY');

        if (To_date.isBefore(From_date)) { // Compare with the from date
            alert("End Date Cannot be Less Than Start Date!");
            document.getElementById('<%=txtEndDate.ClientID%>').value = '';
            return;
        }
    }
</script>
</asp:Content>
