<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AbscentIgradeRemove.aspx.cs" Inherits="ACADEMIC_EXAMINATION_AbscentIgradeRemove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeRemove"
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

    <asp:UpdatePanel ID="updGradeRemove" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hftimeslot" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ABSENT & I GRADE REMOVE</h3>
                            <%--<h3>
                                <asp:Label ID="lblDYAbsentIGrade" runat="server"></asp:Label>
                            </h3>--%>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelection" runat="server">
                                <div class="col-12 mb-4">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%-- <label>College/Scheme</label>--%>
                                                <asp:Label ID="lblDYddlColgScheme_Tab" runat="server" Font-Bold="true"></asp:Label>

                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College/Scheme" ValidationGroup="submit" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%-- <label>Session</label>--%>
                                                <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%--<label>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="3" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" ValidationGroup="submit" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%-- <label>Course</label>--%>
                                                <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" TabIndex="4" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                                Display="None" ErrorMessage="Please Select Course" ValidationGroup="submit" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 mb-4 ">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Absent/I Grade Entry</label>
                                                <%-- <asp:Label ID="lblDYtxtAbsentIGrade" runat="server" Font-Bold="true"></asp:Label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlAbIgEntry" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" OnSelectedIndexChanged="ddlAbIgEntry_SelectedIndexChanged" data-select2-enable="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Absent Entry</asp:ListItem>
                                                <asp:ListItem Value="2">IGrade Entry</asp:ListItem>
                                                 <asp:ListItem Value="3">UFM Entry</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAbIgEntry" runat="server" ControlToValidate="ddlAbIgEntry"
                                                Display="None" ErrorMessage="Please Select Absent Entry/IGrade Entry Grade" ValidationGroup="submit" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <%--<label>Exam Name</label>--%>
                                                <asp:Label ID="lblDYtxtExamName" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlExamName" runat="server" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamName"
                                                Display="None" ErrorMessage="Please Select Exam Name" ValidationGroup="submit" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <%-- <label>Sub Exam Name</label>--%>
                                                <asp:Label ID="lblDYtxtSubExamName" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubExamName" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvSubExamName" runat="server" ControlToValidate="ddlSubExamName"
                                                Display="None" ErrorMessage="Please Select Sub Exam Name" ValidationGroup="submit" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class=" col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-info" Font-Bold="True" TabIndex="6" OnClick="btnShow_Click" Text="Show" ValidationGroup="submit" Style="margin-top: 20px" />
                                    <asp:Button ID="btnSave" runat="server" Font-Bold="true" Text="Save" OnClick="btnSave_Click" TabIndex="7" CssClass="btn btn-success btnSaveEnabled" ValidationGroup="submit" Style="margin-top: 20px" />
                                    <asp:Button ID="btnCancel2" runat="server" Font-Bold="true" Text="Cancel" OnClick="btnCancel2_Click" TabIndex="8" CssClass="btn btn-warning" Style="margin-top: 20px" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div class="col-12">
                                   <%-- <asp:Panel ID="upRemove" runat="server" TabIndex="9">--%>
                                        <asp:ListView ID="lvAbIGRemove" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Students List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>RRN No.</th>
                                                            <th>Student Name</th>
                                                            <th>Semester</th>
                                                           <%-- <th>Absent Grade</th>
                                                            <th>IGrade</th>--%>
                                                            <th>
                                                             <asp:Label ID="AbIgade" runat="server"/>
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
                                                    <td><%# Eval("REGNO")%>
                                                        <asp:HiddenField ID="hdf_IDNO" runat="server" Value='<%#Eval("IDNO")%>' Visible="false" />
                                                    </td>
                                                    <td><%# Eval("STUDNAME")%></td>
                                                    <td><%# Eval("SEMESTERNAME")%></td>
                                                    <td>
                                                        <asp:CheckBox ID="chk_AbGrade" runat="server" class="chk_AbGrade" Checked='<%# Eval("EXTERMARK").ToString().Equals("902.00") ? true:false %>' Enabled='<%# Eval("EXTERMARK").ToString().Equals("902.00")? true:false %>' />
                                                        <asp:CheckBox ID="IGrade" runat="server" class="chk_Igrade" Checked='<%# Eval("GRADE").ToString().Equals("I") ? true:false %>' Enabled='<%# Eval("GRADE").ToString().Equals("I")? true:false %>' />
                                                          <asp:CheckBox ID="UfmGrade" runat="server" class="chk_Ufmgrade" Checked='<%# Eval("EXTERMARK").ToString().Equals("903.00") ? true:false %>' Enabled='<%# Eval("EXTERMARK").ToString().Equals("903.00")? true:false %>'  />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    <%--</asp:Panel>--%>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnShow" />--%>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

