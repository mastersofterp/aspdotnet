<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CommonFeedbackReport.aspx.cs" Inherits="ACADEMIC_CommonFeedbackReport"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFeed"
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

    <asp:UpdatePanel ID="updFeed" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div class="col-lg-12 col-md-12 col-12" id="divrdofeedback" runat="server">
                            <div class="row">
                                <div class="form-group col-12">

                                    <div class="label-dynamic">
                                        <%-- <sup>* </sup>
                                                                            <label></label>--%>
                                    </div>
                                    <asp:RadioButtonList ID="rdotcpartfull" runat="server" CssClass="col-4" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdotcpartfull_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">&nbsp;Faculty Feedback Report</asp:ListItem>
                                        <asp:ListItem Value="2">&nbsp;All Feedback Report</asp:ListItem>

                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="col-12" id="dvFeedbackReport" runat="server" visible="false">
                            <div class="row">
                              
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Feedback Report Type : </label>
                                        </div>
                                        <asp:DropDownList ID="ddlFeedbackReportType" AppendDataBoundItems="true" ToolTip="Please Select Feedback Type" runat="server" data-select2-enable="true"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlFeedbackReportType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Selected="false" Value="1">Faculty Feedback Report</asp:ListItem>--%>
                                            <%--<asp:ListItem Selected="false" Value="2">Faculty Feedback Report Percentage Wise</asp:ListItem>--%>
                                            <%--<asp:ListItem Selected="false" Value="3">HOD Feedback Report</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFeedbackType"
                                        Display="None" ErrorMessage="Please select Feedback Type." SetFocusOnError="true"
                                        ValidationGroup="Report" InitialValue="0" />--%>
                                    </div>
                                </div>
                            
                        </div>
                        <div class="col-lg-12 col-md-12 col-12" id="dvFaculttyFeedback" runat="server" visible="false">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--  <label>College & Scheme</label>--%>
                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                                ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="FeedbackFaculty">
                                            </asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Session</label>--%>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Please Select Session" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession1" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="FeedbackFaculty"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Semester" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                TabIndex="3" ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="FeedbackFaculty"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Section" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="sectiondv" runat="server"> </sup>
                                                <%--<label>Section</label>--%>
                                                <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" TabIndex="4" ToolTip="Please Select Section">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="rfvsection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="FeedbackFaculty"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Feedback Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlFeedbackTyp" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                TabIndex="5" ToolTip="Please Select Feedback Type" OnSelectedIndexChanged="ddlFeedbackTyp_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFeedback" runat="server" ControlToValidate="ddlFeedbackTyp"
                                                Display="None" ErrorMessage="Please Select Feedback Type" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="FeedbackFaculty"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="6" Visible="false"
                                        ValidationGroup="FeedbackFaculty" OnClick="btnShow_Click" CssClass="btn btn-primary" />

                                    <asp:Button ID="btnFacultyFeedbackReport" runat="server" Text="Faculty Feedback Report" TabIndex="6" Visible="false"
                                        ValidationGroup="FeedbackFaculty" OnClick="btnFacultyFeedbackReport_Click" CssClass="btn btn-primary" />

                                   <%-- <asp:Button ID="btnFacultyFeedbackReportPercentageWise" runat="server" Text="Faculty Feedback Report Percentage Wise" TabIndex="6"
                                        ValidationGroup="FeedbackFaculty" OnClick="btnFacultyFeedbackReportPercentageWise_Click" CssClass="btn btn-primary" Visible="false"/>--%>
                                    <asp:Button ID="btnHODFeedbackReport" runat="server" Text="HOD Feedback Report" TabIndex="6" Visible="false"
                                        ValidationGroup="FeedbackFaculty" OnClick="btnHODFeedbackReport_Click" CssClass="btn btn-primary" />

                                    <asp:Button ID="btnEvalutionReport" runat="server" Text="Student Feedback Evalution Report" TabIndex="6"
                                        ValidationGroup="FeedbackFaculty" OnClick="Button1_Click" CssClass="btn btn-primary" Visible="false" />
                                    <asp:Button ID="btnCommentReport" runat="server" Text="Feedback Comments Report" TabIndex="7"
                                        ValidationGroup="FeedbackFaculty" OnClick="btnCommentReport_Click" CssClass="btn btn-primary" Visible="false" />
                                    <asp:Button ID="btnCancelReport" runat="server" Text="Cancel" TabIndex="8"
                                        OnClick="btnCancelReport_Click" CssClass="btn btn-warning" />
                                    <%-- <asp:Button ID="Btnbackfeedback" runat="server" TabIndex="11" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false" 
                                           CssClass="btn btn-info" OnClick="Btnbackfeedback_Click" />--%>
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="FeedbackFaculty" />
                                    <div id="divMsg" runat="server">
                                    </div>
                                </div>


                                <div class="col-12">
                            <asp:ListView ID="lvFacultyDetails" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Faculty Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divFacultylist">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Teacher name
                                                    </th>
                                                    <th>Subject name
                                                    </th>
                                                    <th>Average percentage % of feedback
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
                                        <td>
                                            <%# Container.DataItemIndex + 1%>
                                        </td>                                        
                                        <td>
                                            <asp:LinkButton ID="lnkFacultyName" runat="server" Text='<%# Eval("UA_FULLNAME") %>' OnClick="lnkFacultyName_Click" ToolTip='<%# Eval("UA_NO")%>' CommandArgument='<%# Eval("COURSENO")%>'>LinkButton</asp:LinkButton>  
                                        </td>
                                        <td>
                                            <asp:Label ID="lblsem" Text='<%# Eval("CCODE") + " - " + Eval("SECTION")%>' ToolTip='<%# Eval("COURSENO")%>' runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnsection" runat="server" Value=' <%# Eval("SECTIONNO") %>' />
                                        </td>    
                                        <td>                                            
                                             <%# Eval("FEEDBACKPERCENT") %>
                                        </td>                                       
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>




                            </div>
                        </div>
                        <%--   <div  class="col-lg-12 col-md-12 col-12" id="dvallfeedback" runat="server" visible="false">--%>


                        <div class="col-12" id="dvallfeedback" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>College/Session</label>
                                        <%--<asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>--%>
                                    </div>
                                    <%--<asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>--%>
                                    <asp:ListBox runat="server" ID="ddlCollege" SelectionMode="Multiple" CssClass="form-control multi-select-demo"></asp:ListBox>
                                    <%-- <asp:ListBox ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="true"></asp:ListBox>--%>
                                    <asp:RequiredFieldValidator ID="rfvddlCollege" ControlToValidate="ddlCollege" InitialValue=""
                                        Display="None" ValidationGroup="Report" runat="server" ErrorMessage="Please select Session."></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Feedback Type : </label>
                                    </div>
                                    <asp:DropDownList ID="ddlFeedbackType" AppendDataBoundItems="true" ToolTip="Please Select Feedback Type" runat="server" TabIndex="2" data-select2-enable="true"
                                        CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvFeedbackType" runat="server" ControlToValidate="ddlFeedbackType"
                                        Display="None" ErrorMessage="Please select Feedback Type." SetFocusOnError="true"
                                        ValidationGroup="Report" InitialValue="0" />
                                </div>
                                <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Report Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbReport" runat="server" TabIndex="2" RepeatDirection="Horizontal" 
                                           CssClass="col-6">
                                            <asp:ListItem Value="1">&nbsp;Feedback Report</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;Statistical Report</asp:ListItem>
                                        <%--    <asp:ListItem Value="3">Pending Course Registration By Students</asp:ListItem>--%>
                                <%-- </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvReport" runat="server" ControlToValidate="rdbReport" SetFocusOnError="true"
                                            ErrorMessage="Please Select Report Type." Display="None" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>--%>
                            </div>
                            <div class="col-12 btn-footer">
                                <%--<asp:Button ID="btnReport" runat="server" Text="Report(Excel)"
                                    TabIndex="12" CssClass="btn btn-info" OnClick="btnReport_Click1" ValidationGroup="Report"  />--%>
                                <asp:Button ID="btnreport" runat="server" Text="Report(Excel)" TabIndex="6"
                                    ValidationGroup="Report" OnClick="btnreport_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" TabIndex="8"
                                    CssClass="btn btn-warning" OnClick="btncancel_Click" />
                                <%-- <asp:Button ID="btnBack" runat="server" TabIndex="11" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false" 
                                           CssClass="btn btn-info" OnClick="btnBack_Click" />--%>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnreport" />
            <asp:PostBackTrigger ControlID="btnCommentReport" />
            <asp:PostBackTrigger ControlID="btnHODFeedbackReport" />
        </Triggers>
    </asp:UpdatePanel>
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
</asp:Content>
