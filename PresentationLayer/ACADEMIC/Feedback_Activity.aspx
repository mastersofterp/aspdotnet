<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Feedback_Activity.aspx.cs" Inherits="ACADEMIC_Feedback_Activity" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<script runat="server">

</script>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />

    <style>
        #ctl00_ContentPlaceHolder1_pnlCourse .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_lvFeedbackType_DataPager1 a:first-child,
        #ctl00_ContentPlaceHolder1_lvFeedbackType_DataPager1 a:last-child {
            padding: 5px 10px;
            border-radius: 0%;
            background: white;
            margin: 0 0px;
            box-shadow: none;
        }

        #ctl00_ContentPlaceHolder1_lvFeedbackType_DataPager1 a {
            padding: 5px 10px;
            border-radius: 50%;
            background: white;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }

        #ctl00_ContentPlaceHolder1_lvFeedbackType_DataPager1 span {
            padding: 5px 10px;
            border-radius: 50%;
            background: #4183c4;
            color: #fff;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }
    </style>



    <%--<div style="z-index: 1; position:absolute; top: 50px; left: 600px;">--%>
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
    <%--</div>--%>

    <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#example').DataTable({
                "bDestroy": true,
            });
            $('#myDatepicker1').datetimepicker({
                format: 'DD/MM/YYYY'
            });
            $('#myDatepicker2').datetimepicker({
                format: 'DD/MM/YYYY'
            });
        }

    </script>

    <asp:UpdatePanel ID="updSesActivity" runat="server">
        <ContentTemplate>
            <div class="panel-group">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div3" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session : </label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" ToolTip="Please Select Session" runat="server" TabIndex="1" data-select2-enable="true"
                                                CssClass="form-control" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="valSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please select session" SetFocusOnError="true" ValidationGroup="submit"
                                                InitialValue="0" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                                <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlClgname" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged"
                                                ValidationGroup="offered" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:ListBox ID="ddlClgname" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>--%>
                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree </label>
                                            </div>
                                            <asp:ListBox ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo" SelectionMode="multiple" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>

                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" ValidationGroup="teacherassign">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Branch </label>
                                            </div>
                                            <asp:ListBox ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                                CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>

                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Branch" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Semester : </label>
                                            </div>
                                            <asp:ListBox ID="ddlSemester" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control  multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" SetFocusOnError="true" ValidationGroup="submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Feedback Type : </label>
                                            </div>
                                            <asp:DropDownList ID="ddlFeedbackType" AppendDataBoundItems="true" ToolTip="Please Select Feedback Type" runat="server" TabIndex="6" data-select2-enable="true"
                                                CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFeedbackType" runat="server" ControlToValidate="ddlFeedbackType"
                                                Display="None" ErrorMessage="Please select Feedback Type" SetFocusOnError="true"
                                                ValidationGroup="submit" InitialValue="0" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Activity Status : </label>
                                            </div>

                                            <asp:RadioButton ID="rdoStart" runat="server" Text="Started" GroupName="act_status"
                                                TabIndex="9" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoStop" runat="server" Text="Stopped" Checked="true" GroupName="act_status"
                                            TabIndex="9" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Show Status : </label>
                                            </div>

                                            <asp:RadioButton ID="rdoYes" runat="server" Text="Yes" GroupName="sh_status" Checked="true" TabIndex="10" />&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdoNo" runat="server" Text="No" GroupName="sh_status" TabIndex="10" />

                                        </div>
                                        <div class="form-group col-lg-2 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Date & Time Selection</label>
                                            </div>
                                            <div class="form-group col-lg-12 col-md-6 col-12">
                                                <asp:RadioButtonList ID="rbddatetime" runat="server" RepeatDirection="VERTICAL" AutoPostBack="true" OnSelectedIndexChanged="rbddatetime_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Date Wise</asp:ListItem>
                                                    <asp:ListItem Value="2">Date & Time Wise</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                        </div>
                                        <asp:Panel ID="PnlDate" runat="server">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Start Date : </label>
                                                        </div>
                                                        <div class='input-group date' id="myDatepicker">
                                                            <asp:TextBox ID="txtStartDate" runat="server" autocomplete="off" ToolTip="Please Enter Start Date" CssClass="form-control datepickerinput" TabIndex="7" data-inputmask="'mask': '99/99/9999'" />


                                                            <div class="input-group-addon" id="Div1" runat="server">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStartDate"
                                            Display="None" ErrorMessage="Please select Start Date" SetFocusOnError="true"
                                            ValidationGroup="submit" InitialValue="0" />--%>
                                                            <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Div1" TargetControlID="txtStartDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2"
                                                                runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left"
                                                                ErrorTooltipEnabled="true"
                                                                Mask="99/99/9999"
                                                                MaskType="Date"
                                                                MessageValidatorTip="true"
                                                                OnInvalidCssClass="errordate"
                                                                TargetControlID="txtStartDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                                ControlExtender="MaskedEditExtender2"
                                                                ControlToValidate="txtStartDate"
                                                                IsValidEmpty="False"
                                                                EmptyValueMessage="Please Enter Start Date"
                                                                InvalidValueMessage="Start Date is invalid(Enter dd/mm/yyyy format)"
                                                                Display="None"
                                                                TooltipMessage="Input a date"
                                                                EmptyValueBlurredText="*"
                                                                InvalidValueBlurredMessage="*"
                                                                ValidationGroup="submit" />

                                                            <%-- <ajaxToolKit:FilteredTextBoxExtender ID="stfilter" TargetControlID="txtStartDate" runat="server" FilterType="Custom,Numbers" ValidChars="0123456789 /"  ></ajaxToolKit:FilteredTextBoxExtender>--%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>End Date : </label>
                                                        </div>
                                                        <div class="input-group date" id="myDatepicker3">
                                                            <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" TabIndex="8" ToolTip="Please Enter End Date" CssClass="form-control datepickerinput" data-inputmask="'mask': '99/99/9999'" />
                                                            <div class="input-group-addon" id="Div2" runat="server">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Div2" TargetControlID="txtEndDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1"
                                                                runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left"
                                                                ErrorTooltipEnabled="true"
                                                                Mask="99/99/9999"
                                                                MaskType="Date"
                                                                MessageValidatorTip="true"
                                                                OnInvalidCssClass="errordate"
                                                                TargetControlID="txtEndDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                                ControlExtender="MaskedEditExtender2"
                                                                ControlToValidate="txtEndDate"
                                                                IsValidEmpty="False"
                                                                EmptyValueMessage="Please Enter End Date"
                                                                InvalidValueMessage="End Date is invalid(Enter dd/mm/yyyy format)"
                                                                Display="None"
                                                                TooltipMessage="Input a date"
                                                                EmptyValueBlurredText="*"
                                                                InvalidValueBlurredMessage="*"
                                                                ValidationGroup="submit" />
                                                            <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtEndDate" runat="server" FilterType="Custom,Numbers" ValidChars="0123456789 /"  ></ajaxToolKit:FilteredTextBoxExtender>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:Panel>
                                        <asp:Panel ID="PnlDateTime" runat="server">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <label><span style="color: red"></span>Start Time</label>

                                                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" ToolTip="Please Enter Time" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                                        <ajaxToolKit:MaskedEditExtender ID="meStarttime" runat="server" TargetControlID="txtStartTime"
                                                            Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                            MaskType="Time" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid Start Time"
                                                            ControlToValidate="txtStartTime" Display="NONE" SetFocusOnError="true" ValidationGroup="submit"
                                                            ValidationExpression="((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>
                                                        <%--<asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Start Time."></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <label><span style="color: red"></span>End Time</label>

                                                        <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control" ToolTip="Please Enter Time" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                                        <ajaxToolKit:MaskedEditExtender ID="meEndtime" runat="server" TargetControlID="txtEndTime"
                                                            Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                            MaskType="Time" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a valid End Time"
                                                            ControlToValidate="txtEndTime" Display="NONE" SetFocusOnError="true" ValidationGroup="submit"
                                                            ValidationExpression="((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndTime"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter End Time."></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="text-center col-sm-12">

                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" CssClass="btn btn-primary"
                                                OnClick="btnSubmit_Click" TabIndex="11" />&nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="12" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlCourse" runat="server">
                                    <asp:ListView ID="lvFeedbackType" runat="server" OnPagePropertiesChanging="lvFeedbackType_PagePropertiesChanging">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Feedback Activity List</h5>
                                            </div>
                                            <div class="col-lg-3 col-md-6">
                                                <div class="input-group sea-rch">
                                                    <%--<input type="text" id="FilterData2" class="form-control" placeholder="Search" />--%>
                                                    <asp:TextBox ID="FilterData2" runat="server" TabIndex="1" CssClass="form-control" MaxLength="20" placeholder="Search" AutoPostBack="true" OnTextChanged="FilterData2_TextChanged"></asp:TextBox>

                                                    <div class="input-group-addon">
                                                        <i class="fa fa-search"></i>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="table-responsive" style="max-height: 520px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable2">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; background: #fff!important; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th style="width: 4%">Edit
                                                            </th>
                                                            <th style="width: 8%" align="left">Feedback Type
                                                            </th>
                                                            <th style="width: 8%" align="left">Session
                                                            </th>
                                                            <th style="width: 8%" align="left">Start Date
                                                            </th>
                                                            <th style="width: 8%" align="left">End Date
                                                            </th>
                                                            <th style="width: 8%" align="left">Start Time
                                                            </th>
                                                            <th style="width: 8%" align="left">End Time
                                                            </th>
                                                            <th style="width: 8%" align="left">Activity Status
                                                            </th>
                                                            <th style="width: 8%" align="left">Show Status
                                                            </th>
                                                            <th style="width: 8%" align="left">College
                                                            </th>
                                                            <th style="width: 8%" align="left">Degree
                                                            </th>
                                                            <th style="width: 8%" align="left">Branch
                                                            </th>
                                                            <th style="width: 8%" align="left">Semester
                                                            </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                <div class="float-right">
                                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvFeedbackType" PageSize="50">
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
                                            <tr class="item">
                                                <td style="width: 4%">
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>' AlternateText="Edit Record"
                                                        ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="10" />
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("FEEDBACK_NAME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("SESSION_NAME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("START_DATE") %>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("END_DATE") %>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("START_TIME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("END_TIME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("STATUS")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("SHOWSTATUS")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("BRANCH")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="altitem">
                                                <td style="width: 4%">
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>' AlternateText="Edit Record"
                                                        ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="10" />
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("FEEDBACK_NAME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("SESSION_NAME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# (Eval("START_DATE").ToString() != string.Empty) ? (Eval("START_DATE","{0:dd-MMM-yyyy}")) : Eval("START_DATE" ,"{0:dd-MMM-yyyy}") %>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# (Eval("END_DATE").ToString() != string.Empty) ? (Eval("END_DATE", "{0:dd-MMM-yyyy}")) : Eval("END_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("START_TIME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("END_TIME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("STATUS")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("SHOWSTATUS")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("BRANCH")%>
                                                </td>
                                                <td style="width: 8%" align="left">
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>

                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script>
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>

    <script type="text/javascript">
        function isNumber(evt) {

            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode >= 65 && iKeyCode <= 122)//&& (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
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
                });
            });
        });
    </script>
</asp:Content>


