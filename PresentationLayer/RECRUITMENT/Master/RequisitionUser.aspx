<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="RequisitionUser.aspx.cs" Inherits="RECRUITMENT_Master_RequisitionUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
<%--<style>
    .active-badge {
        background-color: lightgreen;
    }

    .inactive-badge {
        background-color: red;
    }
</style>--%>

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

     <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Requisition User</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Department</label>
                                </div>
                                <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" InitialValue="0" ControlToValidate="ddlDepartment"
                                 Display="None" ErrorMessage="Please Select Department" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Users</label>
                                </div>
                                <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfvUser" runat="server" InitialValue="0" ControlToValidate="ddlUser"
                                  Display="None" ErrorMessage="Please Select User" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <%--<asp:ListBox ID="lstUsers" runat="server" SelectionMode="Multiple" TabIndex="1" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>--%>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Post Category</label>
                                </div>
                                 <asp:DropDownList ID="ddlPostCategory" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Teaching</asp:ListItem>
                                    <asp:ListItem Value="2">Non Teaching</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfvPostCategory" runat="server" InitialValue="0" ControlToValidate="ddlPostCategory"
                                  Display="None" ErrorMessage="Please Select Post Category" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <%--<asp:ListBox ID="lstCategory" runat="server" SelectionMode="Multiple" TabIndex="1" CssClass="form-control multi-select-demo" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Teaching</asp:ListItem>
                                    <asp:ListItem Value="2">Non Teaching</asp:ListItem>
                                </asp:ListBox>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Allowed Post </label>
                                </div>
                               <asp:ListBox ID="lstPost" runat="server" SelectionMode="Multiple" TabIndex="4" CssClass="form-control multi-select-demo"  AppendDataBoundItems="true"></asp:ListBox>
                                 <asp:RequiredFieldValidator ID="rfvPost" runat="server" ControlToValidate="lstPost"
                                 Display="None" ErrorMessage="Please Select Allowed Post" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            <%--    <div class="border rounded p-2">
                                    <asp:CheckBoxList ID="CheckBoxList" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" TabIndex="1">
                                        <asp:ListItem Value="1"> Assistance Professor &nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2"> Professor &nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="3"> Admin &nbsp;&nbsp;</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>--%>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Status</label>
                                </div>
                                <div class="switch form-inline">
                                    <input type="checkbox" id="rdActive" name="switch" checked="checked" tabindex="5" />
                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" TabIndex="6" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="submit"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Clear" TabIndex="7" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>

                    <div class="col-12 mt-3">
                        <asp:Panel ID="pnlList" runat="server">
                         <asp:ListView ID="lvRecUser" runat="server">
                          <LayoutTemplate>
                           <div class="sub-heading">
	                        <h5>Requisition User List</h5>
                             </div>
                           <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Edit</th>
                                    <th>Department</th>
                                    <th>Users</th>
                                    <th>Post Category</th>
                                    <th>Allowed Post</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server" />
                            </tbody>
                            </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                               <tr>
                                   <td>
                                     <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("REQUNO") %>'
                                         AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="8" />

                                     <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LNO") %>'
                                                     AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                     OnClientClick="showConfirmDel(this); return false;" />--%>
                                 </td>
                                 <td>
                                     <%--<%# Eval("LEAVENAME") %>--%>
                                     <%# Eval("SUBDEPT") %>
                                 </td>
                                 <td>
                                     <%# Eval("UA_FULLNAME") %>
                                 </td>
                                 <td>
                                     <%# Eval("POSTCAT") %>
                                 </td>
                                 <td>
                                     <%# Eval("POST") %>
                                 </td>
                                 <td>
                                     <span id="statusSpan" class='<%# Convert.ToBoolean(Eval("ISACTIVE")) ? "badge badge-pill badge-success" : "badge badge-pill badge-danger" %>'>
                                    <%# Convert.ToBoolean(Eval("ISACTIVE")) ? "Active" : "Inactive" %>
                                    </span>
                                 </td>
                               </tr>
                        </ItemTemplate>
                       </asp:ListView>
                      </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });

        function SetStatRecUser(val) {
            $('#rdActive').prop('checked', val);
        }


        // JavaScript code to toggle between active and inactive states
        const statusSpan = document.getElementById('statusSpan');

        // Function to toggle the state
        function toggleState() {
            if (statusSpan.classList.contains('false')) {
                statusSpan.textContent = 'Inactive';
                statusSpan.classList.remove('badge badge-pill badge-success');
                statusSpan.classList.add('badge badge-pill badge-danger');
            } else {
                statusSpan.textContent = 'Active';
                statusSpan.classList.remove('badge badge-pill badge-danger');
                statusSpan.classList.add('badge badge-pill badge-success');
            }
        }

        // Example: Toggle the state on a button click
        document.addEventListener('DOMContentLoaded', function () {
            // Attach the toggleState function to a button click event
            const toggleButton = document.createElement('button');
            toggleButton.textContent = 'Toggle State';
            toggleButton.addEventListener('click', toggleState);
            document.body.appendChild(toggleButton);
        });
    </script>

     <div id="divMsg" runat="server">
    </div>
    </asp:Content>