<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="BulkOperatorallotmentPG.aspx.cs" Inherits="ACADEMIC_BulkOperatorallotmentPG" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upddetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>

    <asp:Panel ID="pnlMain" runat="server">
        <asp:UpdatePanel ID="upddetails" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">EXAM MARK ENTRY OPERATOR ALLOCATION FOR END SEM</h3>
                            </div>

                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <div class="form-group col-md-6">
                                            <label>Session</label>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" Font-Bold="True">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>College /School Name </label>
                                            <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Degree</label>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Branch</label>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Scheme</label>
                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                                ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Semester</label>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Subject Type</label>
                                            <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" TabIndex="7"
                                                ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged1">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Subject Type" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6" id="TRSubSujectType" runat="server" visible="false">
                                            <label>Theory/Practical</label>
                                            <asp:DropDownList ID="DDLSubSubjectType" runat="server" AppendDataBoundItems="true" TabIndex="7"
                                                ValidationGroup="teacherallot">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Theory</asp:ListItem>
                                                <asp:ListItem Value="2">Practical</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-6" style="display: none">
                                            <label>Section</label>
                                            <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" TabIndex="9" ValidationGroup="teacherallot">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-6" id="TROperator" runat="server" visible="false">
                                            <label>Select Operator</label>
                                            <asp:DropDownList ID="ddlOprator" runat="server" AppendDataBoundItems="true" TabIndex="11" ValidationGroup="teacherallot">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Operator 1</asp:ListItem>
                                                <asp:ListItem Value="2">Operator 2</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlOprator"
                                                Display="None" ErrorMessage="Please Select Operator" InitialValue="0" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6" id="TROverwrite" runat="server" visible="false" style="margin-top: 25px">
                                            <asp:CheckBox runat="server" ID="chkover" Text="Overwrite exist one" TabIndex="12" />
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>User Type</label>
                                            <asp:DropDownList ID="ddlTeachertype" runat="server" TabIndex="13" AppendDataBoundItems="true"
                                                ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlTeachertype_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTeachertype"
                                                Display="None" ErrorMessage="Please Select User Type" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Operator</label>
                                            <asp:DropDownList ID="ddlTeacher" runat="server" TabIndex="14" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged"
                                                AutoPostBack="true" ValidationGroup="teacherallot">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                                Display="None" ErrorMessage="Please Select Teacher" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvCurrentSubjects" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <h4>Current Semester Subjects </h4>
                                                            <table id="tblCurrentSubjects" class="table table-hover table-bordered table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>
                                                                            <asp:CheckBox ID="cbHeadReg" runat="server" Text="Check" ToolTip="Check/Check all" onclick="SelectAll(this,1,'chkRegister');" />
                                                                        </th>
                                                                        <th>Course Code
                                                                        </th>
                                                                        <th>Course Name
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trCurRow">
                                                            <td>
                                                                <asp:CheckBox ID="chkRegister" runat="server" ToolTip='<%# Eval("COURSENO")%>'
                                                                    onclick="ChkHeader(1,'cbHeadReg','chkRegister');" /><%--ToolTip="Click to select this subject for allotment"--%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCCode" Font-Size-size="5%" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCourseName" runat="server" Font-Size-size="5%" Text='<%# Eval("COURSE_NAME") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnSave" runat="server" TabIndex="14" Text="Save" ValidationGroup="teacherallot" class="buttonStyle ui-corner-all"
                                        OnClick="btnSave_Click" ToolTip="SAVE" CssClass="btn btn-success" />
                                    <asp:Button ID="btnReport" runat="server" Text="Print Report" class="buttonStyle ui-corner-all"
                                        ToolTip="Report" OnClick="btnReport_Click" CssClass="btn btn-info" />
                                    <asp:Button ID="btnClear" runat="server" TabIndex="15" Text="Clear"
                                        class="buttonStyle ui-corner-all" OnClick="btnClear_Click" CssClass="btn btn-danger" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                                <div class="col-md-12">
                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">

                                        <asp:Repeater ID="lvDetails" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-hover table-bordered table-responsive">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Session
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Subject Type
                                                            </th>
                                                            <th>Scheme Name
                                                            </th>
                                                            <th>Course
                                                            </th>
                                                            <th>Operator name
                                                            </th>
                                                            <th>Theory/Practical
                                                            </th>
                                                        </tr>
                                                        <thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("SESSION_PNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SCHEMENAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COURSENAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_FULLNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("THEORY/PRAC") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnReport" />
                <asp:PostBackTrigger ControlID="btnClear" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

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
        // function SelectAll(headchk) {

        //    var frm = document.forms[0]
        //   for (i = 0; i < document.forms[0].elements.length; i++) {
        //  var e = frm.elements[i];
        //  if (e.type == 'checkbox') {
        //    if (headchk.checked == true) {
        //        e.checked = true;

        //    }
        //   else {
        //       e.checked = false;
        //    }
        //    document.getElementById('<%=chkover.ClientID %>').checked = false;

        //   }

        // }

        // if (headchk.checked == true) {
        //     txtTot.value = hdfTot.value;
        //      txtCredits.value = hdfCredits.value;
        //  }
        // else {
        //    txtTot.value = 0;
        //     txtCredits.value = 0;
        //   }
        // }





        var TBL_Global = '';
        var LST_Global = '';

        function SelectAll(headerid, headid, chk) {
            debugger;
            //var tbl = '';
            //var list = '';
            if (headid == 1) {
                TBL_Global = document.getElementById('tblCurrentSubjects');
                LST_Global = 'lvCurrentSubjects';
            }

            try {
                var dataRows = TBL_Global.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + LST_Global + '_ctrl' + i + '_' + chk;

                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                    // ValidateFeeDetail();
                }
            }
            catch (e) {
                alert(e);
            }
        }


    </script>

</asp:Content>
