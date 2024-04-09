<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApprovalbyAdmin.aspx.cs" Inherits="HOSTEL_GATEPASS_ApprovalbyAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        ul.ui-autocomplete {
            StaffInformation max-height: 180px !important;
            overflow: auto !important;
        }
    </style>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };

        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;
            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }

        function formatNumber(input) {
            var value = input.value;
            if (value < 10) {
                input.value = '0' + value;
            }
        }
    </script>

    <meta charset="UTF-8">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--lblDynamicPageTitle Added By Himanshu tamrakar 23-02-2024--%>
                    <h3 class="box-title" style="text-transform: uppercase;">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <%--<div class="box-header with-border">
                    <h3 class="box-title">Final Approval by Admin</h3>
                </div>--%>
                <br />
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>From Date </label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDateSearch" runat="server" TabIndex="5" ToolTip="Enter Out Date" CssClass="form-control" />
                                    <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromDateSearch" PopupButtonID="imgFromDate" Enabled="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFromDateSearch"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="false" />
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" EmptyValueMessage="Please Enter From date."
                                        ControlExtender="MaskedEditExtender1" ControlToValidate="txtFromDateSearch" IsValidEmpty="false"
                                        InvalidValueMessage="From Date  is invalid" Display="None" TooltipMessage="Input a date"
                                        ErrorMessage="Please Select From Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="search" SetFocusOnError="true" />
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>To Date </label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDateSearch" runat="server" TabIndex="5" ToolTip="Enter Out Date" CssClass="form-control" />
                                    <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtToDateSearch" PopupButtonID="imgFromDate" Enabled="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtToDateSearch"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="false" />
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" EmptyValueMessage="Please Enter To Date."
                                        ControlExtender="MaskedEditExtender3" ControlToValidate="txtToDateSearch" IsValidEmpty="false"
                                        InvalidValueMessage="To Date  is invalid" Display="None" TooltipMessage="Input a In Date"
                                        ErrorMessage="Please Select To Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="search" SetFocusOnError="true" />
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Apply Date </label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtApplyDate" runat="server" TabIndex="1" ToolTip="Enter Date" CssClass="form-control" />
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtApplyDate" PopupButtonID="imgFromDate" Enabled="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtApplyDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="false" />
                                </div>
                            </div>
                            <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Purpose </label>
                                </div>
                                <asp:DropDownList ID="ddlPurposeSearch" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPurposeSearch"
                                    Display="None" ErrorMessage="Please Select Purpose" SetFocusOnError="True"
                                    InitialValue="0" />

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic ">
                                    <label>Status </label>
                                </div>
                                <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                    <asp:ListItem Value="A" Text="Approve"></asp:ListItem>
                                    <asp:ListItem Value="R" Text="Reject"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Pending"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvStatus" runat="server" ControlToValidate="ddlStatus"
                                    Display="None"
                                    InitialValue="0" />
                            </div>--%>
                        </div>
                        <div class="col-12 btn-footer" id="Div2" runat="server">
                            <asp:Button ID="btnSearch" runat="server" Text="Search"  CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" />

                            <asp:Button ID="btnBack" runat="server" Text="Back"  CssClass="btn btn-outline-danger" OnClick="btnBack_Click" />
                            <asp:ValidationSummary ID="search" DisplayMode="List" runat="server" ValidationGroup="search" ShowMessageBox="true" ShowSummary="false" />

                        </div>
<<<<<<< HEAD

                        <div class="row">
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Fourth Approval</label>
                                </div>
                                <asp:DropDownList ID="ddlAA4" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAA4_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvAA4" runat="server" ErrorMessage="Please Select Fourth Approval"
                                    Display="None" ControlToValidate="ddlAA4" SetFocusOnError="True" ValidationGroup="AA"></asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <label>Path</label>
                                <asp:TextBox ID="txtAAPath" runat="server" ReadOnly="true" TextMode="MultiLine"
                                    Rows="1" TabIndex="7" ToolTip="Path" Height="67px"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <br />
                    <br />
                    <div class="col-12 btn-footer">
<<<<<<< HEAD
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" TabIndex="2" ValidationGroup="submit"
                            CssClass="btn btn-primary" OnClick="btnApprove_Click"/>
                        <asp:Button ID="btnChangeApproval" runat="server" Text="Change Approval" ValidationGroup="AA" 
                            CssClass="btn btn-outline-success" OnClick="btnChangeApproval_Click" />
                        <asp:Button ID="btnUpdatePath" runat="server" Text="Update Approval" ValidationGroup="AA" Visible="false"  
=======
                        <asp:Button ID="btnApprove" runat="server" Text="Submit" TabIndex="2" ValidationGroup="submit"
                            CssClass="btn btn-primary" OnClick="btnApprove_Click" />
                        <asp:Button ID="btnChangeApproval" runat="server" Text="Change Approval" ValidationGroup="AA"
                            CssClass="btn btn-outline-success" OnClick="btnChangeApproval_Click" Visible="false" />
                        <asp:Button ID="btnUpdatePath" runat="server" Text="Update Approval" ValidationGroup="AA" Visible="false"
>>>>>>> 81a226ca ([ENHANCEMENT][56241] GATE PASS PAGES)
                            CssClass="btn btn-outline-success" OnClick="btnUpdatePath_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="3"
                            CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                        <asp:ValidationSummary ID="valAA" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="AA" />
                    </div>

                    <div class="col-12">
                        <asp:ListView ID="lvGatePass" runat="server" OnItemDataBound="lvGatePass_ItemDataBound">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>List of Applied Students</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>
                                                <asp:CheckBox ID="chkAll" runat="server" onclick="return CheckAll(this);" />
                                                All
                                            </th>
                                            <th>Student Name
                                            </th>
                                            <th>Gate Pass No
                                            </th>
                                            <th>Out Date
                                            </th>
                                            <th>In Date
                                            </th>
                                            <th>Purpose
                                            </th>
                                            <th>Remarks
                                            </th>
                                            <th>Status
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblSearchResults">
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                    <tbody>
                                        <tr id="Tr1" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkApprove" runat="server" onclick="totSubjects(this)" />
                                        <asp:HiddenField ID="hidrecid" runat="server" Value='<%# Eval("HGP_ID") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblname" runat="server" style="text-wrap:normal" Text='<%# Eval("REGNO")+" - "+Eval("STUDNAME") %>'></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblGatepassnno" runat="server" Text='<%# (Eval("HOSTEL_GATE_PASS_NO").ToString())=="" ? "..." : Eval("HOSTEL_GATE_PASS_NO") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <%# Eval("OUTDATE","{0:dd/MM/yyyy hh:mm tt}") %>
                                    </td>
                                    <td>
                                        <%# Eval("INDATE","{0:dd/MM/yyyy hh:mm tt}") %>
                                    </td>
                                    <td>
                                        <%# Eval("PURPOSE_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("REMARKS") %>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>' ForeColor='<%# Eval("STATUS").Equals("APPROVED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                        <%--Added By Himanshu tamrakar 13-032024--%>

                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyItemTemplate>
                                -- Record Not Found --
                            </EmptyItemTemplate>
                        </asp:ListView>
                    </div>



=======
>>>>>>> 9ae1c2d5 ([ENHANCEMENT][56241] GATE PASS CHANGES)
                </div>
                    <hr />
    <div class="col-12" id="finalAproval" runat="server">
        <div class="row">
            <div class="form-group col-lg-3 col-md-4 col-12">
                <div class="label-dynamic">
                    <sup>* </sup>
                    <label>Status</label>
                </div>
                <asp:DropDownList ID="ddlremark" runat="server">
                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                    <asp:ListItem Value="A" Text="Approve"></asp:ListItem>
                    <asp:ListItem Value="R" Text="Reject"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfcRemark" runat="server" ErrorMessage="Please Enter Status" InitialValue="0"
                    Display="None" ControlToValidate="ddlremark" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-lg-9 col-md-4 col-12">
                <div class="label-dynamic">
                    <sup>* </sup>
                    <label>Remark</label>
                </div>
                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="1" Rows="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtRemark" runat="server" ErrorMessage="Please Enter Remark"
                    Display="None" ControlToValidate="txtRemark" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <div class="col-12" id="changeApproval" runat="server" visible="false">
        <div class="row">
            <div class="form-group col-lg-6 col-md-4 col-12">
                <div class="label-dynamic">
                    <label>Student Name</label>
                </div>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TabIndex="2" Enabled="False"></asp:TextBox>
            </div>
        </div>

        <div class="row">

            <div class="form-group col-lg-4 col-md-4 col-12">
                <div class="label-dynamic">
                    <sup>* </sup>
                    <label>First Approval</label>
                </div>
                <asp:DropDownList ID="ddlAA1" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAA1_SelectedIndexChanged">
                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvAA1" runat="server" ErrorMessage="Please Select First Approval"
                    Display="None" ControlToValidate="ddlAA1" SetFocusOnError="True" ValidationGroup="AA"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-lg-4 col-md-4 col-12">
                <div class="label-dynamic">
                    <sup>* </sup>
                    <label>Second Approval</label>
                </div>
                <asp:DropDownList ID="ddlAA2" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAA2_SelectedIndexChanged">
                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvAA2" runat="server" ErrorMessage="Please Select Second Approval"
                    Display="None" ControlToValidate="ddlAA2" SetFocusOnError="True" ValidationGroup="AA"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-lg-4 col-md-4 col-12">
                <div class="label-dynamic">
                    <sup>* </sup>
                    <label>Third Approval</label>
                </div>
                <asp:DropDownList ID="ddlAA3" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAA3_SelectedIndexChanged">
                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvAA3" runat="server" ErrorMessage="Please Select Third Approval"
                    Display="None" ControlToValidate="ddlAA3" SetFocusOnError="True" ValidationGroup="AA"></asp:RequiredFieldValidator>
            </div>

        </div>

        <div class="row">
            <div class="form-group col-lg-4 col-md-4 col-12">
                <div class="label-dynamic">
                    <%--<sup>* </sup>--%>
                    <label>Fourth Approval</label>
                </div>
                <asp:DropDownList ID="ddlAA4" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAA4_SelectedIndexChanged">
                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                </asp:DropDownList>
                <%--<asp:RequiredFieldValidator ID="rfvAA4" runat="server" ErrorMessage="Please Select Fourth Approval"
                                    Display="None" ControlToValidate="ddlAA4" SetFocusOnError="True" ValidationGroup="AA"></asp:RequiredFieldValidator>--%>
            </div>

            <div class="form-group col-lg-4 col-md-4 col-12">
                <label>Path</label>
                <asp:TextBox ID="txtAAPath" runat="server" ReadOnly="true" TextMode="MultiLine"
                    Rows="1" TabIndex="7" ToolTip="Path" Height="67px"></asp:TextBox>
            </div>
        </div>
    </div>

    
    <div class="col-12 btn-footer">
        <asp:Button ID="btnApprove" runat="server" Text="Submit" TabIndex="2" ValidationGroup="submit"
            CssClass="btn btn-primary" OnClick="btnApprove_Click" />
        <asp:Button ID="btnChangeApproval" runat="server" Text="Change Approval" ValidationGroup="AA"
            CssClass="btn btn-outline-success" OnClick="btnChangeApproval_Click" Visible="false" />
        <asp:Button ID="btnUpdatePath" runat="server" Text="Update Approval" ValidationGroup="AA" Visible="false"
            CssClass="btn btn-outline-success" OnClick="btnUpdatePath_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="3"
            CssClass="btn btn-warning" OnClick="btnCancel_Click" />

        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="submit" />
        <asp:ValidationSummary ID="valAA" runat="server" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="AA" />
    </div>

    <div class="col-12">
        <asp:ListView ID="lvGatePass" runat="server" OnItemDataBound="lvGatePass_ItemDataBound">
            <LayoutTemplate>
                <div class="sub-heading">
                    <h5>List of Applied Students</h5>
                </div>
                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                    <thead class="bg-light-blue">
                        <tr>
                            <th>
                                <asp:CheckBox ID="chkAll" runat="server" onclick="return CheckAll(this);" />
                                All
                            </th>
                            <th>Student Name
                            </th>
                            <th>Gate Pass No
                            </th>
                            <th>Out Date
                            </th>
                            <th>In Date
                            </th>
                            <th>Purpose
                            </th>
                            <th>Remarks
                            </th>
                            <th>Status
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tblSearchResults">
                        <tr id="itemPlaceholder" runat="server" />
                    </tbody>
                    <tbody>
                        <tr id="Tr1" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkApprove" runat="server" onclick="totSubjects(this)" />
                        <asp:HiddenField ID="hidrecid" runat="server" Value='<%# Eval("HGP_ID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblname" runat="server" Style="text-wrap: normal" Text='<%# Eval("REGNO")+" - "+Eval("STUDNAME") %>'></asp:Label>

                    </td>
                    <td>
                        <asp:Label ID="lblGatepassnno" runat="server" Text='<%# (Eval("HOSTEL_GATE_PASS_NO").ToString())=="" ? "..." : Eval("HOSTEL_GATE_PASS_NO") %>'></asp:Label>
                    </td>
                    <td>
                        <%# Eval("OUTDATE","{0:dd/MM/yyyy hh:mm tt}") %>
                    </td>
                    <td>
                        <%# Eval("INDATE","{0:dd/MM/yyyy hh:mm tt}") %>
                    </td>
                    <td>
                        <%# Eval("PURPOSE_NAME") %>
                    </td>
                    <td>
                        <%# Eval("REMARKS") %>
                    </td>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>' ForeColor='<%# Eval("STATUS").Equals("APPROVED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                        <%--Added By Himanshu tamrakar 13-032024--%>

                    </td>
                </tr>
            </ItemTemplate>
            <EmptyItemTemplate>
                -- Record Not Found --
            </EmptyItemTemplate>
        </asp:ListView>
    </div>



    </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">

        function CheckAll(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;
                        headchk.checked == true;
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGatePass_chkAll').checked = true;
                    }
                    else {
                        e.checked = false;
                        headchk.checked == false;
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGatePass_chkAll').checked = false;
                    }
                }
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .form-control {
        }
    </style>
</asp:Content>



