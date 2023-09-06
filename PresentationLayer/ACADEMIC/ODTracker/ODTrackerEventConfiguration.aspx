<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ODTrackerEventConfiguration.aspx.cs" Inherits="ACADEMIC_ODTracker_ODTrackerEventConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hfdFacultyConfig" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStudentConfig" runat="server" ClientIDMode="Static" />

    <style>
        .nav-tabs-custom .nav-link {
            background: #fff;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updEventConfig"
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
    <asp:UpdatePanel ID="updEventConfig" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Faculty Event Configuration</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <asp:Button Text="Faculty Event Configuration" ID="btnTabFacultyEventConfig" CssClass="nav-link active" runat="server" ToolTip="How many backs days faculty can Create Event for student OD Apply." OnClick="btnTabFacultyEventConfig_Click" />
                                        </li>
                                        <li class="nav-item">
                                            <asp:Button Text="Student Event Configuration" ID="btnTabStudentEventConfig" CssClass="nav-link" runat="server" ToolTip="How many backs days student can apply for Event OD Configuration." OnClick="btnTabStudentEventConfig_Click" />
                                        </li>
                                    </ul>
                                    <div class="tab-content" id="my-tab-content">
                                        <asp:MultiView ID="MainView" runat="server">
                                            <asp:View ID="View1" runat="server">
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Event OD Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlEventType" runat="server" TabIndex="1" ToolTip="Please Select OD Type" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Event OD</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True" InitialValue="0"
                                                                ErrorMessage="Please Select Event OD Type" ControlToValidate="ddlEventType" Display="None" ValidationGroup="FacultyConfig" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Minimum Days Allowed</label>
                                                            </div>
                                                            <asp:TextBox ID="txtMinDaysFaculty" AutoComplete="off" runat="server" CssClass="form-control" TabIndex="2" onkeypress="return isNumber(event)" MaxLength="2"
                                                                ToolTip="Please Enter minimum Days" placeholder="Enter minimum Days" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                                                                ErrorMessage="Please Enter Minimum Days Allowed" ControlToValidate="txtMinDaysFaculty" Display="None" ValidationGroup="FacultyConfig" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="row">
                                                                <div class="form-group col-6">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Status</label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="rdFacultyConfigActive" name="switch" checked />
                                                                        <label data-on="Active" data-off="Inactive" for="rdFacultyConfigActive"></label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitEventConfigFaculty" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="3" ValidationGroup="FacultyConfig" CausesValidation="true" OnClientClick="validateFacultyConfig();" OnClick="btnSubmitEventConfigFaculty_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" DisplayMode="List" ValidationGroup="FacultyConfig" />
                                                </div>

                                                <div class="col-12">
                                                    <div class="table-responsive">
                                                        <asp:ListView ID="lvEventConfigDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="listViewGrid" class="vista-grid">
                                                                    <div class="sub-heading">
                                                                        <h5>Faculty Event Configuration List</h5>
                                                                    </div>
                                                                    <table id="tblEventsConfigDetails" class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center;">Edit
                                                                                </th>
                                                                                <%--<th>Event Type
                                                                                </th>--%>
                                                                                <th>Days Allowed
                                                                                </th>
                                                                                <th>Is Active
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td style="text-align: center;">
                                                                        <asp:ImageButton ID="btnEditFacultyEventConfig" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("CONFIG_SRNO")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="14"
                                                                            OnClick="btnEditFacultyEventConfig_Click" />
                                                                    </td>
                                                                    <%--<td>
                                                                        <%# Eval("EVENT_TYPE")%>
                                                                    </td>--%>
                                                                    <td>
                                                                        <%# Eval("MINIMUM_DAYS_ALLOWED")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>' ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active")||Eval("IS_ACTIVE").ToString().ToLower().Equals("yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="View2" runat="server">
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Event OD Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlStudentEventODType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Event OD</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlEventCategory" runat="server" SetFocusOnError="True" ErrorMessage="Please Select Event OD Type"
                                                                ControlToValidate="ddlStudentEventODType" InitialValue="0" Display="None" ValidationGroup="StudentConfig" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Minimum Days Allowed</label>
                                                            </div>
                                                            <asp:TextBox ID="txtStudentAllowDays" runat="server" CssClass="form-control" onkeypress="return isNumber(event)" MaxLength="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="True" ErrorMessage="Please Enter Minimum Days Allowed"
                                                                ControlToValidate="txtStudentAllowDays" Display="None" ValidationGroup="StudentConfig" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="rdStudentConfigActive" name="switch" checked />
                                                                <label data-on="Active" data-off="Inactive" for="rdStudentConfigActive"></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitEventConfigStudent" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="3" ValidationGroup="StudentConfig" CausesValidation="true" OnClientClick="validateStudentConfig();" OnClick="btnSubmitEventConfigStudent_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="StudentConfig" />
                                                </div>

                                                <div class="col-12">
                                                    <asp:ListView ID="lvEventConfigDetailsStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="listViewGrid" class="vista-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Student Event Configuration List</h5>
                                                                </div>
                                                                <table id="tblEventConfigDetailsStudent" class="table table-striped table-bordered nowrap" style="width: 100% !important;">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center;">Edit
                                                                            </th>
                                                                            <th>Days Allowed
                                                                            </th>
                                                                            <th>Is Active
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <EmptyDataTemplate>
                                                        </EmptyDataTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td style="text-align: center;">
                                                                    <asp:ImageButton ID="btnEditStudentEvenConfig" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%# Eval("CONFIG_SRNO")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="14"
                                                                        OnClick="btnEditStudentEvenConfig_Click" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MINIMUM_DAYS_ALLOWED")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>' ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active") || Eval("IS_ACTIVE").ToString().ToLower().Equals("yes") ?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function SetFacultyConfig(val) {
            $('#rdFacultyConfigActive').prop('checked', val);
        }

        function SetStudentConfig(val) {
            $('#rdStudentConfigActive').prop('checked', val);
        }



        function validateFacultyConfig() {
            $('#hfdFacultyConfig').val($('#rdFacultyConfigActive').prop('checked'));
        }
        function validateStudentConfig() {
            $('#hfdStudentConfig').val($('#rdStudentConfigActive').prop('checked'));
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
