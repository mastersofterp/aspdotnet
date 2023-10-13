<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Exam_Configue.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Exam_Configue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 30px;
            height: 15px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 10px;
                width: 10px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }
        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        table, tr, td {
            padding-right: 20px;
            padding-bottom: 25px;
        }

        checkbox {
            margin-left: 500px;
        }
        /*table, td {
            padding-right:20px;
        }*/
    </style>

    <asp:UpdatePanel ID="mainpnl" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Configuration Details</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span>
                                            <label for="chk_Reg" style="font-size: small;">Exam Registration</label>
                                        </span>
                                    </div>

                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_Reg">
                                            <label class="custom-control-label" for="chk_Reg"></label>
                                            <asp:HiddenField ID="hdfexamregister" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-2 col-12 ">
                                        <span>
                                            <label for="chk_GraceRule" style="font-size: small;">Grace Rule</label>
                                        </span>

                                    </div>


                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_GraceRule">
                                            <label class="custom-control-label" for="chk_GraceRule"></label>
                                            <asp:HiddenField ID="hdfgarcerule" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <%--<asp:Panel ID="Panel1" runat="server">--%>

                                        <span class="pr-5">
                                            <label for="chk_ExamRule" style="font-size: small;">Exam Rule</label>
                                        </span>
                                    </div>


                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_ExamRule">
                                            <label class="custom-control-label" for="chk_ExamRule"></label>
                                            <asp:HiddenField ID="hdfexamrule" runat="server" ClientIDMode="Static" />

                                            <asp:Panel runat="server" ID="pnlrule" Visible="false">
                                                <div class="form-check-inline">

                                                    <asp:RadioButton ID="rdbfixed" runat="server" Text="Fixed" GroupName="ExRule" />&nbsp;
                                                                           
                                                            <asp:RadioButton ID="rdbdiff" runat="server" Text="Different" GroupName="ExRule" />
                                                </div>
                                            </asp:Panel>
                                        </div>

                                    </div>

                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="">
                                            <label for="chk_LateFee" style="font-size: small;">Late Fee</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_LateFee">
                                            <label class="custom-control-label" for="chk_LateFee"></label>
                                            <asp:HiddenField ID="hdflatefee" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chk_Improvement" style="font-size: small;">Improvement</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_Improvement">
                                            <label class="custom-control-label" for="chk_Improvement"></label>
                                            <asp:HiddenField ID="hdfImprovement" runat="server" ClientIDMode="Static" />
                                        </div>

                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="">
                                            <label for="chk_Condonation" style="font-size: small;">Condonation</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_Condonation">
                                            <label class="custom-control-label" for="chk_Condonation"></label>
                                            <asp:HiddenField ID="hdfcondonation" runat="server" ClientIDMode="Static" />

                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="">
                                            <label for="chk_ResultPublish" style="font-size: small;">ResultPublish & OTP</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_ResultPublish">
                                            <label class="custom-control-label" for="chk_ResultPublish"></label>
                                            <asp:HiddenField ID="hdfresultpublish" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="">
                                            <label for="chk_ExamPattern" style="font-size: small;">Exam Pattern</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_ExamPattern">
                                            <label class="custom-control-label" for="chk_ExamPattern"></label>
                                            <asp:HiddenField ID="hdfexampattern" runat="server" ClientIDMode="Static" />
                                        </div>

                                    </div>

                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chk_Revaluation_Process" style="font-size: small;">Revaluation Process</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_Revaluation_Process">
                                            <label class="custom-control-label" for="chk_Revaluation_Process"></label>
                                            <asp:HiddenField ID="hdfrevaluation" runat="server" ClientIDMode="Static" />
                                        </div>

                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chk_Decode" style="font-size: small;">Decode Number</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_Decode">
                                            <label class="custom-control-label" for="chk_Decode"></label>
                                            <asp:HiddenField ID="hdfdecodenos" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chk_SeatNumber" style="font-size: small;">Seat Number</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_SeatNumber">
                                            <label class="custom-control-label" for="chk_SeatNumber"></label>
                                            <asp:HiddenField ID="hdfSeatno" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chk_MarkEnrtyExcel" style="font-size: small;">Mark Entry By Excel</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_MarkEnrtyExcel">
                                            <label class="custom-control-label" for="chk_MarkEnrtyExcel"></label>
                                            <asp:HiddenField ID="hdfmarkentryexcel" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-2 col-md-2 col-122">
                                        <span class="pr-5">
                                            <label for="chk_Section" style="font-size: small;">Section Wise Time Table</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_Section">
                                            <label class="custom-control-label" for="chk_Section"></label>
                                            <asp:HiddenField ID="hdfsection" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chk_Batch" style="font-size: small;">Batch Wise Time Table</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_Batch">
                                            <label class="custom-control-label" for="chk_Batch"></label>
                                            <asp:HiddenField ID="hdfbatch" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chk_grade_faculty" style="font-size: small;">Grade Allotment From Faculty End </label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_grade_faculty">
                                            <label class="custom-control-label" for="chk_grade_faculty"></label>
                                            <asp:HiddenField ID="hdfFacgrade" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chk_grade_admin" style="font-size: small;">Grade Allotment From Admin End</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_grade_admin">
                                            <label class="custom-control-label" for="chk_grade_admin"></label>
                                            <asp:HiddenField ID="hdfadmingrade" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>


                                </div>

                                <div class="row">

                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chkGraph" style="font-size: small;">Graph On Relative Grade Page</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chkGraph">
                                            <label class="custom-control-label" for="chkGraph"></label>
                                            <asp:HiddenField ID="hdfgraph" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <span class="pr-5">
                                            <label for="chk_chgrange" style="font-size: small;">Change Grade Range</label>
                                        </span>
                                    </div>
                                    <div class="form-group  col-md-1 ">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_chgrange">
                                            <label class="custom-control-label" for="chk_chgrange"></label>
                                            <asp:HiddenField ID="hdfrange" runat="server" ClientIDMode="Static" />

                                        </div>
                                    </div>



                                    <div class="form-group col-lg-2 col-md-2 col-12 ">
                                        <span class="pr-5">
                                            <label for="chk_college" style="font-size: small;">College</label>
                                        </span>
                                    </div>

                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_college">
                                            <label class="custom-control-label" for="chk_college"></label>
                                            <asp:HiddenField ID="hdfcollege" runat="server" ClientIDMode="Static" />

                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-2 col-12 ">
                                        <span class="pr-5">
                                            <label for="chk_session" style="font-size: small;">Session</label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_session">
                                            <label class="custom-control-label" for="chk_session"></label>
                                            <asp:HiddenField ID="hdfsession" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-2 col-md-2 col-12 ">
                                        <span class="pr-5">
                                            <label for="chk_feescollection" style="font-size: small;">Academic Fees Collection</label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_feescollection">
                                            <label class="custom-control-label" for="chk_feescollection"></label>
                                            <asp:HiddenField ID="hdffeescollection" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-2 col-12 ">
                                        <span class="pr-5">
                                            <label for="chk_relative" style="font-size: small;">Relative</label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_relative">
                                            <label class="custom-control-label" for="chk_relative"></label>
                                            <asp:HiddenField ID="hdfrelative" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12 ">
                                        <span class="pr-5">
                                            <label for="chk_absolute" style="font-size: small;">Absolute</label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_absolute">
                                            <label class="custom-control-label" for="chk_absolute"></label>
                                            <asp:HiddenField ID="hdfabsolute" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-2 col-12 ">
                                        <span class="pr-5">
                                            <label for="chk_barcode" style="font-size: small;">Barcode Number</label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_barcode">
                                            <label class="custom-control-label" for="chk_barcode"></label>
                                            <asp:HiddenField ID="hdfbarcode" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-2 col-12 ">
                                        <span class="pr-5">
                                            <label for="chk_feedback" style="font-size: small;">Student Feedback</label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-1">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="chk_feedback">
                                            <label class="custom-control-label" for="chk_feedback"></label>
                                            <asp:HiddenField ID="hdffeedback" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-12">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div id="divlist">
                                            <asp:Panel ID="pnlBind" runat="server">
                                                <asp:ListView ID="lvBinddata" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <div class="sub-heading">
                                                                <h5>Subject Type</h5>
                                                            </div>
                                                            <div style="width: 100%; height: 320px; overflow: auto">
                                                                <table class="table table-striped table-bordered nowrap">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr.NO</th>
                                                                            <th>Subject ID</th>
                                                                            <th>Subject Name</th>
                                                                            <th>Internal Status</th>
                                                                            <th>External Status</th>
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
                                                            <td><%# Container.DataItemIndex +1 %></td>

                                                            <td>

                                                                <asp:Label ID="lblsubid" runat="server" Text='<%# Eval("SUBID") %>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsubname" runat="server" Text='<%# Eval("SUBNAME") %>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtinternal" runat="server" Text='<%# Eval("INTERMARKS") %>' CssClass="form-control" Width="100px"></asp:TextBox>

                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" ValidChars=".0123456789" TargetControlID="txtinternal">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <%--<asp:Label ID="lblinternal" runat="server" Text='<%# Eval("INTERSTATUS") %>'></asp:Label>--%>

                                                            <td>
                                                                <asp:TextBox ID="txtexternal" runat="server" Text='<%# Eval("EXTERMARKS") %>' CssClass="form-control" Width="100px"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars=".0123456789" TargetControlID="txtexternal"></ajaxToolKit:FilteredTextBoxExtender>

                                                                <%--<asp:Label ID="lblExternal" runat="server" Text='<%# Eval("EXTERSTATUS") %>'></asp:Label>--%>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-12 text-center mt-3">
                                <asp:Button ID="btnSave" runat="server" ToolTip="Submit" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return SetStat(this);" TabIndex="0" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="0" />

                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>

    </asp:UpdatePanel>
    <script></script>
    <script>


        $(document).ready(function () {
            debugger
            document.getElementById("<%= pnlrule.ClientID %>").style.display = "none";
        });
        function SetPnl(val) {
            var examrule = document.getElementById("chk_ExamRule");
            if (examrule.checked) {

                //$('#pnlrule').
                document.getElementById("<%= pnlrule.ClientID %>").style.display = "block";
                // $('#hdfexamrule').val(true);
            }
            else {
                document.getElementById("<%= pnlrule.ClientID %>").style.display = "none";
            }

        }

        function SetStat(val) {
            debugger

            var examreg = document.getElementById("chk_Reg");
            var examrule = document.getElementById("chk_ExamRule");
            var gracerule = document.getElementById("chk_GraceRule");
            var latefee = document.getElementById("chk_LateFee");
            var improvement = document.getElementById("chk_Improvement");
            var exampattern = document.getElementById("chk_ExamPattern");
            var revaluation = document.getElementById("chk_Revaluation_Process");
            var result = document.getElementById("chk_ResultPublish");
            var condonation = document.getElementById("chk_Condonation");
            var decode = document.getElementById("chk_Decode");
            var seatno = document.getElementById("chk_SeatNumber");
            var excel = document.getElementById("chk_MarkEnrtyExcel");
            var section = document.getElementById("chk_Section");
            var batch = document.getElementById("chk_Batch");
            var gradeadmin = document.getElementById("chk_grade_admin");
            var gradefaculty = document.getElementById("chk_grade_faculty");
            var graph = document.getElementById("chkGraph");
            var GradeRange = document.getElementById("chk_chgrange");
            var college = document.getElementById("chk_college");
            var session = document.getElementById("chk_session");
            var feescollection = document.getElementById("chk_feescollection");
            var relative = document.getElementById("chk_relative");
            var absolute = document.getElementById("chk_absolute");
            var barcode = document.getElementById("chk_barcode");
            var feedback = document.getElementById("chk_feedback");


            if (examreg.checked) {
                $('#hdfexamregister').val(true);
            }
            else {
                $('#hdfexamregister').val(false);
            }

            if (examrule.checked) {

                //$('#pnlrule').
                // document.getElementById("<%= pnlrule.ClientID %>").style.display = "block";
                $('#hdfexamrule').val(true);
            }
            else {
                $('#hdfexamrule').val(false);
            }


            if (gracerule.checked) {

                $('#hdfgarcerule').val(true);
            }
            else {
                $('#hdfgarcerule').val(false);
            }

            if (latefee.checked) {

                $('#hdflatefee').val(true);

            }
            else {
                $('#hdflatefee').val(false);
            }
            if (improvement.checked) {

                $('#hdfImprovement').val(true);

            }
            else {
                $('#hdfImprovement').val(false);
            }

            if (exampattern.checked) {

                $('#hdfexampattern').val(true);

            }
            else {
                $('#hdfexampattern').val(false);
            }

            if (revaluation.checked) {

                $('#hdfrevaluation').val(true);

            }
            else {
                $('#hdfrevaluation').val(false);
            }

            if (result.checked) {

                $('#hdfresultpublish').val(true);
            }
            else {
                $('#hdfresultpublish').val(false);
            }

            if (condonation.checked) {

                $('#hdfcondonation').val(true);

            }
            else {
                $('#hdfcondonation').val(false);
            }

            if (decode.checked) {
                $('#hdfdecodenos').val(true);
            }
            else {
                $('#hdfdecodenos').val(false);
            }


            if (gradeadmin.checked) {                      //not working
                $('#hdfadmingrade').val(true);
            }
            else {
                $('#hdfadmingrade').val(false);
            }


            if (gradefaculty.checked) {
                $('#hdfFacgrade').val(true);
            }
            else {
                $('#hdfFacgrade').val(false);
            }



            if (seatno.checked) {
                $('#hdfSeatno').val(true);
            }
            else {
                $('#hdfSeatno').val(false);
            }

            if (excel.checked) {
                $('#hdfmarkentryexcel').val(true);
            }
            else {
                $('#hdfmarkentryexcel').val(false);
            }

            if (section.checked) {
                $('#hdfsection').val(true);
            }
            else {
                $('#hdfsection').val(false);
            }

            if (batch.checked) {
                $('#hdfbatch').val(true);
            }
            else {
                $('#hdfbatch').val(false);
            }

            if (graph.checked) {
                $('#hdfgraph').val(true);
            }
            else {
                $('#hdfgraph').val(false);
            }

            if (GradeRange.checked) {
                $('#hdfrange').val(true);
            }
            else {
                $('#hdfrange').val(false);
            }

            if (college.checked) {
                $('#hdfcollege').val(true);
            }
            else {
                $('#hdfcollege').val(false);
            }

            if (session.checked) {
                $('#hdfsession').val(true);
            }
            else {
                $('#hdfsession').val(false);

            }

            if (feescollection.checked) {
                $('#hdffeescollection').val(true);
            }
            else {
                $('#hdffeescollection').val(false);

            }

            if (relative.checked) {
                $('#hdfrelative').val(true)
            }
            else {
                $('#hdfrelative').val(false);
            }

            if (absolute.checked) {
                $('#hdfabsolute').val(true)
            }
            else {
                $('#hdfabsolute').val(false);
            }

            if (barcode.checked) {
                $('#hdfbarcode').val(true)
            }
            else {
                $('#hdfbarcode').val(false);
            }

            if (feedback.checked) {
                $('#hdffeedback').val(true)
            }
            else {
                $('#hdffeedback').val(false)
            }
        }


    </script>
    <script>
        function setrule(val) {

            $('#chk_Reg').prop('checked', val);

        }
        // $('#chk_Reg').prop('checked', true);
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            window.history.replaceState('', '', window.location.href) // it prevent page refresh to firing the event again
        })

    </script>
    <%-- <script type="text/javascript">
            $(document).ready(function () {
                debugger;
                chk_Reg.val = false;
                })
            </script>--%>
</asp:Content>

