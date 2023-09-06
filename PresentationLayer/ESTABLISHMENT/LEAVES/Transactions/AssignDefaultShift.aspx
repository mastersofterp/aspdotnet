<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AssignDefaultShift.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_AssignDefaultShift" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)


        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>

    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ASSIGN DEFAULT SHIFT</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Criteria To Assign Default Shift</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="Shift"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Staff Type" AutoPostBack="true" OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                                Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Shift"
                                                SetFocusOnError="true" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStafftype"
                                                Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave1"
                                                SetFocusOnError="true" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trdept" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Department" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Shift</label>
                                            </div>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                TabIndex="4" ToolTip="Select Shift">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvShift" runat="server" ControlToValidate="ddlShift"
                                                Display="None" ErrorMessage="Please Select Shift" ValidationGroup="Shift"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Assign" ValidationGroup="Shift" OnClick="btnSave_Click" TabIndex="5"
                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" OnClick="btnShowReport_Click"
                                        Visible="false" ToolTip="Click here to Show Report" TabIndex="6" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="7"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />                                        
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Shift"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvEmpList" runat="server">
                                        <EmptyDataTemplate>
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees" CssClass="d-block text-center mt-3"/>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                                <div class="sub-heading">
	                                                <h5>Select Employees</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr.No
                                                            </th>
                                                            <th>
                                                                <%--<asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true" runat="server" onclick="checkAllEmployees(this)" />--%>
                                                                <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true" runat="server"
                                                                    onclick="totAllSubjects(this)" TabIndex="8" ToolTip="Check to Select All Employee" />
                                                            </th>
                                                            <th>Employee Name
                                                            </th>
                                                            <th>Shift Name
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
                                                <td>
                                                    <%#Container.DataItemIndex+1 %>
                                                </td>
                                                <td>
                                                    <%-- <asp:CheckBox ID="chkSelect" runat="server" TabIndex="9" ToolTip='<%#Eval("Idno")%>' /> Text='<%#Eval("NAME")%>'--%>
                                                    <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem'  ToolTip='<%#Eval("Idno")%>' />
                                                </td>
                                                <td>
                                                    <%#Eval("NAME")%>
                                                    <%-- <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("NAME")%>'/>--%>
                                                </td>
                                                <td>
                                                    <%#Eval("SHIFTNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                    <div class="text-center">
                                        <div class="modal-content">
                                            <div class="modal-body">
                                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                                                <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                                <div class="text-center">
                                                    <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-primary" />
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
            <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

