<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Exam_Activity.aspx.cs" Inherits="Activity_Exam_Activity"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
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
    <script>
        $(document).ready(function () {
            var table = $('#tblSessionAct').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

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
                    paging: false,
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
                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:ListBox ID="ddlCollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                    OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged1" AutoPostBack="true"></asp:ListBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollege" SetFocusOnError="true"
                                                    Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Exam Pattern</label>
                                                </div>
                                                <asp:DropDownList ID="ddlExamPattern" runat="server" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1"
                                                    CssClass="form-control" OnSelectedIndexChanged="ddlExamPattern_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Exam Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlExamNo" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlExamNo_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Sub-Exam No.</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSubExamNo" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true" OnSelectedIndexChanged="ddlSubExamNo_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Activity</label>
                                                </div>
                                                <asp:DropDownList ID="ddlActivity" AppendDataBoundItems="true" runat="server" TabIndex="1" data-select2-enable="true"
                                                    CssClass="form-control" onclick="validate()">
                                                    <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
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
                                                    <asp:TextBox ID="txtStartDate" runat="server" TabIndex="1" CssClass="form-control" AutoComplete="off" />
                                                    <asp:RequiredFieldValidator ID="valStartDate" runat="server" ControlToValidate="txtStartDate"
                                                        Display="None" ErrorMessage="Please enter start date." SetFocusOnError="true"
                                                        ValidationGroup="submit" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                        TargetControlID="txtStartDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>

                                                    <asp:CompareValidator ID="valStartDateType"
                                                        runat="server"
                                                        ControlToValidate="txtStartDate"
                                                        ControlToCompare="txtEndDate"
                                                        Display="Dynamic"
                                                        ErrorMessage="Please enter a valid date."
                                                        Operator="LessThan"
                                                        SetFocusOnError="true"
                                                        Type="Date"
                                                        CultureInvariantValues="true"
                                                        ValidationGroup="submit" />
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
                                                    <asp:TextBox ID="txtEndDate" runat="server" TabIndex="1" CssClass="form-control" AutoComplete="off" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                        TargetControlID="txtEndDate" PopupButtonID="imgCalEndDate" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="valEndDate" runat="server" ControlToValidate="txtEndDate"
                                                        Display="None" ErrorMessage="Please enter end date." SetFocusOnError="true" ValidationGroup="submit" />
                                                    <asp:CompareValidator ID="valEndDateType"
                                                        runat="server"
                                                        ControlToValidate="txtEndDate"
                                                        ControlToCompare="txtStartDate"
                                                        Display="Dynamic"
                                                        ErrorMessage="Please enter a valid date."
                                                        Operator="GreaterThan"
                                                        SetFocusOnError="true"
                                                        Type="Date"
                                                        ValidationGroup="submit"
                                                        CultureInvariantValues="true" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Exam Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlexamtype" AppendDataBoundItems="true" runat="server" TabIndex="1" data-select2-enable="true"
                                                    CssClass="form-control">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="0">Regular</asp:ListItem>
                                                    <asp:ListItem Value="1">Backlog</asp:ListItem>
                                                    <asp:ListItem Value="2">PhotoCopy/Revaluation</asp:ListItem>
                                                    <asp:ListItem Value="3">Redo/Resit</asp:ListItem>
                                                </asp:DropDownList>
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
                                                    <asp:CheckBox ID="chkInstitute" runat="server" Text="All Institute" AutoPostBack="true" onClick="SelectAllInstitute()" CssClass="select-all-checkbox" TabIndex="1" />
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
                                                    <asp:CheckBox ID="chkDegree" runat="server" Text="All Degree" AutoPostBack="true" onClick="SelectAllDegree()" CssClass="select-all-checkbox" TabIndex="1" />
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
                                                    <asp:CheckBox ID="chkBranch" runat="server" Text="All Branches" onClick="SelectAllBranch()" CssClass="select-all-checkbox" TabIndex="1" />
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
                                                    <asp:CheckBox ID="chkSemester" runat="server" Text="All Semesters" onclick="SelectAllSem()" CssClass="select-all-checkbox" TabIndex="1" />
                                                    <asp:CheckBoxList ID="chkSemesterList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column">
                                                <div class="label-dynamic">
                                                    <label>User Rights</label>
                                                </div>
                                                <div class="checkbox-list-box">
                                                    <asp:CheckBox ID="chkUserRights" runat="server" Text="All User" onclick="SelectAllUsers()" CssClass="select-all-checkbox" TabIndex="1" />
                                                    <asp:CheckBoxList ID="chkUserRightsList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
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
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validate();"
                                            OnClick="btnSubmit_Click" TabIndex="1" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" OnClick="btnCancel_Click"
                                            CssClass="btn btn-warning" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="submit" />

                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvSessionActivities" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Exam Activities</h5>
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
                                                                <th>Exam Type</th>
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
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" TabIndex="1" ToolTip="Edit Record" />
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
                                                        <td><%# Eval("SHOWSTATUS")%></td>
                                                        <td><%# Eval("EXAM_TYPE")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
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
                $(ddlCollege).focus();
                return false;
            }

            var idtxtweb = $("[id$=ddlActivity]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value == 0) {
                alert('Please Select Activity', 'Warning!');
                $(txtweb).focus();
                return false;
            }


            var idtxtweb = $("[id$=txtStartDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Start Date', 'Warning!');
                $(txtweb).focus();
                return false;
            }


            var idtxtweb = $("[id$=txtEndDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter End Date', 'Warning!');
                $(txtweb).focus();
                return false;

            }
            var idtxtweb = $("[id$=ddlSession]").attr("id");
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

    </script>

</asp:Content>
