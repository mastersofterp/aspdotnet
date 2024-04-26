<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualificationDetails.aspx.cs"
    MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_QualificationDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #sidebar {
            display: none;
        }

        .page-wrapper.toggled .page-content {
            padding-left: 15px;
        }

        .panel-info > .panel-heading b {
            padding: 8px;
            font-size: 12px;
        }

        .sidebar-menu {
            padding: 0;
            list-style: none;
        }

            .sidebar-menu .treeview {
                padding: 0px 0px;
                color: #255282;
            }

        .treeview i {
            padding-left: 10px;
        }

        .treeview span a {
            color: #255282 !important;
            font-weight: 600;
            padding-left: 3px;
        }

            .treeview span a:hover {
                color: #0d70fd !important;
            }

        .treeview.active i, .treeview.active span a {
            color: #0d70fd !important;
        }

        hr {
            margin: 12px 0px;
            border-top: 1px solid #ccc;
        }

        #ctl00_ContentPlaceHolder1_divtabs {
            box-shadow: rgb(0 0 0 / 20%) 0px 5px 10px;
            padding: 15px 5px;
            margin: 5px 0px 15px 0px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        table .form-control {
            padding: 0.375rem 0.15rem;
        }
    </style>
    <script type="text/javascript">
        function ErrorMessage(errorString) {
            debugger;
            var errorString = errorString.replace(/,/g, '\n');
            alert(errorString)
        }
    </script>
    <script type="text/javascript">
        function alertmessage(commaSeparatedString) {
            debugger;

            var parts = commaSeparatedString.split(',');
            var errorMessage = parts.join('\n');
            alert(errorMessage);
        }
    </script>

    <script type="text/javascript">
        function findTotal(val) {
            debugger;
            var CatchTotMarks = Number(document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value);  // Added by sachin on 21-07-2022
            var val1 = document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value = '';
            var numVal1 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtphymark").value);
            var numVal2 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtchem").value);
            var numVal3 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").value);
            var numVal4 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").value);
            var vocTotal = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmarktotal").value);
            var numVal5 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtbiology").value);
            var isHidden = document.getElementById("ctl00_ContentPlaceHolder1_trBiology");

            if (val1 == '') {
                if (numVal1 > 100) {
                    alert('Enter marks should not be greater than total marks');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtphymark").value = '';
                    document.getElementById("ctl00_ContentPlaceHolder1_txtphymark").focus();
                    document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value = CatchTotMarks;        // Added by sachin on 21-07-2022
                    return;
                }
                if (numVal2 > 100) {
                    alert('Enter marks should not be greater than total marks');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtchem").value = '';
                    document.getElementById("ctl00_ContentPlaceHolder1_txtchem").focus();
                    document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value = CatchTotMarks;        // Added by sachin on 21-07-2022
                    return;
                }
                if (numVal3 > 100) {
                    alert('Enter marks should not be greater than total marks');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").value = '';
                    document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").focus();
                    document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value = CatchTotMarks;        // Added by sachin on 21-07-2022
                    return;
                }
                if (numVal4 > vocTotal) {
                    alert('Vocational marks should not be greater than total marks');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").value = '';
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").focus();
                    document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value = CatchTotMarks;         // Added by sachin on 21-07-2022
                    return;
                }
                if (numVal5 > 100) {
                    alert('Enter marks should not be greater than total marks');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtbiology").value = '';
                    document.getElementById("ctl00_ContentPlaceHolder1_txtbiology").focus();
                    document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value = CatchTotMarks;         // Added by Shrikant on 18-05-2023
                    return;
                }

                var sum = Number(parseFloat(numVal1)) + Number(parseFloat(numVal2)) + Number(parseFloat(numVal3)) + Number(parseFloat(numVal4));

                if (isHidden.style.display == "none") {
                    PCMTotal(this);
                }
                else {
                    if (numVal3 >= numVal5) {
                        PCMTotal(this);
                    }
                    else if (numVal3 < numVal5) {
                        PCBTotal(this);
                    }
                }

                document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value = CatchTotMarks;
                return false;
            }

            var sum = 0;
            if (val == '') {
                alert('error');
                val = 0;
            } else {
                sum += Number(val.value);
            }
            document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value = CatchTotMarks;
        }

        function calculateMarksObtained() {
            var pcmMarksInput = document.getElementById('ctl00_ContentPlaceHolder1_txtPcmMarks');
            var vocationalMarkInput = document.getElementById('ctl00_ContentPlaceHolder1_txtVocationalmark');
            var maxMarksObtainedInput = document.getElementById('ctl00_ContentPlaceHolder1_txtMarksObtainedHssc');
            var pcmMaxMarksInput = document.getElementById('ctl00_ContentPlaceHolder1_txtPCMTotal');
            var vocationalMaxMarksInput = document.getElementById('ctl00_ContentPlaceHolder1_txtVocationalmarktotal');
            var outOfMarksInput = document.getElementById('ctl00_ContentPlaceHolder1_txtOutOfMarksHssc');

            var pcmMarks = parseFloat(pcmMarksInput.value) || 0;
            var vocationalMark = parseFloat(vocationalMarkInput.value) || 0;
            var pcmMaxMarks = parseFloat(pcmMaxMarksInput.value) || 0;
            var vocationalMaxMarks = parseFloat(vocationalMaxMarksInput.value) || 0;

            if (isNaN(pcmMarks) || isNaN(vocationalMark) || isNaN(pcmMaxMarks) || isNaN(vocationalMaxMarks)) {
                maxMarksObtainedInput.value = '';
                outOfMarksInput.value = '';
            } else {
                if (vocationalMarkInput.value === '') {
                    maxMarksObtainedInput.value = pcmMarks;
                } else {
                    var sum = pcmMarks + vocationalMark;
                    maxMarksObtainedInput.value = sum;
                }
                outOfMarksInput.value = pcmMaxMarks + vocationalMaxMarks;
            }
        }

        function calculateTotalMarks() {                                                                            // Added by Shrikant on 19-05-2022
            var pcmMaxMarksInput = document.getElementById('ctl00_ContentPlaceHolder1_txtPCMTotal');
            var pcmMarksObtainedMarks = document.getElementById('ctl00_ContentPlaceHolder1_txtVocationalmark');
            var vocationalMaxMarksInput = document.getElementById('ctl00_ContentPlaceHolder1_txtVocationalmarktotal');
            var outOfMarksInput = document.getElementById('ctl00_ContentPlaceHolder1_txtOutOfMarksHssc');

            var pcmMaxMarks = parseFloat(pcmMaxMarksInput.value) || 0;
            var vocationalMaxMarks = parseFloat(vocationalMaxMarksInput.value) || 0;


            if (isNaN(pcmMaxMarks) || isNaN(vocationalMaxMarks)) {
                outOfMarksInput.value = ''; // Clear the result if either input is not a valid number
            } else {
                var sum = pcmMaxMarks + vocationalMaxMarks;
                outOfMarksInput.value = sum;
            }
        }

        function validateVocDetails() {
            var vocObtainedMarks = Number(document.getElementById('ctl00_ContentPlaceHolder1_txtVocationalmark').value);
            var vocMaxMarks = Number(document.getElementById('ctl00_ContentPlaceHolder1_txtVocationalmarktotal').value);

            if (vocObtainedMarks > vocMaxMarks) {
                alert('Obtained Vocational Marks should not be greater than Maximum Vocational Marks');
                document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").value = '';
                document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").focus();
                return;
            }
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "PCMTotal()", true);

        function PCMTotal() {
            //alert("PCMtotal")
            debugger;

            var total = parseInt(document.getElementById('<%=txtphymark.ClientID%>').value);
            var val2 = parseInt(document.getElementById('<%=txtchem.ClientID%>').value);
            var val3 = parseInt(document.getElementById('<%=txtmaths.ClientID%>').value);

            // to make sure that they are numbers
            if (!total) { total = 0; }
            if (!val2) { val2 = 0; }
            if (!val3) { val3 = 0; }

            var ansD = document.getElementById('<%=txtPcmMarks.ClientID%>');
            ansD.value = total + val2 + val3;
            document.getElementById("hfdPcmMarks").value = ansD.value;         //Added by sachin on 20-07-2022
            var marksPCM = ansD.value;
            var Percentage = 0.00;
            //alert(marksPCM);
            //var Percentage = (marksPCM * 100 / 300).toFixed(2);
            Percentage = Number((marksPCM / 300) * 100);
            var Per = Percentage.toFixed(2)
            //alert(Percentage);
            document.getElementById('<%=txtPcmPerct.ClientID %>').value = Per;
            document.getElementById("hfdPcmPer").value = Per;
        }

        function PCBTotal() {
            //alert("PCMtotal")

            var var1 = parseInt(document.getElementById('<%=txtphymark.ClientID%>').value);
            var var2 = parseInt(document.getElementById('<%=txtchem.ClientID%>').value);
            var var3 = parseInt(document.getElementById('<%=txtbiology.ClientID%>').value);

            // to make sure that they are numbers
            if (!var1) { var1 = 0; }
            if (!var2) { var2 = 0; }
            if (!var3) { var3 = 0; }

            var ansC = document.getElementById('<%=txtPcmMarks.ClientID%>');
            ansC.value = var1 + var2 + var3;
            document.getElementById("hfdPcmMarks").value = ansC.value;         //Added by sachin on 20-07-2022
            var marksPCB = ansC.value;
            var Percentage = 0.00;
            //alert(marksPCM);
            //var Percentage = (marksPCM * 100 / 300).toFixed(2);
            Percentage = Number((marksPCB / 300) * 100);
            var Per2 = Percentage.toFixed(2)
            //alert(Percentage);
            document.getElementById('<%=txtPcmPerct.ClientID %>').value = Per2;
            document.getElementById("hfdPcmPer").value = Per2;


            //function myFunction() {
            //    var num = 26.666666666666668;
            //    var n = num.toFixed(2)
            //    document.getElementById("demo").innerHTML = n;
            //}

        }
    </script>


    <script type="text/javascript">
        function SumTotal(val) {
            debugger;
            var val1 = 300;//document.getElementById('<%--=txtOutOfMarksHssc.ClientID--%>').value;      
            if (val1 != '') {
                var sum = Number(300 + parseFloat(val.value));
                //   alert(sum);             
                //document.getElementById('<%=txtOutOfMarksHssc.ClientID%>').value = sum;
                return false;
            }
            var sum = 0;
            if (val == '') {
                alert('error');
                val = 0;
            }
            else {
                sum += Number(val.value);
            }
            document.getElementById('<%=txtOutOfMarksHssc.ClientID%>').value = sum;
        }

    </script>


    <script type="text/javascript">
        function SumTotal(val) {
            debugger;
            var val1 = 300;//document.getElementById('<%--=txtOutOfMarksHssc.ClientID--%>').value;      
            if (val1 != '') {
                var sum = Number(300 + parseFloat(val.value));
                //   alert(sum);             
                //document.getElementById('<%=txtOutOfMarksHssc.ClientID%>').value = sum;
                return false;
            }
            var sum = 0;
            if (val == '') {
                alert('error');
                val = 0;
            }
            else {
                sum += Number(val.value);
            }
            document.getElementById('<%=txtOutOfMarksHssc.ClientID%>').value = sum;
        }

    </script>



    <script type="text/javascript">
        function TotPer(txt) {

            debugger;

            findTotal(txt);


            var numVal1 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtMarksObtainedHssc").value);
            var numVal2 = (300 + Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmarktotal").value));

            //alert(numVal1);
            //  alert(numVal2);

            //var totalValue = Number(numVal1 / numVal2) * 100;
            //document.getElementById('<%=txtPercentageHssc.ClientID %>').value = ((numVal1 / numVal2) * 100).toFixed(2);
            //  document.getElementById("ctl00_ContentPlaceHolder1_txtPercentageHssc").value = totalValue.toFixed(2);

            //return false;
            document.getElementById('<%=txtPercentageHssc.ClientID %>').value = ((numVal1 / numVal2) * 100).toFixed(2);

        }

    </script>

    <script type="text/javascript">
        function TotPercet() {
            var numVal1 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtMarksObtainedHssc").value);
            var numVal2 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmarktotal").value);
            var OutOfMarks = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtOutOfMarksHssc").value);
            if (OutOfMarks == null || OutOfMarks == '') {
                numVal = (300 + Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmarktotal").value));
                //alert(numVal2);
            }
            else {
                return false;
                numVal = (Number(document.getElementById("ctl00_ContentPlaceHolder1_txtOutOfMarksHssc").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmarktotal").value));
                //alert(numVal2);
            }
            return false;
            document.getElementById('<%=txtPercentageHssc.ClientID %>').value = ((numVal1 / numVal) * 100).toFixed(2);
            //  document.getElementById("ctl00_ContentPlaceHolder1_txtPercentageHssc").value = totalValue.toFixed(2);

        }

    </script>

    <script>
        function calculatePercentage() {
            var marksObtained = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtMarksObtainedHssc").value) || 0;
            var outOfMarks = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtOutOfMarksHssc").value) || 0;

            if (marksObtained > outOfMarks) {
                alert('Obtained Marks should not be Greater than Total Marks');
                document.getElementById("ctl00_ContentPlaceHolder1_txtMarksObtainedHssc").value = '';
                document.getElementById("ctl00_ContentPlaceHolder1_txtMarksObtainedHssc").focus();
                return;
            }
            var percentage = (marksObtained / outOfMarks) * 100;

            document.getElementById("ctl00_ContentPlaceHolder1_txtPercentageHssc").value = percentage.toFixed(2);
        }

    </script>

    <asp:HiddenField ID="hdnMarksheetNo" runat="server" />
    <asp:HiddenField ID="hdnOrgId" runat="server" />
    <asp:HiddenField ID="hdnDegree" runat="server" />
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div4" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT INFORMATION</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-2 col-md-4 col-12" id="divtabs" runat="server">
                                <aside class="sidebar">

                                    <!-- sidebar: style can be found in sidebar.less -->
                                    <section class="sidebar" style="background-color: #ffffff">
                                        <ul class="sidebar-menu">
                                            <!-- Optionally, you can add icons to the links -->
                                            <li class="treeview" id="divhome" runat="server">
                                                <i class="fa fa-search"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkGoHome"
                                                        ToolTip="Please Click Here To Go To Home" OnClick="lnkGoHome_Click" Text="Search New Student"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>
                                            <li class="treeview">
                                                <i class="fa fa-user"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                        ToolTip="Please select Personal Details" OnClick="lnkPersonalDetail_Click" Text="Personal Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview ">
                                                <i class="fa fa-map-marker"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                        ToolTip="Please select Address Details" OnClick="lnkAddressDetail_Click" Text="Address Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview" id="divadmissiondetailstreeview" runat="server">
                                                <i class="fa fa-university"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkAdmissionDetail"
                                                        ToolTip="Please select Admission Details" OnClick="lnkAdmissionDetail_Click" Text="Admission Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview" style="display: none">
                                                <i class="fa fa-info-circle"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkDasaStudentInfo"
                                                        ToolTip="Please select DASA Student Information" Text="Information"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview">
                                                <i class="fa fa-file"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkUploadDocument"
                                                        ToolTip="Please Upload Documents" OnClick="lnkUploadDocument_Click" Text="Document Upload"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>
                                            <li class="treeview active">
                                                <i class="fa fa-graduation-cap"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkQualificationDetail"
                                                        ToolTip="Please select Qualification Details" OnClick="lnkQualificationDetail_Click" Text="Qualification Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview">
                                                <i class="fa fa-stethoscope"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkCovid" Visible="true"
                                                        ToolTip="Covid Vaccination Details" OnClick="lnkCovid_Click" Text="Covid Information"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview">
                                                <i class="fa fa-link"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkotherinfo"
                                                        ToolTip="Please select Other Information." OnClick="lnkotherinfo_Click" Text="Other Information"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview" id="divAdmissionApprove" runat="server">
                                                <i class="fa fa-check-circle"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkApproveAdm"
                                                        ToolTip="Verify Information" OnClick="lnkApproveAdm_Click" Text="Verify Information"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview" id="divPrintReport" runat="server" visible="false">
                                                <i class="fas fa-print"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkprintapp" OnClick="lnkprintapp_Click" Text="Print"></asp:LinkButton>
                                                </span>
                                            </li>
                                        </ul>
                                    </section>
                                </aside>
                            </div>
                            <%--Added IDs to the div(s) and sup(s) by Shrikant W. on 26-12-2023--%>
                            <div class="col-lg-10 col-md-8 col-12">
                                <div class="col-12 pl-0 pr-0 pl-lg-2 pr-lg-2">
                                    <div id="divStudentLastQualification" runat="server">
                                        <asp:Panel ID="pnlHssc" runat="server">
                                            <div class="row" id="divSecMArks" runat="server">
                                                <div class="col-md-12">
                                                    <div class="sub-heading">
                                                        <h5>Secondary/10th Marks</h5>
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divSscCollegeName" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supSscSchoolName" runat="server">* </sup>
                                                                <label>School/College Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSchoolCollegeNameSsc" runat="server" CssClass="form-control"
                                                                TabIndex="136" placeholder="Enter School/College Name"
                                                                ToolTip="Please Enter School/College Name"></asp:TextBox>
                                                            <%-- <ajaxToolKit:FilteredTextBoxExtender ID="fteSchoolCollegeNameSsc" runat="server"
                                                    TargetControlID="txtSchoolCollegeNameSsc" FilterType="Custom" FilterMode="InvalidChars"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />--%>
                                                            <%--<asp:RequiredFieldValidator ID="rfvSchoolName" runat="server" ControlToValidate="txtSchoolCollegeNameSsc"
                                                                Display="None" ErrorMessage="Please Enter Secondary/10th School/College Name"
                                                                SetFocusOnError="True" ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSscBoard" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supSscBoard" runat="server">* </sup>
                                                                <label>Board </label>
                                                            </div>
                                                            <asp:TextBox ID="txtBoardSsc" runat="server" TabIndex="137" placeholder="Enter Board"
                                                                ToolTip="Please Enter SSC Board" onkeypress="return alphaOnly(event);" CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteBoardSsc" runat="server" TargetControlID="txtBoardSsc"
                                                                FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                            <%--<asp:RequiredFieldValidator ID="rfvBoardSsc" runat="server" ControlToValidate="txtBoardSsc"
                                                                Display="None" ErrorMessage="Please Enter SSC Board" SetFocusOnError="True" ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSscYearOfExam" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supSscYearOfExam" runat="server">* </sup>
                                                                <label>Year Of Exam</label>
                                                            </div>
                                                            <asp:TextBox ID="txtYearOfExamSsc" runat="server" onkeyup="validateNumeric(this);"
                                                                TabIndex="138" ToolTip="Please Enter Secondary/10th Year Of Exam" placeholder="Enter Year Of Exam"
                                                                CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvYearOfExamssc" runat="server" ControlToValidate="txtYearOfExamSsc"
                                                                Display="None" ErrorMessage="Please Enter Secondary/10th Year Of Exam" SetFocusOnError="True"
                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSscMedium" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supSscMedium" runat="server">* </sup>
                                                                <label>Medium</label>
                                                            </div>

                                                            <asp:TextBox ID="txtSSCMedium" runat="server" CssClass="form-control" placeholder="Enter Medium"
                                                                ToolTip="Please Enter Medium in Secondary/10th Exam" TabIndex="139" MaxLength="50"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender49" runat="server"
                                                                TargetControlID="txtSSCMedium" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />
                                                            <%--    <asp:RequiredFieldValidator ID="rfvSSCMedium" runat="server" ControlToValidate="txtSSCMedium"
                                            Display="None" ErrorMessage="Please Enter SSC Medium" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDivisionSsc" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supDivisionSsc" runat="server">* </sup>
                                                                <label>Division</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDivisionSsc" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                AppendDataBoundItems="True" TabIndex="8" ToolTip="Please Select Division">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Distinction</asp:ListItem>
                                                                <asp:ListItem Value="2">First Class</asp:ListItem>
                                                                <asp:ListItem Value="3">Second Class</asp:ListItem>
                                                                <asp:ListItem Value="4">Third Class</asp:ListItem>
                                                                <asp:ListItem Value="5">NA</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSscMarksObtained" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supSscMarksObtained" runat="server">* </sup>
                                                                <label>Marks/GPA Obtained </label>
                                                            </div>
                                                            <asp:TextBox ID="txtMarksObtainedSsc" onkeypress="return isNumberKey(this, event);;" runat="server"
                                                                placeholder="Enter Marks Obtained" ToolTip="Please Enter Marks Obtained" MaxLength="3"
                                                                TabIndex="140" CssClass="form-control" onchange="calc(1);"></asp:TextBox>
                                                            <%--    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender57" runat="server"
                                                                FilterType="Numbers" TargetControlID="txtMarksObtainedSsc">
                                                            </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMarksObtainedSsc"
                                                                Display="None" ErrorMessage="Please Enter Secondary/10th Obtained Marks" SetFocusOnError="True"
                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            <%-- OnTextChanged="txtMarksObtainedSsc_TextChanged"--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSscOutOfMarksObtained" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supOutOfMarksObtained" runat="server">* </sup>
                                                                <label>Out Of Marks/GPA </label>
                                                            </div>
                                                            <asp:TextBox ID="txtOutOfMarksSsc" onkeyup="validateNumeric(this);" placeholder=" Enter Out Of Marks"
                                                                ToolTip="Please Enter Out Of Marks" onchange="calc(1);" runat="server" MaxLength="3"
                                                                TabIndex="141" CssClass="form-control"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvOutOfMarksSsc" runat="server" ControlToValidate="txtOutOfMarksSsc"
                                                                Display="None" ErrorMessage="Please Enter Secondary/10th Out Of Marks" SetFocusOnError="True"
                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            <%-- onblur="calPercentage(this,'ssc'),validateSscMarksCam(this)"--%><%-- OnTextChanged="txtOutOfMarksSsc_TextChanged" AutoPostBack="true"--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSscPercentage" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supSscPercentage" runat="server">* </sup>
                                                                <label>Percentage</label>
                                                            </div>

                                                            <asp:TextBox ID="txtPercentageSsc" CssClass="form-control" runat="server" onkeyup="validateNumeric(this);"
                                                                MaxLength="5" ToolTip="Please Enter Percentage" placeholder="Enter Percentage"
                                                                TabIndex="142"></asp:TextBox>
                                                            <asp:CompareValidator ID="rfvPercentageSSC" runat="server" ControlToValidate="txtPercentageSsc"
                                                                Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                                SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSscSeatNo" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supSscSeatNo" runat="server">* </sup>
                                                                <label>Seat No.</label>
                                                            </div>

                                                            <asp:TextBox ID="txtExamRollNoSsc" runat="server" CssClass="form-control" TabIndex="143" MaxLength="10"
                                                                placeholder="Enter Seat No." ToolTip="Please Enter Seat No."></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteExamRollNoSsc" runat="server" FilterType="Custom"
                                                                TargetControlID="txtExamRollNoSsc" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <%-- <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" TabIndex="123" ToolTip="Please Enter Exam Roll No."></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtExamRollNoHssc" FilterType="Custom" FilterMode="InvalidChars"
                                                 />--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <label>DGPA/CGPA</label>
                                                            </div>

                                                            <asp:TextBox ID="txtPercentileSsc" onkeyup="validateNumeric(this);" runat="server"
                                                                ToolTip="Please Enter DGPA/CGPA" CssClass="form-control" TabIndex="144"></asp:TextBox>
                                                            <asp:CompareValidator ID="rfvPercentileSSC" runat="server" ControlToValidate="txtPercentileSsc"
                                                                Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                                SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <label>Grade</label>
                                                            </div>

                                                            <asp:TextBox ID="txtGradeSsc" runat="server" ToolTip="Please Enter Grade" TabIndex="145"
                                                                CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender50" runat="server"
                                                                TargetControlID="txtGradeSsc" FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_=,./:;<>?'{}[]\|&&quot;'1234567890" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <label>Attempts</label>
                                                            </div>

                                                            <asp:TextBox ID="txtAttemptSsc" runat="server" ToolTip="Please Enter Attempts" CssClass="form-control"
                                                                TabIndex="146" MaxLength="3"></asp:TextBox>
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPercentileSsc"
                                                                Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                                SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender46" runat="server"
                                                                TargetControlID="txtAttemptSsc" FilterType="Numbers" />
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divMarksheetNoSsc" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supMarksheetNoSsc" runat="server">* </sup>
                                                                <label>Marksheet Number</label>
                                                            </div>
                                                            <asp:TextBox ID="txtMarksheetNoSsc" runat="server" CssClass="form-control"
                                                                Rows="1" ToolTip="Marksheet Number" placeholder=" Enter Marksheet Number"
                                                                MaxLength="20" TabIndex="1" onkeypress="allowAlphaNumericSpaceHyphen(event)"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divSscSchoolAddress" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supSscSchoolAddress" runat="server">* </sup>
                                                                <label>School/College Address </label>
                                                            </div>
                                                            <asp:TextBox ID="txtSSCSchoolColgAdd" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                                Rows="1" ToolTip="School/College Address" placeholder=" Enter School/College Address"
                                                                MaxLength="100" TabIndex="147"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel2" runat="server">
                                            <div class="row" id="DivHigherEdu" runat="server">
                                                <div class="col-md-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>Higher Secondary/12th Marks / Diploma Marks</h5>
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                                            <asp:RadioButton ID="rdoHsc" runat="server" GroupName="Qual" Text="Higher Secondary/12th Marks"
                                                                OnCheckedChanged="rdoHsc_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;&nbsp;
                                                            <asp:RadioButton ID="rdoDiploma" runat="server" GroupName="Qual" Text="Diploma Marks"
                                                                OnCheckedChanged="rdoDiploma_CheckedChanged" AutoPostBack="true" />
                                                        </div>
                                                    </div>

                                                    <asp:Panel ID="pnlHsc" runat="server">
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divHscSchoolName" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supHscSchoolName" runat="server">*</sup>
                                                                    <label>School/College Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSchoolCollegeNameHssc" runat="server" CssClass="form-control"
                                                                    TabIndex="148" placeholder=" Enter School/College Name" ToolTip="Please Enter Higher Secondary/12th School/College Name"></asp:TextBox>
                                                                <%-- <ajaxToolKit:FilteredTextBoxExtender ID="fteSchoolCollegeHssc" runat="server" TargetControlID="txtSchoolCollegeNameHssc"
                                                        FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />--%>

                                                                <%--  <asp:TextBox ID="txtSchoolCollegeNameHssc" runat="server" CssClass="form-control"
                                                                    TabIndex="148" placeholder=" Enter School/College Name" ToolTip="Please Enter Higher Secondary/12th School/College Name" onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));"></asp:TextBox>--%>
                                                                <%--<asp:RequiredFieldValidator ID="rfvCollageName" runat="server" ControlToValidate="txtSchoolCollegeNameHssc"
                                                                    Display="None" ErrorMessage="Please Enter Higher Secondary/12th School/College Name"
                                                                    SetFocusOnError="True" ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divHscBoard" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supHscBoard" runat="server">* </sup>
                                                                    <label>Board </label>
                                                                </div>
                                                                <asp:TextBox ID="txtBoardHssc" runat="server" placeholder="Enter Board" ToolTip="Please Enter Board"
                                                                    TabIndex="149" CssClass="form-control"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteBoardHssc" runat="server" TargetControlID="txtBoardHssc"
                                                                    FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />
                                                                <%--<asp:RequiredFieldValidator ID="rfvBoard" runat="server" ControlToValidate="txtBoardHssc"
                                                                    Display="None" ErrorMessage="Please Enter HSSC Board" SetFocusOnError="True"
                                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divHscYearOfExam" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supHscYearOfExam" runat="server">* </sup>
                                                                    <label>Year Of Exam </label>
                                                                </div>
                                                                <asp:TextBox ID="txtYearOfExamHssc" placeholder="Enter Year Of Exam" onkeyup="validateNumeric(this);"
                                                                    runat="server" TabIndex="150" ToolTip="Please Enter Higher Secondary/12th Year Of Exam"
                                                                    CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvExamYear" runat="server" ControlToValidate="txtYearOfExamHssc"
                                                                    Display="None" ErrorMessage="Please enter Higher Secondary/12th Year Of Exam"
                                                                    SetFocusOnError="True" ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divHscMedium" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supHscMedium" runat="server">* </sup>
                                                                    <label>Medium</label>
                                                                </div>

                                                                <asp:TextBox ID="txtHSSCMedium" runat="server" CssClass="form-control" placeholder="Enter Medium"
                                                                    ToolTip="Please Enter Medium in Higher Secondary/12th Exam" TabIndex="151" onkeypress="return alphaOnly(event);"
                                                                    MaxLength="50"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server"
                                                                    TargetControlID="txtHSSCMedium" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                                <%-- <asp:RequiredFieldValidator ID="rfvMedium" runat="server" ControlToValidate="txtHSSCMedium"
                                            Display="None" ErrorMessage="Please Enter HSSC Medium" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDivisionHsc" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDivisionHsc" runat="server">* </sup>
                                                                    <label>Division</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDivisionHsc" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                    AppendDataBoundItems="True" TabIndex="8" ToolTip="Please Select Division">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Distinction</asp:ListItem>
                                                                    <asp:ListItem Value="2">First Class</asp:ListItem>
                                                                    <asp:ListItem Value="3">Second Class</asp:ListItem>
                                                                    <asp:ListItem Value="4">Third Class</asp:ListItem>
                                                                    <asp:ListItem Value="5">NA</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divHscSeatNo" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supHscSeatNo" runat="server">* </sup>
                                                                    <label>Seat No.</label>
                                                                </div>

                                                                <asp:TextBox ID="txtExamRollNoHssc" CssClass="form-control" runat="server" TabIndex="152"
                                                                    placeholder="Enter Seat No." ToolTip="Please Enter Seat No." MaxLength="10"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server"
                                                                    TargetControlID="txtExamRollNoHssc" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divMarksheetNoHsc" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supMarksheetNoHsc" runat="server">* </sup>
                                                                    <label>Marksheet Number</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMarksheetNoHsc" runat="server" CssClass="form-control"
                                                                    Rows="1" ToolTip="Marksheet Number" placeholder=" Enter Marksheet Number"
                                                                    MaxLength="20" TabIndex="1" onkeypress="allowAlphaNumericSpaceHyphen(event)"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-12 col-12">
                                                                <table class="table table-bordered">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Subject</th>
                                                                            <th>Obtained Marks</th>
                                                                            <th>Total Marks</th>
                                                                            <th>Precentage</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>Physics</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtphymark" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    onchange="findTotal(this);" onblur="TotPer(this);" TabIndex="153" placeholder="Enter Physics's Marks"
                                                                                    ToolTip="Please Enter Physics's Mark" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtphymark">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtphymarktotal" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    TabIndex="154" onchange="SumTotal(this);" placeholder="Enter Physics's Marks"
                                                                                    ToolTip="Please Enter Physics's Mark" onkeyup="validateNumeric(this);" Text="100"
                                                                                    Enabled="false"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtphymarktotal">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Chemistry</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtchem" runat="server" CssClass="form-control" MaxLength="4" onchange="findTotal(this);"
                                                                                    onblur="TotPer(this);" TabIndex="155" placeholder="Enter Chemistry Marks" ToolTip="Please Enter Physics's Mark"
                                                                                    onkeyup="validateNumeric(this);"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtchem">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtchemtot" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    onchange="SumTotal(this);" TabIndex="156" placeholder="Enter Chemistry's Marks"
                                                                                    ToolTip="Please Enter Physics's Mark" onkeyup="validateNumeric(this);" Text="100"
                                                                                    Enabled="false"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtchemtot">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Mathematics</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtmaths" runat="server" CssClass="form-control" MaxLength="4" onchange="findTotal(this);"
                                                                                    onblur="TotPer(this);" TabIndex="157" placeholder="Enter Math's Marks" ToolTip="Please Enter Physics's Mark"
                                                                                    onkeyup="validateNumeric(this);"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtmaths">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>


                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtmathstot" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    onchange="SumTotal(this);" TabIndex="158" placeholder="Enter Math's Marks" ToolTip="Please Enter Physics's Mark"
                                                                                    onkeyup="validateNumeric(this);" Text="100" Enabled="false"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtmathstot">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <%--                                                                        <div style="clear:b"></div>--%>
                                                                        <tr id="trBiology" runat="server" style="display: block">
                                                                            <td>Biology</td>

                                                                            <td>
                                                                                <asp:TextBox ID="txtbiology" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    onchange="findTotal(this);" onblur="TotPer(this);" TabIndex="153" placeholder="Enter Biology's Marks"
                                                                                    ToolTip="Please Enter Physics's Mark" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtbiology">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtbiologytot" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    TabIndex="154" onchange="SumTotal(this);" placeholder="Enter Biology's Marks"
                                                                                    ToolTip="Please Enter Biology's Mark" onkeyup="validateNumeric(this);" Text="100"
                                                                                    Enabled="false"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtbiologytot">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>PCM/PCB Total</b></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPcmMarks" onkeyup="validateNumeric(this);" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    TabIndex="158" Enabled="false"></asp:TextBox>
                                                                                <%-- Added Enabled=false sachin by on 20-07-2022--%>
                                                                                <asp:HiddenField ID="hfdPcmMarks" runat="server" ClientIDMode="Static" />

                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPCMTotal" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    TabIndex="158" Text="300" Enabled="false" onchange=""></asp:TextBox>
                                                                                <%-- Added Enabled=false sachin by on 20-07-2022--%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPcmPerct" runat="server" onkeyup="validateNumeric(this);" CssClass="form-control" MaxLength="4"
                                                                                    TabIndex="158" Enabled="false"></asp:TextBox>
                                                                                <%-- Added Enabled=false sachin by on 20-07-2022--%>
                                                                                <asp:HiddenField ID="hfdPcmPer" runat="server" ClientIDMode="Static" />
                                                                            </td>

                                                                        </tr>

                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtVocsub" runat="server" CssClass="form-control" TabIndex="159"
                                                                                    placeholder="Enter Vocational Subject" ToolTip="Please Enter Vocational Subject"
                                                                                    onkeypress="return alphaOnly(event);"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtVocationalmark" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    TabIndex="160" placeholder="Enter Vocational sub Marks" ToolTip="Please Enter Physics's Mark"
                                                                                    onkeyup="validateNumeric(this);" oninput="calculateMarksObtained(this);" onblur="calculatePercentage();" onchange="validateVocDetails();"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtVocationalmark">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtVocationalmarktotal" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    TabIndex="161" placeholder="Enter Vocational sub Marks" ToolTip="Please Enter Physics's Mark"
                                                                                    onkeyup="validateNumeric(this);" oninput="calculateTotalMarks(this);" onblur="calculatePercentage();" onchange="validateVocDetails();"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtVocationalmarktotal">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td id="divHscTotalMarks" runat="server"><sup id="supHscTotalMarks" runat="server">*</sup><b>Total Marks</b></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMarksObtainedHssc" runat="server" CssClass="form-control" MaxLength="4"
                                                                                    TabIndex="162" placeholder="Enter Marks Obtained" ToolTip="Please Enter Marks Obtained"
                                                                                    name="txtMarksObtainedHssc" onkeyup="validateNumeric(this);" oninput="calculatePercentage();"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtmnarksobtain" runat="server" FilterType="Numbers"
                                                                                    TargetControlID="txtMarksObtainedHssc">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                                <%--<asp:RequiredFieldValidator ID="rfvMarksObtainedHssc" runat="server" ControlToValidate="txtMarksObtainedHssc"
                                                                                    Display="None" ErrorMessage="Please Enter Higher Secondary/12th Obtained Marks"
                                                                                    SetFocusOnError="True" ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                                            </td>

                                                                            <td>
                                                                                <asp:TextBox ID="txtOutOfMarksHssc" onkeyup="validateNumeric(this);" placeholder="Enter Total Marks" Enabled="true"
                                                                                    ToolTip="Please Enter Total Out Of Marks" onchange="calculatePercentage();"
                                                                                    runat="server" MaxLength="4" CssClass="form-control" TabIndex="163" Text="600"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtOutOfMarksHssc">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPercentageHssc" onkeyup="validateNumeric(this);" runat="server" MaxLength="4"
                                                                                    ToolTip="Please Enter Percentage" CssClass="form-control"
                                                                                    ValidationGroup="Academic" TabIndex="164"></asp:TextBox>
                                                                                <asp:CompareValidator ID="rfvPercentage" runat="server" ControlToValidate="txtPercentageHssc"
                                                                                    Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                                                    SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <label>DGPA/CGPA</label>
                                                                </div>

                                                                <asp:TextBox ID="txtPercentileHssc" runat="server" placeholder="Enter DGPA/CGPA"
                                                                    ToolTip="Please Enter Higher Secondary/12th DGPA/CGPA" CssClass="form-control"
                                                                    onkeyup="validateNumeric(this);" ValidationGroup="Academic" TabIndex="156"></asp:TextBox>
                                                                <asp:CompareValidator ID="rfvPercentile" runat="server" ControlToValidate="txtPercentileHssc"
                                                                    Display="None" ErrorMessage="Please Enter Higher Secondary/12th DGPA/CGPA" Operator="DataTypeCheck"
                                                                    SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <label>Grade</label>
                                                                </div>

                                                                <asp:TextBox ID="txtGradeHssc" runat="server" placeholder="Enter Grade" ToolTip="Please Enter Higher Secondary/12th Grade"
                                                                    onkeypress="return alphaOnly(event);" CssClass="form-control" TabIndex="157"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server"
                                                                    TargetControlID="txtGradeHssc" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_=,./:;<>?'{}[]\|&&quot;'" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <label>Attempts</label>
                                                                </div>

                                                                <asp:TextBox ID="txtAttemptHssc" runat="server" placeholder="Enter Attempt" ToolTip="Please Enter Attempt"
                                                                    CssClass="form-control" TabIndex="158" MaxLength="3"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender47" runat="server"
                                                                    TargetControlID="txtAttemptHssc" FilterType="Numbers" />
                                                            </div>
                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divHscCollegeAddress" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supHscCollegeAddress" runat="server">* </sup>
                                                                    <label>School/College Address</label>
                                                                </div>

                                                                <asp:TextBox ID="txtHSCColgAddress" runat="server" CssClass="form-control"
                                                                    TextMode="MultiLine" TabIndex="165" placeholder=" Enter School/College Address"
                                                                    ToolTip="School/College Address" MaxLength="100" Rows="1"></asp:TextBox>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divNataMarks" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <label>NATA Marks</label>
                                                                </div>
                                                                <asp:TextBox ID="txtNataMarks" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                                    ToolTip="Enter Nata Marks" placeholder="Enter NATA Marks" AutoComplete="off"
                                                                    MaxLength="6" TabIndex="165"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-12" style="display: none">
                                                                <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                                    <h4>
                                                                        <b>Enter PM and CHE/B/COMP/E/DIPLOMA FINAL YEAR MARK</b></h4>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <div class="form-group col-md-3">
                                                                        <label>
                                                                            Marks Obtained</label>
                                                                        <asp:TextBox ID="txtHscChe" onkeyup="validateNumeric(this);" runat="server" placeholder="Marks Obtained"
                                                                            ToolTip="Please Enter Higher Secondary/12th CHE" MaxLength="4" CssClass="form-control"
                                                                            TabIndex="160"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                        <div class="row">
                                                                            <label>
                                                                                &nbsp;&nbsp;&nbsp;Out Of Marks</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtHscCheMax" onkeyup="validateNumeric(this);" onblur="calPercentage(this,'pcm'),validateHscMarksCam(this)"
                                                                            runat="server" MaxLength="4" placeholder="Out Of Marks" ToolTip="Please Enter Higher Secondary/12th CHE MAX"
                                                                            CssClass="form-control" TabIndex="161"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                        <label>
                                                                            Percentage</label>
                                                                        <asp:TextBox ID="txtHscPhy" onkeyup="validateNumeric(this);" runat="server" MaxLength="3"
                                                                            placeholder=" Enter Percentage" CssClass="form-control" TabIndex="162"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-2" style="display: none">
                                                                        <div class="row">
                                                                            <label>
                                                                                &nbsp;&nbsp;&nbsp;PCM-PHY-MAX</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtHscPhyMax" onkeyup="validateNumeric(this);" runat="server" ToolTip="Please Enter Higher Secondary/12th PHY MAX"
                                                                            MaxLength="3" CssClass="form-control" TabIndex="131"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-2" style="display: none">
                                                                        <label>
                                                                            HSSC-ENG</label>
                                                                        <asp:TextBox ID="txtHscEngHssc" onkeyup="validateNumeric(this);" runat="server" ToolTip="Please Enter Higher Secondary/12th ENG"
                                                                            MaxLength="3" CssClass="form-control" TabIndex="132"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-2" style="display: none">
                                                                        <div class="row">
                                                                            <label>
                                                                                &nbsp;&nbsp;&nbsp;HSSC-ENG-MAX</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtHscEngMaxHssc" onkeyup="validateNumeric(this);" runat="server"
                                                                            ToolTip="Please Enter Higher Secondary/12th ENG MAX" MaxLength="3" CssClass="form-control"
                                                                            TabIndex="133"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-2" style="display: none">
                                                                        <label>
                                                                            PCM-MATHS</label>
                                                                        <asp:TextBox ID="txtHscMaths" onkeyup="validateNumeric(this);" runat="server" ToolTip="Please Enter Higher Secondary/12th MATHS"
                                                                            MaxLength="3" CssClass="form-control" TabIndex="134"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-3" style="display: none">
                                                                        <div class="row">
                                                                            <label>
                                                                                &nbsp;&nbsp;&nbsp;PCM-MATHS-MAX</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtHscMathsMax" onkeyup="validateNumeric(this);" Width="60%" runat="server"
                                                                            MaxLength="3" ToolTip="Please Enter Higher Secondary/12th MATHS MAX" CssClass="form-control"
                                                                            TabIndex="135"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div style="display: none">
                                                                <div class="form-group col-md-1">
                                                                    <label>
                                                                        HSSC-PCM</label>
                                                                    <asp:TextBox ID="txtHscPcmHssc" onkeyup="validateNumeric(this);" runat="server" MaxLength="3"
                                                                        ToolTip="Please Enter Higher Secondary/12th PCM Marks" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group col-md-1">
                                                                    <label>
                                                                        HSSC-PCM-MAX</label>
                                                                    <asp:TextBox ID="txtHscPcmMaxHssc" onkeyup="validateNumeric(this);" runat="server"
                                                                        MaxLength="3" ToolTip="Please Enter Higher Secondary/12th PCM MAX" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group col-md-1">
                                                                    <label>
                                                                        HSSC-BIO</label>
                                                                    <asp:TextBox ID="txtHscBioHssc" onkeyup="validateNumeric(this);" runat="server" ToolTip="Please Enter Higher Secondary/12th BIO"
                                                                        MaxLength="3" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group col-md-1">
                                                                    <label>
                                                                        HSSC-BIO-MAX</label>
                                                                    <asp:TextBox ID="txtHscBioMaxHssc" onkeyup="validateNumeric(this);" runat="server"
                                                                        MaxLength="3" ToolTip="Please enter Higher Secondary/12th BIO MAX" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                                <asp:Panel ID="pnlDiploma" runat="server" Visible="false">
                                                    <div class="col-md-12 mt-3">
                                                        <div class="sub-heading">
                                                            <h5>Diploma Marks</h5>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divDiplomaCollegeName" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDiplomaCollegeName" runat="server">* </sup>
                                                                    <label>School/College Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSchoolCollegeNameDiploma" runat="server" CssClass="form-control"
                                                                    TabIndex="166" placeholder=" Enter Diploma School/College Name" ToolTip="Please Enter Diploma School/College Name"></asp:TextBox>
                                                                <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtSchoolCollegeNameDiploma"
                                                        FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />--%>
                                                                <%--<asp:RequiredFieldValidator ID="rfvDipCollageName" runat="server" ControlToValidate="txtSchoolCollegeNameDiploma"
                                                                    Display="None" ErrorMessage="Please Enter Diploma School/College Name" SetFocusOnError="True"
                                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDiplomaBoard" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDiplomaBoard" runat="server">* </sup>
                                                                    <label>Board</label>
                                                                </div>
                                                                <asp:TextBox ID="txtBoardDiploma" runat="server" placeholder="Enter Board" ToolTip="Please Enter Board"
                                                                    TabIndex="167" CssClass="form-control"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                                    TargetControlID="txtBoardDiploma" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />
                                                                <%--<asp:RequiredFieldValidator ID="rfvBoardDip" runat="server" ControlToValidate="txtBoardDiploma"
                                                                    Display="None" ErrorMessage="Please Enter Diploma Board" SetFocusOnError="True"
                                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDiplomaYearOfExam" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDiplomaYearOfExam" runat="server">* </sup>
                                                                    <label>Year Of Exam</label>
                                                                </div>
                                                                <asp:TextBox ID="txtYearOfExamDiploma" onkeyup="validateNumeric(this);" placeholder="Year Of Exam"
                                                                    runat="server" TabIndex="168" ToolTip="Please Enter Diploma Year Of Exam" CssClass="form-control"
                                                                    MaxLength="4"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvExamYearDiploma" runat="server" ControlToValidate="txtYearOfExamDiploma"
                                                                    Display="None" ErrorMessage="Please enter Diploma Year Of Exam" SetFocusOnError="True"
                                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDiplomaMedium" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDiplomaMedium" runat="server">* </sup>
                                                                    <label>Medium</label>
                                                                </div>

                                                                <asp:TextBox ID="txtDiplomaMedium" runat="server" CssClass="form-control" placeholder=" Enter Medium in Diploma Exam"
                                                                    ToolTip="Please Enter Medium in Diploma Exam" TabIndex="169" onkeypress="return alphaOnly(event);"
                                                                    MaxLength="50"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                    TargetControlID="txtDiplomaMedium" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                                <%-- <asp:RequiredFieldValidator ID="rfvMedium" runat="server" ControlToValidate="txtHSSCMedium"
                                            Display="None" ErrorMessage="Please Enter HSSC Medium" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDivisionDiploma" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDivisionDiploma" runat="server">* </sup>
                                                                    <label>Division</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDivisionDiploma" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                    AppendDataBoundItems="True" TabIndex="8" ToolTip="Please Select Division">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Distinction</asp:ListItem>
                                                                    <asp:ListItem Value="2">First Class</asp:ListItem>
                                                                    <asp:ListItem Value="3">Second Class</asp:ListItem>
                                                                    <asp:ListItem Value="4">Third Class</asp:ListItem>
                                                                    <asp:ListItem Value="5">NA</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDiplomaMarksObtained" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDiplomaMarksObtained" runat="server">* </sup>
                                                                    <label>Marks Obtained</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMarksObtainedDiploma" runat="server" CssClass="form-control"
                                                                    MaxLength="4" TabIndex="170" placeholder="Enter Diploma Marks Obtained" ToolTip="Please Enter Diploma Marks Obtained"
                                                                    onchange="calc(3);" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                                    FilterType="Numbers" TargetControlID="txtMarksObtainedDiploma">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMarksObtainedDiploma"
                                                                    Display="None" ErrorMessage="Please Enter Diploma Obtained Marks" SetFocusOnError="True"
                                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>--%><%-- OnTextChanged="txtMarksObtainedDiploma_TextChanged" AutoPostBack="true" --%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDiplomaOutOfMarks" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDiplomaOutOfMarks" runat="server">* </sup>
                                                                    <label>Out Of Marks</label>
                                                                </div>
                                                                <asp:TextBox ID="txtOutOfMarksDiploma" onkeyup="validateNumeric(this);" placeholder="Enter Out Of Marks"
                                                                    ToolTip="Please Enter Out Of Marks" onchange="calc(3);" runat="server" MaxLength="4"
                                                                    CssClass="form-control" TabIndex="171"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtOutOfMarksDiploma"
                                                                    Display="None" ErrorMessage="Please Enter Diploma Out Of Marks" SetFocusOnError="True"
                                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                                <%-- onblur="calPercentage(this,'diploma'),validateHscMarksCam(this)"--%>
                                                                <%--OnTextChanged="txtOutOfMarksDiploma_TextChanged" AutoPostBack="true" --%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDiplomaPercentage" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDiplomaPercentage" runat="server">* </sup>
                                                                    <label>Percentage</label>
                                                                </div>

                                                                <asp:TextBox ID="txtPercentageDiploma" onkeyup="validateNumeric(this);" runat="server"
                                                                    ToolTip="Please Enter Percentage" placeholder="Enter Percentage" CssClass="form-control"
                                                                    ValidationGroup="Academic" TabIndex="172" AutoPostBack="true" OnTextChanged="txtOutOfMarksDiploma_TextChanged1"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtPercentageDiploma"
                                                                    Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                                    SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDiplomaSeatNo" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDiplomaSeatNo" runat="server">* </sup>
                                                                    <label>Seat No.</label>
                                                                </div>

                                                                <asp:TextBox ID="txtExamRollNoDiploma" CssClass="form-control" runat="server" placeholder="Enter Seat No."
                                                                    TabIndex="173" ToolTip="Please Enter Seat No."></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                                    TargetControlID="txtExamRollNoDiploma" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divMarksheetNoDiploma" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supMarksheetNoDiploma" runat="server">* </sup>
                                                                    <label>Marksheet Number</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMarksheetNoDiploma" runat="server" CssClass="form-control"
                                                                    Rows="1" ToolTip="Marksheet Number" placeholder=" Enter Marksheet Number"
                                                                    MaxLength="20" TabIndex="1" onkeypress="allowAlphaNumericSpaceHyphen(event)"></asp:TextBox>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <label>DGPA/CGPA</label>
                                                                </div>

                                                                <asp:TextBox ID="txtPercentileDiploma" runat="server" placeholder="Enter Enter Diploma DGPA/CGPA"
                                                                    ToolTip="Please Enter Diploma DGPA/CGPA" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                                    ValidationGroup="Academic" TabIndex="156"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtPercentileDiploma"
                                                                    Display="None" ErrorMessage="Please Enter Diploma DGPA/CGPA" Operator="DataTypeCheck"
                                                                    SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <label>Grade</label>
                                                                </div>

                                                                <asp:TextBox ID="txtGradeDiploma" runat="server" placeholder=" Enter Diploma Grade"
                                                                    ToolTip="Please Enter Diploma Grade" onkeypress="return alphaOnly(event);" CssClass="form-control"
                                                                    TabIndex="157"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                                    TargetControlID="txtGradeDiploma" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_=,./:;<>?'{}[]\|&&quot;'" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <label>Attempts</label>
                                                                </div>

                                                                <asp:TextBox ID="txtAttemptDiploma" runat="server" placeholder="Enter Attempt" ToolTip="Please Enter Attempt"
                                                                    CssClass="form-control" TabIndex="158" MaxLength="3"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                                    TargetControlID="txtAttemptDiploma" FilterType="Numbers" />
                                                            </div>
                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divDiplomaSchoolCollegeAddress" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup id="supDiplomaSchoolCollegeAddress" runat="server">* </sup>
                                                                    <label>School/College Address</label>
                                                                </div>

                                                                <asp:TextBox ID="txtDiplomaColgAddress" runat="server" placeholder="Enter School/College Address"
                                                                    CssClass="form-control" TextMode="MultiLine" TabIndex="174" ToolTip="School/College Address"
                                                                    MaxLength="100" Rows="1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </asp:Panel>

                                        <div id="divEntranceExamScores" runat="server" class="row">
                                            <div class="col-md-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Entrance Exam Scores</h5>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divExamName" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supExamName" runat="server">* </sup>
                                                            <label>Exam Name</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlExamNo" placeholder=" Enter Exam Name" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            AppendDataBoundItems="True" ToolTip="Please Select Exam no." TabIndex="175">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSeatNo" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supSeatNo" runat="server">* </sup>
                                                            <label>Seat No.</label>
                                                        </div>

                                                        <asp:TextBox ID="txtQExamRollNo" runat="server" placeholder=" Enter Seat No." CssClass="form-control" MaxLength="16"
                                                            ToolTip="Please Enter Qualifying Seat No" TabIndex="176"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divYearOfExam" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supYearOfExam" runat="server">* </sup>
                                                            <label>Year of Exam</label>
                                                        </div>
                                                        <asp:TextBox ID="txtYearOfExam" runat="server" CssClass="form-control" placeholder=" Enter Year of Exam"
                                                            TabIndex="177" onkeyup="validateNumeric(this);" ToolTip="Please Enter Year of Exam"
                                                            MaxLength="4"></asp:TextBox>
                                                        <%--  <asp:RequiredFieldValidator ID="rfvYearOfEntranceExam" runat="server" ControlToValidate="txtYearOfExam"
                                    Display="None" SetFocusOnError="true" ValidationGroup="EntranceExam" ErrorMessage="Please Enter Year of Entrance Exam"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <label>State Rank</label>
                                                        </div>

                                                        <asp:TextBox ID="txtStateRank" runat="server" placeholder=" Enter State Rank" CssClass="form-control"
                                                            ToolTip="Please Enter State Rank" TabIndex="109"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteStateRank" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtStateRank">
                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divPercentage" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supPercentage" runat="server">* </sup>
                                                            <asp:Label ID="lblDYtxtPercentile" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>

                                                        <asp:TextBox ID="txtPer" runat="server" CssClass="form-control" placeholder=" Enter Percentile" MaxLength="5"
                                                            ToolTip="Please Enter Percentile" onkeyup="validateNumeric(this);" TabIndex="177"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <label>DGPA/CGPA</label>
                                                        </div>

                                                        <asp:TextBox ID="txtPercentile" runat="server" CssClass="form-control" placeholder=" Enter DGPA/CGPA"
                                                            onkeyup="validateNumeric(this);" ToolTip="Please Enter DGPA/CGPA" TabIndex="167">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamNo"
                                                            ValidationGroup="EntranceExam" Display="None" SetFocusOnError="true" InitialValue="0"
                                                            ErrorMessage="Please Select Exam Name"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divRank" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supRank" runat="server">* </sup>
                                                            <label>Rank</label>
                                                        </div>

                                                        <asp:TextBox ID="txtAllIndiaRank" runat="server" placeholder="Enter Rank" CssClass="form-control" MaxLength="8"
                                                            ToolTip="Please Enter Rank" TabIndex="180" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                            TargetControlID="txtAllIndiaRank" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars=".~`!@#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" />
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-8 col-12" id="divLastSchoolName" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supLastSchoolName" runat="server">* </sup>
                                                            <label>Last School/College Name as per TC/Leaving Certificate</label>
                                                        </div>

                                                        <asp:TextBox ID="txtLastSchoolName" runat="server" placeholder="Enter Last School/College Name as per TC/Leaving Certificate" CssClass="form-control" MaxLength="8"
                                                            ToolTip="Please Enter Last School/College Name as per TC/Leaving Certificate" TabIndex="1"></asp:TextBox>

                                                    </div>



                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <label>Quota</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlQuota" runat="server" placeholder=" Enter Quota" CssClass="form-control"
                                                            AppendDataBoundItems="True" TabIndex="113">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <label>Score</label>
                                                        </div>

                                                        <asp:TextBox ID="txtScore" runat="server" CssClass="form-control" placeholder=" Enter Score"
                                                            onkeyup="validateNumeric(this);" ToolTip="Please Enter Score" TabIndex="169"></asp:TextBox>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="tdpaper" style="display: none;" runat="server">
                                                        <div class="label-dynamic">
                                                            <label id="tdpapertxt" runat="server">
                                                                Paper</label>
                                                        </div>

                                                        <asp:TextBox ID="txtPaper" CssClass="form-control" placeholder=" Enter Paper" runat="server"
                                                            TabIndex="115"></asp:TextBox>
                                                    </div>
                                                    <asp:Panel ID="pnlpapercode" runat="server" Visible="false">
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tdpapercode" runat="server">
                                                            <div class="label-dynamic">
                                                                <label id="tdpapercodetxt" runat="server">
                                                                    Paper Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtpaperCode" CssClass="form-control" placeholder=" Enter Paper Code"
                                                                runat="server"></asp:TextBox>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnAddEntranceExam" runat="server" OnClick="btnAddEntranceExam_Click"
                                                    TabIndex="180" Text="Add" ToolTip="Add Entrance Exam" ValidationGroup="Entrance"
                                                    CssClass="btn btn-primary" />
                                            </div>

                                            <div class="col-md-12">
                                                <asp:Panel ID="divEntrancequal" runat="server" ScrollBars="Auto">
                                                    <asp:ListView ID="lvEntranceExm" runat="server">
                                                        <LayoutTemplate>
                                                            <%-- <div class="vista-grid">--%>
                                                            <div id="demo-grid">
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th style="text-align: center">Edit
                                                                            </th>
                                                                            <th style="text-align: center">Delete
                                                                            </th>
                                                                            <th>Entrance Exam 
                                                                            </th>
                                                                            <th>Seat No.
                                                                            </th>
                                                                            <th>Year of Exam
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtPercentile" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Rank
                                                                            </th>
                                                                            <th>Last School/College Name as per TC/Leaving Certificate
                                                                            </th>
                                                                            <th style="display: none;">DGPA/CGPA
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td style="text-align: center">
                                                                    <asp:ImageButton ID="btnEditEntranceDetail" runat="server" OnClick="btnEditEntranceDetail_Click" TabIndex="180"
                                                                        CommandArgument='<%# Eval("QUALIFYNO") %>' ImageUrl="~/images/edit1.gif" ToolTip='<%# Eval("QUALIFYNAME")%>' />
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:ImageButton ID="btnDeleteEntranceDetail" runat="server" OnClick="btnDeleteEntranceDetail_Click" TabIndex="180"
                                                                        CommandArgument='<%# Eval("QUALIFYNO") %>' ImageUrl="~/images/delete.png" ToolTip="Delete Record"
                                                                        OnClientClick="return confirm('Do You Want To Delete Added Qualification Exam ?');" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("QUALIFYNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("EXMROLLNO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("YEAR_OF_EXAM")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PER")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ALL_INDIA_RANK")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("LAST_SCHOOL_NAME") %>
                                                                </td>
                                                                <td style="display: none;">
                                                                    <%# Eval("CGPA") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                        <%--Entrance Exam Scores--%>
                                        <div id="Div1" class="row" runat="server" style="display: none">
                                            <div class="col-md-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>
                                                        <b>Other Entrance Exam Scores(P.G)</b></h5>
                                                </div>
                                            </div>
                                            <div id="div2" runat="server">
                                                <div class="form-group col-md-3">
                                                    <label>
                                                        Exam Name</label>
                                                    <asp:DropDownList ID="ddlpgentranceno" runat="server" CssClass="form-control" Enabled="false"
                                                        AppendDataBoundItems="True" ToolTip="Please Select Exam no." TabIndex="163">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label>
                                                        Seat No.</label>
                                                    <asp:TextBox ID="txtpgrollno" runat="server" CssClass="form-control" Enabled="false"
                                                        ToolTip="Please Enter Qualifying Seat No" TabIndex="164"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-2">
                                                    <label>
                                                        <%--<span style="color: red;">*</span>--%>Year of Exam</label>
                                                    <asp:TextBox ID="txtpgexamyear" runat="server" CssClass="form-control" TabIndex="165"
                                                        onkeyup="validateNumeric(this);" ToolTip="Please Enter Year of Exam" MaxLength="4"></asp:TextBox>
                                                    <%--  <asp:RequiredFieldValidator ID="rfvYearOfEntranceExam" runat="server" ControlToValidate="txtYearOfExam"
                                    Display="None" SetFocusOnError="true" ValidationGroup="EntranceExam" ErrorMessage="Please Enter Year of Entrance Exam"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-md-2" style="display: none;">
                                                    <label>
                                                        State Rank</label>
                                                    <asp:TextBox ID="txtpgsrank" runat="server" CssClass="form-control" ToolTip="Please Enter State Rank"
                                                        TabIndex="109"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        FilterType="Numbers" TargetControlID="txtStateRank">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-md-2">
                                                    <label>
                                                        Percentage</label>
                                                    <asp:TextBox ID="txtpgpercentage" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage"
                                                        onkeyup="validateNumeric(this);" TabIndex="166"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-2">
                                                    <label>
                                                        DGPA/CGPA</label>
                                                    <asp:TextBox ID="txtpgpercentile" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                        ToolTip="Please Enter DGPA/CGPA" TabIndex="167">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExamNo"
                                                        ValidationGroup="EntranceExam" Display="None" SetFocusOnError="true" InitialValue="0"
                                                        ErrorMessage="Please Select Exam Name"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-2">
                                                    <label>
                                                        Rank</label>
                                                    <asp:TextBox ID="txtpgrank" runat="server" CssClass="form-control" ToolTip="Please Enter Rank"
                                                        TabIndex="168"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        FilterType="Numbers" TargetControlID="txtpgrank">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-md-2" style="display: none;">
                                                    <label>
                                                        Quota</label>
                                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                        TabIndex="113">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-2">
                                                    <label>
                                                        Score</label>
                                                    <asp:TextBox ID="txtpgscore" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                        ToolTip="Please Enter Score" TabIndex="169"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-2" id="Div3" style="display: none;" runat="server">
                                                    <label id="Label1" runat="server">
                                                        Paper</label>
                                                    <asp:TextBox ID="TextBox8" CssClass="form-control" runat="server" TabIndex="115"></asp:TextBox>
                                                </div>
                                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                    <div class="form-group col-md-2" id="Div5" runat="server">
                                                        <label id="Label2" runat="server">
                                                            Paper Code</label>
                                                        <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <%--Entrance Exam Scores--%>
                                    </div>


                                    <div class="mt-3">
                                        <div class="sub-heading" id="trLastQual" runat="server">
                                            <h5>Student Last Qualification Details (Only for PG students)</h5>
                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="upEditQualExm" runat="server">
                                        <ContentTemplate>
                                            <div class="">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="divQualCollegeName" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualCollegeName" runat="server">* </sup>
                                                            <label>School / College Name</label>
                                                        </div>

                                                        <asp:TextBox ID="txtSchoolCollegeNameQualifying" runat="server" CssClass="form-control"
                                                            placeholder="Enter School / College Name" TabIndex="181"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualBoard" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualBoard" runat="server">* </sup>
                                                            <label>Board</label>
                                                        </div>

                                                        <asp:TextBox ID="txtBoardQualifying" runat="server" TabIndex="182" placeholder="Please Enter Board"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                                            TargetControlID="txtBoardQualifying" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualExam" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualExam" runat="server">* </sup>
                                                            <label>Qualifying Exam</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddldegree" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="183" Placeholder="Please Select Qualifying Exam" ToolTip="Please Select Qualifying Exam">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <label>Medium in Qualify Exam</label>
                                                        </div>

                                                        <asp:TextBox ID="txtQualiMedium" runat="server" placeholder="" CssClass="form-control"
                                                            ToolTip="Please Enter Medium in Qualify Exam" TabIndex="173" MaxLength="50"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender51" runat="server"
                                                            TargetControlID="txtQualiMedium" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualSeatNo" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualSeatNo" runat="server">* </sup>
                                                            <label>Seat No.</label>
                                                        </div>

                                                        <asp:TextBox ID="txtQualExamRollNo" runat="server" MaxLength="15" CssClass="form-control"
                                                            TabIndex="184" placeholder="Enter Exam Roll No." ToolTip="Please Enter Seat No."></asp:TextBox>
                                                        <%--                                        <asp:RequiredFieldValidator ID="rfvQualExamRollNo" runat="server" ControlToValidate="txtQualExamRollNo"
                                            Display="None" SetFocusOnError="true" ValidationGroup="Qual" ErrorMessage="PLease Enter Exam Roll No."></asp:RequiredFieldValidator>--%>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender52" runat="server"
                                                            TargetControlID="txtQualExamRollNo" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualYearExam" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualYearExam" runat="server">* </sup>
                                                            <label>Year of Exam</label>
                                                        </div>
                                                        <asp:TextBox ID="txtYearOfExamQualifying" runat="server" ToolTip="Please Enter Year Of Exam"
                                                            placeholder="Enter Year Of Exam" onkeyup="validateNumeric(this);" CssClass="form-control"
                                                            MaxLength="4" TabIndex="185"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualMarksObtained" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualMarksObtained" runat="server">* </sup>
                                                            <label>Marks Obtained</label>
                                                        </div>

                                                        <asp:TextBox ID="txtMarksObtainedQualifying" onkeyup="validateNumeric(this);" runat="server"
                                                            MaxLength="4" placeholder="Enter Marks Obtained" CssClass="form-control" TabIndex="186"
                                                            onchange="calc1(4);"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender58" runat="server"
                                                            FilterType="Numbers" TargetControlID="txtMarksObtainedQualifying">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualOutOfMarks" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualOutOfMarks" runat="server">* </sup>
                                                            <label>Out Of Marks</label>
                                                        </div>

                                                        <asp:TextBox ID="txtOutOfMarksQualifying" onkeyup="validateNumeric(this);" onblur="calPercentage(this,'other'),validateOtherMarksCam(this)"
                                                            onchange="calc1(4);" runat="server" MaxLength="4" placeholder="Enter Out Of Marks"
                                                            CssClass="form-control" TabIndex="187"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualPercentage" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualPercentage" runat="server">* </sup>
                                                            <label>Percentage</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPercentageQualifying" onkeyup="validateNumeric(this);" runat="server" MaxLength="4"
                                                            ToolTip="Please Enter Percentage" CssClass="form-control" placeholder="Enter Percentage"
                                                            ValidationGroup="Academic" TabIndex="187"></asp:TextBox>
                                                        <%--                                        <asp:RequiredFieldValidator ID="rfvPercentageQualifying" runat="server" ControlToValidate="txtPercentageQualifying"
                                            Display="None" ErrorMessage="Please Enter Percentage" SetFocusOnError="true"
                                            ValidationGroup="Qual"></asp:RequiredFieldValidator>--%>
                                                        <asp:CompareValidator ID="rfvPercentageQualifying1" runat="server" ControlToValidate="txtPercentageQualifying"
                                                            Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                            SetFocusOnError="True" Type="Double" ValidationGroup="Qual"></asp:CompareValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualGrade" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualGrade" runat="server">* </sup>
                                                            <label>Grade</label>
                                                        </div>

                                                        <asp:TextBox ID="txtGradeQualifying" runat="server" ToolTip="Please Enter Grade"
                                                            placeholder="Enter Grade" CssClass="form-control" TabIndex="188" MaxLength="2"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender53" runat="server"
                                                            TargetControlID="txtGradeQualifying" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_=,./:;<>?'{}[]\|&&quot;'1234567890" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualCGPA" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualCGPA" runat="server">* </sup>
                                                            <label>DGPA/CGPA</label>
                                                        </div>

                                                        <asp:TextBox ID="txtPercentileQualifying" runat="server" onkeyup="validateNumeric(this);" MaxLength="4"
                                                            ToolTip="Please Enter Qualifying DGPA/CGPA" placeholder=" Enter Qualifying DGPA/CGPA"
                                                            CssClass="form-control" ValidationGroup="Academic" TabIndex="190"></asp:TextBox>
                                                        <asp:CompareValidator ID="rfvPercentileQualifying" runat="server" ControlToValidate="txtPercentileQualifying"
                                                            Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                            SetFocusOnError="True" Type="Double" ValidationGroup="Qual"></asp:CompareValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <label>Attempt</label>
                                                        </div>

                                                        <asp:TextBox ID="txtAttemptQualifying" runat="server" placeholder="Enter Attempt"
                                                            ToolTip="Please Enter Attempt" TabIndex="181" MaxLength="3" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender54" runat="server"
                                                            TargetControlID="txtAttemptQualifying" FilterType="Numbers" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQualCollegeAddress" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup id="supQualCollegeAddress" runat="server">* </sup>
                                                            <label>School/College Address</label>
                                                        </div>

                                                        <asp:TextBox ID="txtQualExmAddress" runat="server" placeholder="School/College Address"
                                                            CssClass="form-control" TextMode="MultiLine" ToolTip="School/College Address" Rows="1"
                                                            MaxLength="100" TabIndex="191"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <asp:Panel ID="pnlsupervision" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Research Topic</label>
                                                            </div>

                                                            <asp:TextBox ID="txtResearchTopic" runat="server" placeholder="Enter Percentage"
                                                                ToolTip="Please Enter Percentage" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Supervisor Name1</label>
                                                            </div>

                                                            <asp:TextBox ID="txtSupervisorName1" runat="server" placeholder="Enter Supervisor Name1"
                                                                ToolTip="Please Enter Percentage" CssClass="form-control" TabIndex="158"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server"
                                                                TargetControlID="txtSupervisorName1" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="1234567890" />
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Supervisor Name2</label>
                                                            </div>

                                                            <asp:TextBox ID="txtSupervisorName2" runat="server" placeholder="Enter Supervisor Name2"
                                                                ToolTip="Please Enter Percentile" CssClass="form-control" TabIndex="159"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server"
                                                                TargetControlID="txtSupervisorName2" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="1234567890" />
                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtPercentileQualifying"
                                                                Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                                SetFocusOnError="True" Type="Double" ValidationGroup="Qual"></asp:CompareValidator>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" TabIndex="192" Text="Add"
                                                    ToolTip="Add Qualification Detail" ValidationGroup="Qual" CssClass="btn btn-primary" />
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="divlstugpgqual" runat="server" ScrollBars="Auto">
                                                    <asp:ListView ID="lvQualExm" runat="server">
                                                        <LayoutTemplate>
                                                            <%-- <div class="vista-grid">--%>
                                                            <div id="demo-grid">
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th style="text-align: center">Edit
                                                                            </th>
                                                                            <th style="text-align: center">Delete
                                                                            </th>
                                                                            <th>School/College Name
                                                                            </th>
                                                                            <th>Qualifying Exam 
                                                                            </th>
                                                                            <th>Year of Exam
                                                                            </th>
                                                                            <th>Board
                                                                            </th>
                                                                            <th>Percentage
                                                                            </th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td style="text-align: center">
                                                                    <asp:ImageButton ID="btnEditQualDetail" runat="server" OnClick="btnEditQualDetail_Click"
                                                                        CommandArgument='<%# Eval("QUALIFYNO") %>' ImageUrl="~/images/edit1.gif" ToolTip='<%# Eval("QUALIFYNAME")%>' />
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:ImageButton ID="btnDeleteQualDetail" runat="server" OnClick="btnDeleteQualDetail_Click"
                                                                        CommandArgument='<%# Eval("QUALIFYNO") %>' ImageUrl="~/images/delete.png" ToolTip="Delete Record"
                                                                        OnClientClick="return confirm('Do You Want To Delete Added Qualification Exam ?');" />
                                                                </td>
                                                                <td id="school_college_name" runat="server">
                                                                    <%# Eval("SCHOOL_COLLEGE_NAME")%>
                                                                </td>
                                                                <td id="qualifyno" runat="server">
                                                                    <%# Eval("QUALIFYNAME")%>
                                                                </td>
                                                                <td id="year_of_exam" runat="server">
                                                                    <%# Eval("YEAR_OF_EXAMHSSC")%>
                                                                </td>

                                                                <td id="board" runat="server">
                                                                    <%# Eval("BOARD") %>
                                                                </td>
                                                                <td id="per" runat="server">
                                                                    <%# Eval("PER")%>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnSubmit" runat="server" TabIndex="193" Text="Save & Continue >>"
                                        ToolTip="Click to Save" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClick="btnSubmit_Click"
                                        ValidationGroup="Academic" OnClientClick="return validateSubjectTextBoxes();" />
                                    <asp:ValidationSummary ID="vsAcademic" runat="server" ValidationGroup="Academic"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                    <button runat="server" id="btnGohome" visible="false" tabindex="194" onserverclick="btnGohome_Click"
                                        class="btn btn-warning btnGohome" tooltip="Click to Go Back Home">
                                        Go Back Home
                                    </button>
                                    <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Academic" />--%>
                                </div>
                            </div>
                            <%--student Last UG or PG Qualification--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

    <script type="text/javascript" language="javascript">

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.focus();
                alert('Only Alphabets Allowed');
                return false;
            }
            else
                return true;

        }
        function validateDecimalNumber(txt) {
            var expAlphabet = /^[0-9.]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.focus();
                alert('Only Alphabets Allowed');
                return false;
            }
            else
                return true;

        }

        function isNumberKey(txt, evt) {                                  //added by sachin 19-05-2022
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;

                }
            } else {
                if (charCode > 31 &&
                  (charCode < 48 || charCode > 57))
                    return false;

            }
            return true;

        }



        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function validateAlphaNumeric(txt) {
            var expAN = /[$\\@\\\#%\^\&\*\(\)\[\]\+\_popup\{\}|`\~\=\|]/;
            var strPass = txt.value;
            var strLength = strPass.length;
            var lchar = txt.value.charAt((strLength) - 1);

            if (lchar.search(expAN) != -1) {
                txt.value(txt.value.substring(0, (strLength) - 1));
                txt.focus();
                alert('Only Alpha-Numeric Characters Allowed!');
            }
            return true;
        }




        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }


    </script>
    <script>
        function CalcMarks() {

            //alert('hi');
            var txtObtMarks = eval(document.getElementById('<%=txtMarksObtainedHssc.ClientID %>').value);
            var txtTotalMarks = eval(document.getElementById('<%=txtOutOfMarksHssc.ClientID %>').value);
            //alert(txtTotalMarks);
            if (txtTotalMarks == null || txtTotalMarks == '') {
                txtTotalMarks = 300;
            }
            if (txtObtMarks > txtTotalMarks) {
                alert('Marks Obtained should be less than Out of Marks');
                document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').value = '';
                document.getElementById('<%=txtOutOfMarksHssc.ClientID %>').value = '';

                document.getElementById('<%=txtMarksObtainedHssc.ClientID%>').focus();

            }
        }
    </script>

    <script type="text/javascript">
        function calc(val) {
            debugger;

            //alert(hi);
            if (val = 1) {
                var txt1 = eval(document.getElementById('<%=txtMarksObtainedSsc.ClientID %>').value);
                var txt2 = eval(document.getElementById('<%=txtOutOfMarksSsc.ClientID %>').value);
                if (txt2 < txt1) {
                    alert('Please Enter Secondary/10th Total marks less than Secondary/10th out of marks');
                    document.getElementById('<%=txtOutOfMarksSsc.ClientID%>').value = '';
                    document.getElementById('<%=txtPercentageSsc.ClientID %>').value = '';
                    document.getElementById('<%=txtOutOfMarksSsc.ClientID%>').focus();
                }
                else {
                    document.getElementById('<%=txtPercentageSsc.ClientID %>').value = ((txt1 / txt2) * 100).toFixed(2);
                }
            }

            if (val = 3) {

                var txt13 = eval(document.getElementById('<%=txtMarksObtainedDiploma.ClientID %>').value);
                var txt23 = eval(document.getElementById('<%=txtOutOfMarksDiploma.ClientID %>').value);
                if (txt23 < txt13) {
                    alert('Please Enter Diploma Total marks less than Diploma out of marks');
                    document.getElementById('<%=txtOutOfMarksDiploma.ClientID%>').value = '';
                    document.getElementById('<%=txtPercentageDiploma.ClientID %>').value = '';
                    document.getElementById('<%=txtOutOfMarksDiploma.ClientID%>').focus();
                }
                else {
                    document.getElementById('<%=txtPercentageDiploma.ClientID %>').value = ((txt13 / txt23) * 100).toFixed(2);
                }
            }


            if (val = 4) {

                var txt11 = eval(document.getElementById('<%=txtMarksObtainedQualifying.ClientID %>').value);
                var txt21 = eval(document.getElementById('<%=txtOutOfMarksQualifying.ClientID %>').value);
                if (txt21 < txt11) {
                    alert('Last Qualification Obtained Marks should be less than Last Qualification Out of Marks');
                    document.getElementById('<%=txtOutOfMarksQualifying.ClientID%>').value = '';
                    document.getElementById('<%=txtPercentageQualifying.ClientID %>').value = '';
                    document.getElementById('<%=txtOutOfMarksQualifying.ClientID%>').focus();
                }
                else {
                    document.getElementById('<%=txtPercentageQualifying.ClientID %>').value = ((txt11 / txt21) * 100).toFixed(2);
                }
            }
        }


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            function calc(val) {
                debugger;

                if (val = 1) {
                    var txt1 = eval(document.getElementById('<%=txtMarksObtainedSsc.ClientID %>').value);
                    var txt2 = eval(document.getElementById('<%=txtOutOfMarksSsc.ClientID %>').value);
                    if (txt2 < txt1) {
                        alert('Please Enter Secondary/10th Total marks less than Secondary/10th out of marks');
                        return false;
                        document.getElementById('<%=txtOutOfMarksSsc.ClientID%>').value = '';
                        document.getElementById('<%=txtPercentageSsc.ClientID %>').value = '';
                        document.getElementById('<%=txtOutOfMarksSsc.ClientID%>').focus();
                    }
                    else {
                        document.getElementById('<%=txtPercentageSsc.ClientID %>').value = ((txt1 / txt2) * 100).toFixed(2);
                    }
                }


                //  if (val = 3) {
                // var txt13 = eval(document.getElementById('<%=txtMarksObtainedDiploma.ClientID %>').value);
                // var txt23 = eval(document.getElementById('<%=txtOutOfMarksDiploma.ClientID %>').value);
                // if (txt23 < txt13) {
                //   alert('Please Enter Diploma Total marks is less than Diploma out of marks');
                //  document.getElementById('<%=txtOutOfMarksDiploma.ClientID%>').value = '';
                //  document.getElementById('<%=txtPercentageDiploma.ClientID %>').value = '';
                //  document.getElementById('<%=txtOutOfMarksDiploma.ClientID%>').focus();
                // }
                // else {
                //    document.getElementById('<%=txtPercentageDiploma.ClientID %>').value = ((txt13 / txt23) * 100).toFixed(2);
                // }
                //}

                if (val = 3) {
                    var txt13 = eval(document.getElementById('<%=txtMarksObtainedDiploma.ClientID %>').value);
                    var txt23 = eval(document.getElementById('<%=txtOutOfMarksDiploma.ClientID %>').value);
                    if (txt23 < txt13) {
                        alert('Please Enter Diploma Total marks less than Diploma out of marks');
                        document.getElementById('<%=txtOutOfMarksDiploma.ClientID%>').value = '';
                        document.getElementById('<%=txtPercentageDiploma.ClientID %>').value = '';
                        document.getElementById('<%=txtOutOfMarksDiploma.ClientID%>').focus();
                    }
                    else {
                        document.getElementById('<%=txtPercentageDiploma.ClientID %>').value = ((txt13 / txt23) * 100).toFixed(2);
                    }
                }


                if (val = 4) {
                    var txt11 = eval(document.getElementById('<%=txtMarksObtainedQualifying.ClientID %>').value);
                    var txt21 = eval(document.getElementById('<%=txtOutOfMarksQualifying.ClientID %>').value);
                    if (txt21 < txt11) {
                        alert('Last Qualification Obtained Marks should be less than Last Qualification Out of Marks');
                        document.getElementById('<%=txtOutOfMarksQualifying.ClientID%>').value = '';
                        document.getElementById('<%=txtPercentageQualifying.ClientID %>').value = '';
                        document.getElementById('<%=txtOutOfMarksQualifying.ClientID%>').focus();
                    }
                    else {
                        document.getElementById('<%=txtPercentageQualifying.ClientID %>').value = ((txt11 / txt21) * 100).toFixed(2);
                    }
                }
            }
        });
    </script>

    <script type="text/javascript">
        function calc1(val) {
            debugger;
            if (val = 4) {

                var txt11 = eval(document.getElementById('<%=txtMarksObtainedQualifying.ClientID %>').value);
                var txt21 = eval(document.getElementById('<%=txtOutOfMarksQualifying.ClientID %>').value);
                if (txt21 < txt11) {
                    alert('Last Qualification Obtained Marks should be less than Last Qualification Out of Marks');
                    document.getElementById('<%=txtOutOfMarksQualifying.ClientID%>').value = '';
                    document.getElementById('<%=txtPercentageQualifying.ClientID %>').value = '';
                    document.getElementById('<%=txtOutOfMarksQualifying.ClientID%>').focus();
                }
                else {
                    document.getElementById('<%=txtPercentageQualifying.ClientID %>').value = ((txt11 / txt21) * 100).toFixed(2);
                }
            }
        }


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            function calc1(val) {

                if (val = 4) {
                    var txt11 = eval(document.getElementById('<%=txtMarksObtainedQualifying.ClientID %>').value);
                    var txt21 = eval(document.getElementById('<%=txtOutOfMarksQualifying.ClientID %>').value);
                    if (txt21 < txt11) {
                        alert('Last Qualification Obtained Marks should be less than Last Qualification Out of Marks');
                        document.getElementById('<%=txtOutOfMarksQualifying.ClientID%>').value = '';
                        document.getElementById('<%=txtPercentageQualifying.ClientID %>').value = '';
                        document.getElementById('<%=txtOutOfMarksQualifying.ClientID%>').focus();
                    }
                    else {
                        document.getElementById('<%=txtPercentageQualifying.ClientID %>').value = ((txt11 / txt21) * 100).toFixed(2);
                    }
                }
            }
        });
    </script>
    <script>
        function updateDue() {

            var total = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_txtphymark").value);
            var val2 = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_txtchem").value);
            var val3 = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").value);

            // to make sure that they are numbers
            if (!total) { total = 0; }
            if (!val2) { val2 = 0; }
            if (!val3) { val3 = 0; }


            var ansD = document.getElementById("txtPcmMarks");
            ansD.value = total + val2 + val3;


        }
    </script>

    <script>
        function validateSubjectTextBoxes() {
            //debugger;
            //alert('hi')
            var numVal1 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtphymark").value);
            var numVal2 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtchem").value);
            var numVal3 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").value);
            var numVal4 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").value);
            var numVal6 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmarktotal").value);
            var numVal5 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtbiology").value);
            var isHidden = document.getElementById("ctl00_ContentPlaceHolder1_trBiology");
            var obtainedMarks = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtMarksObtainedHssc").value);
            var totalMarks = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtOutOfMarksHssc").value);
            var vocSubName = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocsub").value);
            var vocMarksObtained = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").value);
            var vocTotalMarks = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmarktotal").value);
            var orgid = $("#<%=hdnOrgId.ClientID %>").val();
            var degree = $("#<%=hdnDegree.ClientID %>").val();
            //alert(orgid + degree)
            if ((orgid != "5" && orgid == "6" && degree != "MASTER OF BUSINESS ADMINISTRATION") || orgid == "19" || orgid == "20" || orgid == "21") {   // Added by Bhagyashree on 12062023 //// Added by orgid and PG validation condiation Sachin Lohakare 11092023
                if (numVal1 <= 0) {                                                                                                                   // Modified to Add Condition for PCEN, PJLCOE, TGPCET by Shrikant W. on 29-12-2023
                    alert('Enter Valid Marks for Physics');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtphymark").focus();
                    return false;
                }
                if (numVal2 <= 0) {
                    alert('Enter Valid Marks for Chemistry');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtchem").focus();
                    return false;
                }
                if (isHidden.style.display != "none") {
                    if ((numVal3 <= 0 || numVal3 == '') && (numVal5 <= 0 || numVal5 == '')) {
                        alert('Enter Valid Marks for Maths/Biology');
                        document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").focus();
                        return false;
                    }
                }
                if (numVal4 <= 0 && numVal6 != 0) {
                    alert('Enter Valid Marks for Vocational Subject');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").focus();
                    return false;
                }
                if (numVal6 != '' && numVal4 == '') {
                    alert('Enter Valid Marks for Vocational Subject');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").focus();
                    return false;
                }
                if (isHidden.style.display === "none") {
                    if (numVal3 <= 0 || numVal3 == '') {
                        alert('Enter Valid Marks for Maths');
                        document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").focus();
                        return false;
                    }
                }
                if (vocMarksObtained != '' && vocTotalMarks != '' && vocSubName == '') {
                    alert('Enter Vocational Subject Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocsub").value = '';
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocsub").focus();
                    return false;
                }
            }
        }
    </script>

    <script>
        function allowAlphaNumericSpaceHyphen(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code == 45) && // hyphen
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
                // alert("Not Allowed Special Character..!");
                return true;
            } else {
                return false;
            }
        }
    </script>

<<<<<<< HEAD
=======
    <script>
        function checkPercentageSsc() {
            debugger;
            var txtPer = document.getElementById("<%=txtPercentageSsc.ClientID%>");
        var txtPerValue = txtPer.value;

        var percentage = parseFloat(txtPerValue);

        if (isNaN(percentage) || percentage > 100) {
            alert("Percentage cannot be greater than 100!");
            txtPer.value = "";
        }
    }

    function checkPercentageDiploma() {
        debugger;
        var txtPer = document.getElementById("<%=txtPercentageDiploma.ClientID%>");
        var txtPerValue = txtPer.value;

        var percentage = parseFloat(txtPerValue);

        if (isNaN(percentage) || percentage > 100) {
            alert("Percentage cannot be greater than 100!");
            txtPer.value = "";
        }
    }

    function checkPercentageEntrance() {
        debugger;
        var txtPer = document.getElementById("<%=txtPer.ClientID%>");
        var txtPerValue = txtPer.value;

        var percentage = parseFloat(txtPerValue);

        if (isNaN(percentage) || percentage > 100) {
            alert("Percentile cannot be greater than 100!");
            txtPer.value = "";
        }
    }

    function checkPercentageQual() {
        debugger;
        var txtPer = document.getElementById("<%=txtPercentageQualifying.ClientID%>");
        var txtPerValue = txtPer.value;

        var percentage = parseFloat(txtPerValue);

        if (isNaN(percentage) || percentage > 100) {
            alert("Percentage cannot be greater than 100!");
            txtPer.value = "";
        }
    }
    </script>


>>>>>>> 4a98c761 ([BUGFIX][51653][Changes in personal qualification page validation page])
    <%--  <script>
        function validateSubjectTextBoxes() {
            debugger;
            var numVal1 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtphymark").value);
            var numVal2 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtchem").value);
            var numVal3 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").value);
            var numVal4 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").value);
            var numVal6 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmarktotal").value);
            var numVal5 = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtbiology").value);
            var isHidden = document.getElementById("ctl00_ContentPlaceHolder1_trBiology");
            var obtainedMarks = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtMarksObtainedHssc").value);
            var totalMarks = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtOutOfMarksHssc").value);
            var vocSubName = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocsub").value);
            var vocMarksObtained = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").value);
            var vocTotalMarks = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmarktotal").value);
            var orgid = $("#<%=hdnOrgId.ClientID %>").val(); //Added by Bhagyashree on 12062023 //// Added by orgid Sachin Lohakare 11092023
            var degree = "<%= Session["Degree"] %>";
            //var lowercaseDegree = degree.toUpperCase();
            //if (orgid != "5" && orgid != "6") {

            //console.log("orgid:", orgid);
            //console.log("degree:", degree);

            if (orgid != "5" && degree != "MASTER OF BUSINESS ADMINISTRATION") {
                //if (orgid == "6" && degree == "Master of Business Administration") {
                // alert('hi')
                // return false;
                //}

                if (numVal1 <= 0) {
                    alert('Enter Valid Marks for Physics');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtphymark").focus();
                    return false;
                }
                if (numVal2 <= 0) {
                    alert('Enter Valid Marks for Chemistry');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtchem").focus();
                    return false;
                }
                if (isHidden.style.display != "none") {
                    if ((numVal3 <= 0 || numVal3 == '') && (numVal5 <= 0 || numVal5 == '')) {
                        alert('Enter Valid Marks for Maths/Biology');
                        document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").focus();
                        return false;
                    }
                }
                if (numVal4 <= 0 && numVal6 != 0) {
                    alert('Enter Valid Marks for Vocational Subject');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").focus();
                    return false;
                }
                if (numVal6 != '' && numVal4 == '') {
                    alert('Enter Valid Marks for Vocational Subject');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocationalmark").focus();
                    return false;
                }
                if (isHidden.style.display === "none") {
                    if (numVal3 <= 0 || numVal3 == '') {
                        alert('Enter Valid Marks for Maths');
                        document.getElementById("ctl00_ContentPlaceHolder1_txtmaths").focus();
                        return false;
                    }
                }
                if (vocMarksObtained != '' && vocTotalMarks != '' && vocSubName == '') {
                    alert('Enter Vocational Subject Name');
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocsub").value = '';
                    document.getElementById("ctl00_ContentPlaceHolder1_txtVocsub").focus();
                    return false;
                }
            }
        }
    </script>--%>

    <%--    <script type="text/javascript">
        function shouldValidate() {
            var orgid = $("#<%=hdnOrgId.ClientID %>").val();
        var degree = $("#<%=hdnDegree.ClientID %>").val();

        if (orgid == "6" && degree == "MASTER OF BUSINESS ADMINISTRATION") {
            return true;
        } else {
            var validationPassed = validateSubjectTextBoxes();
            return validationPassed;
        }
    }
    </script>--%>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
