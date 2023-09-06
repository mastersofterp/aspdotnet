<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MarkEntry.aspx.cs" Inherits="Academic_MarkEntry" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMarkEntry"
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

    <%--<style>
        .GridHeader
        {
            /*text-align: center !important;*/
            text-align:center !important;
        }
    </style>--%>
    <style style="text/css">
        .bounce
        {
            height: 30px;
            overflow: hidden;
            position: relative;
            background: white;
            color: red;
            padding: 5px;
        }

            .bounce p
            {
                position: absolute;
                width: 100%;
                height: 50%;
                margin: 0;
                line-height: 30px;
                text-align: center;
                /* Starting position */
                -moz-transform: translateX(50%);
                -webkit-transform: translateX(50%);
                transform: translateX(50%);
                /* Apply animation to this element */
                -moz-animation: bouncing-text 15s linear infinite alternate;
                -webkit-animation: bouncing-text 15s linear infinite alternate;
                animation: bouncing-text 15s linear infinite alternate;
            }
        /* Move it (define the animation) */
        @-moz-keyframes bouncing-text
        {
            0%
            {
                -moz-transform: translateX(50%);
            }

            100%
            {
                -moz-transform: translateX(-50%);
            }
        }

        @-webkit-keyframes bouncing-text
        {
            0%
            {
                -webkit-transform: translateX(50%);
            }

            100%
            {
                -webkit-transform: translateX(-50%);
            }
        }

        @keyframes bouncing-text
        {
            0%
            {
                -moz-transform: translateX(50%); /* Browser bug fix */
                -webkit-transform: translateX(50%); /* Browser bug fix */
                transform: translateX(50%);
            }

            100%
            {
                -moz-transform: translateX(-50%); /* Browser bug fix */
                -webkit-transform: translateX(-50%); /* Browser bug fix */
                transform: translateX(-50%);
            }
        }

        .bounce p:hover
        {
            -moz-animation-play-state: paused;
            -webkit-animation-play-state: paused;
            animation-play-state: paused;
        }
    </style>
    <%--<style>
        .HeaderAlign {
        text-align:center;
        }
    </style>--%>


    <asp:UpdatePanel runat="server" ID="updMarkEntry">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Mark Entry</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="box-header with-border bounce" id="divstatus" runat="server" visible="false">
                                    <%--<marquee onmouseover="this.stop();" onmouseout="this.start();"><b>This is to inform you that Mark Entry activity is going to be stopped on <asp:Label ID="lblstatusmark" runat="server"></asp:Label> , so kindly enter the marks and lock for your respective subjects !!!!!</b></marquee>--%>
                                    <p style="margin-top: -5px">
                                        <b>This is to inform you that Mark Entry activity is going to be stopped on
                                        <asp:Label ID="lblstatusmark" runat="server"></asp:Label>
                                        , so kindly enter the marks and lock for your respective subjects !!!!!</b>
                                    </p>
                                </div>
                            </div>

                            <asp:Panel ID="pnlSelection" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1" AppendDataBoundItems="true" Font-Bold="true" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Course Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                                Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="selcourse">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" id="TRCourses" runat="server">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvCourse" runat="server" Visible="false" class="table table-striped table-hover">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Course Name
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblStatus" Visible="false" runat="server" Style="color: red; font: larger;"></asp:Label>
                                </div> 
                                <div class="col-12">
                                    <div style="overflow : auto; width:100%;">
                                        <asp:GridView ID="GVEntryStatus" runat="server" AutoGenerateColumns="false" OnPreRender="GVEntryStatus_PreRender" OnRowDataBound="GVEntryStatus_RowDataBound" Width="100%"
                                            CssClass="table table-bordered table-hover"  CellPadding="3" CellSpacing="2">
                                            <Columns>
                                                <asp:TemplateField HeaderText="COURSE NAME">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>'
                                                            CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO") %>'
                                                            OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("COURSENO")%>' />
                                                        <asp:HiddenField ID="hdnfld_courseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                        <asp:HiddenField ID="hdnsem" runat="server" Value='<%# Eval("semesterno")%>' />
                                                        <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                        <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("CCODE")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="EXAMNAME" HeaderText="EXAM NAME" />
                                                <asp:TemplateField HeaderText="STATUS" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("MARK_ENTRY_STATUS")%>' ID="lblStatus" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hffldname" runat="server" Value='<%# Eval("EXAMNAME")%>' />
                                                        <%--<asp:BoundField DataField="MARK_ENTRY_STATUS" HeaderText="MARK ENTRY STATUS" ItemStyle-Width="200" HeaderStyle-Height="40%" ItemStyle-Font-Size="92%" ItemStyle-BorderStyle="None" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EXAMWISE MARKS REPORT" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnreportexamwise" runat="server" CausesValidation="false" CommandName='<%# Eval("SECTIONNO")%>' CssClass="btn btn-primary"
                                                            OnClick="btnreportexamwise_Click" Text="Print" CommandArgument='<%# Eval("EXAMNAME") %>' ToolTip='<%# Eval("CCODE")%>' />
                                                        <asp:HiddenField ID="hdnsemester" runat="server" Value='<%# Eval("semesterno")%>' />
                                                        <asp:HiddenField ID="hdncorseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                        <asp:HiddenField ID="hdnExamField" runat="server" Value='<%# Eval("EXAMNAME1")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COURSEWISE MARKS REPORT" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCourseWISE" runat="server" Text="Print" CssClass="btn btn-primary" CommandArgument='<%# Eval("SECTIONNO")%>'
                                                            OnClick="btnCourseWISE_Click" ToolTip='<%# Eval("COURSENAME") %>' CommandName='<%# Eval("COURSENO")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle />
                                            <HeaderStyle CssClass="bg-light-blue" Width="100%" ForeColor="white" />
                                            <PagerStyle />
                                            <RowStyle />
                                            <SelectedRowStyle ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlMarkEntry" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Selection Criteria</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession2" runat="server" AppendDataBoundItems="true" Font-Bold="true" CssClass="form-control" data-select2-enable="true">
                                                    </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Course Name</label>
                                            </div>
                                            <asp:Label ID="lblCourse" runat="server" Font-Bold="true" />
                                            <asp:HiddenField ID="hdfSection" runat="server" />
                                            <asp:HiddenField ID="hdfBatch" runat="server" />
                                            <asp:HiddenField ID="hdfSemNo" runat="server" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Exam</label>
                                            </div>
                                            <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged"
                                                ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                                Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="SubDiv" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Sub Exam</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubExam" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSubExam_SelectedIndexChanged"
                                                ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSubExam" runat="server" ControlToValidate="ddlSubExam"
                                                Display="None" ErrorMessage="Please Select Sub Exam" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" OnClick="btnShow_Click"
                                        Text="Show Student" ValidationGroup="show" />
                                    <asp:Button ID="btnAutoGenAttendance" runat="server" CssClass="btn btn-primary" OnClick="btnAutoGenAttendance_Click"
                                        Text="Auto Generate Attendance Marks" ValidationGroup="show" Visible="false" />
                                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn btn-primary" />

                                    <asp:Button ID="btnSave" runat="server" Enabled="false"
                                        OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary" ValidationGroup="val" />

                                    <asp:Button ID="btnLock" runat="server" Enabled="false"
                                        OnClick="btnLock_Click" OnClientClick="return showLockConfirm(this,'val');" Text="Lock"
                                        CssClass="btn btn-primary" />

                                    <asp:Button ID="btnMIDReport" runat="server" Text="MID Report" CssClass="btn btn-info"
                                        OnClick="btnTAReport_Click" Visible="False" />
                                    <asp:Button ID="btnConsolidateReport" runat="server" Visible="False"
                                        Text="Consolidate Report" CssClass="btn btn-info" OnClick="btnConsolidateReport_Click" />

                                    <asp:Button ID="btnCancel2" runat="server" OnClick="btnCancel2_Click"
                                        Text="Cancel" CssClass="btn btn-warning" Visible="False" />

                                    <%--<asp:Button ID="btnReport" runat="server" Font-Bold="true" Text="Print" CssClass="btn btn-info" 
                                    OnClick="btnReport_Click" Enabled="false" Visible="False" />--%>
                                    <%--<asp:Button ID="btncoursereport" runat="server" Font-Bold="true" Text="SubjectWise Marks Report" CssClass="btn btn-info"
                                    OnClick="btncoursereport_Click" Visible="False" />--%>

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-12 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Save and Lock for Final Submission of Marks</span>  </p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Enter - </span></p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>"-1" for Absent Student</span></p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>"-2" for UFM(Copy Case)</span></p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>"-3" for WithDraw Student</span></p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>"-4" for Drop Student</span></p>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-6 col-md-12 col-12 d-none">
                                            <div class="note-div" id="spanNote" runat="server" visible="false">
                                                <h5 class="heading">Note</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Marks can not modified for Auto Genarate Attendance Marks, You can only save and lock the marks.</span>  </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-12" id="tdStudent" runat="server">
                                    <div style="overflow : auto; width:100%;">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
                                                <div id="demo-grid" class="vista-grid">
                                                    <div class="sub-heading">
                                                        <h5>Enter Marks for following Students</h5>
                                                    </div>
                                                    <%--    <div class="col-md-12 table table-responsive">--%>
                                                    <%--  <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-striped table-bordered table-hover"
                                                    BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
                                                    CellPadding="3" CellSpacing="2">--%>
                                                    <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="GridHeader"
                                                         OnRowDataBound="gvStudent_RowDataBound"  CssClass="table table-bordered table-hover">
                                                        <HeaderStyle CssClass="bg-light-blue" Width="100%" ForeColor="white" />
                                                        <%--  <AlternatingRowStyle BackColor="#FFFFD2" />--%>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No." >
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Registration No. / Roll No." >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO1") %>' ToolTip='<%# Bind("IDNO") %>'/>
                                                                </ItemTemplate>
                                                                <ItemStyle/>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="STUDNAME" HeaderText="Student Name" >
                                                                <ItemStyle/>
                                                            </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Attendance Percentage" HeaderStyle-CssClass="GridHeader" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPercentage" runat="server" Text='<%# Bind("TH_PERCENTAGE") %>' />
                                                                </ItemTemplate>
                                                                    <HeaderStyle />
                                                                           
                                                                <ItemStyle/>
                                                            </asp:TemplateField>
                                                                    
                                                            <%--EXAM MARK ENTRY--%>
                                                            <asp:TemplateField HeaderText="MARKS" Visible="false">
                                                                <ItemTemplate>
                                                                    <%--ReadOnly='<%# Bind("LOCK") %>'--%>
                                                                    <asp:TextBox ID="txtMarks" runat="server" Text='<%# Bind("SMARK") %>'
                                                                        Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                        MaxLength="5" Font-Bold="true" Style="text-align: center" onkeyup="return CheckMark(this);" />
                                                                    <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCK") %>'
                                                                        Visible="false" />
                                                                    <asp:Label ID="lblMinMarks" runat="server" Text='<%# Bind("SMIN") %>' Visible="false" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                        ValidChars="0123456789-." TargetControlID="txtMarks">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                    <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("SMAX") %>' ControlToValidate="txtMarks"
                                                                        Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                                                        ValidationGroup="val" Text="*" Display="None"></asp:CompareValidator>
                                                                    <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comTutMarks" ID="cecomTutMarks"
                                                                        runat="server">
                                                                    </ajaxToolKit:ValidatorCalloutExtender>
                                                                    <asp:CompareValidator ID="cvAbsentStud" ValueToCompare="-1" ControlToValidate="txtMarks"
                                                                        Operator="NotEqual" Type="Double" runat="server" ErrorMessage="-1 for absent student"
                                                                        ValidationGroup="val1" Text="*" Display="None">
                                                                    </asp:CompareValidator>
                                                                    <ajaxToolKit:ValidatorCalloutExtender TargetControlID="cvAbsentStud" ID="vceAbsentStud"
                                                                        runat="server">
                                                                    </ajaxToolKit:ValidatorCalloutExtender>
                                                                </ItemTemplate>
                                                                <HeaderStyle />
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle />
                                                        <HeaderStyle ForeColor="White" />
                                                        <PagerStyle/>
                                                        <RowStyle/>
                                                        <SelectedRowStyle/>
                                                    </asp:GridView>
                                                    <%--</div>--%>
                                                </div>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </div>
                                </div>
                                        
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
    <asp:HiddenField ID="hdfDegree" runat="server" Value="" />
    <script type="text/javascript">

        function CheckMark(id) {
            //alert(id.value);
            if (id.value < 0) {
                if (id.value == -1 || id.value == -2 || id.value == -3 || id.value == -4)  ///
                {
                    //alert("You have marked student as ABSENT.");
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1, -2, -3 and -4 can be entered.");
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
