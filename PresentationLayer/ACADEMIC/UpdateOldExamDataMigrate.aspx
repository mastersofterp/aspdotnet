<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UpdateOldExamDataMigrate.aspx.cs" Inherits="ACADEMIC_UpdateOldExamDataMigrate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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
    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div id="dvMain" runat="server">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">UPDATE OLD EXAM DATA MIGRATION</h3>
                            </div>
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                                                ControlToValidate="ddlSession" Display="None" ValidationGroup="Show" InitialValue="0" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Registration No</label>
                                            </div>
                                            <asp:TextBox ID="txtAdmissionNo" runat="server" TabIndex="2" CssClass="form-control" OnTextChanged="txtAdmissionNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Registration No."
                                                ControlToValidate="txtAdmissionNo" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" data-select2-enable="true" AppendDataBoundItems="True" CssClass="form-control" TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlSemester" runat="server" Display="None"
                                                InitialValue="0" ErrorMessage="Please Select Semester." ValidationGroup="Show" />
                                        </div>
                                    </div>

                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="4" ValidationGroup="Show"
                                        OnClick="btnShow_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                        Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" Enabled="false" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="valShowSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                </div>
                                <asp:Panel runat="server" ID="pnlResult" Visible="true" class="col-12 mt-5">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Registered Credits </label>
                                            </div>
                                            <asp:TextBox ID="txtRegCredits" runat="server" TabIndex="6" CssClass="form-control" MaxLength="5" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvRegCredits" runat="server" ErrorMessage="Please Enter Registered Credits"
                                            ControlToValidate="txtRegCredits" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Earn Credits</label>
                                            </div>
                                            <asp:TextBox ID="txtEarnCredits" runat="server" TabIndex="7" CssClass="form-control" MaxLength="5" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvEarnCredits" runat="server" ErrorMessage="Please Enter Earn Credits"
                                            ControlToValidate="txtEarnCredits" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Commulative Credits</label>
                                            </div>
                                            <asp:TextBox ID="txtCommCredits" runat="server" TabIndex="8" CssClass="form-control" MaxLength="5" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvCommCredits" runat="server" ErrorMessage="Please Enter Commulative Credits"
                                            ControlToValidate="txtCommCredits" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Result</label>
                                            </div>
                                            <asp:DropDownList ID="ddlResult" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="9">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">PASS</asp:ListItem>
                                                <asp:ListItem Value="2">FAIL</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlResult" runat="server" Display="None"
                                            InitialValue="0" ErrorMessage="Please Select Result." ValidationGroup="Submit" />--%>
                                            <%--<asp:TextBox ID="txtResult" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvResult" runat="server" ErrorMessage="Please Enter Result"
                                            ControlToValidate="txtResult" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Out of Marks</label>
                                            </div>
                                            <asp:TextBox ID="txtOutOfmarks" runat="server" TabIndex="10" CssClass="form-control" MaxLength="6" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <%--   <asp:RequiredFieldValidator ID="rfvOutMarks" runat="server" ErrorMessage="Please Enter Out of Marks"
                                            ControlToValidate="txtOutOfmarks" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Total Obtained Marks</label>
                                            </div>
                                            <asp:TextBox ID="txtObtainMarks" runat="server" TabIndex="11" CssClass="form-control" MaxLength="6" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvObtainMarks" runat="server" ErrorMessage="Please Enter Total Obtained Marks"
                                            ControlToValidate="txtObtainMarks" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>SGPA</label>
                                            </div>
                                            <asp:TextBox ID="txtSGPA" runat="server" TabIndex="12" CssClass="form-control" MaxLength="6" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvSGPA" runat="server" ErrorMessage="Please Enter SGPA"
                                            ControlToValidate="txtSGPA" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>YGPA</label>
                                            </div>
                                            <asp:TextBox ID="txtYGPA" runat="server" TabIndex="13" CssClass="form-control" MaxLength="6" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvCGPA" runat="server" ErrorMessage="Please Enter CGPA"
                                            ControlToValidate="txtCGPA" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>DGPA</label>
                                            </div>
                                            <asp:TextBox ID="txtDGPA" runat="server" TabIndex="14" CssClass="form-control" MaxLength="6" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvCGPA" runat="server" ErrorMessage="Please Enter CGPA"
                                            ControlToValidate="txtCGPA" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Commulative EGP</label>
                                            </div>
                                            <asp:TextBox ID="txtCommEGP" runat="server" TabIndex="15" CssClass="form-control" MaxLength="5" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="efvConnEGP" runat="server" ErrorMessage="Please Enter Commulative EGP"
                                            ControlToValidate="txtCommEGP" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Promoted Status</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPromoStatus" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="16">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">PASS</asp:ListItem>
                                                <asp:ListItem Value="2">PROMOTED</asp:ListItem>
                                                <asp:ListItem Value="3">NOT PROMOTED</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvPromoStatus" ControlToValidate="ddlPromoStatus" runat="server" Display="None"
                                            InitialValue="0" ErrorMessage="Please Select Promoted Status." ValidationGroup="Submit" />--%>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-12">
                                    <asp:Panel ID="pvlCourse" runat="server" Visible="false">
                                        <div id="divCourseList" style="display: block" visible="true" runat="server">
                                            <div class="sub-heading">
                                                <h5>Course List</h5>
                                            </div>
                                            <asp:ListView ID="lvCourse" runat="server">
                                                <LayoutTemplate>
                                                    <table id="tblCourse" class="table table-striped table-bordered display " style="width: 100%">
                                                        <thead>
                                                            <tr class="header bg-light-blue">
                                                                <th>Select</th>
                                                                <th>Course Name</th>
                                                                <th>Course Type</th>
                                                                <th>Grade</th>
                                                                <th>Grade Point</th>
                                                                <%-- <th>CA-I</th>
                                            <th>CA-II</th>
                                            <th>CA-III</th>
                                            <th>CA-VI</th>
                                            <th>PCA-I</th>
                                            <th>PCA-II</th>--%>
                                                                <th>End Sem</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td class="check">
                                                            <asp:CheckBox ID="cbRow" runat="server" onclick="EstablishmentSelfRating(this);" ToolTip='<%# Eval("COURSENO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblCourseName" Text='<%# Eval("COURSENAME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblCourseType" Text='<%# Eval("SUBJECTTYPE") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGrade" runat="server" Enabled="false" TextMode="SingleLine" onkeyup="conver_uppercase(this);" class="form-control" Text='<%# Eval("GRADE") %>'></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtGrade"
                                                                FilterType="Custom" FilterMode="InvalidChars" InvalidChars="1234567890." />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGradePt" runat="server" Enabled="false" TextMode="SingleLine" class="form-control" Text='<%# Eval("GDPOINT") %>'></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtGradePt"
                                                                FilterType="Custom" ValidChars="1234567890." FilterMode="ValidChars" />
                                                        </td>

                                                        <td>
                                                            <asp:TextBox ID="txtEndSem" runat="server" Enabled="false" TextMode="SingleLine" class="form-control" Text='<%# Eval("EXTERMARK") %>'></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txtEndSem"
                                                                FilterType="Custom" ValidChars="1234567890." FilterMode="ValidChars" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function validateCheckBoxes() {
            var count = false;
            var dataRows = null;

            if (document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_tblCourse') != null)
                dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_tblCourse').getElementsByTagName('tr');
            if (dataRows != null) {
                for (var i = 0; i < listView.rows.length; i++) {
                    for (i = 1; i < dataRows.length; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(3);
                        var controls = dataCell.getElementsByTagName('input');

                        if (controls.item[0].type == "checkbox") {
                            if (controls.item[0].checked = true) {

                                count = true;
                            }
                        }

                    }
                }
            }

            if (count == true) {
                alert('Please Enter Grade, Grade Point And End Sem Marks');
            }
        }

        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }

        function EstablishmentSelfRating(vall) {

            var myArr = new Array();
            myString = "" + vall.id + "";
            myArr = myString.split("_");
            var index = myArr[3];

            var txtGrade = document.getElementById("ctl00_ContentPlaceHolder1_lvCourse_" + index + "_txtGrade");
            var txtGradePt = document.getElementById("ctl00_ContentPlaceHolder1_lvCourse_" + index + "_txtGradePt");
            var txtEndSem = document.getElementById("ctl00_ContentPlaceHolder1_lvCourse_" + index + "_txtEndSem");
            //var SelfRatingGen = document.getElementById("ctl00_ContentPlaceHolder1_lvCatI_Establishment_" + index + "_txtSelfScore").value;
            //var hdnMaxRateGen = document.getElementById("ctl00_ContentPlaceHolder1_lvCatI_Establishment_" + index + "_hdn_MaxMark").value;
            if (vall.checked == true) {

                txtGrade.disabled = false;
                txtGradePt.disabled = false;
                txtEndSem.disabled = false;

            }
            else {
                txtGrade.disabled = true;
                txtGradePt.disabled = true;
                txtEndSem.disabled = true;
            }
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

    </script>

</asp:Content>

