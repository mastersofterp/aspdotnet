<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeDetailsExcel.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ESTABLISHMENT_LEAVES_Reports_EmployeeDetailsExcel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Employee Details In Excel</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Criteria for Details In Excel</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                        CssClass="form-control" ToolTip="Select College" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please select College" SetFocusOnError="true" ValidationGroup="Report" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        CssClass="form-control" TabIndex="2" AutoPostBack="True" ToolTip="Select Staff" OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvstafftype" runat="server" ControlToValidate="ddlStaffType"
                                        Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true" ValidationGroup="Report" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        CssClass="form-control" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" ToolTip="Select Department">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="btn btn-info"
                            ToolTip="Click here to Export To Excel" TabIndex="8" ValidationGroup="Report" OnClick="btnExport_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="9"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Report"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:Repeater ID="lvEmpList" runat="server">
                                <HeaderTemplate>
                                    <div class="sub-heading">
                                        <h5>Select Employees</h5>
                                    </div>
                                    <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true" runat="server"
                                                        onclick="checkAllEmployees(this)" TabIndex="10" ToolTip="Check to Select All Employee" />
                                                </th>
                                                <th>Employee Name
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkID" runat="server" TabIndex="11" ToolTip='<%#Eval("Idno")%>' />
                                        </td>
                                        <td>
                                            <%#Eval("NAME")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody></table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;


        function checkAllEmployees(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
</asp:Content>



