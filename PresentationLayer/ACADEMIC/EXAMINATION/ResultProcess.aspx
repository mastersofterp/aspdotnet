<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ResultProcess.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ResultProcess" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <style>
        .tableFixHead {
            height: 450px;
        }

            .tableFixHead thead th {
                position: sticky;
                top: 0;
            }

        /* Just common table stuff. Really.overflow-y: auto; */
        table {
            border-collapse: collapse;
            width: 180%;
        }

        th, td {
            padding: 8px 16px;
        }

        th {
            background: #758aa8;
        }
    </style>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updresult"
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
    <asp:UpdatePanel ID="updresult" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">RESULT PROCESS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Scheme</label>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="save" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            Font-Bold="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>School/Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="True" ToolTip="Please Select Institute" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcolg" runat="server" ControlToValidate="ddlColg"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"--%>
                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList><%-- AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"--%>
                                        <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--                                <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                    Display="None" ErrorMessage="Please Select Section" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Student Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudentType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentType_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Backlog</asp:ListItem>
                                            <asp:ListItem Value="2">PhotoCopy/Revaluation</asp:ListItem>
                                            <asp:ListItem Value="3">Redo/Resit</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStudentType"
                                            Display="None" ErrorMessage="Please Select Student Type" InitialValue="-1" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rbMarksEntryStatus" runat="server" CssClass="form-control" data-select2-enable="true" RepeatColumns="2" RepeatDirection="Horizontal" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="rbMarksEntryStatus_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">&nbsp;&nbsp;Single Mark Entry&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <%-- <asp:ListItem Value="2">&nbsp;&nbsp;Double Mark Entry</asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvMarksEntryStatus" runat="server" ControlToValidate="rbMarksEntryStatus"
                                            Display="None" ErrorMessage="Please Select Mark Entry Status" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <asp:ListItem Value="2">Revaluation</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please Select Exam Type" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Student</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStudent" runat="server" ControlToValidate="ddlStudent"
                                            Display="None" ErrorMessage="Please Select Student" SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>session</label>
                                        </div>
                                        <asp:RadioButtonList ID="radSelectOptions" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Selected="True" Value="0">&nbsp;&nbsp;Result Processing&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <%--Regular Result Processing--%>
                                            <%-- <asp:ListItem  Value="1">&nbsp;&nbsp;Covid 19 Result Processing</asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radSelectOptions"
                                            Display="None" ErrorMessage="Please Select Result Processing Option Regular or Covid 19" ValidationGroup="report"></asp:RequiredFieldValidator>

                                    </div>

                                </div>
                            </div>
                            <div class=" col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server"
                                    Text="Show" ValidationGroup="report" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnProcessResult" runat="server" OnClick="btnProcessResult_Click"
                                    Text="Process Result" ValidationGroup="report" CssClass="btn btn-primary" Enabled="false" />
                                <asp:Button ID="btnLock" runat="server"
                                    Text="Lock" ValidationGroup="report" CssClass="btn btn-primary" OnClientClick="return ConfirmLock();" Enabled="false" OnClick="btnLock_Click" />
                                <asp:Button ID="btnUnlock" runat="server" Visible="true"
                                    Text="UnLock" ValidationGroup="report" CssClass="btn btn-primary" ToolTip="Unlock" TabIndex="10" OnClientClick="return ConfirmUnLock();" Enabled="false" OnClick="btnUnlock_Click" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Clear"
                                    CssClass="btn btn-warning" CausesValidation="False" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                            </div>
                            <div class="col-12 mt-3" runat="server" id="divStudentRecord" visible="true">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="listViewGrid" class="tableFixHead">
                                                <div class="sub-heading">
                                                    <h5>Select Student</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Select
                                                           <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" />
                                                            </th>
                                                            <th>Reg. No.
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Date
                                                            </th>
                                                            <th>Process Status
                                                            </th>
                                                            <th>Lock Status
                                                            </th>
                                                             <th id="studcount">Serial No.
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            </div>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkStudent" runat="server" ToolTip="Select to view tabulation chart" />
                                                </td>

                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>


                                                <td>
                                                    <asp:Label ID="lblStudname" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <%# Eval("RESULTDATE")%>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblPstatus" Font-Bold="true" Text='<%# (Eval("PROCESS_STATUS"))%>' ToolTip='<%# (Eval("PSTATUS"))%>' ForeColor='<%# (Convert.ToInt32(Eval("PSTATUS") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblLockstatus" Font-Bold="true" ToolTip='<%# (Eval("LOCK"))%>' Text='<%# (Eval("LOCK_STATUS"))%>' ForeColor='<%# (Convert.ToInt32(Eval("LOCK") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                </td>
                                                <td id="stdcounts">
                                                    <asp:Label ID="lblCount" runat="server" Text='<%# Eval("GRADE_SRNO")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 mt-3" runat="server">
                                <asp:ListView ID="lvCourse" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="listViewGrid">
                                            <div id="divmark" runat="server" class="sub-heading">
                                                <h5>Select Student</h5>
                                            </div>
                                            <%--<div class="box-body well well-sm" style="margin-right: 0px; margin-left: 0px; margin-top: -16px; margin-bottom: -38px;">
                                                <div class="dataTables_header clearfix">
                                                    <div class="">
                                                        <div id="table3" class="dataTables_length" runat="server">
                                                            <label>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Course Name
                                                        </th>
                                                        <th>Exam
                                                        </th>
                                                        <%--<th>Faculty Name
                                                    </th>--%>
                                                        <th>
                                                            <asp:Label ID="lblFacName" runat="server"></asp:Label>
                                                        </th>
                                                        <th>Contact No.
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>

                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr>

                                            <td>
                                                <%# Eval("COURSENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("EXAM")%>
                                            </td>
                                            <td>
                                                <%# Eval("UA_FULLNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("UA_MOBILE")%>
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
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnProcessResult" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
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
    <script language="javascript" type="text/javascript">
        function ConfirmLock() {
            var ret = confirm('You can not reprocess the result after lock are you sure?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>

