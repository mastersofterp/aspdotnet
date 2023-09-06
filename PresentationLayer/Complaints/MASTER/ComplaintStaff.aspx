<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ComplaintStaff.aspx.cs" Inherits="Complaints_MASTER_ComplaintStaff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CREATE STAFF MEMBERS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Staff Members
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Department <span style="color: red;">*</span> </td>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlDepartmentName" Enabled="false" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlDepartmentName_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDept" runat="server"
                                                        ControlToValidate="ddlDepartmentName" Display="None" ErrorMessage="Please Select Department Name."
                                                        ValidationGroup="Staff" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Service Category Type <span style="color: red;">*</span> </td>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlComplaintNatureType" Enabled="true" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="2">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCNType" runat="server"
                                                        ControlToValidate="ddlComplaintNatureType" Display="None" ErrorMessage="Please Select Complaint Nature Type."
                                                        ValidationGroup="Staff" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Engineer/Staff Name <span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtEngStaffName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTye" runat="server" ErrorMessage="Please Enter Engineer/Staff Name."
                                                        ControlToValidate="txtEngStaffName" Display="None" ValidationGroup="Staff"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtEngStaffName"
                                                        FilterType="Custom, LowercaseLetters,UppercaseLetters" ValidChars=" ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Mobile Number <span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="15" TabIndex="4"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMobNo" runat="server" ErrorMessage="Please Enter Mobile No."
                                                        ControlToValidate="txtMobileNo" Display="None" ValidationGroup="Staff"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeMobNo" runat="server" TargetControlID="txtMobileNo"
                                                        FilterType="Numbers" ValidChars="">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Email ID:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="5" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                    Display="None" ErrorMessage="Please Enter Valid EmailID" SetFocusOnError="True"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Staff"></asp:RegularExpressionValidator>--%>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Staff" OnClick="btnSubmit_Click" CssClass="btn btn-primary" TabIndex="6" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="7" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Staff" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlList" runat="server">
                                                        <asp:ListView ID="lvStaff" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <h4 class="box-title">STAFF ENTRY LIST</h4>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Action</th>
                                                                                <th>Staff Name</th>
                                                                                <th>Department</th>
                                                                                <th>Complaint Nature Type</th>
                                                                                <th>Mobile No.</th>
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
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" TabIndex="8"
                                                                            CommandArgument='<%# Eval("STAFFID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                                    </td>
                                                                    <td><%# Eval("STAFF_NAME")%></td>
                                                                    <td><%# Eval("DEPTNAME")%></td>
                                                                    <td><%# Eval("TYPENAME")%></td>
                                                                    <td><%# Eval("MOBILENO")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

