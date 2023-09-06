<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="R_CreateUser.aspx.cs"
    Inherits="Estate_R_CreateUser" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ASSIGN MAINTENANCE IN-CHARGE/ STAFF</h3>
                        </div>
                        <div class="box-body">
                           
                                <asp:Panel ID="pnlAdd" runat="server">
                                
                                          <div class="box-body">
                                              <div class="col-12">
                                                    <div class="sub-heading"><h5>Add/Edit User</h5></div>
                                             <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Service Department</label>
                                                </div>
                                                <asp:DropDownList ID="ddlRMDept" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"  OnSelectedIndexChanged="ddlRMDept_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlRMDept" Display="None" ErrorMessage="Please Select Department."
                                                    ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Employee </label>
                                                </div>
                                                <asp:DropDownList ID="ddlRMEmp" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true" data-select2-enable="true"  OnSelectedIndexChanged="ddlRMEmp_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlRMEmp" Display="None" ErrorMessage="Please Select Employee." ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trEntry" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Entry For </label>
                                                </div>
                                                <asp:DropDownList ID="ddlRMentryfor" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true" >
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEntryfor" runat="server" ControlToValidate="ddlRMentryfor" Display="None" ErrorMessage="Please Select Entry For."
                                                    ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>User Type </label>
                                                </div>
                                                <asp:CheckBox ID="ChkRMAdmin" runat="server" AutoPostBack="true" OnCheckedChanged="ChkRMAdmin_CheckedChanged" TabIndex="1" />
                                                <asp:Label ID="lblchk" runat="server"></asp:Label>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Active Status </label>
                                                </div>
                                                <asp:RadioButtonList ID="rdoStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" TabIndex="1">
                                                    <asp:ListItem Text="Active &nbsp;&nbsp;" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="DeActive" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>


                                            </div>

                                            </div>
                                                  </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="complaint" OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="1" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" />
                                                <%--  <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"  Width="70px" TabIndex="5"/>--%>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="complaint" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel runat="server" ID="pnlList">
                                                    <div class="form-group row" style="display: none;">
                                                        <div class="form-group row">
                                                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <asp:ListView ID="lvCreateUser" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            CLICK ADD TO CREATE USERS
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <div class="sub-heading"><h5>User Details</h5></div>
                                                                <table class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>ACTION</th>
                                                                            <th>EMPLOYEE NAME</th>
                                                                            <th>STATUS</th>
                                                                            <th>SERVICE DEPARTMENT</th>
                                                                            <th>ACTIVE STATUS</th>
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
                                                                    <asp:ImageButton ID="btnEdit" runat="server" TabIndex="7" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%# Eval("C_NO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                        OnClick="btnEdit_Click" />
                                                                </td>
                                                                <td><%# Eval("Employee_name")%></td>
                                                                <td><%# Eval("C_STATUS")%></td>
                                                                <td><%# Eval("DEPTNAME")%></td>
                                                                <td>
                                                                    <asp:Label ID="lblAsta" runat="server" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                </asp:Panel>
                           
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
