<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Rajagiri_End_SemMark.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Rajagiri_End_SemMark" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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

    <%--<script src="<%=Page.ResolveClientUrl("~/JAVASCRIPTS/RajagiriEndSemMark.js")%>"> UpdateMode="Conditional"  ChildrenAsTriggers="true"</script>--%>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>

            <%--<style>
                table#ctl00_ContentPlaceHolder1_gvStudent_ctl02_cecomTutMarks_popupTable {
                    left: 980px !important;
                    top: 85px !important;
                }
            </style>--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDYtxtEndSemMark" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlMarkEntry" runat="server">

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-7 col-md-12 col-12 form-group">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>College/Scheme</label>--%>
                                                        <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlcollege" TabIndex="1" runat="server" CssClass="form-control" AppendDataBoundItems="true" ToolTip="Please Select Institute" data-select2-enable="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlcollege"
                                                        Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>


                                                    <asp:HiddenField runat="server" ID="hdfschemno" />
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label 	>Session</label>--%>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" TabIndex="2" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="col-lg-2 col-md-6 col-12 form-group pl-m-lg-0 pr-lg-0">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label	>Semester</label>--%>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlsemester" TabIndex="3" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvsemester" runat="server" ControlToValidate="ddlsemester"
                                                        Display="None" ErrorMessage="Please Select Semester." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-6 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Course Name</label>--%>
                                                        <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                        CssClass="form-control" AutoPostBack="True" TabIndex="4" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                                        Display="None" ErrorMessage="Please Select Course Name." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="form-group col-lg-5 col-md-6 col-12">
                                            <div class="note-div">
                                                <h5 class="heading">Note </h5>
                                                <%-- <p><i class="fa fa-star" aria-hidden="true"></i><span>Session is mandatory for generating Marks Entry Report</span>  </p>--%>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>You won't be able to modify the MARKS after it's been locked.</span></p>
                                                <p>
                                                    <asp:Label runat="server" ID="lbltxtmax" Visible="false">
                                                         <i class="fa fa-star" aria-hidden="true"></i>
                                                         The maximum mark for the selected course is: </asp:Label>
                                                    <b>
                                                        <asp:Label ID="lblmax" runat="server" Visible="false" />

                                                    </b>
                                                    <asp:HiddenField ID="hdfmaxMark" runat="server" />
                                                    <asp:HiddenField ID="hdnUserno" runat="server" />
                                                    <asp:HiddenField ID="hdfIpAdd" runat="server" />

                                                </p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Save and Lock for Final Submission.</span>  </p>
                                                <p>
                                                    <i class="fa fa-star" aria-hidden="true"></i><span><span style="color: green; font-weight: bold">(902) for Absent</span>
                                                        <span style="color: green; margin-left: 30px; font-weight: bold">(903) for UFM </span>
                                                        <br />
                                                </p>

                                            </div>
                                        </div>


                                        <%--<div  class="col-lg-3">
                                            <div class="p-4">
                                           <%-- <asp:Label runat="server" ID="lbltxtmax" Visible="false"><b>Max Mark Of Selected Course:</b> </asp:Label> 
                                               <b> <asp:Label ID="lblmax" runat="server" /></b>
                                                <asp:HiddenField ID="hdfmaxMark" runat="server" />--%
                                                </div>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnShow" runat="server" TabIndex="5"
                                        Text="Show Student" ValidationGroup="show"
                                        CssClass="btn btn-info" OnClick="btnShow_Click" />
                                    <button type="button" class="btn btn-primary" id="btnSubmit" causesvalidation="false" runat="server" tabindex="6" disabled>Save</button>
                                    <button type="button" class="btn btn-primary" id="btnLock" causesvalidation="false" tabindex="8" runat="server" disabled>Save & Lock</button>
                                    <button type="button" class="btn btn-primary" id="btnUnLock" runat="server" tabindex="9" style="display: none;">UnLock</button>

                                    <%--<asp:Button ID="btnUnlock" runat="server" TabIndex="9" 
                                        Text="Unlock" CssClass="btn btn-primary" OnClientClick="unlockScore();" Visible="true"/>--%>

                                    <asp:Button ID="btnReport" runat="server" TabIndex="10"
                                        Text="Report"
                                        CssClass="btn btn-info" ValidationGroup="show" OnClick="btnReport_Click" />

                                    <button type="button" class="btn btn-primary" id="btnfinal" causesvalidation="false" runat="server" tabindex="11" style="display: none;">Final Mark Submission</button>

                                    <asp:Button ID="btnCancel" runat="server" TabIndex="12" Font-Bold="true" CausesValidation="false"
                                        Text="Cancel" CssClass="btn btn-warning" ValidationGroup="val" OnClick="btnCancel_Click" />


                                    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="show" DisplayMode="List"
                                        ShowSummary="false" ShowMessageBox="true" />

                                </div>
                                </span>
                            </asp:Panel>

                            <div class="col-12 mt-3" id="divStudent" runat="server">
                                <asp:ListView ID="lvScore" runat="server" Style="display: none;">
                                    <LayoutTemplate>

                                        <div class="sub-heading">
                                            <%--<asp:Label ID="lblDYtxtStudentMark" runat="server" Font-Bold="true" ></asp:Label>--%>
                                            <h5>Student Mark</h5>
                                        </div>
                                        <%--  --%>
                                        <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblScore">
                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <%--                                                    <tr>
                                                        
                                                        <th colspan="4" style="text-align:center">Score</th>

                                                        <th id="tbl_Grace" colspan="2" style="text-align:center">Grace Score</th>
                                                    </tr>--%>

                                                    <tr>
                                                        <th>Sr.no</th>
                                                        <th>PRN No</th>
                                                        <th>False No</th>
                                                        <%--<th>NAME</th>--%>
                                                        <th>Score 1</th>
                                                        <th>Score 2</th>
                                                        <th>Score 3</th>
                                                        <th>Grace Mark for events</th>
                                                        <th>Grace Mark for Disability</th>
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
                                            <td>
                                                <asp:Label ID="lblsrno" runat="server" Text='<%# Eval("Column1") %>'></asp:Label>
                                            </td>

                                            <td>

                                                <asp:Label ID="lblRegno" runat="server" ToolTip='<%# Eval("IDNO") %>' Text='<%# Eval("REGNO") %>' />
                                                <input type="hidden" id="hdfIdnos" name="hdfIdnos" value='<%# Eval("IDNO") %>' />
                                                <input type="hidden" id="hdfL1" name="hdfL1" value='<%# Eval("LOCKEM1")%>' />
                                                <input type="hidden" id="hdfL2" name="hdfL2" value='<%# Eval("LOCKEM2") %>' />
                                                <input type="hidden" id="hdfL3" name="hdfL3" value='<%# Eval("LOCKEM3") %>' />


                                            </td>
                                            <td>
                                                <asp:Label ID="lblFalseno" runat="server" Text='<%# Eval("SEATNO") %>' ></asp:Label>
                                            </td>
                                            <%--<td><asp:Label ID="lblName" runat="server" Text='<%# Eval("studname") %>' /></td>--%>
                                            <td>
                                                <%--<asp:TextBox ID="txtscore1" runat="server"></asp:TextBox>--%>
                                                <input type="text" id="txtscore1" class="marksOnly" value='<%# Eval("EXTERMARK1").ToString()==""?"": Eval("EXTERMARK1")%>' maxlength="5" />
                                                <span id="spanSC1" style="display: none; color: red;">Enter Score 1</span>

                                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="fltscore1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtscore1"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                               
                                            </td>
                                            <td>
                                                <input type="text" id="txtscore2" class="marksOnly statS2" data-toggle="tooltip" data-placement="top" value='<%# Eval("EXTERMARK2").ToString()==""?"": Eval("EXTERMARK2")%>' maxlength="5" disabled />
                                                <span id="spanSC2" style="display: none; color: red;">Enter Score 2 For 20% Of Student</span>
                                                <%--<asp:TextBox ID="txtscore2" runat="server" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtscore2"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                            </td>
                                            <td>
                                                <input type="text" id="txtscore3" class="marksOnly" value='<%# Eval("EXTERMARK3").ToString()==""?"": Eval("EXTERMARK3")%>' maxlength="5" disabled />
                                                <span id="spanSC3" style="display: none; color: red;">Enter Score 3</span>

                                            </td>
                                            <td>
                                                <input type="text" id="txtGME" class="grace" maxlength="2" />
                                            </td>
                                            <td>
                                                <input type="text" id="txtGMD" class="grace" maxlength="2" />
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <%--<asp:PostBackTrigger ControlID="btnExcelReport" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>

    <script>
        $(document).ready(function () {
            //allowNums is an array of Numbers that are allowed to be entered e.g. 903 means - Un Fair Means
            var allowedNums = [902, 903];
            //Click Event of Save Button
            $('#ctl00_ContentPlaceHolder1_btnSubmit').click(function () {
                // alert('hi');
                var mode; // will store the mode used in procedure to perform certain action
                var dataTable = '[{';

                var maxStdCount = 0; //total count of students
                var marksEnterCount = 0;//total count of students having score 2
                var retStat = true;
                var maxMark = $('#ctl00_ContentPlaceHolder1_hdfmaxMark').val();

                var locks1; //lock Status of Score 1
                var locks2; //lock Status of Score 2
                var locks3; //lock Status of Score 3

                $('#tblScore tbody tr').each(function () {
                    maxStdCount += 1;
                    var IDNO = $(this).find($('[id^=hdfIdnos]')).val();
                    // debugger;

                    locks1 = $(this).find('[id^=hdfL1]').val();
                    locks2 = $(this).find('[id^=hdfL2]').val();
                    locks3 = $(this).find('[id^=hdfL3]').val();

                    if (locks1 == "False") {
                        var score1 = $(this).find('#txtscore1').val();
                        mode = "EXTER1_MARK_ENTRY";
                        //  if (score1 != "") {
                        //  debugger; Number(score1) != 902 && Number(score1) != 903
                        if (!allowedNums.includes(Number(score1)) && Number(score1) > Number(maxMark)) {
                            alert("The score should not exceed the maximum marks!");
                            $(this).find("td").each(function () {
                                $(this).find('#txtscore1').css({ 'border-color': 'red' });
                                $(this).find('#txtscore1').focus();
                            })
                            retStat = false;
                            return false;
                        }
                        else {
                            score1 = score1 == "." ? "" : score1;
                            if (dataTable == '[{') {
                                dataTable += '"IDNO": "' + IDNO + '","EXTERMARKS": "' + score1 + '"}'
                            }
                            else {
                                dataTable += ',{"IDNO": "' + IDNO + '","EXTERMARKS": "' + score1 + '"}'
                            }

                            $(this).find("td").each(function () {
                                $(this).find('#txtscore1').css({ 'border-color': '' });
                                $(this).find("#spanSC1").css({ 'display': 'none' });
                            })
                            retStat = true;
                        }
                        //}
                    }
                    else if (locks1 == "True" && locks2 == "False") {
                        var score2 = $(this).find('#txtscore2').val();
                        mode = "EXTER2_MARK_ENTRY";
                        //  debugger;Number(score2) != 902 && Number(score2) != 903 &&
                        if (Number(score2) > Number(maxMark)) {
                            alert("The score should not exceed the maximum marks!");
                            $(this).find("td").each(function () {
                                $(this).find('#txtscore2').css({ 'border-color': 'red' });
                                $(this).find('#txtscore2').focus();
                            })
                            retStat = false;
                            return false;
                        }
                        else {
                            if ((score2 != "" && score2 != ".")) {
                                marksEnterCount += 1;
                            }
                            score2 = score2 == "." ? "" : score2;
                            debugger;
                            if (!allowedNums.includes(Number($(this).find('#txtscore1').val()))) {
                                if (dataTable == '[{') {
                                    dataTable += '"IDNO": "' + IDNO + '","EXTERMARKS": "' + score2 + '"}'
                                }
                                else {
                                    dataTable += ',{"IDNO": "' + IDNO + '","EXTERMARKS": "' + score2 + '"}'
                                }
                            }


                            $(this).find("td").each(function () {
                                $(this).find('#txtscore2').css({ 'border-color': '' });
                                $(this).find("#spanSC2").css({ 'display': 'none' });
                            })
                            retStat = true;
                        }
                    }
                    else if (locks1 == "True" && locks2 == "True" && locks3 == "False") {
                        var score3 = $(this).find('#txtscore3').val();
                        mode = "EXTER3_MARK_ENTRY";
                        // if (score3 != "") {
                        if (allowedNums.includes(Number($(this).find('#txtscore1').val())) && !allowedNums.includes(Number(score3))) {
                            debugger;
                            //alert("This Student Score is " + Number($(this).find('#txtscore1').val()) + " in Score 1");
                            $(this).find('#txtscore3').css({ 'border-color': 'red' });
                            $(this).find('#txtscore3').val($(this).find('#txtscore1').val());
                            $(this).find('#txtscore3').prop('disabled', true);
                            ret = false;
                            return;
                        }
                        else if (!allowedNums.includes(Number(score3)) && Number(score3) > Number(maxMark)) {
                            alert("The score should not exceed the maximum marks!");
                            $(this).find("td").each(function () {
                                $(this).find('#txtscore3').css({ 'border-color': 'red' });
                                $(this).find('#txtscore3').focus();
                            })
                            retStat = false;
                            return false;
                        }
                        else {
                            score3 = score3 == "." ? "" : score3;
                            if (dataTable == '[{') {
                                dataTable += '"IDNO": "' + IDNO + '","EXTERMARKS": "' + score3 + '"}'
                            }
                            else {
                                dataTable += ',{"IDNO": "' + IDNO + '","EXTERMARKS": "' + score3 + '"}'
                            }
                            $(this).find('#txtscore3').css({ 'border-color': '' });
                            $(this).find("#spanSC3").css({ 'display': 'none' });
                            retStat = true;
                        }
                        //}
                    }

                })
                // debugger;
                if (retStat != false) {
                    if (dataTable == '[{') {
                        alert("Please Enter Score")
                        return false;
                    }
                    else {
                          debugger;
                        //here calculated 20% of the students,if marks is entered for 20% students the savedata else alert message
                        if (locks1 == "True" && locks2 == "False") {
                            var per = Math.round((maxStdCount * 20) / 100);
                            if (marksEnterCount <= per) {
                                dataTable = dataTable + ']';
                                savedata(dataTable, mode);

                            }
                            else {
                                alert("To lock Score 2 mark entry,\n the mark entry for 20% of the students (i.e. " + per + " student(s))\n must be completed & you have entered score of " + marksEnterCount + " Student(s)");
                                return false;
                            }
                        }
                        else {
                            dataTable = dataTable + ']';
                            savedata(dataTable, mode);
                        }
                    }
                }

            });

            function savedata(dataTable, mode) {
                debugger
                var datatable = JSON.parse(dataTable);

                var obj = {};

                obj.datatable = JSON.stringify(datatable);
                obj.schemeno = $("#ctl00_ContentPlaceHolder1_hdfschemno").val();
                obj.sessionno = $("#ctl00_ContentPlaceHolder1_ddlSession").val();
                obj.semesterno = $("#ctl00_ContentPlaceHolder1_ddlsemester").val();
                obj.courseno = $("#ctl00_ContentPlaceHolder1_ddlCourse").val();
                obj.userno = $("#ctl00_ContentPlaceHolder1_hdnUserno").val();
                obj.ipaddress = $('#ctl00_ContentPlaceHolder1_hdfIpAdd').val();
                obj.mode = mode;
                //url: "Rajagiri_End_SemMark.aspx/SaveMarks",//?datatable=" + JSON.stringify(datatable),

                $.ajax({
                    type: "POST",
                    url: "<%=Page.ResolveClientUrl("Rajagiri_End_SemMark.aspx/SaveMarks")%>",
                    data: JSON.stringify(obj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var data = response.d;
                        // alert(names);
                        CallBack(data);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
            function handleSuccess(message) {
                alert(message);
                $('#ctl00_ContentPlaceHolder1_btnShow').trigger('click');
            }

            function CallBack(data) {
                var value = parseInt(data);
                switch (value) {
                    case 1:
                        handleSuccess('Score 1 Saved Successfully !');
                        break;
                    case 11:
                        handleSuccess('Score 1 Saved & Locked Successfully !');
                        break;
                    case 10:
                        handleSuccess('Score 1 Unlocked Successfully!');
                        break;
                    case 2:
                        console.log('score 2 hit');
                        handleSuccess('Score 2 Saved Successfully !');
                        break;
                    case 21:
                        console.log('score 2 hit');
                        handleSuccess('Score 2 Saved & Locked Successfully !');
                        break;
                    case 20:
                        handleSuccess('Score 2 Unlocked Successfully !');
                        break;
                    case 3:
                        handleSuccess('Score 3 Saved Successfully !');
                        break;
                    case 31:
                        handleSuccess('Score 3 Saved & Locked Successfully !');
                        break;
                    case 30:
                        handleSuccess('Score 3 Unlocked Successfully !');
                        break;
                    case 55:
                        handleSuccess('Final Mark Submission Successful');
                        break;
                    default:
                        alert('Record Not Saved,Please Try Again.', 'Warning!');
                        break;
                }
            }

            //Click Event of Lock Button
            $('#ctl00_ContentPlaceHolder1_btnLock').click(function () {
                debugger
                var ret = confirm('Are you sure, do you really want to lock entered mark of students.?\n Once locked it cannot be modified.');
                if (ret == true) {
                    var mode;
                    var dataTable = '[{';
                    var maxStdCount1 = 0;
                    var marksEnterCount1 = 0;
                    var lockS1;
                    var lockS2;
                    var lockS3;
                    var ret;
                    var maxMark1 = $('#ctl00_ContentPlaceHolder1_hdfmaxMark').val();
                    $('#tblScore tbody tr').each(function () {
                        maxStdCount1 += 1;
                        //debugger;
                        lockS1 = $(this).find('[id^=hdfL1]').val();
                        lockS2 = $(this).find('[id^=hdfL2]').val();
                        lockS3 = $(this).find('[id^=hdfL3]').val();

                        $(this).find("td").each(function () {
                            if (lockS1 == "False") {
                                if ($(this).find('#txtscore1').val() == "" || $(this).find('#txtscore1').val() == ".") {
                                    //alert("Please Enter Score 1");
                                    $(this).find('#txtscore1').css({ 'border-color': 'red' });
                                    $(this).find('#txtscore1').focus();
                                    $(this).find("#spanSC1").css({ 'display': 'block' });
                                    return false;
                                }
                                else {
                                    $(this).find('#txtscore1').css({ 'border-color': '' });
                                    $(this).find("#spanSC1").css({ 'display': 'none' });

                                }
                            }
                            else if ((lockS2 == "True") && (lockS3 == "False")) {
                                if ($(this).find('#txtscore3').val() == "" || $(this).find('#txtscore3').val() == ".") {
                                    //alert("Please Enter Score 1");
                                    $(this).find('#txtscore3').css({ 'border-color': 'red' });
                                    $(this).find('#txtscore3').focus();
                                    $(this).find("#spanSC3").css({ 'display': 'block' });
                                    return false;
                                }
                                else {
                                    $(this).find('#txtscore3').css({ 'border-color': '' });
                                    $(this).find("#spanSC3").css({ 'display': 'none' });

                                }
                            }
                        })

                        var IDNO = $(this).find($('[id^=hdfIdnos]')).val();
                        if (lockS1 == "False") {
                            var score1 = $(this).find('#txtscore1').val();
                            mode = "EXTER1_MARK_LOCK";
                            if (!allowedNums.includes(Number(score1)) && Number(score1) > Number(maxMark1)) {
                                alert("The score should not exceed the maximum marks!");
                                $(this).find("td").each(function () {
                                    $(this).find('#txtscore1').css({ 'border-color': 'red' });
                                    $(this).find('#txtscore1').focus();
                                })
                                ret = false;
                                return false;
                            }
                            else {
                                if (score1 != "") {

                                    if (dataTable == '[{') {
                                        dataTable += '"IDNO": "' + IDNO + '","EXTERMARKS": "' + score1 + '"}'
                                    }
                                    else {
                                        dataTable += ',{"IDNO": "' + IDNO + '","EXTERMARKS": "' + score1 + '"}'
                                    }
                                    ret = true;
                                }
                                else {
                                    alert("Enter Score 1 of all Students  to Lock!");
                                    ret = false;
                                    return false;
                                }
                            }

                        }
                        else if (lockS1 == "True" && lockS2 == "False") {
                            var score2 = $(this).find('#txtscore2').val();
                            mode = "EXTER2_MARK_LOCK";//!allowedNums.includes(Number(score2))&&
                            if (Number(score2) > Number(maxMark1)) {
                                alert("The score should not exceed the maximum marks!");
                                $(this).find("td").each(function () {
                                    $(this).find('#txtscore2').css({ 'border-color': 'red' });
                                    $(this).find('#txtscore2').focus();
                                })
                                ret = false;
                                return false;
                            }
                            else {

                                if (score2 == ".") {
                                    alert("Enter Score Properly!");
                                    $(this).find('#txtscore2').css({ 'border-color': 'red' });
                                    $(this).find('#txtscore2').focus();
                                    ret = false;
                                }
                                else {
                                    if (allowedNums.includes(Number($(this).find('#txtscore1').val())) && score2 != "") {
                                        debugger;
                                        //alert("This Student Score is " + Number($(this).find('#txtscore1').val()) + " in Score 1");
                                        $(this).find('#txtscore2').css({ 'border-color': 'red' });
                                        $(this).find('#txtscore2').val("");
                                        score2 = "";
                                        $(this).find('#txtscore2').prop('disabled', true);
                                        //ret = false;
                                        // return;
                                    }
                                    if (score2 != "") {
                                        marksEnterCount1 += 1;
                                    }

                                    if (dataTable == '[{') {
                                        dataTable += '"IDNO": "' + IDNO + '","EXTERMARKS": "' + score2 + '"}'
                                    }
                                    else {
                                        dataTable += ',{"IDNO": "' + IDNO + '","EXTERMARKS": "' + score2 + '"}'
                                    }

                                    ret = true;
                                }
                            }

                        }
                        else if (lockS1 == "True" && lockS2 == "True" && lockS3 == "False") {

                            var score3 = $(this).find('#txtscore3').val();
                            mode = "EXTER3_MARK_LOCK";
                            //Number(score3) != 902 && Number(score3) != 903 &&
                            if (!allowedNums.includes(Number(score3)) && Number(score3) > Number(maxMark1)) {
                                alert("The score should not exceed the maximum marks!");
                                $(this).find("td").each(function () {
                                    $(this).find('#txtscore3').css({ 'border-color': 'red' });
                                    $(this).find('#txtscore3').focus();
                                })
                                ret = false;
                                return false;
                            }
                            else {
                                if (allowedNums.includes(Number($(this).find('#txtscore1').val())) && !allowedNums.includes(Number(score3))) {
                                    debugger;
                                    //alert("This Student Score is " + Number($(this).find('#txtscore1').val()) + " in Score 1");
                                    $(this).find('#txtscore3').css({ 'border-color': 'red' });
                                    $(this).find('#txtscore3').val($(this).find('#txtscore1').val());
                                    score3 = $(this).find('#txtscore1').val();
                                    $(this).find('#txtscore3').prop('disabled', true);
                                }
                                if (score3 != "") {

                                    if (dataTable == '[{') {
                                        dataTable += '"IDNO": "' + IDNO + '","EXTERMARKS": "' + score3 + '"}'
                                    }
                                    else {
                                        dataTable += ',{"IDNO": "' + IDNO + '","EXTERMARKS": "' + score3 + '"}'

                                    }
                                    ret = true;
                                }
                                else {
                                    alert("Enter Score 3 of all Students  to Lock!");
                                    ret = false;
                                    return false;
                                }
                            }
                        }

                    })// tbody tr loop
                    //debugger;
                    //dataTable = dataTable + ']';
                    //savedata(dataTable, mode);
                    if (ret != false) {
                        if (lockS1 == "True" && lockS2 == "False") {
                            var per1 = Math.round((maxStdCount1 * 20) / 100);

                            if (marksEnterCount1 == per1) {
                                dataTable = dataTable + ']';
                                savedata(dataTable, mode);
                                return;
                            }
                            else {
                                alert("To lock Score 2 mark entry,\n the mark entry for 20% of the students (i.e. " + per1 + " student(s))\n must be completed & you have entered score of " + marksEnterCount1 + " Student(s)");
                                return false;

                            }
                        }
                        else
                            dataTable = dataTable + ']';
                        savedata(dataTable, mode);
                    }
                }
            });

            //Clieck Event of Unlock Button
            $('#ctl00_ContentPlaceHolder1_btnUnLock').click(function () {
                unlockScore();
            });
            function unlockScore() {
                var ret = confirm('Do you really want to Unlock entered marks of students.?');
                if (ret == true) {
                    var ls1, ls2, ls3;
                    var IDNO;
                    var modeUnlock;
                    var dataTable = '[{';
                    $('#tblScore tbody tr').each(function () {

                        var lockStat = $(this).find('[id^=hdfL1],[id^=hdfL2],[id^=hdfL3],[id^=hdfIdnos]');
                        ls1 = lockStat.filter('[id^=hdfL1]').val();
                        ls2 = lockStat.filter('[id^=hdfL2]').val();
                        ls3 = lockStat.filter('[id^=hdfL3]').val();
                        IDNO = lockStat.filter('[id^=hdfIdnos]').val();

                        if (dataTable == '[{') {
                            dataTable += '"IDNO": "' + IDNO + '","EXTERMARKS": "' + "" + '"}'
                        }
                        else {
                            dataTable += ',{"IDNO": "' + IDNO + '","EXTERMARKS": "' + "" + '"}'
                        }

                        if (ls1 == 'True' && ls2 == 'False' && ls3 == 'False') {
                            modeUnlock = 'EXTER1_MARK_UNLOCK';
                        }
                        else if (ls1 == 'True' && ls2 == 'True' && ls3 == 'False')
                        {
                            modeUnlock = 'EXTER2_MARK_UNLOCK';
                        }
                        else if (ls1 == 'True' && ls2 == 'True' && ls3 == 'True')
                        {
                            modeUnlock = 'EXTER3_MARK_UNLOCK';
                        }

                    });
                    debugger;
                    dataTable = dataTable + ']';
                    savedata(dataTable, modeUnlock);

                }

            }

            //Clieck Event of  Final Mark Submission
            $('#ctl00_ContentPlaceHolder1_btnfinal').click(function () {
                var mode;
                var dataTable = '[{';
                $('#tblScore tbody tr').each(function () {

                    var IDNO = $(this).find($('[id^=hdfIdnos]')).val();
                    var locks1 = $(this).find('[id^=hdfL1]').val();
                    var locks2 = $(this).find('[id^=hdfL2]').val();


                    if ((locks1 == "True") && (locks2 == "True")) {
                        //var score1 = $(this).find('#txtscore1').val();
                        mode = "FINAL_MARK_ENTRY";
                        // if (score1 != "") {

                        if (dataTable == '[{') {
                            dataTable += '"IDNO": "' + IDNO + '","EXTERMARKS": "' + "" + '"}'
                        }
                        else {
                            dataTable += ',{"IDNO": "' + IDNO + '","EXTERMARKS": "' + "" + '"}'
                        }
                        //}
                    }
                    else {
                        return;
                    }
                })
                debugger;
                dataTable = dataTable + ']';
                savedata(dataTable, mode);
            });

        });

    </script>
    <script>
        //$('#ctl00_ContentPlaceHolder1_btnUnlock').click(function () {
        //    unlockScore();
        //});

    </script>

    <script>
        //Validation for text box to accept only numbers allowedNums
        $(document).ready(function () {
            var maxMark = $('#ctl00_ContentPlaceHolder1_hdfmaxMark').val();
            var markEnterCount = 0;
            $('.marksOnly').on('keypress paste', function (event) {
                var allowedNums = [902, 903];
                if (!$.isNumeric(event.which)) {
                    event.preventDefault();
                }
                if (event.which === 46 && $(this).val().indexOf('.') !== -1) {
                    event.preventDefault();
                } else if (!$.isNumeric(String.fromCharCode(event.which)) && event.which !== 46) {
                    event.preventDefault();
                } else {
                    var input = $(this).val() + String.fromCharCode(event.which);
                    if (Number(input) > Number(100) && !allowedNums.includes(Number(input))) {
                        event.preventDefault();
                    }
                }
            });
            $('.grace').on('keypress', function (event) {
                // Check if the text box contains more than two characters
                if (!$.isNumeric(event.which)) {
                    event.preventDefault();
                }
                if (event.which === 46) {
                    event.preventDefault();
                } else if (!$.isNumeric(String.fromCharCode(event.which)) && event.which !== 46) {
                    event.preventDefault();
                } else {
                    var input = $(this).val() + String.fromCharCode(event.which);
                    if (Number(input) > Number(100)) {
                        event.preventDefault();
                    }

                }
            });
        });
    </script>

    <script>

        $('.statS2').keyup(function () {
            toottipScoreTwo();
        })
        //to show tatal marks,Marks Enter Count,20% of total student on tooltip
        function toottipScoreTwo() {
            var totalCount = 0;
            var markDone = 0;
            $('#tblScore tbody tr').each(function () {
                totalCount += 1;
            })
            $('#tblScore tbody tr').each(function () {
                if ($(this).find('#txtscore2').val() != "") {
                    markDone += 1;
                }
            });
            var per = Math.round((totalCount * 20) / 100);
            $('#tblScore tbody tr').each(function () {
                $(this).find('#txtscore2').attr('title', 'Total: ' + totalCount + ' | Entry Done: ' + markDone + ' | 20%: ' + per);
            })
        }
    </script>
</asp:Content>

