<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AssignBundleToFaculty.aspx.cs" Inherits="ACADEMIC_EXAMINATION_AssignBundleToFaculty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updExam"
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
    <asp:UpdatePanel ID="updExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BUNDLE ALLOTMENT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Session</label>--%>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        <asp:DropDownList ID="ddlSession" AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control"  data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Session"
                                            ControlToValidate="ddlSession" Display="None" ValidationGroup="submit" InitialValue="0" />
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Course Code</label>--%>
                                                <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="8"
                                            CssClass="form-control"  data-select2-enable="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ErrorMessage="Please Select Course"
                                            ControlToValidate="ddlCourse" Display="None" ValidationGroup="submit" InitialValue="0" />
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Bundle</label>
                                                <asp:Label ID="lblDYddlBundle" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        <asp:DropDownList ID="ddlBundle" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                            CssClass="form-control"  data-select2-enable="true" OnSelectedIndexChanged="ddlBundle_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBundle" runat="server" ErrorMessage="Please Select Bundle"
                                            ControlToValidate="ddlBundle" Display="None" ValidationGroup="submit" InitialValue="0" />
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Faculty</label>--%>
                                                <asp:Label ID="lblDYddlFaculty" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        <asp:DropDownList ID="ddlValuer" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control"  data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvValuer" runat="server" ErrorMessage="Please Select Faculty"
                                            ControlToValidate="ddlValuer" Display="None" ValidationGroup="submit" InitialValue="0" />
                                    </div>


                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Date Of Issue</label>--%>
                                                <asp:Label ID="lblDYddlDateOfIssue" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="imgIssuetDate"></i>
                                            </div>
                                            <asp:TextBox ID="txtDtOfIssue" runat="server" TabIndex="8" />
                                            <asp:RequiredFieldValidator ID="rfvDtOfIssue" runat="server" ErrorMessage="Please Select Date of Issue"
                                                ControlToValidate="txtDtOfIssue" Display="None" ValidationGroup="submit" />
                                            <ajaxToolKit:CalendarExtender ID="ceissueDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgIssuetDate" TargetControlID="txtDtOfIssue" />
                                            <ajaxToolKit:MaskedEditExtender ID="meissueDate" runat="server" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                TargetControlID="txtDtOfIssue" />
                                        </div>


                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary progress-button" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>
                            <div class="col-12">
                                <div class="row">
                                   <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Date Of Paper </label>--%>
                                                <asp:Label ID="lblDYDateofexam" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                       <%-- <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="imgPprDate"></i>
                                            </div>--%>
                                            <asp:DropDownList ID="ddlDate" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" TabIndex="2" data-select2-enable="true" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="ddlDate"
                                            Display="None" ErrorMessage="Please Select Date" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                            <asp:RequiredFieldValidator ID="rfvReportDate" runat="server" ErrorMessage="Please Select Date Of Exam"
                                                ControlToValidate="ddlDate" Display="None" ValidationGroup="Report" />
                                            
                                        </div>

                                    <div class="col-lg-3 col-md-6 col-12 form-group mt-4">
                                        <asp:Button ID="btnReport" runat="server" Text="Report" CausesValidation="false" CssClass="btn btn-info progress-button"
                                            OnClick="btnReport_Click" />
                                    </div>
                                    </div>
                                    
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvAssignBundleTOFaculty" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Allot Bundle List</h5>
                                        </div>
                                         <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <%-- <th style="width: 5%">
                                                                Action
                                                            </th>--%>
                                                    <th>Bundle No.
                                                    </th>
                                                    <%--<th style="width: 10%">Rack No.
                                                        </th>--%>
                                                    <th>Course Name
                                                    </th>
                                                    <th>Date of Paper
                                                    </th>
                                                    <th>Bundle Book Count
                                                    </th>
                                                    <th>Faculty Name
                                                    </th>
                                                    <th >Issue Date
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" >
                                            <%--<td>
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandName='<%# Eval("BUNDLEID") %>'
                                                            OnClick="btnDelete_CLick" OnClientClick="return ConfirmToDelete(this);" />
                                                    </td>--%>
                                            <td>
                                                <asp:Label ID="lblBundleNo" runat="server" Text='<%# Eval("BUNDLE")%>'></asp:Label>
                                            </td>
                                            <%--<td style="width: 10%">
                                                <asp:Label ID="lblRackNo" runat="server" Text='<%# Eval("RACKNO")%>'></asp:Label>
                                            </td>--%>
                                            <td>
                                                <asp:Label ID="lblCcode" runat="server" Text='<%# Eval("COURSE_NAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExamDate" runat="server" Text='<%# Eval("EXAMDATE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblBundleCount" runat="server" Text='<%# Eval("COUNT")%>' ToolTip='<%# Eval("COUNT")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFaculty" runat="server" Text='<%# Eval("RPNAME") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("ISSUEDATE")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

