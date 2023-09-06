<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="EventAndHolidayEntry.aspx.cs" Inherits="ACADEMIC_MASTERS_EventAndHolidayEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <script>
        $(document).ready(function () {
            var table = $('#tblEvents').DataTable({
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
                            var arr = [0, 1];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblEvents').DataTable().column(idx).visible();
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
                                            var arr = [0, 1];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblEvents').DataTable().column(idx).visible();
                                            }
                                        },
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0, 1];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblEvents').DataTable().column(idx).visible();
                                            }
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
                var table = $('#tblEvents').DataTable({
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
                                var arr = [0, 1];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblEvents').DataTable().column(idx).visible();
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
                                                var arr = [0, 1];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblEvents').DataTable().column(idx).visible();
                                                }
                                            },
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0, 1];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblEvents').DataTable().column(idx).visible();
                                                }
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
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updHoliday"
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

    <asp:UpdatePanel ID="updHoliday" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">

                            <div class="col-12">
                                <div class="form-group col-12">

                                    <asp:RadioButton ID="rbnHolidayMaster" runat="server" GroupName="ST" Text="Holiday Entry" Checked="false"
                                            AutoPostBack="true" OnCheckedChanged="rbnHolidayMaster_CheckedChanged" />
                                    &nbsp;
                                    <asp:RadioButton ID="rbnHoliday" runat="server" Checked="true" GroupName="ST" Text="Holiday Mapping"
                                        AutoPostBack="true" OnCheckedChanged="rbnHoliday_CheckedChanged" />
                                    &nbsp;
                                        <asp:RadioButton ID="rbnEvent" runat="server" GroupName="ST" Text="Event"
                                            Checked="False" AutoPostBack="true"
                                            OnCheckedChanged="rbnEvent_CheckedChanged" />
                                    &nbsp;
                                        <asp:RadioButton ID="rbnSuspendClass" runat="server" GroupName="ST" Text="Suspend Class" Checked="false"
                                            AutoPostBack="true" OnCheckedChanged="rbnSuspendClass_CheckedChanged" />
                                    
                                        
                                </div>
                            </div>
                            <div id="divholidayMapping" runat="server">
                                <div class="col-12">
                                    <div class="row">

                                        <%--<div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Session"
                                                InitialValue="0" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege" Display="None"
                                                ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Report" />
                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12 ">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Session"
                                                InitialValue="0" SetFocusOnError="true" />
                                            <asp:HiddenField ID="hiddenfieldfromDt" runat="server" Visible="False" />
                                            <asp:HiddenField ID="hiddenFieldToDt" runat="server" Visible="False" />
                                            <asp:RequiredFieldValidator ID="rfvSessionrpt" runat="server" ControlToValidate="ddlSession" Display="None"
                                                ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" ValidationGroup="Report" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>

                                            <asp:ListBox runat="server" ID="ddlCollege" SelectionMode="Multiple" CssClass="form-control multi-select-demo"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="rfvddlCollege" ControlToValidate="ddlCollege" InitialValue="" SetFocusOnError="true"
                                                Display="None" ValidationGroup="Submit" runat="server" ErrorMessage="Please Select College."></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12 ">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Holiday List</label>--%>
                                                <asp:Label ID="lblholiday" runat="server" Text=" Holiday List"  Font-Bold="true"></asp:Label></label>
                                            </div>
                                            <asp:DropDownList ID="ddlHolidayList" runat="server" AppendDataBoundItems="true"
                                                TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlHolidayList"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Holiday List"
                                                SetFocusOnError="true" InitialValue="0" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic" id="lblmultiple" runat="server">
                                                <%--<sup>* </sup>--%>
                                                <label>For Multiple Days Holiday</label>
                                            </div>
                                            <asp:CheckBox ID="ChkDate" runat="server" AutoPostBack="True" TabIndex="3"
                                                OnCheckedChanged="ChkDate_CheckedChanged" Text="Check This box to Select From Date and To Date" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    <asp:Label ID="lblFromDate" runat="server" Text=" Holiday Date :"></asp:Label></label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-addon" id="imgFromDate">
                                                    <i class="fa fa-calendar text-green"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="4" ValidationGroup="submit" />
                                                <%-- <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgFromDate" OnClientDateSelectionChanged="selectfromdate" />
                                                <%--<asp:RangeValidator ID="rvFromDt" Type="Date" ControlToValidate="txtFromDate" runat="server"
                                                ErrorMessage="RangeValidator" EnableClientScript="False"></asp:RangeValidator>--%>
                                                <%--<ajaxToolKit:ValidatorCalloutExtender ID="rvFromDt_ValidatorCalloutExtender" 
                                                runat="server" Enabled="True" TargetControlID="rvFromDt">
                                                </ajaxToolKit:ValidatorCalloutExtender>--%>
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" />
                                                <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" EmptyValueMessage="Please Enter Holiday Date"
                                                    ControlExtender="meFromDate" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                                    InvalidValueMessage=" Date is invalid" Display="None" ErrorMessage="Please Enter Holiday Date"
                                                    InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tdToDate" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    <asp:Label ID="lblTodate" runat="server" Text=" To Date :" Font-Bold="true"></asp:Label></label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-addon" id="imgToDate">
                                                    <i class="fa fa-calendar text-green"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" ValidationGroup="submit" />
                                                <%-- <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDate" PopupButtonID="imgToDate" OnClientDateSelectionChanged="selectfromdate" />
                                                <%--<asp:RangeValidator ID="rvFromDt" Type="Date" ControlToValidate="txtFromDate" runat="server"
                                                ErrorMessage="RangeValidator" EnableClientScript="False"></asp:RangeValidator>--%>
                                                <%--<ajaxToolKit:ValidatorCalloutExtender ID="rvFromDt_ValidatorCalloutExtender" 
                                                runat="server" Enabled="True" TargetControlID="rvFromDt">
                                                </ajaxToolKit:ValidatorCalloutExtender>--%>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDate"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter To Date"
                                                    ControlExtender="meFromDate" ControlToValidate="txtToDate" IsValidEmpty="false"
                                                    InvalidValueMessage=" Date is invalid" Display="None" ErrorMessage="Please Enter To Date"
                                                    InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic" id="lblDetails" runat="server">
                                                <%-- <sup>* </sup>--%>
                                                <label>Holiday Detail (if any)</label>
                                            </div>
                                            <asp:TextBox ID="txtEventDetail" runat="server" TabIndex="5" TextMode="MultiLine" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">

                                            <asp:CheckBox ID="chkcancel" runat="server" TabIndex="6"
                                                Text="check if required cancel time table" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Day Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="true"
                                                TabIndex="6" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDay" runat="server" ControlToValidate="ddlDay"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Day Name"
                                                SetFocusOnError="true" InitialValue="0" />
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="5"
                                        CssClass="btn btn-primary" OnClick="btnSubmit_Click" /><%--//OnClientClick="return Validation();" />--%>
                                    <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" Visible="false"
                                        OnClick="btnReport_Click" ValidationGroup="Report" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="5" />

                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Report" />
                                    <asp:ValidationSummary ID="valSummery0" runat="server" DisplayMode="List"
                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                </div>
                                <div class="form-group col-lg-8 col-md-12 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading">Note</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>If you want to Cancel Individual Time Table use Cancel Time Table Option.</span>  </p>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Carry out the transaction responsibly, timetable once cancelled cannot be reverted.</span>  </p>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvExamday" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Holiday / Event Days List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblEvents">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th style="display: none">Delete
                                                        </th>
                                                        <th>Holiday / Event Name
                                                        </th>
                                                        <th>Holiday / Event Start Date
                                                        </th>
                                                        <th>Holiday / Event End Date
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
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
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="true" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("GROUPID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td style="display: none">
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("GROUPID") %>'
                                                        ToolTip="Delete Record" OnClientClick="return ConfirmSubmit();" OnClick="btnDelete_Click" />
                                                </td>

                                                <td>
                                                    <%# Eval("ACADEMIC_HOLIDAY_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ACADEMIC_HOLIDAY_STDATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ACADEMIC_HOLIDAY_ENDDATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SHORT_NAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                            <div id="divholidaymaster" runat="server" visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup id="mandatory" runat="server" visible="false">* </sup>
                                                <label id="lblName" runat="server">Holiday Name</label>
                                            </div>
                                            <asp:TextBox ID="txtEventName" runat="server" TabIndex="1" CssClass="form-control">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEventName"
                                                ValidationGroup="Holiday" Display="None" ErrorMessage="Please Enter Holiday Name"
                                                SetFocusOnError="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Holiday/Event for</label>
                                            </div>
                                            <asp:DropDownList ID="ddlHolidayfor" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="H">Holiday/Event</asp:ListItem>
                                                <asp:ListItem Value="E">Event</asp:ListItem>
                                                <asp:ListItem Value="S">Suspend Class</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlHolidayfor"
                                                ValidationGroup="Holiday" Display="None" ErrorMessage="Please Select Holiday/Event for"
                                                InitialValue="0" SetFocusOnError="true" />
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmitHoliday" runat="server" Text="Submit" ValidationGroup="Holiday" TabIndex="1"
                                            CssClass="btn btn-primary" OnClick="btnSubmitHoliday_Click" />
                                        <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClear_Click" TabIndex="1" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Holiday" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlLV" runat="server">
                                            <asp:ListView ID="lvHolidayList" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <h5>Holiday List</h5>
                                                        </div>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                        <tr>
                                                                            <th>Edit
                                                                            </th>
                                                                            <%--<th>Sr. No.
                                                                        </th>--%>
                                                                            <th>Holiday Name
                                                                            </th>
                                                                            <th>Holiday For
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>

                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <%--<td>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </td>--%>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditHoliday" runat="server" AlternateText="Edit Record"
                                                                CommandArgument='<%# Eval("HOLIDAY_LISTNO") %>' ImageUrl="~/images/Edit.png"
                                                                OnClick="btnEditHoliday_Click" ToolTip="Edit Record" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("HOLIDAY_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("HOLIDAY_FOR")%>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="rbnHolidayMaster" />
            <asp:PostBackTrigger ControlID="rbnHoliday" />
            <asp:PostBackTrigger ControlID="rbnEvent" />
            <asp:PostBackTrigger ControlID="rbnSuspendClass" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="Server">
    </div>


    <script type="text/javascript">
        function selectfromdate(sender, args) {
        }

        function ConfirmSubmit() {
            var ret = confirm('Are you sure to delete this entry?');
            if (ret == true)
                return true;
            else
                return false;
        }

        function Validation() {
            if ($('[id*=ctl00_ContentPlaceHolder1_ddlCollege] option:selected').val() == "0") {
                alert('Please Select School/Institute.');
                return false;
            }
            if ($('[id*=ctl00_ContentPlaceHolder1_ddlSession] option:selected').val() == "0") {
                alert('Please Select Session.');
                return false;
            }
            if ($('[id*=ctl00_ContentPlaceHolder1_rbnHoliday]').is(':checked')) {
                if ($('[id*=ctl00_ContentPlaceHolder1_txtEventName]').val() == "") {
                    alert('Please Enter Holiday Name.');
                    return false;
                }
                if ($('[id*=ctl00_ContentPlaceHolder1_ChkDate]').is(":checked")) {
                    if ($('[id*=ctl00_ContentPlaceHolder1_txtFromDate]').val() == "") {
                        alert('Please Enter Holiday From Date.');
                        return false;
                    }
                    else if ($('[id*=ctl00_ContentPlaceHolder1_txtToDate]').val() == "") {
                        alert('Please Enter Holiday To Date.');
                        return false;
                    }
                }
                if (!$('[id*=ctl00_ContentPlaceHolder1_ChkDate]').is(":checked")) {
                    if ($('[id*=ctl00_ContentPlaceHolder1_txtFromDate]').val() == "") {
                        alert('Please Enter Holiday Date.');
                        return false;
                    }
                    else return true;
                }
            }
            if ($('[id*=ctl00_ContentPlaceHolder1_rbnEvent]').is(':checked')) {
                if ($('[id*=ctl00_ContentPlaceHolder1_txtEventName]').val() == "") {
                    alert('Please Enter Event Name.');
                    return false;
                }
                if ($('[id*=ctl00_ContentPlaceHolder1_ChkDate]').is(":checked")) {
                    if ($('[id*=ctl00_ContentPlaceHolder1_txtFromDate]').val() == "") {
                        alert('Please Enter Event From Date.');
                        return false;
                    }
                    else if ($('[id*=ctl00_ContentPlaceHolder1_txtToDate]').val() == "") {
                        alert('Please Enter Event To Date.');
                        return false;
                    }
                }
                if (!$('[id*=ctl00_ContentPlaceHolder1_ChkDate]').is(":checked")) {
                    if ($('[id*=ctl00_ContentPlaceHolder1_txtFromDate]').val() == "") {
                        alert('Please Enter Event Date.');
                        return false;
                    }
                    else return true;
                }
            }
            if ($('[id*=ctl00_ContentPlaceHolder1_rbnSuspendClass]').is(':checked')) {
                if ($('[id*=ctl00_ContentPlaceHolder1_txtEventName]').val() == "") {
                    alert('Please Enter Suspend Class Name.');
                    return false;
                }
                if ($('[id*=ctl00_ContentPlaceHolder1_ChkDate]').is(":checked")) {
                    if ($('[id*=ctl00_ContentPlaceHolder1_txtFromDate]').val() == "") {
                        alert('Please Enter Suspend Class From Date.');
                        return false;
                    }
                    else if ($('[id*=ctl00_ContentPlaceHolder1_txtToDate]').val() == "") {
                        alert('Please Enter Suspend Class To Date.');
                        return false;
                    }
                }
                if (!$('[id*=ctl00_ContentPlaceHolder1_ChkDate]').is(":checked")) {
                    if ($('[id*=ctl00_ContentPlaceHolder1_txtFromDate]').val() == "") {
                        alert('Please Enter Suspend Class Date.');
                        return false;
                    }
                    else return true;
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

</asp:Content>
