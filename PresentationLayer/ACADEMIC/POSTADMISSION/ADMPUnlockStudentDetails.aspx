<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPUnlockStudentDetails.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPUnlockStudentDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
    <%-- <style>
            .multiselect.dropdown-toggle::after {
            display: none;
        }

        .daterangepicker .drp-calendar .table-condensed {
            display: none;
        }
    </style>--%>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="updunlock" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <%--<asp:Label ID="lbllabel" runat="server" Text="Unlock Student Details"></asp:Label>--%>
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlPortal" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Admission Batch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="frvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="show"
                                                ErrorMessage="Please Select Admission Batch" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Applicant Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlApplicantType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlApplicantType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">UG</asp:ListItem>
                                                <asp:ListItem Value="2">PG</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvApplicantType" runat="server" ControlToValidate="ddlApplicantType" ValidationGroup="show"
                                                ErrorMessage="Please Select Applicant Type" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Degree - Program</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegreeProgram" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegreeProgram" runat="server" ControlToValidate="ddlDegreeProgram" ValidationGroup="show"
                                                ErrorMessage="Please Select Degree - Program" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Search By</label>
                                            </div>
                                            <asp:RadioButton ID="rboApplicationNo" runat="server" Text="Application No." GroupName="unlock" />&nbsp;&nbsp;
                                         <asp:RadioButton ID="rboName" runat="server" Text="Name" GroupName="unlock" />&nbsp;&nbsp     
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Search String</label>
                                            </div>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <%--<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />--%>
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="show" TabIndex="1" />
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnsubmit_Click" ValidationGroup="submit" Visible="false" TabIndex="1" />
                                    <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClear_Click" TabIndex="1" />
                                    <asp:ValidationSummary ID="vsshow" runat="server" ValidationGroup="show" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                                <div id="divAllowProcess" runat="server" visible="false">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Allow Process</label>
                                                </div>
                                                <asp:ListBox ID="lstbxAllowProcess" runat="server" CssClass="form-control multi-select-demo"
                                                    AppendDataBoundItems="true" SelectionMode="Multiple" TabIndex="7"></asp:ListBox>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Start - End Date/Time</label>
                                                </div>
                                                <asp:TextBox ID="txtStartEndDateTime" runat="server" TabIndex="6" CssClass="form-control" Width="100%"
                                                    ToolTip="Please Enter Exam Time" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row" style="display: none">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><sup>* </sup>Start Date</label>
                                                <asp:TextBox ID="txtStartDate" runat="server" type="date" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><sup>* </sup>Start Time</label>
                                                <asp:TextBox ID="txtStartTime" runat="server" TabIndex="8" CssClass="form-control" ToolTip="Please Enter Start Time."></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="meStarttime" runat="server" TargetControlID="txtStartTime"
                                                    Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                    MaskType="Time" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><sup>* </sup>End Date</label>
                                                <asp:TextBox ID="txtEndDate" runat="server" type="date" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>End Time</label>
                                                </div>
                                                <asp:TextBox ID="txtEndTime" runat="server" TabIndex="10" CssClass="form-control" ToolTip="Please Enter End Time."></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="meEndTime" runat="server" TargetControlID="txtEndTime"
                                                    Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                    MaskType="Time" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div id="divStudnetDetails" class="col-12" runat="server" visible="false">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvStudnetDetails" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblData">
                                                        <thead class="bg-light-blue">
                                                            <tr>

                                                                <th>
                                                                    <asp:CheckBox ID="chkAll" runat="server" onchange="CheckAll(this)" /></th>
                                                                <th>Candidate Name</th>
                                                                <th>Email Id</th>
                                                                <th>Mobile No</th>
                                                                <th>Start Date Time</th>
                                                                <th>End Date Time</th>
                                                                <th>Stage Name</th>
                                                                <th>Application Id</th>
                                                                <th>Allow Process</th>
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
                                                        <asp:CheckBox ID="chkStud" runat="server" />
                                                        <asp:HiddenField ID="hdnUserno" runat="server" Value='<%#Eval("USERNO") %>' />
                                                    </td>
                                                    <td><%#Eval("FULLNAME") %></td>
                                                    <td><%#Eval("EMAILID") %></td>
                                                    <td><%#Eval("MOBILENO") %></td>
                                                    <td><%#Eval("STARTDATE") %> <%#Eval("STARTIME") %></td>
                                                    <td><%#Eval("ENDDATE") %> <%#Eval("ENDTIME") %></td>
                                                    <td><%#Eval("STAGENAME") %></td>
                                                    <td><%#Eval("APPLICATION_ID") %></td>
                                                    <td><%#Eval("ALLOWPROCESSNAME") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlErp" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Admission Batch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlErpAdmBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvErpAdmBatch" runat="server" ControlToValidate="ddlErpAdmBatch" ValidationGroup="erpshow"
                                                ErrorMessage="Please Select Admission Batch" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Institute Name</label>
                                                <asp:Label ID="lblErpInstituteName" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlErpInstituteName" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                AppendDataBoundItems="True" AutoPostBack="True"
                                                ToolTip="Please Select Institute" OnSelectedIndexChanged="ddlErpInstituteName_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvInstName" runat="server" ControlToValidate="ddlErpInstituteName"
                                                Display="None" ErrorMessage="Please Select Institute" ValidationGroup="erpshow"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                                <asp:Label ID="lblErpDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlErpDegree" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlErpDegree_SelectedIndexChanged"
                                                AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlErpDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="erpshow"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Branch</label>
                                                <asp:Label ID="lblErpBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlErpBranch" runat="server" AppendDataBoundItems="True" CssClass="form-control" 
                                                data-select2-enable="true" TabIndex="1" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnErpShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="erpshow" TabIndex="1" OnClick="btnErpShow_Click" />
                                    <asp:Button ID="btnErpSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Visible="false" TabIndex="1" OnClick="btnErpSubmit_Click" />
                                    <asp:Button ID="btnErpClear" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="1" OnClick="btnErpClear_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="erpshow" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                                <div id="divErpAllowProcess" runat="server" visible="false">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Allow Process</label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxErpAllowProcess" runat="server" CssClass="form-control multi-select-demo"
                                                        AppendDataBoundItems="true" SelectionMode="Multiple" TabIndex="7"></asp:ListBox>
                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Start - End Date/Time</label>
                                                    </div>
                                                    <asp:TextBox ID="txtErpSEDateTime" runat="server" TabIndex="1" CssClass="form-control" Width="100%"
                                                        ToolTip="Please Enter Exam Time" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                <div id="divErpUnlockDetails" class="col-12" runat="server" visible="false">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:ListView ID="lvErpUnlockDetails" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblData">
                                                        <thead class="bg-light-blue">
                                                            <tr>

                                                                <th>
                                                                    <asp:CheckBox ID="chkAll" runat="server" onchange="CheckAll(this)" /></th>
                                                                <th>Registration No</th>
                                                                <th>Candidate Name</th>
                                                                <th>Email Id</th>
                                                                <th>Mobile No</th>
                                                                <th>Degree - Branch</th>
                                                                <th>Start Date Time</th>
                                                                <th>End Date Time</th>
                                                                <th>Allow Process</th>
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
                                                        <asp:CheckBox ID="chkStud" runat="server" />
                                                        <asp:HiddenField ID="hdnIdno" runat="server" Value='<%#Eval("IDNO") %>' />
                                                    </td>
                                                    <td><%#Eval("REGNO") %></td>
                                                    <td><%#Eval("FULLNAME") %></td>
                                                    <td><%#Eval("EMAILID") %></td>
                                                    <td><%#Eval("STUDENTMOBILE") %></td>
                                                    <td><%#Eval("DEGREEBRANCH") %></td>
                                                    <td><%#Eval("STARTDATE") %> <%#Eval("STARTIME") %></td>
                                                    <td><%#Eval("ENDDATE") %> <%#Eval("ENDTIME") %></td>
                                                    <td><%#Eval("ALLOWPROCESSNAME") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

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

        function CheckAll(checkid) {
            var updateButtons = $('#tblData input[type=checkbox]');
            if ($(checkid).children().is(':checked')) {
                updateButtons.each(function () {
                    if (($(this).attr("id") != $(checkid).children().attr("id"))) {
                        $(this).prop("checked", true);
                    }
                });
            }
            else {
                updateButtons.each(function () {
                    if ($(this).attr("id") != $(checkid).children().attr("id")) {
                        $(this).prop("checked", false);
                    }
                });
            }
        }

    </script>

    <script>
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_txtStartEndDateTime').daterangepicker({
                DatePicker: true,
                timePicker: true,
                locale: {
                    format: 'DD/MM/YYYY hh:mm A'
                },
            },
            function (start, end, label) {
                $('#ctl00_ContentPlaceHolder1_txtStartEndDateTime').val(start.format('DD/MM/YYYY hh:mm A') + ' - ' + end.format('DD/MM/YYYY hh:mm A'));
            });
            $("#ctl00_ContentPlaceHolder1_txtStartEndDateTime").on('apply.daterangepicker', function (ev, Picker) {
                // $('#ctl00_ContentPlaceHolder1_txtExamTime').val(Picker.startDate.format('hh:mm A') + ' - ' + Picker.endDate.format('hh:mm A'));
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#ctl00_ContentPlaceHolder1_txtStartEndDateTime').daterangepicker({
                    DatePicker: true,
                    timePicker: true,
                    locale: {
                        format: 'DD/MM/YYYY hh:mm A'
                    },
                },
                function (start, end, label) {
                    $('#ctl00_ContentPlaceHolder1_txtStartEndDateTime').val(start.format('DD/MM/YYYY hh:mm A') + ' - ' + end.format('DD/MM/YYYY hh:mm A'));
                });
                $("#ctl00_ContentPlaceHolder1_txtStartEndDateTime").on('apply.daterangepicker', function (ev, Picker) {
                    // $('#ctl00_ContentPlaceHolder1_txtExamTime').val(Picker.startDate.format('hh:mm A') + ' - ' + Picker.endDate.format('hh:mm A'));
                });
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_txtErpSEDateTime').daterangepicker({
                DatePicker: true,
                timePicker: true,
                locale: {
                    format: 'DD/MM/YYYY hh:mm A'
                },
            },
            function (start, end, label) {
                $('#ctl00_ContentPlaceHolder1_txtErpSEDateTime').val(start.format('DD/MM/YYYY hh:mm A') + ' - ' + end.format('DD/MM/YYYY hh:mm A'));
            });
            $("#ctl00_ContentPlaceHolder1_txtErpSEDateTime").on('apply.daterangepicker', function (ev, Picker) {
                // $('#ctl00_ContentPlaceHolder1_txtExamTime').val(Picker.startDate.format('hh:mm A') + ' - ' + Picker.endDate.format('hh:mm A'));
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#ctl00_ContentPlaceHolder1_txtErpSEDateTime').daterangepicker({
                    DatePicker: true,
                    timePicker: true,
                    locale: {
                        format: 'DD/MM/YYYY hh:mm A'
                    },
                },
                function (start, end, label) {
                    $('#ctl00_ContentPlaceHolder1_txtErpSEDateTime').val(start.format('DD/MM/YYYY hh:mm A') + ' - ' + end.format('DD/MM/YYYY hh:mm A'));
                });
                $("#ctl00_ContentPlaceHolder1_txtErpSEDateTime").on('apply.daterangepicker', function (ev, Picker) {
                    // $('#ctl00_ContentPlaceHolder1_txtExamTime').val(Picker.startDate.format('hh:mm A') + ' - ' + Picker.endDate.format('hh:mm A'));
                });
            });
        });
    </script>

</asp:Content>

