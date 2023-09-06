<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ExtraClassTT.aspx.cs" Inherits="ACADEMIC_ExtraClassTT" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updDepart" runat="server" AssociatedUpdatePanelID="updTimeTable"
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
    <asp:UpdatePanel ID="updTimeTable" runat="server">

        <ContentTemplate>
            <div class="row">
                <!--academic Calendar-->
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="offered" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme." ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AutoPostBack="true"
                                            TabIndex="1" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Department</label>--%>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control"
                                            AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:ListBox ID="ddlDepartment" runat="server" SelectionMode="Multiple" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                            CssClass="form-control multi-select-demo" AppendDataBoundItems="true" AutoPostBack="true"></asp:ListBox>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select Department." InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree </label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" Visible="false" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Programme/ Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true"
                                            TabIndex="1" AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" Visible="false" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Scheme </label>--%>
                                            <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"
                                            TabIndex="1" AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" Visible="false" ErrorMessage="Please Select Scheme" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Semester  </label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Section</label>--%>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourseType" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCourseType_SelectedIndexChanged"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Section</label>--%>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBatch" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                            Display="None" ErrorMessage="Please Select Batch" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Slot Type</label>--%>
                                            <asp:Label ID="lblDYddlSlotType" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlSlotType" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSlotType_SelectedIndexChanged"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSlotType"
                                            Display="None" ErrorMessage="Please Select Slot Type" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Slot</label>
                                            <%--<asp:Label ID="Slot" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSlot" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSlot"
                                            Display="None" ErrorMessage="Please Select Slot" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>

                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divStartDate" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Time Table Start Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgStartDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" data-inputmask="'alias': 'dd/mm/yyyy'"
                                                TabIndex="1" AutoPostBack="true" data-mask="" Style="z-index: 0" />

                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate" ValidationGroup="submit"
                                                Display="None" ErrorMessage="Please Enter Time Table Start Date">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtStartDate" PopupButtonID="imgStartDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" TargetControlID="txtStartDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" EmptyValueMessage="Please Enter Start Date"
                                                ControlExtender="meeStartDate" ControlToValidate="txtStartDate" IsValidEmpty="true"
                                                InvalidValueMessage="Start Date is Invalid!" Display="None" TooltipMessage="Input a Date"
                                                ErrorMessage="Please Enter Start Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                SetFocusOnError="true" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divEndDate" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Time Table End Date</label>
                                        </div>

                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgEndDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" data-inputmask="'alias': 'dd/mm/yyyy'"
                                                TabIndex="1" AutoPostBack="true" data-mask="" Style="z-index: 0" onchange="return validateDate();" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEndDate"
                                                Display="None" ErrorMessage="Please Enter Time Table End Date" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>

                                            <ajaxToolKit:CalendarExtender ID="ceEndDate" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged='validateDate()'
                                                TargetControlID="txtEndDate" PopupButtonID="imgEndDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" TargetControlID="txtEndDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />

                                            <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" EmptyValueMessage="Please Enter End Date"
                                                ControlExtender="meeEndDate" ControlToValidate="txtEndDate" IsValidEmpty="true"
                                                InvalidValueMessage="End Date is Invalid!" Display="None" TooltipMessage="Input a Date"
                                                ErrorMessage="Please Enter End Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                SetFocusOnError="true" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDateDetails" runat="server" visible="false">
                                        <br />
                                        <p class="text-center" style="border-style: double; font-size: 14px; font-weight: bold; color: #3c8dbc;">
                                            <asp:Label ID="lblTitleDate" runat="server" Text="Selected Session Start & End Date :" TabIndex="14"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="btn-footer">
                            <asp:Button ID="bntSave" TabIndex="1" runat="server" Text="Create Extra Class Time Table" ValidationGroup="submit" CssClass="btn btn-primary" OnClick="bntSave_Click" />
                            <asp:Button ID="btnExisting" TabIndex="1" runat="server" Text="Existing Extra Class Time Table" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnExisting_Click" />
                            <asp:Button ID="btnCancleExtra" TabIndex="1" runat="server" Text="Cancel Extra Class Time Table" CssClass="btn btn-danger" OnClick="btnCancleExtra_Click" />
                            <asp:Button ID="bntCancel" TabIndex="1" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="bntCancel_Click" />
                            <asp:ValidationSummary runat="server" ValidationGroup="submit" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvTimeTable" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Extra Time Table</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <%--Added for cancel extra time table - 02-03-23--%>
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="selectAll(this);" />
                                                    </th>
                                                    <th>Sr No.</th>
                                                    <th>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblDYddlSection_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                    <th>Batch</th>
                                                    <th>
                                                        <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblDYddlSlotType" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th>Slot
                                                    </th>
                                                    <th>Time Table Date</th>
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
                                        <%--Added for cancel extra time table - 02-03-23--%>
                                        <td>
                                            <asp:CheckBox ID="chkTimetable" runat="server" ToolTip='<%# Eval("TTNO") %>' />
                                            <asp:HiddenField ID="hidTtno" runat="server" Value='<%# Eval("TTNO") %>' />
                                        </td>
                                        <td>
                                            <%# Container.DataItemIndex + 1%>
                                        </td>
                                        <td>
                                            <%# Eval("SESSION_NAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SECTIONNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("BATCHNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("COURSE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SLOTTYPE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SLOTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("TIME_TABLE_DATE")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function selectAll(chk) {
            var totalCheckboxes = $('[id*=divsessionlist] td input:checkbox').length;
            for (var i = 0; i < totalCheckboxes; i++) {
                if (chk.checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_lvTimeTable_ctrl" + i + "_chkTimetable").checked = true;
                }
                else
                    document.getElementById("ctl00_ContentPlaceHolder1_lvTimeTable_ctrl" + i + "_chkTimetable").checked = false;
            }
        }
    </script>
    <script>
        function validateDate() {

            var mydate1 = Array();
            var mydate2 = Array();
            mydate1 = $('#ctl00_ContentPlaceHolder1_txtStartDate').val().split('/');
            mydate2 = $('#ctl00_ContentPlaceHolder1_txtEndDate').val().split('/');
            var sdate = new Date(mydate1[2], mydate1[1] - mydate1[2], mydate1[0]);
            var edate = new Date(mydate2[2], mydate2[1] - mydate2[2], mydate2[0]);
            if (sdate == "") {
                alert('Please enter Time Table Start Date.');
                return false;
            }
            else if (sdate > edate) {
                alert('Time Table Start Date should be less than or equal to Time Table End Date');
                $('#ctl00_ContentPlaceHolder1_txtEndDate').val('');
                return false;

            }
            else true;
        }
    </script>
</asp:Content>

