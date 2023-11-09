<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AuditCourseGradeAllotment.aspx.cs" Inherits="ACADEMIC_AuditCourseGradeAllotment"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
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



    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">AUDIT COURSES GRADE ALLOTMENT</h3>
                            <%-- <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Audit Course Selection</h5>
                                </div>
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College & Scheme</label>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                            ValidationGroup="Show" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSessionrpt" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvDegreerpt" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSemrpt" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>


                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Courses</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Select Course" ControlToValidate="ddlCourse" InitialValue="0"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowResult" runat="server" OnClick="btnShowResult_Click"
                                    Text="Show Data" ValidationGroup="Show" CssClass="btn btn-primary" TabIndex="1" />
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                    ValidationGroup="Show" CssClass="btn btn-primary" Visible="false" TabIndex="1" />
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Show Report"
                                    CssClass="btn btn-info" ValidationGroup="Show" Visible="false" TabIndex="1" />
                                <asp:Button ID="btnLock" runat="server"
                                    Text="Lock" CssClass="btn btn-primary" ValidationGroup="Show" OnClick="btnLock_Click" Visible="false" TabIndex="1" />

                                <asp:Button ID="btnUnlock" runat="server"
                                    Text="Unlock" CssClass="btn btn-primary" ValidationGroup="Show" OnClick="btnUnlock_Click" Visible="false" TabIndex="1" />
                                <asp:Button ID="btnCancel" runat="server"
                                    OnClick="btnCancel_Click" Text="Clear" CssClass="btn btn-warning" TabIndex="1" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="sub-heading">
                                                <h5>Students List For Audit Courses</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>

                                                        <th>RegNo
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Class RollNo
                                                        </th>
                                                        <th>Section
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Grade
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
                                            <%--<td style="width:5%">
                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' />
                                    </td>--%>
                                            <td>
                                                <%# Eval("REGNO")%>
                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("COURSENO")%>' Visible="false" />
                                            </td>
                                            <td>
                                                <%# Eval("BRANCHNAME")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRollNo" runat="server" Text='<%# Eval("ROLLNO")%>'></asp:Label>
                                                <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("IDNO")%>' ToolTip='<%# Eval("COURSENO")%>'
                                                    Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <%# Eval("SECTION")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlGrades" runat="server" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <%--   <asp:ListItem Value="1">SA</asp:ListItem>
                                                        <asp:ListItem Value="2">US</asp:ListItem>
                                                        <asp:ListItem Value="3">W</asp:ListItem>
                                                        <asp:ListItem Value="4">I</asp:ListItem>--%>
                                                    <%-- <asp:ListItem Value="NN">NN</asp:ListItem>
                                                        <asp:ListItem Value="PP">PP</asp:ListItem>--%>
                                                    <%--<asp:ListItem Value="W">W</asp:ListItem>
                                                        <asp:ListItem Value="I">I</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:Label ID="lblLock" runat="server" Text='<%# Eval("LOCKE")%>' ToolTip='<%# Eval("LOCKE")%>'
                                                    Visible="false"></asp:Label>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlGrade"
                                                            Display="None" ErrorMessage="Please Select Grade" InitialValue="0" ValidationGroup="Submit_F" />--%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
