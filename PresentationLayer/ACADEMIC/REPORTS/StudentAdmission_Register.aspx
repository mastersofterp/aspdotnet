<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentAdmission_Register.aspx.cs" Inherits="ACADEMIC_StudentHorizontalReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Student Admission Register Report</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                        </asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="excel"
                                            InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="rfvddlAdmbatch" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="report"
                                            InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="Excel"
                                            InitialValue="0" />--%>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                              <label>Academic Year</label>                                               
                                            </div>
                                            <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year" ValidationGroup="RegisterReport">
                                            </asp:RequiredFieldValidator>
                                            
                                        <asp:RequiredFieldValidator ID="rfcvacdyear" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year" ValidationGroup="admYear" Visible="false" >
                                            </asp:RequiredFieldValidator>
                                    </div>

                               
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>School/Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClg" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlClg_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClg" runat="server" ControlToValidate="ddlClg"
                                            Display="None" ErrorMessage="Please Select School/Institute" SetFocusOnError="true" ValidationGroup="report" InitialValue="0" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlClg"
                                            Display="None" ErrorMessage="Please Select School/Institute" SetFocusOnError="true" ValidationGroup="RegisterReport"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divd" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Department Name</label>--%>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true" ValidationGroup="report" InitialValue="0" />
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true" ValidationGroup="Excel"
                                            InitialValue="0" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" ValidationGroup="report" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="true" ValidationGroup="report"
                                            InitialValue="0" />
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="true" ValidationGroup="RegisterReport"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Programme/Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" ValidationGroup="report" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="6">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" SetFocusOnError="true" ValidationGroup="report"
                                            InitialValue="0" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvddlSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" SetFocusOnError="true" ValidationGroup="report"
                                            InitialValue="0" />--%>
                                    </div>

                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div id="Div5" runat="server">
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblDYddlYear" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="9"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <%--<div class="form-group col-lg-3 col-md-6 col-12" id="DIVADM" runat="server" visible="false">--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divadmrounf" runat="server" visible="false">
                                        <label>Admission Round</label>
                                        <asp:DropDownList ID="ddlAdmRound" runat="server" TabIndex="8" AppendDataBoundItems="true" ssClass="form-control" data-select2-enable="true"/>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="report" OnClick="btnReport_Click" TabIndex="9" Visible="false" CssClass="btn btn-info" />
                                <asp:Button ID="btnRport1" runat="server" OnClick="btnRport1_Click" Text="Admission Registered Report" ValidationGroup="excel" TabIndex="10" Visible="false" CssClass="btn btn-info" />

                                <asp:Button ID="btnRegReport" runat="server" Text="Admission Roll List" OnClick="btnRegReport_Click"
                                    TabIndex="11" ValidationGroup="report" CssClass="btn btn-info" />

                                <asp:Button ID="Button1" runat="server" Text="Excel Report" OnClick="Button1_Click" TabIndex="12" ValidationGroup="Excel" CssClass="btn btn-info" />

                                <asp:Button ID="btnBranchcount" runat="server" Text="Branch Wise Excel" Visible="false" OnClick="btnBranchcount_Click" TabIndex="13" ValidationGroup="Excel" CssClass="btn btn-info" />
                               
                                 <asp:Button ID="btnAdmissionBatchWiseReport" 
                                    runat="server" Text="Admission Batch/Year Wise Student Data (Excel)" ValidationGroup="admYear" OnClick="btnAdmissionBatchWiseReport_Click" TabIndex="14"  CssClass="btn btn-info" />

                              <%--  OnClientClick="return validationAdmBatch();"--%>

                                <asp:Button ID="btnAdmissionRegReport"  runat="server" Text="Admission Register Student Data (Excel)" OnClick="btnAdmissionRegReport_Click" TabIndex="14"  ValidationGroup="RegisterReport" CssClass="btn btn-info"  Visible="false"  />
                                
                                <asp:Button ID="btnstudcount" runat="server" Text="Student Count Excel Report" OnClick="btnstudcount_Click" TabIndex="15" ValidationGroup="Excel" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnMothFathNotAlive" runat="server" Text="Mother/Father is not Alive Report" OnClick="btnMothFathNotAlive_Click" TabIndex="16" ValidationGroup="Excel" CssClass="btn btn-info" Visible="false"  />
                                 <asp:Button ID="btnTotalApplifeereport" runat="server" Text="Total Applicable Fee Report" OnClick="btnTotalApplifeereport_Click"  TabIndex="17"   CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="17" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="report" TabIndex="18" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="excel" TabIndex="19" />

                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Excel" TabIndex="20" />

                                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="RegisterReport" TabIndex="21" />

                                 <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="admYear" TabIndex="22" />

                            </div>

                            <div class="form-group col-lg-6 col-md-12 col-12">
                                <div class=" note-div">
                                    <h5 class="heading">Note</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span> Only Academic Year Selection is Mandatory for Excel Report.</span>  </p>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="btnRegReport" />
            <asp:PostBackTrigger ControlID="btnBranchcount" />
            <asp:PostBackTrigger ControlID="btnAdmissionBatchWiseReport" />
            <asp:PostBackTrigger ControlID="btnMothFathNotAlive" />
            <asp:PostBackTrigger ControlID="btnAdmissionRegReport" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function validationAdmBatch() {
            var admbatch = $('[id*=ctl00_ContentPlaceHolder1_ddlAcdYear]').val();
            if (admbatch == 0) {
                alert('Please Select  Academic Year');
                return false;
            }
            else return true;
        }
    </script>
</asp:Content>
