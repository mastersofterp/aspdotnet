<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Exam_Fee_Config_New.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Exam_Fee_Config_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <style>
        .fa-edit {
            color: #4c6ef5;
            font-size: 14px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .switch label {
            cursor: pointer;
            width: 60px;
            height: 25px;
        }

        .switch input:checked + label:before {
            content: attr(data-on);
            position: absolute;
            left: 0;
            font-size: 12px;
            padding: 3px 10px;
        }

        .switch input:checked + label:after {
            transform: translateX(49px);
        }

        .switch label:after {
            content: '';
            position: absolute;
            top: 1.4px;
            left: 1.7px;
            width: 7.5px;
            height: 22.5px;
        }

        .switch label:before {
            content: attr(data-off);
            position: absolute;
            right: 0;
            font-size: 12px;
            padding: 3px 10px;
            font-weight: 400;
        }

            @keyframes marquee {
        from {
            transform: translateX(100%);
        }
        to {
            transform: translateX(-100%);
        }
    }
    </style>

   



    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFee"
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

    <%-- <script type="text/javascript">
        $(window).on("load", function () {
            $("#ctl00_ContentPlaceHolder1_txtLateFee").hide();
        });
    </script>--%>

    <%--<script>
        $(window).load(function () {
            $("#ctl00_ContentPlaceHolder1_").hide();
        });
    </script>--%>

    <asp:UpdatePanel ID="updFee" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">New Exam Fee Configuration</h3>--%>
                             <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">

                                        
                                        <div class="label-dynamic">

                                            <sup>*</sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>

                                        </div>

                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfsession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">

                                            <sup>*</sup>
                                            <%--<label>College</label>--%>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>

                                        </div>

                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">

                                        <div class="label-dynamic">

                                            <sup>*</sup>
                                            <%--<label>Exam Type</label>--%>
                                            <asp:Label ID="lblDYddlExamType" runat="server" Font-Bold="true"></asp:Label>

                                        </div>

                                        <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                            <%--<asp:ListItem Value="-1">Please Select</asp:ListItem>--%>
                                            <%--     <asp:ListItem Value="0">Regular</asp:ListItem>
                                    <asp:ListItem Value="1">BackLog</asp:ListItem>
                                    <asp:ListItem Value="2">Re-Do</asp:ListItem>
                                    <asp:ListItem Value="3">Re-Evaluation</asp:ListItem>
                                            --%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfExamDate" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please Select Exam Type" InitialValue="-1" ValidationGroup="Show"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">

                                        <div class="label-dynamic">

                                            <sup>*</sup>
                                            <%--<label>Fees Structure</label>--%>
                                            <asp:Label ID="lblDYddlFeesStructure" runat="server" Font-Bold="true"></asp:Label>

                                        </div>

                                        <asp:DropDownList ID="ddlFeesStructure" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true" TabIndex="4" OnSelectedIndexChanged="ddlFeesStructure_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Course Type Wise</asp:ListItem>
                                            <asp:ListItem Value="2">Credit Wise</asp:ListItem>
                                            <asp:ListItem Value="3">Course Wise</asp:ListItem>
                                            <asp:ListItem Value="4">Fix</asp:ListItem>
                                            <asp:ListItem Value="5">Credit Range Wise</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="FeesStructure" runat="server" ControlToValidate="ddlFeesStructure"
                                            Display="None" ErrorMessage="Please Select Fee Structure" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">

                                        <div class="label-dynamic">

                                            <sup>*</sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>

                                        </div>

                                        <%--<asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple" data-select2-enable="true" TabIndex="5" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>

                                        <asp:ListBox ID="ddlDegree" runat="server" CssClass="form-control multi-select-demo" AutoPostBack="true" SelectionMode="Multiple" TabIndex="6" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="rfDegree" runat="server" ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>

                                        <%--ValidationGroup="Show"--%>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12">
                                        <div class="row">
                                            <div id="fsem" runat="server" class="form-group col-lg-7 col-md-7 col-12">

                                                <div class="label-dynamic">

                                                    <sup>*</sup>
                                                    <%--<label>Semester</label>--%>
                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>

                                                </div>

                                                <asp:ListBox ID="lstSemester" runat="server" CssClass="form-control multi-select-demo" AutoPostBack="true" SelectionMode="Multiple" TabIndex="6" OnSelectedIndexChanged="lstSemester_SelectedIndexChanged"></asp:ListBox>

                                                <asp:RequiredFieldValidator ID="rfSemester" runat="server" ControlToValidate="lstSemester" Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>

                                                <%--ValidationGroup="Show"--%>
                                            </div>

                                            <div class="form-group col-lg-5 col-md-5 col-12 pl-0">
                                                <div class="label-dynamic">
                                                    <%--<sup>*</sup>--%>
                                                    <%--<label>Fee Applicable</label>--%>
                                                    <asp:Label ID="lblDYFeeApplicable" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="switch form-inline" onclick="clickRdActive();">
                                                    <input type="checkbox" id="rdActive" name="rdActive" runat="server" tabindex="7" />
                                                    <label data-on="Yes" data-off="No" for="rdActive"></label>
                                                    <asp:HiddenField ID="hdFeeApplicable" runat="server" ClientIDMode="Static" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-8 col-md-8 col-12 pr-0 d-none">
                                                <div class="label-dynamic">
                                                    <%--<sup>*</sup>--%>
                                                    <label>Processing Fee Applicable</label>
                                                    <%--<asp:Label ID="Label8" runat="server" Font-Bold="true"></asp:Label>--%>
                                                </div>
                                                <div class="switch form-inline" onclick="clickTest();">
                                                    <input type="checkbox" id="test" name="test" runat="server" tabindex="8" />
                                                    <label data-on="Yes" data-off="No" for="test"></label>
                                                    <asp:HiddenField ID="hdFeeProcessApplicable" runat="server" ClientIDMode="Static" />
                                                </div>
                                            </div>

                                            <div id="txtProcess" class="form-group col-lg-4 col-md-4 col-12 pl-sm-1 d-none">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>If Yes, Then </label>
                                                </div>
                                                <asp:TextBox ID="txtYes" MaxLength="6" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Enter Processing Fees" OnTextChanged="txtYes_TextChanged" Text="0"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftxtYes" runat="server" ValidChars=".0123456789"
                                                    TargetControlID="txtYes">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 d-none">
                                        <div class="row">
                                            <%-- is certificate fee --%>

                                            <div class="form-group col-lg-8 col-md-8 col-12 pr-0">
                                                <div class="label-dynamic">
                                                    <%--<sup>*</sup>--%>
                                                    <label>Certificate Fee Applicable</label>
                                                </div>
                                                <div class="switch form-inline" onclick="clickCerFee();">
                                                    <input type="checkbox" id="chkFeeCer" name="Certificate Fee" tabindex="10" runat="server" />
                                                    <label data-on="Yes" data-off="No" for="Certificate Fee"></label>
                                                    <asp:HiddenField ID="hfdFeeCer" runat="server" ClientIDMode="Static" />
                                                </div>
                                            </div>

                                            <div id="txtCertiFee" class="form-group col-lg-4 col-md-4 col-12 pl-sm-1 d-none">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>If Yes, Then </label>
                                                </div>
                                                <asp:TextBox ID="txtCerFee" MaxLength="6" runat="server" CssClass="form-control" TabIndex="11" ToolTip="Enter Certificate Fees" Text="0"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fltxtCerFee" runat="server" ValidChars=".0123456789"
                                                    TargetControlID="txtCerFee">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-12 col-md-8">
                                        <div class="row d-none">
                                            <div class="form-group col-lg-4 col-md-7 col-6">
                                                <div class="row">
                                                    <div class="form-group col-lg-7 col-md-7 col-6">
                                                        <div class="label-dynamic">
                                                            <%--<sup>*</sup>--%>
                                                            <label>Late Fee Applicable</label>
                                                        </div>
                                                        <div class="switch form-inline" onclick="clickLateFee();">
                                                            <input type="checkbox" id="chkLateFee" name="Late Fees" tabindex="12" runat="server" />
                                                            <label data-on="Yes" data-off="No" for="chkLateFee"></label>

                                                            <asp:HiddenField ID="hidLatefeeChecked" runat="server" ClientIDMode="Static" />

                                                        </div>
                                                    </div>
                                                    <div id="txtLateFee" class="form-group col-lg-5 col-md-5 col-6 pl-sm-0 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>If Yes, Then </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlLateFee" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="13">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Daily</asp:ListItem>
                                                            <asp:ListItem Value="2">Weekly</asp:ListItem>
                                                            <asp:ListItem Value="3">Monthly</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="LateFees" runat="server" ControlToValidate="ddlLateFee" Display="None" ErrorMessage="Please Select Fee Mode" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                            </div>
                                            <div id="DivLateFeeDate" class="form-group col-lg-5 col-md-7 col-6 pl-lg-5 d-none">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Late Fee Date</label>
                                                </div>
                                                <%--<div id="picker" class="form-control" tabindex="3">
                                                    <i class="fa fa-calendar"></i>&nbsp;
                                                    <span id="date"></span>
                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                </div>--%>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i id="dvlatefee" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtLateFeeDate" runat="server" ToolTip="Please Enter Late Fee Date" AutoPostBack="True" OnTextChanged="txtLateFeeDate_TextChanged" TabIndex="14"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="LateFeeDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLateFeeDate" PopupButtonID="dvlatefee"></ajaxToolKit:CalendarExtender>

                                                    <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtLateFeeDate"
                                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                        MaskType="Date" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" EmptyValueMessage="Please Enter From Date"
                                                        ControlExtender="meFromDate" ControlToValidate="txtLateFeeDate" IsValidEmpty="false"
                                                        InvalidValueMessage=" Date is invalid" Display="None" ErrorMessage="Please Enter From Date"
                                                        InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                </div>
                                            </div>

                                            <div id="DivLateFeeAmount" class="form-group col-lg-3 col-md-4 col-12 d-none">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtLateFeeAmount" MaxLength="6" runat="server" CssClass="form-control" TabIndex="15" ToolTip="Enter Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtLateAmount" runat="server" ValidChars=".0123456789"
                                                    TargetControlID="txtLateFeeAmount">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-7 col-6">
                                                <div class="row">

                                                    <div class="form-group col-lg-6 col-md-7 col-6">

                                                        <div class="label-dynamic">
                                                            <%--<sup>*</sup>--%>
                                                            <label>Paper Valuation Fee</label>
                                                        </div>

                                                        <asp:TextBox ID="txtValuationFee" runat="server" TabIndex="16" CssClass="form-control" MaxLength="6" ToolTip="Enter Valuation Fee" Text="0"></asp:TextBox>

                                                        <ajaxToolKit:FilteredTextBoxExtender ID="txtValueFee" runat="server" ValidChars=".0123456789"
                                                            TargetControlID="txtValuationFee">
                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-7 col-6">

                                                        <div class="label-dynamic">
                                                            <%--<sup>*</sup>--%>
                                                            <label>Valuation Max Fee</label>
                                                        </div>

                                                        <asp:TextBox ID="txtValuationMaxFee" runat="server" TabIndex="17" CssClass="form-control" MaxLength="6" ToolTip="Enter Valuation Max Fee" Text="0"></asp:TextBox>

                                                        <ajaxToolKit:FilteredTextBoxExtender ID="txtValueMaxFee" runat="server" ValidChars=".0123456789"
                                                            TargetControlID="txtValuationMaxFee">
                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                    </div>

                                                    <div id="ddlPayment" class="form-group col-lg-6 col-md-7 col-6 d-none">

                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Payment Mode</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Online</asp:ListItem>
                                                            <asp:ListItem Value="2">Offline</asp:ListItem>
                                                            <asp:ListItem Value="3">Both</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 col-md-4 col-lg-4">
                                        <div id="divsem">
                                            <asp:ListView ID="lvSem" Visible="false" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading">
                                                            <h5>Semester Wise Amount</h5>
                                                        </div>
                                                        <div class="table-responsive" style="height: 150px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Semester
                                                                        </th>

                                                                        <th>Fee Amount
                                                                        </th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>

                                                            <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNO")%>' ToolTip=' <%# Eval("SEMESTERNAME")%>'></asp:Label>
                                                        </td>


                                                        <td>
                                                            <asp:TextBox ID="txtFee" runat="server" MaxLength="9" Text='<%# Eval("FEE")%>' placeholder="Enter Fee" />
                                                            <asp:RequiredFieldValidator ID="rfvMaxMark" runat="server" ControlToValidate="txtFee"
                                                                Display="None" ErrorMessage="Please Enter  Mark" ValidationGroup="submit"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtFee">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                        <div id="Lvcourse" runat="server">
                                            <asp:ListView ID="lvFee" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading">
                                                            <h5>Fee Amount</h5>
                                                        </div>
                                                        <div class="table-responsive" style="height: 150px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>

                                                                        <th>Course
                                                                        </th>

                                                                        <th>Fee Amount
                                                                        </th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>

                                                            <asp:Label ID="lblSubType" runat="server" Text='<%# Eval("SUBNAME")%>' ToolTip=' <%# Eval("SUBID")%>'></asp:Label>
                                                        </td>


                                                        <td>
                                                            <asp:TextBox ID="txtFee" runat="server" MaxLength="9" Text='<%# Eval("FEE")%>' placeholder="Enter Fee" />
                                                            <asp:RequiredFieldValidator ID="rfvMaxMark" runat="server" ControlToValidate="txtFee"
                                                                Display="None" ErrorMessage="Please Enter Mark" ValidationGroup="submit"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtFee">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>


                                        <div id="Div2" runat="server" visible="false">
                                            <div class="sub-heading">
                                                <h5>Fee Amount</h5>
                                            </div>
                                            <div class="table-responsive" style="height: 150px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th>Course</th>
                                                            <th>Fee Amount</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>Theory</td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text="200"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Practical</td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Text="150"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Laboratory</td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Text="100"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Design Studio</td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" Text="100"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <%-- is certificate fee end --%>


                                        <div id="divCredit" runat="server" visible="false">
                                            <div class="sub-heading">
                                                <h5>Fee Amount</h5>
                                            </div>
                                            <div class="table-responsive" style="border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                    <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th style="color: black">Credit Wise</th>
                                                            <th>
                                                                <asp:TextBox ID="txtCredit" runat="server" CssClass="form-control" MaxLength="9" placeholder="Enter Fee"></asp:TextBox></th>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftxtCredit" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtCredit">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div id="divCourse" runat="server" visible="false">
                                            <div class="sub-heading">
                                                <h5>Fee Amount</h5>
                                            </div>
                                            <div class="table-responsive" style="border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="Table1">
                                                    <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th style="color: black">Course Wise</th>
                                                            <th>
                                                                <asp:TextBox ID="txtCourse" runat="server" CssClass="form-control" MaxLength="9" placeholder="Enter Fee"></asp:TextBox></th>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftxtCourse" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtCourse">
                                                            </ajaxToolKit:FilteredTextBoxExtender>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div id="divFix" runat="server" visible="false">
                                            <div class="sub-heading">
                                                <h5>Fee Amount</h5>
                                            </div>
                                            <div class="table-responsive" style="border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="Table2">
                                                    <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th style="color: black">Fix</th>
                                                            <th>
                                                                <asp:TextBox ID="txtfix" runat="server" CssClass="form-control"></asp:TextBox></th>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextender1" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtfix">
                                                            </ajaxToolKit:FilteredTextBoxExtender>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div id="divRenge_bk" class="d-none" runat="server" visible="false">
                                            <div class="sub-heading">
                                                <h5>Credit Range</h5>
                                            </div>
                                            <div class="table-responsive" style="height: 150px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="Table3">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th>NO</th>
                                                            <th>MinRange</th>
                                                            <th>MaxRange</th>
                                                            <th>Amount</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>1</i></td>
                                                            <td>
                                                                <asp:TextBox ID="txtMinRange" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextender2" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtMinRange">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <td>
                                                                <asp:TextBox ID="txtMaxRange" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextender3" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtMaxRange">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <td>
                                                                <asp:TextBox ID="txtAmount" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextender4" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtAmount">
                                                            </ajaxToolKit:FilteredTextBoxExtender>

                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div id="divRenge" runat="server" visible="false">

                                            <asp:ListView ID="lvrange" Visible="false" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="table-responsive" style="height: 150px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>

                                                                        <th style="text-align: center">SrNo
                                                                        </th>
                                                                        <th style="text-align: center">MinRange
                                                                        </th>


                                                                        <th style="text-align: center">MaxRange
                                                                        </th>
                                                                        <th style="text-align: center">Amount
                                                                        </th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <%--  <td><%# Container.DataItemIndex + 1 %></i></td>--%>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <asp:HiddenField ID="hfsrno" runat="server" Value='<%#Eval("FID") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:TextBox ID="txtMinRange" MaxLength="6" Text='<%#Eval("Minmark") %>' runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextender2" runat="server" ValidChars=".0123456789"
                                                            TargetControlID="txtMinRange">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <td style="text-align: center">
                                                            <asp:TextBox ID="txtMaxRange" MaxLength="6" Text='<%#Eval("Maxmark") %>' runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextender3" runat="server" ValidChars=".0123456789"
                                                            TargetControlID="txtMaxRange">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <td style="text-align: center">
                                                            <asp:TextBox ID="txtAmount" MaxLength="6" runat="server" Text='<%#Eval("Amount") %>' CssClass="form-control"></asp:TextBox></td>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextender4" runat="server" ValidChars=".0123456789"
                                                            TargetControlID="txtAmount">
                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                        </td>
                                    
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                            <div class="col-12 btn-footer">
                                                <%--<asp:Button ID="BtnAddAssesment" causevalidation="false" runat="server" Text="Add" Visible="false" CssClass="btn btn-outline-info" OnClick="BtnAddAssesment_Click" />--%>
                                                <asp:Button ID="btnadd" runat="server" Text="Add" Visible="false" OnClientClick="return test();" CssClass="btn btn-outline-primary" OnClick="btnadd_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <%--ADDED BY GAURAV START--%>
                                      <div id="divhead">
                                            <asp:ListView ID="lvheads" Visible="true" runat="server" OnItemDataBound="lvheads_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading">
                                                            <h5>HEADDDDD</h5>
                                                        </div>
                                                        <div class="table-responsive" style="height: 150px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>HEAD
                                                                        </th>

                                                                        <th>Fee Amount
                                                                        </th>
                                                                          <th>AMOUNT
                                                                        </th>


                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>

                                                            <asp:Label ID="lblhead" runat="server" Text='<%# Eval("FEE_HEAD")%>' ToolTip=' <%# Eval("FEE_HEAD")%>'></asp:Label>
                                                        </td>
                                                        <td>

                                                            <asp:Label ID="lbllongname" runat="server" Text='<%# Eval("FEE_LONGNAME")%>' ToolTip=' <%# Eval("RECIEPT_CODE")%>'></asp:Label>
                                                        </td>



                                                        <td>
                                                            <%--<asp:TextBox ID="txtFee" runat="server" MaxLength="9" Text='<%# Eval("FEE")%>' placeholder="Enter Fee" />--%>
                                                                <asp:TextBox ID="txtFeeamount" runat="server" MaxLength="9" Text='<%# Eval("AMOUNT")%>'  placeholder="Enter Fee Amount" />
                                                           <%-- <asp:RequiredFieldValidator ID="rfvMaxMark" runat="server" ControlToValidate="txtFee"
                                                                Display="None" ErrorMessage="Please Enter  Mark" ValidationGroup="submit"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtFeeamount">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    <%--ADDED BY GAURAV END--%>


                                </div>

                                <%-- Copy Session --%>

                                <asp:Panel ID="pnlCopySession" runat="server" Height="80px" Visible="false" >

                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none " >

                                            <div class="label-dynamic">

                                                <sup>*</sup>
                                                <label>Copy To Session</label>

                                            </div>

                                            <asp:DropDownList ID="ddlCsession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCsession_SelectedIndexChanged">

                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                            </asp:DropDownList>

                                            <%--<asp:RequiredFieldValidator ID="rfvCsession" runat="server" ControlToValidate="ddlCsession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>

                                </asp:Panel>

                            </div>

                            <div class="col-12 btn-footer">

                                <div id="divnote" runat="server" visible="false">
                                <svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
                                    <symbol id="info-fill" fill="currentColor" viewBox="0 0 16 16">
                                        <path d="M8 0a8 8 0 0 0-8 8 8 8 0 1 0 16 0A8 8 0 0 0 8 0zm0 12.5a.5.5 0 0 1-.5-.5V6a.5.5 0 0 1 1 0v6a.5.5 0 0 1-.5.5zm0-9a.5.5 0 0 1 0 1A.5.5 0 0 1 7.5 5a.5.5 0 0 1 0-1A.5.5 0 0 1 8 3.5z" />
                                    </symbol>
                                </svg>

                                <div class="alert alert-danger d-flex align-items-center" role="alert" style="height: 30px; overflow: hidden; ">
                                    <svg class="bi flex-shrink-0 me-1" width="10" height="10" role="img" aria-label="Info:">
                                        <use xlink:href="#info-fill" />
                                    </svg>
                                    <div style="white-space: nowrap; overflow: hidden; width: 100%; ">  <%--animation: marquee 20s linear infinite;--%>
                                        <b>NOTE : </b>SESSION, SCHEME, EXAMTYPE, FEESTRUCTURE, DEGREE CAN NOT MODIFY, PLEASE CREATE NEW ONE
                                    </div>
                                </div>
                                    </div>






                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="Show" TabIndex="18" />

                                <asp:HiddenField ID="txtconformmessageValue" runat="server" ClientIDMode="Static" />

                                <asp:Button ID="btnCopyData" runat="server" Text="Copy To Session" CssClass="btn btn-primary" TabIndex="19" OnClick="btnCopyData_Click" Visible="false" CausesValidation="false" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Visible="false" ValidationGroup="Show" OnClientClick="return Fee();" TabIndex="20" />
                                <asp:Button ID="btnReport" runat="server" Text="Excel" CssClass="btn btn-primary" OnClick="btnReport_Click" CausesValidation="false" TabIndex="21" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="22" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="Show" />

                            </div>

                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvLoad" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="sub-heading">
                                                <h5>Fee Configuration List</h5>
                                            </div>
                                            <%--  <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">--%>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        
                                                        <th>Cancel</th>
                                                        <th>Edit</th>
                                                        <%--Delete--%>
                                                        <th>Exam Type</th>
                                                        <th>Session</th>
                                                        <th>Fees Structure</th>

                                                        <th>Degree</th>
                                                        <th>College</th>

                                                        <th>Semester</th>
                                                        <th>Fees Applicable</th>
                                                        <th>Fees</th>
                                                        <th>Processing Fees Applicable</th>
                                                        <th>Applicable Fee</th>
                                                        <th>Certificate Fees Applicable</th>
                                                        <th>Certificate Fee</th>
                                                        <th>Late Fees Applicable</th>
                                                        <th>Late Fee Amount</th>
                                                        <%-- <th>Course Type Wise</th>
                                     <th>Credit Wise</th>
                                    <th>Course Wise</th>--%>
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


                                            <%--   <td>  <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click"  /></td>--%>


                                            <td>
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/Delete.png" CommandArgument='<%# Eval("FID") %>'
                                                    AlternateText="Cancel Record" OnClientClick="javascript:ConfirmMessage();" ToolTip="Cancel Record" OnClick="btnDelete_Click" />--%>

                                                <%--<asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Images/Delete.png" CommandArgument='<%# Eval("FID") %>'
                                                    AlternateText="Cancel Record" ToolTip="Cancel Record" OnClick="btnDel_Click" />--%>

                                                <asp:ImageButton ID="del" runat="server" ImageUrl="~/Images/Delete.png" CommandArgument='<%# Eval("FID") %>'
                                                    AlternateText="Cancel Record" OnClientClick="return ConfirmMessage();" ToolTip="Cancel Record" OnClick="del_Click" />
                                            </td>
                                            <td>
                                               <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("FID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click1" TabIndex="1" ToolTip="Edit Record" />
                                            </td>
                                            <td>
                                                <asp:Label ID="examtype" runat="server" Text='<%# Eval("[Exam Type]")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="session" runat="server" Text='<%# Eval("session_name")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="feestru" runat="server" Text='<%# Eval("[Fees Structure]")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="deg" runat="server" Text='<%# Eval("DEGREE")%>'></asp:Label>
                                            </td>
                                             <td>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("COLLEGE_NAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="seme" runat="server" Text='<%# Eval("SEMESTERNAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="feeappli" runat="server" Text='<%# Eval("[Fees Applicable]")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("[FEE]")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("[Processing Fees Applicable]")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("[ApplicableFee]")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("[Certificate Fees Applicable]")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("[CertificateFee]")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLateFee" runat="server" Text='<%# Eval("LateFeesApplicable") %>'>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("LateFeeAmount") %>'>'></asp:Label>
                                            </td>

                                            <%--<td>
                                              <asp:Label ID="fee" runat="server" Text='<%# Eval("[Course Type Wise]")%>' ></asp:Label>
                                            </td>
                                             <td>
                                              <asp:Label ID="Label1" runat="server" Text='<%# Eval("[Credit Wise Fee]")%>' ></asp:Label>
                                            </td>
                                             <td>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Eval("[Course Wise Fee]")%>' ></asp:Label>
                                            </td>--%>
                                        </tr>

                                        <asp:HiddenField runat="server" ID="hfdegree" Value='<%# Eval("degreeno")%>' />
                                        <asp:HiddenField runat="server" ID="hfsession" Value='<%# Eval("sessionno")%>' />
                                        <asp:HiddenField runat="server" ID="hffeetype" Value='<%# Eval("FEETYPE")%>' />
                                        <asp:HiddenField runat="server" ID="hfcollegeid" Value='<%# Eval("college_id")%>' />
                                        <asp:HiddenField runat="server" ID="hffeestructuretype" Value='<%# Eval("FEESTRUCTURE_TYPE")%>' />
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <%--<div class="col-12 mt-3" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Fee Configuration List</h5>
                                </div>
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit</th>
                                            <th>Exam Type</th>
                                            <th>Fees Structure</th>
                                            <th>Degree</th>
                                            <th>Semester</th>
                                            <th>Fees Applicable</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><i class="fas fa-edit"></i></td>
                                            <td>Regular</td>
                                            <td>Course Type Wise</td>
                                            <td>BSC </td>
                                            <td>I, III, V, VII</td>
                                            <td>Yes</td>
                                        </tr>
                                        <tr>
                                            <td><i class="fas fa-edit"></i></td>
                                            <td>Backlog </td>
                                            <td>Course Wise</td>
                                            <td>BSC </td>
                                            <td>I, III, V, VII</td>
                                            <td>Yes</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnadd" />--%>
            <asp:PostBackTrigger ControlID="btnReport" />
            <%--<asp:AsyncPostBackTrigger ControlID="lvLoad" />--%>
        </Triggers>

    </asp:UpdatePanel>

    <script>

        function test() {

            //alert('hii')
        }

    </script>

    <script type="text/javascript">

        function clickTest() {

            if ($('#ctl00_ContentPlaceHolder1_test').is(':checked')) {

                $('#ctl00_ContentPlaceHolder1_test').prop('checked', false);
                //document.getElementById('ctl00_ContentPlaceHolder1_txtProcess').style.display = 'none'

                $('#txtProcess').addClass('d-none');
            }
            else {

                $('#ctl00_ContentPlaceHolder1_test').prop('checked', true);

                $('#txtProcess').removeClass('d-none');

                //document.getElementById('ctl00_ContentPlaceHolder1_txtProcess').style.display = 'block'
                document.getElementById('<%=txtYes.ClientID%>').value = "";
            }
        }

        function clickRdActive() {

            if ($('#ctl00_ContentPlaceHolder1_rdActive').is(':checked')) {

                $('#ctl00_ContentPlaceHolder1_rdActive').prop('checked', false);

                $('#ddlPayment').addClass('d-none');
                $("#hdFeeApplicable").attr("value", "False");
            }
            else {

                $('#ctl00_ContentPlaceHolder1_rdActive').prop('checked', true);

                $('#ddlPayment').removeClass('d-none');
                $("#hdFeeApplicable").attr("value", "True");

                $("#<%=ddlPaymentMode.ClientID%>").find('option[value="0"]').attr("selected", true);
                $("#<%=ddlPaymentMode.ClientID%>").val(0).change();
            }
        }

        // Is certificate fee

        function clickCerFee() {

            if ($('#ctl00_ContentPlaceHolder1_chkFeeCer').is(':checked')) {

                $('#ctl00_ContentPlaceHolder1_chkFeeCer').prop('checked', false);
                //document.getElementById('ctl00_ContentPlaceHolder1_txtCertiFee').style.display = 'none'

                $('#txtCertiFee').addClass('d-none');
            }
            else {

                $('#ctl00_ContentPlaceHolder1_chkFeeCer').prop('checked', true);

                $('#txtCertiFee').removeClass('d-none');

                //document.getElementById('ctl00_ContentPlaceHolder1_txtCertiFee').style.display = 'block'
                document.getElementById('<%=txtCerFee.ClientID%>').value = "";
            }
        }

        //is Late fee

        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        ////alert('123');

        //prm.add_endRequest(function () {
        //    $(function () {
        //        alert("s")
        //        $('#chkLateFee').click(function () {
        //            alert("a")
        //            clickLateFee();
        //        });
        //    });
        //});

        function Demo(val) {
            debugger;
            alert('hii1');
            $('#test').prop('checked', val);

        }

        //function LateOnOff(val) {
        //    debugger;
        //    alert('hii1111');
        //    $('#chkLateFee').prop('checked', val);

        //}

    </script>

    <script type="text/javascript">
        function onoff(val) {
            debugger;
            alert('hii2');
            $('#rdActive').prop('checked', val);


        }


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

        function Fee() {
            $('#hdFeeApplicable').val($('#rdActive').prop('checked'));
            $('#hdFeeProcessApplicable').val($('#test').prop('checked'));
            //$('#hidLatefeeChecked').val($('#chkLateFee').prop('checked'));
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    // alert("hi");
                    Fee();
                });
            });
        });


        function ConfirmMessage() {
            var selectedvalue = confirm("Do you want to Cancel Record...???");
            if (selectedvalue) {
                //alert(selectedvalue)
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
                return true;
            }
            else {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
                return false;
            }
        }



    </script>

    <%--    <script  type="text/javascript">
        function OFFON(value)
        {
            //debugger;

            $('#rdStart').prop('checked', value);

        }
    </script>--%>

    <%--<script type="text/javascript">
        function onoff(val)
        {
            debugger;
            //alert('hii2');
           // Demo(val);
            $('#rdActive').prop('checked', val);


            //$('#test').prop('checked', val);

        }

    </script>--%>

    <script>
        function clickLateFee() {
            //alert("11111");
            //if ($('#hidLatefeeChecked').val($('#chkLateFee').prop('checked'))) {
            //    document.getElementById('chkLateFee').innerHTML = '0';
            //}
            //else {
            //    document.getElementById('chkLateFee').innerHTML = '1';
            //}

            //$('#hidLatefeeChecked').val($('#chkLateFee').prop('checked'));

            //alert("123");
            if ($('#ctl00_ContentPlaceHolder1_chkLateFee').is(':checked')) {
                //debugger;
                //alert("off");
                //document.getElementById('ctl00_ContentPlaceHolder1_txtLateFee') = 1;
                $('#ctl00_ContentPlaceHolder1_chkLateFee').prop('checked', false);
                //document.getElementById('ctl00_ContentPlaceHolder1_txtLateFee').style.display = 'none';
                $('#txtLateFee').addClass('d-none');
                //document.getElementById('hidLatefeeChecked').innerHTML = '0';
                //$('#hidLatefeeChecked').val($('#chkLateFee').prop('checked'));
                $("#hidLatefeeChecked").attr("value", "False");
                //$("#<%=ddlLateFee.ClientID%>").find('option[value="0"]').attr("selected", true);
                // $("#ctl00$ContentPlaceHolder1$ddlLateFee option[value='" + 0 + "']").attr("selected", "selected");
                // $('#ctl00$ContentPlaceHolder1$ddlLateFee').change().val(0);
                //$("#hidLatefeeChecked").val("value", "False");
                //alert("4444");
                $('#DivLateFeeDate').addClass('d-none');
                $('#DivLateFeeAmount').addClass('d-none');
            }
            else {

                try {

                    $('#ctl00_ContentPlaceHolder1_chkLateFee').prop('checked', true);
                    //alert("on");
                    $('#txtLateFee').removeClass('d-none');
                    // document.getElementById('ctl00_ContentPlaceHolder1_txtLateFee').style.display = 'block';
                    //document.getElementById('txtLateFee').style.display = 'block';
                    //document.getElementById('hidLatefeeChecked').innerHTML = '1';
                    //$('#hidLatefeeChecked').val($('#chkLateFee').prop('checked'));
                    $("#hidLatefeeChecked").attr("value", "True");
                    //$("#hidLatefeeChecked").val("value", "True");
                    //alert("555");
                    //document.getElementById('< %=ddlLateFee.ClientID%>').value = '0';
                    //$('#ctl00$ContentPlaceHolder1$ddlLateFee').val(0);
                    // $("#ctl00$ContentPlaceHolder1$ddlLateFee option[value='" + 0 + "']").attr("selected", "selected");
                    //$('#ctl00$ContentPlaceHolder1$ddlLateFee').val(0).change();
                    $("#<%=ddlLateFee.ClientID%>").find('option[value="0"]').attr("selected", true);
                    $("#<%=ddlLateFee.ClientID%>").val(0).change();
                    //$("#< %=ddlLateFee.ClientID%>").text("Please Select");
                    //var Dropdown = document.getElementById("ddlLateFee");
                    //alert(Dropdown);
                    //Dropdown.selectedIndex = "0";
                    $('#DivLateFeeDate').removeClass('d-none');
                    $('#DivLateFeeAmount').removeClass('d-none');
                    document.getElementById('<%=txtLateFeeAmount.ClientID%>').value = "";
                    //alert("123");
                    //$('#txtLateFeeDate').val('')
                    //$('.txtLateFeeDate').datetimepicker('update', '');
                    //$('.txtLateFeeDate').datetimepicker('update', '2015-01-01');
                    //$("#txtLateFeeDate").data("DateTimePicker").date(null);
                    //$("#txtLateFeeDate").data("DateTimePicker").date(new Date());

                    //var myCoolDate = "27 05 1975";
                    //$("#txtLateFeeDate").data("DateTimePicker").date(myCoolDate);

                    //$('#txtLateFeeDate').datepicker('setDate', null).datepicker('fill');

                    //var datepickerObject = document.getElementById("txtLateFeeDate").ej2_instances[0];
                    //alert(datepickerObject)
                    ////Clear date 
                    //datepickerObject.value = null;

                    //$find("_Calendar1").set_selectedDate(null);
                    //$("[id*=Calendar1]").val("");
                    //$(".ajax__calendar_active").removeClass("ajax__calendar_active");
                    //return false;

                    //document.getElementById('txtLateFeeDate').value = "";
                    $('#<%= txtLateFeeDate.ClientID %>').val('');

                }
                catch (error) {
                    document.getElementById('hidLatefeeChecked').innerHTML = error;
                    //alert(error);
                    //document.getElementById('txtLateFee').style.display = 'block';
                }
            }
        }
    </script>

    <script>

        function ShowDropDown() {

            if ($('#ctl00_ContentPlaceHolder1_chkLateFee').is(':checked')) {

                //alert('hii')

                $('#txtLateFee').removeClass('d-none');
                $('#DivLateFeeDate').removeClass('d-none');
                $('#DivLateFeeAmount').removeClass('d-none');

                ShowProcessingFeeDropDown();
                ShowCertificateFeeDropDown();
                ShowPaymentModeDropDown();

                return true;
            }
            else {

                //alert('bye')

                $('#txtLateFee').addClass('d-none');
                $('#DivLateFeeDate').addClass('d-none');
                $('#DivLateFeeAmount').addClass('d-none');

                ShowProcessingFeeDropDown();
                ShowCertificateFeeDropDown();
                ShowPaymentModeDropDown();

                return true;
            }
        }

    </script>

    <script>

        function ShowProcessingFeeDropDown() {

            if ($('#ctl00_ContentPlaceHolder1_test').is(':checked')) {

                //alert('hello')
                $('#txtProcess').removeClass('d-none')

                return true;
            }
            else {

                //alert('welcome')
                $('#txtProcess').addClass('d-none');

                return true;
            }
        }

    </script>

    <script>

        function ShowCertificateFeeDropDown() {

            if ($('#ctl00_ContentPlaceHolder1_chkFeeCer').is(':checked')) {

                //alert('hello')
                $('#txtCertiFee').removeClass('d-none')

                return true;
            }
            else {

                //alert('welcome')
                $('#txtCertiFee').addClass('d-none');

                return true;
            }
        }

    </script>

    <script>

        function ShowPaymentModeDropDown() {

            if ($('#ctl00_ContentPlaceHolder1_rdActive').is(':checked')) {

                //alert('hello')
                $('#ddlPayment').removeClass('d-none')

                return true;
            }
            else {

                //alert('welcome')
                $('#ddlPayment').addClass('d-none');

                return true;
            }
        }

    </script>

</asp:Content>
