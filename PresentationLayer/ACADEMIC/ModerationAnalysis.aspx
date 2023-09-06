<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ModerationAnalysis.aspx.cs" Inherits="ModerationAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .cl-grn {
            color: #70ad47;
        }

        .cl-red {
            color: #ff0000;
        }

        .cl-pur {
            color: #aa14f0;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%-- <label>Degree Type</label>--%>
                                    <asp:Label ID="lblacademic" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddladmbatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddladmbatch"
                                    Display="None" ErrorMessage="Please Select Admission Batch" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                    <%--      <label>College</label>--%>
                                </div>
                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please Select School & Scheme" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                    <%--  <label>Session</label>--%>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please Select Session" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                    <%--  <label>Semester</label>--%>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%-- <label>Student Type</label>--%>
                                    <asp:Label ID="lblDYtxtStudType" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlStudentType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true">
                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0">Regular Student</asp:ListItem>
                                    <asp:ListItem Value="1">Backlog Student</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStudentType"
                                    Display="None" ErrorMessage="Please Select Student Type" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="-1"></asp:RequiredFieldValidator>
                            </div>
                           <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%-- <label>Degree Type</label>--%>
                                  <%--  <asp:Label ID="lblDYddlDegreeType" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlDegreeType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="6" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDegreeType"
                                    Display="None" ErrorMessage="Please Select Degree Type" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>--%>

                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="Show" TabIndex="7" OnClick="btnShow_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="8" />
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                    </div>
                    <div class="col-12 mt-3">
                        <div class="row">
                            <div class="col-lg-6 col-12">
                                <asp:Panel ID="pnlProvosional" runat="server">
                                <asp:ListView ID="LvProvisional" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Provisional Result</h5>
                                        </div>
                                        <div class="table-responsive" style="height: 200px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                        <th>Degree
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Total Student
                                                        </th>
                                                        <th>Pass
                                                        </th>
                                                        <th>Fail
                                                        </th>
                                                        <th>% Pass
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
                                        <tr>
                                            <td>
                                                <%# Eval("PROGRAM")%>   
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>   
                                            </td>
                                            <td>
                                                <asp:Label ID="lbltotal" runat="server" Text='<%# Eval("TOTAL_STUDENT") %>'></asp:Label>

                                            </td>

                                            <td class="cl-grn">
                                                <asp:Label ID="lblpassstudent" runat="server" Text='<%# Eval("PASS_STUDENT") %>'></asp:Label>
                                            </td>
                                            <td class="cl-red">
                                                <asp:Label ID="lblback" runat="server" Text='<%# Eval("BACK_STUDENT") %>'></asp:Label>
                                            </td>
                                            <td class="cl-pur">
                                                <asp:Label ID="lblpassper" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                  <%-- <EmptyDataTemplate>
                                        <table  cellpadding="5" cellspacing="5">
                                            <tr>

                                                <td>No records available.
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>--%>
                                </asp:ListView>
                                    </asp:Panel>
                            </div>

                            <div class="col-lg-6 col-12">
                                <asp:Panel ID="pnlfinal" runat="server">
                                <asp:ListView ID="LvFinal" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Final Result</h5>
                                        </div>
                                        <div class="table-responsive" style="height: 200px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="Table1">
                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                        <th>Degree
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Total Student
                                                        </th>
                                                        <th>Pass
                                                        </th>
                                                        <th>Fail
                                                        </th>
                                                        <th>% Pass
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
                                        <tr>
                                            <td>
                                                <%# Eval("PROGRAM")%>   
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>   
                                            </td>
                                            <td>
                                                <asp:Label ID="lbltotal" runat="server" Text='<%# Eval("TOTAL_STUDENT") %>'></asp:Label>

                                            </td>

                                            <td class="cl-grn">
                                                <asp:Label ID="lblpassstudent" runat="server" Text='<%# Eval("PASS_STUDENT") %>'></asp:Label>
                                            </td>
                                            <td class="cl-red">
                                                <asp:Label ID="lblback" runat="server" Text='<%# Eval("BACK_STUDENT") %>'></asp:Label>
                                            </td>
                                            <td class="cl-pur">
                                                <asp:Label ID="lblpassper" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                   <%-- <EmptyDataTemplate>
                                        <table  cellpadding="5" cellspacing="5">
                                            <tr>

                                                <td>No records available.
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>--%>
                                </asp:ListView>
                                    </asp:Panel>
                            </div>
                            <%--<div class="col-lg-6 col-12">
                                <div class="sub-heading">
                                    <h5>Final Result</h5>
                                </div>
                                <div class="table-responsive" style="height: 200px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="Table1">
                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                            <tr>
                                                <th>Degree
                                                </th>
                                                <th>Semester
                                                </th>
                                                <th>Total Student
                                                </th>
                                                <th>Pass
                                                </th>
                                                <th>Fail
                                                </th>
                                                <th>% Pass
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>B Tech. - CSE</td>
                                                <td>I</td>
                                                <td>120</td>
                                                <td class="cl-grn">60</td>
                                                <td class="cl-red">60</td>
                                                <td class="cl-pur">50%</td>
                                            </tr>
                                            <tr>
                                                <td>B Tech. - CSE</td>
                                                <td>I</td>
                                                <td>100</td>
                                                <td class="cl-grn">60</td>
                                                <td class="cl-red">40</td>
                                                <td class="cl-pur">60%</td>
                                            </tr>
                                            <tr>
                                                <td>B Tech. - CSE</td>
                                                <td>I</td>
                                                <td>100</td>
                                                <td class="cl-grn">60</td>
                                                <td class="cl-red">40</td>
                                                <td class="cl-pur">60%</td>
                                            </tr>
                                            <tr>
                                                <td>B Tech. - CSE</td>
                                                <td>I</td>
                                                <td>120</td>
                                                <td class="cl-grn">60</td>
                                                <td class="cl-red">60</td>
                                                <td class="cl-pur">50%</td>
                                            </tr>
                                            <tr>
                                                <td>B Tech. - CSE</td>
                                                <td>I</td>
                                                <td>100</td>
                                                <td class="cl-grn">60</td>
                                                <td class="cl-red">40</td>
                                                <td class="cl-pur">60%</td>
                                            </tr>
                                            <tr>
                                                <td>B Tech. - CSE</td>
                                                <td>I</td>
                                                <td>100</td>
                                                <td class="cl-grn">60</td>
                                                <td class="cl-red">40</td>
                                                <td class="cl-pur">60%</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

