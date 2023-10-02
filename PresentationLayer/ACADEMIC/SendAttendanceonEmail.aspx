<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SendAttendanceonEmail.aspx.cs" Inherits="ACADEMIC_SendAttendanceonEmail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #ctl00_ContentPlaceHolder1_lvAttStatus_DataPager1 a:first-child,
        #ctl00_ContentPlaceHolder1_lvAttStatus_DataPager1 a:last-child {
            padding: 5px 10px;
            border-radius: 0%;
            background: white;
            margin: 0 0px;
            box-shadow: none;
        }

        #ctl00_ContentPlaceHolder1_lvAttStatus_DataPager1 a {
            padding: 5px 10px;
            border-radius: 50%;
            background: white;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }

        #ctl00_ContentPlaceHolder1_lvAttStatus_DataPager1 span {
            padding: 5px 10px;
            border-radius: 50%;
            background: #4183c4;
            color: #fff;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <%--<asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>--%>
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>

                    </h3>

                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Student & Parents</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Absent Student Weekly Basis</a>
                            </li>

                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DynamicLayout="true" DisplayAfter="0" AssociatedUpdatePanelID="updAttendance">
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

                                    <asp:UpdatePanel ID="updAttendance" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" SetFocusOnError="true" ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session"
                                                                InitialValue="0" ValidationGroup="show">
                                                            </asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSchoolInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ControlToValidate="ddlSchoolInstitute" Display="None" ErrorMessage="Please Select College"
                                                                InitialValue="0" ValidationGroup="show">
                                                            </asp:RequiredFieldValidator>
                                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ControlToValidate="ddlSchoolInstitute" Display="None" ErrorMessage="Please Select College"
                                                InitialValue="0" ValidationGroup="showweekly">
                                            </asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" data-select2-enable="true" CssClass="form-control" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="rfvSem" runat="server" SetFocusOnError="true" ControlToValidate="ddlSem" Display="None" ErrorMessage="Please Select Semester"
                                                                InitialValue="0" ValidationGroup="show">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>From Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <div class="input-group-addon" id="imgCalDateOfBirth1" runat="server">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="7" CssClass="form-control pull-right"
                                                                    placeholder="From Date" ToolTip="Please Select From Date" />

                                                                <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtFromDate" PopupButtonID="imgCalDateOfBirth1" Enabled="True" OnClientDateSelectionChanged="checkDate">
                                                                </ajaxToolKit:CalendarExtender>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFromDate"
                                                                    Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="show"></asp:RequiredFieldValidator>

                                                            </div>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>To Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <div class="input-group-addon" id="imgCalDateOfBirth2" runat="server">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>

                                                                <asp:TextBox ID="txtTodate" runat="server" TabIndex="8" ValidationGroup="submit" placeholder="To Date"
                                                                    ToolTip="Please Select To Date" CssClass="form-control pull-right" />

                                                                <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtTodate" PopupButtonID="imgCalDateOfBirth2" Enabled="True" OnClientDateSelectionChanged="checkDate">
                                                                </ajaxToolKit:CalendarExtender>

                                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtTodate"
                                                                    Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="show"></asp:RequiredFieldValidator>

                                                            </div>

                                                        </div>
                                                        <%--   <div class="form-group col-lg-9 col-md-12 col-12">
                                                            <div class=" note-div">
                                                                <h5 class="heading">Note </h5>
                                                                <p><i class="fa fa-star" aria-hidden="true"></i><span><span style="color: green; font-weight: bold">Send Email For Absent Student Weekly Basis Button </span>Required Only Session, From Date and To Date Selection</span></p>

                                                            </div>
                                                        </div>--%>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" CssClass="btn btn-info" Text="Show" ValidationGroup="show" />
                                                    <asp:Button ID="btnSendEmailtostudent" runat="server" OnClick="btnSendEmailtostudent_Click" CssClass="btn btn-info" Text="Send Email To Students & Parents" ValidationGroup="show" />

                                                    <%--<asp:Button ID="btnSendEmailtoparent" runat="server" OnClick="btnSendEmailtoparent_Click" CssClass="btn btn-info" Text="Send Email To Parent" ValidationGroup="show"/>--%>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" class="btn btn-warning" TabIndex="1" OnClick="btnCancel_Click" />


                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />

                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlAttStatus" runat="server">
                                                        <asp:ListView ID="lvAttStatus" runat="server" OnPagePropertiesChanging="lvAttStatus_PagePropertiesChanging">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Faculty Attendance List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>
                                                                                <asp:CheckBox ID="chkHead" runat="server" onclick="totAllSubjects(this)" /></th>
                                                                            <th>Registration No.</th>
                                                                            <th>Student Name</th>
                                                                            <th>Semester</th>
                                                                            <th>Student Email Id</th>
                                                                            <th>Father Email Id</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                                <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                                    <div class="float-right">
                                                                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvAttStatus" PageSize="100">
                                                                            <Fields>
                                                                                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                                    ShowNextPageButton="false" />
                                                                                <asp:NumericPagerField ButtonType="Link" />
                                                                                <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                                            </Fields>
                                                                        </asp:DataPager>
                                                                    </div>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr id="trCurRow">
                                                                    <td>
                                                                        <asp:CheckBox ID="chkRows" runat="server" ToolTip='<%#Eval("REGNO") %>' /></td>
                                                                    <td><%#Eval("REGNO") %></td>
                                                                    <td>
                                                                        <asp:Label ID="lblStudName" runat="server" Text='<%#Eval("[STUDENT NAME]") %>'></asp:Label></td>
                                                                    <td><%#Eval("SEMESTERNO") %></td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmailStud" runat="server" Text='<%#Eval("EMAILID") %>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmailparent" runat="server" Text='<%#Eval("FATHER_EMAIL") %>'></asp:Label>
                                                                        <asp:Label ID="lblMotherEmail" runat="server" Text='<%#Eval("MOTHER_EMAIL") %>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>


                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updAbsentStudent"
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
                                <asp:UpdatePanel ID="updAbsentStudent" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYddlSession_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSessionAbsentStudent" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" SetFocusOnError="true" ControlToValidate="ddlSessionAbsentStudent" Display="None" ErrorMessage="Please Select Session"
                                                                InitialValue="0" ValidationGroup="showweekly">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>From Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <div class="input-group-addon" id="imgCalFromDate" runat="server">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtAbsentFromDate" runat="server" TabIndex="7" CssClass="form-control pull-right"
                                                                    placeholder="From Date" ToolTip="Please Select From Date" />

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtAbsentFromDate" PopupButtonID="imgCalFromDate" Enabled="True" OnClientDateSelectionChanged="checkDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAbsentFromDate"
                                                                    Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="showweekly"></asp:RequiredFieldValidator>

                                                            </div>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>To Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <div class="input-group-addon" id="imgCalToDate" runat="server">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>

                                                                <asp:TextBox ID="txtAbsentToDate" runat="server" TabIndex="8" ValidationGroup="submit" placeholder="To Date"
                                                                    ToolTip="Please Select To Date" CssClass="form-control pull-right" />

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtAbsentToDate" PopupButtonID="imgCalToDate" Enabled="True" OnClientDateSelectionChanged="checkDate">
                                                                </ajaxToolKit:CalendarExtender>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtAbsentToDate" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtAbsentToDate"
                                                                    Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="showweekly"></asp:RequiredFieldValidator>

                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnShowAbsentStudent" runat="server" OnClick="btnShowAbsentStudent_Click" CssClass="btn btn-info" Text="Show" ValidationGroup="showweekly" />
                                                    <asp:Button ID="btnSendEmailForWeekly" runat="server" OnClick="btnSendEmailForWeekly_Click" CssClass="btn btn-info" Text="Send Email For Absent Student Weekly Basis" ValidationGroup="showweekly" />
                                                    <asp:Button ID="btnCancelAbsenStudent" runat="server" Text="Cancel" CausesValidation="false" class="btn btn-warning" TabIndex="1" OnClick="btnCancelAbsenStudent_Click" />
                                                    <asp:Button ID="btnReport" runat="server" Text="Report" CausesValidation="false" class="btn btn-warning" TabIndex="1" OnClick="btnReport_Click" Visible="true" />
                                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="showweekly" />
                                                </div>
                                                <div class="col-12">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:ListView ID="lvAbsentAttedance" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Faculty Attendance List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                    <thead class="bg-light-blue" style="top: -15px !important;">
                                                                        <tr>
                                                                            <th>Sr.No.</th>
                                                                            <th>School</th>
                                                                            <th>Degree</th>
                                                                            <th>Branch</th>
                                                                            <th>Semester</th>
                                                                            <th>Section</th>
                                                                            <th>Registration No.</th>
                                                                            <th>Student Name</th>
                                                                            <th>Total Class</th>
                                                                            <th>Present</th>
                                                                            <th>Absent</th>
                                                                            <th>Present %</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                                <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                                    <div class="float-right">
                                                                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvAbsentAttedance" PageSize="100">
                                                                            <Fields>
                                                                                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                                    ShowNextPageButton="false" />
                                                                                <asp:NumericPagerField ButtonType="Link" />
                                                                                <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                                            </Fields>
                                                                        </asp:DataPager>
                                                                    </div>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr id="trCurRow">
                                                                    <td>
                                                                        <%#: Container.DataItemIndex + 1 %></td>
                                                                    <td><%#Eval("SCHOOL") %></td>
                                                                    <td><%#Eval("DEGREE") %></td>
                                                                    <td><%#Eval("BRANCH") %></td>
                                                                    <td><%#Eval("SEMESTER") %></td>
                                                                    <td><%#Eval("SECTION") %></td>
                                                                    <td><%#Eval("REGNO") %></td>
                                                                    <td><%#Eval("[STUDENT NAME]") %></td>
                                                                    <td><%#Eval("[TOTAL CLASSES]") %></td>
                                                                    <td><%#Eval("PRESENT") %></td>
                                                                    <td><%#Eval("[ABSENT]") %></td>
                                                                    <td><%#Eval("[PRESENT %]") %></td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnReport" />
                                        <asp:PostBackTrigger ControlID="btnSendEmailForWeekly" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function checkDate(sender, args) {
            // I change the < operator to >
            if (sender._selectedDate > new Date()) {
                alert("Unable to select future date !!!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value('')
            }

        }
    </script>
    <script>
        function totAllSubjects(headchk) {
            debugger;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }

        }
    </script>
</asp:Content>

