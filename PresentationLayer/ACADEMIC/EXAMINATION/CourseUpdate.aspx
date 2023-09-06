<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CourseUpdate.aspx.cs" Inherits="ACADEMIC_EXAMINATION_CourseUpdate" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">COURSE UPDATE</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Roll No</label>
                                </div>
                                <asp:TextBox ID="txtRollNo" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRollno" runat="server" SetFocusOnError="true"
                                    ControlToValidate="txtRollNo" Display="None" ErrorMessage="Please Enter Roll Number."
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Semester No</label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true"
                                    CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server"
                                    ControlToValidate="ddlSemester" Display="None"
                                    ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                <asp:Button ID="btnAdd" runat="server" Text="Show" OnClick="btnAdd_Click" CssClass="btn btn-primary" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="pnlStud" runat="server" Visible="false">
                        <div class="col-12">
                            <asp:ListView ID="lvStudents" runat="server"
                                OnItemDataBound="lvStudents_ItemDataBound">
                                <LayoutTemplate>
                                    <div id="listViewGrid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Course List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Course Name
                                                    </th>
                                                    <%--<th>
                                                    Old Status
                                                </th>--%>
                                                    <th>Status
                                                    </th>
                                                    <%-- <th>
                                                 Old Session
                                                </th>--%>
                                                    <th>Session
                                                    </th>
                                                    <%-- <th  style="width:10%;" align="center">
                                                   Old Semester
                                                </th>--%>
                                                    <th>Semester
                                                    </th>
                                                    <th>Old Grade
                                                    </th>
                                                    <th>New Grade
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
                                            <asp:Label ID="lblcourse" runat="server" Text='<%# Eval("COURSENAME")%>' ToolTip='<%# Eval("COURSENO")%>'></asp:Label>
                                        </td>
                                        <%--<td>
                                           <asp:Label ID="Label1" Font-Bold="true" runat="server" Text='<%# Eval("PREV")%>' ></asp:Label> 
                                        </td>--%>
                                        <td>
                                            <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                <asp:ListItem Value="0">Regular</asp:ListItem>
                                                <asp:ListItem Value="1">Backlog</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("PREV_STATUS")%>' Visible="false"></asp:Label>
                                        </td>
                                        <%-- <td>
                                           <asp:Label ID="Label2" Font-Bold="true" runat="server" Text='<%# Eval("SESSION")%>'></asp:Label> 
                                        </td>--%>
                                        <td>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:HiddenField ID="hdfsession" runat="server" Value='<%# Eval("SESSIONNO")%>' />--%>

                                            <asp:Label ID="lblsession" runat="server" Text='<%# Eval("SESSIONNO")%>' Visible="false"></asp:Label>
                                        </td>
                                        <%--<td style="width:10%;" align="center" >
                                           <asp:Label ID="Label3" Font-Bold="true" runat="server" Text='<%# Eval("SEMESTER")%>'></asp:Label> 

                                        </td>--%>
                                        <td>
                                            <asp:DropDownList ID="ddlsempro" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:HiddenField ID="hdfsempro" runat="server" Value='<%# Eval("SEMESTERNO")%>' />--%>
                                            <asp:Label ID="lblsemestr" runat="server" Text='<%# Eval("SEMESTERNO")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOldgrade" Font-Bold="true" runat="server" Text='<%# Eval("GRADE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewGrade" runat="server" Width="30px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary"/>

                        <asp:Button ID="btnProcess" runat="server" Text="Process" OnClick="btnProcess_Click" Visible="false" CssClass="btn btn-primary"/>

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

