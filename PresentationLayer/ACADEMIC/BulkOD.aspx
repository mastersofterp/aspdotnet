<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkOD.aspx.cs" Inherits="ACADEMIC_BulkOD" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <style>
        #tblHead_length, #tblHead_info , #tblHead_paginate {
            display:none;
        }
    </style>--%>
    <%--    <style>
        #ctl00_ContentPlaceHolder1_updBulkReg .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>--%>
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <%--        <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .table {
            width: auto !important;
        }
    </style>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>
                <div class="nav-tabs-custom col-12">
                    <ul class="nav nav-tabs mt-2" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#tabLC" tabindex="1">Bulk OD Apply and Approve</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tabsingle" tabindex="1">Single Student OD Apply and Approve</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tabBC" tabindex="1">OD Report</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tabODCancel" tabindex="1">OD Cancel</a>
                        </li>
                    </ul>

                    <div class="tab-content" id="my-tab-content">
                        <div class="tab-pane active" id="tabLC">

                            <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBulkReg"
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
                            <div class="box-body">
                                <asp:UpdatePanel ID="updBulkReg" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>

                                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="1"
                                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCollege"
                                                        ValidationGroup="Submit" ErrorMessage="Please Select College."
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:HiddenField ID="hiddenfieldfromDt" runat="server" Visible="False" />
                                                    <asp:HiddenField ID="hiddenFieldToDt" runat="server" Visible="False" />
                                                    <asp:HiddenField ID="hiddenfieldfromDtBulk" runat="server" Visible="False" />
                                                    <asp:HiddenField ID="hiddenFieldToDtBulk" runat="server" Visible="False" />

                                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit">
                                                    </asp:RequiredFieldValidator>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDegree" runat="server" Font-Bold="true">Degree</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="3" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            InitialValue="0" Display="None" ValidationGroup="show" ErrorMessage="Please Select Program/Branch"></asp:RequiredFieldValidator>--%>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Program/Branch"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlScheme" runat="server" TabIndex="4" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            InitialValue="0" Display="None" ValidationGroup="show" ErrorMessage="Please Select Scheme"></asp:RequiredFieldValidator>--%>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlScheme"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Scheme"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSection" TabIndex="1" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvsection" runat="server" ControlToValidate="ddlSection"
                                                        Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSection"
                                                        Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>OD Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlOdType" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        TabIndex="1" OnSelectedIndexChanged="ddlOdType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlOdType" runat="server" ControlToValidate="ddlOdType"
                                                        ValidationGroup="submit" Display="None" ErrorMessage="Please Select OD Type"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlOdType"
                                                        InitialValue="0" Display="None" ValidationGroup="show" ErrorMessage="Please Select OD Type"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>OD Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="txtFromDate1" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" ValidationGroup="Submit"
                                                            CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" />
                                                        <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                                            PopupButtonID="txtFromDate1" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter To Date"
                                                            ControlExtender="meFromDate" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                                            InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Enter From Date"
                                                            InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="tdToDate" runat="server" visible="true">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="txtToDate1" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="1" ValidationGroup="Submit"
                                                            CssClass="form-control" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged" />

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
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Leave Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlLeaveName" runat="server" AppendDataBoundItems="True" ValidationGroup="Submit"
                                                        TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="RFVddlLeaveName" runat="server" ControlToValidate="ddlLeaveName"
                                                        ValidationGroup="submit" Display="None" ErrorMessage="Please Select Leave Type"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Reason </label>
                                                    </div>
                                                    <asp:TextBox ID="txtEventDetail" runat="server" CssClass="form-control" Style="resize: none" TabIndex="1" TextMode="MultiLine">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtEventDetail" runat="server"
                                                        ControlToValidate="txtEventDetail" Display="None"
                                                        ErrorMessage="Please Enter Reason" SetFocusOnError="true"
                                                        ValidationGroup="submit" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%--<sup>* </sup>--%>
                                                        <asp:Label ID="lblSlotCompulsary" runat="server" Text="*" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblSlots1" runat="server" Text="Slots (Select All" Visible="false"></asp:Label>
                                                    </div>
                                                    <asp:CheckBox ID="chkCheckAll" OnCheckedChanged="chkCheckAll_CheckedChanged" AutoPostBack="true" Visible="false" runat="server" />
                                                    <asp:Label ID="lblSlots2" runat="server" Text=")" Visible="false"></asp:Label>
                                                    </label>
                                <asp:CheckBoxList ID="chkSlots" CssClass="col-lg-12" RepeatDirection="Horizontal" RepeatColumns="4" runat="server" TabIndex="1">
                                </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <%--  <div class="form-group col-md-12">
                                <p class="text-center">
                                   <div class="col-md-3">
                                        <label>Sort By:</label>&nbsp;&nbsp;&nbsp;
                                         <asp:RadioButtonList runat="server" ID="rbSortBy" RepeatDirection="Horizontal" TabIndex="9" AutoPostBack="true">
                                             <asp:ListItem Value="1" Selected="True" Text="&nbsp;Adm. No&nbsp;&nbsp;"></asp:ListItem>
                                             <asp:ListItem Value="0" Text="&nbsp;Univ. Reg. No.&nbsp;&nbsp;"></asp:ListItem>
                                         </asp:RadioButtonList>
                                    </div>

                                </p>
                            </div>--%>
                                        <%if (Session["usertype"].ToString() == "3")
                                          {%>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-5 col-md-12 col-12">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note</h5>
                                                        <p runat="server" id="spanNote"><i class="fa fa-star" aria-hidden="true"></i><span>You Cannot Approve any applied OD Leave more than <b>63 Times</b></span></p>
                                                        <p runat="server" id="P1"><i class="fa fa-star" aria-hidden="true"></i><span><span style="COLOR: red; FONT-WEIGHT: BOLD">Univ. Reg. No.</span> Display in <span style="COLOR: red; FONT-WEIGHT: BOLD">Red Colour</span> if OD Leave Approved <b>63 Times</b></span></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <%}%>
                                        <div class="col-12 btn-footer">
                                            <%--  <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="10"
                                    Text="Show Student" ValidationGroup="show" class="btn btn-primary"><i class=" fa fa-eye"></i> Show</asp:LinkButton>--%>

                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                                TabIndex="1" Text="OD Apply" Enabled="false" ValidationGroup="submit" CssClass="btn btn-primary"> OD Apply</asp:LinkButton>

                                            <asp:LinkButton ID="btnODApprove" runat="server" OnClick="btnODApprove_Click" OnClientClick="return UserApproveConfirmation();"
                                                TabIndex="1" Text="OD Approve" Visible="false" ValidationGroup="show" CssClass="btn btn-success"> OD Approve</asp:LinkButton>

                                            <asp:LinkButton ID="btnODReject" runat="server" OnClick="btnODReject_Click" OnClientClick="return UserRejectConfirmation();"
                                                TabIndex="1" Text="OD Reject" Visible="false" ValidationGroup="show" CssClass="btn btn-danger"> OD Reject</asp:LinkButton>

                                            <asp:LinkButton ID="btnODCancel" runat="server" OnClick="btnODCancel_Click" OnClientClick="return UserCancelConfirmation();"
                                                TabIndex="1" Text="OD Cancel" Visible="false" ValidationGroup="show" CssClass="btn btn-danger" Style="display: none"> OD Cancel</asp:LinkButton>

                                            <asp:Button ID="btnCancel" runat="server" TabIndex="1" CausesValidation="False" Text="Cancel"
                                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                ValidationGroup="submit" ShowSummary="false" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                ValidationGroup="show" ShowSummary="false" />
                                        </div>
                                        <div class="col-12">
                                            <%--<div class="col-md-6">--%>
                                            <%--    <asp:Panel ID="pnlStudents" runat="server" Visible="true" ScrollBars="Auto" Height="400px">--%>
                                            <%--  <div id="listViewGrid">--%>
                                            <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Students List</h5>
                                                    </div>
                                                    <table id="tblHead" class="table table-striped table-bordered nowrap display">
                                                        <thead>
                                                            <tr class="bg-light-blue" id="trRow">
                                                                <th>
                                                                    <asp:CheckBox ID="chkHeader" TabIndex="1" runat="server" onclick="return totAllStudents(this)" ToolTip="Select/Select all" />
                                                                </th>
                                                                <th style="text-transform: uppercase">
                                                                    <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label></th>
                                                                <%--  <th>Adm. No.
                                                            </th>--%>
                                                                <th>STUDENT NAME</th>
                                                                <th>APPLY DATE</th>
                                                                <th>START DATE</th>
                                                                <th>END DATE</th>
                                                                <th>LEAVE TYPE</th>
                                                                <th>Download Document</th>
                                                                <th>STATUS</th>
                                                                <%-- <th>Total Approved Count</th>--%>
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
                                                            <asp:CheckBox ID="cbRow" runat="server" TabIndex="12" ToolTip='<%# Eval("IDNO") %>' /><%--onClick="totStudents(this);"--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("REGNO") %>' />
                                                        </td>
                                                        <%-- <td>
                                                    <asp:Label ID="lblUSNno" runat="server" Text='<%# Eval("ENROLLNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                </td>--%>
                                                        <td>
                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                                                Visible="false" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("APPLY_DATE") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("STARTDATE") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("ENDDATE") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("LEAVETYPE") %>' ToolTip='<%# Eval("IDNO") %>' />
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
                                                        <%-- <td>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("SLOTNAME") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                </td>--%>
                                                        <td>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                            <%--<%# Eval("COUNTIDNO") %>--%>
                                                            <asp:HiddenField ID="hfHolidayno" runat="server" Value='<%# Eval("HOLIDAY_NO") %>' />
                                                            <asp:HiddenField ID="hfCountOfIDNO" runat="server" Value='<%# Eval("COUNTIDNO") %>' />
                                                            <asp:HiddenField ID="hfODTYPE" runat="server" Value='<%# Eval("ODTYPE") %>' />
                                                            <asp:HiddenField ID="hfSelectedSlotCnt" runat="server" Value='<%# Eval("TOTALSLOTCOUNT") %>' />
                                                        </td>
                                                        <%-- <td>
                                                    <asp:Label ID="lblTotalApprovedStudentCount" runat="server" Text='<%# Eval("COUNTIDNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                            <%-- </div>--%>
                                            <%--    </asp:Panel>--%>
                                            <%--  </div>--%>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="txtFromDate" />
                                        <asp:PostBackTrigger ControlID="btnSubmit" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="tab-pane" id="tabsingle">

                            <div>
                                <asp:UpdateProgress ID="updProg2" runat="server" AssociatedUpdatePanelID="updSingle"
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
                            <div class="box-body">
                                <asp:UpdatePanel ID="updSingle" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtRollNo" runat="server" ValidationGroup="Submitshow" CssClass="form-control" TabIndex="1" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:Button ID="btnShow" runat="server" Text="Show"
                                                        ValidationGroup="Submitshow" CssClass="btn btn-primary" TabIndex="2" OnClick="btnShow_Click" />
                                                    <asp:RequiredFieldValidator ID="rfvregno" runat="server" ControlToValidate="txtRollNo" Display="None"
                                                        ErrorMessage="Please Enter REGNO" SetFocusOnError="true" ValidationGroup="Submitshow" />
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
                                                                <asp:Label ID="lblSem" runat="server" Font-Bold="true">Semester</asp:Label>
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
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>

                                                    <asp:DropDownList ID="ddlCollegesingle" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlCollegesingle_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlCollegesingle"
                                                        ValidationGroup="submitsingle" ErrorMessage="Please Select College."
                                                        InitialValue="0" SetFocusOnError="true" />--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlSessionsingle" runat="server" Font-Bold="true" Text="Session"></asp:Label>
                                                    </div>

                                                    <asp:DropDownList ID="ddlSessionsingle" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlSessionsingle_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlSessionsingle"
                                                        ValidationGroup="submitsingle" Display="None" ErrorMessage="Please Select Session"
                                                        InitialValue="0" SetFocusOnError="true" />--%>
                                                    <asp:HiddenField ID="hiddenfieldfromDtsingle" runat="server" Visible="False" />
                                                    <asp:HiddenField ID="hiddenField6" runat="server" Visible="False" />
                                                    <asp:HiddenField ID="hiddenfield7" runat="server" Visible="False" />
                                                    <asp:HiddenField ID="hiddenField8" runat="server" Visible="False" />
                                                    <%-- <asp:RequiredFieldValidator ID="rfvSessionrpt" runat="server"
                                                        ControlToValidate="ddlSession" Display="None"
                                                        ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                                        ValidationGroup="Report" />--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>OD Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlOdTypesingle" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlOdTypesingle_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="5">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlOdTypesingle"
                                                        ValidationGroup="submitsingle" Display="None" ErrorMessage="Please Select OD Type"
                                                        InitialValue="0" SetFocusOnError="true" />--%>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>OD Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="txtFromDate1single" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDatesingle" runat="server" TabIndex="6" ValidationGroup="Submit"
                                                            CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFromDatesingle_TextChanged" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDatesingle"
                                                            PopupButtonID="txtFromDate1single" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtFromDatesingle"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" EmptyValueMessage="Please Enter OD Date"
                                                            ControlExtender="meFromDate" ControlToValidate="txtFromDatesingle" IsValidEmpty="false"
                                                            InvalidValueMessage=" Date is invalid" Display="None" ErrorMessage="Please Enter OD Date"
                                                            InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                    </div>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div id="tdToDatesingle" runat="server" visible="true">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>To Date </label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="txtToDate1single" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtToDatesingle" runat="server" TabIndex="7" ValidationGroup="Submit"
                                                                CssClass="form-control" />

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDatesingle"
                                                                PopupButtonID="txtToDate1single" />
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtToDatesingle"
                                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                MaskType="Date" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" EmptyValueMessage="Please Enter To Date"
                                                                ControlExtender="meToDate" ControlToValidate="txtToDatesingle" IsValidEmpty="false"
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
                                                    <asp:DropDownList ID="ddlLeaveNamesingle" runat="server" AppendDataBoundItems="True" ValidationGroup="Submit" data-select2-enable="true" TabIndex="8"
                                                        CssClass="form-control">
                                                    </asp:DropDownList>

                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlLeaveNamesingle"
                                                        ValidationGroup="submitsingle" Display="None" ErrorMessage="Please Select Leave Type"
                                                        InitialValue="0" SetFocusOnError="true" />--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Reason </label>
                                                    </div>
                                                    <asp:TextBox ID="txtEventDetailSingle" runat="server" CssClass="form-control" Style="resize: none" TabIndex="9" TextMode="MultiLine">
                                                    </asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtEventDetailSingle" Display="None"
                                                        ErrorMessage="Please Enter Reason" SetFocusOnError="true" ValidationGroup="submitsingle" />--%>
                                                </div>

                                                <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label id="lblSlot" runat="server" visible="false">Slots (Select All) </label>
                                                    </div>

                                                    <asp:CheckBox ID="chkCheckAllsingle" OnCheckedChanged="chkCheckAllsingle_CheckedChanged" AutoPostBack="true" Visible="false" runat="server" />
                                                    <asp:CheckBoxList ID="chkSlotssingle" CssClass="col-lg-12" RepeatDirection="Horizontal" RepeatColumns="4" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnApply" runat="server" TabIndex="10" Text="OD Apply and Approve"
                                                CssClass="btn btn-info" ValidationGroup="submitsingle" OnClick="btnApply_Click" />
                                            <asp:Button ID="btnReject" runat="server" TabIndex="11" CausesValidation="False" Text="OD Reject"
                                                OnClientClick="return UserRejectConfirmation();" CssClass="btn btn-warning" OnClick="btnReject_Click" />
                                            <asp:Button ID="btnCancelODsingle" runat="server" TabIndex="12" CausesValidation="False" Text="OD Cancel"
                                                OnClientClick="return UserCancelConfirmation();" CssClass="btn btn-warning" OnClick="btnCancelODsingle_Click" Style="display: none" Visible="false" />
                                            <asp:Button ID="btnCancelsingle" runat="server" TabIndex="13" CausesValidation="False" Text="Cancel"
                                                OnClick="btnCancelsingle_Click" CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                                ValidationGroup="submitsingle" ShowSummary="false" />
                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="true"
                                                ValidationGroup="Submitshow" ShowSummary="false" />
                                        </div>

                                        <div class="col-12">

                                            <asp:ListView ID="lvExamday" runat="server" OnItemDataBound="lvExamday_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>APPLIED OD LIST </h5>
                                                    </div>
                                                    <table id="tblHead1" class="table table-striped table-bordered nowrap display">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Select
                                                                </th>
                                                                <th style="display: none">Edit
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
                                                            <asp:CheckBox ID="chkOd" runat="server" ToolTip='<%# Eval("HOLIDAY_NO") %>' />
                                                            <asp:HiddenField ID="hidTtno" runat="server" Value='<%# Eval("HOLIDAY_NO") %>' />
                                                        </td>
                                                        <td style="display: none">
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
                                                            <asp:CheckBox ID="chkOd" runat="server" ToolTip='<%# Eval("HOLIDAY_NO") %>' />
                                                            <asp:HiddenField ID="hidTtno" runat="server" Value='<%# Eval("HOLIDAY_NO") %>' />
                                                        </td>
                                                        <td style="display: none">
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                CommandArgument='<%# Eval("HOLIDAY_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEdit_Click" />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("HOLIDAY_NO") %>'
                                                                ToolTip='<%# Eval("HOLIDAY_NO") %>' OnClientClick="return ConfirmSubmit();" OnClick="btnDelete_Click" />
                                                        </td>
                                                        <td>
                                                            <%--<%# Eval("REGNO")%>--%>
                                                            <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("REGNO") %>' />
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

                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="btnApply" />--%>
                                        <%-- <asp:PostBackTrigger ControlID="ddlOdTypesingle" />
                                        <asp:PostBackTrigger ControlID="txtFromDatesingle" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>



                            </div>
                        </div>

                        <div class="tab-pane " id="tabBC">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updBulkReg"
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
                            <div class="box-body">
                                <asp:UpdatePanel ID="updODReport" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>College & Session</label>
                                                    </div>
                                                    <asp:ListBox ID="ddlSession1" runat="server" SelectionMode="Multiple" AutoPostBack="true" CssClass="form-control multi-select-demo" AppendDataBoundItems="true" TabIndex="1"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnReport" runat="server" TabIndex="1" Text="Excel Report" OnClientClick="return validate();"
                                                CssClass="btn btn-primary" OnClick="btnReport_Click" />
                                            <asp:Button ID="btnCancel1" runat="server" TabIndex="1" CausesValidation="False" Text="Cancel"
                                                CssClass="btn btn-warning" OnClick="btnCancel1_Click" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="tab-pane" id="tabODCancel">

                            <div>
                                <asp:UpdateProgress ID="updProg4" runat="server" AssociatedUpdatePanelID="updODCancel"
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
                            <div class="box-body">
                                <asp:UpdatePanel ID="updODCancel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblRRNO" runat="server" Font-Bold="true" Text="RRNO"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtRollNoCancel" runat="server" ValidationGroup="SubmitCancel" CssClass="form-control" TabIndex="1" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:Button ID="btnSubmitShowCancel" runat="server" Text="Show"
                                                        ValidationGroup="SubmitCancel" CssClass="btn btn-primary" TabIndex="2" OnClick="btnSubmitShowCancel_Click" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRollNoCancel" Display="None"
                                                        ErrorMessage="Please Enter REGNO" SetFocusOnError="true" ValidationGroup="SubmitCancel" />
                                                </div>
                                            </div>

                                            <asp:Panel ID="tblInfoCancel" runat="server" Visible="false">

                                                <div class="row">
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblNamecan" runat="server" Font-Bold="True" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Mother Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblMotherNamecan" runat="server" Font-Bold="True" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblRegnocan" runat="server" Font-Bold="true" Text="Registration No"></asp:Label>
                                                                :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblEnrollNocan" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblSemcan" runat="server" Font-Bold="true">Semester</asp:Label>
                                                                :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblSemestercan" runat="server" Font-Bold="True" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>PH :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblPHcan" runat="server" Font-Bold="True" />
                                                                </a>
                                                            </li>

                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Father Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblFatherNamecan" runat="server" Font-Bold="True" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblDegreecan" runat="server" Font-Bold="True" Text="Degree" />
                                                                /
                                                                    <asp:Label ID="lbltxtBranchcan" runat="server" Font-Bold="True" Text="Branch / Programme" />
                                                                :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBranchcan" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>

                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblddlAdmBatchcan" runat="server" Font-Bold="True" Text="Admission Batch" />
                                                                :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblAdmBatchcan" runat="server" Font-Bold="True" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblddlSchemecan" runat="server" Font-Bold="True" Text="Scheme" />
                                                                :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblSchemecan" runat="server" Font-Bold="True" />
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>

                                                    <div class="col-lg-2 col-md-6 col-12">
                                                        <div class="form-group col-md-12">
                                                            <asp:Image ID="imgPhotocan" runat="server" Width="128px" Height="128px"
                                                                BorderColor="Black" BorderWidth="2px" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                        <div class="col-12 mt-4">
                                            <div class="row" id="Div3" runat="server">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblSessionsinglecan" runat="server" Font-Bold="true" Text="Session"></asp:Label>
                                                    </div>

                                                    <asp:DropDownList ID="ddlSessionsinglecan" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlSessionsinglecan_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hiddenfieldfromDtsinglecan" runat="server" Visible="False" />
                                                    <asp:HiddenField ID="hiddenField2" runat="server" Visible="False" />
                                                    <asp:HiddenField ID="hiddenfield3" runat="server" Visible="False" />
                                                    <asp:HiddenField ID="hiddenField4" runat="server" Visible="False" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnCancelsinglecan" runat="server" TabIndex="13" CausesValidation="False" Text="Cancel"
                                                OnClick="btnCancelsinglecan_Click" CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true"
                                                ValidationGroup="SubmitCancel" ShowSummary="false" />
                                        </div>

                                        <div class="col-12">

                                            <asp:ListView ID="lvExamdaycancel" runat="server" OnItemDataBound="lvExamdaycancel_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>APPLIED OD LIST </h5>
                                                    </div>
                                                    <table id="tblHead1can" class="table table-striped table-bordered nowrap display">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <%-- <th>
                                                                    <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="True" />
                                                                </th>
                                                                <th>Name
                                                                </th>--%>
                                                                <th>OD Type
                                                                </th>
                                                                <th>OD Name
                                                                </th>
                                                                <th>OD Start Date
                                                                </th>
                                                                <th>OD End Date
                                                                </th>
                                                                <th>Status
                                                                </th>
                                                                <th style="text-align:center">Cancel OD
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
                                                        <%-- <td>
                                                            <%# Eval("REGNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STUDNAME")%>
                                                        </td>--%>
                                                        <td>
                                                            <%# Eval("OD_NAME")%>
                                                            <asp:HiddenField ID="hdnODTYPEcan" runat="server" Value='<%# Eval("ODTYPE")%>' />
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
                                                            <asp:Label ID="lblStatuscan" runat="server" Text='<%# Eval("APPROVAL_STATUS")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align:center">
                                                            <asp:ImageButton ID="btnCancelOd" runat="server" ImageUrl="../Images/delete.png" CommandArgument='<%# Eval("HOLIDAY_NO") %>'
                                                             ToolTip='<%# Eval("HOLIDAY_NO") %>'  OnClick="btnCancelOd_Click" OnClientClick="return UserCancelConfirmation();"/> 
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>



                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>





    <div id="divMsg" runat="server">
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

    <script>
        function totAllStudents(headchk) {
            var rows = document.getElementById("tblHead").getElementsByTagName("tbody")[0].getElementsByTagName("tr").length;

            for (i = 0; i < rows; i++) {
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                if (headchk.checked == true) {
                    if (e.disabled == false)
                        e.checked = true;
                }
                else
                    e.checked = false;
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
    <script>
        function validate() {
            var session = document.getElementById("ctl00_ContentPlaceHolder1_ddlSession1").value;

            if (session == "") {
                alert("Please Select College & Session.");
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnReport').click(function () {
                    validate();
                });
            });
        });
    </script>
    <script type="text/javascript">
        function UserApproveConfirmation() {
            if (confirm("Are You Sure You Want To Approve Leave For All Selected Students?"))
                return true;
            else
                return false;
        }
        function UserRejectConfirmation() {
            if (confirm("Are You Sure You Want To Reject Leave For All Selected Students?"))
                return true;
            else
                return false;
        }
        function UserCancelConfirmation() {
            if (confirm("Are You Sure You Want To Cancel Leave For All Selected Students?"))
                return true;
            else
                return false;
        }

    </script>
</asp:Content>
