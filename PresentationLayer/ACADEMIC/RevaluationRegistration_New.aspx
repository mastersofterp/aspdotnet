<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RevaluationRegistration_New.aspx.cs"
    Inherits="ACADEMIC_RevaluationRegistration" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetails"
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
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>
    <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">REVALUATION APPLY</h3>
                        </div>
                        <div class="box-body">
                            <div id="divCourses" runat="server" visible="false">
                                <div class="col-12" id="tblSession" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Examination</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlSession" runat="server" Display="None"
                                                InitialValue="0" ErrorMessage="Please Select Examination." ValidationGroup="Show" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlSession" runat="server" Display="None"
                                                InitialValue="0" ErrorMessage="Please Select Examination." ValidationGroup="Submit" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Univ. Reg. No. / Adm. No.</label>
                                            </div>
                                            <asp:TextBox ID="txtRollNo" runat="server" MaxLength="15" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                                Display="None" ErrorMessage="Please Enter Univ. Reg. No. / Adm. No." ValidationGroup="Show" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtRollNo" runat="server" Display="None"
                                                ErrorMessage="Please Enter Univ. Reg. No. / Adm. No." ValidationGroup="Submit" />

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Clear" Font-Bold="true" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />

                                    </div>
                                </div>
                                <div class="col-12" id="tblInfo" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <b>Student Name :</b><a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Father's Name :</b><a class="sub-label">
                                                        <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Mother's Name :</b><a class="sub-label">
                                                        <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Univ. Reg. No. / Adm. No. :</b><a class="sub-label">
                                                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Admission Batch :</b><a class="sub-label">
                                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>

                                            </ul>
                                        </div>

                                        <div class="col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <b>College Name :</b><a class="sub-label">
                                                        <asp:Label ID="lblCollegeName" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Degree / Branch :</b><a class="sub-label">
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                    <asp:HiddenField ID="hfDegreeNo" runat="server" />
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Phone No. :</b><a class="sub-label">
                                                        <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Regulation :</b><a class="sub-label">
                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Semester :</b><a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                    <asp:Label ID="lblOrderID" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                </li>
                                                <li class="list-group-item" style="display: none">
                                                    <b>Total Subjects :</b>
                                                    <a class="pull-right">
                                                        <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0" Style="text-align: center;"></asp:TextBox>
                                                    </a>
                                                </li>
                                                <li class="list-group-item" style="display: none">
                                                    <b>Total Credits :</b>
                                                    <a class="pull-right">
                                                        <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0" Style="text-align: center;"></asp:TextBox>
                                                    </a>
                                                    <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">

                                        <%--<div class="form-group col-lg-6 col-md-12 col-12" id="divNote" runat="server" visible="false">
                                            <div class=" note-div">
                                                <h5 class="heading">Note </h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>You can apply revaluation for a maximum of 5 subjects only.</span></p>
                                            </div>
                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12 mt-1" id="divTotalCourseAmount" runat="server" visible="false">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Total Amount :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSem" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Apply For Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlSemester" runat="server" Display="None"
                                            InitialValue="0" ErrorMessage="Please Select Semester." ValidationGroup="Submit" />
                                    </div>

                                </div>
                            </div>
                               <div class="col-12">
                                <asp:Label ID="lblErrorMsg" runat="server">
                                </asp:Label>
                            </div>
                           </div>
                       
                        <div class="col-12" id="divAllCoursesFromHist" runat="server" visible="false">
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvCurrentSubjects" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>Subject List for Revaluation</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select </th>
                                                        <th>Sr. No.</th>
                                                        <th>Subject Code</th>
                                                        <th>Subject Name </th>
                                                        <th>Semester </th>
                                                        <th>Grades </th>
                                                        <%--<th style="text-align: center;">Oral Mark
                                                        </th>--%>
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
                                                <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("COURSENO")%>' ClientIDMode="Static"
                                                    AutoPostBack="true" OnCheckedChanged="chkAccept_CheckedChanged" />
                                                <%-- onclick="CalTotalFee(this)" Enabled='<%# Eval("FAIL_MORE_THAN_2_SUB").ToString()== "1" ? false : true %>'--%>
                                            </td>

                                            <td><%# Container.DataItemIndex+1 %></td>

                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("SCHEMENO") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSEMSCHNO" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExtermark" runat="server" Text='<%# Eval("GRADE") %>' />
                                                <asp:Label ID="lblMarks" runat="server" Text='<%# Eval("EXTERMARK") %>' Visible="false" />
                                            </td>
                                            <%--<td>
                                                <asp:Label ID="LblOral" runat="server" Text='<%# Eval("S2MARK") %>' />
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:HiddenField ID="requestparams" runat="server"></asp:HiddenField>

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Visible="false"
                                    ValidationGroup="Submit"  CssClass="btn btn-outline-primary" />   <%--OnClientClick="return showConfirm();"--%>
                                <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip" OnClick="btnPrintRegSlip_Click"
                                    Visible="false" CssClass="btn btn-outline-primary"/>
                                <%-- <asp:Button ID="btnCancel" runat="server" Text="Clear" Font-Bold="true" CssClass="btn btn-warning" CausesValidation="false" 
                                               OnClick="btnCancel_Click" />--%>

                                <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                        </div>
                        <div class="col-12" id="divRegCourses" runat="server" visible="false">
                            <asp:Panel ID="pnlFinalCourses" runat="server">
                                <asp:ListView ID="lvFinalCourses" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <div class=" sub-heading">
                                                <h5>Registered Subject List for Revaluation</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblCurrentSubjects">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr. No.</th>
                                                        <th>Subject Code</th>
                                                        <th>Subject Name</th>
                                                        <th>Semester</th>
                                                        <th>Grades</th>
                                                        <th>Amount</th>
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
                                            <td><%# Container.DataItemIndex+1 %></td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSEMSCHNO" runat="server" Text='<%# Eval("SEMESTERNAME") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExtermark" runat="server" Text='<%# Eval("GRADE") %>' />
                                                <asp:Label ID="lblMarks" runat="server" Text='<%# Eval("EXTERMARK") %>' Visible="false" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("RF_AMOUNT") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnPayOnline" runat="server" Text="Online Payment" OnClick="btnPayOnline_Click" Visible="false"
                                    ValidationGroup="Submit" CssClass="btn btn-outline-primary" />  <%-- OnClientClick="return showPayConfirm();" --%>
                                <asp:Button ID="btnChallan" runat="server" Text="Challan Receipt" Visible="false"
                                     Style="display:none" OnClick="btnChallan_Click" CssClass="btn btn-outline-primary" />   <%--OnClientClick="return showChallanConfirm();"--%>

                            </div>



                        </div>
                    </div>
                </div>
            </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <%-- <asp:AsyncPostBackTrigger ControlID="btnPrintRegSlip" EventName="Click"/>--%>
            <asp:PostBackTrigger ControlID="btnPrintRegSlip" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnPayOnline" />
            <asp:PostBackTrigger ControlID="btnChallan" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript" language="JavaScript">

        //function FreezeScreen(msg) {
        //    scroll(0, 0);
        //    var outerPane = document.getElementById('FreezePane');
        //    var innerPane = document.getElementById('InnerFreezePane');
        //    if (outerPane) outerPane.className = 'FreezePaneOn';
        //    if (innerPane) innerPane.innerHTML = msg;
        //}

        function showConfirm() {
            var ret = confirm('Do you Really want to Submit this Subjects for Revaluation?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }


        function showPayConfirm() {
            var ret = confirm('Applied Revaluation details Will be confirmed only after successful payments.Do you Really want to Pay Amount Online ?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }


        function showChallanConfirm() {
            var ret = confirm('Do you Really want to Print Revaluation Challan ?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>


    <%--  <script src="../Content/jquery.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("[id$='cbHead']").live('click', function () {
                $("[id$='chkAccept']").attr('checked', this.checked);
            });
        });
    </script>


</asp:Content>
