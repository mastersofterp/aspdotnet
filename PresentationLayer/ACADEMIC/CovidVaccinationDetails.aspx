<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CovidVaccinationDetails.aspx.cs" Inherits="ACADEMIC_CovidVaccinationDetails" %>

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
    </style>

    <div class="modal fade" id="preview" role="dialog">
                 <div class="modal-dialog modal-lg">
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
                             <div class="modal-content">

                                 <!-- Modal Header -->
                                 <div class="modal-header">
                                     <h4 class="modal-title">Document</h4>
                                     <button type="button" class="close" data-dismiss="modal">&times;</button>
                                 </div>

                                 <!-- Modal body -->
                                 <div class="modal-body text-center">
                                     <asp:Literal ID="ltEmbed" runat="server" />
                                 </div>

                                 <!-- Modal footer -->
                                 <div class="modal-footer">
                                     <button type="button" class="btn btn-danger" data-dismiss="modal" id="btnclose">Close</button>
                                 </div>

                             </div>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                 </div>
             </div>

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
                                            <li class="treeview">
                                                <i class="fa fa-graduation-cap"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkQualificationDetail"
                                                        ToolTip="Please select Qualification Details" OnClick="lnkQualificationDetail_Click" Text="Qualification Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview active">
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

                                            <li class="treeview"  id="divAdmissionApprove" runat="server">  <%--Added by sachin on 14-07-2022--%>
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

                            <div class="col-lg-10 col-md-8 col-12">
                                <div class="col-12 pl-0 pr-0 pl-lg-2 pr-lg-2">
                                    <div class="row d-none">
                                        <div class="col-md-12">
                                            <div class="sub-heading">
                                                <h5>Covid Vaccination Details</h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divCovidData" class="d-none">
                                        <div class="row" id="trGeneralInfo" runat="server">
                                            <%--<div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>General Information</h5>
                                                </div>
                                            </div>--%>
                                            <div class="col-md-6 col-sm-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student Name :</b><a>
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="True" ForeColor="#00569d"></asp:Label></a> </li>

                                                    <li class="list-group-item"><b>Enrollment No:</b><a>
                                                        <asp:Label ID="lblRegNo" runat="server" Font-Bold="True" ForeColor="#00569d"></asp:Label>
                                                    </a>
                                                        <br />
                                                    </li>
                                                    <li class="list-group-item"><b>Gender:</b><a>
                                                        <asp:Label ID="lblGender" runat="server" Font-Bold="True" ForeColor="#00569d"></asp:Label>
                                                    </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-md-6 col-sm-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Degree:</b><a>
                                                        <asp:Label ID="lblDegree" runat="server" Font-Bold="True" ForeColor="#00569d"></asp:Label></a> </li>

                                                    <li class="list-group-item"><b>Branch:</b><a>
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True" ForeColor="#00569d"></asp:Label>
                                                    </a>

                                                    </li>

                                                </ul>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row mt-3" id="Div3" runat="server">
                                        <div class="col-md-12">
                                            <div class="sub-heading">
                                                <h5>Vaccination Status</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trVaccinationCondition" runat="server">
                                            <div class="label-dynamic">
                                                <label>Are you vaccinated ? </label>
                                            </div>
                                            <asp:RadioButton ID="rdVaccinated" GroupName="VaccineStat" runat="server" Text="Yes"
                                                 AutoPostBack="true" OnCheckedChanged="rdVaccinated_CheckedChanged" />
                                            <asp:RadioButton ID="rdNotVaccinated" GroupName="VaccineStat" runat="server" Text="No"
                                                AutoPostBack="true" OnCheckedChanged="rdVaccinated_CheckedChanged" />
                                            <asp:HiddenField ID="hidVaccinationStat" runat="server" />
                                        </div>


                                        <div class="form-group col-lg-5 col-md-6 col-12" runat="server" id="divNote" visible="false">
                                            <div class=" note-div">
                                                <h5 class="heading">Note </h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Only PDF file required and each file size upto 1 MB only.</span> </p>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div id="trVaccinated" runat="server" class="row">
                                                <div class="col-md-4 col-12 vac-dose offset-md-1">
                                                    <div class="col-md-12">
                                                        <div class="sub-heading">
                                                            <h5>First Dose</h5>
                                                        </div>
                                                    </div>
                                                    <div id="div1" class="col-md-12">
                                                        <div class="row">
                                                            <div class="form-group col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Vaccine Name </label>
                                                                </div>
                                                                <asp:TextBox ID="txtFirstDoseVaccName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                                            </div>

                                                            <div class="form-group col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Vaccination Center</label>
                                                                </div>
                                                                <asp:TextBox ID="txtFirstDoseVaccCenter" runat="server" CssClass="form-control" MaxLength="100" />
                                                            </div>

                                                            <div class="form-group col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Vaccination Date </label>
                                                                </div>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i id="imgFirstDoseVaccDate1" runat="server" class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtFirstDoseVaccDate" runat="server" TabIndex="3" ValidationGroup="submit"
                                                                        CssClass="form-control" Font-Bold="true" onchange="return ValidateDate()" />
                                                                    <%--  <asp:Image ID="imgFirstDoseVaccDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                                    <ajaxToolKit:CalendarExtender ID="ceFirstDoseVaccDate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtFirstDoseVaccDate" PopupButtonID="imgFirstDoseVaccDate1" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meFirstDoseVaccDate" runat="server" TargetControlID="txtFirstDoseVaccDate"
                                                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                        MaskType="Date" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mvFirstDoseVaccDate" runat="server" EmptyValueMessage="Please Enter First Dose Vaccinated Date"
                                                                        ControlExtender="meFirstDoseVaccDate" ControlToValidate="txtFirstDoseVaccDate"
                                                                        IsValidEmpty="false" InvalidValueMessage="First Dose Vaccinated Date is invalid"
                                                                        Display="None" ErrorMessage="Please Enter First Dose Vaccinated Date" InvalidValueBlurredMessage="*"
                                                                        ValidationGroup="Submit" SetFocusOnError="true" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Certificate Upload:</label>
                                                                </div>
                                                                <asp:FileUpload ID="fuFirstDoseVaccCert" runat="server" accept=".pdf,.PDF" onchange="Filevalidation()" />
                                                            </div>

                                                            <div class="form-group col-md-12 col-sm-12" id="trFirstDoseCert" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Vaccination Certificate:</label>
                                                                </div>
                                                                <asp:UpdatePanel ID="updPreview" runat="server">
                                                                <ContentTemplate>
                                                                <asp:Button ID="lnkbtnFirstDoseCert" runat="server" OnClick="lnkbtnFirstDoseCert_Click" ToolTip='<%# Eval("FIRSTDOSE_FILE_NAME") %>' data-toggle="modal" data-target="#preview"
                                                                    CommandArgument='<%# Eval("FIRSTDOSE_FILE_NAME") %>'
                                                                         Visible='<%# Convert.ToString(Eval("FIRSTDOSE_FILE_NAME"))==string.Empty?false:true %>'></asp:Button>
                                                                <asp:HiddenField ID="hidFirstDoseFilePath" runat="server" />
                                                                     </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="lnkbtnFirstDoseCert" EventName="Click" />
                                                                    <%--  <asp:PostBackTrigger ControlID="btnFetch"/>data-toggle="modal" data-target="#PassModel"--%>
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnFirstDose" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnFirstDose_Click"
                                                                    OnClientClick="return showLockConfirm();" />
                                                                <asp:Button ID="btnFirstCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnFirstCancel_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-4 col-12 vac-dose offset-md-1" id="trseconddose" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="sub-heading">
                                                            <h5>Second Dose</h5>
                                                        </div>
                                                    </div>
                                                    <div id="div2" class="col-md-12">
                                                        <div class="row">
                                                            <div class="form-group col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Vaccine Name </label>
                                                                </div>
                                                                <asp:TextBox ID="txtSecondDoseVaccName" runat="server" CssClass="form-control" MaxLength="25" />
                                                            </div>

                                                            <div class="form-group col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Vaccination Center</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSecondDoseVaccCenter" runat="server" CssClass="form-control" MaxLength="100" />
                                                            </div>

                                                            <div class="form-group col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Vaccination Date </label>
                                                                </div>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i id="imgSecondDoseVaccDate1" runat="server" class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtSecondDoseVaccDate" runat="server" TabIndex="3" ValidationGroup="submit"
                                                                        CssClass="form-control" Font-Bold="true" onchange="return ValidateDate()" />
                                                                    <%--    <asp:Image ID="imgSecondDoseVaccDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                                    <ajaxToolKit:CalendarExtender ID="ceSecondDoseVaccDate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtSecondDoseVaccDate" PopupButtonID="imgSecondDoseVaccDate1" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meSecondDoseVaccDate" runat="server" TargetControlID="txtSecondDoseVaccDate"
                                                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                        MaskType="Date" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Second Dose Vaccinated Date"
                                                                        ControlExtender="meSecondDoseVaccDate" ControlToValidate="txtSecondDoseVaccDate"
                                                                        IsValidEmpty="false" InvalidValueMessage="Second Dose Vaccinated Date is invalid"
                                                                        Display="None" ErrorMessage="Please Enter Second Dose Vaccinated Date" InvalidValueBlurredMessage="*"
                                                                        ValidationGroup="Submit" SetFocusOnError="true" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Certificate Upload</label>
                                                                </div>
                                                                <asp:FileUpload ID="fuSecondDoseVaccCert" runat="server" accept=".pdf,.PDF" onchange="Filevalidation()" />
                                                            </div>

                                                            <div class="form-group col-md-12 col-sm-12" id="trSecondDoseCert" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Vaccination Certificate:</label>
                                                                </div>
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                <asp:Button ID="lnkbtnSecondDoseVaccCert" runat="server" OnClick="lnkbtnSecondDoseVaccCert_Click" 
                                                                        ToolTip='<%# Eval("SECONDDOSE_FILE_NAME") %>' data-toggle="modal" data-target="#preview"
                                                                    CommandArgument='<%# Eval("SECONDDOSE_FILE_NAME") %>'  Visible='<%# Convert.ToString(Eval("SECONDDOSE_FILE_NAME"))==string.Empty?false:true %>'></asp:Button>
                                                                <asp:HiddenField ID="hidSecondDoseFilePath" runat="server" />
                                                                    </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="lnkbtnSecondDoseVaccCert" EventName="Click" />
                                                                    <%--  <asp:PostBackTrigger ControlID="btnFetch"/>data-toggle="modal" data-target="#PassModel"--%>
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnSecondDose" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSecondDose_Click" Enabled="false" OnClientClick="return showLockConfirm();" />
                                                                <asp:Button ID="btnsecondcancel" runat="server" Text="Cancel" CssClass="btn btn-warning" Enabled="false" OnClick="btnsecondcancel_Click" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-12 btn-footer mt-4">
                                    <asp:LinkButton ID="Button1" runat="server" TabIndex="38" Text="Save & Continue >>" ToolTip="Click to Submit"
                                        class="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Academic" OnClientClick="return validateVaccination();"/>

                                    <button runat="server" id="Button2" visible="false" tabindex="39" onserverclick="btnGohome_ServerClick" class="btn btn-warning btnGohome" tooltip="Click to Go Back Home">
                                        Go Back Home
                                    </button>

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Academic" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function showLockConfirm() {
            var ret = confirm('Do you really want to submit your form. After submitting no changes can be made');
            if (ret == true)
                return true;
            else
                return false;
        }

        function showUnLockConfirm() {
            var ret = confirm('Are you sure to unlock the form ?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        function validateVaccination() {
            var userType = '<%= Session["usertype"].ToString() %>';
            var rdVaccinated = document.getElementById('<%= rdVaccinated.ClientID %>');
            var rdNotVaccinated = document.getElementById('<%= rdNotVaccinated.ClientID %>');

            if (usertype == 2) {
                if (!rdVaccinated.checked && !rdNotVaccinated.checked) {
                    alert("Please select Either Yes or No for Vaccination Status!");
                    return false;
                }
                return true;
            }
            
        }
    </script>

        <script>
            function ValidateDate() {
                var Fromdate = document.getElementById('<%=txtFirstDoseVaccDate.ClientID%>').value;
                var Todate = document.getElementById('<%=txtSecondDoseVaccDate.ClientID%>').value;
                var From_date = moment(Fromdate, 'DD/MM/YYYY');
                var To_date = moment(Todate, 'DD/MM/YYYY');

                var currentDate = moment(); // Get the current date

                if (From_date.isAfter(currentDate)) { // Compare with the current date
                    alert("First Dose Vaccination Date Cannot Be Future Date!");
                    document.getElementById('<%=txtFirstDoseVaccDate.ClientID%>').value = '';
                    return;
                }
                else if (To_date.isAfter(currentDate)) {
                    alert("Second Dose Vaccination Date Cannot Be Future Date!");
                    document.getElementById('<%=txtSecondDoseVaccDate.ClientID%>').value = '';
                    return;
                }
                else if (To_date.isBefore(From_date)) { 
                    alert("Second Dose Vaccination Date Cannot be Less Than First Dose Vaccination Date!");
                    document.getElementById('<%=txtSecondDoseVaccDate.ClientID%>').value = '';
                    return;
                }
            }

    </script>

    <script>
        
        document.addEventListener('contextmenu', function (event) {
            event.preventDefault();
        });

        document.onkeydown = function (e) {
            if (e.keyCode == 123) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
                return false;
            }
        };

</script>

    <script src="../JAVASCRIPTS/Inspect.js"></script>


    <script>

    // ADDED BY GAURAV SONPAROTE 16_08_2023

        $(document).ready(function () {
var getSystemOS = getMobileOperatingSystem();
if (getSystemOS !== "iOS") {
console.log('Is DevTools open:', window.devtools.isOpen);
console.log('DevTools orientation:', window.devtools.orientation);
if (window.devtools.isOpen == true && window.devtools.orientation != undefined) {
alert("Please Close The Inspector Window.");
location.reload();
}
// Get notified when it's opened/closed or orientation changes
window.addEventListener('devtoolschange', function (event) {
// window.addEventListener('devtoolschange', event => {
console.log('Is DevTools open:', event.detail.isOpen);
console.log('DevTools orientation:', event.detail.orientation);
console.log(event);
if (event.detail.isOpen == true && event.detail.orientation != undefined) {
alert("Please Close The Inspector Window.");
location.reload();
}
});
}
function getMobileOperatingSystem() {
var userAgent = navigator.userAgent || navigator.vendor || window.opera;

// Windows Phone must come first because its UA also contains "Android"
if (/windows phone/i.test(userAgent)) {
return "Windows Phone";
}

if (/android/i.test(userAgent)) {
return "Android";
}

// iOS detection from: http://stackoverflow.com/a/9039885/177710
if (/iPad|iPhone|iPod/.test(userAgent) && !window.MSStream) {
return "iOS";
}

return "unknown";
}
});

$(document).ready(function () {

$(document)[0].oncontextmenu = function () { return true; }

$(document).mousedown(function (e) {
if (e.button == 2) {
return true;
} else {
return true;
}
});
// FOR right click off ---------------START
//$(document).ready(function () {

// $(document)[0].oncontextmenu = function () { return false; }

// $(document).mousedown(function (e) {
// if (e.button == 2) {
// return false;
// } else {
// return true;
// }
// });

// --------------------------------------END
document.onkeydown = function (e) {
if (event.keyCode == 123) {
return false;
}
if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
return false;
}
if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) {
return false;
}
if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
return false;
}
if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
return false;
}
}
});
</script>
</asp:Content>


