<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Testprep_Data.aspx.cs" Inherits="ACADEMIC_REPORTS_Testprep_Data" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
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
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">DOWNLOAD/UPLOAD TESTPREP DATA</h3>
                            </div>
                            <div class="box-body">
                                <div class="form-group col-12">
                                    <asp:RadioButtonList ID="rdbFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbFilter_SelectedIndexChanged" RepeatColumns="8" RepeatDirection="Horizontal" TabIndex="1">
                                        <asp:ListItem Value="1"><span style="padding-left:5px">Download For Test Prep Data </span></asp:ListItem>
                                        <asp:ListItem Value="2"><span style="padding-left:5px">Exam Marks Upload From Test Prep </span></asp:ListItem>
                                        <asp:ListItem Value="3"><span style="padding-left:5px">Test Prep Marks Through API</span></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSession" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" runat="server" AppendDataBoundItems="true" Visible="false" TabIndex="1" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report">
                                            </asp:RequiredFieldValidator>

                                            <%--  <span style="color: red;">*</span> <b>Session<b /> </b>--%>
                                            <asp:DropDownList ID="ddlSession_1" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" runat="server" Visible="false" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession_1" Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Pending"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession_1" Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Submit">
                                            </asp:RequiredFieldValidator>

                                            <asp:DropDownList ID="ddlApiSession" AutoPostBack="true" runat="server" Visible="false" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvApiSession" runat="server" ControlToValidate="ddlApiSession" Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="apiSubmit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSchool" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>School/Institute Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSchool" runat="server" AppendDataBoundItems="true" Visible="false" AutoPostBack="true" TabIndex="1" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSchool" runat="server" ControlToValidate="ddlSchool" Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="report">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSchool2" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>School/Institute Name</label>
                                            </div>
                                             <asp:DropDownList ID="ddlSchool2" runat="server" AppendDataBoundItems="true" Visible="false" AutoPostBack="true" TabIndex="1" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSchool2" Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12" id="divAttachfile" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Attach File</label>
                                            </div>
                                             <%--<asp:FileUpload ID="fUpdExcel" runat="server" Visible="false"/>--%>
                                            <asp:FileUpload ID="fileUpload" runat="server" Visible="false" TabIndex="1"/><br />
                                            <span id="spanNote" runat="server" visible="false" style="color: red">Note : Upload .xls or .xlsx excel format only. </span>
                                            <asp:RequiredFieldValidator ID="rfvfile" runat="server" ControlToValidate="fileUpload" Display="None" ErrorMessage="Please attach Excel to upload" ValidationGroup="report"> </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divApiSubject" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Subject Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlApiSubject" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlApiSubject_OnSelectedIndexChanged" Visible="false" AutoPostBack="true" TabIndex="1" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvApiSubject" runat="server" ControlToValidate="ddlApiSubject" Display="None" ErrorMessage="Please Select Subject Name" ValidationGroup="apiSubmit" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divApiExamName" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Exam Name</label>
                                            </div>
                                             <asp:DropDownList ID="ddlApiExamName" runat="server" AppendDataBoundItems="true" Visible="false" TabIndex="1" AutoPostBack="true" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvApiExamName" runat="server" ControlToValidate="ddlApiExamName" Display="None" ErrorMessage="Please Select Exam Name" ValidationGroup="apiSubmit" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>                                   
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-primary" Visible="false" Text="Testprep Data(Excel)" OnClick="btnExcel_Click" ValidationGroup="report" TabIndex="1"/>
                                    <asp:Button ID="btnUploadData" runat="server" CssClass="btn btn-primary" Visible="false" Text="Upload Data" OnClick="btnUploadData_Click" ValidationGroup="report" TabIndex="1"/>
                                    <asp:Button ID="btnUploadPending" runat="server" CssClass="btn btn-primary" Visible="false" Text="Upload Pending (Excel)" OnClick="btnUploadPending_Click" ValidationGroup="Pending" TabIndex="1"/>
                                    <asp:Button ID="btnCancel" runat="server" Visible="false" CssClass="btn btn-warning" Text="Clear" OnClick="btnCancel_OnClick" TabIndex="1"/>
                                    <asp:Button ID="btnCancel_1" runat="server" Visible="false" CssClass="btn btn-warning" OnClientClick="Clear()" Text="Clear" OnClick="btnCancel_OnClick" TabIndex="1"/>

                                    <%--- api code   ---%>
                                    <asp:Button ID="btnApiShow" runat="server" CssClass="btn btn-primary" Visible="false" Text="Show" OnClick="btnApiShow_Click" ValidationGroup="apiSubmit" TabIndex="1"/>
                                    <asp:Button ID="btnApiCancel" runat="server" Visible="false" CssClass="btn btn-warning" OnClientClick="Clear()" Text="Clear" OnClick="btnApiCancel_OnClick" TabIndex="1"/>
                                    <asp:ValidationSummary ID="vsapi" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="apiSubmit" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" DisplayMode="List" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Pending" />
                                </div>
                                <div class="col-12" id="divExamMarksEntryExcelData" visible="false" runat="server" >
                                    <div class="sub-heading">
	                                    <h5>End Sem Exam Excel Data</h5>
                                    </div>
                                    <asp:Panel ID="pnlInfo" runat="server">
                                    <asp:ListView ID="lvExamMarksEntryExcelData" runat="server">
                                        <LayoutTemplate>
                                            <%--<div id="demo-grid" class="tableFixHead">--%>
                                                <table id="tblExamMarksEntryExcelData" class="table table-striped table-bordered nowrap display" style="width:100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr.No
                                                            </th>
                                                            <th>Registration No
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Subject Name
                                                            </th>
                                                            <th>Max Marks
                                                            </th>
                                                            <th>Marks Obtained
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            <%--</div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <%# Container.DataItemIndex +1 %>
                                                    <asp:HiddenField ID="hdfExamName" runat="server" Value='<%# Eval("ExamName") %>' />
                                                    <asp:HiddenField ID="hdfSubjectName" runat="server" Value='<%# Eval("SubjectName") %>' />
                                                    <asp:HiddenField ID="hdfRollNo" runat="server" Value='<%# Eval("RollNo") %>' />
                                                    <asp:HiddenField ID="hdfPRNNo" runat="server" Value='<%# Eval("PRNNo") %>' />
                                                    <asp:HiddenField ID="hdfName" runat="server" Value='<%# Eval("Name") %>' />
                                                    <asp:HiddenField ID="hdfMobileNo" runat="server" Value='<%# Eval("MobileNo") %>' />
                                                    <asp:HiddenField ID="hdfMaxMarks" runat="server" Value='<%# Eval("MaxMarks") %>' />
                                                    <asp:HiddenField ID="hdfMarksObtained" runat="server" Value='<%# Eval("MarksObtained") %>' />
                                                    <asp:HiddenField ID="hdfExamSubmitDate" runat="server" Value='<%# Eval("ExamSubmitDate") %>' />
                                                    <asp:HiddenField ID="hdfExamStatus" runat="server" Value='<%# Eval("ExamStatus") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("PRNNo") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Name") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SubjectName") %>
                                                </td>
                                                <td>
                                                    <%# Eval("MaxMarks") %>
                                                </td>
                                                <td>
                                                    <%# Eval("MarksObtained") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12 btn-footer mt-4">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" Visible="false" OnClick="btnSubmit_Click" ValidationGroup="Submit" TabIndex="1"/>
                                </div>
                                <div class="col-12" id="divExamMarksEntryDataThroughtApi" visible="false" runat="server">
                                    <div class="sub-heading">
	                                    <h5>Test Prep End Sem Exam Data Through Api</h5>
                                    </div>
                                     <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lstExamMarksEntryDataThroughtApi" runat="server">
                                            <LayoutTemplate>
                                                <%--<div id="demo-grid" class="tableFixHead">--%>
                                                    <table id="tblExamMarksDataThroughtApi" class="table table-striped table-bordered nowrap display" style="width:100%;">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr.No
                                                                </th>
                                                                <th>Registration No
                                                                </th>
                                                                <th>Student Name
                                                                </th>
                                                                <th>Subject Name
                                                                </th>
                                                                <th>Max Marks
                                                                </th>
                                                                <th>Marks Obtained
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                <%--</div>--%>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <%# Container.DataItemIndex +1 %>
                                                        <asp:HiddenField ID="hdfIdno" runat="server" Value='<%# Eval("CandidateId") %>' />
                                                        <asp:HiddenField ID="hdfRollNo" runat="server" Value='<%# Eval("RollNo") %>' />
                                                        <asp:HiddenField ID="hdfPRNNo" runat="server" Value='<%# Eval("PRNNo") %>' />
                                                        <asp:HiddenField ID="hdfMaxMarks" runat="server" Value='<%# Eval("TotalMarks") %>' />
                                                        <asp:HiddenField ID="hdfMarksObtained" runat="server" Value='<%# Eval("MarksObtained") %>' />
                                                        <asp:HiddenField ID="hdfExamSubmitDate" runat="server" Value='<%# Eval("ExamSubmitDate") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("PRNNo") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Name") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CourseName") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TotalMarks") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MarksObtained") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12 mt-4">
                                    <asp:Button ID="btnApiSubmit" runat="server" CssClass="btn btn-primary" Visible="false" Text="Submit" OnClick="btnApiSubmit_Click" ValidationGroup="apiSubmit" TabIndex="1"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnCancel_1" />

            <asp:PostBackTrigger ControlID="btnUploadPending" />
            <asp:PostBackTrigger ControlID="btnUploadData" />
            <asp:PostBackTrigger ControlID="rdbFilter" />

            <asp:PostBackTrigger ControlID="btnApiShow" />
            <asp:PostBackTrigger ControlID="btnApiCancel" />
            <asp:PostBackTrigger ControlID="btnApiSubmit" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>
