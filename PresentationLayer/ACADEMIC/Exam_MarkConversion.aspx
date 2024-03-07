<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Exam_MarkConversion.aspx.cs" Inherits="ACADEMIC_Exam_MarkConversion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        element.style {
            width: auto;
            width: 1045px;
        }

        #gridrow.width {
            width: auto;
            width: 1045px;
        }
    </style>
    <script>
        $(document).ready(function () {
            var table = $('#lvStudent').DataTable({
                responsive: true,
                lengthChange: true,
                // scrollY: 320,
                // scrollX: true,
                //scrollCollapse: true,
                pagingType: 'full_numbers',
            })
        });
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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
    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <div class="box-header with-border">
                                <h3 class="box-title">Exam Wise Final Mark Conversion</h3>
                                <span id="spnPagetitle"></span>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeIdDepMap" OnSelectedIndexChanged="ddlCollegeIdDepMap_SelectedIndexChanged" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ddlcollege" runat="server" ControlToValidate="ddlCollegeIdDepMap" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College/Scheme" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollegeIdDepMap" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College/Scheme" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollegeIdDepMap" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College/Scheme" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivSubtype">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Course/Subject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsubjecttype" runat="server" TabIndex="1" AutoPostBack="true" AppendDataBoundItems="true" ToolTip="Please Select Subject Type" data-select2-enable="true" OnSelectedIndexChanged="ddlsubjecttype_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvProgram" runat="server" ControlToValidate="ddlsubjecttype"
                                            Display="None" ErrorMessage="Please Select Course/Subject" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlsubjecttype"
                                            Display="None" ErrorMessage="Please Select Course/Subject" ValidationGroup="report"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlsubjecttype"
                                            Display="None" ErrorMessage="Please Select Course/Subject" ValidationGroup="show"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamName" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfExamName" runat="server" ControlToValidate="ddlExamName" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlExamName" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlExamName" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Conversion Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlconversion" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlconversion_SelectedIndexChanged" ValidationGroup="show">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Top One</asp:ListItem>
                                            <asp:ListItem Value="2">Top Two</asp:ListItem>
                                            <asp:ListItem Value="3">Marks</asp:ListItem>
                                            <asp:ListItem Value="4">Average</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlconversion" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Conversion Type" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlconversion" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Conversion Type" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlconversion" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Conversion Type" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divmark" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtmarks" runat="server" Enabled="false" TabIndex="1" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvfmarks" ErrorMessage="Please Enter Marks" ControlToValidate="txtmarks" runat="server" ValidationGroup="submit" Display="None" SetFocusOnError="true" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltMark" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtmarks" />
                                    </div>



                                </div>
                                <div class="row">



                                    <div class="form-group col-lg-9 col-md-6 col-12">
                                        <div class="" style="margin-top: 15px">

                                            <%--<h6 class="heading">Note </h6>--%>
                                            <label style="border-left: 3px solid #2087CE">Note :</label>
                                            <p>
                                                <i class="fa fa-star" aria-hidden="true">&nbsp</i>
                                                <span><span style="color: green; font-weight: bold">(902) for Absent  </span>

                                                    <span style="color: green; margin-left: 30px; font-weight: bold">(903) for UFM </span>

                                                    <span style="color: green; margin-left: 30px; font-weight: bold">(904) for Detention </span>

                                                    <span style="color: green; margin-left: 30px; font-weight: bold">(905) for Incomplete </span>

                                                    <span style="color: green; margin-left: 30px; font-weight: bold">(906) for Withdrawl </span></span>
                                            </p>
                                        </div>

                                    </div>

                                </div>

                                <div class="row mt-9" id="DivAdd" runat="server">
                                    <div class="col-12 btn-footer" style="text-align: center">
                                        <asp:Button ID="btnShow" runat="server" Text="Show Student" ValidationGroup="show"
                                            TabIndex="1" CssClass="btn btn-outline-info" OnClick="btnShow_Click" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                            TabIndex="1" CssClass="btn btn-outline-primary" ValidationGroup="submit" OnClick="btnSubmit_Click" />

                                        <asp:Button ID="btnReport" runat="server" Text="Report"
                                            TabIndex="1" CssClass="btn btn-outline-info" ValidationGroup="report" OnClick="btnReport_Click" />

                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                            TabIndex="1" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <asp:ValidationSummary ID="valSummary" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />

                                <asp:ValidationSummary ID="valSummaryReport" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="report" />

                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="show" />
                            </div>

                            <div class="col-12 mt-3" id="divStudent" runat="server">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student Details</h5>
                                        </div>
                                        <div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblStudent">
                                                <%--<thead class="bg-light-blue" style="z-index: 0; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                         <th>REGNO</th>
                                            <th>NAME</th>
                                                        <th colspan="3" style="text-align: center">Details</th>
                                                        <th id="tbl_mark" colspan="3" style="text-align: center">Marks</th>
                                                    </tr>
                                                </thead>--%>
                                                <thead style="position: sticky; z-index: 0; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                        <th style="text-align: center;">
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAll(this)" ToolTip="Select/Select all" />Select
                                                        </th>
                                                        <th>REGNO</th>
                                                        <th>NAME</th>
                                                        <th id="th0" style="text-align: center; display: none;"></th>
                                                        <th id="th1" style="text-align: center; display: none;"></th>
                                                        <th id="th2" style="text-align: center; display: none;"></th>
                                                        <th id="th3" style="text-align: center; display: none;"></th>
                                                        <th id="th4" style="text-align: center; display: none;"></th>
                                                        <th id="th5" style="text-align: center; display: none;"></th>
                                                        <th id="th6" style="text-align: center; display: none;"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>

                                    <ItemTemplate>
                                        <tr id="trCurRow">
                                            <td style="text-align: center;">
                                                <asp:CheckBox ID="chkStudent" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO") %>' />

                                            </td>
                                            <%--   STUDNAME--%>
                                            <td>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                            </td>
                                            <td id="td0" runat="server">
                                                <asp:Label ID="lbl0" runat="server"></asp:Label>
                                            </td>
                                            <td id="td1" runat="server" style="display: none;">
                                                <asp:Label ID="lbl1" runat="server"></asp:Label>
                                            </td>
                                            <td id="td2" runat="server" style="display: none;">
                                                <asp:Label ID="lbl2" runat="server"></asp:Label>
                                            </td>
                                            <td id="td3" runat="server" style="display: none;">
                                                <asp:Label ID="lbl3" runat="server"></asp:Label>
                                            </td>
                                            <td id="td4" runat="server" style="display: none;">
                                                <asp:Label ID="lbl4" runat="server"></asp:Label>
                                            </td>
                                            <td id="td5" runat="server" style="display: none;">
                                                <asp:Label ID="lbl5" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%--<Triggers>
                        <asp:PostBackTrigger ControlID="BtnAddAssesment" />
            <asp:PostBackTrigger ControlID="btnsubmit" />
        </Triggers>--%>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnreport" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <%--  <script type="text/javascript">
        function IsNumeric(txt) {
            var ValidChars = "0123456789.";

            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }

            //var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
            //var cellIndex = id.offsetParent.cellIndex;

            //var Apllicable = 0; var Discount = 0;
            //Apllicable = document.getElementById("ctl00_ContentPlaceHolder1_lvAssessment_ctrl" + rowIndex + "_txtOutOfMarks").value;
            //alert(Apllicable);
            return num;
        }
    </script>--%>
    <script type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];

                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;

                    }
                    else {
                        e.checked = false;

                    }
                }
            }
        }
    </script>
</asp:Content>

