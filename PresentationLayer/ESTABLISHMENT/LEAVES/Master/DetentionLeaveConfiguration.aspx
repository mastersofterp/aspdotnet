<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"  CodeFile="DetentionLeaveConfiguration.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_DetentionLeaveConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script type="text/javascript">
         //On Page Load
         $(document).ready(function () {
             $('#table2').DataTable();
         });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

    <style>
        .dataTables_scrollHeadInner {
            width:100% !important;
        }
        .table {
            width:100% !important;
        }
    </style>

    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">DETENTION LEAVE CONFIGURATION</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Detention Leave Configuration</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="4" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Staff Type">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                                Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave"
                                                SetFocusOnError="true" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Leave Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlleaveshrtname" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                                AppendDataBoundItems="true" AutoPostBack="True" ToolTip="Select Leave Name">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvLeave" runat="server" ControlToValidate="ddlleaveshrtname"
                                                Display="None" ErrorMessage="Please Select Leave Name" ValidationGroup="Leave"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <%--<td align="center" colspan="2" style="width: 599px">--%>
                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New" CssClass="btn btn-primary"
                                    ToolTip="Click here to Add New Detention Leave Configuration" TabIndex="2"></asp:LinkButton>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leave" OnClick="btnSave_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Save" TabIndex="3" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Return to Previous Menu" TabIndex="4" />
                               <%-- <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                                    Visible="false" OnClick="btnShowReport_Click" TabIndex="5" />--%>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leave"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="6"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />

                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <asp:Repeater ID="lvDetLeave" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>Detention Leave Configuration List</h5>
                                                </div>
                                            
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Staff Type
                                                        </th>
                                                         <th>Leave Name
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DLNO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />&nbsp;
                                                   <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PANO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STAFFTYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Leave_Name")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnShowReport" />--%>
            <asp:PostBackTrigger ControlID="btnBack" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>