<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PhdStudProgress.aspx.cs" Inherits="Academic_PhdStudProgress" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--FOLLOWING SCRIPT USED FOR THE ONLY DATE--%>
    <script type="text/javascript">

        function CountCharacters() {
            var maxSize = 700;

            if (document.getElementById('<%= txtDescription.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtDescription.ClientID %>');
                var len = document.getElementById('<%= txtDescription.ClientID %>').value.length;
                if (len <= maxSize) {
                    var diff = parseInt(maxSize) - parseInt(len);

                    if (document.getElementById('<%= txtRemain.ClientID %>')) {
                        document.getElementById('<%= txtRemain.ClientID %>').value = diff;
                    }
                }
                else {
                    ctrl.value = ctrl.value.substring(0, maxSize);
                }
            }

            return false;
        }
    </script>
    <%--END : FOLLOWING CODE ALLOWS THE AUTOCOMPLETE TO BE FIRED IN UPDATEPANEL--%>

    <!--Create New User-->
    <asp:Panel ID="pnDisplay" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div2" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">PHD PROGRESS REPORT </h3>
                    </div>

                    <div class="box-body">
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Search Criteria</label>
                                            </div>

                                            <%--onchange=" return ddlSearch_change();"--%>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                                            </asp:DropDownList>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                            <asp:Panel ID="pnltextbox" runat="server">
                                                <div id="divtxt" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search String</label>
                                                    </div>
                                                    <%--onkeypress="return Validate()"--%>
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                <div id="divDropDown" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <%-- <label id="lblDropdown"></label>--%>
                                                        <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>

                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <%-- OnClientClick="return submitPopup(this.name);"--%>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

                                        <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="Panellistview" runat="server">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Name
                                                                    </th>
                                                                    <th style="display: none">IdNo
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>Branch
                                                                                                    <%--<asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                    </th>
                                                                    <th>Semester
                                                                                                    <%--<asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                    </th>
                                                                    <th>Father Name
                                                                    </th>
                                                                    <th>Mother Name
                                                                    </th>
                                                                    <th>Mobile No.
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td style="display: none">
                                                        <%# Eval("idno")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FATHERNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MOTHERNAME") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("STUDENTMOBILE") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lvStudent" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <div id="divGeneralInfo" style="display: block;" class="col-12">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlId" runat="server" Visible="false">
                                    <div class="form-group col-md-4 col-md-offset-4 col-sm-12">
                                        <label><b>ID No.</b></label>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtIDNo" runat="server" class="form-control" TabIndex="1" Enabled="False" />
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgSearch" runat="server" ImageUrl="~/IMAGES/search.png" TabIndex="1" data-toggle="modal" data-target="#myModal"
                                                    AlternateText="Search" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <asp:Panel ID="pnlmainbody" runat="server">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblsession" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>ID.No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Enrollment No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Roll No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRollNo" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Degree :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegree" runat="server" Text="Ph.D" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Father's Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Date of Joining :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDateOfJoining" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Department :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Status :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Supervisor :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSupervisor" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Total No.of Credits :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCredits" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Grade Status :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblgradestatus" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="row mt-3">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>
                                                <asp:Label ID="lblCo" runat="server" Font-Bold="true" Text="Co-Supervisor"></asp:Label>
                                            </label>
                                        </div>
                                        <asp:Label ID="lblCoSupervisor" runat="server"></asp:Label>
                                    </div>
                                    <%-- ADDED FOR EXTRA SUPERVISOR--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="secoundsupervisor" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>
                                                <asp:Label ID="lbljointsup1" runat="server" Text="Joint-Supervisor" Visible="false"></asp:Label>
                                            </label>
                                        </div>
                                        <asp:Label ID="lbljointsupervisior1" runat="server"></asp:Label>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Semester No.</label>
                                        </div>
                                        <asp:Label ID="lblsemesterno" runat="server"></asp:Label>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Research Topic</label>
                                        </div>
                                        <asp:TextBox ID="txtReserchTopic" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTopic" runat="server"
                                            ErrorMessage="Please Enter Research Topic" SetFocusOnError="True" Display="None"
                                            ControlToValidate="txtReserchTopic" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row mt-3" id="divbody" runat="server">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Description of work done by student during the period</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                                            CssClass="form-control" onkeyup="return CountCharacters();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvWorkDone" runat="server" ControlToValidate="txtDescription"
                                            ErrorMessage="Please enter description of work done by student"
                                            SetFocusOnError="True" ValidationGroup="Academic" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divCountCheck" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Characters Remaining</label>
                                        </div>
                                        <asp:TextBox ID="txtRemain" runat="server" Text="" ForeColor="Red" Enabled="False" class="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div runat="server" id="divremark" visible="true">
                                    <div id="dvRemark" runat="server" class="row mt-3">
                                        <div class="col-12">
                                            <div class="sub-heading" id="tr1" runat="server">
                                                <h5>Remarks of
                                                    <asp:Label ID="lblname" runat="server"></asp:Label>
                                                    on the work done by the candidate</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divgradeaw" visible="true" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Grade Awarded </label>
                                            </div>
                                            <asp:DropDownList ID="ddlGrade" runat="server" TabIndex="15"
                                                ToolTip="Please Select Grade" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                <asp:ListItem Value="0">Satisfactory</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGrade"
                                                ErrorMessage="Please Select Grade Awarded" InitialValue="-1"
                                                SetFocusOnError="True" ValidationGroup="Academic" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="row mt-3" runat="server" id="DivDGC" visible="true">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>
                                                <asp:Label ID="lbldgc" runat="server"></asp:Label>Comments</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine"
                                            CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />

                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="131" Text="Submit" ValidationGroup="Academic" />
                                    <asp:Button ID="btnProgrApply" runat="server" CssClass="btn btn-primary" OnClick="btnProgrApply_Click" TabIndex="133" Text="Progress Confirmation Status" />

                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="133" Text="Progress Report" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="132" Text="Cancel" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Qual" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EntranceExam" />
                                </div>

                                <div class="col-12 table-responsive">
                                    <asp:Panel ID="pnlSlots" Visible="true" runat="server" Width="100%">
                                        <asp:ListView ID="lvlprogrss" runat="server" Visible="true">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Current Session Progress Report Status</h5>
                                                </div>

                                                <table id="example" class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Members
                                                            </th>
                                                            <th>Members Name
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                            <th>Remark
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
                                                    <td>Supervisor</td>
                                                    <td><%# Eval("FAC_NAME_S")%> </td>
                                                    <td><%# Eval("SUPERVISORSTATUS")%> </td>
                                                    <td><%# Eval("REMARK")%> </td>
                                                </tr>

                                                <tr>
                                                    <td><%# Eval("MEMBERNAME")%> </td>
                                                    <td><%# Eval("FAC_NAME_JS")%> </td>
                                                    <td><%# Eval("JOINTSUPERVISORSTATUS")%> </td>
                                                    <td><%# Eval("REMARKCOSUPER")%> </td>
                                                </tr>

                                                <%--<tr runat="server" visible=<%# Eval("SUPERROLE")=="T" ? true : Eval("SUPERROLE")=="S" ? false : Eval("SUPERROLE")=="J" ? false :true %>> --%>
                                                <%-- <tr runat="server" id="tr2" visible=<%# Convert.ToBoolean(Eval("SUPERROLE")=="T")==true %>>  --%>
                                                <tr id="Tr2" runat="server" visible='<%# Convert.ToBoolean(Eval("SUPROLESTATUS"))%>'>
                                                    <td>Second joint Supervisor</td>
                                                    <td><%# Eval("FAC_NAME_JSS")%> </td>
                                                    <td><%# Eval("JOINTSECONDSUPERVISORSTATUS")%> </td>
                                                    <td><%# Eval("REMARKSECCOSUPER")%> </td>
                                                </tr>
                                                <tr>
                                                    <td>Institute faculty</td>
                                                    <td><%# Eval("FAC_NAME_IF")%> </td>
                                                    <td><%# Eval("INSTITUTEFACULTYSTATUS")%> </td>
                                                    <td><%# Eval("REMARKINSTITUTE")%> </td>
                                                </tr>
                                                <tr>
                                                    <td>DRC Nominee</td>
                                                    <td><%# Eval("FAC_NAME_DRC")%> </td>
                                                    <td><%# Eval("DRCSTATUS")%> </td>
                                                    <td><%# Eval("REMARKDGC")%> </td>
                                                </tr>
                                                <tr>
                                                    <td>DRC ChairPerson</td>
                                                    <td><%# Eval("FAC_NAME_DRCC")%> </td>
                                                    <td><%# Eval("DRCCHAIRMANSTATUS")%> </td>
                                                    <td><%# Eval("REMARKDRCCHAIRMAN")%> </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        jQuery(function ($) {
            $(document).ready(function () {
                bindDataTable();
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            });
            function bindDataTable() {
                var myDT = $('#id1').DataTable({
                });
            }

        });
    </script>


    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please Select Search Criteria.')
                $(e).focus();
                return false;
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>
</asp:Content>

