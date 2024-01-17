<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdminExamRegDec_Approval.aspx.cs" Inherits="ACADEMIC_AdminExamRegDec_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAdmit"
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
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }

        .btn-primary {
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">EXAM REGISTRATION APPROVAL</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body" id="divExamHalTckt" runat="server">
                    <asp:UpdatePanel ID="updAdmit" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" TabIndex="2" runat="server" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select Institute">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Session" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="SHOW" OnClick="btnShow_Click" TabIndex="8" ToolTip="Shows Details" ValidationGroup="show" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnsubmit" runat="server" Text="SUBMIT" OnClick="btnSubmit_Click" TabIndex="9" ToolTip="" ValidationGroup="show" CssClass="btn btn-primary" />

                                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-warning" TabIndex="11" OnClick="btncancle_Click" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:ListView ID="lvStudentRecords" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="listViewGrid" class="vista-grid">
                                            <%--<div class="titlebar">
                                                <h4>EXAM REGISTRATION STUDENT</h4>
                                            </div>--%>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" />
                                                            All
                                                        </th>

                                                        <th>Enrollment No.
                                                        
                                                        <th>Student Name
                                                        </th>

                                                            <th>Semester
                                                            </th>
                                                            <th>Course Code
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                            <th>DATE
                                                            </th>
                                                        </th>
                                                        <th>REMARK
                                                        </th>
                                                        <%--[PAY_STATUS] [PAY_MODE],TOTAL_AMT[TOTAL_AMT]--%>
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
                                            <td>
                                                <asp:CheckBox ID="chkAccept" runat="server" Checked='<%#(Convert.ToInt32(Eval("DISCIPLINE_APPROVE"))==1 ? true : false)%>' Enabled='<%#(Convert.ToInt32(Eval("EXAM_REGISTERED"))==1 ? false : true)%>' /><%--OnCheckedChanged="chkAccept_CheckedChanged"--%>  <%--Checked='<%#(Convert.ToInt32(Eval("STUD_EXAM_REGISTERED"))==1 ? true : false)%>' --%>
                                            </td>

                                            <td>
                                                <%--<asp:LinkButton ID="lnkbtnPrint" runat="server" Text='<%# Eval("REGNO") %>' CommandArgument='<%# Eval("REGNO") %>' />--%>
                                                <asp:Label ID="lblregno" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("REGNO")%>' />
                                            </td>

                                            <td>
                                                <asp:Label ID="lblstudname" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("IDNO")%>' />
                                                <%--<%# Eval("STUDNAME")%>--%>
                                            </td>
                                            <td>

                                                <asp:Label ID="lblsem" runat="server" Text='<%# Eval("SEMESTERNO")%>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                                <%--<asp:HiddenField ID="hdfsem" runat="server" Value='<%# Eval("Semesterno") %>' ToolTip='<%# Eval("Semesterno")%>'/>--%>
                                            </td>
                                            <td>
                                                <%-- <%# Eval("CCODE")%>--%>
                                                <asp:Label ID="lblccode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                <%--<asp:HiddenField ID="hdfprev_status" runat="server" Value='<%# Eval("PREV_STATUS") %>' />--%>
                                            </td>
                                            <td>
                                                <b><%# Eval("STATUS")%></b>
                                            </td>
                                            <td>
                                                <b><%# Eval("DISCIPLINE_APPROVE_DATE","{0: dd/MM/yyyy}")%></b>

                                                <%--<td><%# Eval("BirthDate", "{0: dd/MM/yyyy}")%></td>--%>
                                            </td>
                                         
                                            <td>
                                                <asp:TextBox ID="DISCIPLINE_REMARK" class='defaultText' Text='<%# Eval("DISCIPLINE_REMARK") %>' runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="search" />

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lvStudentRecords" />

                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <%--  </ContentTemplate>
       
    </asp:UpdatePanel>--%>

    <script>

        function totAllSubjects(headchk) {
            debugger;
            var sum = 0;
            var frm = document.forms[0]
            try {
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true) {
                            // SumTotal();
                            // var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                            //// alert(j);
                            // sum += parseFloat(j);
                            if (e.disabled == false) {
                                e.checked = true;
                            }
                        }
                        else {
                            if (e.disabled == false) {
                                e.checked = false;
                                headchk.checked = false;
                            }
                        }

                        // x = sum.toString();
                    }

                }
                //if (headchk.checked == true) {
                //    // SumTotal();
                //}
                //else {
                //    // SumTotal();
                //}
            }
            catch (err) {
                alert("Error : " + err.message);
            }
        }
    </script>
</asp:Content>
