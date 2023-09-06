<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MarksEntryByOperator_For_EndSem.aspx.cs" Inherits="ACADEMIC_EXAMINATION_MarksEntryByOperator_For_EndSem" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:Panel ID="pnlSelection" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">MARKS ENTRY BY OPERATOR FOR END SEM</h3>
                    </div>
                    <div class="box-body">
                        <fieldset>
                            <legend>Selection Criteria Marks Entry</legend>
                            <div class="form-group col-md-3">
                                <label>Session</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-3">
                                <label>College /School Name</label>
                                <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="selcourse"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="teacherallot">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Degree</label>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="selcourse"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Branch</label>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="selcourse"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallot">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Scheme</label>
                                <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                    ValidationGroup="selcourse" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Semester</label>
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="selcourse"
                                    OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Subject Type</label>
                                <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged"
                                    TabIndex="1">
                                    <asp:ListItem>Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                    Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="selcourse">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-12" id="TRCourses" runat="server">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvCourse" runat="server" Style="Width: 70% !important;">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h4>Course List </h4>
                                                <table class="table table-hover table-bordered table-responsive">
                                                    <thead>
                                                        <tr class="bg-light-blue">
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
                                            <tr>
                                                <td>
                                                    <%--<asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>'
                                            CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO") %>'
                                            OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("CCODE")%>' />--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-md-12" id="TRStatus" runat="server">
                                <div class="col-md-12">
                                    <asp:Label ID="lblStatus" runat="server" Style="color: red; font: larger; margin-right: 912px;"></asp:Label>
                                </div>
                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                    <asp:GridView ID="GVEntryStatus" runat="server" AutoGenerateColumns="false" OnPreRender="GVEntryStatus_PreRender" OnRowDataBound="GVEntryStatus_RowDataBound"
                                        class="table table-hover table-bordered table-responsive">
                                        <Columns>
                                            <asp:TemplateField HeaderText="COURSE NAME">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>'
                                                        CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO") %>'
                                                        OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("CCODE")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EXAMNAME" HeaderText="EXAM NAME" />
                                            <asp:BoundField DataField="MARK_ENTRY_STATUS" HeaderText="MARK ENTRY STATUS" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </fieldset>

                        <asp:Panel ID="pnlMarkEntry" runat="server">
                            <fieldset class="fieldset">
                                <legend class="legend">End Sem Marks Entry</legend>
                                <div class="col-md-9">
                                    <div class="col-md-4">
                                        <label>Session</label>
                                        <asp:DropDownList ID="ddlSession2" runat="server" AppendDataBoundItems="true" Font-Bold="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Course Name</label>
                                        <br />
                                        <asp:Label ID="lblCourse" runat="server" Font-Bold="true" />
                                        <asp:HiddenField ID="hdfSection" runat="server" />
                                        <asp:HiddenField ID="hdfBatch" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <label>Exam</label>
                                        <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" ValidationGroup="show">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please Select Exam" InitialValue="Select Exam" SetFocusOnError="True"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <fieldset class="fieldset" style="padding: 5px; color: Green">
                                        <legend class="legend">Note</legend>
                                        <span style="font-weight: bold; color: Red;">Please Save and Lock for Final Submission of Marks
                                        </span>
                                        <br />
                                        <b>Enter :<br />
                                            "-1" for Absent Student<br />
                                            "-2" for Not Eligible Student </b>
                                    </fieldset>
                                </div>
                                <div class="col-md-12" style="margin-top: 25px">
                                    <p class="text-center">
                                        <asp:Button ID="btnShow" runat="server" Font-Bold="True" OnClick="btnShow_Click"
                                            OnClientClick="return CheckExam();" Text="Show Student" ValidationGroup="show" CssClass="btn btn-primary" />
                                    </p>
                                    <p>
                                        <asp:Label ID="lblStudents" runat="server" Font-Bold="true" /></p>
                                </div>

                                <div class="Col-md-12" style="margin-top: 15px">
                                    <p class="text-center">
                                        <asp:Button ID="btnBack" runat="server" Font-Bold="true" OnClick="btnBack_Click"
                                            Text="Back" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnSave" runat="server" Enabled="false" Font-Bold="true"
                                            OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary" ValidationGroup="val" />
                                        <asp:Button ID="btnLock" runat="server" Enabled="false" Font-Bold="true"
                                            OnClick="btnLock_Click" OnClientClick="return showLockConfirm(this,'val');" Text="Lock"
                                            CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel2" runat="server" Font-Bold="true" OnClick="btnCancel2_Click"
                                            Text="Cancel" CssClass="btn btn-warning" Visible="False" />
                                        <asp:Button ID="btnReport" runat="server" Font-Bold="true" Text="Print" CssClass="btn btn-info"
                                            OnClick="btnReport_Click" Enabled="false" Visible="False" />
                                        <asp:Button ID="btnMIDReport" runat="server" Font-Bold="true" Text="MID Report" CssClass="btn btn-info"
                                            OnClick="btnTAReport_Click" Visible="False" />
                                        <asp:Button ID="btnConsolidateReport" runat="server" Font-Bold="true" Visible="False"
                                            Text="Consolidate Report" CssClass="btn btn-info" OnClick="btnConsolidateReport_Click" />
                                    </p>
                                </div>
                                <div class="col-md-12" id="tdStudent" runat="server">
                                    <asp:Panel ID="pnlStudGrid" runat="server" Visible="false" ScrollBars="Auto">
                                        <div id="demo-grid">
                                            <h4>Enter Marks for following Students</h4>
                                            <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-hover table-bordered table-responsive">
                                                <HeaderStyle CssClass="bg-light-blue" />
                                                <AlternatingRowStyle BackColor="#FFFFD2" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Decode No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("RANDOM_NO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                Font-Size="9pt" />
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="STUDNAME" HeaderText="Student Name">
                                                        <ItemStyle />
                                                    </asp:BoundField>
                                                    <%--EXAM MARK ENTRY--%>
                                                    <asp:TemplateField HeaderText="MARKS" Visible="false">
                                                        <ItemTemplate>
                                                            <%--ReadOnly='<%# Bind("LOCK") %>'--%>
                                                            <asp:TextBox ID="txtMarks" runat="server" Text='<%# Bind("SMARK") %>'
                                                                Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                MaxLength="4" Font-Bold="true" Style="text-align: center" onkeyup="return CheckMark(this);" />
                                                            <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCK") %>'
                                                                Visible="false" />
                                                            <asp:Label ID="lblMinMarks" runat="server" Text='<%# Bind("SMIN") %>' Visible="false" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txtMarks">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("SMAX") %>' ControlToValidate="txtMarks"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                                                ValidationGroup="val" Text="*"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comTutMarks" ID="cecomTutMarks"
                                                                runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>
                                                            <asp:CompareValidator ID="cvAbsentStud" ValueToCompare="-1" ControlToValidate="txtMarks"
                                                                Operator="NotEqual" Type="Double" runat="server" ErrorMessage="-1 for absent student"
                                                                ValidationGroup="val1" Text="*">
                                                            </asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="cvAbsentStud" ID="vceAbsentStud"
                                                                runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </div>

                            </fieldset>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    
    <div id="divMsg" runat="server">
    </div>
    <asp:HiddenField ID="hdfDegree" runat="server" Value="" />


    <script language="javascript" type="text/javascript">

        function CheckMark(id) {
            //alert(id.value);
            if (id.value < 0) {
                if (id.value == -1 || id.value == -2 || id.value == -3)  ///
                {
                    //alert("You have marked student as ABSENT.");
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1, -2 and -3 can be entered.");
                    id.value = '';
                    id.focus();
                }
            }
        }

        function validateMark(txttut, txtmidsem, txtCT, txtCE, txtatt, txttot, col) {

            var mark1, mark2, mark3, mark4, mark5, total;

            if (txttut.value == "") {
                txttot.value = "";
                mark1 = 0
            }
            else {
                mark1 = txttut.value;

            }
            if (txtmidsem.value == "") {
                txttot.value = "";
                mark2 = 0;
            }
            else {
                mark2 = txtmidsem.value;

            }
            if (txtCT.value == "") {
                txttot.value = "";
                mark3 = 0;
            }
            else {
                mark3 = txtCT.value;

            }
            if (txtCE.value == "") {
                txttot.value = "";
                mark4 = 0;
            }
            else {
                mark4 = txtCE.value;

            }
            if (txtatt.value == "") {
                txttot.value = "";
                mark5 = 0;
            }
            else {
                mark5 = txtatt.value;
            }
            if (col == 2) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5);
            }
            else if (col == 1) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5) + parseFloat(mark1) + parseFloat(mark4);
            }
            else if (col == 3) {
                total = parseFloat(mark4);
            }
            else if (col == 4) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5) + parseFloat(mark1);
            }
            txttot.value = total;


            //            if (col == 2 || col == 3) {
            //                if (txt2 == '') {
            //                    if (txt.value == "" || txt.value == 401)
            //                        txttot.value = "0";
            //                    else
            //                        txttot.value = txt.value;
            //                }
            //                else if (txt == '') {
            //                    if (txt2.value == "" || txt2.value == 401)
            //                        txttot.value = "0";
            //                    else
            //                        txttot.value = txt2.value;
            //                }
            //                else if (txt != '' && txt2 != '') {
            //                    var mark1, mark2;

            //                    if (txt.value == "" || txt.value == 401)
            //                        mark1 = parseInt("0");
            //                    else
            //                        if (isNaN(Number(txt.value)))
            //                        mark1 = 0;
            //                    else
            //                        mark1 = Number(txt.value)

            //                    if (txt2.value == "" || txt2.value == 401)
            //                        mark2 = parseInt("0");
            //                    else
            //                        if (isNaN(Number(txt2.value)))
            //                        mark2 = 0;
            //                    else
            //                        mark2 = Number(txt2.value)

            //                    txttot.value = Math.round(Number(mark1 + mark2));
            //                }

            //            }
            //            alert(txttot.value);
            //            if (col == 2 || col == 3 || col == 1)
            //                IsNumericTest(metxt);
            //            else
            //            //check for numeric
            //                IsNumeric(met);
        }

        //        function IsNumericTest(txt) {
        //            var ValidChars = "0123456789.";
        //            var num = true;
        //            var mChar;
        //            cnt = 0

        //            for (i = 0; i < txt.value.length && num == true; i++) {
        //                mChar = txt.value.charAt(i);
        //                if (mChar == '.') cnt++;
        //                if (cnt > 1) { alert("Please Check the value."); txt.value = ""; return }
        //                else
        //                    if (ValidChars.indexOf(mChar) == -1) {
        //                    num = false;
        //                    txt.value = '';
        //                    alert("Please enter Numeric values only")
        //                    txt.select();
        //                    txt.focus();
        //                }
        //            }
        //            return num;
        //        }
        //        function IsNumeric(txt) {
        //            var ValidChars = "0123456789";
        //            var num = true;
        //            var mChar;
        //            cnt = 0

        //            for (i = 0; i < txt.value.length && num == true; i++) {
        //                mChar = txt.value.charAt(i);

        //                if (ValidChars.indexOf(mChar) == -1) {
        //                    num = false;
        //                    txt.value = '';
        //                    alert("Please enter Numeric values only")
        //                    txt.select();
        //                    txt.focus();
        //                }
        //            }
        //            return num;
        //        }

        function showLockConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {
                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                var ret = confirm('Do you really want to lock marks for selected exam?\n\nOnce Locked it cannot be modified or changed.');
                if (ret == true) {
                    var ret2 = confirm('You are about to lock entered marks, be sure before locking.\n\nOnce Locked it cannot be modified or changed. \n\nClick OK to Continue....');
                    if (ret2 == true) {
                        validate = true;
                    }
                    else
                        validate = false;
                }
                else
                    validate = false;
            }
            return validate;
        }

        function showLockConfirm_old() {
            var ret = confirm('Do you really want to lock marks for selected exam?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>

</asp:Content>

