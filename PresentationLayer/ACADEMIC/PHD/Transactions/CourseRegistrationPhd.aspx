<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CourseRegistrationPhd.aspx.cs" Inherits="ACADEMIC_CourseRegistrationPhd" Title="PHD Course Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSection"
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

    <!--academic Calendar-->
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div id="divCourses" runat="server" visible="false">
                        <asp:Panel ID="pnlMain" runat="server">
                            <asp:UpdatePanel ID="updSection" runat="server">
                                <ContentTemplate>
                                    <div class="col-12" id="divpnlCourse" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True"
                                                    CssClass="form-control" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvtxtSession" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select Session" SetFocusOnError="True" InitialValue="0"
                                                    ValidationGroup="CRPhd"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="showRollno" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Roll No.</label>
                                                </div>
                                                <div class="form-inline form-group">
                                                    <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRollNo"
                                                        Display="None" ErrorMessage="Please Enter Roll No" SetFocusOnError="True"
                                                        ValidationGroup="CRPhd"></asp:RequiredFieldValidator>

                                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                                        Font-Bold="true" ValidationGroup="CRPhd" CssClass="btn btn-primary" />

                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="CRPhd"></asp:ValidationSummary>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnShow" />
                                </Triggers>
                            </asp:UpdatePanel>

                            <div class="col-12" id="tblInfo" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Father Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Mother Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Degree / Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Enrollment No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>PH :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPH" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Scheme :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Admission Batch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="row mt-3">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" CssClass="form-control" runat="server" AppendDataBoundItems="true"
                                            OnSelectedIndexChanged="ddlSem_SelectedIndexChanged1" AutoPostBack="True" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Offered Courses</label>
                                        </div>
                                        <asp:DropDownList ID="ddlOfferedCourse" runat="server"
                                            AppendDataBoundItems="True" CssClass="form-control x" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlOfferedCourse_SelectedIndexChanged" data-select2-enable="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:DropDownList ID="ddlExem" runat="server"
                                            AppendDataBoundItems="True" CssClass="form-control y" AutoPostBack="True" Visible="False" data-select2-enable="true">
                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Exempted</asp:ListItem>
                                            <asp:ListItem Value="2">NotExempted</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlType" runat="server"
                                            AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="True" data-select2-enable="true">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trremark" runat="server">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trDeptCourse" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Department Courses</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSelectCourse" runat="server"
                                            AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group col-lg-8 col-md-12 col-12" id="div1" runat="server">
                                    <div class=" note-div">
                                        <h5 class="heading">Note </h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Course-Work for all the Semesters must be entered by the Supervisor till the end of first Semester.</span> </p>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <h1>
                                        <asp:Label ID="lblReasoncan" runat="server" ForeColor="Red" Font-Size="Large" Visible="false"></asp:Label>
                                    </h1>
                                </div>


                                <div class=" form-inline col-sm-12" id="trProAdm" runat="server" visible="false">
                                    Provisional Admission :
         <asp:RadioButton ID="rbYes" runat="server" Text="Yes"
             GroupName="adm" AutoPostBack="True"
             OnCheckedChanged="rbYes_CheckedChanged" />
                                    <asp:RadioButton ID="rbNo" runat="server" Text="No" Checked="true"
                                        GroupName="adm" />
                                </div>


                                <div class=" form-inline col-sm-12" id="trPayStatus" runat="server" visible="false">
                                    Payment Status :
                    <asp:RadioButtonList ID="rbPaymentStatus" runat="server"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">Yes</asp:ListItem>
                        <asp:ListItem Selected="True" Value="1">No</asp:ListItem>
                    </asp:RadioButtonList>
                                </div>

                                <div class=" form-inline col-sm-12" style="display: none">
                                    Total Subjects :
                      <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0" Width="10%"
                          Style="text-align: center;"></asp:TextBox>
                                    Total Credits :
                        <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0" Width="10%"
                            Style="text-align: center;"></asp:TextBox>
                                    <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                </div>

                                <div class=" form-inline col-sm-12" id="trEligible" runat="server" visible="false" style="background-color: #00E171">

                                    <asp:Image ID="imgEligible" runat="server" ImageUrl="~/IMAGES/success.png" />
                                    The Student is Eligible for Course Registration.
                                </div>

                                <div class=" form-inline col-sm-12" id="trUnEligible" runat="server" visible="false" style="background-color: red">

                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/IMAGES/error.png" />
                                    The Student is UnEligible for Course Registration.
                                </div>


                                <div class=" form-inline col-sm-12" id="trCancel" runat="server" visible="false" style="background-color: red">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/IMAGES/error.png" />
                                    The Student Course Registration is Cancelled.
                                </div>


                                <div class=" form-inline col-sm-12" id="tr1" runat="server" visible="false" style="background-color: #00E171">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/IMAGES/success.png" />
                                    The Student Course Registration is Confirmed.
                                </div>


                                <div class="col-sm-12" visible="false" style="display: none">

                                    <fieldset class="fieldset" style="background-color: #B9FFB9">
                                        <legend class="legend">Applicable Fees Structure</legend>
                                        <table class="table table-bordered table-hover table-fixed ">
                                            <tr>
                                                <td>Payment Type Category :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPayType" runat="server" AppendDataBoundItems="true" Width="95%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Semester:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" Width="95%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Applicable Fees:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFeeAmount" runat="server" Font-Bold="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center">
                                                    <asp:Button ID="btnModifyPType" runat="server" Text="Modify Payment Type Category"
                                                        OnClick="btnModifyPType_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>

                                </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return validateAssign();" />
                                    <asp:Button ID="btnstatus" runat="server" Text="Course Registration Status" OnClick="btnstatus_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnConfirmed" runat="server" Text="Confirm" CssClass="btn btn-primary" OnClick="btnConfirmed_Click" />

                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-warning" OnClick="btnReject_Click" />

                                    <asp:Button ID="btnPrintRegSlip" runat="server"
                                        OnClick="btnPrintRegSlip_Click" CssClass="btn btn-primary" Text="Registration Slip" OnClientClick="return ValidateRegist();" Visible="False" />

                                    <asp:Button ID="btnPrePrintClallan" runat="server" CssClass="btn btn-primary" Text="Re-Print Challan" Visible="false"
                                        OnClick="btnPrePrintClallan_Click" />

                                    <asp:Button ID="btnCancel" CssClass="btn btn-warning" runat="server" OnClick="btnCancel_Click"
                                        Text="Cancel" />

                                    <asp:Button ID="btnLock" runat="server" CssClass="btn btn-warning" OnClick="btnLock_Click"
                                        OnClientClick="return showLockConfirm();" Text="Lock" Visible="false" />

                                    <asp:HyperLink ID="hlEdit" runat="server" Text="Edit PhD Course Registration" Visible="false" NavigateUrl="~/ACADEMIC/CourseRegistrationPhdEdit.aspx?pageno=1186"></asp:HyperLink>

                                    <asp:Button
                                        ID="btnEdit" runat="server" Text="EditCourseRegistration"
                                        OnClick="btnEdit_Click" CssClass="btn btn-info" />

                                </div>

                                <div class="col-12">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example2">
                                        <asp:Repeater ID="lvHistory" runat="server" OnItemDataBound="lvHistory_ItemDataBound">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="display: none;">
                                                            <%-- Action--%>
                                                        </th>
                                                        <th>Delete
                                                        </th>
                                                        <th>Session
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Course Code & Name
                                                        </th>
                                                        <th>Type
                                                        </th>
                                                        <th>Credits
                                                        </th>
                                                    </tr>
                                                    <%--   <tr id="itemPlaceholder" runat="server" />--%>
                                                <thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="display: none;">
                                                        <asp:CheckBox ID="chkSelect" runat="server" Visible="false" ToolTip='<%#Eval("COURSENO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnDelete" TabIndex="12" runat="server" CausesValidation="false" ImageUrl="~/images/delete.gif"
                                                            CommandArgument='<%# Eval("COURSENO") %>' AlternateText='<%# Eval("COURSENO") %>' ToolTip="Select to Delete Record"
                                                            OnClientClick="return confirm('Are you sure you want to Delete?');" OnClick="btnDelete_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbReport" runat="server" OnClick="lbReport_Click"><%# Eval("SESSION_NAME") %></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("CCODE") %>' />
                                                        - 
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                        <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                        <asp:HiddenField ID="hdfIDNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                        <asp:HiddenField ID="hdfScheme" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                        <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl" runat="server" Text='<%# Eval("TYPE") %>' ForeColor="Red" />
                                                    </td>
                                                    <td style="width: 10%">
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                        
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <div id="divMsg" runat="server">
                    </div>
                    <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function totSubjects(chk) {
            //            var checkBoxid = chk.id;
            //            var txtTot = document.getElementById('<%= txtAllSubjects.ClientID %>');
            //            var txtCredits = document.getElementById('<%= txtCredits.ClientID %>');
            //            var totNoCourses = document.getElementById('<%= hdfTotNoCourses.ClientID %>').value;
            //            var credits = checkBoxid.substring(0, 50) + 'lblCredits';

            //            if (chk.checked == true) {
            //                if ((Number(txtTot.value) + 1) > totNoCourses) {
            //                    chk.checked = false;
            //                    alert('Only ' + totNoCourses + ' Courses Allowed for Exam Registration!!!');
            //                }
            //                else {
            //                    txtTot.value = Number(txtTot.value) + 1;
            //                    txtCredits.value = Number(txtCredits.value) + Number(document.getElementById('' + credits + '').innerHTML);
            //                }
            //            }
            //            else {
            //                txtTot.value = Number(txtTot.value) - 1;
            //                txtCredits.value = Number(txtCredits.value) - Number(document.getElementById('' + credits + '').innerHTML)
            //            }
        }

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtAllSubjects.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

        function SelectAll(headchk) {
            var txtTot = document.getElementById('<%= txtAllSubjects.ClientID %>');
            var txtCredits = document.getElementById('<%= txtCredits.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
            var hdfCredits = document.getElementById('<%= hdfCredits.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;
                    }
                    else {
                        e.checked = false;
                    }
                }
            }

            if (headchk.checked == true) {
                txtTot.value = hdfTot.value;
                txtCredits.value = hdfCredits.value;
            }
            else {
                txtTot.value = 0;
                txtCredits.value = 0;
            }
        }




        function validateAssign() {
            var ddlsession = document.getElementById('<%= ddlSession.ClientID %>').value;
            var ddloffered = document.getElementById('<%= ddlOfferedCourse.ClientID %>').value;
            var ddltype = document.getElementById('<%= ddlType.ClientID %>').value;
            if (ddlsession == 0) {
                alert('Please Select Session');
                return false;
            }
            else if (ddloffered == 0) {
                alert('Please Select offered Course');
                return false;
            }
            else if (ddltype == 0) {
                alert('Please Select Course Type');
                return false;
            }
            else
                return true;
        }

        function ValidateRegist() {
            var ddlsession = document.getElementById('<%=ddlSession.ClientID %>').value;
            if (ddlsession == 0) {
                alert('Please Select Session');
                return false;
            }
            else
                return true;
        }

        function MutualExclusive(radio) {
            var dvData = document.getElementById("dvData");
            var inputs = dvData.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "radio") {
                    if (inputs[i] != radio) {
                        inputs[i].checked = false;

                    }
                }
            }
        }

        function MutualExclusiveGrp(radio) {
            var dvData = document.getElementById("dvDataGrp");
            var inputs = dvData.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "radio") {
                    if (inputs[i] != radio) {
                        inputs[i].checked = false;

                    }
                }
            }
        }


        function CheckSelectionCount(chk) {
            var count = -2;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (count == 2) {
                    chk.checked = false;
                    alert("You have reached maximum limit!");
                    return;
                }
                else if (count < 2) {
                    if (e.checked == true) {
                        count += 1;
                    }
                }
                else {
                    return;
                }
            }


            function showLockConfirm() {
                var ret = confirm('Do you really want to lock marks for selected exam?');
                if (ret == true)
                    return true;
                else
                    return false;
            }
        }

    </script>

</asp:Content>

