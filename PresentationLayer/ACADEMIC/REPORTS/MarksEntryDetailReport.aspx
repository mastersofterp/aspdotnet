<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MarksEntryDetailReport.aspx.cs" Inherits="ACADEMIC_REPORTS_MarksEntryDetailReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMarksEntryDetailReport"
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

    <asp:UpdatePanel runat="server" ID="updMarksEntryDetailReport">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Mark Entry Detail Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" runat="server" >
                                        <div class="label-dynamic">
                                           <%-- <sup>*</sup>--%>
                                    <%--   <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddldegree" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Degree" runat="server" data-select2-enable="true" TabIndex="1"
                                            >
                                              <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddldegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                    </div>--%>
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server" >
                                        <div class="label-dynamic">
                                           <%-- <sup>*</sup>--%>
                                    <%--    <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlbranch" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Branch" runat="server" data-select2-enable="true" TabIndex="2"
                                          >
                                              <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                   
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlbranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report">
                                        </asp:RequiredFieldValidator >
                                    </div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSchoolInstitute" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchoolInstitite" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select School/Institute Name" runat="server" data-select2-enable="true" TabIndex="1"
                                            OnSelectedIndexChanged="ddlSchoolInstitite_OnSelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSchoolInstitite" runat="server" ControlToValidate="ddlSchoolInstitite"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSchoolInstitite"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSchoolInstitite"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report1">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSchoolInstitite"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Excel">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlSchoolInstitite"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="SubjectWiseMarkEntry">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Session"
                                            runat="server" TabIndex="1" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSession_OnSelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report1">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Excel">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="SubjectWiseMarkEntry">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="div3">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsemester" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Semester"
                                            runat="server" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlsemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlsemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report1">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlsemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="SubjectWiseMarkEntry">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlsemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlsemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Excel">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divCourseType" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourseType" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Subject Type" runat="server" data-select2-enable="true" TabIndex="1"
                                            OnSelectedIndexChanged="ddlCourseType_OnSelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCourseType"
                                            Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCourseType"
                                            Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlCourseType"
                                            Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report1">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlCourseType"
                                            Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Excel">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlCourseType"
                                            Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="SubjectWiseMarkEntry">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="div2">
                                        <div class="label-dynamic">
                                            <%-- <sup>* </sup>--%>
                                            <label>Subject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlcourse" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Subject"
                                            runat="server" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlcourse_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlcourse"
                                            Display="None" ErrorMessage="Please Select Subject" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlsemester"
                                            Display="None" ErrorMessage="Please Select Subject" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="report1">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlcourse"
                                            Display="None" ErrorMessage="Please Select Subject" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="SubjectWiseMarkEntry">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="Show" OnClick="btnShow_OnClick" TabIndex="1" />
                                <asp:Button ID="btnreport" runat="server" Text="Subject Wise Mark Entry Report" CssClass="btn btn-primary" ValidationGroup="report" OnClick="btnreport_Click" TabIndex="1" />
                                <asp:Button ID="btnreportIE" runat="server" Text="Internal External Mark Entry Report" CssClass="btn btn-primary" ValidationGroup="report" OnClick="btnreportIE_Click" TabIndex="1" />
                                <asp:Button ID="btnIntWeightageReport" runat="server" TabIndex="1" Text="Internal Rollwise Report" ValidationGroup="report1" OnClick="btnIntWeightageReport_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnMarksReport" runat="server" TabIndex="1" Text="Internal Marks Reports" ValidationGroup="report1" OnClick="btnMarksReport_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnMarksEntry" runat="server" TabIndex="1" Text="Marks Entry Report(Excel)" OnClick="btnMarksEntry_Click" ValidationGroup="Show" Visible="false" CssClass="btn btn-primary" />
                                <asp:Button ID="btnOverallMarks" runat="server" TabIndex="1" Text="Overall Marks Entry Status" OnClick="btnOverallMarks_Click" ValidationGroup="Show" Visible="false" CssClass="btn btn-primary" />
                                <asp:Button ID="btnExcel" runat="server" TabIndex="1" Text="Mark Entry Status(Excel)" ValidationGroup="Excel" Visible="false" CssClass="btn btn-primary" OnClick="btnExcel_Click" />
                                <asp:Button ID="btnSubjectWiseMarkEntry" runat="server" TabIndex="1" Text="Subject Wise Mark Entry(Excel)" ValidationGroup="SubjectWiseMarkEntry" Visible="false" CssClass="btn btn-primary" OnClick="btnSubjectWiseMarkEntry_Click" />
                                <asp:Button ID="btnQReports" runat="server" TabIndex="1" Text="Question Wise Mark Entry(Excel)" ValidationGroup="SubjectWiseMarkEntry" Visible="false" CssClass="btn btn-primary" OnClick="btnQReports_Click" />
                                <asp:Button ID="btnblankmarkreport" runat="server" TabIndex="1" Text="Blank Mark Report" ValidationGroup="SubjectWiseMarkEntry" Visible="true" CssClass="btn btn-primary" OnClick="btnblankmarkreport_Click" />
                                <asp:Button ID="btnCancel1" runat="server" TabIndex="1" Text="Cancle" Visible="true" CssClass="btn btn-outline-danger" OnClick="btnCancel1_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="report"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="report1"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Excel"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="SubjectWiseMarkEntry"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12" runat="server" id="divCourseMarksEntryDetail" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>1)  AB for Absent Student 2) UFC for UFM(Copy case) Student 3) WDS for Withdraw Student 4) DPS for Drop Student</span>  </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <asp:GridView ID="gvParentGrid1" runat="server" DataKeyNames="COLLEGE_ID" Width="100%"
                                            CssClass="datatable" AutoGenerateColumns="false"
                                            BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="gvParentGrid1_RowDataBound" GridLines="Horizontal"
                                            ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">

                                            <HeaderStyle CssClass="bg-light-blue" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="146px" ItemStyle-HorizontalAlign="Center" HeaderText="VIEW DETAILS"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div id="divcR" runat="server">
                                                            <a href="JavaScript:divexpandcollapse('div4<%# Eval("COLLEGE_ID") %>');">
                                                                <%--<img alt="View Course Detail" id='CLOSE<%# Eval("SRNO") %>' border="0" title="VIEW COURSE DETAILS" src="~/Images/action_down.png" />--%>
                                                                <%--<asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />--%>
                                                                <asp:Image ID='CLOSE' runat="server" ImageUrl="~/Images/action_down.png" AlternateText="View Details" ToolTip="VIEW COURSE DETAILS" />
                                                        </div>
                                                        <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="COLLEGES" HeaderText="College Name" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="left" />
                                                <asp:BoundField DataField="DEGREENAME" HeaderText="Degree Name" HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="left" />
                                                <asp:BoundField DataField="BRANCHNAME" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="left" />--%>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                                <div id='div4<%# Eval("COLLEGE_ID") %>' style="display: none; position: relative; left: 30px; overflow: auto">
                                                                    <asp:GridView ID="gvChildGrid" runat="server" DataKeyNames="COLLEGE_ID" AutoGenerateColumns="false"
                                                                        CssClass="datatable" OnRowDataBound="gvChildGrid_RowDataBound"
                                                                        Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                        <HeaderStyle CssClass="bg-light-blue" />
                                                                        <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                        <RowStyle />
                                                                        <AlternatingRowStyle />
                                                                        <HeaderStyle CssClass="bg-light-blue" />
                                                                        <Columns>

                                                                            <asp:TemplateField ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderText="VIEW DETAILS"
                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <div id="divcR" runat="server">
                                                                                        <a href="JavaScript:divexpandcollapse('div5<%# Eval("SRNO") %>');">
                                                                                            <%--<img alt="View Student's Mark Detail" id='CLOSE<%# Eval("SRNO") %>' border="0" title="VIEW STUDENT MARKS DETAIL" src="~/Images/action_down.png" />--%>
                                                                                            <%--<asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />--%>
                                                                                            <asp:Image ID='CLOSE' runat="server" ImageUrl="~/Images/action_down.png" AlternateText="View Details" ToolTip="VIEW STUDENT MARKS DETAIL" />
                                                                                    </div>
                                                                                    <%--  <asp:HiddenField ID="childhdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />--%>
                                                                                    <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                    <asp:HiddenField ID="childhdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                                    <asp:HiddenField ID="childhdfSubType" runat="server" Value='<%# Eval("SUBID") %>' />
                                                                                    <asp:HiddenField ID="childhdfUANO" runat="server" Value='<%# Eval("UA_NO") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="left" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <%# Eval("CCODECOURSENAME") %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="left" />
                                                                                <ItemStyle Width="35%" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Subject Type" ItemStyle-HorizontalAlign="left" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <%# Eval("SUBJECTTYPE") %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="left" />
                                                                                <ItemStyle Width="11%" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Faculty" ItemStyle-HorizontalAlign="left" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <%# Eval("FACULTY") %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="left" />
                                                                                <ItemStyle Width="25%" />
                                                                            </asp:TemplateField>

                                                                            <%--  <asp:TemplateField HeaderText="Print Report" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="9%" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" CommandArgument='<%# Eval("COURSENO") %>' CommandName='<%# Eval("SUBID") %>' ToolTip='<%# Eval("COLLEGE_ID") %>' ValidationGroup='<%# Eval("UA_NO") %>'
                                                                                        OnClick="btnPrint_OnClick" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="left" />
                                                                                <ItemStyle Width="9%" />
                                                                            </asp:TemplateField>--%>

                                                                            <%--<%-- <asp:TemplateField HeaderText="Send Email" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="9%" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnSentEmail" runat="server" Text="Send Email" CssClass="btn btn-primary" CommandArgument='<%# Eval("UA_NO") %>'
                                                                                        OnClick="btnSentEmail_Click" ToolTip='<%# Eval("COLLEGE_ID") %>' CommandName='<%# Eval("COURSENO") %>' />
                                                                                    <%--data-toggle="modal"  data-target="#myModal1"--%>
                                                                            <%-- </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="left" />
                                                                                <ItemStyle Width="9%" />
                                                                            </asp:TemplateField>--%>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td colspan="100%">
                                                                                            <div id='div5<%# Eval("SRNO") %>' style="display: none; position: relative; left: 30px; overflow: auto">
                                                                                                <asp:GridView ID="gvChildGrid_1" runat="server" AutoGenerateColumns="false"
                                                                                                    CssClass="datatable"
                                                                                                    Width="95%" ShowFooter="true" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                                                                    <FooterStyle />
                                                                                                    <RowStyle />
                                                                                                    <AlternatingRowStyle />
                                                                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="S.no." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <%# Container.DataItemIndex+1 %>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Student/Semester Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <%# Eval("STUDNAME") %>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle Width="7%" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Registration/Roll No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center">

                                                                                                            <ItemTemplate>
                                                                                                                <%# Eval("REGROLL") %>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle Width="5%" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="M1" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <%-- <%# Eval("BINDS1") %>--%>
                                                                                                                <asp:Label ID="lblBINDSR1" runat="server" Text='<%# Eval("BINDS1") %>' Visible='<%# Eval("BINDL1").ToString() == "0" ? true : false %>' ForeColor="Red"></asp:Label>
                                                                                                                <asp:Label ID="lblBINDSG1" runat="server" Text='<%# Eval("BINDS1") %>' Visible='<%# Eval("BINDL1").ToString() == "1" ? true : false %>' ForeColor="Green"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle Width="7%" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="M2" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <%--  <%# Eval("BINDS2") %>--%>
                                                                                                                <asp:Label ID="lblBINDSR2" runat="server" Text='<%# Eval("BINDS2") %>' Visible='<%# Eval("BINDL2").ToString() == "0" ? true : false %>' ForeColor="Red"></asp:Label>
                                                                                                                <asp:Label ID="lblBINDSG2" runat="server" Text='<%# Eval("BINDS2") %>' Visible='<%# Eval("BINDL2").ToString() == "1" ? true : false %>' ForeColor="Green"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle Width="7%" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="M3" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <%-- <%# Eval("BINDS3") %>--%>
                                                                                                                <asp:Label ID="lblBINDSR3" runat="server" Text='<%# Eval("BINDS3") %>' Visible='<%# Eval("BINDL3").ToString() == "0" ? true : false %>' ForeColor="Red"></asp:Label>
                                                                                                                <asp:Label ID="lblBINDSG3" runat="server" Text='<%# Eval("BINDS3") %>' Visible='<%# Eval("BINDL3").ToString() == "1" ? true : false %>' ForeColor="Green"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle Width="7%" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="M4" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <%-- <%# Eval("BINDS4") %>--%>
                                                                                                                <asp:Label ID="lblBINDSR4" runat="server" Text='<%# Eval("BINDS4") %>' Visible='<%# Eval("BINDL4").ToString() == "0" ? true : false %>' ForeColor="Red"></asp:Label>
                                                                                                                <asp:Label ID="lblBINDSG4" runat="server" Text='<%# Eval("BINDS4") %>' Visible='<%# Eval("BINDL4").ToString() == "1" ? true : false %>' ForeColor="Green"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle Width="7%" />
                                                                                                        </asp:TemplateField>

                                                                                                        <asp:TemplateField HeaderText="M5" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <%-- <%# Eval("BINDS5") %>--%>
                                                                                                                <asp:Label ID="lblBINDSR5" runat="server" Text='<%# Eval("BINDS5") %>' Visible='<%# Eval("BINDL5").ToString() == "0" ? true : false %>' ForeColor="Red"></asp:Label>
                                                                                                                <asp:Label ID="lblBINDSG5" runat="server" Text='<%# Eval("BINDS5") %>' Visible='<%# Eval("BINDL5").ToString() == "1" ? true : false %>' ForeColor="Green"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle Width="7%" />
                                                                                                        </asp:TemplateField>

                                                                                                        <asp:TemplateField HeaderText="M6" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <%-- <%# Eval("BINDS6") %>--%>
                                                                                                                <asp:Label ID="lblBINDSR6" runat="server" Text='<%# Eval("BINDS6") %>' Visible='<%# Eval("BINDL6").ToString() == "0" &&  Eval("PUB_STATUS").ToString() == "1" ? true : false %>' ForeColor="Red"></asp:Label>
                                                                                                                <asp:Label ID="lblBINDSG6" runat="server" Text='<%# Eval("BINDS6") %>' Visible='<%# Eval("BINDL6").ToString() == "1" &&  Eval("PUB_STATUS").ToString() == "1" ? true : false %>' ForeColor="Green"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle Width="7%" />
                                                                                                        </asp:TemplateField>

                                                                                                    </Columns>
                                                                                                    <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
                    <asp:PostBackTrigger ControlID="btnShow" />
                </Triggers>--%>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnGradeCard" />--%>
            <asp:PostBackTrigger ControlID="btnMarksEntry" />
            <asp:PostBackTrigger ControlID="btnOverallMarks" />
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnSubjectWiseMarkEntry" />
            <asp:PostBackTrigger ControlID="btnQReports" />

            <%--Added By Praful on 20_01_2023--%>
        </Triggers>
    </asp:UpdatePanel>



    <div class="col-12">
        <div class="row">
            <div class="modal fade" style="padding-top: 5%;" data-backdrop="static" data-keyboard="false" aria-modal="true" id="Model_Message" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header text-center" style="background-color: #00C6D7 !important">
                            <h4 class="modal-title" style="font-style: normal; font-weight: bold; color: white">Send Mail</h4>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-12 form-group">
                                                <div class="row">
                                                    <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>To<sup>*</sup></label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txt_emailid" Enabled="false" placeholder="Email ID" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter To Email "
                                                                    ControlToValidate="txt_emailid" Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_emailid"
                                                                    Display="None" ErrorMessage="Please Enter Valid To Email " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ValidationGroup="login1"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>Subject<sup>*</sup></label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtSubject" runat="server" TabIndex="1" MaxLength="128" placeholder="Enter Subject" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject"
                                                                    ErrorMessage="Please Enter Subject" Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>Message Body<sup>*</sup></label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtBody" runat="server" TabIndex="1" MaxLength="8000" TextMode="MultiLine" placeholder="Enter Message" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBody"
                                                                    ErrorMessage="Please Enter Message  " Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="modal-footer text-center">
                                <asp:Button ID="btnSent" runat="server" Text="Send" CssClass="btn btn-primary" UseSubmitBehavior="false"
                                    OnClientClick="if (!Page_ClientValidate('EmailSend')){ return false; } this.disabled = true; this.value ='  Please Wait...';"
                                    TabIndex="1" ValidationGroup="EmailSend" OnClick="btnSent_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Close" data-dismiss="modal" OnClientClick="ClearMessageText();" CssClass="btn btn-danger" TabIndex="1" OnClick="btnCancel_Click"></asp:Button>
                                <asp:HiddenField ID="hdfReceiver_id" runat="server" />
                                <asp:ValidationSummary ID="vsMessage" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EmailSend" DisplayMode="List" />
                            </div>

                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../IMAGES/minus.png";
            }
            else {
                div.style.display = "none";
                img.src = "../IMAGES/plus.gif";
            }
        }
    </script>

    <script>
        function View(txtvalue) {
            $("#Model_Message").modal();
            document.getElementById('ctl00_ContentPlaceHolder1_txt_emailid').value = txtvalue;
            document.getElementById('ctl00_ContentPlaceHolder1_txtSubject').focus();
        }

        //Clear Message Popup
        function ClearMessageText() {
            document.getElementById('<%=txtSubject.ClientID%>').value = "";
            document.getElementById('<%=txtBody.ClientID%>').value = "";
        }
    </script>

</asp:Content>

