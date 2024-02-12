<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="RequisitionApprovalLevels.aspx.cs" Inherits="RECRUITMENT_Master_RequisitionApprovalLevels" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
        <style>
        .dataTables_scrollHeadInner {
    width: max-content!important;
}
    </style>
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Requisition Approval Levels</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Department</label>
                                </div>
                                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" InitialValue="0" ControlToValidate="ddlDepartment"
                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Post</label>
                                </div>
                                <asp:ListBox ID="lstPost" runat="server" SelectionMode="Multiple" TabIndex="1" CssClass="form-control multi-select-demo" AppendDataBoundItems="true" ></asp:ListBox>
                            </div>
                            
                            <div class="form-group col-lg-4 col-md-6 col-12">
                                <table class="table table-striped table-bordered nowrap">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Approval Authorities</th>
                                            <th class="text-center">ADD</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlApproval" runat="server"  AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <%--<td class="text-center"><i class="fas fa-plus text-success border-success border rounded p-1"></i></td>--%>
                                            <td class="text-center"><asp:Button class="fas fa-plus text-success border-success border rounded p-1" ID="btnAddAppr" runat="server" Text="ADD" OnClick="btnAddAppr_Click" ToolTip="Add Approval Levels" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="form-group col-lg-2 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Status</label>
                                </div>
                                <div class="switch form-inline">
                                    <input type="checkbox" id="rdActive" name="switch" checked tabindex="1" />
                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                </div>
                            </div>

                        <%--     <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                       <sup>* </sup>
                                       <label>Sectional Head 01 </label>
                                   </div>
                                   <asp:DropDownList ID="ddlPA01" runat="server" TabIndex="4" ToolTip="Select Sectional Head 01" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                       AutoPostBack="True" OnSelectedIndexChanged="ddlPA01_SelectedIndexChanged">
                                       <asp:ListItem Value="0">Please Select</asp:ListItem>
                                   </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="rfvPA01" runat="server" ControlToValidate="ddlPA01"
                                       Display="None" ErrorMessage="Please select Passing Authority 01" SetFocusOnError="true"
                                       ValidationGroup="PAPath" InitialValue="0">
                                   </asp:RequiredFieldValidator>
                               </div>

                               <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                       <label>Sectional Head 02</label>
                                   </div>
                                   <asp:DropDownList ID="ddlPA02" runat="server" AppendDataBoundItems="true" TabIndex="5" ToolTip="Select Sectional Head 02" CssClass="form-control" data-select2-enable="true"
                                       Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA02_SelectedIndexChanged">
                                       <asp:ListItem Value="0">Please Select</asp:ListItem>
                                   </asp:DropDownList>
                               </div>

                               <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                       <label>Sectional Head 03 </label>
                                   </div>
                                   <asp:DropDownList ID="ddlPA03" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="6" ToolTip="Select Sectional Head 03"
                                       Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA03_SelectedIndexChanged">
                                       <asp:ListItem Value="0">Please Select</asp:ListItem>
                                   </asp:DropDownList>
                               </div>

                               <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                       <label>Sectional Head 04 </label>
                                   </div>
                                   <asp:DropDownList ID="ddlPA04" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="7" ToolTip="Select Sectional Head 04"
                                       Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA04_SelectedIndexChanged">
                                       <asp:ListItem Value="0">Please Select</asp:ListItem>
                                   </asp:DropDownList>
                               </div>

                               <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                       <label>Sectional Head 05 </label>
                                   </div>
                                   <asp:DropDownList ID="ddlPA05" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="6" ToolTip="Select Sectional Head 05"
                                       Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA05_SelectedIndexChanged">
                                       <asp:ListItem Value="0">Please Select</asp:ListItem>
                                   </asp:DropDownList>
                               </div>

                               <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                       <label>Path </label>
                                   </div>
                                   <asp:TextBox ID="txtPAPath" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine"
                                       TabIndex="7" ToolTip="Path"></asp:TextBox>
                               </div>--%>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>User</label>
                                </div>
                                <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfvUser" runat="server" InitialValue="0" ControlToValidate="ddlUser"
                                  Display="None" ErrorMessage="Please Select User" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <%--<asp:ListBox ID="lstUsers" runat="server" SelectionMode="Multiple" TabIndex="1" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>--%>
                            </div>

                            <div class="col-md-6">
                              <asp:Panel ID="pnlAl" runat="server" Visible="false">
                                  <asp:ListView ID="lvApplvl" runat="server">
                                      <LayoutTemplate>
                                          <div id="lgv1">
                                              <h4>Approval Authority level List</h4>
                                              <table class="table table-bordered table-hover">
                                                  <thead>
                                                      <tr class="bg-light-blue">
                                                          <th>Remove</th>
                                                          <th>AuthorityNo</th>
                                                          <th>Authority</th>
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
                                                 <td> <asp:ImageButton ID="btnDeleteEN" runat="server" CausesValidation="false"
                                                      CommandArgument='<%# Eval("SRNO")%>' ImageUrl="~/images/delete.gif" ToolTip="Delete Record"
                                                      EnableViewState="true" OnClick="btnDeleteEN_Click" /></td>
                                              <td><%#Eval("AuthorityNo") %></td>
                                              <td><%#Eval("Authority") %></td>
                                          </tr>
                                      </ItemTemplate>
                                                 </asp:ListView>
                                             </asp:Panel>
                                         </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="1" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  ValidationGroup="submit"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click"/>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>

                    <div class="col-12 mt-3">
                        <asp:Panel ID="pnlList" runat="server">
                         <asp:ListView ID="lvRecApprLvl" runat="server">
                          <LayoutTemplate>
                           <div class="sub-heading">
	                        <h5>Requisition Approval Level List</h5>
                             </div>
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Edit</th>
                                    <th>Department</th>
                                    <th>User</th>
                                    <th>Post</th>
                                    <th>Approval Level 1</th>
                                    <th>Approval Level 2</th>
                                    <th>Approval Level 3</th>
                                    <th>Approval Level 4</th>
                                    <th>Approval Level 5</th>
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
                                     <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("REQ_PANO") %>'
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
                                     <%# Eval("POST") %>
                                 </td>
                                   <td>
                                     <%# Eval("PANAME1") %>
                                 </td>
                                   <td>
                                     <%# Eval("PANAME2") %>
                                 </td>
                                   <td>
                                     <%# Eval("PANAME3") %>
                                 </td>
                                   <td>
                                     <%# Eval("PANAME4") %>
                                 </td>
                                   <td>
                                     <%# Eval("PANAME5") %>
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
    </script>

<div id="divMsg" runat="server">
</div>
</asp:Content>
