<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentReport.aspx.cs" Inherits="Academic_StudentReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">STUDENT REPORT</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="sub-heading">
                                    <h5>Report Type</h5>
                                </div>
                                <asp:RadioButton ID="rdoVerticalReport" runat="server" Text="Vertical Report"
                                    Checked="True" GroupName="A" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoHorizontalReport" runat="server"
                                    Text="Horizontal Report" GroupName="A" />
                            </div>

                            <div class="col-lg-6 col-md-6 col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Single Student Record</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search by</label>
                                        </div>
                                        <asp:RadioButton ID="rdoEnrollmentNo" Text="Enrollment No"
                                            Visible="false" runat="server" GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoStudentName" Text="Student Name" runat="server"
                                            GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoRollNo" Text="Roll No" runat="server" GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoIdNo" Checked="true" Text="Student Id" runat="server" GroupName="stud" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-9 col-12">
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control" TabIndex="8" />
                                        <asp:RequiredFieldValidator ID="rfvSearchText" runat="server" ControlToValidate="txtSearchText"
                                            ErrorMessage="Please Enter Value" ValidationGroup="Search" SetFocusOnError="true" Display="None">
                                        </asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Search" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-3 col-12">
                                        <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click"
                                            ValidationGroup="Search" TabIndex="9" CssClass="btn btn-primary" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:ListView ID="lvStudentRecords" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Search Results</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Report
                                                </th>
                                                <th>Student Name
                                                </th>
                                                <th>Roll No.
                                                </th>
                                                <th>Degree
                                                </th>
                                                <th>Branch
                                                </th>
                                                <th>Year
                                                </th>
                                                <th>Semester
                                                </th>
                                                <th>Batch
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <EmptyDataTemplate>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnShowReport" runat="server" AlternateText="Show Report" CommandArgument='<%# Eval("IDNO") %>'
                                                ImageUrl="~/Images/print.png" ToolTip="Show Report" OnClick="btnShowReport" />
                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("ENROLLMENTNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREENO")%>
                                        </td>
                                        <td>
                                            <%# Eval("SHORTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("YEARNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("BATCHNAME")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div align="center" class="data_label">
                                        -- No Student Record Found --
                                    </div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
                            TabIndex="10" ValidationGroup="report" Visible="False" CssClass="btn btn-info" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" TabIndex="11" Visible="False" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="report" />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
