<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RevaluationReport.aspx.cs" Inherits="ACADEMIC_RevaluationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <!-- MultiSelect Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReval"
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
    <asp:UpdatePanel ID="updReval" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">REVALUATION REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/Session</label>
                                        </div>
                                        <asp:ListBox ID="ddlCollegeSession" runat="server" AppendDataBoundItems="true" ValidationGroup="configure" TabIndex="1"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="true"></asp:ListBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College & Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select College & Session">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="StudentPhotoReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="SubjectPhotoReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="ReviewReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="ReviewApproveRefund"></asp:RequiredFieldValidator>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" ToolTip="Please Select Session">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="StudentPhotoReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="SubjectPhotoReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvReviewReport" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="ReviewReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvApprovedRefunf" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="ReviewApproveRefund"></asp:RequiredFieldValidator>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2" ToolTip="Please Select Degree" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="StudentPhotoReport"></asp:RequiredFieldValidator>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="SubjectPhotoReport"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="3" ToolTip="Please Select Branch" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Semester" runat="server">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="4" ToolTip="Please Select Semester">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Section" runat="server">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" TabIndex="5" ToolTip="Please Select Section">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Adm. Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" TabIndex="6"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Reval Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Revaluation Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRevalType" runat="server" AppendDataBoundItems="true" TabIndex="7"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Reval Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Photo Copy</asp:ListItem>
                                            <asp:ListItem Value="2">Revaluation</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlRevalType"
                                            Display="None" ErrorMessage="Please Select Revaluation Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="StudentPhotoReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlRevalType"
                                            Display="None" ErrorMessage="Please Select Revaluation Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="SubjectPhotoReport"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer mt-3">
                                <asp:Button ID="btnStudentWisePhotoReport" runat="server" Text="Student Wise Report" TabIndex="8" OnClientClick="return  validateField();"
                                    ValidationGroup="StudentPhotoReport" OnClick="btnStudentWisePhotoReport_Click" CssClass="btn btn-outline-primary" />

                                <asp:Button ID="btnSubjectWisePhotoReport" runat="server" Text="Subject Wise Report" TabIndex="9" OnClientClick="return  validateField();"
                                    ValidationGroup="SubjectPhotoReport" OnClick="btnSubjectWisePhotoReport_Click" CssClass="btn btn-outline-primary" />

                                <asp:Button ID="btnReviewReport" runat="server" Text="Review Report" TabIndex="10"
                                    ValidationGroup="ReviewReport" OnClick="btnReviewReport_Click" CssClass="btn btn-outline-primary" />

                                <asp:Button ID="btnApproved" runat="server" Text="Review Approved" TabIndex="10"
                                    ValidationGroup="ReviewApproveRefund" OnClick="btnApproved_Click" CssClass="btn btn-outline-primary" />

                                <asp:Button ID="btnRefund" runat="server" Text="Review Refund" TabIndex="10"
                                    ValidationGroup="ReviewApproveRefund" OnClick="btnRefund_Click" CssClass="btn btn-outline-primary" />

                                   <asp:Button ID="btnExcelRegStud" runat="server" Text="Excel Report" OnClientClick="return  validateField();"
                                    CssClass="btn btn-info" CausesValidation="false" TabIndex="10" Visible="true" OnClick="btnExcelRegStud_Click" /> 

                                    <asp:Button ID="btnSupplyReport" runat="server" Text="Supply Exam Reg Excel Report" OnClientClick="return  validateSupplyField();"
                                    CssClass="btn btn-info" CausesValidation="false" TabIndex="10" Visible="false" OnClick="btnSupplyReport_Click" /> 

                                <asp:Button ID="btnCancelReport" runat="server" Text="Cancel" TabIndex="11"
                                    OnClick="btnCancelReport_Click" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="StudentPhotoReport" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="SubjectPhotoReport" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="ReviewReport" />
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="ReviewApproveRefund" />

                                <div id="divMsg" runat="server">
                                </div>

                                </p>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelRegStud" /> 
            <asp:PostBackTrigger ControlID="btnSupplyReport" />
        </Triggers>
    </asp:UpdatePanel>

     <script language="javascript" type="text/javascript">

         function validateField() {
             var summary = "";
             summary += isvalidSession();
             summary += isvalidSemester();
             if (summary != "") {
                 alert(summary);
                 return false;
             }
             else {
                 return true;
             }
         }

         function validateSupplyField() {
             var summary = "";
             summary += isvalidSession();
             if (summary != "") {
                 alert(summary);
                 return false;
             }
             else {
                 return true;
             }
         }

         function isvalidSession() {
             debugger;
             var uid;
             var temp = document.getElementById("<%=ddlCollegeSession.ClientID %>");
            uid = temp.value;
            if (uid == 0) {
                return ("Please Select College/Session" + "\n");
            }
            else {
                return "";
            }
        }
        function isvalidSemester() {
            debugger;
            var uid;
            var temp = document.getElementById("<%=ddlRevalType.ClientID %>");
              uid = temp.value;
              if (uid == 0) {
                  return ("Please Select Revaluation Type" + "\n");
              }
              else {
                  return "";
              }
          }
    </script>


</asp:Content>
