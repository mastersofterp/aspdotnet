<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceConfig.aspx.cs" Inherits="ACADEMIC_AttendanceConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .table {
            width: auto !important;
        }
    </style>
     <%--<style>
        .multiselect-container {
            position: absolute;
            transform: translate3d(0px, -46px, 0px);
            top: 0px;
            left: 0px;
            will-change: transform;
            height: 200px;
            overflow: auto;
        }
    </style>--%>


     <%--<script>
         $(document).ready(function () {
             $('.multi-select-demo').multiselect({
                 includeSelectAllOption: true,
                 enableFiltering: true,
                 filterPlaceholder: 'Search',
                 enableCaseInsensitiveFiltering: true,
                 enableHTML: true,
                 templates: {
                     filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
                     filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 33px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
                 }
                 //dropRight: true,
                 //search: true,
             });
         });

         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $(document).ready(function () {
                 $('.multi-select-demo').multiselect({
                     includeSelectAllOption: true,
                     enableFiltering: true,
                     filterPlaceholder: 'Search',
                     enableCaseInsensitiveFiltering: true,
                     enableHTML: true,
                     templates: {
                         filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
                         filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 33px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
                     }
                     //dropRight: true,
                     //search: true,
                 });
             });
         });
         
    </script>--%>

     <style>
        .ms-options-wrap {
            position: relative;
        }

        .ms-options {
            /*position:absolute;
        bottom:10px;left:0;
        width:100%;*/
            min-height: inherit !important;
        }

        #example2_wrapper #example2_filter {
            /*display: none;*/
        }
    </style>

    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>

    <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999;
            /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
        }
    </style>

    <style type="text/css">
        .DefaultCheckBoxList li {
            float: left;
            list-style-type: none;
        }

        @media screen and (max-width: 300px) {
            .DefaultCheckBoxList li {
                float: none;
            }
        }
    </style>

   <%-- <script type="text/javascript">

        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                InitAutoCompl();
            }

        });

    </script>

     <script type="text/javascript">
         document.onreadystatechange = function () {
             var state = document.readyState
             if (state == 'interactive') {
                 document.getElementById('contents').style.visibility = "hidden";
             } else if (state == 'complete') {
                 setTimeout(function () {
                     document.getElementById('interactive');
                     document.getElementById('load').style.visibility = "hidden";
                     document.getElementById('contents').style.visibility = "visible";
                 }, 1000);
             }
         }
    </script>--%>
    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tab-le').DataTable({
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
                                return $('#tab-le').DataTable().column(idx).visible();
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
                                                return $('#tab-le').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#tab-le').DataTable().column(idx).visible();
                                            }
                                        }
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
                var table = $('#tab-le').DataTable({
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
                                    return $('#tab-le').DataTable().column(idx).visible();
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
                                                    return $('#tab-le').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#tab-le').DataTable().column(idx).visible();
                                                }
                                            }
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
    <asp:HiddenField ID="hfdSms" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdEmail" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdCourse" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdTeaching" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpnl"
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <form role="form">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-12">
                                    <asp:UpdatePanel ID="updpnl" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSchoolInstitute" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                    </asp:DropDownList>

                                                    <%-- <asp:ListBox ID="lstbxSchool" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true"></asp:ListBox>--%>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSchoolInstitute"
                                                        Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="" ValidationGroup="course">
                                                    </asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <%--<asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                                        AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" >
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                    <asp:ListBox ID="ddlSession" runat="server" SelectionMode="Multiple" AutoPostBack="true" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"  OnSelectedIndexChanged="ddlSession_SelectedIndexChanged1"></asp:ListBox>
                                                   <%-- <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divDepartment" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true">Department</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                                        Display="None" ErrorMessage="Please Select ddlDepartment" InitialValue="0" ValidationGroup="course">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true">Degree</asp:Label>
                                                    </div>
                                                   <%-- <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                                        CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                     <asp:ListBox ID="ddlDegree" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="2" AutoPostBack="true"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 ">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Scheme Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSchemeType" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" TabIndex="3" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlSchemeType" runat="server" ControlToValidate="ddlSchemeType" SetFocusOnError="true"
                                                        Display="None" ErrorMessage="Please Select Scheme Type" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">Semester</asp:Label>
                                                    </div>
                                                   <%-- <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True"
                                                        CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                    <asp:ListBox ID="ddlSemester" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="4" AutoPostBack="true"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Attendance Start Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="txtStartDate1" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" TabIndex="5" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                                        <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtStartDate" PopupButtonID="txtStartDate1" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                            DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                            ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Attendance Start Date" IsValidEmpty="false"
                                                            InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" ErrorMessage="Start Date is Invalid (Enter dd/mm/yyyy Format)"
                                                            TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="submit" SetFocusOnError="True" />
                                                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                            Display="None" SetFocusOnError="True"
                                                            ValidationGroup="submit" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Attendance End Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="txtEndDate1" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" TabIndex="6" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtEndDate" PopupButtonID="txtEndDate1" />

                                                        <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                            DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                            ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Attendance End Date"
                                                            InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" IsValidEmpty="false"
                                                            TooltipMessage="Please Enter Attendance End Date" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />

                                                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                            ControlToValidate="txtEndDate" Display="None"
                                                            ValidationGroup="submit" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 ">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Attendance Lock By Day</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAttLockDay" runat="server" CssClass="form-control" TabIndex="7" MaxLength="3" />
                                                    <asp:RequiredFieldValidator ID="rfvSessionLName" runat="server" SetFocusOnError="True"
                                                        ErrorMessage="Please Enter Attendance Lock By Day" ControlToValidate="txtAttLockDay"
                                                        Display="None" ValidationGroup="submit" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtAttLockDay" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%-- <div class="form-group col-md-4">
                                    <span style="color: #FF0000; font-weight: bold">*</span>
                                    <label for="city">Attendance Lock By Hours </label>
                                    <asp:TextBox ID="txtAttLockHrs" runat="server" CssClass="form-control" placeholder="HH:MM" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeEnterTimeFrom" runat="server" ClearMaskOnLostFocus="false"
                                        MaskType="Time" Mask="99:99" TargetControlID="txtAttLockHrs" Filtered=":" AcceptAMPM="false"
                                        ClearTextOnInvalid="True" AutoCompleteValue="00:00">
                                    </ajaxToolKit:MaskedEditExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Attendance Lock By Hours" ControlToValidate="txtAttLockHrs"
                                        Display="None" ValidationGroup="submit" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevFrom" runat="server" ControlExtender="meeEnterTimeFrom"
                                        ControlToValidate="txtAttLockHrs" IsValidEmpty="False" EmptyValueMessage="Time is required"
                                        InvalidValueMessage="Hours is invalid" Display="None" TooltipMessage="Input a time hours"
                                        EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" ValidationGroup="submit"
                                        ErrorMessage="mevEnterTime" />

                                </div>--%>
                                <div class="form-group col-lg-2 col-md-6 col-6">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>SMS Facility</label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rdSMSYes" name="switch" checked />
                                        <label data-on="Yes" data-off="No" for="rdSMSYes"></label>
                                    </div>
                                </div>


                                <div class="form-group col-lg-2 col-md-6 col-6">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Email Facility</label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rdEmailYes" name="switch" checked />
                                        <label data-on="Yes" data-off="No" for="rdEmailYes"></label>
                                    </div>
                                </div>


                                <div class="form-group col-lg-2 col-md-6 col-6" style="display:none">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Course Registration</label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rbCRegBefore" name="switch" checked />
                                        <label data-on="Before" data-off="After" for="rbCRegBefore"></label>
                                    </div>
                                </div>


                                <div class="form-group col-lg-2 col-md-6 col-6">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Teaching Plan</label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rdTeachYes" name="switch" checked />
                                        <label data-on="Yes" data-off="No" for="rdTeachYes"></label>
                                    </div>
                                </div>


                                <div class="form-group col-lg-2 col-md-6 col-6">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Check For Active</label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rdActive" name="switch" checked />
                                        <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="1" ValidationGroup="submit" OnClientClick="return validate();" OnClick="btnSubmit_Click" class="btn btn-primary" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" OnClick="btnCancel_Click" class="btn btn-warning" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                        </div>

                        <div class="col-12">
                            <asp:ListView ID="lvAttConfig" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <div class="sub-heading">
                                            <h5>Configuration List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tab-le">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Edit</th>
                                                    <th>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblDYrdoSchemeType" runat="server" Font-Bold="true"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>
                                                    <th>Start Date</th>
                                                    <th>End Date</th>
                                                    <th>SMS Facility</th>
                                                    <th>Email Facility</th>
                                                    <th>Att Lock Days</th>
                                                    <%--  <th>Att Lock Hours</th>--%>
                                                    <th style="display:none">Course Reg.(Before/After)</th>
                                                    <th>Teaching Plan</th>
                                                    <th>Active</th>
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
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("SRNO")%>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                        </td>
                                        <td><%#Eval("SESSION_PNAME") %></td>
                                        <td><%#Eval("COLLEGE_NAME") %></td>
                                        <td><%#Eval("DEGREENAME")%></td>
                                        <td><%#Eval("SCHEMETYPENAME")%></td>
                                        <td><%#Eval("SEMESTERNAME")%></td>
                                        <td><%#Eval("START_DATE")%></td>
                                        <td><%#Eval("END_DATE")%></td>
                                        <td><%#Eval("SMS_FACILITY")%></td>
                                        <td><%#Eval("EMAIL_FACILITY")%></td>
                                        <td><%#Eval("LOCK_ATT_DAYS")%></td>
                                        <%-- <td><%#Eval("LOCK_ATT_HOURS")%></td>--%>
                                        <td style="display:none"><%#Eval("CREG_STATUS")%></td>
                                        <td><%#Eval("TEACHING_PLAN")%></td>
                                        <td><%#Eval("ACTIVE")%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                    </form>
                </div>
            </div>
        </div>
        <!--academic Calendar-->
    </div>

     

    <script>
        function SetStatSms(val) {
            $('[id*=rdSMSYes]').prop('checked', val);
        }
        function SetStatEmail(val) {
            $('[id*=rdEmailYes]').prop('checked', val);
        }
        function SetStatCourse(val) {
            $('#rbCRegBefore').prop('checked', val);
        }
        function SetStatTeaching(val) {
            $('#rdTeachYes').prop('checked', val);
        }
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {
            //var school = document.getElementById("ctl00_ContentPlaceHolder1_ddlSchoolInstitute").value;
            var session = document.getElementById("ctl00_ContentPlaceHolder1_ddlSession").value;
            var degree = document.getElementById("ctl00_ContentPlaceHolder1_ddlDegree").value;
            var schmType = document.getElementById("ctl00_ContentPlaceHolder1_ddlSchemeType").value;
            var semester = document.getElementById("ctl00_ContentPlaceHolder1_ddlSemester").value;
            var attLock = document.getElementById("ctl00_ContentPlaceHolder1_txtAttLockDay").value;
            var AttStartDate = $("#ctl00_ContentPlaceHolder1_txtStartDate").val();
            var AttEndDtae = document.getElementById("ctl00_ContentPlaceHolder1_txtEndDate").value;
            //if (school == "0") {
            //    alert("Please Select School/Institute Name.");
            //    return false;
            //}
            if (session == "") {
                alert("Please Select Session.");
                return false;
            }
            if (degree == "") {
                alert("Please Select Degree.");
                return false;
            }
            if (schmType == "0") {
                alert("Please Select Scheme Type.");
                return false;
            }
            if (semester == "") {
                alert("Please Select Semester.");
                return false;
            }
            if (AttStartDate == "" || AttStartDate == "DD/MM/YYYY") {
                alert("Please Enter Attendance Start Date.");
                return false;
            }
            if (AttEndDtae == "" || AttEndDtae == "DD/MM/YYYY") {
                alert("Please Enter Attendance End Date.");
                return false;
            }
            if (attLock == "") {
                alert("Please Select Attendance Lock By Day.");
                return false;
            }
            //alert(school);
            $('#hfdSms').val($('#rdSMSYes').prop('checked'));
            $('#hfdEmail').val($('#rdEmailYes').prop('checked'));
            $('#hfdCourse').val($('#rbCRegBefore').prop('checked'));
            $('#hfdTeaching').val($('#rdTeachYes').prop('checked'));
            $('#hfdActive').val($('#rdActive').prop('checked'));
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





   <%-- <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                    enableHTML: true,
                    templates: {
                        filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
                        filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 33px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
                    }
                    //dropRight: true,
                    //search: true,
                });

            });
        });
    </script>--%>




  <%-- <script>
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
    </script>--%>

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

</asp:Content>

