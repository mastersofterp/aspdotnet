<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LeaveAndHolidayEntry.aspx.cs" Inherits="ACADEMIC_MASTERS_LeaveAndHolidayEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #ctl00_ContentPlaceHolder1_btnShow {
            z-index: 0;
        }

        .switch-field {
            font-family: "Lucida Grande", Tahoma, Verdana, sans-serif;
            padding: 10px;
            overflow: hidden;
        }

        .switch-title {
            margin-bottom: 6px;
        }

        .switch-field input {
            position: absolute !important;
            clip: rect(0, 0, 0, 0);
            height: 1px;
            width: 1px;
            border: 0;
            overflow: hidden;
        }

        .switch-field label {
            float: left;
        }

        .switch-field label {
            display: inline-block;
            width: 80px;
            background-color: #e4e4e4;
            color: rgba(0, 0, 0, 0.6);
            font-size: 14px;
            font-weight: normal;
            text-align: center;
            text-shadow: none;
            padding: 6px 14px;
            border: 1px solid rgba(0, 0, 0, 0.2);
            -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.3), 0 1px rgba(255, 255, 255, 0.1);
            box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.3), 0 1px rgba(255, 255, 255, 0.1);
            -webkit-transition: all 0.1s ease-in-out;
            -moz-transition: all 0.1s ease-in-out;
            -ms-transition: all 0.1s ease-in-out;
            -o-transition: all 0.1s ease-in-out;
            transition: all 0.1s ease-in-out;
        }

            .switch-field label:hover {
                cursor: pointer;
            }

        .switch-field input:checked + label {
            background-color: #A5DC86;
            -webkit-box-shadow: none;
            box-shadow: none;
        }

        .switch-field label:first-of-type {
            border-radius: 4px 0 0 4px;
        }

        .switch-field label:last-of-type {
            border-radius: 0 4px 4px 0;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title" style="text-transform: uppercase">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item"><a class="nav-link active" href="#tab_1" data-toggle="tab">Apply Single Student OD</a></li>
                                    <%--  <li><a href="#tab_2" data-toggle="tab">Bulk Student</a></li>--%>
                                    <li class="nav-item"><a class="nav-link" href="#tab_3" data-toggle="tab">OD Approval</a></li>

                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_1">
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

                                        <asp:UpdatePanel ID="updHoliday" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div id="divCourses" runat="server">

                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <%--<label>Univ. Reg. No/Adm. No.</label>--%>
                                                                        <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtRollNo" runat="server" ValidationGroup="Show" CssClass="form-control" TabIndex="1" />

                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label></label>
                                                                    </div>
                                                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                                                        ValidationGroup="Show" CssClass="btn btn-primary" TabIndex="2" />
                                                                    <asp:RequiredFieldValidator ID="rfvregno" runat="server"
                                                                        ControlToValidate="txtRollNo" Display="None"
                                                                        ErrorMessage="Please Enter REGNO" SetFocusOnError="true"
                                                                        ValidationGroup="Submit" />
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <asp:Panel ID="tblInfo" runat="server" Visible="false">

                                                            <div class="row">
                                                                <div class="col-lg-4 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Student Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Mother Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" />
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>
                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblEnrollNo" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>
                                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                            :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblSemester" runat="server" Font-Bold="True" />
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>PH :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblPH" runat="server" Font-Bold="True" />
                                                                            </a>
                                                                        </li>

                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-6 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Father Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" />
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>
                                                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="True" />
                                                                            /
                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="True" />
                                                                            :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblBranch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                        </li>

                                                                        <li class="list-group-item"><b>
                                                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="True" />
                                                                            :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True" />
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>
                                                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="True" />
                                                                            :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblScheme" runat="server" Font-Bold="True" />
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                                <div class="col-lg-2 col-md-6 col-12">
                                                                    <div class="form-group col-md-12">
                                                                        <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px"
                                                                            BorderColor="Black" BorderWidth="2px" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </asp:Panel>

                                                    </div>

                                                    <div class="col-12 mt-4">
                                                        <div class="row" id="SingleStudOD" runat="server">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>

                                                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                                    ValidationGroup="Submit" ErrorMessage="Please Select College."
                                                                    InitialValue="0" SetFocusOnError="true" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>

                                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Session"
                                                                    InitialValue="0" SetFocusOnError="true" />
                                                                <asp:HiddenField ID="hiddenfieldfromDt" runat="server" Visible="False" />
                                                                <asp:HiddenField ID="hiddenFieldToDt" runat="server" Visible="False" />
                                                                <asp:HiddenField ID="hiddenfieldfromDtBulk" runat="server" Visible="False" />
                                                                <asp:HiddenField ID="hiddenFieldToDtBulk" runat="server" Visible="False" />
                                                                <asp:RequiredFieldValidator ID="rfvSessionrpt" runat="server"
                                                                    ControlToValidate="ddlSession" Display="None"
                                                                    ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                                                    ValidationGroup="Report" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>OD Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOdType" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlOdType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="5">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlOdType" runat="server" ControlToValidate="ddlOdType"
                                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select OD Type"
                                                                    InitialValue="0" SetFocusOnError="true" />

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>For Multiple Days Leave</label>
                                                                </div>
                                                                <asp:CheckBox ID="ChkDate" runat="server" AutoPostBack="True" ForeColor="#006600" Checked="true"
                                                                    OnCheckedChanged="ChkDate_CheckedChanged" Text="Check This box if Want to Select From Date and To Date" TabIndex="6" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>OD Date</label>
                                                                </div>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i id="txtFromDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="7" ValidationGroup="Submit"
                                                                        CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" />
                                                                    <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtFromDate" PopupButtonID="txtFromDate1" OnClientDateSelectionChanged="selectfromdate" />

                                                                    <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                        MaskType="Date" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" EmptyValueMessage="Please Enter OD Date"
                                                                        ControlExtender="meFromDate" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                                                        InvalidValueMessage=" Date is invalid" Display="None" ErrorMessage="Please Enter OD Date"
                                                                        InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                                </div>
                                                            </div>


                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div id="tdToDate" runat="server" visible="true">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>To Date </label>
                                                                    </div>
                                                                    <div class="input-group date">
                                                                        <div class="input-group-addon">
                                                                            <i id="txtToDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                                                        </div>
                                                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="8" ValidationGroup="Submit"
                                                                            CssClass="form-control" />

                                                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                                            PopupButtonID="txtToDate1" />
                                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                            MaskType="Date" />
                                                                        <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" EmptyValueMessage="Please Enter To Date"
                                                                            ControlExtender="meToDate" ControlToValidate="txtToDate" IsValidEmpty="false"
                                                                            InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Enter To Date"
                                                                            InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Leave Type </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlLeaveName" runat="server" AppendDataBoundItems="True" ValidationGroup="Submit" data-select2-enable="true" TabIndex="9"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="RFVddlLeaveName" runat="server" ControlToValidate="ddlLeaveName"
                                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Leave Type"
                                                                    InitialValue="0" SetFocusOnError="true" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Reason </label>
                                                                </div>
                                                                <asp:TextBox ID="txtEventDetail" runat="server" CssClass="form-control" Style="resize: none" TabIndex="10" TextMode="MultiLine">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtEventDetail" runat="server"
                                                                    ControlToValidate="txtEventDetail" Display="None"
                                                                    ErrorMessage="Please Enter Reason" SetFocusOnError="true"
                                                                    ValidationGroup="Submit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--  <sup>* </sup>--%>
                                                                    <label>Document Upload</label>
                                                                </div>
                                                                <asp:FileUpload ID="fuDoc" runat="server" Width="220px" />
                                                                <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label id="lblSlot" runat="server" visible="false">Slots (Select All) </label>
                                                                </div>

                                                                <asp:CheckBox ID="chkCheckAll" OnCheckedChanged="chkCheckAll_CheckedChanged" AutoPostBack="true" Visible="false" runat="server" />
                                                                <asp:CheckBoxList ID="chkSlots" CssClass="col-lg-12" RepeatDirection="Horizontal" RepeatColumns="4" runat="server">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <div class="col-lg-3 col-md-6 col-12">
                                                                <label for="city">Report In</label>
                                                                <br />
                                                                <label>
                                                                    <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Selected="True" Value="1">Adobe Reader</asp:ListItem>
                                                                        <asp:ListItem Value="2">MS-Excel</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </label>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ddlSession" />
                                                <asp:PostBackTrigger ControlID="ddlODType" />
                                                <asp:PostBackTrigger ControlID="txtFromDate" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div>

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-12 btn-footer">
                                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List"
                                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />

                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />

                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" Enabled="false"
                                                            CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" Enabled="false"
                                                            OnClick="btnReport_Click" ValidationGroup="Report" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />

                                                        <asp:Button ID="btnDeptwise" runat="server" Text="Branchwise Report" CssClass="btn btn-info" Visible="false"
                                                            OnClick="btnDeptwise_Click" ValidationGroup="Report" />
                                                        <%-- <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="Report" />--%>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnReport" />
                                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="col-12">

                                                <asp:ListView ID="lvExamday" runat="server" OnItemDataBound="lvExamday_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>APPLIED OD LIST </h5>
                                                        </div>
                                                        <table id="tblHead" class="table table-striped table-bordered nowrap display">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Edit
                                                                    </th>
                                                                    <th style="display: none;">Delete
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="True" />
                                                                    </th>
                                                                    <th>Name
                                                                    </th>
                                                                    <th>OD Type
                                                                    </th>
                                                                    <th>OD Name
                                                                    </th>
                                                                    <th>OD Start Date
                                                                    </th>
                                                                    <th>OD End Date
                                                                    </th>
                                                                    <th>Download Document 
                                                                    </th>
                                                                    <th>Status
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                        </tbody>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                    CommandArgument='<%# Eval("HOLIDAY_NO") %>' CommandName='<%# Eval("IDNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                    OnClick="btnEdit_Click" />
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="" CommandArgument='<%# Eval("HOLIDAY_NO") %>'
                                                                    ToolTip='<%# Eval("HOLIDAY_NO") %>' OnClientClick="return ConfirmSubmit();" OnClick="btnDelete_Click" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("REGNO")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("STUDNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("OD_NAME")%>
                                                                <asp:HiddenField ID="hdnODTYPE" runat="server" Value='<%# Eval("ODTYPE")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("ACADEMIC_LEAVE_NAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ACADEMIC_HOLIDAY_STDATE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ACADEMIC_HOLIDAY_ENDDATE")%>
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="updnpfPreview" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtnpfPrevDoc" runat="server" CommandArgument='<%# Eval("FILENAME") %>' ImageUrl="~/images/downarrow.jpg" Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' OnClick="imgbtnpfPrevDoc_Click" />
                                                                        <asp:Label ID="lblnpfPreview" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="imgbtnpfPrevDoc" />

                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAStatus" runat="server" Text='<%# Eval("APPROVAL_STATUS")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                    CommandArgument='<%# Eval("HOLIDAY_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                    OnClick="btnEdit_Click" />
                                                            </td>
                                                            <td style="display: none;">
                                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("HOLIDAY_NO") %>'
                                                                    ToolTip='<%# Eval("HOLIDAY_NO") %>' OnClientClick="return ConfirmSubmit();" OnClick="btnDelete_Click" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("REGNO")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("STUDNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("OD_NAME")%>                                                     
                                                            </td>
                                                            <td>
                                                                <%# Eval("ACADEMIC_LEAVE_NAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ACADEMIC_HOLIDAY_STDATE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ACADEMIC_HOLIDAY_ENDDATE")%>
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="updnpfPreview1" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtnpfPrevDoc1" runat="server" CommandArgument='<%# Eval("FILENAME") %>' ImageUrl="~/images/downarrow.jpg" Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' OnClick="imgbtnpfPrevDoc_Click" />
                                                                        <asp:Label ID="lblnpfPreview1" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <%--  <asp:AsyncPostBackTrigger ControlID="imgbtnpfPrevDoc1" EventName="Click" />--%>
                                                                        <asp:PostBackTrigger ControlID="imgbtnpfPrevDoc1" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAStatus" runat="server" Text='<%# Eval("APPROVAL_STATUS")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:ListView>

                                            </div>

                                        </div>
                                    </div>
                                   

                                    <div class="tab-pane" id="tab_3">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server"
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
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <asp:ListView ID="lvLeaveApproval" runat="server" OnItemDataBound="lvLeaveApproval_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>APPLIED OD LIST FOR APPROVAL</h5>
                                                            </div>

                                                            <small>Note : Please click on Status for details
                                                                <strong>RegNo.(Red) = Approval Limit Exceeded !</strong>
                                                            </small>
                                                            <table class="table table-striped table-bordered nowrap display" style="table-layout: fixed">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="True" />
                                                                        </th>
                                                                        <th>Name
                                                                        </th>
                                                                        <th>OD Type
                                                                        </th>
                                                                        <th>OD Name
                                                                        </th>
                                                                        <th>OD Start Date
                                                                        </th>
                                                                        <th>OD End Date
                                                                        </th>
                                                                        <td>Download Document
                                                                        </td>
                                                                        <th>Status
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
                                                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' Font-Size="11pt" />
                                                                </td>
                                                                <td style="white-space: normal;">
                                                                    <%# Eval("STUDNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("OD_NAME")%>
                                                                    <asp:HiddenField ID="hdnODTYPE" runat="server" Value='<%# Eval("ODTYPE")%>' />
                                                                    <asp:HiddenField ID="OdCount" runat="server" Value='<%# Eval("OD_COUNT")%>' />
                                                                </td>
                                                               <td style="white-space: normal;">
                                                                    <%# Eval("ACADEMIC_LEAVE_NAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ACADEMIC_HOLIDAY_STDATE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ACADEMIC_HOLIDAY_ENDDATE")%>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="updnpfPreview2" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:ImageButton ID="imgbtnpfPrevDoc2" runat="server" CommandArgument='<%# Eval("FILENAME") %>' ImageUrl="~/images/downarrow.jpg" Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' OnClick="imgbtnpfPrevDoc2_Click" />
                                                                            <asp:Label ID="lblnpfPreview2" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="imgbtnpfPrevDoc2" />
                                                                            <%--<asp:AsyncPostBackTrigger ControlID="imgbtnpfPrevDoc2" EventName="Click" />--%>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td title="View Details">
                                                                    <asp:UpdatePanel ID="updShow" runat="server">
                                                                        <ContentTemplate>
                                                                            <%--<a href="#" id="myBtn" data-target="#myModal1" data-toggle="modal" style="cursor: pointer; font-weight: bold;">
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("APPROVAL_STATUS")%>'></asp:Label></a>--%>
                                                                            <asp:LinkButton runat="server" ID="btnShowLeave" OnClick="btnShowLeave_Click" ToolTip='<%#Eval("SESSIONNO") %>' CommandArgument='<%#Eval("HOLIDAY_NO") %>' Style="font-weight: bold;">
                                                                                <asp:Label ID="lblStatus" class="status_app" runat="server" Text='<%# Eval("APPROVAL_STATUS")%>'></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnShowLeave" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>


                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' Font-Size="11pt" />
                                                                </td>
                                                                <td style="white-space: normal;">
                                                                    <%# Eval("STUDNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("OD_NAME")%>
                                                                    <asp:HiddenField ID="hdnODTYPE" runat="server" Value='<%# Eval("ODTYPE")%>' />
                                                                    <asp:HiddenField ID="OdCount" runat="server" Value='<%# Eval("OD_COUNT")%>' />
                                                                </td>
                                                                <td style="white-space: normal;">
                                                                    <%# Eval("ACADEMIC_LEAVE_NAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ACADEMIC_HOLIDAY_STDATE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ACADEMIC_HOLIDAY_ENDDATE")%>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="updnpfPreview3" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:ImageButton ID="imgbtnpfPrevDoc3" runat="server" CommandArgument='<%# Eval("FILENAME") %>' ImageUrl="~/images/downarrow.jpg" Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' OnClick="imgbtnpfPrevDoc2_Click" />
                                                                            <asp:Label ID="lblnpfPreview3" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <%--<asp:AsyncPostBackTrigger ControlID="imgbtnpfPrevDoc3" EventName="Click" />--%>
                                                                            <asp:PostBackTrigger ControlID="imgbtnpfPrevDoc3" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td title="View Details">
                                                                    <%-- <a href="#" id="myBtn" data-target="#myModal1" data-toggle="modal" style="cursor: pointer; font-weight: bold;">
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("APPROVAL_STATUS")%>'></asp:Label></a>--%>
                                                                    <asp:LinkButton runat="server" ID="btnShowLeave" OnClick="btnShowLeave_Click" ToolTip='<%#Eval("SESSIONNO") %>' CommandArgument='<%#Eval("HOLIDAY_NO") %>' Style="font-weight: bold;">
                                                                        <asp:Label ID="lblStatus" runat="server" class="status_app" Text='<%# Eval("APPROVAL_STATUS")%>'></asp:Label>
                                                                    </asp:LinkButton>

                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                     <div id="divMsg" runat="Server"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="lvExamday" />
            <asp:PostBackTrigger ControlID="lvLeaveApproval" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="modal fade" id="myModal1" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">

                    <h4 class="modal-title">Leave Details <i class="fa fa-info-circle"></i>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
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
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Regno :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRegno" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Leave Type:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblLeaveType" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Leave Status :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="leaveStatus" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudname" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Leave From Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblfromDate" runat="server" Font-Bold="True" />
                                            </a>

                                        </li>
                                        <li class="list-group-item"><b>Leave Details :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblLeaveDetail" runat="server" Text=""></asp:Label>
                                                <asp:HiddenField runat="server" ID="hdnLeaveNo" Value="0" />
                                                <asp:HiddenField runat="server" ID="hdnSessionNo" Value="0" />
                                                <asp:HiddenField runat="server" ID="hdnODType" Value="0" Visible="false" />
                                            </a>
                                        </li>

                                    </ul>
                                </div>

                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>OD Type :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblODType" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Leave to Date:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblToDate" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Slots:</b>
                                            <a class="sub-label">
                                                <asp:CheckBoxList ID="chkSelectdSlots" AutoPostBack="true" OnSelectedIndexChanged="chkSelectdSlots_SelectedIndexChanged" CssClass="col-lg-12" RepeatDirection="Horizontal" RepeatColumns="3" runat="server">
                                                </asp:CheckBoxList>
                                            </a>
                                        </li>

                                    </ul>
                                </div>


                                <div class="col-12">
                                    <div class="switch-field">

                                        <div class="switch-title">Mark Leave as :</div>
                                        <input type="radio" id="switch_left" name="Status" value="1" />
                                        <label for="switch_left">Approve</label>
                                        <input type="radio" id="switch_right" name="Status" value="2" />
                                        <label for="switch_right">Reject</label>
                                    </div>
                                    <asp:Label ID="ODCountFlag" runat="server" Visible="false" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmitStatus" runat="server" Text="Submit" OnClick="btnSubmitStatus_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-warning"
                                    data-dismiss="modal" />

                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <div class="modal fade" id="myModal2" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="box-body modal-warning">
                            <div class="form-group" style="text-align: center">
                                <asp:Label ID="lblmessageShow" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-warning"
                                        data-dismiss="modal" />
                                </p>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopup"
        TargetControlID="lnkPreview" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
    <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FILENAME") %>'></asp:LinkButton>

    <!-- The Modal -->
    <div class="modal fade" id="PassModel">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
                            <div class="modal-header">
                                <h4 class="modal-title">Document</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>

                            <!-- Modal body -->
                            <div class="modal-body">
                                <div class="col-12">
                                    <iframe runat="server" width="420px" height="500px" id="iframeView"></iframe>
                                </div>
                            </div>

                            <!-- Modal footer -->
                            <div class="modal-footer d-none">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger no" />
                            </div>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>--%>




    <script type="text/javascript">
        function selectfromdate(sender, args) {
        }
        function ConfirmSubmit() {
            var ret = confirm('Are you sure to delete this Leave entry?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function SelectAll(chk) {
            for (i = 0; i < hftot.value; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_chkSelect');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }
        function SetCheckBox(value) {
            $("input:radio[value=" + value + "]").attr("checked", true);
        }

    </script>

    <script>
        function OpenPreview() {
            //$("#pnlPopup1").hide();
            //$('#pnlPopup1').modal('toggle');
            //alert('hi');
            $('#myModal1').modal('show');
        }
    </script>

</asp:Content>
